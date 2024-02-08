<%@ Page Title="" Language="C#" MasterPageFile="~/SPE.Master" AutoEventWireup="true" CodeBehind="UnitConvert.aspx.cs" Inherits="SPEWEB.F_34_Mgt.UnitConvert" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

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
             <div class="card card-fluid">
                     <div class="card-body">
                    <div class="row">
                        <div class="col-md-2">
                              <div class="form-group">
                                 
                                        <asp:Label ID="Label7" runat="server" CssClass="label" Text="Base Unit"></asp:Label>                                      
                                        <asp:DropDownList ID="ddlbUnit" runat="server" CssClass=" chzn-select form-control form-control-sm"  TabIndex="2"></asp:DropDownList>
         
                                    </div>
                        </div>
                                  <div class="col-md-2">
                              <div class="form-group">
                                      
                                        <asp:Label ID="Label1" runat="server" CssClass="label " Text="Con Unit"></asp:Label>

                                        <asp:DropDownList ID="ddlcUnit" runat="server" CssClass=" chzn-select form-control form-control-sm"  TabIndex="2"></asp:DropDownList>
                                       
                                
                                    </div>
                                        </div>
                                    <div class="col-md-1">
                                           <div class="form-group">
                                                <asp:LinkButton ID="lbtnAdd" runat="server" CssClass="btn btn-primary btn-sm" style="margin-top:20px;" OnClick="lbtnAdd_Click" TabIndex="8">Add</asp:LinkButton>

                                    </div>
                                  
                                 
                                </div>
                                      
                                  
                    </div>
                   </div>
                 </div>
            <div class="card card-fluid">
                <div class="card-body" style="min-height:350px;">
                     <asp:GridView ID="gvunit" runat="server" OnRowDeleting="gvunit_RowDeleting" AutoGenerateColumns="False" CssClass=" table-striped table-hover table-bordered grvContentarea"
                        ShowFooter="True" Style="text-align: left">
                        <PagerSettings Position="Top" />
                        <Columns>
                            <asp:TemplateField HeaderText="Sl">
                                <ItemTemplate>
                                    <asp:Label ID="lblserialnoid" runat="server" Style="text-align: right"
                                        Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle  Font-Size="12px" />
                                <ItemStyle Font-Size="12px" />
                            </asp:TemplateField>
                                                                         <asp:CommandField ShowDeleteButton="True" ControlStyle-ForeColor="Red" DeleteText="<span class='fa fa-trash'></span>" />

                                  
                            <asp:TemplateField HeaderText="Base Unit ">
                                <ItemTemplate>
                                    <asp:Label ID="lblgvStyle" runat="server"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "bcodesc")) %>'
                                        Width="110px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                              <asp:TemplateField HeaderText="QTY">
                                <ItemTemplate>
                                    <asp:Label ID="lblgvqty" runat="server" style="text-align:right;"
                                        Text='1.00'
                                        Width="30"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Conversion Unit">
                                <ItemTemplate>
                                    <asp:Label ID="lblgvColor1" runat="server"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "ccodesc")) %>'
                                        Width="110px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                           

                            <asp:TemplateField HeaderText="Con Rate">                              
                                <ItemTemplate>
                                    <asp:TextBox ID="txtgvConrat" runat="server" BackColor="Transparent"
                                        BorderStyle="None" Font-Size="12px" Style="text-align: right"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "conrat")).ToString("#,##0.000000;(#,##0.000000); ") %>'
                                        Width="100px"></asp:TextBox>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="right" />
                                <FooterStyle HorizontalAlign="Right" />
                            </asp:TemplateField>
                             <asp:TemplateField HeaderText="Note">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtgvRemarks" runat="server" BackColor="Transparent"
                                        BorderStyle="None"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "remarks")) %>'
                                        Width="110px"></asp:TextBox>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" />
                            </asp:TemplateField>


                        </Columns>
                      
                        <FooterStyle CssClass="grvFooter" />
                        <EditRowStyle />
                        <AlternatingRowStyle />
                        <PagerStyle CssClass="gvPagination" />
                        <HeaderStyle CssClass="grvHeader" />
                    </asp:GridView>



                </div>
            </div>
            


        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

