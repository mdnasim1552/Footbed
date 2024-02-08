<%@ Page Title="" Language="C#" MasterPageFile="~/SPE.Master" AutoEventWireup="true" CodeBehind="ConvJobPoEntry.aspx.cs" Inherits="SPEWEB.F_01_Mer.ConvJobPoEntry" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
   
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
   <script type="text/javascript">

       $(document).ready(function () {
           Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);

       });
       function pageLoaded() {

           $("input, select").bind("keydown", function (event) {
               var k1 = new KeyPress();
               k1.textBoxHandler(event);
           });

           $('.chzn-select').chosen({ search_contains: true });

       };

   </script>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="card card-fluid">
                <div class="card-body">
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

                    <div class="row">
                        <div class="col-md-1">
                            <asp:Label ID="lCurReqdate" runat="server" CssClass="lblTxt lblName" Text="Date:"></asp:Label>
                            <asp:TextBox ID="txtCurReqDate" runat="server" AutoCompleteType="Disabled" CssClass="form-control form-control-sm" ToolTip="(dd.mm.yyyy)"></asp:TextBox>
                            <cc1:CalendarExtender ID="txtCurReqDate_CalendarExtender" runat="server"
                                Enabled="True" Format="dd.MM.yyyy" TargetControlID="txtCurReqDate"></cc1:CalendarExtender>
                        </div>
                        
                        <div class="col-md-1">
                            <div class="form-group">
                                <asp:Label ID="LblSeason" runat="server" class="lblTxt" for="DdlSeason">Season</asp:Label>
                                <asp:DropDownList ID="DdlSeason" CssClass="form-control form-control-sm chzn-select" runat="server" AutoPostBack="true" OnSelectedIndexChanged="DdlSeason_SelectedIndexChanged" ></asp:DropDownList>
                            </div>
                        </div>

                        <div class="col-md-2 ">
                            <asp:LinkButton ID="ImgbtnFindOrder" CssClass="lblTxt lblName" runat="server" OnClick="ImgbtnFindOrder_Click" TabIndex="2" Text="Order No"></asp:LinkButton>
                            <asp:DropDownList ID="ddlOrderList" OnSelectedIndexChanged="ddlOrderList_SelectedIndexChanged" runat="server" CssClass="chzn-select form-control form-control-sm" AutoPostBack="True" TabIndex="3"></asp:DropDownList>
                            <asp:Label ID="lblddlOrder" runat="server" Visible="False" CssClass="form-control inputTxt"></asp:Label>
                        </div>
                        <div class="col-md-3" style="margin-top: 21px">
                            <asp:DropDownList ID="ddlStyle" OnSelectedIndexChanged="ddlStyle_SelectedIndexChanged" runat="server" CssClass="chzn-select form-control inputTxt" AutoPostBack="True" TabIndex="4"></asp:DropDownList>
                        </div>
                        <div class="col-md-2" style="margin-top: 19px">

                            <asp:LinkButton ID="lbtnOk" runat="server" CssClass="btn btn-sm btn-primary" OnClick="lbtnOk_Click">Ok</asp:LinkButton>
                         
                        </div>
                       

                    </div>


                </div>
            </div>
            </div>
            <div class="card card-fluid">
                <div class="card-body" style="min-height: 350px">
                    <div class="row">
                       <div class="table-responsive">
                            <asp:GridView ID="gv1" runat="server" AutoGenerateColumns="False" Height="1px" PageSize="15"
                                ShowFooter="True" Width="253px" CssClass=" table-striped table-hover table-bordered grvContentarea" Font-Size="11px">
                                <Columns>
                                    <asp:TemplateField HeaderText="Style ID" Visible="False">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvStyleID" runat="server" Style="text-transform: capitalize" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "styleid")) %>'
                                                Width="51px"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Order No.">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvOrderNo" runat="server" Style="text-transform: capitalize; text-align: center;"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mlcdesc")) %>'
                                                Width="160px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Justify" />
                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderText="Article">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvArticle" runat="server" Style="text-transform: capitalize; text-align: center;"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "artno")) %>'
                                                Width="160px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Justify" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Description">

                                        <ItemTemplate>
                                            <asp:Label ID="lblgvStyleDesc0" runat="server" Style="text-transform: capitalize; text-align: center;"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "discripts")) %>'
                                                Width="160px"></asp:Label>
                                        </ItemTemplate>
                                       
                                        <FooterStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Justify" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Color">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvColorDesc0" runat="server" Style="text-transform: capitalize"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "colordesc")) %>'
                                                Width="91px"></asp:Label>
                                        </ItemTemplate>
                                        
                                        <FooterStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Total">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvTotal1" runat="server" Style="font-size: 11px; text-align: right"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ordrqty")).ToString("#,##0;(#,##0); ") %>'
                                                Width="40px"></asp:Label>

                                        </ItemTemplate>
                                    
                                        <FooterStyle HorizontalAlign="Right" />
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


