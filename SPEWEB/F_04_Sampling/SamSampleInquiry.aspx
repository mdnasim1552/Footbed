<%@ Page Title="" Language="C#" MasterPageFile="~/SPE.Master" AutoEventWireup="true" CodeBehind="SamSampleInquiry.aspx.cs" Inherits="SPEWEB.F_04_Sampling.SamSampleInquiry" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript" language="javascript">
        function OpenGenCode(CodeType, name) {
            // alert(name)
            $("#exampleModalSm h5").html("<span>Open " + name + " New Code</span>");
            $("#txtCodeType").val(CodeType);
        }
        function openModal() {
            $('#myModal').modal('show');
        }
        function CLoseMOdal() {
            $('body').removeClass('modal-open');
            $('.modal-backdrop').remove();
            $('.modal ').modal('hide');
        }

        function DrawerModal() {
            $('#exampleModalDrawerRight').modal('show');
        }
        function CloseModal_AlrtMsg() {


            $('body').removeClass('modal-open');
            $('.modal-backdrop').remove();
            $('.modal ').modal('hide');

            openModal();
        };

        $(document).ready(function () {

            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);
        });
        function pageLoaded() {

            $('.chzn-select').chosen({ search_contains: true });
        }

    </script>
    <style type="text/css">
        .chzn-single {
            border-radius: 3px !important;
            height: 29px !important;
        }

        .UpdateMOdel {
            position: fixed;
            margin: 0;
            width: 100%;
            height: 100%;
            padding: 0;
        }

        .allmaterial .modal-dialog {
            max-width: 100% !important;
        }

        .ConStylewithddelbtn {
            width: 15px !important;
        }

        .sample .form-group {
            margin-bottom: 0.3rem;
        }
    </style>

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
                        <div class="col-md-2">
                            <div class="form-group">
                                <asp:Label ID="Label1" runat="server" CssClass="label">Date</asp:Label>
                                <asp:TextBox ID="txtDatefrom" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
                                <cc1:CalendarExtender ID="datefrom" runat="server" Format="dd-MMM-yyyy" TargetControlID="txtDatefrom"></cc1:CalendarExtender>
                            </div>
                        </div>
                        <div class="col-md-2">
                            <div class="form-group">
                                <asp:Label ID="lblLcName" runat="server" CssClass="label">SDI No</asp:Label>
                                <div class="form-inline">
                                    <asp:Label ID="lblCurNo1" Text="SDI00" runat="server" CssClass="form-control form-control-sm"></asp:Label>
                                    <asp:Label ID="lblCurNo2" Text="00000" runat="server" CssClass="form-control form-control-sm inputName"></asp:Label>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-1">
                            <div class="form-group">
                                <asp:Label ID="Label7" runat="server" CssClass="label">Version</asp:Label>

                                <asp:DropDownList ID="DdlVersion" CssClass="form-control from-control-sm chzn-select" runat="server">
                                </asp:DropDownList>

                            </div>
                        </div>
                        <div class="col-md-1">
                            <div class="form-group">
                                <asp:LinkButton ID="lbtnOk" runat="server" Text="Ok" OnClick="lbtnOk_Click" CssClass="btn btn-primary btn-sm" TabIndex="4" Style="margin-top: 20px"></asp:LinkButton>
                            </div>
                        </div>
                        <div class="col-md-2">
                            <div class="form-group">
                                <asp:LinkButton ID="LbtnArticleHistory" runat="server" Text="Article History" OnClick="LbtnArticleHistory_Click" CssClass="btn btn-warning btn-sm" TabIndex="4" Style="margin-top: 20px"></asp:LinkButton>
                            </div>
                        </div>
                        <div class="col-md-2">
                            <div class="form-group">
                                <asp:LinkButton ID="ibtnPreList" runat="server" CssClass="btn-link " OnClick="ibtnPreList_Click">Previous. SDI</asp:LinkButton>


                                <asp:DropDownList ID="ddlPreList" runat="server" CssClass="form-control chzn-select inputTxt" AutoPostBack="true">
                                </asp:DropDownList>
                                <asp:Label ID="Label4" runat="server" CssClass="form-control dataLblview" Height="22" Style="line-height: 1.5" Visible="false"></asp:Label>
                            </div>
                        </div>

                    </div>

                </div>
            </div>

            <div class="sample" style="min-height: 300px">
                <asp:Panel ID="DetailsPanel" runat="server" Visible="false">
                    <div class="row">
                        <div class="col-md-6">
                            <div class="card card-fluid">
                                <div class="card-body" style="min-height: 300px;">
                                    <div class="row">
                                        <div class="col-md-6">
                                            <div class="form-group">
                                                <asp:Label ID="LblLastForma" runat="server" Text="Last (Forma) Name" CssClass="control-label"></asp:Label>

                                                <div class="input-group input-group-sm input-group-alt">
                                                    <asp:DropDownList ID="ddlForma" CssClass="form-control from-control-sm chzn-select" runat="server"></asp:DropDownList>

                                                    <div class="input-group-append">
                                                        <a data-toggle="modal" href="#" onclick="OpenGenCode(41,'Last Forma')" class="input-group-text text-success" data-target="#exampleModalSm"><span class="fa fa-plus-circle"></span></a>



                                                    </div>
                                                </div>

                                            </div>
                                            <div class="form-group">
                                                <asp:Label ID="LblCategory" runat="server" Text="Category Name" CssClass="control-label"></asp:Label>
                                                <div class="input-group input-group-sm input-group-alt">
                                                    <asp:DropDownList ID="DdlCategory" CssClass="form-control from-control-sm chzn-select" runat="server"></asp:DropDownList>
                                                    <div class="input-group-append">
                                                        <a data-toggle="modal" href="#" onclick="OpenGenCode(11,'Category')" class="input-group-text text-success" data-target="#exampleModalSm"><span class="fa fa-plus-circle"></span></a>



                                                    </div>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <asp:Label ID="Label3" runat="server" Text="Shoe Type" CssClass="control-label"></asp:Label>
                                                <div class="input-group input-group-sm input-group-alt">
                                                    <asp:DropDownList ID="DdlShoType" CssClass="form-control from-control-sm chzn-select" runat="server"></asp:DropDownList>
                                                    <div class="input-group-append">
                                                        <a data-toggle="modal" href="#" onclick="OpenGenCode(43,'Shoe Type')" class="input-group-text text-success" data-target="#exampleModalSm"><span class="fa fa-plus-circle"></span></a>



                                                    </div>
                                                </div>
                                            </div>

                                            <div class="form-group">
                                                <asp:Label ID="LblSeason" runat="server" Text="Season" CssClass="control-label"></asp:Label>
                                                <div class="input-group input-group-sm input-group-alt">
                                                    <asp:DropDownList ID="DdlSeason" CssClass="form-control form-control-sm chzn-select" runat="server"></asp:DropDownList>
                                                    <div class="input-group-append">
                                                        <a data-toggle="modal" href="#" onclick="OpenGenCode(33,'Season')" class="input-group-text text-success" data-target="#exampleModalSm"><span class="fa fa-plus-circle"></span></a>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <asp:Label ID="LBlBrand" runat="server" Text="Brand" CssClass="control-label"></asp:Label>
                                                <div class="input-group input-group-sm input-group-alt">
                                                    <asp:DropDownList ID="DdlBrand" CssClass="form-control from-control-sm chzn-select" runat="server"></asp:DropDownList>
                                                    <div class="input-group-append">
                                                        <a data-toggle="modal" href="#" onclick="OpenGenCode(37,'Brand')" class="input-group-text text-success" data-target="#exampleModalSm"><span class="fa fa-plus-circle"></span></a>



                                                    </div>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <asp:Label ID="Label5" runat="server" Text="Color" CssClass="control-label"></asp:Label>
                                                <div class="input-group input-group-sm input-group-alt">
                                                    <asp:DropDownList ID="DDlColor" CssClass="form-control from-control-sm chzn-select" runat="server"></asp:DropDownList>
                                                    <div class="input-group-append">
                                                        <a data-toggle="modal" href="#" onclick="OpenGenCode(51,'Color')" class="input-group-text text-success" data-target="#exampleModalSm"><span class="fa fa-plus-circle"></span></a>



                                                    </div>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <asp:Label ID="LblLining" runat="server" Text="Lining" CssClass="control-label"></asp:Label>
                                                <asp:TextBox ID="TxtLinnig" Enabled="false" placeholder="Lining" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>

                                            </div>
                                            <div class="form-group">
                                                <asp:Label ID="LblOutsole" runat="server" Text="Outsole" CssClass="control-label"></asp:Label>
                                                <asp:TextBox ID="TxtOutsole" Enabled="false" placeholder="Outsole" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>

                                            </div>

                                        </div>
                                        <div class="col-md-6">
                                            <div class="form-group">
                                                <asp:Label ID="LblSampleType" runat="server" Text="Sample Type: <abbr class='text-danger' title='Required'>*</abbr>" CssClass="control-label"></asp:Label>
                                                <div class="input-group input-group-sm input-group-alt">
                                                    <asp:DropDownList ID="DdlSampType" CssClass="form-control from-control-sm chzn-select" runat="server"></asp:DropDownList>
                                                    <div class="input-group-append">
                                                        <a data-toggle="modal" href="#" onclick="OpenGenCode(34,'Sample Type')" class="input-group-text text-success" data-target="#exampleModalSm"><span class="fa fa-plus-circle"></span></a>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <asp:Label ID="Label6" runat="server" Text="Confirmation Order #: <abbr class='text-danger' title='Required'>*</abbr>" CssClass="control-label"></asp:Label>
                                                <asp:TextBox ID="txtConOrdNo" placeholder="Confirmation Order #" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>

                                            </div>
                                            <div class="form-group">
                                                <asp:Label ID="LblAgent" runat="server" Text="Agent Name" CssClass="control-label"></asp:Label>
                                                <div class="input-group input-group-sm input-group-alt">
                                                    <asp:DropDownList ID="DdlAgent" CssClass="form-control from-control-sm chzn-select" runat="server"></asp:DropDownList>
                                                    <div class="input-group-append">
                                                        <a data-toggle="modal" href="#" onclick="OpenGenCode(32,'Agent')" class="input-group-text text-success" data-target="#exampleModalSm"><span class="fa fa-plus-circle"></span></a>

                                                    </div>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <asp:Label ID="LblCustomer" runat="server" Text="Customer Name" CssClass="control-label"></asp:Label>
                                                <asp:DropDownList ID="DdlCustomer" CssClass="form-control from-control-sm chzn-select" runat="server"></asp:DropDownList>

                                            </div>
                                            <div class="form-group">
                                                <asp:Label ID="LblConstruction" runat="server" Text="Construction" CssClass="control-label"></asp:Label>
                                                <div class="input-group input-group-sm input-group-alt">

                                                    <asp:DropDownList ID="DdlContruction" CssClass="form-control from-control-sm chzn-select" runat="server"></asp:DropDownList>
                                                    <div class="input-group-append">
                                                        <a data-toggle="modal" href="#" onclick="OpenGenCode(31,'Construction')" class="input-group-text text-success" data-target="#exampleModalSm"><span class="fa fa-plus-circle"></span></a>



                                                    </div>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <asp:Label ID="LblArticle" runat="server" Text="Article: <abbr class='text-danger' title='Required'>*</abbr>" CssClass="control-label"></asp:Label>
                                                <asp:TextBox ID="TxtArticle" placeholder="Article-Name" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>

                                            </div>
                                            <div class="form-group">
                                                <asp:Label ID="LblUpperMat" runat="server" Text="Upper Material" CssClass="control-label"></asp:Label>
                                                <asp:TextBox ID="TxtUppMat" Enabled="false" placeholder="Upper Material" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>

                                            </div>
                                            <div class="form-group">
                                                <asp:Label ID="LblSock" runat="server" Text="Sock" CssClass="control-label"></asp:Label>
                                                <asp:TextBox ID="TxtSock" Enabled="false" placeholder="Sock" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>

                                            </div>
                                            <div class="form-group" style="display: none;">
                                                <asp:Label ID="LblAccess" runat="server" Text="Accessories" CssClass="control-label"></asp:Label>
                                                <asp:TextBox ID="TxtAcces" Enabled="false" placeholder="Accessories" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>

                                            </div>

                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="card card-fluid">
                                <div class="card-body" style="min-height: 455px;">
                                    <div class="row">
                                        <div class="col-md-6">
                                            <div class="form-group">
                                                <asp:Label ID="Label2" runat="server" Text="Batch" CssClass="control-label"></asp:Label>
                                                <div class="input-group input-group-sm input-group-alt">
                                                    <asp:DropDownList ID="DdlBatch" CssClass="form-control from-control-sm chzn-select" runat="server"></asp:DropDownList>
                                                    <div class="input-group-append">
                                                        <a href='<%= ResolveUrl("~/F_34_Mgt/AccProjectCode") %>' target="_blank" class="input-group-text text-success"><span class="fa fa-plus-circle"></span></a>



                                                    </div>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <asp:Label ID="LblUnit" runat="server" Text="Unit" CssClass="control-label"></asp:Label>
                                                <div class="input-group input-group-sm input-group-alt">
                                                    <asp:DropDownList ID="DdlUnit" CssClass="form-control from-control-sm chzn-select" runat="server"></asp:DropDownList>
                                                    <div class="input-group-append">
                                                        <a data-toggle="modal" href="#" onclick="OpenGenCode(21,'Unit')" class="input-group-text text-success" data-target="#exampleModalSm"><span class="fa fa-plus-circle"></span></a>



                                                    </div>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <asp:Label ID="LblDelDate" runat="server" Text="Delivery Date" CssClass="control-label"></asp:Label>
                                                <asp:TextBox ID="TxtDelDate" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
                                                <cc1:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd-MMM-yyyy" TargetControlID="TxtDelDate"></cc1:CalendarExtender>

                                            </div>
                                            <div class="form-group">
                                                <asp:Label ID="Lbl" runat="server" Text="Sample Size" CssClass="control-label"></asp:Label>
                                                <asp:TextBox ID="TxtSampleSize" placeholder="Sample Size" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>

                                            </div>
                                            <div class="form-group">
                                                <asp:Label ID="LblConSize" runat="server" Text="Consumption Size" CssClass="control-label"></asp:Label>
                                                <asp:TextBox ID="TxtConSize" placeholder="Sample Size" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>

                                            </div>
                                            <div class="form-group">
                                                <asp:Label ID="LblSizeRange" runat="server" Text="Size Range" CssClass="control-label"></asp:Label>
                                                <asp:TextBox ID="TxtSizeRange" placeholder="Size range" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>

                                            </div>


                                            <div class="form-group">
                                                <asp:Label ID="LblQty" runat="server" Text="Qty" CssClass="control-label"></asp:Label>
                                                <asp:TextBox ID="TxtQty" placeholder="Qty" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>

                                            </div>
                                            <div class="form-group">
                                                <asp:Label ID="LblRemarks" runat="server" Text="Remarks" CssClass="control-label"></asp:Label>
                                                <asp:TextBox ID="TxtRemarks" placeholder="Remarks" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>

                                            </div>


                                        </div>
                                        <div class="col-md-6">
                                            <section class="card card-figure">
                                                <!-- .card-figure -->
                                                <figure class="figure">
                                                    <!-- .figure-img -->
                                                    <div class="figure-img">
                                                        <asp:Image ID="ImgSample" runat="server" class="img-fluid" Style="max-height: 310px"></asp:Image>
                                                        <%--<img class="img-fluid" src="assets/images/dummy/img-5.jpg" alt="Card image cap">--%>
                                                        <div class="figure-action">
                                                            <asp:HyperLink ID="HypLinkImage" runat="server" CssClass="btn btn-block btn-sm btn-primary" Target="_blank">
                                         Quick View
                                                            </asp:HyperLink>

                                                        </div>
                                                    </div>
                                                    <!-- /.figure-img -->
                                                    <!-- .figure-caption -->
                                                    <figcaption class="figure-caption">
                                                        <h6 class="figure-title">
                                                            <span class="fa fa-hand-point-right"></span><a target="_blank" href="<%= ResolveUrl("~/F_04_Sampling/SamConsumptionSheet?Type=Entry&genno="+this.lblCurNo1.Text.ToString().Trim().Substring(0, 3) + Convert.ToDateTime(this.txtDatefrom.Text).ToString("dd-MMM-yyyy").Substring(7, 4) + this.lblCurNo1.Text.ToString().Trim().Substring(3, 2) + this.lblCurNo2.Text.ToString().Trim()) %>">Basic Information</a>
                                                        </h6>
                                                        <p class="text-muted mb-0">Note: After Save go to Basic information </p>
                                                    </figcaption>
                                                    <!-- /.figure-caption -->
                                                </figure>
                                                <!-- /.card-figure -->
                                            </section>
                                            <div class="form-group">

                                                <asp:LinkButton ID="LbtnDetailsInput" runat="server" CssClass="btn btn-sm btn-info" OnClick="LbtnDetailsInput_Click">Click for Photo & Details Input</asp:LinkButton>
                                                <asp:LinkButton ID="LbtnDel" runat="server" CssClass="btn btn-secondary btn-xs" OnClientClick="return FunConfirm();" OnClick="LbtnDel_Click"
                                                    ToolTip="Cancel Inquery"><i class="fa fa-trash" style="color:red;" aria-hidden="true"></i> Remove
                                                </asp:LinkButton>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <asp:GridView ID="gvSampleInq" Visible="false" runat="server" AutoGenerateColumns="False" CssClass=" table-striped table-hover table-bordered grvContentarea"
                                ShowFooter="True" OnRowDataBound="gvSampleInq_RowDataBound">
                                <RowStyle />
                                <Columns>
                                    <asp:TemplateField HeaderText="SL">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lblgvItmCode" runat="server" Font-Bold="True" OnClick="lblgvItmCodc_Click"
                                                Style="text-align: right" ToolTip="Click for Details Input"
                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:LinkButton>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkbtnDelInquery" runat="server" CssClass="btn  btn-xs" OnClientClick="return FunConfirm();" Style="text-align: center;" OnClick="lnkbtnDelInquery_Click"
                                                ToolTip="Cancel Inquery"><i class="fa fa-trash" style="color:red;" aria-hidden="true"></i>
                                            </asp:LinkButton>
                                        </ItemTemplate>



                                    </asp:TemplateField>



                                    <asp:TemplateField HeaderText="Last(Forma)<br/> Name">
                                        <ItemTemplate>
                                            <asp:DropDownList ID="ddllformaname" CssClass="form-control inputTxt chzn-select" Width="80px" runat="server"></asp:DropDownList>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Article Name">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgArticlename" runat="server" AutoCompleteType="Disabled"
                                                BackColor="Transparent" BorderStyle="solid" BorderWidth="1px" BorderColor="#6acc39" Style="text-align: left;"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "article")).Trim() %>'
                                                Width="60px"></asp:TextBox>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        <ItemStyle HorizontalAlign="Left" />

                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Catagory Name">
                                        <ItemTemplate>
                                            <asp:DropDownList ID="ddlcatagory" CssClass="form-control inputTxt chzn-select" Width="100px" runat="server"></asp:DropDownList>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderText="Shoe Type">
                                        <ItemTemplate>
                                            <asp:DropDownList ID="ddlshoetype" CssClass="form-control inputTxt chzn-select" Width="80px" runat="server"></asp:DropDownList>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderText="Season">
                                        <ItemTemplate>
                                            <asp:DropDownList ID="ddlseason" CssClass="form-control inputTxt chzn-select" Width="90px" runat="server"></asp:DropDownList>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Image">
                                        <ItemTemplate>
                                            <asp:HyperLink ID="hyprrr" runat="server" NavigateUrl='<%# (Eval("images").ToString()=="")?"~/images/no_img_preview.png":Eval("images") %>' Target="_blank">
                                                <asp:Image ID="lblImageUrl" Width="60" Height="40" runat="server" ImageUrl='<%# (Eval("images").ToString()=="")?"~/images/no_img_preview.png":Eval("images") %>' class="img-responsive"></asp:Image>
                                            </asp:HyperLink>

                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderText="Upper Material">
                                        <ItemTemplate>


                                            <asp:Label ID="lblgvUppermaterial" runat="server" AutoCompleteType="Disabled"
                                                BackColor="Transparent" BorderStyle="None" Font-Size="11px"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "upmaterial")) %>'
                                                Width="80px"></asp:Label>

                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Lining">
                                        <ItemTemplate>


                                            <asp:Label ID="lblgvlinematerial" runat="server" AutoCompleteType="Disabled"
                                                BackColor="Transparent" BorderStyle="None" Font-Size="11px"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "limaterial")) %>'
                                                Width="80px"></asp:Label>

                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Sock">
                                        <ItemTemplate>


                                            <asp:Label ID="lblgvskmaterial" runat="server" AutoCompleteType="Disabled"
                                                BackColor="Transparent" BorderStyle="None" Font-Size="11px"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "skmaterial")) %>'
                                                Width="80px"></asp:Label>

                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderText="Outsole">
                                        <ItemTemplate>


                                            <asp:Label ID="lblgvosmaterial" runat="server" AutoCompleteType="Disabled"
                                                BackColor="Transparent" BorderStyle="None" Font-Size="11px"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "osmaterial")) %>'
                                                Width="80px"></asp:Label>

                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderText="Accessories">
                                        <ItemTemplate>


                                            <asp:Label ID="lblgvaccessories" runat="server" AutoCompleteType="Disabled"
                                                BackColor="Transparent" BorderStyle="None" Font-Size="11px"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "accessories")) %>'
                                                Width="80px"></asp:Label>

                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>



                                    <asp:TemplateField HeaderText="Agent">
                                        <ItemTemplate>
                                            <asp:DropDownList ID="ddlagent" CssClass="form-control inputTxt chzn-select" Width="80px" runat="server"></asp:DropDownList>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Sample Size">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvsamsize" runat="server" BackColor="Transparent"
                                                BorderStyle="solid" BorderWidth="1px" BorderColor="#6acc39" Style="text-align: right" Font-Size="11px"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "samsize")) %>'
                                                Width="40px"></asp:TextBox>
                                        </ItemTemplate>

                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Com. Size">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvcomsize" runat="server" BackColor="Transparent"
                                                BorderStyle="solid" BorderWidth="1px" BorderColor="#6acc39" Style="text-align: right" Font-Size="11px"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "comsize")) %>'
                                                Width="40px"></asp:TextBox>
                                        </ItemTemplate>

                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Size Range">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvsizerange" runat="server" AutoCompleteType="Disabled"
                                                BackColor="Transparent" BorderStyle="solid" BorderWidth="1px" BorderColor="#6acc39"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sizerange")) %>'
                                                Width="50px"></asp:TextBox>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Qty">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvsamqty" runat="server" BackColor="Transparent"
                                                BorderStyle="solid" BorderWidth="1px" BorderColor="#6acc39" Style="text-align: right" Font-Size="11px"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "samqty")).ToString("#,##0;(#,##0); ") %>'
                                                Width="40px"></asp:TextBox>
                                        </ItemTemplate>

                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Remarks">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvremarks" runat="server" AutoCompleteType="Disabled"
                                                BackColor="Transparent" BorderStyle="solid" BorderWidth="1px" BorderColor="#6acc39"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "remarks")) %>'
                                                Width="50px"></asp:TextBox>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="" Visible="false">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lbAddMore" runat="server" CommandArgument="lbAddMore"
                                                OnClick="AddMore_Click" Width="30px" CssClass="btn btn-xs btn-success"><i class="glyphicon glyphicon-plus"></i></asp:LinkButton>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <%-- <asp:TemplateField HeaderText="">
                                <ItemTemplate>
                                    <asp:LinkButton ID="LbtnApprove" OnClick="LbtnApprove_Click" runat="server" OnClientClick="return  confirm('Do You want to Approve?')" Width="30px" CssClass="btn btn-xs btn-info"><i class="glyphicon glyphicon-ok"></i></asp:LinkButton>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>--%>
                                </Columns>


                                <FooterStyle CssClass="grvFooter" />
                                <EditRowStyle />
                                <AlternatingRowStyle />
                                <PagerStyle CssClass="gvPagination" />
                                <HeaderStyle CssClass="grvHeader" />

                            </asp:GridView>

                        </div>
                    </div>
                </asp:Panel>
            </div>


            <div id="myModal" class="modal animated slideInLeft allmaterial" role="dialog" data-keyboard="false" data-backdrop="static">
                <div class="modal-dialog UpdateMOdel modal-lg">
                    <div class="modal-content  ">
                        <div class="modal-header">


                            <h4 class="modal-title">
                                <span class="fa fa-table"></span>Material </h4>
                        </div>
                        <div class="modal-body">
                            <div class="row">
                                <div class="col-md-2">
                                    <div class="label label-success"><big>Upper Material</big></div>

                                    <fieldset class="scheduler-border">

                                        <div class="form-horizontal">
                                            <div class="form-group">
                                                <div class="col-md-10 pading5px">
                                                    <asp:DropDownList ID="ddlUpperMat" runat="server" CssClass="form-control chzn-select inputTxt">
                                                    </asp:DropDownList>

                                                </div>


                                                <div class="col-md-2 pading5px">
                                                    <asp:LinkButton ID="lnkAddUpperMat" runat="server" Text="Add" OnClick="lnkAddUpperMat_Click" CssClass="btn btn-xs btn-info " TabIndex="4"></asp:LinkButton>

                                                </div>
                                            </div>

                                        </div>
                                    </fieldset>
                                    <asp:GridView ID="gvupper" runat="server" AutoGenerateColumns="False"
                                        CssClass=" table-striped table-hover table-bordered grvContentarea" Width="148px"
                                        Font-Size="11px">

                                        <Columns>

                                            <asp:TemplateField HeaderText="Sl.">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvslup" runat="server" Style="text-transform: capitalize; text-align: left"
                                                        Text='<%# Convert.ToDouble(Container.DataItemIndex+1).ToString("0")+"." %>' Width="1px"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkbtnupup" runat="server" CssClass="btn  btn-xs" OnClientClick="return FunConfirm();" Style="text-align: center;" OnClick="lnkbtnupup_Click" ToolTip="Cancel Item"><i class="fa fa-trash" style="color:red;" aria-hidden="true"></i>
                                                    </asp:LinkButton>
                                                </ItemTemplate>

                                                <%--<ItemStyle HorizontalAlign="Center" />--%>
                                                <%--<ItemStyle   CssClass="ConStylewithddelbtn"/>--%>
                                            </asp:TemplateField>


                                            <asp:TemplateField HeaderText="Description">


                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvdescrriptionupp" runat="server" Style="text-align: left;"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "msirdesc")).Trim() %>'
                                                        Width="190px"></asp:Label>
                                                </ItemTemplate>

                                                <HeaderStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Item" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvmsircodeup" runat="server" Style="text-align: left;"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "msircode")).Trim() %>'
                                                        Width="80px"></asp:Label>

                                                    <asp:Label ID="lblspcfcodup" runat="server" Style="text-align: left;"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "spcfcod")).Trim() %>'
                                                        Width="80px"></asp:Label>



                                                </ItemTemplate>

                                                <HeaderStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>



                                        </Columns>
                                        <FooterStyle CssClass="grvFooter" />
                                        <EditRowStyle />
                                        <AlternatingRowStyle />
                                        <PagerStyle CssClass="gvPagination" />
                                        <HeaderStyle CssClass="grvHeader" />

                                    </asp:GridView>
                                </div>
                                <div class="col-md-2">
                                    <div class="label label-success"><big>Lining Material</big></div>

                                    <fieldset class="scheduler-border">

                                        <div class="form-horizontal">
                                            <div class="form-group">
                                                <div class="col-md-10 pading5px">
                                                    <asp:DropDownList ID="ddllineMat" runat="server" CssClass="form-control chzn-select inputTxt">
                                                    </asp:DropDownList>

                                                </div>


                                                <div class="col-md-2 pading5px">
                                                    <asp:LinkButton ID="lnkAddLineMat" runat="server" Text="Add" OnClick="lnkAddLineMat_Click" CssClass="btn btn-xs btn-info" TabIndex="4"></asp:LinkButton>

                                                </div>
                                            </div>

                                        </div>
                                    </fieldset>
                                    <asp:GridView ID="gvlinem" runat="server" AutoGenerateColumns="False"
                                        CssClass=" table-striped table-hover table-bordered grvContentarea"
                                        Font-Size="11px">

                                        <Columns>

                                            <asp:TemplateField HeaderText="Sl.">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvslline" runat="server" Style="text-transform: capitalize; text-align: left"
                                                        Text='<%# Convert.ToDouble(Container.DataItemIndex+1).ToString("0")+"." %>' Width="1px"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkbtnline" runat="server" CssClass="btn  btn-xs" OnClientClick="return FunConfirm();" Style="text-align: center;" OnClick="lnkbtnline_Click" ToolTip="Cancel Item"><i class="fa fa-trash" style="color:red;" aria-hidden="true"></i>
                                                    </asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>


                                            <asp:TemplateField HeaderText="Description">


                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvdescrriptionline" runat="server" Style="text-align: left;"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "msirdesc")).Trim() %>'
                                                        Width="190px"></asp:Label>
                                                </ItemTemplate>


                                                <HeaderStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Item" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvmsircodeline" runat="server" Style="text-align: left;"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "msircode")).Trim() %>'
                                                        Width="80px"></asp:Label>

                                                    <asp:Label ID="lblspcfcodline" runat="server" Style="text-align: left;"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "spcfcod")).Trim() %>'
                                                        Width="80px"></asp:Label>
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
                                <div class="col-md-2">
                                    <div class="label label-success"><big>Sock Material</big></div>

                                    <fieldset class="scheduler-border">

                                        <div class="form-horizontal">
                                            <div class="form-group">
                                                <div class="col-md-10 pading5px">
                                                    <asp:DropDownList ID="ddlsockMat" runat="server" CssClass="form-control chzn-select inputTxt">
                                                    </asp:DropDownList>

                                                </div>


                                                <div class="col-md-2 pading5px">
                                                    <asp:LinkButton ID="lnkAddSockMat" runat="server" Text="Add" OnClick="lnkAddSockMat_Click" CssClass="btn btn-xs btn-info " TabIndex="4"></asp:LinkButton>

                                                </div>
                                            </div>

                                        </div>
                                    </fieldset>
                                    <asp:GridView ID="gvsockm" runat="server" AutoGenerateColumns="False"
                                        CssClass=" table-striped table-hover table-bordered grvContentarea"
                                        Font-Size="11px">

                                        <Columns>

                                            <asp:TemplateField HeaderText="Sl.">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvslsock" runat="server" Style="text-transform: capitalize; text-align: left"
                                                        Text='<%# Convert.ToDouble(Container.DataItemIndex+1).ToString("0")+"." %>' Width="1px"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkbtnsock" runat="server" CssClass="btn  btn-xs" Style="text-align: center;" OnClick="lnkbtnsock_Click" ToolTip="Cancel Item"><i class="fa fa-trash" style="color:red;" aria-hidden="true"></i>
                                                    </asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>


                                            <asp:TemplateField HeaderText="Description">


                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvdescrriptionsock" runat="server" Style="text-align: left;"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "msirdesc")).Trim() %>'
                                                        Width="190px"></asp:Label>
                                                </ItemTemplate>

                                                <HeaderStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Item" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvmsircodesock" runat="server" Style="text-align: left;"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "msircode")).Trim() %>'
                                                        Width="80px"></asp:Label>

                                                    <asp:Label ID="lblspcfcodsock" runat="server" Style="text-align: left;"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "spcfcod")).Trim() %>'
                                                        Width="80px"></asp:Label>
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

                                <div class="col-md-2">
                                    <div class="label label-success"><big>Outsole Material</big></div>

                                    <fieldset class="scheduler-border">

                                        <div class="form-horizontal">
                                            <div class="form-group">
                                                <div class="col-md-10 pading5px">
                                                    <asp:DropDownList ID="ddloutsoleMat" runat="server" CssClass="form-control chzn-select inputTxt">
                                                    </asp:DropDownList>

                                                </div>


                                                <div class="col-md-2 pading5px">
                                                    <asp:LinkButton ID="lnkAddOutSoleMat" runat="server" Text="Add" OnClick="lnkAddOutSoleMat_Click" CssClass="btn btn-xs btn-info " TabIndex="4"></asp:LinkButton>

                                                </div>
                                            </div>

                                        </div>
                                    </fieldset>
                                    <asp:GridView ID="gvosm" runat="server" AutoGenerateColumns="False"
                                        CssClass=" table-striped table-hover table-bordered grvContentarea"
                                        Font-Size="11px">

                                        <Columns>

                                            <asp:TemplateField HeaderText="Sl.">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvslosole" runat="server" Style="text-transform: capitalize; text-align: left"
                                                        Text='<%# Convert.ToDouble(Container.DataItemIndex+1).ToString("0")+"." %>' Width="1px"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkbtnsole" runat="server" CssClass="btn  btn-xs" OnClientClick="return FunConfirm();" Style="text-align: center;" OnClick="lnkbtnsole_Click" ToolTip="Cancel Item"><i class="fa fa-trash" style="color:red;" aria-hidden="true"></i>
                                                    </asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>


                                            <asp:TemplateField HeaderText="Description">


                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvdescrriptionosole" runat="server" Style="text-align: left;"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "msirdesc")).Trim() %>'
                                                        Width="190px"></asp:Label>
                                                </ItemTemplate>

                                                <HeaderStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Item" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvmsircodeosole" runat="server" Style="text-align: left;"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "msircode")).Trim() %>'
                                                        Width="80px"></asp:Label>
                                                    <asp:Label ID="lblspcfcodsole" runat="server" Style="text-align: left;"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "spcfcod")).Trim() %>'
                                                        Width="80px"></asp:Label>
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

                                <div class="col-md-2">
                                    <div class="label label-success"><big>Accessories Material</big></div>

                                    <fieldset class="scheduler-border">

                                        <div class="form-horizontal">
                                            <div class="form-group">
                                                <div class="col-md-10 pading5px">
                                                    <asp:DropDownList ID="ddlaccMat" runat="server" CssClass="form-control chzn-select inputTxt">
                                                    </asp:DropDownList>

                                                </div>


                                                <div class="col-md-2 pading5px">
                                                    <asp:LinkButton ID="lnkAddAccMat" runat="server" Text="Add" OnClick="lnkAddAccMat_Click" CssClass="btn btn-xs btn-info " TabIndex="4"></asp:LinkButton>

                                                </div>
                                            </div>

                                        </div>
                                    </fieldset>
                                    <asp:GridView ID="gvaccm" runat="server" AutoGenerateColumns="False"
                                        CssClass=" table-striped table-hover table-bordered grvContentarea"
                                        Font-Size="11px">

                                        <Columns>

                                            <asp:TemplateField HeaderText="Sl.">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvslacc" runat="server" Style="text-transform: capitalize; text-align: left"
                                                        Text='<%# Convert.ToDouble(Container.DataItemIndex+1).ToString("0")+"." %>' Width="1px"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lnkbtnacc" runat="server" CssClass="btn  btn-xs" OnClientClick="return FunConfirm();" Style="text-align: center;" OnClick="lnkbtnacc_Click" ToolTip="Cancel Item"><i class="fa fa-trash" style="color:red;" aria-hidden="true"></i>
                                                    </asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Description">


                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvdescrriptionacc" runat="server" Style="text-align: left;"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "msirdesc")).Trim() %>'
                                                        Width="190px"></asp:Label>
                                                </ItemTemplate>

                                                <HeaderStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>


                                            <asp:TemplateField HeaderText="Item" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvmsircodeacc" runat="server" Style="text-align: left;"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "msircode")).Trim() %>'
                                                        Width="80px"></asp:Label>

                                                    <asp:Label ID="lblspcfcodacc" runat="server" Style="text-align: left;"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "spcfcod")).Trim() %>'
                                                        Width="80px"></asp:Label>

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


                                <div class="col-md-2">
                                    <div class="form-group">
                                        <asp:DropDownList ID="ddluploadtype" runat="server" CssClass="form-control">
                                            <asp:ListItem Value="samimg" Selected="True">Sample Image</asp:ListItem>
                                            <asp:ListItem Value="doc">Document</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <cc1:AsyncFileUpload OnClientUploadError="uploadError"
                                        OnClientUploadComplete="uploadComplete" runat="server"
                                        ID="AsyncFileUpload1" UploaderStyle="Modern"
                                        CompleteBackColor="White"
                                        UploadingBackColor="#CCFFFF" ThrobberID="imgLoader"
                                        OnUploadedComplete="FileUploadComplete" />
                                    <asp:Image ID="Uploadedimg" runat="server" CssClass="img img-thumbnail img-responsive" />

                                </div>
                            </div>


                        </div>
                        <div class="modal-footer ">
                            <asp:Label ID="lblStylecode" runat="server" Visible="false"></asp:Label>
                            <asp:LinkButton ID="lblbtnSave" runat="server" CssClass="btn btn-sm btn-success" OnClientClick="CLoseMOdal();" OnClick="lblbtnSave_Click"><span class="glyphicon glyphicon-save"></span> Update </asp:LinkButton>

                            <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>


                        </div>
                    </div>
                </div>
            </div>

            <div id="exampleModalSm" class="modal fade " tabindex="-1" role="dialog" aria-labelledby="mySmallModalLabel">
                <!-- .modal-dialog -->
                <div class="modal-dialog modal-sm" role="document">
                    <!-- .modal-content -->
                    <div class="modal-content">
                        <!-- .modal-header -->
                        <div class="modal-header">
                            <h5 class="modal-title"></h5>
                        </div>
                        <!-- /.modal-header -->
                        <!-- .modal-body -->
                        <div class="modal-body">
                            <div class="form-group">
                                <asp:TextBox ID="txtCodeType" Style="display: none" Visible="true" runat="server" ClientIDMode="Static"></asp:TextBox>

                                <label class="label">Write New</label>
                                <asp:TextBox ID="TxtNewGenCode" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
                            </div>
                        </div>
                        <!-- /.modal-body -->
                        <!-- .modal-footer -->
                        <div class="modal-footer">
                            <asp:LinkButton ID="LbtnReRunUpdate" OnClick="LbtnReRunUpdate_Click" runat="server" CssClass="btn btn-primary">Update</asp:LinkButton>
                            <button type="button" class="btn btn-light" data-dismiss="modal">Close</button>
                        </div>
                        <!-- /.modal-footer -->
                    </div>
                    <!-- /.modal-content -->
                </div>
                <!-- /.modal-dialog -->
            </div>

            <div class="modal modal-drawer fade has-shown" id="exampleModalDrawerRight" tabindex="-1" role="dialog" aria-labelledby="exampleModalDrawerRightLabel" style="display: none;" aria-hidden="true">
                <!-- .modal-dialog -->
                <div class="modal-dialog modal-drawer-right" role="document" style="max-width: 1000px !important;">
                    <!-- .modal-content -->
                    <div class="modal-content">
                        <!-- .modal-header -->
                        <div class="modal-header modal-body-scrolled">
                            <h5 id="exampleModalDrawerRightLabel" class="modal-title">Article Details History</h5>
                        </div>
                        <!-- /.modal-header -->
                        <!-- .modal-body -->
                        <div class="modal-body">
                            <asp:GridView ID="GvArticleHistory" runat="server" AutoGenerateColumns="False"
                                CssClass=" table-striped table-hover table-bordered grvContentarea" Width="148px"
                                Font-Size="11px">

                                <Columns>

                                    <asp:TemplateField HeaderText="Sl.">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvslNoHis" runat="server" Style="text-transform: capitalize; text-align: left"
                                                Text='<%# Convert.ToDouble(Container.DataItemIndex+1).ToString("0")+"." %>' Width="1px"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="SDI">
                                        <ItemTemplate>
                                            <asp:Label ID="LblgvSdino" runat="server" Style="text-align: left;"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "inqno")).Trim() %>'
                                                Width="90px"></asp:Label>
                                        </ItemTemplate>

                                        <HeaderStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Date">
                                        <ItemTemplate>
                                            <asp:Label ID="LblgvDate" runat="server" Style="text-align: left;"
                                                Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "inqdat")).ToString("dd-MMM-yyyy") %>'
                                                Width="80px"></asp:Label>
                                        </ItemTemplate>

                                        <HeaderStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Article Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvArtName" runat="server" Style="text-align: left;"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "article")).Trim() %>'
                                                Width="180px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Buyer">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvBUyerName" runat="server" Style="text-align: left;"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "buyername")).Trim() %>'
                                                Width="100px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Color">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvColorName" runat="server" Style="text-align: left;"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "colorname")).Trim() %>'
                                                Width="80px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                </Columns>
                                <FooterStyle CssClass="grvFooter" />
                                <EditRowStyle />
                                <AlternatingRowStyle />
                                <PagerStyle CssClass="gvPagination" />
                                <HeaderStyle CssClass="grvHeader" />

                            </asp:GridView>
                        </div>
                        <!-- /.modal-body -->
                        <!-- .modal-footer -->
                        <div class="modal-footer modal-body-scrolled">
                            <button type="button" class="btn btn-light" data-dismiss="modal">Close</button>
                        </div>
                        <!-- /.modal-footer -->
                    </div>
                    <!-- /.modal-content -->
                </div>
                <!-- /.modal-dialog -->
            </div>

        </ContentTemplate>
    </asp:UpdatePanel>
    <script type="text/javascript">
        function uploadComplete(sender) {
            $get("<%=((Label)this.Master.FindControl("lblmsg")).ClientID%>").style.color = "green";
            $get("<%=((Label)this.Master.FindControl("lblmsg")).ClientID%>").innerHTML = "File Uploaded Successfully";
        }

        function uploadError(sender) {
            $get("<%=((Label)this.Master.FindControl("lblmsg")).ClientID%>").style.color = "red";
            $get("<%=((Label)this.Master.FindControl("lblmsg")).ClientID%>").innerHTML = "File upload failed.";
        }


    </script>
</asp:Content>

