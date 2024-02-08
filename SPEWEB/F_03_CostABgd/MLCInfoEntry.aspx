<%@ Page Title="" Language="C#" MasterPageFile="~/SPE.Master" AutoEventWireup="true" CodeBehind="MLCInfoEntry.aspx.cs" Inherits="SPEWEB.F_03_CostABgd.MLCInfoEntry" %>

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

        }

    </script>

    <style>
        .lcgeninfo .form-group {
            margin-bottom: 0rem;
        }

        .chzn-single {
            border-radius: 3px !important;
            height: 29px !important;
        }

        .uploadedimg .image {
            opacity: 1;
            display: block;
            width: 100%;
            height: auto;
            transition: .5s ease;
            backface-visibility: hidden;
        }

        .uploadedimg:hover .image {
            opacity: 0.3;
        }

        .uploadedimg:hover .middle {
            opacity: 1;
        }
    </style>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>

            <div class="card card-fluid">
                <div class="card-body">
                    <div class="row">
                        <div class="col-md-3 ">
                            <div class="form-group">
                                <asp:Label ID="Label16" runat="server" CssClass="label" Text="Buyer"></asp:Label>
                                <asp:DropDownList ID="ddlBuyer" runat="server" CssClass="form-control form-control-sm chzn-select" AutoPostBack="true" OnSelectedIndexChanged="ddlBuyer_SelectedIndexChanged" TabIndex="3"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <asp:Label ID="lblDept" runat="server" CssClass="label">Master L/C</asp:Label>
                                <asp:DropDownList ID="ddlMLC" runat="server" AutoPostBack="true" CssClass="form-control form-control-sm chzn-select" TabIndex="2">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <asp:Label ID="Label1" runat="server" CssClass="label">Select Article</asp:Label>
                                <asp:DropDownList ID="dllorderType" runat="server" CssClass=" form-control form-control-sm chzn-select"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-1">
                            <div class="form-group" style="margin-top: 20px;">
                                <asp:LinkButton ID="lbtnOk" runat="server" Text="Ok" OnClick="lbtnOk_Click" CssClass="btn btn-primary btn-sm" TabIndex="4"></asp:LinkButton>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <div class="card card-fluid">
                <div class="card-body">
                    <div class="row" style="min-height: 500px;">
                        <div class="col-md-7">
                            <asp:MultiView ID="MultiView1" runat="server">
                                <asp:View ID="ViewLcInfo" runat="server">
                                    <div class="row lcgeninfo">

                                        <asp:GridView ID="gvPersonalInfo" runat="server" AutoGenerateColumns="False"
                                            ShowFooter="True" Width="810px" CssClass="table-striped table-hover table-bordered grvContentarea"
                                            OnRowDataBound="gvPersonalInfo_RowDataBound">
                                            <RowStyle />
                                            <Columns>

                                                <asp:TemplateField HeaderText="SL.">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvSlNo0" runat="server" Font-Bold="True" Height="16px"
                                                            Style="text-align: right"
                                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Code">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvItmCode" runat="server" Height="16px"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gcod")) %>'
                                                            Width="100px"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Type">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvgval" runat="server" Height="16px"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gval")) %>'
                                                            Width="20px"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Description">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lgcResDesc1" runat="server"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gdesc")) %>'
                                                            Width="200px"></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvgph" runat="server" Height="16px"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gph")) %>'
                                                            Width="2px"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle Font-Bold="True" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Type" Visible="False">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lgvgval" runat="server"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gval")) %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField>
                                                    <%--<FooterTemplate>
                                                    <asp:LinkButton ID="lUpdatPerInfo" runat="server" CssClass="btn btn-danger btn-sm" OnClick="lUpdatPerInfo_Click">Update LC Info</asp:LinkButton>
                                                </FooterTemplate>--%>
                                                    <ItemTemplate>

                                                        <asp:TextBox ID="txtgvVal" runat="server" BackColor="Transparent"
                                                            BorderColor="#660033" BorderStyle="None" BorderWidth="1px" Height="16px"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gdesc1")) %>'
                                                            Width="437px"></asp:TextBox>

                                                        <asp:TextBox ID="txtgvdVal" runat="server" BackColor="Transparent"
                                                            BorderColor="#660033" BorderStyle="None" BorderWidth="1px" Height="16px"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gdesc1")) %>'
                                                            Width="437px"></asp:TextBox>

                                                        <cc1:CalendarExtender ID="txtgvdVal_CalendarExtender" runat="server"
                                                            Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txtgvdVal"></cc1:CalendarExtender>

                                                        <asp:Panel ID="Panegrd" runat="server">
                                                            <div class="form-group">
                                                                <div class="col-md-12 pading5px">
                                                                    <asp:DropDownList ID="ddlval" runat="server" OnSelectedIndexChanged="ddlval_SelectedIndexChanged" CssClass="form-control form-control-sm  chzn-select " Width="350" TabIndex="2">
                                                                    </asp:DropDownList>
                                                                </div>
                                                            </div>
                                                        </asp:Panel>
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

                                        <div class="col-md-3" style="display: none;">
                                            <div class="panel panel-default">
                                                <div class="panel-heading">
                                                    <span class="fa fa-file-o"></span>UPLOAD
                                                <div class="pull-right">
                                                    <cc1:AsyncFileUpload OnClientUploadError="uploadError" Width="50" CssClass="btn btn-xs btn-success"
                                                        OnClientUploadComplete="uploadComplete" runat="server"
                                                        ID="AsyncFileUpload1" UploaderStyle="Modern"
                                                        CompleteBackColor="" Visible="True"
                                                        UploadingBackColor="#CCFFFF" ThrobberID="imgLoader"
                                                        OnUploadedComplete="FileUploadComplete" />
                                                    <asp:Image ID="imgLoader" runat="server" Visible="false" ImageUrl="~/images/Wait.gif" />
                                                </div>
                                                </div>
                                                <div class="panel-body">
                                                    <div class="col-md-4 col-md-offset-4">
                                                        <a id="btnLink" class="uploadedimg" runat="server" target="_blank">
                                                            <asp:Image ID="LoadImg" Visible="false" runat="server" CssClass="img img-responsive" />
                                                        </a>
                                                    </div>
                                                    <asp:Label ID="lblMesg" runat="server" Text=""></asp:Label>
                                                </div>
                                            </div>


                                        </div>

                                    </div>
                                </asp:View>

                            </asp:MultiView>
                        </div>

                        <div class="col-md-5" runat="server" id="importFrom" visible="false">
                            <div class="row">
                                <div class="col-md-4 form-group">
                                    <asp:Label ID="Label2" runat="server" CssClass="label">Master L/C</asp:Label>
                                    <asp:DropDownList ID="ddlMLC2" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlMLC2_SelectedIndexChanged" CssClass="form-control form-control-sm chzn-select">
                                    </asp:DropDownList>
                                </div>
                                <div class="col-md-5 form-group">
                                    <asp:Label ID="Label3" runat="server" CssClass="label">Select Article</asp:Label>
                                    <asp:DropDownList ID="dllorderType2" runat="server" CssClass=" form-control form-control-sm chzn-select"></asp:DropDownList>
                                </div>

                                <div class="col-md-3 form-group" style="margin-top:20px">
                                    <asp:LinkButton ID="btnImport" runat="server" Text="Import From" CssClass="btn btn-outline-primary btn-sm" OnClick="btnImport_Click"></asp:LinkButton>
                                </div>
                            </div>
                        </div>

                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>

    <script type="text/javascript">
        function uploadComplete(sender) {
            $get("<%=lblMesg.ClientID%>").style.color = "green";
            $get("<%=lblMesg.ClientID%>").innerHTML = "File Uploaded Successfully";
        }

        function uploadError(sender) {
            $get("<%=lblMesg.ClientID%>").style.color = "red";
            $get("<%=lblMesg.ClientID%>").innerHTML = "File upload failed.";
        }


    </script>
</asp:Content>


