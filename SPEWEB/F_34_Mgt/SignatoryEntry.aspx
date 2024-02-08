<%@ Page Title="" Language="C#" MasterPageFile="~/SPE.Master" AutoEventWireup="true" CodeBehind="SignatoryEntry.aspx.cs" Inherits="SPEWEB.F_34_Mgt.SignatoryEntry" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <script type="text/javascript" language="javascript">

         $(document).ready(function () {

             Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);
         });
         function pageLoaded() {

             $('.chzn-select').chosen({ search_contains: true });
         }

     </script>
        <style>
        .chzn-single {
    border-radius: 3px !important;
    height: 29px !important;
    
}
    </style>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="card card-fluid">
                <div class="card-body">
                    <div class="row">
                        <div class="col-md-3">
                            <div class="form-group">
                                <asp:Label ID="Label1" runat="server" CssClass="label">Module</asp:Label>
                                        <asp:DropDownList ID="ddlModuleName" runat="server" AutoPostBack="false"
                                            CssClass="form-control form-control-sm">
                                        </asp:DropDownList>
                            </div>
                        </div>
                       
                        <div class="col-md-1">
                            <div class="form-group">
                                <asp:LinkButton ID="lnkok" runat="server" Text="Ok" Style="margin-top: 20px" AutoPostBack="false" OnClick="lnkok_Click" CssClass="btn btn-primary btn-sm"></asp:LinkButton>
                            </div>
                        </div>
                        <div class="col-md-1">
                            <div class="form-group">
                                <asp:Label ID="lblPagesize" runat="server" CssClass="control-label" Text="Page Size: "></asp:Label>
                                <asp:DropDownList ID="ddlpagesize" runat="server" AutoPostBack="True" CssClass="chzn-select"
                                    Width="85px">
                                    <asp:ListItem Value="10">10</asp:ListItem>
                                    <asp:ListItem Value="20">20</asp:ListItem>
                                    <asp:ListItem Value="30">30</asp:ListItem>
                                    <asp:ListItem Value="40">40</asp:ListItem>
                                    <asp:ListItem Value="50">50</asp:ListItem>
                                    <asp:ListItem Value="100">100</asp:ListItem>
                                    <asp:ListItem Value="200">200</asp:ListItem>
                                    <asp:ListItem Value="300">300</asp:ListItem>
                                    <asp:ListItem Value="400">400</asp:ListItem>
                                    <asp:ListItem Value="500">500</asp:ListItem>
                                    <asp:ListItem Value="1000">1000</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="clearfix"></div>
                    </div>
                </div>
            </div>
            <div class="card card-fluid" >
                <div class="card-body" style="min-height: 300px;">
                    <div class="row">
                        <div class="col-md-2">
                            <asp:Label ID="lblemp" runat="server" CssClass="lblTxt lblName" Visible="false">Employee</asp:Label>
                            <asp:DropDownList ID="ddlEmpName" runat="server" CssClass="chzn-select form-control  form-control-sm " AutoPostBack="true" Visible="false">
                            </asp:DropDownList>
                        </div>
                        <div class="col-md-1">
                            <div class="form-group">
                                <asp:LinkButton ID="lnkadd" runat="server" Text="Add" Style="margin-top: 20px" Visible="false" AutoPostBack="false" OnClick="lnkadd_Click" CssClass="btn btn-primary btn-sm"></asp:LinkButton>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                      
                    <asp:GridView ID="grvsign" runat="server" AllowPaging="True" CssClass="table-striped table-hover table-bordered grvContentarea"
                        AutoGenerateColumns="False" Font-Size="12px" PageSize="50" Width="284px" OnRowDeleting="grvsign_RowDeleting"
                        ShowFooter="True" >
                        <PagerSettings NextPageText="Next" PreviousPageText="Previous" />
                        <FooterStyle Font-Bold="True" />

                        <Columns>
                            <asp:TemplateField HeaderText="Sl">
                                <ItemTemplate>
                                    <asp:Label ID="lblserialnoid" runat="server" Style="text-align: center"
                                        Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle Font-Bold="True" Font-Size="13px" />
                                <ItemStyle Font-Size="12px" />
                            </asp:TemplateField>
                            
                                <asp:CommandField ShowDeleteButton="True" />
                               
                            <asp:TemplateField HeaderText="Id Card">
                                <ItemTemplate>
                                    <asp:Label ID="lblgridcard" runat="server"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "idcard"))%>'
                                        Width="60px"></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Font-Size="12px" />
                            </asp:TemplateField> 
                           <asp:TemplateField HeaderText="Id Card" Visible="false"> 
                                <ItemTemplate>
                                    <asp:Label ID="lblgrsigntype" runat="server"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "signtype"))%>'
                                        Width="60px"></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Font-Size="12px" />
                            </asp:TemplateField> 
                            <asp:TemplateField HeaderText="Name">
                                <%--<EditItemTemplate>
                                    <asp:TextBox ID="txtgrcode" runat="server" Font-Size="12px" Height="16px"
                                        MaxLength="5"
                                        Style="border-top-style: none; border-right-style: none; border-left-style: none; border-bottom-style: none; font-size: 12px; border-left-color: midnightblue; border-bottom-color: midnightblue; border-top-color: midnightblue; border-right-color: midnightblue;"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gcod3")) %>'
                                        Width="50px"></asp:TextBox>
                                </EditItemTemplate>--%>
                                <ItemTemplate>
                                    <asp:Label ID="lbgrcod3" runat="server" Font-Size="12px"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "signame")) %>'
                                        Width="150px"></asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:LinkButton ID="lbtnUpPer" runat="server" Font-Bold="True" OnClick="lbtnUpPer_Click" CssClass="btn btn-sm btn-secondary" >Update</asp:LinkButton>
                                </FooterTemplate>
                                <HeaderStyle Font-Bold="True" Font-Size="13px" />
                                <ItemStyle Font-Size="12px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Designation">
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtgvDesc" runat="server" Font-Size="12px" MaxLength="100"
                                        Style="border-top-style: none; border-right-style: none; border-left-style: none; border-bottom-style: none; font-size: 12px; border-left-color: midnightblue; border-bottom-color: midnightblue; border-top-color: midnightblue; border-right-color: midnightblue;"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "signdesig")) %>'
                                        Width="150px"></asp:TextBox>
                                </EditItemTemplate>

                                <ItemTemplate>
                                    <asp:Label ID="lbldesc" runat="server" Font-Size="12px"
                                        Style="font-size: 12px"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "signdesig")) %>'
                                        Width="250px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle Font-Bold="True" Font-Size="13px" HorizontalAlign="Left" />
                                <ItemStyle Font-Size="12px" HorizontalAlign="Left" />
                            </asp:TemplateField>
                         
                             <asp:TemplateField HeaderText="Active">


                                 <ItemTemplate>
                                     <asp:DropDownList ID="ddlCodeBookSegment" CssClass="chzn-select form-control form-control-sm" Enabled="false" runat="server" Width="60px">
                                         <asp:ListItem Value="0">Yes</asp:ListItem>
                                         <asp:ListItem Value="1">No</asp:ListItem>
                                     </asp:DropDownList>
                                 </ItemTemplate>
                                <HeaderStyle Font-Bold="True" Font-Size="13px" HorizontalAlign="Left" />
                                <ItemStyle Font-Size="12px" HorizontalAlign="Left" />
                            </asp:TemplateField>
                        </Columns>

                        <PagerStyle CssClass="gvPagination" />
                        <HeaderStyle CssClass="grvHeader" />
                        <FooterStyle CssClass="grvFooter" />
                        <AlternatingRowStyle BackColor="" />
                    </asp:GridView>
                        </div>

                </div>
            </div>












        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
