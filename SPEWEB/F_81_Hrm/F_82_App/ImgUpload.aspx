<%@ Page Title="" Language="C#" MasterPageFile="~/SPE.Master" AutoEventWireup="true" CodeBehind="ImgUpload.aspx.cs" Inherits="SPEWEB.F_81_Hrm.F_82_App.ImgUpload" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style>
        .center {
            border: 5px solid;
            margin: auto;
            padding: 10px;
        }
    </style>
    <script type="text/javascript" language="javascript">

        $(document).ready(function () {

            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);

        });

        function pageLoaded() {

            $('.chzn-select').chosen({ search_contains: true });
        }

        function showImagePreview(input) {
            document.getElementById("imgPreview").style.display = "block";
            if (input.files && input.files[0]) {
                var filerdr = new FileReader();
                filerdr.onload = function (e) {
                    $('#imgPreview').attr('src', e.target.result);
                }
                filerdr.readAsDataURL(input.files[0]);
            }
        }

        function showImagePreview1(input) {
            document.getElementById("imgPreview1").style.display = "block";
            if (input.files && input.files[0]) {
                var filerdr = new FileReader();
                filerdr.onload = function (e) {
                    $('#imgPreview1').attr('src', e.target.result);
                }
                filerdr.readAsDataURL(input.files[0]);
            }
        }

    </script>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="nahidProgressbar">
                <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1" DisplayAfter="30">
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
            <div class="card card-fluid">
                <div class="card-body">
                    <div class="row">
                        <div class="col-md-2 col-sm-2 col-lg-2">
                            <div class="form-group">
                                <asp:Label ID="lblEmpType" runat="server" CssClass="label">Employee Type</asp:Label>
                                <asp:DropDownList ID="ddlWstation" runat="server" OnSelectedIndexChanged="ddlWstation_SelectedIndexChanged" CssClass="chzn-select form-control form-contro-sm" AutoPostBack="true" TabIndex="2"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-2 col-sm-2 col-lg-2">
                            <div class="form-group">
                                <asp:Label ID="lblDiv" runat="server" CssClass="label">Division</asp:Label>
                                <asp:DropDownList ID="ddlDivision" runat="server" OnSelectedIndexChanged="ddlDivision_SelectedIndexChanged" CssClass="chzn-select form-control form-control-sm" AutoPostBack="true" TabIndex="2"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-2 col-sm-2 col-lg-2">
                            <div class="form-group">
                                <asp:Label ID="lblDept" runat="server" CssClass="label">Department</asp:Label>
                                <asp:DropDownList ID="ddlDept" runat="server" OnSelectedIndexChanged="ddlDept_SelectedIndexChanged" CssClass="chzn-select form-control form-control-sm" AutoPostBack="true" TabIndex="2"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-2 col-sm-2 col-lg-2">
                            <div class="form-group">
                                <asp:Label ID="lblSection" runat="server" CssClass="label">Section</asp:Label>
                                <asp:DropDownList ID="ddlSection" runat="server" OnSelectedIndexChanged="ddlSection_SelectedIndexChanged" CssClass="chzn-select form-control form-control-sm" AutoPostBack="true"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-2 col-sm-2 col-lg-2">
                            <div class="form-group">
                                <asp:Label ID="lblEmpLine" runat="server" CssClass="label ">Line</asp:Label>
                                <asp:DropDownList ID="ddlempline" runat="server" CssClass="chzn-select form-control form-control-sm" TabIndex="2"></asp:DropDownList>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-2 col-sm-2 col-lg-2">
                            <div class="form-group">
                                <label for="ddlLvType">
                                    Employee List
                                 <asp:LinkButton ID="ibtnEmpList" runat="server" OnClick="ibtnEmpList_Click" ToolTip="Get Employee"><i class="fa fa-search"></i></asp:LinkButton>
                                </label>
                                <asp:DropDownList ID="ddlEmpName" runat="server" CssClass="chzn-select form-control inputTxt" AutoPostBack="true" OnSelectedIndexChanged="ddlEmpName_SelectedIndexChanged">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-4 col-sm-4 col-lg-4">
                            <div class="form-group">
                                <label for="tf3">Employee Image</label>
                                <div class="custom-file">
                                    <asp:FileUpload ID="imgFileUpload" runat="server" class="custom-file-input" ToolTip="Employee Image" AllowMultiple="true" onchange="showImagePreview(this)" />
                                    <label class="custom-file-label" for="imgFileUpload">Choose file</label>
                                </div>
                            </div>
                            <img id="imgPreview" alt="Preview image" height="100" style="display: none;" />

                        </div>
                        <div class="col-md-4 col-sm-4 col-lg-4">
                            <div class="form-group">
                                <label for="tf3">Employee Signature</label>
                                <div class="custom-file">
                                    <asp:FileUpload ID="imgSigFileUpload" runat="server" class="custom-file-input" ToolTip="Employee Image" AllowMultiple="true" onchange="showImagePreview1(this)" />
                                    <label class="custom-file-label" for="imgFileUpload">Choose file</label>
                                </div>

                            </div>
                            <img id="imgPreview1" alt="Preview image" height="100" style="display: none;" />
                        </div>

                        <div class="col-lg-12 mb-2 text-center">
                            <asp:LinkButton ID="lnkbtnUpdateEMPImage" runat="server" CssClass="btn btn-success" OnClick="lnkbtnUpdateEMPImage_Click" Style="margin-top: 20px;" ToolTip="Save Image & Sign">Save</asp:LinkButton>
                        </div>
                    </div>
                </div>
            </div>
            <div class="card card-fluid" style="min-height: 350px;">
                <div class="card-body">
                    <div class="row table-responsive">
                        <asp:GridView CssClass="table-striped table-hover table-bordered grvContentarea center" ID="gvimg" runat="server" AutoGenerateColumns="false">
                            <Columns>

                                <asp:TemplateField HeaderText="Image">
                                    <ItemTemplate>
                                        <asp:Label ID="lblimg" runat="server" Text='<%#Eval("imgurl")%>' Visible="false"></asp:Label>
                                        <asp:Label ID="lblsign" runat="server" Text='<%#Eval("signurl")%>' Visible="false"></asp:Label>
                                        <asp:Label ID="lblid" runat="server" Text='<%#Eval("empid")%>' Visible="false"></asp:Label>

                                        <asp:HyperLink runat="server" NavigateUrl='<%#Eval("imgurl")%>' Target="_blank">
                                                <asp:Image Width="200px" Height="150px" runat="server" ImageUrl ='<%#Eval("imgurl")%>'/>
                                        </asp:HyperLink>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="">
                                    <ItemTemplate>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Signature">
                                    <ItemTemplate>
                                        <asp:HyperLink runat="server" NavigateUrl='<%#Eval("signurl")%>' Target="_blank">
                                            <asp:Image Width="200px" Height="150px" runat="server" ImageUrl ='<%#Eval("signurl")%>'/>
                                        </asp:HyperLink>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Action">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="btn_remove" runat="server" CssClass="btn btn-danger btn-sm" OnClick="btn_remove_Click1" ToolTip="Delete Image & Sign"> <i class="fa fa-trash"></i> 
                                        </asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="lnkbtnUpdateEMPImage" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>


