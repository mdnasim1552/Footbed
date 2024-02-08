<%@ Page Title="" Language="C#" MasterPageFile="~/SPE.Master" AutoEventWireup="true" CodeBehind="PurReqEntry02.aspx.cs" Inherits="SPEWEB.F_13_CWare.PurReqEntry02" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script language="javascript" type="text/javascript">
        $(document).ready(function () {
            //For navigating using left and right arrow of the keyboard
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);
        });
        function openModal() {
            $('#myModal').modal({ backdrop: "static" });
        }
        function CLoseMOdal() {
            $('#myModal').modal('hide');

        }

        function pageLoaded() {

            $("input, select").bind("keydown", function (event) {
                var k1 = new KeyPress();
                k1.textBoxHandler(event);
            });
          <%--  var gridview = $('#<%=this.gvReqInfo.ClientID %>');
            $.keynavigation(gridview)--%>;
            $('.chzn-select').chosen({ search_contains: true });


            $(function () {
                $('[id*=ddlResSpcf]').multiselect({
                    
                    maxWidth: 500,
                    maxHeight: 400,
                    includeSelectAllOption: true,
                    searchable: true,
                    enableFiltering: true,
                    onDropdownShow: function (event) {
                        $(this).closest('select').css('width', '500px')
                    }

                })
                $(".Multidropdown button").addClass("multiselect dropdown-toggle btn btn-default btn-sm");
            });

        };
    </script>

    <style>
        /* .fileinput-dropzone {
          
            padding: 1em 2.5rem !important;
        }*/
        #ContentPlaceHolder1_AsyncFileUpload1_ctl01 {
            width: 0px !important;
        }

        .chzn-single {
            border-radius: 3px !important;
            height: 29px !important;
        }

        .rbtnList1 label {
            line-height: 20px;
            margin: 0px 8px 0 0px;
        }
        .Multidropdown ul {
            top: -47px !important;
        }

        .Multidropdown b.caret {
            display: none !important;
        }

        .Multidropdown ul.dropdown-menu {
            min-width: 21rem;
        }

        .Multidropdown .multiselect-container > li > a > label {
            margin: 0;
            height: 100%;
            cursor: pointer;
            font-weight: 400;
            padding: 3px 2px 3px 2px;
            f
    </style>



    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
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
            <div class="card card-fluid">
                <div class="card-body">
                    <div class="row">
                        <div class="col-md-1">
                            <div class="form-group">
                                <asp:Label ID="lblCurDate" runat="server" CssClass="label" Text="Req.Date"></asp:Label>

                                <asp:TextBox ID="txtCurReqDate" runat="server" CssClass="form-control form-control-sm small" TabIndex="5" ToolTip="(dd.mm.yyyy)"></asp:TextBox>
                                <cc1:CalendarExtender ID="txtCurReqDate_CalendarExtender" runat="server"
                                    Format="dd.MM.yyyy" TargetControlID="txtCurReqDate"></cc1:CalendarExtender>
                            </div>
                        </div>
                        <div class="col-md-1">
                            <div class="form-group">
                                <asp:Label ID="lblmrfno" runat="server" CssClass="label" Text="M.R.F. No."></asp:Label>
                                <asp:TextBox ID="txtMRFNo" runat="server" TabIndex="7" CssClass="form-control form-control-sm"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-1">
                            <div class="form-group">

                                <asp:Label ID="lblCurReqNo1" runat="server" CssClass="label" Text="REQ00"></asp:Label>

                                <asp:TextBox ID="txtCurReqNo2" runat="server" CssClass="form-control small form-control-sm disabled readonlyValue" ReadOnly="True" TabIndex="8">00000</asp:TextBox>

                            </div>
                        </div>

                        <div class="col-md-1">
                            <div class="form-group">
                                <asp:Label ID="lblPage" runat="server" >Page Size</asp:Label>
                                <asp:DropDownList ID="ddlpagesize" runat="server" AutoPostBack="True" CssClass="form-control form-control-sm" OnSelectedIndexChanged="ddlpagesize_SelectedIndexChanged">
                                    <asp:ListItem>10</asp:ListItem>
                                    <asp:ListItem>15</asp:ListItem>
                                    <asp:ListItem>20</asp:ListItem>
                                    <asp:ListItem>30</asp:ListItem>
                                    <asp:ListItem>50</asp:ListItem>
                                    <asp:ListItem>100</asp:ListItem>
                                    <asp:ListItem>150</asp:ListItem>
                                    <asp:ListItem>200</asp:ListItem>
                                    <asp:ListItem>300</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-1">
                            <div class="form-group">
                                <asp:Label ID="lblSeason" runat="server" CssClass="label ">Season</asp:Label>
                                <asp:DropDownList ID="DdlSeason" CssClass="form-control form-control-sm chzn-select" AutoPostBack="true" OnSelectedIndexChanged="DdlSeason_SelectedIndexChanged" runat="server"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-2">
                            <div class="form-group" style="margin-top: 20px">
                                <div class="form-inline">
                                    <asp:LinkButton ID="lbtnOk" runat="server" Text="Ok" OnClick="lbtnOk_Click" CssClass="btn btn-primary btn-sm" TabIndex="4"></asp:LinkButton>
                                    &nbsp;
                                    <asp:CheckBox ID="Chckorder" runat="server" AutoPostBack="true"  OnCheckedChanged="Chckorder_CheckedChanged" Text="From Order" />
                                    &nbsp;  
                                    <asp:TextBox ID="TxtReqQty" ToolTip="Add Required FG qty before material Add" runat="server" CssClass="form-control form-control-sm" Width="35%" OnTextChanged="TxtReqQty_TextChanged" AutoPostBack="true" placeholder="Req Qty"></asp:TextBox>
                                </div>
                            </div>
                        </div>

                        <div id="panelorder" class="col-md-5" visible="false" runat="server">
                            <div class="row">

                                <div class="col-md-5">

                                    <div class="form-group">
                                        <asp:Label ID="Label3" runat="server" CssClass="label" Text="Order"></asp:Label>

                                        <asp:DropDownList ID="ddlOrder" runat="server" CssClass="chzn-select form-control form-control-sm" AutoPostBack="true" OnSelectedIndexChanged="ddlOrder_SelectedIndexChanged" TabIndex="3"></asp:DropDownList>
                                    </div>
                                </div> 
                                <div class="col-md-2">

                                    <div class="form-group" style="margin-top: 23px">
                                        <asp:CheckBox ID="ChckJob" runat="server" AutoPostBack="true" CssClass="float-right" OnCheckedChanged="ChckJob_CheckedChanged" Text="Job Work" />

                                     </div>
                                </div>
                                <div class="col-md-5">
                                    <div class="form-group">
                                        <asp:Label ID="Label5" runat="server" CssClass="label" Text="Supplier"></asp:Label>

                                        <asp:DropDownList ID="ddlSupplier" runat="server" CssClass="chzn-select form-control form-control-sm" AutoPostBack="true" OnSelectedIndexChanged="ddlSupplier_SelectedIndexChanged" TabIndex="3"></asp:DropDownList>
                                    </div>


                                </div>

                            </div>
                        </div>
                        <div class="col-md-2">
                            <div class="form-group">
                                <asp:LinkButton ID="ImgbtnFindReq" runat="server" CssClass="label" OnClick="ImgbtnFindReq_Click"
                                    TabIndex="3">Pre. Req. List</asp:LinkButton>

                                <asp:DropDownList ID="ddlPrevReqList" runat="server" CssClass="chzn-select form-control form-control-sm" TabIndex="6">
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-3">
                            <div class="form-group">
                                <asp:Label ID="Label6" runat="server" CssClass="label" Text="Store Name"></asp:Label>
                                <asp:TextBox ID="txtProjectSearch" runat="server" Visible="false" CssClass="form-control form-control-sm" TabIndex="1"></asp:TextBox>
                                <asp:DropDownList ID="ddlProject" runat="server" CssClass="form-control form-control-sm chzn-select" AutoPostBack="True" OnSelectedIndexChanged="ddlProject_SelectedIndexChanged" TabIndex="3"></asp:DropDownList>
                            </div>
                        </div>

                        <div class="col-md-2">
                            <div class="form-group">
                                <asp:Label ID="Label1" runat="server" CssClass="label" Text="Department"></asp:Label>
                                <asp:TextBox ID="TextBox1" runat="server" Visible="false" CssClass="form-control form-control-sm" TabIndex="1"></asp:TextBox>

                                <asp:LinkButton ID="LinkButton1" Visible="false" CssClass="btn btn-primary srearchBtn" runat="server" TabIndex="2"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>


                                <asp:DropDownList ID="ddlDeptCode" runat="server" CssClass="form-control form-control-sm chzn-select" TabIndex="3"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-7" id="Panel1" runat="server">
                            <div class="row">
                                <div class="col-md-4">
                                    <div class="form-group">
                                        <asp:LinkButton ID="ImgbtnFindRes" runat="server" OnClick="ImgbtnFindRes_Click" TabIndex="2" CssClass="label" Text="Material List"></asp:LinkButton>

                                        <asp:TextBox ID="txtResSearch" runat="server" Visible="false" CssClass=" inputTxt inputName inpPixedWidth" TabIndex="1"></asp:TextBox>



                                        <asp:DropDownList ID="ddlResList" runat="server" CssClass="form-control form-control-sm chzn-select" AutoPostBack="True" OnSelectedIndexChanged="ddlResList_SelectedIndexChanged" TabIndex="3"></asp:DropDownList>

                                        <asp:Label ID="Label2" runat="server" Visible="False" CssClass="dataLblview label txtAlgLeft"></asp:Label>


                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <asp:Label ID="lblSpecification" runat="server" CssClass="label" Text="Specification"></asp:Label>

                                        <asp:TextBox ID="txtSrchSpecification" Visible="false" runat="server" CssClass="inputTxt inputName inpPixedWidth" TabIndex="5"></asp:TextBox>

                                        <asp:LinkButton ID="ImgbtnSpecification" Visible="false" CssClass="btn btn-primary srearchBtn" runat="server" OnClick="ImgbtnSpecification_Click" TabIndex="2"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>

                                        <%--<asp:DropDownList ID="ddlResSpcf" runat="server" CssClass="form-control form-control-sm chzn-select" TabIndex="3"></asp:DropDownList>--%>

                                        <div class="Multidropdown" style="border: 1px solid #c6c9d5 !important; border-radius: 5px; Width :200px">
                                            <asp:ListBox ID="ddlResSpcf" SelectionMode="Multiple" CssClass="form-control multiselect-search"  runat="server"></asp:ListBox>

                                        </div>


                                    </div>

                                </div>
                                <div class="col-md-2">
                                    <div class="form-group" style="margin-top: 20px">
                                        <asp:LinkButton ID="lbtnSelectRes" runat="server" OnClick="lbtnSelectRes_Click" CssClass="btn btn-primary btn-sm"><span class="fa fa-check"></span></asp:LinkButton>
                                        <asp:LinkButton ID="lbtnSelectAll" runat="server" CssClass="btn btn-primary btn-sm" OnClick="lbtnSelectAll_Click"><span class="fa fa-check-double"></span></asp:LinkButton>
                                        <asp:LinkButton ID="LbtnWithImage" runat="server" ToolTip="Items with Images" CssClass="btn btn-warning btn-sm" OnClick="LbtnWithImage_Click"><span class="fa fa-image"></span></asp:LinkButton>


                                    </div>
                                </div>

                                <div class="col-md-2">
                                    <div class="form-group">
                                        <asp:LinkButton ID="btnCurr" runat="server" CssClass="label" Text="Currency:"></asp:LinkButton>
                                        <div class="input-group input-group-sm input-group-alt">
                                            <asp:DropDownList ID="ddlCurrency" CssClass="form-control form-control-sm" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlCurrency_SelectedIndexChanged"></asp:DropDownList>

                                            <div class="input-group-append">
                                                <asp:HyperLink ID="HyperLink1" runat="server" CssClass="input-group-text text-success" ToolTip="Create List" Target="_blank"
                                                    NavigateUrl="~/F_34_Mgt/AccConversion"><span class="fa fa-plus"></span></asp:HyperLink>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-1">
                                    <div class="form-group">
                                        <asp:Label ID="Label22" runat="server" CssClass=" smLbl_to" Text="Rate:"></asp:Label>
                                        <asp:TextBox ID="lblConRate" runat="server" CssClass="form-control form-control-sm small"></asp:TextBox>

                                    </div>
                                </div>

                            </div>

                            <div class="" style="display: none;">
                                <asp:Label ID="lblCurNo" Visible="false" runat="server" CssClass="lblTxt lblName" Text="Requisition No."></asp:Label>
                                <asp:TextBox ID="txtReqText" Visible="false" runat="server" TabIndex="5" CssClass="inputTxt inputName inputBlink inpPixedWidth"></asp:TextBox>

                                <asp:LinkButton ID="ImgbtnReqse" Visible="false" CssClass="btn btn-primary srearchBtn" runat="server" OnClick="ImgbtnReqse_Click" TabIndex="6"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>

                            </div>

                            <div class="">

                                <asp:CheckBox ID="chkdupMRF" runat="server" Text="Dup.M.R.F" CssClass="btn btn-primary checkBox" Visible="false"
                                    TabIndex="9" />
                                <asp:CheckBox ID="chkneBudget" runat="server" Text="Not Exceed Budget" CssClass="btn btn-primary checkBox" Visible="false"
                                    TabIndex="10" />

                            </div>

                        </div>


                    </div>



                    <%-- <div class="col-md-1 pading5px">
                                            <cc1:ListSearchExtender ID="ListSearchExt1" runat="server"
                                                QueryPattern="Contains" TargetControlID="ddlResList">
                                            </cc1:ListSearchExtender>
                                           
                                        </div>--%>
                </div>
            </div>

            <div class="" style="min-height: 450px">


                <div class="row" id="Panel2" runat="server" visible="False">

                    <div class="col-md-10">
                        <div class="card card-fluid">
                            <div class="card-body" style="min-height: 450px;">
                                <div class="row">
                                    <div class="col-md-12">
                                        <asp:GridView ID="gvReqInfo" runat="server" AllowPaging="True" CssClass="table-striped table-hover table-bordered grvContentarea"
                                            AutoGenerateColumns="False" OnRowDeleting="gvReqInfo_RowDeleting"
                                            ShowFooter="True" Width="16px" PageSize="15" OnPageIndexChanging="gvReqInfo_PageIndexChanging"
                                            OnRowDataBound="gvReqInfo_RowDataBound">
                                            <PagerSettings Visible="True" />
                                            <RowStyle />
                                            <Columns>
                                                <asp:TemplateField HeaderText="Sl">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvSlNo0" runat="server"
                                                            Style="text-align: right"
                                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="15px"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:CommandField ShowDeleteButton="True" ControlStyle-CssClass="text-red" DeleteText="<span class='fa fa-trash'></span>" />

                                                <asp:TemplateField HeaderText="Res Code" Visible="False">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvResCod" runat="server" Height="16px"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rsircode")) %>'
                                                            Width="80px"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Spcf Code" Visible="False">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvSpcfCod" runat="server" Height="16px"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "spcfcod")) %>'
                                                            Width="80px"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Description <br> of  Goods">

                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvResDesc" runat="server" Font-Size="9px"
                                                            Text='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "rsirdesc1"))   %>'
                                                            Width="130px">
                                                            
                                                            
                                                        </asp:Label>

                                                    </ItemTemplate>
                                                    <FooterStyle HorizontalAlign="Center" />
                                                    <HeaderStyle HorizontalAlign="Left" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="THICKNESS/</br>SIZE">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvspcfdesc" runat="server"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "spcfdesc")) %>'
                                                            Width="60px"></asp:Label>
                                                    </ItemTemplate>

                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="WIDTH/</br>LEN">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvdesc1" runat="server"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "desc1")) %>'
                                                            Width="50px"></asp:Label>
                                                    </ItemTemplate>

                                                </asp:TemplateField>
                                                <%--  <asp:TemplateField HeaderText="Product Code">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvdesc5" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "desc5")) %>'
                                                    Width="50px"></asp:Label>
                                            </ItemTemplate>

                                        </asp:TemplateField>--%>
                                                <asp:TemplateField HeaderText="Colour">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvdesc2" runat="server"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "desc2")) %>'
                                                            Width="40px"></asp:Label>
                                                    </ItemTemplate>

                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Brand">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvdesc3" runat="server"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "desc3")) %>'
                                                            Width="50px"></asp:Label>
                                                    </ItemTemplate>

                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Unit">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvResUnit" runat="server"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rsirunit")) %>'
                                                            Width="35px"></asp:Label>
                                                    </ItemTemplate>

                                                </asp:TemplateField>





                                                <asp:TemplateField HeaderText="Present <br> Stock">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvstkqty" runat="server" Style="text-align: right" Font-Size="9px"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "stkqty")).ToString("#,##0.0000;-#,##0.0000; ") %>'
                                                            Width="60px"></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Minimum Stock" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvminstkqty" runat="server" Style="text-align: right"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "minstkqty")).ToString("#,##0.0000;-#,##0.0000; ") %>'
                                                            Width="70px"></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </asp:TemplateField>


                                                <asp:TemplateField HeaderText="Useable Stock" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvuseablestkqty" runat="server" Style="text-align: right"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "useablestk")).ToString("#,##0.000;-#,##0.000; ") %>'
                                                            Width="70px"></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Package size" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="tXTgvPKg" runat="server"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pkgsize")) %>'
                                                            Width="35px"></asp:TextBox>
                                                    </ItemTemplate>

                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Present Requirement">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtgvReqQty" runat="server" BorderColor="#99CCFF"
                                                            BorderStyle="Solid" BorderWidth="0px" Font-Size="9px" AutoPostBack="true"
                                                            Style="text-align: right; background-color: Transparent" OnTextChanged="TxtQuantity_TextChanged"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "preqty")).ToString("#,##0.0000;-#,##0.0000; ") %>'
                                                            Width="70px"></asp:TextBox>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label ID="lblgvFooterReqQty" runat="server"
                                                            Width="70px"></asp:Label>
                                                    </FooterTemplate>
                                                    <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Right" />

                                                    <ItemStyle HorizontalAlign="Right" />
                                                </asp:TemplateField>


                                                <asp:TemplateField HeaderText="Store Supply Date" Visible="False">
                                                    <%--  <FooterTemplate>
                                                <asp:LinkButton ID="lbtnUpdateResReq" runat="server" CssClass="btn btn-danger primaryBtn" OnClick="lbtnUpdateResReq_Click">Final Update</asp:LinkButton>
                                            </FooterTemplate>--%>
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtgvUseDat" runat="server" BorderColor="#99CCFF"
                                                            BorderStyle="Solid" BorderWidth="0px" Font-Size="11px"
                                                            Style="text-align: left; background-color: Transparent"
                                                            Text='<%# DataBinder.Eval(Container.DataItem, "expusedt").ToString() %>'
                                                            Width="80px"></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Approv.Qty" Visible="False">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtgvappQty" runat="server" BackColor="White"
                                                            BorderStyle="None" Style="text-align: right"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "areqty")).ToString("#,##0.0000;-#,##0.0000; ") %>'
                                                            Width="80px"></asp:TextBox>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:LinkButton ID="lbtnResFooterDelete" runat="server" CssClass="btn btn-danger primaryBtn"
                                                            OnClick="lbtnResFooterDelete_Click">Delete</asp:LinkButton>
                                                    </FooterTemplate>
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Supplier" Visible="False">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvssirdesc" runat="server"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "ssirdesc")) %>'
                                                            Width="180px"></asp:Label>
                                                    </ItemTemplate>

                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Last Purchase Rate" Visible="False">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lgvgvlpurRate" runat="server" BorderColor="#99CCFF"
                                                            BorderStyle="Solid" BorderWidth="1px" Font-Size="11px"
                                                            Style="text-align: right; background-color: Transparent"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "lpurrate")).ToString("#,##0.000000;(#,##0.000000); ") %>'
                                                            Width="70px"></asp:Label>
                                                    </ItemTemplate>

                                                    <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Right" />
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Purchase Supply Date" Visible="False">

                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtgvpursupDat" runat="server" BorderColor="#99CCFF"
                                                            BorderStyle="Solid" BorderWidth="0px" Font-Size="11px"
                                                            Style="text-align: left; background-color: Transparent"
                                                            Text='<%# DataBinder.Eval(Container.DataItem, "pursdate").ToString() %>'
                                                            Width="80px"></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>


                                                <asp:TemplateField HeaderText="Proposed Rate">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtgvResRat" runat="server" BorderColor="#99CCFF"
                                                            BorderStyle="Solid" BorderWidth="0px" Font-Size="9px" AutoPostBack="true"
                                                            Style="text-align: right; background-color: Transparent" OnTextChanged="TxtQuantity_TextChanged"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "reqrat")).ToString("#,##0.000000;(#,##0.000000);  ") %>'
                                                            Width="70px"></asp:TextBox>
                                                    </ItemTemplate>
                                                    <%--<FooterTemplate>
                                                <asp:LinkButton ID="lbtnResFooterTotal" runat="server" CssClass="btn btn-primary   primarygrdBtn"
                                                    OnClick="lbtnResFooterTotal_Click">Total :</asp:LinkButton>
                                            </FooterTemplate>
                                            <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Right" />--%>
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </asp:TemplateField>




                                                <asp:TemplateField HeaderText="Total Amount">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvTAprAmt" runat="server" Font-Size="9px"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "preqamt")).ToString("#,##0.000;(#,##0.000); ") %>'
                                                            Width="70px"></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label ID="lblgvFooterTAprAmt" runat="server"
                                                            Width="70px"></asp:Label>
                                                    </FooterTemplate>
                                                    <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Right" />
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="BDT Amount">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvTbdtAmt" runat="server"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "bdtamt")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                            Width="70px"></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label ID="lblgvFooterTbdtAmt" runat="server"
                                                            Width="70px"></asp:Label>
                                                    </FooterTemplate>
                                                    <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Right" />
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </asp:TemplateField>



                                                <asp:TemplateField HeaderText="Used For <span style='color:red; font-size:18px'>*</span>">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtgvReqNote" runat="server" BorderColor="#99CCFF" TextMode="SingleLine"
                                                            BorderStyle="Solid" BorderWidth="0px" Font-Size="11px"
                                                            Style="text-align: left; background-color: Transparent"
                                                            Text='<%# DataBinder.Eval(Container.DataItem, "reqnote").ToString() %>'
                                                            Width="80px"></asp:TextBox>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Left" />
                                                </asp:TemplateField>



                                                <asp:TemplateField HeaderText="Store Code" Visible="False">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvstorecode" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "storecode").ToString() %>'
                                                            Width="70px"></asp:Label>
                                                    </ItemTemplate>


                                                    <HeaderStyle HorizontalAlign="Left" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="ResCode" Visible="False">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvrsircode" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "rsircode").ToString() %>'
                                                            Width="70px"></asp:Label>
                                                    </ItemTemplate>


                                                    <HeaderStyle HorizontalAlign="Left" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Conversion</br> Rate" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="txtgvConRate" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                                            BorderWidth="0px" Font-Size="11px" Style="text-align: right; background-color: Transparent"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "conrate")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                            Width="60px"></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                    </FooterTemplate>
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </asp:TemplateField>




                                                <asp:TemplateField HeaderText="Nominated">
                                                    <HeaderTemplate>
                                                        <asp:DropDownList ID="ddlBudgetHead" AutoPostBack="true" OnSelectedIndexChanged="ddlBudgetHead_SelectedIndexChanged" CssClass="form-control form-control-sm" Width="100px" runat="server">
                                                            <%--  <asp:ListItem Value="Yes" Selected="True">Yes</asp:ListItem>
                                                    <asp:ListItem Value="No">No</asp:ListItem>--%>
                                                        </asp:DropDownList>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:DropDownList ID="ddlBudget" CssClass="form-control  chzn-select" Width="100px" autocomplete="off" runat="server">
                                                            <%--  <asp:ListItem Value="Yes" Selected="True">Yes</asp:ListItem>
                                                    <asp:ListItem Value="No">No</asp:ListItem>--%>
                                                        </asp:DropDownList>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Purchase Type">
                                                    <ItemTemplate>
                                                        <asp:DropDownList ID="ddlType" CssClass="chzn-select" runat="server">

                                                            <asp:ListItem Value="">Select</asp:ListItem>
                                                            <asp:ListItem Value="fixedasset">Fixed Asset</asp:ListItem>
                                                            <asp:ListItem Value="Others">Others</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                </asp:TemplateField>

                                                <%--                                         <asp:TemplateField HeaderText="Nominated">
                                            <ItemTemplate>
                                                <asp:DropDownList ID="ddlnominated" CssClass="inputTxt chzn-select" runat="server"></asp:DropDownList>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle Width="80px" />
                                        </asp:TemplateField>--%>
                                                <asp:TemplateField HeaderText="BOMID">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvBomid" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                                            BorderWidth="0px" Font-Size="9px" Style="text-align: right; background-color: Transparent"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "bomid")) %>'
                                                            Width="60px"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    <ItemStyle Width="60px" />
                                                </asp:TemplateField>
                                            </Columns>
                                            <FooterStyle CssClass="grvFooter" />
                                            <EditRowStyle />
                                            <AlternatingRowStyle />
                                            <PagerStyle CssClass="gvPagination" />
                                            <HeaderStyle CssClass="grvHeader" />
                                        </asp:GridView>
                                        <asp:Panel ID="PnlDesc" runat="server" Visible="False">
                                            <asp:Panel ID="Panel4" runat="server" Visible="False">
                                                <asp:Label ID="lblDescription" runat="server" CssClass="lblTxt panelTitel" Text="Description:"></asp:Label>

                                                <div class="table-responsive">
                                                    <asp:GridView ID="gvDescrip" runat="server" AutoGenerateColumns="False" CssClass="table-striped table-hover table-bordered grvContentarea inptNoneBorder">
                                                        <PagerSettings Visible="False" />
                                                        <RowStyle />
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="Sl">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvSlNo1" runat="server" Height="16px"
                                                                        Style="text-align: right"
                                                                        Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <ItemStyle Width="30px" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Terms ID" Visible="False">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvTermsID" runat="server" Height="16px"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "termsid")) %>'
                                                                        Width="80px"></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Subject">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtgvSubject" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "termssubj").ToString() %>' CssClass="form-control txtAlgRight "></asp:TextBox>
                                                                </ItemTemplate>
                                                                <FooterStyle HorizontalAlign="Center" />
                                                                <HeaderStyle HorizontalAlign="Left" Width="150px" />
                                                            </asp:TemplateField>
                                                            <%--<asp:TemplateField HeaderText="">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvColon" runat="server" Text=" : "></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle HorizontalAlign="Center" />
                                            <HeaderStyle HorizontalAlign="Left" Width="50px" />
                                        </asp:TemplateField>--%>
                                                            <asp:TemplateField HeaderText="Description">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtgvDesc" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "termsdesc").ToString() %>' CssClass="form-control"></asp:TextBox>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Left" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Remarks">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtgvRemarks" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "termsrmrk").ToString() %>' CssClass="form-control"></asp:TextBox>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Left" />
                                                            </asp:TemplateField>
                                                        </Columns>
                                                        <PagerStyle CssClass="gvPagination" />
                                                        <HeaderStyle BackColor="#F5F5F5" ForeColor="#000" />
                                                        <EditRowStyle BackColor="#E2D5CD" VerticalAlign="Top" />
                                                        <AlternatingRowStyle />
                                                    </asp:GridView>
                                                </div>
                                            </asp:Panel>
                                            <asp:Label ID="lblJus" CssClass="lblHead" Visible="false" runat="server"><h4> Selected Vendor Justification</h4> </asp:Label>

                                            <div class="col-md-4 pading5px" style="display: none;">
                                                <div class="input-group">
                                                    <span class="input-group-addon glypingraddon"></span>
                                                    <asp:TextBox ID="txtMSRNarr" runat="server" class="form-control" Visible="false" Rows="2" TextMode="MultiLine"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <asp:Label ID="lblMurketSurvey" runat="server" CssClass="smLbl_to text-left" Visible="False"></asp:Label>

                                                <asp:Label ID="lblsurveyby" CssClass="smLbl_to text-left" runat="server"></asp:Label>
                                            </div>
                                            <asp:GridView ID="gvMSRInfo" runat="server" AutoGenerateColumns="False" CssClass="table-striped table-hover table-bordered grvContentarea"
                                                ShowFooter="True" Width="274px">
                                                <PagerSettings Visible="False" />
                                                <RowStyle />
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Sl">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvMSRSlNo" runat="server" Height="16px"
                                                                Style="text-align: right"
                                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>


                                                    <asp:TemplateField HeaderText=" Materials Description ">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvMSRResDesc" runat="server"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rsirdesc1")) %>'
                                                                Width="180px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Left" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Specification">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvspcfdescServery" runat="server"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "spcfdesc")) %>'
                                                                Width="70px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Left" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Unit">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvMSRResUnit" runat="server"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rsirunit")) %>'
                                                                Width="30px"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Supplier Name">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvMSRSuplDesc" runat="server"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "ssirdesc1")) %>'
                                                                Width="150px"></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterStyle HorizontalAlign="Center" />
                                                        <HeaderStyle HorizontalAlign="Left" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Unit Price">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvMSRRate" runat="server" BorderColor="#99CCFF"
                                                                BorderStyle="Solid" BorderWidth="0px" Font-Size="11px"
                                                                Style="text-align: right; background-color: Transparent"
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "resrate")).ToString("#,##0.000000;(#,##0.000000); ") %>'
                                                                Width="60px"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Credit Period (Day)">
                                                        <ItemTemplate>
                                                            <asp:Label ID="txtgvpaytrm" runat="server" BorderColor="#99CCFF"
                                                                BorderStyle="Solid" BorderWidth="0px" Font-Bold="False" Font-Size="11px"
                                                                Height="16px" Style="text-align: left; background-color: Transparent"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "paytrm")) %>'
                                                                Width="100px"></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterStyle HorizontalAlign="Center" />
                                                        <HeaderStyle HorizontalAlign="Left" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Concern  Person">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvMSRCperson" runat="server"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "cperson")) %>'
                                                                Width="120px"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Telephone">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvMSRPhone" runat="server"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "phone")) %>'
                                                                Width="80px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Mobile">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvMSRMobile" runat="server"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mobile")) %>'
                                                                Width="80px"></asp:Label>
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

                                        </asp:Panel>


                                        <asp:Panel runat="server" ID="pnlAttacDeocx">
                                            <div class="row">
                                                <div class="col-md-12">
                                                    <div class="log-divider">
                                                        <span>
                                                            <i class="fa fa-fw fa-file"></i>Uploaded Files 
                                    <asp:LinkButton ID="btnShowimg" OnClick="btnShowimg_OnClick" runat="server"><span class="fa fa-sync"></span></asp:LinkButton>
                                                        </span>
                                                    </div>
                                                    <asp:ListView ID="ListViewEmpAll" runat="server" ItemPlaceholderID="itemplaceholder" OnItemDataBound="ListViewEmpAll_ItemDataBound">
                                                        <LayoutTemplate>
                                                            <asp:PlaceHolder ID="itemplaceholder" runat="server" />
                                                        </LayoutTemplate>
                                                        <ItemTemplate>
                                                            <div class="col-xs-12 col-sm-4 col-md-2 listDiv" style="padding: 0 5px;">
                                                                <div id="EmpAll" runat="server">

                                                                    <asp:Label ID="ImgLink" Visible="False" runat="server" Text='<%# Eval("itemsurl") %>'></asp:Label>
                                                                    <asp:Label ID="reqno" Visible="False" runat="server" Text='<%# Eval("reqno") %>'></asp:Label>

                                                                    <a href="../Upload/ReqDoc/<%# Eval("itemsurl") %>" class="uploadedimg" target="_blank">
                                                                        <asp:Image ID="GetImg" runat="server" CssClass="image img img-responsive img-thumbnail" />
                                                                    </a>
                                                                    <div class="checkboxcls">
                                                                        <asp:CheckBox ID="ChDel" runat="server" />
                                                                    </div>


                                                                </div>

                                                            </div>
                                                        </ItemTemplate>
                                                    </asp:ListView>
                                                </div>
                                            </div>

                                        </asp:Panel>
                                    </div>
                                </div>
                            </div>

                        </div>

                    </div>
                    <div class="col-md-2">
                        <div class="card card-fluid">
                            <div class="card-body">
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="form-group">
                                            <asp:Label ID="Label4" runat="server" CssClass=" smLbl_to" Text="Cost Center"></asp:Label>
                                            <asp:DropDownList ID="ddlDeptCode2" runat="server" CssClass="form-control form-control-sm chzn-select" TabIndex="3"></asp:DropDownList>
                                        </div>

                                    </div>
                                    <div class="col-md-12">
                                        <div class="form-group">
                                            <asp:Label ID="lblReqNarr" runat="server" CssClass="label" Text="Narration:"></asp:Label>

                                            <asp:TextBox ID="txtReqNarr" runat="server" class="form-control form-control-sm" Rows="2" TextMode="MultiLine"></asp:TextBox>
                                        </div>
                                    </div>



                                    <div class="col-md-12">
                                        <div class="form-group">

                                            <asp:Label ID="lblExpDeliveryDate" runat="server" CssClass="label" Text="Exp.Del. Date:"></asp:Label>

                                            <asp:TextBox ID="txtExpDeliveryDate" runat="server" CssClass="form-control form-control-sm" ToolTip="dd.MM.yyyy"></asp:TextBox>
                                            <cc1:CalendarExtender ID="CalendarExtender1" runat="server"
                                                Format="dd.MM.yyyy" TargetControlID="txtExpDeliveryDate"></cc1:CalendarExtender>
                                        </div>

                                    </div>





                                    <div class="form-group" style="display: none">
                                        <div class="col-md-3 pading5px">
                                            <div class="input-group">
                                                <span class="input-group-addon glypingraddon">

                                                    <asp:Label ID="lblPreparedBy" runat="server" CssClass="lblTxt" Text="Prepared By:" Visible="False"></asp:Label>
                                                </span>
                                                <asp:TextBox ID="txtPreparedBy" runat="server" Visible="False" CssClass="form-control inputTxt"></asp:TextBox>

                                            </div>
                                        </div>
                                        <div class="col-md-3 pading5px">
                                            <div class="input-group">
                                                <span class="input-group-addon glypingraddon">
                                                    <asp:Label ID="lblApprovedBy" runat="server" CssClass="lblTxt" Text="Approved By:" Visible="False"></asp:Label>
                                                </span>
                                                <asp:TextBox ID="txtApprovedBy" runat="server" Visible="False" CssClass="form-control inputTxt"></asp:TextBox>

                                            </div>
                                        </div>
                                        <div class="col-md-3 pading5px">
                                            <div class="input-group">
                                                <span class="input-group-addon glypingraddon">
                                                    <asp:Label ID="lblApprovalDate" runat="server" CssClass="lblTxt" Text="Approv.Date:" Visible="False"></asp:Label>
                                                </span>
                                                <asp:TextBox ID="txtApprovalDate" runat="server" CssClass="form-control inputTxt" ToolTip="(dd.mm.yyyy)" Visible="False"></asp:TextBox>

                                            </div>
                                        </div>


                                        <div class="clearfix"></div>
                                    </div>
                                </div>
                                <div class="row" id="pnlSuplist" runat="server" visible="false">
                                    <div class="col-md-12">
                                        <div class="form-group">
                                            <asp:Label ID="Label8" runat="server" CssClass="label" Text="Supplier"></asp:Label>


                                            <asp:DropDownList ID="ddlSup" runat="server" CssClass="form-control form-control-sm chzn-select" TabIndex="3"></asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" InitialValue="" ControlToValidate="ddlSup" ErrorMessage="Supplier required" ForeColor="Red" Display="Dynamic" CssClass="ValidationError"
                                                Text="*Supplier required"
                                                ValidationGroup="FormValiCheck"> 
                                            </asp:RequiredFieldValidator>
                                        </div>


                                    </div>

                                </div>
                                <div class="row" id="pnlPurType" runat="server" visible="false">

                                    <div class="col-md-12">
                                        <div class="form-group">
                                            <asp:Label ID="lblpType" runat="server" CssClass="label" Text="Purchase Type"></asp:Label>



                                            <asp:DropDownList ID="ddlPurType" runat="server" CssClass="form-control form-control-sm chzn-select" OnSelectedIndexChanged="ddlPurType_SelectedIndexChanged" Width="100px" TabIndex="3"></asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" InitialValue="" ControlToValidate="ddlPurType" ErrorMessage="Supplier required" ForeColor="Red" Display="Dynamic" CssClass="ValidationError"
                                                Text="*Required"
                                                ValidationGroup="FormValiCheck"> 
                                            </asp:RequiredFieldValidator>
                                        </div>

                                    </div>
                                </div>
                                <div id="dropzone" class="fileinput-dropzone">
                                    <span>click to upload.</span>
                                    <cc1:AsyncFileUpload OnClientUploadError="uploadError"
                                        OnClientUploadComplete="uploadComplete" runat="server"
                                        ID="AsyncFileUpload1" UploaderStyle="Modern"
                                        ThrobberID="imgLoader"
                                        OnUploadedComplete="FileUploadComplete" />

                                </div>
                                <div id="progress" class="progress progress-xs rounded-0 fade">
                                    <div class="progress-bar progress-bar-striped progress-bar-animated bg-success" role="progressbar" aria-valuemin="0" aria-valuemax="100"></div>
                                </div>
                                <asp:Label ID="lblMesg" runat="server" Text=""></asp:Label>

                            </div>


                        </div>
                    </div>
                </div>
            </div>
            <script type="text/javascript">
                function uploadComplete(sender) {
               <%-- $get("<%=lblMesg.ClientID%>").style.color = "green";
                $get("<%=lblMesg.ClientID%>").innerHTML = "File Uploaded Successfully";--%>
                }

                function uploadError(sender) {
               <%-- $get("<%=lblMesg.ClientID%>").style.color = "red";
                $get("<%=lblMesg.ClientID%>").innerHTML = "File upload failed.";--%>
                }


            </script>
            <div id="myModal" class="modal export" role="dialog">
                <div class="modal-dialog modal-lg ">
                    <div class="modal-content  ">
                        <div class="modal-header">
                            <h4 class="modal-title">
                                <span class="fa fa-table mr-2"></span>Item Specification List with Images 
                            </h4>
                        </div>

                        <div class="modal-body">

                            <div class="row">
                                <div class="col-md-12">                                      
                                        <div class="card-body"> 
                                              
                                                    <asp:GridView ID="gvItemdetails" runat="server" AutoGenerateColumns="False" Height="1px" 
                                                        ShowFooter="True" Width="453px" CssClass=" table-striped table-hover table-bordered grvContentarea" Font-Size="11px">

                                                        <Columns>
                                                            <asp:TemplateField HeaderText="Sl">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="DlblgvSlNo0" runat="server"
                                                                        Style="text-align: right"
                                                                        Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <ItemTemplate>
                                                                    <asp:CheckBox ID="chkColItem" runat="server" />
                                                                </ItemTemplate>
                                                                <HeaderStyle Font-Bold="True" Font-Size="12px" />
                                                            </asp:TemplateField>                                                            
                                                            <asp:TemplateField HeaderText="Description ">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="LblResDesc" runat="server"
                                                                        BackColor="Transparent" BorderStyle="None" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "spcfdesc")) %>'
                                                                        Width="540px"></asp:Label>

                                                                </ItemTemplate>
                                                                <FooterStyle HorizontalAlign="Left" />
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>                                                    
                                                              <asp:TemplateField HeaderText="IMAGE">
                                                    <ItemTemplate>
                                                        <asp:HyperLink ID="hyprrrpg" runat="server" NavigateUrl='<%# (Eval("photo").ToString()=="")?"~/images/no_img_preview.png":Eval("photo") %>' Target="_blank">
                                                            <asp:Image ID="lblImageUrlpg" Width="70" Height="60" runat="server" ImageUrl='<%# (Eval("photo").ToString()=="")?"~/images/no_img_preview.png":Eval("photo") %>' class="img-responsive"></asp:Image>
                                                        </asp:HyperLink>

                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                </asp:TemplateField>
                                                        </Columns>
                                                        <PagerStyle CssClass="gvPagination" />
                                                        <HeaderStyle CssClass="grvHeader" />
                                                        <FooterStyle CssClass="grvFooter" />
                                                        <RowStyle CssClass="grvRows" />
                                                    </asp:GridView>
                                             
                                            
                                        </div>
                                      


                                </div>
                            </div>

                        </div>
                        <div class="modal-footer ">
                            <asp:LinkButton ID="LbtnPushGrid" OnClientClick="CLoseMOdal()" CssClass="btn btn-sm btn-subtle-info" OnClick="LbtnPushGrid_Click" runat="server">Add Selected Item</asp:LinkButton>
                            <button type="button" class="btn btn-sm btn-default" data-dismiss="modal">Close</button>

                        </div>
                    </div>
                </div>
            </div>


        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>

