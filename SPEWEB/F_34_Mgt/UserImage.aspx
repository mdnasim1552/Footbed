<%@ Page Title="" Language="C#" MasterPageFile="~/SPE.Master" AutoEventWireup="true" CodeBehind="UserImage.aspx.cs" Inherits="SPEWEB.F_34_Mgt.UserImage" %>


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

    
    
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
              <div class="card card-fluid">
                <div class="card-body">

                    <div class="row">

                        <div class="col-md-2 col-sm-2 col-lg-2 ">
                            <div class="form-group">
                                <asp:Label ID="Label1" runat="server" CssClass="label">User Name</asp:Label>
                                <asp:DropDownList ID="ddlUserName" runat="server" CssClass="chzn-select form-control form-control-sm"></asp:DropDownList>
                            </div>
                        </div>

                        
                    </div>
                </div>
            </div>
            <div class="card card-fluid">
                <div class="card-body">

                    <div class="row" style="min-height: 350px;">
                        <div class="col-md-1 col-sm-1 col-lg-1 ">
                            <div class="form-group">
                                <asp:Label ID="Label2" runat="server" CssClass="label">User Image</asp:Label>
                                <asp:Image ID="UserImg" runat="server" />
                                        <%--<asp:Label ID="Label11" runat="server" Text="User Image:" Width="100px"></asp:Label>--%>

                            </div>

                        </div>
                        <div class="col-md-2 col-sm-2 col-lg-2 ">
                            <div class="form-group">
                                <asp:FileUpload ID="imgFileUpload" runat="server" Height="26px"
                                                    onchange="submitform();" Width="216px" />

                            </div>

                        </div>
                    </div>
                </div>
            </div>



            


            

        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>


