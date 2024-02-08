<%@ Page Title="" Language="C#" MasterPageFile="~/SPE.Master" AutoEventWireup="true" CodeBehind="SamTagPrint.aspx.cs" Inherits="SPEWEB.F_04_Sampling.SamTagPrint" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
         <style type="text/css">
        .chzn-single {
            border-radius: 3px !important;
            height: 29px !important;
    
        }
        .multiselect {
            width: 300px !important;
            border: 1px solid;
            height: 29px;
            border-color: #cfd1d4;
            font-family: sans-serif;
        }
        .multiselect-container>li>a>label {
            margin: 0;
            height: 100%;
            cursor: pointer;
            font-weight: 400;
            padding: 2px 2px 2px 2px !important;
        }

    </style>
     <script type="text/javascript" language="javascript">       
        $(document).ready(function () {
            $(".multiselect ").addClass("btn-sm");
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);
        });
         $(document).ready(function () {
            
         });
        function pageLoaded() {          
            $('.chzn-select').chosen({ search_contains: true });
            $(function () {
                $('[id*=ddlSDI]').multiselect({
                    includeSelectAllOption: true,
                    searchable: true,
                    enableCaseInsensitiveFiltering: true,
                    maxHeight: 300,
                    innerWidth : 300

                })
                
            });

           
        }
     </script>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="nahidProgressbar">
                <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1" DisplayAfter="30">
                    <ProgressTemplate>

                        <%--  <div class="loader"></div> --%>
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
                <div class="card-body">
                    <div class="row">
                        <div class="col-md-2 col-sm-2 col-lg-2 ">
                            <asp:Label ID="Label2" runat="server" CssClass="smLbl_to text-left">Season</asp:Label>
                            <div class="form-inline">
                                <asp:DropDownList ID="DdlSeason" AutoPostBack="true" OnSelectedIndexChanged="DdlSeason_SelectedIndexChanged" CssClass="form-control form-control-sm chzn-select" Width="100%" runat="server"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-sm-2 col-md-2 col-lg-2" runat="server" id="divDdlSmpltype">
                            <asp:Label runat="server" ID="lblSmplType" class="">Sample Type</asp:Label>
                            <asp:DropDownList ID="ddlSmpltype" CssClass="form-control form-control-sm chzn-select" AutoPostBack="true" OnSelectedIndexChanged="ddlSmpltype_SelectedIndexChanged" runat="server"></asp:DropDownList>
                        </div>

                        <div class="row col-md-4 col-sm-4 col-lg-4 pading5px" id="datewise" runat="server" >
                            <div class="col-md-6 col-sm-6 col-lg-6 pading5px">
                                <asp:Label ID="Label10" runat="server" CssClass="label">Agent Name</asp:Label>
                                <asp:DropDownList ID="DdlAgent" runat="server" OnSelectedIndexChanged="DdlAgent_SelectedIndexChanged" CssClass="form-control form-control-sm chzn-select" AutoPostBack="true" TabIndex="2"></asp:DropDownList>
                            </div>

                            <div class="col-md-6 col-sm-6 col-lg-6 ">
                                <asp:Label ID="Label3" runat="server" CssClass="label">Buyer Name</asp:Label>
                                <asp:DropDownList ID="ddlBuyer" runat="server" CssClass="form-control form-control-sm chzn-select" AutoPostBack="true" OnSelectedIndexChanged="ddlBuyer_SelectedIndexChanged" TabIndex="2"></asp:DropDownList>
                            </div>
                        </div>

                        <div class="col-md-3 col-sm-3 col-lg-3 ">
                            <asp:Label ID="Label1" runat="server" CssClass="smLbl_to text-left">Article No</asp:Label>
                           
                            <div class="Multidropdown">
                                <asp:ListBox ID="ddlSDI" SelectionMode="Multiple" CssClass="form-control form-control-sm " runat="server"></asp:ListBox>

                            </div>
                        </div>

                        <div class="col-md-1">
                            <div class="form-group">
                                <asp:LinkButton ID="lbtnOk" runat="server" Text="Ok" OnClick="lbtnOk_Click" CssClass="btn btn-primary btn-sm" Style="margin-top: 20px" TabIndex="4"></asp:LinkButton>

                            </div>

                        </div>

                    </div>
                </div>
            </div>

            <div class="card card-fluid">
                <div class="card-body" style="min-height: 350px">
                    <asp:MultiView ID="MultiView1" runat="server" ActiveViewIndex="0">
                        <asp:View ID="View1" runat="server">

                        </asp:View>
                        <asp:View ID="View2" runat="server">
                            <div class="row table-responsive">
                            <asp:GridView ID="gvPackList" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                                ShowFooter="True" CssClass="table-striped table-hover table-bordered grvContentarea">
                                <PagerSettings Visible="False" />
                                <RowStyle />
                                <Columns>
                                    <asp:TemplateField HeaderText="Sl">
                                        <ItemTemplate>
                                            <asp:Label ID="gvlblSlNo" runat="server" Height="16px" Style="text-align: right"
                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Art No">
                                        <ItemTemplate>
                                            <asp:Label ID="gvlblArtNo" runat="server" Height="16px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "article")) %>' CssClass="mx-3"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Size">
                                        <ItemTemplate>
                                            <asp:Label ID="gvlblSize" runat="server" Height="16px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "samsize")) %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Upper" >
                                        <ItemTemplate>
                                            <asp:Label ID="gvlblUpper" runat="server" Height="16px" CssClass="mx-3"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "samupper")) %>' ></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Qty (Pcs)">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvMSRResDesc" runat="server" CssClass="mx-3"
                                                Text='<%# Convert.ToDecimal(DataBinder.Eval(Container.DataItem, "samqty")).ToString("F2") %>'> </asp:Label>
                                        </ItemTemplate>

                                        <HeaderStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Sample Type">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvspcfdesc" runat="server" CssClass="mx-3"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gdesc")) %>' ></asp:Label>
                                        </ItemTemplate>

                                        <HeaderStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderText="Customer">
                                        <ItemTemplate>
                                            <asp:Label ID="gvlblCus" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "cust")) %>' CssClass="mx-3"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <FooterStyle CssClass="grvFooter" />
                                <EditRowStyle />
                                <AlternatingRowStyle />
                                <PagerStyle CssClass="gvPagination" />
                                <HeaderStyle CssClass="grvHeader" />
                            </asp:GridView>
                        </div>
                        </asp:View>
                    </asp:MultiView>
                    
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>






