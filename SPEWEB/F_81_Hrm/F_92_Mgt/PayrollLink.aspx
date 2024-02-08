<%@ Page Title="" Language="C#" MasterPageFile="~/SPE.Master" AutoEventWireup="true" CodeBehind="PayrollLink.aspx.cs" Inherits="SPEWEB.F_81_Hrm.F_92_Mgt.PayrollLink" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript" language="javascript">
        $(document).ready(function () {
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);

        });
        function pageLoaded() {

            $("input, select").bind("keydown", function (event) {
                var k1 = new KeyPress();
                k1.textBoxHandler(event);
            });
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
                       <div class="col-md-2">
                                <div class="form-group">                                    
                                        <asp:Label ID="lblUser1" runat="server" CssClass="control-label">User Name</asp:Label>
                                        <asp:TextBox ID="txtUserSearch1"  runat="server" style="display:none;" CssClass=" inputTxt inputName inpPixedWidth"></asp:TextBox>
                                        <asp:LinkButton ID="ImgbtnFindUser1" runat="server" style="display:none;"  CssClass="  btn btn-primary srearchBtn" OnClick="ImgbtnFindUser1_Click"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>
                                     <asp:DropDownList ID="ddlUserList" runat="server" CssClass="form-control inputTxt pull-left chzn-select" AutoPostBack="true" TabIndex="2">
                                        </asp:DropDownList>
                                    </div>
                           </div>
                        <div class="col-md-2">
                                    <div class="form-group">      
                                       

                                        <div class="pull-left">
                                            <asp:LinkButton ID="lbtnOk" runat="server" CssClass="btn btn-primary btn-sm" style="margin-top:20px;" OnClick="lbtnOk_Click">Ok</asp:LinkButton>

                                        </div>

                                    </div>
                                   

                                </div>
                        
                            </div>
                    </div>
                    </div>
                   
             <div class="card card-fluid" style="min-height: 280px;">
                <div class="card-body">
                    <asp:Panel ID="Panel2" runat="server" Visible="False">
                        <div class="row">
                                                      
                                        <div class="col-md-3">
                                           
                                             <div class="form-group">
                                            <asp:Label ID="lblConTrolCode" runat="server" CssClass="control-label">Division Code:</asp:Label>
                                            <asp:TextBox ID="txtCompSearch" style="display:none;" runat="server" CssClass="inputTxt inputName inpPixedWidth"></asp:TextBox>
                                           
                                                  <div class="input-group input-group-alt">
                            <div class="input-group-prepend">
                            <asp:LinkButton ID="ImgbtnFindComp" CssClass="input-group-text"  runat="server"  OnClick="ImgbtnFindComp_Click"><span class="fa fa-search"> </span></asp:LinkButton>
                            </div>
                          
                                                 
                                                 
                                             <asp:DropDownList ID="ddlCompany" runat="server" Width="233" CssClass="form-control form-control-sm chzn-select" AutoPostBack="true" TabIndex="2">
                                            </asp:DropDownList>
                                                      </div>
                                        </div>
                                            </div>
                                        <div class="col-md-4">
                                            <div class="form-group">

                                          
                                                <asp:LinkButton ID="lbtnSelectSupl1" runat="server" style="margin-top:20px;" CssClass="btn btn-primary btn-sm pull-left" OnClick="lbtnSelectSupl1_Click">Select</asp:LinkButton>

                                            
                                                  </div>
                                        </div>

                                    </div>
                               
                       <asp:GridView ID="gvPayrollLinkInfo" runat="server" CssClass=" table-striped table-hover table-bordered grvContentarea"
                            AutoGenerateColumns="False" ShowFooter="True" Width="16px"
                            OnRowDeleting="gvPayrollLinkInfo_RowDeleting">
                            <PagerSettings Visible="False" />
                            <RowStyle />
                            <Columns>
                                <asp:TemplateField HeaderText="Sl.No.">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvSlNo0" runat="server" Height="16px"
                                            Style="text-align: right"
                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="35px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:CommandField ShowDeleteButton="True" />

                                <asp:TemplateField HeaderText="Company Code" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvCompCod" runat="server" Height="16px"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actcode")) %>'
                                            Width="80px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Company Name">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvCompDesc" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc")) %>'
                                            Width="350px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle HorizontalAlign="Left" />
                                    <HeaderStyle HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Remarks">
                                    <FooterTemplate>
                                        <asp:LinkButton ID="lbtnUpdate" runat="server" OnClick="lbtnUpdate_Click" CssClass="btn btn-danger btn-sm" Width="90px">Final Update</asp:LinkButton>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtgvRemarks" runat="server" BorderColor="#99CCFF"
                                            BorderStyle="Solid" BorderWidth="0px" Font-Size="11px"
                                            Style="text-align: left; background-color: Transparent"
                                            Text='<%# DataBinder.Eval(Container.DataItem, "remarks").ToString() %>'
                                            Width="150px"></asp:TextBox>
                                    </ItemTemplate>
                                    <FooterStyle HorizontalAlign="left" />
                                    <HeaderStyle HorizontalAlign="Left" />
                                </asp:TemplateField>
                            </Columns>
                            <FooterStyle CssClass="grvFooter" />
                            <EditRowStyle />
                            <AlternatingRowStyle />
                            <PagerStyle CssClass="gvPagination" />
                            <HeaderStyle CssClass="grvHeader" />

                        </asp:GridView>


                    </asp:Panel>
                </div>
            </div>



        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>


