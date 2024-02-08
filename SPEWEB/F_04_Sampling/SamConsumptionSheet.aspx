<%@ Page Title="" Language="C#" MasterPageFile="~/SPE.Master" AutoEventWireup="true" CodeBehind="SamConsumptionSheet.aspx.cs" Inherits="SPEWEB.F_04_Sampling.SamConsumptionSheet" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style type="text/css">
        .chzn-single {
            border-radius: 3px !important;
            height: 29px !important;
        }
        .Progressbar {
            position: absolute;
            top: 5%;
            left: 45%;
            right: 35%;
            z-index: 599;
        }

    </style>
    <script type="text/javascript" language="javascript">

        function SelectAllCheckboxes(chk) {
            var tblData1 = document.getElementById("<%=gvCompGrp.ClientID %>");

            var i = 0
            $('#<%=gvCompGrp.ClientID %>').find("input:checkbox").each(function () {
                if ((this).disabled == false && tblData1.rows[i].style.display != "none") {
                    if (this != chk) {
                        this.checked = chk.checked;
                    }
                }
                i = i + 1;
            });
        }


        function OpenModalCompChange() {
            $('#ChangeCompModal').modal('show');
        }

        function openModal() {
            $('#SpecificationModal').modal('show');

        }

        function OpenModalCompGroup() {
            $('#CopyCompGroupModal').modal('show');
        }

        function CLoseMOdal() {
            $('#SpecificationModal').modal('hide');
            $('#ChangeCompModal').modal('hide');
            $('#CopyCompGroupModal').modal('hide');
        }

        $(document).ready(function () {

            $('#ContentPlaceHolder1_DirectCost input').on("keydown", function (e) {
                /* ENTER PRESSED*/
                if (e.keyCode == 40 || e.keyCode == 25) {
                    /* FOCUS ELEMENT */
                    var inputs = $(this).parents("form").eq(0).find(":input");
                    var idx = inputs.index(this);

                    if (idx == inputs.length - 1) {
                        inputs[0].select()
                    } else {
                        inputs[idx + 2].focus(); //  handles submit buttons
                        inputs[idx + 2].select();
                    }
                    return false;
                }
                else if (e.keyCode == 38) {
                    /* FOCUS ELEMENT */
                    var inputs = $(this).parents("form").eq(0).find(":input");
                    var idx = inputs.index(this);

                    if (idx == inputs.length - 1) {
                        inputs[0].select()
                    } else {
                        inputs[idx - 2].focus(); //  handles submit buttons
                        inputs[idx - 2].select();
                    }
                    return false;
                }
            });
            $('#ContentPlaceHolder1_gvCost input').on("keydown", function (e) {
                /* ENTER PRESSED*/
                if (e.keyCode == 40) {
                    /* FOCUS ELEMENT */
                    var inputs = $(this).parents("form").eq(0).find(":input");
                    var idx = inputs.index(this);

                    if (idx == inputs.length - 1) {
                        inputs[0].select()
                    } else {
                        inputs[idx + 9].focus(); //  handles submit buttons
                        inputs[idx + 9].select();
                    }
                    return false;
                }
                else if (e.keyCode == 38) {
                    /* FOCUS ELEMENT */
                    var inputs = $(this).parents("form").eq(0).find(":input");
                    var idx = inputs.index(this);

                    if (idx == inputs.length - 1) {
                        inputs[0].select()
                    } else {
                        inputs[idx - 9].focus(); //  handles submit buttons
                        inputs[idx - 9].select();
                    }
                    return false;
                }
                else if (e.keyCode == 39) {
                    /* FOCUS ELEMENT */
                    var inputs = $(this).parents("form").eq(0).find(":input");
                    var idx = inputs.index(this);

                    if (idx == inputs.length - 1) {
                        inputs[0].select()
                    } else {
                        inputs[idx + 1].focus(); //  handles submit buttons
                        inputs[idx + 1].select();
                    }
                    return false;
                }
                else if (e.keyCode == 37) {
                    /* FOCUS ELEMENT */
                    var inputs = $(this).parents("form").eq(0).find(":input");
                    var idx = inputs.index(this);

                    if (idx == inputs.length - 1) {
                        inputs[0].select()
                    } else {
                        inputs[idx - 1].focus(); //  handles submit buttons
                        inputs[idx - 1].select();
                    }
                    return false;
                }
            });
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);

        });
        //function test(e) {
        //    var inputs = $(this).parents("form").eq(0).find(":input");
        //    if (e.keyCode == 40) {
        //        var idx = inputs.index;
        //        inputs[idx + 2].focus(); //  handles submit buttons
        //        inputs[idx + 2].select();
        //    }
        //    else {
        //        alert(e.keyCode);
        //    }

        //}
        function pageLoaded() {
            //$(function () {
            //    $('[id*=ddlComponent]').multiselect({
            //        includeSelectAllOption: true
            //    })
            //});
            <%--var gridview = $('#<%=this.gvdircost.ClientID %>');
            $.keynavigation(gridview);--%>


            var gv = $('#<%=this.gvdircost.ClientID %>');
            gv.Scrollable();

          <%--  var gv1 = $('#<%=this.gvCost.ClientID %>');
            gv1.Scrollable();--%>
            $('.chzn-select').chosen({ search_contains: true });
        }

    </script>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="Progressbar">
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

            <div class="card card-fluid">
                <div class="card-body fixTop">

                    <div class="row">
                        <div class="col-md-1">
                            <div class="form-group">
                                <asp:Label ID="Label1" runat="server" CssClass="label">Date</asp:Label>
                                <asp:TextBox ID="txtDatefrom" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
                                <cc1:CalendarExtender ID="datefrom" runat="server" Format="dd-MMM-yyyy" TargetControlID="txtDatefrom"></cc1:CalendarExtender>
                            </div>
                        </div>

                        <div class="col-md-2" style="display: none;">
                            <div class="form-group">
                                <asp:Label ID="Label3" Visible="false" runat="server" CssClass="smLbl_to">Inquiry No</asp:Label>
                                <asp:DropDownList ID="ddlinqno" OnSelectedIndexChanged="ddlinqno_SelectedIndexChanged" AutoPostBack="true" CssClass="form-control inputTxt chzn-select" runat="server"></asp:DropDownList>
                            </div>
                        </div>

                        <div class="col-md-1">
                            <div class="form-group" style="margin-top: 20px">
                                <asp:LinkButton ID="lbtnOk" runat="server" Text="Ok" OnClick="lbtnOk_Click" CssClass="btn btn-primary btn-sm" TabIndex="4"></asp:LinkButton>
                                <asp:CheckBox ID="ChckVIew" CssClass="checkbox" Text="View All Data" runat="server" Visible="false" AutoPostBack="true" OnCheckedChanged="ChckVIew_CheckedChanged" />
                                <asp:CheckBox ID="ChckCopy" Visible="false" CssClass="checkbox" Text="Copy From" runat="server" AutoPostBack="true" OnCheckedChanged="ChckCopy_CheckedChanged" />
                            </div>
                        </div>

                        <div runat="server" id="pnlArticle" class="col-md-2" visible="false">
                            <div class="form-group">
                                <asp:Label runat="server" ID="lblArtclNo" CssClass="">Article No.</asp:Label>
                                <asp:TextBox runat="server" ID="txtArtclNo" Enabled="false" CssClass="form-control form-control-sm bg-dropbox"></asp:TextBox>
                            </div>
                        </div>

                        <div runat="server" id="pnlBuyer" class="col-md-1" visible="false">
                            <div class="form-group">
                                <asp:Label runat="server" ID="lblByrNm" CssClass="">Buyer</asp:Label>
                                <asp:TextBox runat="server" ID="txtByrNm" Enabled="false" CssClass="form-control form-control-sm bg-dropbox"></asp:TextBox>
                            </div>
                        </div>

                        <div runat="server" id="pnlColor" class="col-md-1" visible="false">
                            <div class="form-group">
                                <asp:Label runat="server" ID="lblClr" CssClass="">Color</asp:Label>
                                <asp:TextBox runat="server" ID="txtClr" Enabled="false" CssClass="form-control form-control-sm bg-dropbox"></asp:TextBox>
                            </div>
                        </div>

                        <div class="col-md-3">
                            <asp:LinkButton ID="ibtnPreList" Visible="false" runat="server" CssClass="btn-link " OnClick="ibtnPreList_Click">Previous. SDI</asp:LinkButton>

                            <div class="form-inline">
                                <div class="row">
                                    <div class="col-8">
                                        <asp:DropDownList ID="ddlPreList" Visible="false" runat="server" Width="70%" CssClass="form-control form-control-sm chzn-select inputTxt">
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-4">
                                        <asp:LinkButton ID="LbtnCopy0" Visible="false" runat="server" Text="Copy" OnClick="LbtnCopy0_Click" CssClass="btn btn-xs btn-success" TabIndex="4"></asp:LinkButton>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="col-md-2" style="margin-top: -30px">
                            <div class="figure-img">
                                <asp:Image ID="Uploadedimg2" Height="120px" runat="server" CssClass="img-fluid" />

                                <%--<img class="img-fluid" src="assets/images/dummy/img-5.jpg" alt="Card image cap">--%>
                                <div class="figure-action">
                                    <a data-toggle="modal" class="btn btn-sm btn-success" href="#myModal">Click for Replace Image</a>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <div class="row" style="min-height: 300px;">

                <div class="col-lg-12">
                    <!-- #accordion -->
                    <div id="accordion" class="card-expansion">
                        <!-- .card -->
                        <section class="card card-expansion-item">
                            <header class="card-header border-0" id="headingTwo">
                                <a class="btn btn-reset collapsed" data-toggle="collapse" data-target="#collapseTwo" aria-expanded="false" aria-controls="collapseTwo">
                                    <span class="collapse-indicator mr-2">
                                        <i class="fa fa-fw fa-caret-right"></i>
                                    </span>
                                    <span>Basic Information</span>
                                </a>
                            </header>
                            <div id="collapseTwo" class="collapse" aria-labelledby="headingTwo" data-parent="#accordion">
                                <div class="card-body pt-0">
                                    <div class="row">
                                        <div class="col-md-10">
                                            <div class="row">
                                                <div class="col-md-2">
                                                    <div class="form-group">
                                                        <asp:Label ID="Label2" runat="server" CssClass="label">Sample Type</asp:Label>
                                                        <asp:TextBox ID="txtSampType" runat="server" CssClass="form-control form-control-sm" Enabled="false"></asp:TextBox>

                                                    </div>
                                                </div>

                                                <div class="col-md-2">
                                                    <div class="form-group">
                                                        <asp:Label ID="Label5" runat="server" CssClass="label">Brand Name</asp:Label>
                                                        <asp:TextBox ID="Txtbrand" runat="server" CssClass="form-control form-control-sm" Enabled="false"></asp:TextBox>
                                                    </div>
                                                </div>

                                                <div class="col-md-2">
                                                    <div class="form-group">
                                                        <asp:Label ID="Label4" runat="server" CssClass="label">Buyer Name</asp:Label>
                                                        <asp:TextBox ID="TxtBuyer" runat="server" CssClass="form-control form-control-sm" Enabled="false"></asp:TextBox>
                                                    </div>
                                                </div>

                                                <div class="col-md-2">
                                                    <div class="form-group">
                                                        <asp:Label ID="Label6" runat="server" CssClass="label">Construction</asp:Label>
                                                        <asp:TextBox ID="txtConstruction" runat="server" CssClass="form-control form-control-sm" Enabled="false"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-md-2">
                                                    <div class="form-group">
                                                        <asp:Label ID="lbllformaname" runat="server" CssClass="label">Last(forma) NAME</asp:Label>

                                                        <asp:TextBox ID="txtlformaname" runat="server" CssClass="form-control form-control-sm" Enabled="false"></asp:TextBox>

                                                    </div>
                                                </div>
                                                <div class="col-md-2">
                                                    <div class="form-group">
                                                        <asp:Label ID="lblarticle" runat="server" CssClass="label">ARTICLE NO.</asp:Label>

                                                        <asp:TextBox ID="txtArtno" runat="server" CssClass="form-control form-control-sm" Enabled="false"></asp:TextBox>

                                                    </div>
                                                </div>

                                                <div class="col-md-2">
                                                    <div class="form-group">

                                                        <asp:Label ID="lblcategory" runat="server" CssClass="label">Catagory</asp:Label>

                                                        <asp:TextBox ID="txtCategory" runat="server" CssClass="form-control form-control-sm" Enabled="false"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-md-2">
                                                    <div class="form-group">
                                                        <asp:Label ID="lblshoetype" runat="server" CssClass="label">Shoe Type</asp:Label>

                                                        <asp:TextBox ID="txtshoetype" runat="server" Enabled="false" CssClass="form-control form-control-sm"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-md-2">
                                                    <div class="form-group">
                                                        <asp:Label ID="lblSeason" runat="server" CssClass="label">Season</asp:Label>

                                                        <asp:TextBox ID="txtSeason" runat="server" CssClass="form-control form-control-sm" Enabled="false"></asp:TextBox>
                                                    </div>

                                                </div>

                                                <div class="col-md-2">
                                                    <div class="form-group">
                                                        <asp:Label ID="lblAgent" runat="server" CssClass="label">Agent</asp:Label>

                                                        <asp:TextBox ID="txtagent" runat="server" CssClass="form-control form-control-sm" Enabled="false"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-md-2">
                                                    <div class="form-group">
                                                        <asp:Label ID="lblsamsize" runat="server" CssClass="label">Sample Size</asp:Label>

                                                        <asp:TextBox ID="txtsamplesize" runat="server" CssClass="form-control  form-control-sm" Enabled="false"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-md-2">
                                                    <div class="form-group">
                                                        <asp:Label ID="lblconsize" runat="server" CssClass="label">Con. Size</asp:Label>

                                                        <asp:TextBox ID="txtsconsize" runat="server" CssClass="form-control form-control-sm" Enabled="false"></asp:TextBox>

                                                    </div>
                                                </div>


                                                <div class="col-md-2">
                                                    <div class="form-group">
                                                        <asp:Label ID="Label7" runat="server" CssClass="label">SIZE RANGE</asp:Label>

                                                        <asp:TextBox ID="txtsizernge" runat="server" CssClass="form-control form-control-sm" Enabled="false"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-md-2">
                                                    <div class="form-group">
                                                        <asp:Label ID="lblsamqty" runat="server" CssClass="label">Sample Qty</asp:Label>

                                                        <asp:TextBox ID="txtsamqty" runat="server" CssClass="form-control form-control-sm" Enabled="false"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-md-2">
                                                    <div class="form-group">
                                                        <asp:Label ID="lblDeldate" runat="server" CssClass="label">Delivery date</asp:Label>

                                                        <asp:TextBox ID="TxtDelDate" runat="server" CssClass="form-control form-control-sm" Enabled="false"></asp:TextBox>

                                                    </div>

                                                </div>
                                                <div class="col-md-2">
                                                    <div class="form-group">

                                                        <asp:Label ID="Label9" runat="server" CssClass="label">Unit</asp:Label>

                                                        <asp:TextBox ID="TxtUnit" runat="server" CssClass="form-control form-control-sm" Enabled="false"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-md-2">
                                                    <div class="form-group">
                                                        <asp:Label ID="Label8" runat="server" CssClass="label">Color</asp:Label>

                                                        <asp:TextBox ID="TxtColor" runat="server" CssClass="form-control form-control-sm" Enabled="false"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="col-md-2">
                                                    <div class="form-group">
                                                        <asp:Label ID="lblremarks" runat="server" CssClass="label">Remarks</asp:Label>

                                                        <asp:TextBox ID="txtRemarks" runat="server" CssClass="form-control form-control-sm" Enabled="false"></asp:TextBox>

                                                    </div>
                                                </div>

                                            </div>
                                        </div>
                                        <div class="col-md-2">
                                            <div class="row">
                                                <section class="card card-figure">
                                                    <!-- .card-figure -->
                                                    <figure class="figure">
                                                        <!-- .figure-img -->
                                                        <div class="figure-img">
                                                            <asp:Image ID="Uploadedimg" Height="120px" runat="server" CssClass="img-fluid" />

                                                            <%--<img class="img-fluid" src="assets/images/dummy/img-5.jpg" alt="Card image cap">--%>
                                                            <div class="figure-action">

                                                                <a data-toggle="modal" class="btn btn-sm btn-success" href="#myModal">Click for Replace Image</a>
                                                            </div>
                                                        </div>
                                                        <!-- /.figure-img -->
                                                        <!-- .figure-caption -->
                                                        <figcaption class="figure-caption">
                                                            <h6 class="figure-title">
                                                                <span class="fa fa-image"></span><a target="_blank" href="#">Sample Image</a>
                                                            </h6>
                                                            <p class="text-muted mb-0">Note: You can change this image </p>
                                                        </figcaption>
                                                        <!-- /.figure-caption -->
                                                    </figure>
                                                    <!-- /.card-figure -->
                                                </section>
                                            </div>
                                        </div>
                                    </div>


                                    <div style="display: none;" id="ProcessPanel" runat="server">
                                        <div class="col-md-8">
                                            <div class="form-group">

                                                <asp:LinkButton ID="LbtnImport" OnClick="LbtnImport_Click" OnClientClick="return confirm('Do You want to import?')" runat="server" Text="Import" Visible="false" CssClass="ol-md-2 btn btn-xs btn-success"></asp:LinkButton>
                                                <asp:Label ID="Label24" runat="server" CssClass="col-md-2 smLbl_to hidden">Ref Merchand</asp:Label>
                                                <div class="col-md-2 pading5px ">
                                                    <asp:TextBox ID="RefMarName" BorderStyle="solid" BorderWidth="1px" BorderColor="#6acc39" runat="server" CssClass="hidden form-control"></asp:TextBox>
                                                </div>

                                                <div class="clearfix"></div>
                                            </div>

                                        </div>
                                        <div class="col-md-4">
                                            <div class="form-group">
                                                <div class="col-md-1 pading5px ">
                                                    <asp:Label ID="lblcolor" runat="server" CssClass="smLbl_to" Text="&nbsp;"></asp:Label>
                                                </div>
                                                <div class="col-md-2 pading5px hidden">
                                                    <asp:DropDownList ID="ddlcolor" runat="server" CssClass="form-control inputTxt chzn-select">
                                                    </asp:DropDownList>
                                                </div>
                                                <div class="col-md-3 hidden">
                                                    <asp:Label ID="lblsize" runat="server" CssClass="smLbl_to" Text="Con. Size"></asp:Label>
                                                    <asp:DropDownList ID="ddlconsize" runat="server" Width="120px" CssClass=" inputTxt chzn-select">
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                            <div class="form-group hidden">
                                                <asp:Label ID="lblSpcf" runat="server" CssClass="col-md-3 smLbl_to" Text="Description"></asp:Label>

                                                <div class="col-md-5 pading5px">
                                                    <asp:DropDownList ID="ddlSpcfcode" runat="server" CssClass="form-control inputTxt chzn-select">
                                                    </asp:DropDownList>
                                                </div>

                                            </div>

                                        </div>

                                    </div>

                                </div>



                            </div>


                        </section>
                        <!-- /.card -->
                        <!-- .card -->
                        <section class="card card-expansion-item expanded">
                            <header class="card-header border-0 " id="headingOne">
                                <a class="btn btn-reset" data-toggle="collapse" data-target="#collapseOne" aria-expanded="true" aria-controls="collapseOne">
                                    <span class="collapse-indicator mr-2">
                                        <i class="fa fa-fw fa-caret-right"></i>
                                    </span>
                                    <span>Input data  
                                    </span>
                                </a>
                                <asp:CheckBox ID="NeedImportCheck" AutoPostBack="true" OnCheckedChanged="NeedImportCheck_CheckedChanged" CssClass="chkBoxControl" Text="Need Import?" runat="server" />
                                <asp:CheckBox ID="ChkBrandAnalysis" AutoPostBack="true" Visible="false" OnCheckedChanged="ChkBrandAnalysis_CheckedChanged" CssClass="chkBoxControl" Text="Import Brand Analysis?" runat="server" />
                                <asp:DropDownList ID="ddlBrand" runat="server" Visible="false"></asp:DropDownList>
                                <label class="text-pinterest">Note: If Brand Not selected system Import Buyer wise analysis</label>
                            </header>
                            <div id="collapseOne" class="collapse show" aria-labelledby="headingOne" data-parent="#accordion">
                                <div class="card-body pt-0">
                                    <div class="row" id="Resourcepanel" runat="server">

                                        <div class="col-md-2">
                                            <div class="form-group">
                                                <%--<asp:Label ID="Label10" runat="server" CssClass="col-md-1 smLbl_to" Text="COMPONENT NAME"></asp:Label>--%>
                                                <asp:LinkButton ID="LbtnComponent" CssClass="label" runat="server" OnClick="LbtnComponent_Click" Text="Component Group"></asp:LinkButton>
                                                <asp:DropDownList ID="DDLCompGroup" runat="server" CssClass="form-control form-control-sm chzn-select " AutoPostBack="true" OnSelectedIndexChanged="DDLCompGroup_SelectedIndexChanged"></asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="col-md-2">
                                            <div class="form-group">
                                                <asp:Label ID="lblmaterial" runat="server" CssClass="label">Component Name</asp:Label>

                                                <asp:DropDownList ID="ddlComponent" runat="server" CssClass="form-control chzn-select "></asp:DropDownList>

                                                <asp:DropDownList ID="ddlGrp" Visible="false" CssClass="form-control inputTxt chzn-select" runat="server">
                                                </asp:DropDownList>

                                            </div>

                                        </div>

                                        <div class="col-md-2">
                                            <div class="form-group">
                                                <asp:LinkButton ID="lblProcess0" runat="server" CssClass="label" OnClick="imgbtnResourceCost_Click" Text="MATERIALS NAME"></asp:LinkButton>
                                                <div class="input-group input-group-sm input-group-alt">
                                                    <asp:DropDownList ID="ddlResourcesCost" AutoPostBack="true" OnSelectedIndexChanged="ddlResourcesCost_SelectedIndexChanged" runat="server" CssClass="form-control chzn-select">
                                                    </asp:DropDownList>

                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-md-2">
                                            <div class="form-group">
                                                <asp:Label ID="Label10" runat="server" CssClass="label">Specifications</asp:Label>


                                                <div class="input-group input-group-sm input-group-alt">
                                                    <asp:DropDownList ID="DdlSpecres" runat="server" CssClass="form-control form-control-sm chzn-select">
                                                    </asp:DropDownList>
                                                    <div class="input-group-append">
                                                        <asp:LinkButton ID="LbtnMatGrpImport" OnClick="LbtnMatGrpImport_Click" runat="server" Text="Import This Material Pre-analysis" CssClass="input-group-text text-success " TabIndex="1"><span class="fa fa-download"></span></asp:LinkButton>


                                                    </div>
                                                </div>
                                            </div>

                                        </div>
                                        <div class="col-md-1">
                                            <div class="form-inline" style="margin-top: 20px;">
                                                <asp:LinkButton ID="lnkAddResouctCost" runat="server" Text="Add Table" OnClick="lnkAddResouctCost_Click" CssClass="btn btn-primary btn-sm " TabIndex="1">Add</asp:LinkButton>
                                                &nbsp; 
                                                <asp:HyperLink NavigateUrl='~/F_21_GAcc/AccResourceCode?Type=Matcode' Target="_blank" ID="lnkComponent" runat="server" Text="Add Table" CssClass="btn btn-success btn-sm " TabIndex="1">&nbsp; <span class="fa fa-plus"></span> &nbsp;</asp:HyperLink>


                                            </div>
                                        </div>
                                        <div class="col-md-2">
                                            <div class="form-group">
                                                <asp:Label ID="LblImprDept" Visible="false" runat="server">Select Department</asp:Label>
                                                <div class="input-group input-group-sm input-group-alt">
                                                    <asp:DropDownList ID="ddlmatdept" Visible="false" runat="server" CssClass="form-control chzn-select"></asp:DropDownList>
                                                    <div class="input-group-append">
                                                        <asp:LinkButton ID="btnMerge" Visible="false" runat="server" Text="Add Table" OnClick="btnMerge_Click" CssClass="input-group-text text-success " TabIndex="1"><span class="fa fa-link"></span></asp:LinkButton>


                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row" id="panelCosting" runat="server">
                                        <div class="col-md-1">
                                            <div class="form-group">
                                                <asp:Label ID="Label22" runat="server" CssClass="label">Currency </asp:Label>

                                                <asp:DropDownList ID="ddlCurList" CssClass="form-control form-control-sm" AutoPostBack="true" OnSelectedIndexChanged="ddlCurrency_SelectedIndexChanged" runat="server"></asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="col-md-1">
                                            <div class="form-group">
                                                <asp:Label ID="lblExchangerate" runat="server" CssClass="label">Exch. Rate </asp:Label>
                                                <asp:TextBox ID="txtExchngerate" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-1">
                                            <div class="form-group">
                                                <asp:Label ID="Label21" runat="server" CssClass="label">Confr. Price</asp:Label>
                                                <asp:TextBox ID="txtconfrmprice" BorderStyle="solid" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-1">
                                            <div class="form-group">
                                                <asp:Label ID="Label23" runat="server" CssClass="label">Est. Price</asp:Label>

                                                <asp:TextBox ID="txtEstimated" Enabled="false" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-1 text-teal">
                                            <div class="form-group">
                                                <asp:Label ID="LblCurone" runat="server" CssClass="label">Currency 1</asp:Label>
                                                <asp:DropDownList ID="DdlCurList1" OnSelectedIndexChanged="DdlCurList1_SelectedIndexChanged" CssClass="form-control form-control-sm" AutoPostBack="true" runat="server"></asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="col-md-1">
                                            <div class="form-group">
                                                <asp:Label ID="Label12" runat="server" CssClass="label">Exch. Rate </asp:Label>
                                                <asp:TextBox ID="txtExrate1" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-1 text-teal" runat="server" visible="false">
                                            <div class="form-group">
                                                <asp:Label ID="Label14" runat="server" CssClass="label">Currency 2</asp:Label>
                                                <asp:DropDownList ID="DdlCurList2" OnSelectedIndexChanged="DdlCurList2_SelectedIndexChanged" CssClass="form-control form-control-sm" AutoPostBack="true" runat="server"></asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="col-md-1" runat="server" visible="false">
                                            <div class="form-group">
                                                <asp:Label ID="Label13" runat="server" CssClass="label">Exch. Rate </asp:Label>
                                                <asp:TextBox ID="txtExrate2" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-1">
                                            <div class="form-group">
                                                <asp:Label ID="Label15" runat="server" CssClass="label">T. Mat. Cost </asp:Label>
                                                <asp:TextBox ID="TxttoalMatCost" Enabled="false" runat="server" CssClass="form-control form-control-sm bg-green"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-3 col-lg-3 col-sm-3 ">
                                            <div class="form-group" style="margin-top: 20px">
                                                <asp:RadioButtonList ID="RadioButtonList1" Visible="false" runat="server" AutoPostBack="true" OnSelectedIndexChanged="RadioButtonList1_SelectedIndexChanged" RepeatDirection="Horizontal">
                                                    <asp:ListItem style="color: red" Selected="True" Value="0">Materials</asp:ListItem>
                                                    <asp:ListItem style="color: blueviolet" Value="1">Common Cost</asp:ListItem>

                                                </asp:RadioButtonList>
                                            </div>
                                        </div>
                                        <%--  <div class="col-md-3">
                                        <div class="form-group">
                                            <asp:Label ID="Label18" runat="server" CssClass="label">Remarks </asp:Label>
                                            <asp:TextBox ID="txtNotes" Rows="2" TextMode="MultiLine" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
                                        </div>
                                    </div>--%>
                                    </div>

                                    <div class="row">
                                        <asp:MultiView ID="MultiView1" runat="server">
                                            <asp:View ID="Consumption" runat="server">
                                                <div class="row">
                                                    <div class="col-md-12">

                                                        <asp:GridView ID="gvCost" runat="server" AutoGenerateColumns="False" ShowFooter="True" CssClass="table-striped table-hover table-bordered grvContentarea"
                                                            Width="479px" OnRowDeleting="gvCost_RowDeleting" OnRowDataBound="gvCost_RowDataBound">
                                                            <Columns>

                                                                <asp:TemplateField HeaderStyle-Font-Bold="true" HeaderStyle-Font-Size="12px" HeaderStyle-ForeColor="#333333"
                                                                    HeaderText="SL" ItemStyle-Font-Size="12px">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblgvCostsl0" runat="server" Text='<%# Convert.ToInt32(Container.DataItemIndex+1).ToString("00")%>'
                                                                            Width="30px" Style="text-align: left"></asp:Label>
                                                                    </ItemTemplate>
                                                                    <HeaderStyle Font-Bold="True" Font-Size="12px" ForeColor="#333333" />
                                                                    <ItemStyle Font-Size="12px" />
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="">
                                                                    <ItemTemplate>
                                                                        <asp:LinkButton ID="lnkbtnDelconsum" runat="server" CssClass="btn  btn-xs" OnClientClick="return FunConfirm();" Style="text-align: center;" OnClick="lnkbtnDelconsum_Click"
                                                                            ToolTip="Cancel Inquery"><i class="fa fa-trash" style="color:red;" aria-hidden="true"></i>
                                                                        </asp:LinkButton>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                                <asp:CommandField ShowDeleteButton="True" ControlStyle-CssClass="btn btn-xs btn-danger " ItemStyle-CssClass="DeleteBtn" DeleteText="<span class='glyphicon glyphicon-remove'></span>" />

                                                                <asp:TemplateField ItemStyle-HorizontalAlign="Left">
                                                                    <HeaderTemplate>
                                                                        <table>
                                                                            <tr>
                                                                                <th class="">Group                                                            
                                                                                </th>
                                                                                <th class="pull-right">
                                                                                    <asp:HyperLink ID="hlbtnRdataExel" runat="server" BackColor="#000066" ToolTip="Export Excel"
                                                                                        BorderColor="White" BorderStyle="Solid" BorderWidth="1px"
                                                                                        ForeColor="White" Style="text-align: center; margin-left: 10px;" Width="20px"><span class="fa fa-file-excel"></span></asp:HyperLink>
                                                                                </th>
                                                                            </tr>
                                                                        </table>
                                                                    </HeaderTemplate>
                                                                    <ItemTemplate>
                                                                        <div class="d-flex">
                                                                        <asp:Label ID="lblgvgrp" runat="server" BorderStyle="None" Style="text-align: left; font-size: 12px;" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "cmpgrpdsc")) %>'
                                                                            Width="90px"></asp:Label>
                                                                        <asp:LinkButton ID="lnkbtnDltCrs" Visible="false" runat="server" CssClass="btn  btn-xs" OnClientClick="return confirm('Do You Want To Delete This Item');" Style="text-align: center;" OnClick="lnkbtnDltCrs_Click"
                                                                            ToolTip="Cancel Inquery"><i class="fa fa-times" style="color:red;" aria-hidden="true"></i>
                                                                        </asp:LinkButton>
                                                                        </div>
                                                                        <asp:Label ID="lblgvCompcode" runat="server" Visible="false" BorderStyle="None" Style="text-align: left" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "compcode")) %>'
                                                                            Width="90px"></asp:Label>
                                                                    </ItemTemplate>
                                                                    <HeaderStyle Font-Bold="True" Font-Size="10px" ForeColor="#333333" />
                                                                    <ItemStyle Font-Size="10px" />
                                                                </asp:TemplateField>

                                                              

                                                                <asp:TemplateField HeaderText="Component">
                                                                    <ItemTemplate>
                                                                        <asp:LinkButton ID="lbtngvChangeComponent" Width="50px" CssClass="text-twitter font-weight-bold" runat="server" Style="text-align: center"
                                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "compdesc")) %>'
                                                                            CommandArgument='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "compcode")) %>'
                                                                            OnClick="lbtngvChangeComponent_Click"></asp:LinkButton>
                                                                    </ItemTemplate>
                                                                    <HeaderStyle Font-Bold="True" Font-Size="10px" ForeColor="#333333" />
                                                                    <ItemStyle Font-Size="10px" />
                                                                    <FooterStyle HorizontalAlign="left" />
                                                                </asp:TemplateField>

                                                                <asp:TemplateField Visible="false" HeaderStyle-Font-Bold="true" HeaderStyle-Font-Size="10px" HeaderStyle-ForeColor="#333333"
                                                                    HeaderText="MATERIALS CODE NO" ItemStyle-Font-Size="10px" ItemStyle-HorizontalAlign="Left">

                                                                    <ItemTemplate>

                                                                        <asp:Label ID="lblgvCostDesc" runat="server" BorderStyle="None" Style="text-align: left" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "resdesc")) %>'
                                                                            Width="150px"></asp:Label>

                                                                        <asp:Label ID="lblgvcodeCost" runat="server" Visible="false" Style="text-align: left" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rescode")) %>'
                                                                            Width="60px"></asp:Label>

                                                                    </ItemTemplate>
                                                                    <HeaderStyle Font-Bold="True" Font-Size="10px" ForeColor="#333333" />
                                                                    <ItemStyle Font-Size="10px" />
                                                                    <FooterStyle HorizontalAlign="left" />
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderStyle-Font-Bold="true" HeaderStyle-Font-Size="10px" HeaderStyle-ForeColor="#333333"
                                                                    HeaderText="MATERIAL NAME" ItemStyle-Font-Size="10px">

                                                                    <ItemTemplate>


                                                                        <asp:Label ID="lblgvspcfcode" runat="server" Visible="false" BorderStyle="None" Style="text-align: left" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "spcfcode")) %>'
                                                                            Width="0px"></asp:Label>
                                                                        <asp:LinkButton ID="lblgvspcfdesc" ToolTip="Click For Change Specifications" OnClick="lblgvspcfdesc_Click" runat="server" BorderStyle="None" Style="text-align: left"
                                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "spcfdesc")) %>'
                                                                            Width="280px"></asp:LinkButton>
                                                                    </ItemTemplate>
                                                                    <FooterTemplate>
                                                                        <asp:Label ID="lbltoalf" runat="server">Direct Material Cost</asp:Label>
                                                                    </FooterTemplate>
                                                                    <HeaderStyle Font-Bold="True" Font-Size="10px" ForeColor="#333333" />
                                                                    <ItemStyle Font-Size="10px" />
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderStyle-Font-Bold="true" HeaderStyle-Font-Size="10px" HeaderStyle-ForeColor="#333333" ItemStyle-HorizontalAlign="Right"
                                                                    HeaderText="CONS/ PAIR">
                                                                    <ItemTemplate>
                                                                        <asp:TextBox ID="txtgvPreqty" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                                                            BorderWidth="1px" CssClass="GridItmTextBoxRight"
                                                                            Font-Size="10px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "preqty")).ToString("#,##0.000000;(#,##0.000000); ") %>'
                                                                            Width="70px"></asp:TextBox>
                                                                    </ItemTemplate>
                                                                    <HeaderStyle Font-Bold="True" Font-Size="10px" ForeColor="#333333" />
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderStyle-Font-Bold="true" HeaderStyle-Font-Size="10px" HeaderStyle-ForeColor="#333333" ItemStyle-HorizontalAlign="Right"
                                                                    HeaderText="Issued">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblgvissuedqty" runat="server" CssClass="GridItmTextBoxRight"
                                                                            Font-Size="10px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "issueqty")).ToString("#,##0.000000;(#,##0.000000); ") %>'
                                                                            Width="70px"></asp:Label>
                                                                    </ItemTemplate>

                                                                    <FooterTemplate>
                                                                        <asp:LinkButton ID="LbtngvCostfootIssue" Font-Bold="true" runat="server" OnClientClick="return confirm('Do you agree to Sync Act. Cons?')" OnClick="LbtngvCostfootIssue_Click"><span class="fa fa-sync-alt"></span>Act. Cons</asp:LinkButton>
                                                                    </FooterTemplate>
                                                                    <HeaderStyle Font-Bold="True" Font-Size="10px" ForeColor="#333333" />
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderStyle-Font-Bold="true" HeaderStyle-Font-Size="10px" HeaderStyle-ForeColor="#333333" ItemStyle-HorizontalAlign="Right"
                                                                    HeaderText="Act. Cons">
                                                                    <ItemTemplate>
                                                                        <asp:TextBox ID="txtgvconqty" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                                                            BorderWidth="1px" CssClass="GridItmTextBoxRight"
                                                                            Font-Size="10px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty")).ToString("#,##0.000000;(#,##0.000000); ") %>'
                                                                            Width="70px"></asp:TextBox>
                                                                    </ItemTemplate>

                                                                    <HeaderStyle Font-Bold="True" Font-Size="10px" ForeColor="#333333" />
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderStyle-Font-Bold="true" HeaderStyle-Font-Size="10px" HeaderStyle-ForeColor="#333333"
                                                                    HeaderText="UNIT" ItemStyle-Font-Size="10px">

                                                                    <ItemTemplate>
                                                                        <asp:Label ID="txtgvunit0" runat="server" BorderStyle="None" Style="text-align: left" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "resunit")) %>'
                                                                            Width="30px"></asp:Label>
                                                                    </ItemTemplate>
                                                                    <HeaderStyle Font-Bold="True" Font-Size="10px" ForeColor="#333333" />
                                                                    <ItemStyle Font-Size="10px" />
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderStyle-Font-Bold="true" HeaderStyle-Font-Size="10px" HeaderStyle-ForeColor="#333333" ItemStyle-HorizontalAlign="Right"
                                                                    HeaderText="Allowance <br>%">
                                                                    <HeaderTemplate>
                                                                        <asp:LinkButton ID="LbtnSyncWestPc" Font-Bold="true" runat="server" OnClientClick="return confirm('Do you agree to Sync Allowance? Please Save CBD before Sync and Remember CBD Allowance will change according to Current Material Allowance')" OnClick="LbtnSyncWestPc_Click"><span class="fa fa-sync-alt"></span> Allowance % </asp:LinkButton>
                                                                    </HeaderTemplate>
                                                                    <ItemTemplate>
                                                                        <asp:TextBox ID="txtgvwestpc" runat="server" BorderStyle="None"
                                                                            CssClass=""
                                                                            Font-Size="10px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "westpc")).ToString("#,##0.000;(#,##0.000); ") %>'
                                                                            Width="60px"></asp:TextBox>
                                                                    </ItemTemplate>

                                                                    <HeaderStyle Font-Bold="True" Font-Size="10px" ForeColor="#333333" />
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderStyle-Font-Bold="true" HeaderStyle-Font-Size="10px" HeaderStyle-ForeColor="#333333" ItemStyle-HorizontalAlign="Right"
                                                                    HeaderText="Allowance">

                                                                    <ItemTemplate>
                                                                        <asp:TextBox ID="txtgvwestpcallow" runat="server" BorderStyle="None"
                                                                            CssClass=""
                                                                            Font-Size="10px" Style="text-align: right;" Text='<%# Convert.ToDouble(((Convert.ToDouble(DataBinder.Eval(Container.DataItem, "westpc"))* (Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty"))))/100)).ToString("#,##0.000000;(#,##0.000000); ")%>'
                                                                            Width="60px"></asp:TextBox>
                                                                    </ItemTemplate>

                                                                    <HeaderStyle Font-Bold="True" Font-Size="10px" ForeColor="#333333" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderStyle-Font-Bold="true" HeaderStyle-Font-Size="10px" HeaderStyle-ForeColor="#333333" ItemStyle-HorizontalAlign="Right"
                                                                    HeaderText="Total Cons">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="LblTotalCon" runat="server" BorderStyle="None"
                                                                            CssClass=""
                                                                            Font-Size="10px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "conqty")).ToString("#,##0.000000;(#,##0.000000); ") %>'
                                                                            Width="70px"></asp:Label>
                                                                    </ItemTemplate>

                                                                    <HeaderStyle Font-Bold="True" Font-Size="10px" ForeColor="#333333" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField>
                                                                    <HeaderTemplate>
                                                                        <asp:LinkButton ID="LbtnSyncPrice" Font-Bold="true" runat="server" OnClientClick="return confirm('Do you agree to Sync Price? Please Save CBD before Sync and Remember CBD Price will change according to Current Material Price')" OnClick="LbtnSyncPrice_Click"><span class="fa fa-sync-alt"></span> Price/Unit</asp:LinkButton>
                                                                    </HeaderTemplate>
                                                                    <ItemTemplate>
                                                                        <%--  OnTextChanged="txtgvqrateCost_TextChanged" AutoPostBack="true" --%>
                                                                        <asp:TextBox ID="txtgvqrateCost" runat="server" BorderStyle="None" Font-Size="12px"
                                                                            Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "rate")).ToString("#,##0.000000;(#,##0.000000); ") %>'
                                                                            Width="70px"></asp:TextBox>
                                                                    </ItemTemplate>

                                                                    <HeaderStyle Font-Bold="True" Font-Size="10px" ForeColor="#333333" />

                                                                    <ItemStyle Font-Size="10px" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Allowance <br> Amount" Visible="false">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="LblAllAmt" runat="server" BorderStyle="None" Font-Size="12px" Style="text-align: right" Text='<%# ((Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty"))*Convert.ToDouble(DataBinder.Eval(Container.DataItem, "westpc"))/100)*Convert.ToDouble(DataBinder.Eval(Container.DataItem, "rate"))).ToString("#,##0.000000;(#,##0.000000); ") %>'
                                                                            Width="50px"></asp:Label>
                                                                    </ItemTemplate>

                                                                    <HeaderStyle Font-Bold="True" Font-Size="10px" ForeColor="#333333" />

                                                                    <ItemStyle Font-Size="10px" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="C&F %">

                                                                    <HeaderTemplate>
                                                                        <asp:LinkButton ID="LbtnSyncCnF" Font-Bold="true" runat="server"
                                                                            OnClientClick="return confirm('Do you agree to sync C&F? Please save CBD before sync and semember C&F will change according to current material C&F Rate')"
                                                                            OnClick="LbtnSyncCnF_Click"
                                                                            ToolTip="After Synchronization, Please Recalculate and Save">
                                                                            <span class="fa fa-sync-alt"></span> C&F %
                                                                        </asp:LinkButton>
                                                                    </HeaderTemplate>


                                                                    <ItemTemplate>
                                                                        <asp:TextBox ID="txtgvqcfrate" onkeydown="test(event)" runat="server" BorderStyle="None" Font-Size="12px" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "cfrate")).ToString("#,##0.000000;(#,##0.000000); ") %>'
                                                                            Width="60px"></asp:TextBox>
                                                                    </ItemTemplate>

                                                                    <HeaderStyle Font-Bold="True" Font-Size="10px" ForeColor="#333333" />

                                                                    <ItemStyle Font-Size="10px" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="F.C&F Rate">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblgvqfcfrate" runat="server" BorderStyle="None" Font-Size="12px" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "fcfrate")).ToString("#,##0.000000;(#,##0.000000); ") %>'
                                                                            Width="60px"></asp:Label>
                                                                    </ItemTemplate>

                                                                    <HeaderStyle Font-Bold="True" Font-Size="10px" ForeColor="#333333" />

                                                                    <ItemStyle Font-Size="10px" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="TOTAL">
                                                                    <ItemTemplate>

                                                                        <asp:TextBox ID="txtgvCostamtCost" runat="server" BorderStyle="None" Font-Size="10px"
                                                                            Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt")).ToString("#,##0.000000;(#,##0.000000); ") %>'
                                                                            Width="70px"></asp:TextBox>
                                                                    </ItemTemplate>
                                                                    <FooterTemplate>
                                                                        <asp:Label ID="lblgvfamtCost" runat="server" Font-Bold="True" Font-Size="10px" ForeColor="#CC0066"
                                                                            Style="text-align: right" Width="60px"></asp:Label>
                                                                    </FooterTemplate>
                                                                    <HeaderStyle Font-Bold="True" Font-Size="10px" ForeColor="#333333" />
                                                                    <ItemStyle HorizontalAlign="Right" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="BDT Amount" Visible="false">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblgBdtamt" runat="server" Font-Size="12px"
                                                                            ItemStyle-Font-Size="12px" Style="text-align: right"
                                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "convrate")).ToString("#,##0.0000;(#,##0.0000); ") %>'
                                                                            Width="80px"></asp:Label>

                                                                    </ItemTemplate>
                                                                    <FooterTemplate>
                                                                        <asp:Label ID="lblgvBdamtCost" runat="server" Font-Bold="True" Font-Size="10px" ForeColor="#CC0066"
                                                                            Style="text-align: right" Width="60px"></asp:Label>
                                                                    </FooterTemplate>
                                                                    <HeaderStyle Font-Bold="True" Font-Size="10px" ForeColor="#333333" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="%" Visible="false">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblgvpercnt" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "percnt")).ToString("#,##0.00;(#,##0.00); ")+(Convert.ToDouble((DataBinder.Eval(Container.DataItem, "percnt")))>0?"%":"")%>'
                                                                            Width="40px" Style="text-align: right" ItemStyle-Font-Size="10px" Font-Size="10px"></asp:Label>
                                                                    </ItemTemplate>
                                                                    <FooterTemplate>
                                                                        <asp:Label ID="lblgvfper" runat="server" Font-Bold="True" Font-Size="10px" ForeColor="#CC0066"
                                                                            Width="40px" Style="text-align: right"></asp:Label>
                                                                    </FooterTemplate>
                                                                    <HeaderStyle Font-Bold="True" Font-Size="10px" ForeColor="#333333" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Notes">
                                                                    <ItemTemplate>
                                                                        <asp:TextBox ID="txtgvnotes" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "notes").ToString()%>'
                                                                            Width="60px" Style="text-align: left" ItemStyle-Font-Size="10px" Font-Size="10px"></asp:TextBox>
                                                                    </ItemTemplate>
                                                                    <HeaderStyle Font-Bold="True" Font-Size="10px" ForeColor="#333333" />
                                                                </asp:TemplateField>
                                                                <asp:TemplateField>
                                                                    <HeaderTemplate>
                                                                        <asp:LinkButton ID="lnkpgAllMatApproval" ToolTip="All Item Approved" OnClick="lnkpgAllMatApproval_Click" runat="server" Font-Underline="false"><i class="fa fa-check" aria-hidden="true"></i>
                                                                        </asp:LinkButton>
                                                                    </HeaderTemplate>
                                                                    <ItemTemplate>
                                                                        <asp:LinkButton ID="lnkpgMatApproval" ToolTip="Item Approval" OnClick="lnkpgMatApproval_Click" runat="server" Font-Underline="false"><i class="fa fa-check" aria-hidden="true"></i>
                                                                        </asp:LinkButton>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField>
                                                                    <ItemTemplate>
                                                                        <asp:CheckBox ID="chkCol" runat="server" />
                                                                    </ItemTemplate>
                                                                    <HeaderTemplate>
                                                                        <asp:CheckBox ID="chkhead" AutoPostBack="true" OnCheckedChanged="chkheadl_CheckedChanged" runat="server" />
                                                                    </HeaderTemplate>

                                                                    <HeaderStyle Font-Bold="True" Font-Size="12px" />
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
                                            </asp:View>
                                            <asp:View ID="ViewCommonCost" runat="server">
                                                <div class="row">
                                                    <div class="col-md-6">
                                                        <asp:Panel ID="DirectCost" runat="server" Visible="false">
                                                            <asp:GridView ID="gvdircost" runat="server" AutoGenerateColumns="False" ShowFooter="True" CssClass="table-striped table-hover table-bordered grvContentarea"
                                                                Width="260px" OnRowDataBound="gvdircost_RowDataBound">
                                                                <Columns>
                                                                    <asp:TemplateField HeaderStyle-Font-Bold="true" HeaderStyle-Font-Size="12px" HeaderStyle-ForeColor="#333333"
                                                                        HeaderText="" ItemStyle-Font-Size="12px">
                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblgvsl0" runat="server" Text='<%# Convert.ToInt32(Container.DataItemIndex+1).ToString("00")%>'
                                                                                Width="15px" Style="text-align: left"></asp:Label>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle Font-Bold="True" Font-Size="12px" ForeColor="#333333" />
                                                                        <ItemStyle Font-Size="12px" />
                                                                    </asp:TemplateField>

                                                                    <asp:TemplateField HeaderText="Common Cost">

                                                                        <ItemTemplate>
                                                                            <asp:Label ID="lblgvcodeCost" runat="server" Visible="false" Style="text-align: left" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sircode")) %>'
                                                                                Width="60px"></asp:Label>

                                                                            <asp:HyperLink ID="lblgvDesc" runat="server" Target="_blank" ForeColor="Black" BorderStyle="None" Style="text-align: left" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sirdesc")) %>'
                                                                                Width="150px"></asp:HyperLink>

                                                                        </ItemTemplate>
                                                                        <FooterTemplate>
                                                                            <asp:Label ID="lbltoalf" runat="server">Total Cost</asp:Label>
                                                                        </FooterTemplate>
                                                                        <HeaderStyle Font-Bold="True" Font-Size="10px" ForeColor="#333333" />
                                                                        <ItemStyle Font-Size="10px" />
                                                                        <FooterStyle HorizontalAlign="right" />
                                                                    </asp:TemplateField>
                                                                    <asp:TemplateField HeaderText="Percent(%)">

                                                                        <ItemTemplate>

                                                                            <asp:TextBox ID="txtpercnt" runat="server" BorderStyle="Solid" Font-Size="10px" BorderWidth="1px" BorderColor="#42f459"
                                                                                Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "percnt")).ToString("#,##0.00;(#,##0.00); ") %>' Width="60px"></asp:TextBox>
                                                                        </ItemTemplate>
                                                                        <FooterTemplate>
                                                                            <asp:Label ID="lblgvfamtPrevCost" runat="server" Font-Bold="True" Font-Size="10px" ForeColor="#CC0066"
                                                                                Style="text-align: right" Width="60px"></asp:Label>
                                                                        </FooterTemplate>
                                                                        <HeaderStyle Font-Bold="True" Font-Size="10px" ForeColor="#333333" />
                                                                    </asp:TemplateField>

                                                                    <asp:TemplateField HeaderText="Amount">
                                                                        <HeaderTemplate>
                                                                            <asp:LinkButton ID="LbtnSyncAmount" Font-Bold="true" runat="server" OnClientClick="return confirm('Do you agree to Sync Amount? Please Save CBD before Sync and Remember Common cost will change according to Current Common cost')" OnClick="LbtnSyncAmount_Click"><span class="fa fa-sync-alt"></span> Amount</asp:LinkButton>

                                                                        </HeaderTemplate>
                                                                        <ItemTemplate>

                                                                            <asp:TextBox ID="txtgvamtCost" runat="server" BorderStyle="Solid" Font-Size="10px" BorderWidth="1px" BorderColor="#42f459"
                                                                                Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "stdamt")).ToString("#,##0.000000;(#,##0.000000); ") %>'
                                                                                Width="70px"></asp:TextBox>
                                                                        </ItemTemplate>
                                                                        <FooterTemplate>
                                                                            <asp:Label ID="lblgvfamtCost" runat="server" Font-Bold="True" Font-Size="10px" ForeColor="#CC0066"
                                                                                Style="text-align: right" Width="50px"></asp:Label>
                                                                        </FooterTemplate>
                                                                        <HeaderStyle Font-Bold="True" Font-Size="10px" ForeColor="#333333" />
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
                                                    <div class="col-md-6">
                                                        <table class="table-bordered table-condensed">
                                                            <tr>
                                                                <td>
                                                                    <asp:Label ID="Label16" runat="server" CssClass="col-md-12 smLbl_to">Conversion 1</asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtConversion1" Style="text-align: right" Enabled="false" BorderStyle="solid" BorderWidth="1px" BorderColor="#6acc39" runat="server" CssClass="" Width="70px"></asp:TextBox>
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="TxtCostCur1" Style="text-align: right" BorderStyle="solid" BorderWidth="1px" BorderColor="#6acc39" runat="server" CssClass="" Width="70px"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr runat="server" visible="false">
                                                                <td>
                                                                    <asp:Label ID="Label17" runat="server" CssClass="col-md-12 smLbl_to">Conversion 2</asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtConversion2" Style="text-align: right" Enabled="false" BorderStyle="solid" BorderWidth="1px" BorderColor="#6acc39" runat="server" CssClass="" Width="70px"></asp:TextBox>
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="TxtCostCur2" Style="text-align: right" BorderStyle="solid" BorderWidth="1px" BorderColor="#6acc39" runat="server" CssClass="" Width="70px"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <asp:Label ID="Label20" runat="server" CssClass="col-md-12 smLbl_to">Offer Price (%)</asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtoffpercnt" Style="text-align: right" Enabled="false" BorderStyle="solid" BorderWidth="1px" BorderColor="#6acc39" runat="server" CssClass="" Width="70px"></asp:TextBox>
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtoffprice" Style="text-align: right" BorderStyle="solid" BorderWidth="1px" BorderColor="#6acc39" runat="server" CssClass="" Width="70px"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <asp:Label ID="Label11" runat="server" Style="text-align: right" CssClass="col-md-12 smLbl_to">P/L Analysis </asp:Label>

                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="TextBox3" Enabled="false" Style="text-align: right" BorderStyle="solid" BorderWidth="1px" BorderColor="#6acc39" runat="server" CssClass="" Width="70px"></asp:TextBox>

                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="TxtPlAnalysis" Style="text-align: right" Enabled="false" BorderStyle="solid" BorderWidth="1px" BorderColor="#6acc39" runat="server" CssClass="" Width="70px"></asp:TextBox>

                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <asp:Label ID="Label19" runat="server" CssClass="col-md-12 smLbl_to">Customer Target (%)</asp:Label>

                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtTarpercnt" Enabled="false" Style="text-align: right" BorderStyle="solid" BorderWidth="1px" BorderColor="#6acc39" runat="server" CssClass="" Width="70px"></asp:TextBox>

                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txttarprice" Style="text-align: right" BorderStyle="solid" BorderWidth="1px" BorderColor="#6acc39" runat="server" CssClass="" Width="70px"></asp:TextBox>

                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </div>
                                                </div>
                                            </asp:View>

                                        </asp:MultiView>

                                        <div class=" col-md-12 ">
                                            <asp:Label ID="Label25" runat="server" CssClass="label">Remarks </asp:Label>
                                            <asp:TextBox ID="txtNotes" Rows="3" TextMode="MultiLine" runat="server" CssClass="form-control "></asp:TextBox>

                                            <div class="clearfix"></div>
                                            <br />
                                        </div>

                                    </div>
                                </div>
                            </div>
                        </section>
                        <!-- /.card -->

                        <!-- .card -->
                        <section class="card card-expansion-item">
                            <header class="card-header border-0" id="headingThree">
                                <a class="btn btn-reset collapsed" data-toggle="collapse" data-target="#collapseThree" aria-expanded="false" aria-controls="collapseThree">
                                    <span class="collapse-indicator mr-2">
                                        <i class="fa fa-fw fa-caret-right"></i>
                                    </span>
                                    <span>Sample Size & Others</span>
                                </a>
                            </header>
                            <div id="collapseThree" class="collapse" aria-labelledby="headingThree" data-parent="#accordion">
                                <div class="card-body pt-0">
                                    <div class="col-md-6">

                                        <div class="row form-inline" style="margin-left: 20px">
                                            <%--<div class="col-md-12">
                                                 <asp:Label ID="Label26" runat="server" CssClass="smLbl_to">Notes</asp:Label>
                                        <div class="pading5px">
                                            <asp:TextBox ID="TextBox1" Rows="2" Columns="140" TextMode="MultiLine" BorderStyle="solid" BorderWidth="1px" BorderColor="#6acc39" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>

                                            </div>--%>
                                            <div class="col-md-12">
                                                <asp:GridView ID="grvSmpleSizes" runat="server" AutoGenerateColumns="False" ShowFooter="True" CssClass="table-striped table-hover table-bordered grvContentarea"
                                                    Width="479px">
                                                    <Columns>
                                                        <asp:TemplateField>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblgvsl0" runat="server" Text='<%# Convert.ToInt32(Container.DataItemIndex+1).ToString("00")+"." %>'
                                                                    Width="100%" Style="text-align: left"></asp:Label>
                                                            </ItemTemplate>

                                                            <ItemStyle Font-Size="12px" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderStyle-Font-Bold="true" HeaderStyle-Font-Size="10px" HeaderStyle-ForeColor="#333333"
                                                            HeaderText="Type Desc" ItemStyle-Font-Size="10px" ItemStyle-HorizontalAlign="Left">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="lblgvtypedesc" runat="server" BorderStyle="None" CssClass="bg-twitter" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "typedesc")) %>'
                                                                    Width="100px"></asp:TextBox>
                                                            </ItemTemplate>
                                                            <HeaderStyle Font-Bold="True" Font-Size="10px" ForeColor="#333333" />
                                                            <ItemStyle Font-Size="12px" />
                                                            <FooterStyle HorizontalAlign="left" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderStyle-Font-Bold="true" HeaderStyle-Font-Size="10px" HeaderStyle-ForeColor="#333333"
                                                            HeaderText="" ItemStyle-Font-Size="10px">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="lblgvs1" runat="server" BorderStyle="None" Style="text-align: left" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "s1")) %>'
                                                                    Width="50px"></asp:TextBox>
                                                            </ItemTemplate>
                                                            <HeaderStyle Font-Bold="True" Font-Size="10px" ForeColor="#333333" />
                                                            <ItemStyle Font-Size="12px" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderStyle-Font-Bold="true" HeaderStyle-Font-Size="10px" HeaderStyle-ForeColor="#333333"
                                                            HeaderText="" ItemStyle-Font-Size="10px" ItemStyle-HorizontalAlign="Left">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="lblgvs2" runat="server" BorderStyle="None" Style="text-align: left" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "s2")) %>'
                                                                    Width="50px"></asp:TextBox>
                                                            </ItemTemplate>
                                                            <HeaderStyle Font-Bold="True" Font-Size="10px" ForeColor="#333333" />
                                                            <ItemStyle Font-Size="12px" />
                                                            <FooterStyle HorizontalAlign="left" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderStyle-Font-Bold="true" HeaderStyle-Font-Size="10px" HeaderStyle-ForeColor="#333333"
                                                            HeaderText="" ItemStyle-Font-Size="10px">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="lblgvs3" runat="server" BorderStyle="None" Style="text-align: left" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "s3")) %>'
                                                                    Width="50px"></asp:TextBox>

                                                            </ItemTemplate>

                                                            <HeaderStyle Font-Bold="True" Font-Size="10px" ForeColor="#333333" />
                                                            <ItemStyle Font-Size="12px" />
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderStyle-Font-Bold="true" HeaderStyle-Font-Size="10px" HeaderStyle-ForeColor="#333333"
                                                            HeaderText="" ItemStyle-Font-Size="10px">

                                                            <ItemTemplate>
                                                                <asp:TextBox ID="lblgvs4" runat="server" BorderStyle="None" Style="text-align: left" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "s4")) %>'
                                                                    Width="50px"></asp:TextBox>

                                                            </ItemTemplate>

                                                            <HeaderStyle Font-Bold="True" Font-Size="10px" ForeColor="#333333" />
                                                            <ItemStyle Font-Size="12px" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderStyle-Font-Bold="true" HeaderStyle-Font-Size="10px" HeaderStyle-ForeColor="#333333"
                                                            HeaderText="" ItemStyle-Font-Size="10px">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="lblgvs5" runat="server" BorderStyle="None" Style="text-align: left" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "s5")) %>'
                                                                    Width="50px"></asp:TextBox>

                                                            </ItemTemplate>

                                                            <HeaderStyle Font-Bold="True" Font-Size="10px" ForeColor="#333333" />
                                                            <ItemStyle Font-Size="12px" />
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderStyle-Font-Bold="true" HeaderStyle-Font-Size="10px" HeaderStyle-ForeColor="#333333"
                                                            HeaderText="" ItemStyle-Font-Size="10px">

                                                            <ItemTemplate>
                                                                <asp:TextBox ID="lblgvs6" runat="server" BorderStyle="None" Style="text-align: left" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "s6")) %>'
                                                                    Width="50px"></asp:TextBox>

                                                            </ItemTemplate>

                                                            <HeaderStyle Font-Bold="True" Font-Size="10px" ForeColor="#333333" />
                                                            <ItemStyle Font-Size="12px" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderStyle-Font-Bold="true" HeaderStyle-Font-Size="10px" HeaderStyle-ForeColor="#333333"
                                                            HeaderText="" ItemStyle-Font-Size="10px">

                                                            <ItemTemplate>
                                                                <asp:TextBox ID="lblgvs7" runat="server" BorderStyle="None" Style="text-align: left" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "s7")) %>'
                                                                    Width="50px"></asp:TextBox>

                                                            </ItemTemplate>

                                                            <HeaderStyle Font-Bold="True" Font-Size="10px" ForeColor="#333333" />
                                                            <ItemStyle Font-Size="12px" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderStyle-Font-Bold="true" HeaderStyle-Font-Size="10px" HeaderStyle-ForeColor="#333333"
                                                            HeaderText="" ItemStyle-Font-Size="10px">

                                                            <ItemTemplate>
                                                                <asp:TextBox ID="lblgvs8" runat="server" BorderStyle="None" Style="text-align: left" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "s8")) %>'
                                                                    Width="50px"></asp:TextBox>

                                                            </ItemTemplate>

                                                            <HeaderStyle Font-Bold="True" Font-Size="10px" ForeColor="#333333" />
                                                            <ItemStyle Font-Size="12px" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderStyle-Font-Bold="true" HeaderStyle-Font-Size="10px" HeaderStyle-ForeColor="#333333"
                                                            HeaderText="" ItemStyle-Font-Size="10px">

                                                            <ItemTemplate>
                                                                <asp:TextBox ID="lblgvs9" runat="server" BorderStyle="None" Style="text-align: left" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "s9")) %>'
                                                                    Width="50px"></asp:TextBox>

                                                            </ItemTemplate>

                                                            <HeaderStyle Font-Bold="True" Font-Size="10px" ForeColor="#333333" />
                                                            <ItemStyle Font-Size="12px" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderStyle-Font-Bold="true" HeaderStyle-Font-Size="10px" HeaderStyle-ForeColor="#333333"
                                                            HeaderText="" ItemStyle-Font-Size="10px">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="lblgvs10" runat="server" BorderStyle="None" Style="text-align: left" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "s10")) %>'
                                                                    Width="50px"></asp:TextBox>

                                                            </ItemTemplate>
                                                            <HeaderStyle Font-Bold="True" Font-Size="10px" ForeColor="#333333" />
                                                            <ItemStyle Font-Size="12px" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderStyle-Font-Bold="true" HeaderStyle-Font-Size="10px" HeaderStyle-ForeColor="#333333"
                                                            HeaderText="" ItemStyle-Font-Size="10px">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="lblgvs11" runat="server" BorderStyle="None" Style="text-align: left" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "s11")) %>'
                                                                    Width="50px"></asp:TextBox>

                                                            </ItemTemplate>
                                                            <HeaderStyle Font-Bold="True" Font-Size="10px" ForeColor="#333333" />
                                                            <ItemStyle Font-Size="12px" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderStyle-Font-Bold="true" HeaderStyle-Font-Size="10px" HeaderStyle-ForeColor="#333333"
                                                            HeaderText="" ItemStyle-Font-Size="10px">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="lblgvs12" runat="server" BorderStyle="None" Style="text-align: left" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "s12")) %>'
                                                                    Width="50px"></asp:TextBox>

                                                            </ItemTemplate>
                                                            <HeaderStyle Font-Bold="True" Font-Size="10px" ForeColor="#333333" />
                                                            <ItemStyle Font-Size="12px" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderStyle-Font-Bold="true" HeaderStyle-Font-Size="10px" HeaderStyle-ForeColor="#333333"
                                                            HeaderText="" ItemStyle-Font-Size="10px">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="lblgvs13" runat="server" BorderStyle="None" Style="text-align: left" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "s13")) %>'
                                                                    Width="50px"></asp:TextBox>

                                                            </ItemTemplate>
                                                            <HeaderStyle Font-Bold="True" Font-Size="10px" ForeColor="#333333" />
                                                            <ItemStyle Font-Size="12px" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderStyle-Font-Bold="true" HeaderStyle-Font-Size="10px" HeaderStyle-ForeColor="#333333"
                                                            HeaderText="" ItemStyle-Font-Size="10px">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="lblgvs14" runat="server" BorderStyle="None" Style="text-align: left" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "s14")) %>'
                                                                    Width="50px"></asp:TextBox>

                                                            </ItemTemplate>
                                                            <HeaderStyle Font-Bold="True" Font-Size="10px" ForeColor="#333333" />
                                                            <ItemStyle Font-Size="12px" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderStyle-Font-Bold="true" HeaderStyle-Font-Size="10px" HeaderStyle-ForeColor="#333333"
                                                            HeaderText="" ItemStyle-Font-Size="10px">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="lblgvs15" runat="server" BorderStyle="None" Style="text-align: left" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "s15")) %>'
                                                                    Width="50px"></asp:TextBox>

                                                            </ItemTemplate>
                                                            <HeaderStyle Font-Bold="True" Font-Size="10px" ForeColor="#333333" />
                                                            <ItemStyle Font-Size="12px" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderStyle-Font-Bold="true" HeaderStyle-Font-Size="10px" HeaderStyle-ForeColor="#333333"
                                                            HeaderText="" ItemStyle-Font-Size="10px">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lnkAddSmpSiz" runat="server" Text="Add Table" OnClick="lnkAddSmpSiz_Click" CssClass="text-flickr" TabIndex="1" Visible="false"><span class="fa fa-plus"></span></asp:LinkButton>
                                                            </ItemTemplate>
                                                            <HeaderStyle Font-Bold="True" Font-Size="10px" ForeColor="#333333" />
                                                            <ItemStyle Font-Size="12px" />
                                                        </asp:TemplateField>
                                                    </Columns>
                                                    <FooterStyle CssClass="grvFooter" />
                                                    <EditRowStyle />
                                                    <AlternatingRowStyle />
                                                    <PagerStyle CssClass="gvPagination" />
                                                    <HeaderStyle CssClass="grvHeader" />
                                                </asp:GridView>
                                                <br />

                                            </div>


                                        </div>


                                    </div>
                                </div>
                            </div>
                        </section>
                        <!-- /.card -->
                    </div>
                    <!-- /#accordion -->
                </div>
                <!-- /grid column -->


                <div class="row">

                    <div class="col-md-6">
                        <asp:Panel ID="pnlSmpleSizes" runat="server" Visible="false" CssClass="form-inline">
                        </asp:Panel>
                    </div>

                </div>

            </div>

            <br />
            <br />

            <div class="modal fade" id="myModal" tabindex="-1" role="dialog" aria-labelledby="followingModalLabel" aria-hidden="true">
                <div class="modal-dialog modal-dialog-scrollable" role="document">
                    <div class="modal-content">
                        <div class="modal-header">
                            <h4 class="modal-title"><span class="fa fa-table"></span>Replace Sample Images </h4>
                        </div>
                        <div class="modal-body px-0">
                            <div class="card-body">
                                <div id="dropzone" class="fileinput-dropzone">
                                    <span>Drop files or click to upload.</span>
                                    <asp:FileUpload ID="fileuploaddropzone" runat="server"
                                        onchange="submitform();" />
                                </div>
                                <div id="progress" class="progress progress-xs rounded-0 fade">
                                    <div class="progress-bar progress-bar-striped progress-bar-animated bg-success" role="progressbar" aria-valuemin="0" aria-valuemax="100"></div>
                                </div>
                            </div>
                        </div>
                        <div class="modal-footer">
                            <button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>
                        </div>
                    </div>
                </div>
            </div>



            <div id="SpecificationModal" class="modal animated slideInLeft" role="dialog">
                <div class="modal-dialog">
                    <div class="modal-content ">
                        <div class="modal-header">
                            <%--<button type="button" style="background-color: red;" class="close btn btn-xs" data-dismiss="modal"><span class="fa fa-close"></span></button>--%>
                            <h4 class="modal-title"><span class="fa fa-table"></span>Change Specifications </h4>
                        </div>
                        <div class="modal-body form-horizontal">
                            <asp:Label ID="lblHelper" runat="server" Visible="false"></asp:Label>
                            <div class="form-group">
                                <label class="col-md-4">Specifications</label>
                                <div class="col-md-8">
                                    <asp:DropDownList ID="ddlSpecification" CssClass="form-control chzn-select" runat="server">
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>



                        <div class="modal-footer ">
                            <asp:LinkButton ID="lnkbtnSpecChange" runat="server" OnClick="lnkbtnSpecChange_Click" CssClass="btn btn-sm btn-success" OnClientClick="CLoseMOdal();"><span class="glyphicon glyphicon-save"></span> Update </asp:LinkButton>
                            <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                        </div>
                    </div>
                </div>
            </div>

            <div id="ChangeCompModal" class="modal animated slideInLeft" role="dialog">
                <div class="modal-dialog">
                    <div class="modal-content ">
                        <div class="modal-header">
                            <h4 class="modal-title">
                                <span class="fa fa-info-circle"></span>Change Component Name
                            </h4>
                        </div>
                        <div class="modal-body form-horizontal">
                            <asp:Label ID="lblHelperComp" runat="server" Visible="false"></asp:Label>
                            <div class="form-group">
                                <asp:Label ID="mdlLblComponent" runat="server" CssClass="col-md-4">Component Name</asp:Label>

                                <div class="col-md-8">
                                    <asp:DropDownList ID="mdlDdlComponent" runat="server" CssClass="form-control chzn-select "></asp:DropDownList>
                                </div>
                            </div>

                            <div class="modal-footer ">
                                <asp:LinkButton ID="lnkbtnUpdateComponent" runat="server" OnClick="lnkbtnUpdateComponent_Click" CssClass="btn btn-sm btn-success" OnClientClick="CLoseMOdal();"><span class="glyphicon glyphicon-save"></span> Update </asp:LinkButton>
                                <button type="button" class="btn btn-default " data-dismiss="modal">Close</button>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <div id="CopyCompGroupModal" class="modal animated slideInLeft" role="dialog">
                <div class="modal-dialog">
                    <div class="modal-content ">
                        <div class="modal-header">
                            <h4 class="modal-title">
                                <span class="fa fa-info-circle"></span>Select component group to copy
                            </h4>
                        </div>
                        <div class="modal-body form-horizontal">
                            <div>
                                <asp:GridView ID="gvCompGrp" runat="server" AutoGenerateColumns="False" ShowFooter="True" CssClass="table-striped table-hover table-bordered grvContentarea" Width="470px">
                                    <Columns>

                                        <asp:TemplateField HeaderText="SL">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvSl" runat="server" Text='<%# Convert.ToInt32(Container.DataItemIndex+1).ToString("00")%>'
                                                    Width="30px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle Font-Bold="True" Font-Size="12px" ForeColor="#333333" HorizontalAlign="Center" />
                                            <ItemStyle Font-Size="10px" HorizontalAlign="Center" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Component Group">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvCompcode" runat="server" Visible="false" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "COMPCODE")) %>'></asp:Label>
                                                <asp:Label ID="lblgvCompGroup" runat="server" Width="100px" Text='<%# DataBinder.Eval(Container.DataItem, "compdesc").ToString()%>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle Font-Bold="True" Font-Size="12px" ForeColor="#333333" />
                                            <ItemStyle Font-Size="12px" HorizontalAlign="Left" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Total Item">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvTotalItem" runat="server" Width="50px" Text='<%# DataBinder.Eval(Container.DataItem, "totalitem").ToString()%>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle Font-Bold="True" Font-Size="12px" ForeColor="#333333" HorizontalAlign="Center" />
                                            <ItemStyle Font-Size="12px" HorizontalAlign="Center" />
                                        </asp:TemplateField>

                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkCol" runat="server" />
                                            </ItemTemplate>
                                            <HeaderTemplate>
                                                <asp:CheckBox ID="chkhead" onclick="javascript:SelectAllCheckboxes(this);" CssClass="checkbox" ClientIDMode="Static" runat="server" />
                                            </HeaderTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" Width="30px" />
                                            <ItemStyle HorizontalAlign="Center" Width="30px" />
                                        </asp:TemplateField>

                                    </Columns>
                                    <FooterStyle CssClass="grvFooter" />
                                    <EditRowStyle />
                                    <AlternatingRowStyle />
                                    <PagerStyle CssClass="gvPagination" />
                                    <HeaderStyle CssClass="grvHeader" />
                                </asp:GridView>
                            </div>

                            <div class="modal-footer ">
                                <asp:LinkButton ID="lkbtnCopyComp" runat="server" OnClick="lkbtnCopyComp_Click" CssClass="btn btn-sm btn-success" OnClientClick="CLoseMOdal();"> Confirm Copy </asp:LinkButton>
                                <button type="button" class="btn btn-sm btn-default " data-dismiss="modal">Close</button>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <%--    <script type="text/javascript">

        function uploadComplete(sender) {
            $get("<%=((Label)this.Master.FindControl("lblmsg")).ClientID%>").style.color = "green";
            $get("<%=((Label)this.Master.FindControl("lblmsg")).ClientID%>").innerHTML = "File Uploaded Successfully";
            $('#myModal').modal('hide');

        }

        function uploadError(sender) {
            $get("<%=((Label)this.Master.FindControl("lblmsg")).ClientID%>").style.color = "red";
            $get("<%=((Label)this.Master.FindControl("lblmsg")).ClientID%>").innerHTML = "File upload failed.";
            $('#myModal').modal('hide');

        }


    </script>--%>
</asp:Content>