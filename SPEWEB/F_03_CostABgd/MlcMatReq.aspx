<%@ Page Title="" Language="C#" MasterPageFile="~/SPE.Master" AutoEventWireup="true" CodeBehind="MlcMatReq.aspx.cs" Inherits="SPEWEB.F_03_CostABgd.MlcMatReq" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" EnableViewState="false" runat="Server">


    <script type="text/javascript" language="javascript">
        function openModal() {
            $('#myModal').modal('toggle');
        }
        function SpcfChangModal() {
            $('#SpecificationModal').modal('toggle');
        }

        function CLoseMOdal() {
            $('#myModal').modal('hide');
            $('#SpecificationModal').modal('hide');
        }
        function myModalSupli() {
            $('#myModalSupli').modal('toggle');
        }
        function CLoseModalSupli() {
            $('#myModalSupli').modal('hide');
        }
        function myModalSupliLoc() {
            $('#myModalSupliLoc').modal('toggle');
        }
        function CLoseModalSupliLoc() {
            $('#myModalSupliLoc').modal('hide');
        }
        <%--function SetPlug(plug) {
            // alert(plug);

            $('#<%=this.TabName.ClientID %>').val(plug);
        }--%>
        $(document).ready(function () {

            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);

        });

        function pageLoaded() {

           <%-- var tab = (document.getElementById('<%= TabName.ClientID%>').value == "") ? "tab2primary" : document.getElementById('<%= TabName.ClientID%>').value;
            /*alert(tab);*/
            $('#<%=this.TabName.ClientID %>').val(tab);
            $('#Tabs a[href="#' + tab + '"]').tab('show');--%>
            $('.chzn-select').chosen({ search_contains: true });

        }

    </script>
    <script type="text/javascript">
        //    $(function () {
        $(function () {
        <%--     var tabName = ($('<%= TabName.ClientID%>').val() != "") ? $('<%= TabName.ClientID%>').val() : "AdditionLocal";--%>

        });
    </script>
    <script type="text/javascript">
        function uploadComplete(sender) {
            $('#myModal').modal('hide');
            $get("<%=((Label)this.Master.FindControl("lblmsg")).ClientID%>").style.color = "green";
            $get("<%=((Label)this.Master.FindControl("lblmsg")).ClientID%>").innerHTML = "File Uploaded Successfully";
        }

        function uploadError(sender) {
            $('#myModal').modal('hide');
            $get("<%=((Label)this.Master.FindControl("lblmsg")).ClientID%>").style.color = "red";
            $get("<%=((Label)this.Master.FindControl("lblmsg")).ClientID%>").innerHTML = "File upload failed.";
        }


    </script>
    <style>
        .chzn-single {
            border-radius: 3px !important;
            height: 29px !important;
        }

        .rdbtn td label{
            margin-right:20px;
        }

        .rdbtn td input{
            margin-right: 7px;
        }
    </style>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>



            <div class="card card-fluid">
                <div class="card-body">
                    <div class="row">
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
                        <div class="col-md-4 ">
                            <div class="form-group">
                                <asp:Label ID="Label1" runat="server" CssClass="label" Text="Master L/C"></asp:Label>

                                <asp:DropDownList ID="ddlmlccode" runat="server" CssClass="form-control form-control-sm chzn-select" OnSelectedIndexChanged="ddlmlccode_SelectedIndexChanged" AutoPostBack="true" TabIndex="3"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-2">
                            <div class="form-group">
                                <asp:Label ID="lblarticle" runat="server" CssClass="label" Text="Article"></asp:Label>
                                <asp:DropDownList ID="ddlArticle" runat="server" CssClass="form-control form-control-sm chzn-select" OnSelectedIndexChanged="ddlArticle_SelectedIndexChanged" AutoPostBack="True" TabIndex="3"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-2 ">
                            <div class="form-group">
                                <asp:Label ID="lblcolor" runat="server" CssClass="smLbl_to" Text="Color"></asp:Label>
                                <asp:DropDownList ID="ddlcolor" runat="server" CssClass="form-control form-control-sm chzn-select" AutoPostBack="True" TabIndex="3"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-1 ">
                            <div class="form-group" style="margin-top: 20px">
                                <div class="row">
                                    <asp:LinkButton ID="OkBtn" OnClick="OkBtn_Click" runat="server" CssClass="btn btn-primary btn-sm mr-1">Ok</asp:LinkButton>
                                    <asp:LinkButton ID="lbtncopyto" OnClick="lbtncopyto_Click" runat="server" ToolTip="Copy From" CssClass="btn btn-warning btn-sm"><span class="fa fa-upload"></span></asp:LinkButton>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-1 ">
                            <div class="form-group">
                                <asp:DropDownList ID="ddlFromcolor" Visible="false" runat="server" CssClass="inputTxt chzn-select" TabIndex="3"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-1 ">
                            <div class="form-group" style="margin-top: 20px">
                                <asp:LinkButton ID="lbtnCopyBom" OnClientClick="return confirm('Are you agree to copy from this Style?')" Visible="false" OnClick="lbtnCopyBom_Click" runat="server" CssClass="btn btn-primary btn-sm">Click</asp:LinkButton>
                            </div>
                        </div>
                        <div class="col-md-1">
                            <div class="form-group" style="margin-top: 20px">
                                <div class="dropdown">
                                    <button class="btn btn-primary btn-xs dropdown-toggle" type="button" data-toggle="dropdown">
                                        <span class="glyphicon glyphicon-plus"></span>Action
                                    </button>
                                    <ul class="dropdown-menu  dropdown-menu-right">
                                        <li class="dropdown-item">
                                            <asp:HyperLink Target="_blank" NavigateUrl="~/F_21_GAcc/AccResourceCode.aspx?Type=Matcode" ID="Hypbtn" runat="server"> New Material</asp:HyperLink>
                                        </li>
                                        <li class="dropdown-item">
                                            <asp:LinkButton ID="LbtnAddSupplier" runat="server" OnClick="LbtnAddSupplier_Click" CssClass="">New Supplier</asp:LinkButton>
                                        </li>
                                    </ul>
                                </div>
                            </div>

                        </div>

                    </div>
                </div>
            </div>
            <div class="card card-fluid mb-5">
                <div class="card-body" style="min-height: 580px;">

                    <asp:MultiView ID="Multiview" runat="server">
                        <asp:View ID="View1" runat="server">

                            <asp:TextBox ID="txtflag" Style="display: none;" runat="server" Text="import"></asp:TextBox>
                            <div class="" id="DataPanel" runat="server" visible="false">
                                <div class="">
                                    <div class="row">
                                        <div class="col-md-2">
                                            <div class="form-group">
                                                <label class="text-teal">CLIENT NAME</label>
                                                <asp:Label ID="BuyerName" runat="server" CssClass="form-control form-control-sm bg-secondary"></asp:Label>                                                
                                            </div>
                                        </div>
                                        <div class="col-md-2">
                                            <div class="form-group">
                                                <label class="text-teal">ARTICLE NO.</label>
                                                <asp:Label ID="lblOrderno" runat="server" CssClass="form-control form-control-sm bg-secondary"></asp:Label>
                                            </div>
                                        </div>
                                        <div class="col-md-2">
                                            <div class="form-group">
                                                <label class="text-teal">ORDER QUANTITY</label>
                                                <asp:Label ID="lblttlorderqty" runat="server" CssClass="form-control form-control-sm bg-secondary"></asp:Label>
                                            </div>
                                        </div>
                                        <div class="col-md-2">
                                            <div class="form-group">
                                                <label class="text-teal">PRODUCTION START DATE</label>
                                                <asp:TextBox ID="Txtprddate" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
                                                <cc1:CalendarExtender ID="txtCurStartDate_CalendarExtender" runat="server"
                                                    Format="dd-MMM-yyyy" TargetControlID="Txtprddate"></cc1:CalendarExtender>
                                            </div>
                                        </div>
                                        <div class="col-md-2">
                                            <div class="form-group">
                                                <label class="text-teal">ORDER DATE</label>
                                                <asp:TextBox ID="txtOrdDate" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
                                                <cc1:CalendarExtender ID="CalendarExtender_txtOrdDate" runat="server" Format="dd-MMM-yyyy" TargetControlID="txtOrdDate"></cc1:CalendarExtender>
                                            </div>
                                        </div>
                                          <div class="col-md-2">
                                            <div class="form-group">
                                                <label class="text-teal">INSPECTION DATE</label>                                            
                                                <asp:TextBox ID="txtInspctDate" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
                                                <cc1:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd-MMM-yyyy" TargetControlID="txtInspctDate"></cc1:CalendarExtender>
                                            </div>
                                        </div>
                                       
                                    </div>

                                    <div class="row">
                                        <div class="col-md-2">
                                            <div class="form-group">
                                                <label class="text-teal">STYLE NAME</label>                                                    
                                                <asp:Label ID="lblstyle" runat="server" CssClass="form-control form-control-sm bg-secondary"></asp:Label>                       
                                            </div>
                                        </div>
                                        <div class="col-md-2">
                                            <div class="form-group">
                                                <label class="text-teal">ORDER COLOR</label>
                                                <asp:Label ID="lblcolorName" runat="server" CssClass="form-control form-control-sm bg-secondary"></asp:Label>                                                    
                                            </div>
                                        </div>
                                        <div class="col-md-2">
                                            <div class="form-group">
                                                <label class="text-teal">ORDER ARTICLE</label>                                                    
                                                <asp:Label ID="lblartcle" runat="server" CssClass="form-control form-control-sm bg-secondary"></asp:Label>
                                            </div>
                                        </div>

                                        <div class="col-md-2">
                                            <div class="form-group">
                                                <label class="text-teal">CATEGORY</label>                                            
                                                <asp:Label ID="lblcatedesc" runat="server" CssClass="form-control form-control-sm bg-secondary"></asp:Label>
                                            </div>
                                        </div>
                                        
                                       <div class="col-md-1">
                                            <div class="form-group">
                                                <label class="text-teal">Confirm Price</label>                                                    
                                                <asp:Label ID="lblConfirm" runat="server" CssClass="form-control form-control-sm bg-secondary"></asp:Label>                                                    
                                            </div>
                                        </div>
                                        <div class="col-md-1">
                                            <div class="form-group">
                                                <label id="lblscondate"  runat="server"   class="text-teal">S.Confirm Date</label>                                            
                                                <asp:TextBox ID="TxtConfirmDate" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
                                                <cc1:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd-MMM-yyyy" TargetControlID="TxtConfirmDate"></cc1:CalendarExtender>
                                            </div>
                                        </div>
                                        <div class="col-md-2">
                                            <div class="form-group">
                                                <label class="text-teal">REMARKS</label>
                                                <asp:TextBox ID="TxtNotes" TextMode="MultiLine" Rows="2" runat="server" CssClass="form-control"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="row">
                                        

                                        
                                    </div>                                    
                                </div>

                                <div class="my-3">
                                    <%--<div class="card card-fluid">--%>

                                    <%--<ul class="nav nav-tabs card-header-tabs">
                                                <li class="nav-item active"><a href="#tab2primary" class="nav-link active show" onclick="SetPlug('tab2primary');" data-toggle="tab"><span class="glyphicon glyphicon-file"></span>Import</a></li>
                                                <li class="nav-item"><a href="#tab3primary" data-toggle="tab" class="nav-link" onclick="SetPlug('tab3primary');"><span class="glyphicon glyphicon-import"></span>Local </a></li>
                                                <li class="nav-item"><a href="#AdditionImport" data-toggle="tab" class="nav-link" onclick="SetPlug('AdditionImport');"><span class="glyphicon glyphicon-th"></span>Additional Import </a></li>
                                                <li class="nav-item"><a href="#AdditionLocal" data-toggle="tab" class="nav-link" onclick="SetPlug('AdditionLocal');"><span class="glyphicon glyphicon-th"></span>Additional Local </a></li>
                                                <li class="nav-item"><a href="#CommonCost" data-toggle="tab" class="nav-link" onclick="SetPlug('CommonCost');"><span class="glyphicon glyphicon-th"></span>Common </a></li>

                                            </ul>--%>

                                    <div class="nav nav-tabs">
                                        <asp:RadioButtonList ID="RadioButtonList1" runat="server" AutoPostBack="true" CssClass="btn btn-info tab-pane rdbtn" OnSelectedIndexChanged="RadioButtonList1_SelectedIndexChanged" RepeatDirection="Horizontal">
                                            <asp:ListItem Value="0">Import</asp:ListItem>
                                            <asp:ListItem Value="1">Local</asp:ListItem>
                                            <asp:ListItem Value="2">Additional Import</asp:ListItem>
                                            <asp:ListItem Value="3">Additional Local</asp:ListItem>
                                            <asp:ListItem Value="4">Common</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </div>
                                    <%--</div>--%>
                                </div>

                                <div class="">
                                    <asp:MultiView ID="Multiview1" runat="server">
                                        <asp:View ID="View3" runat="server">
                                            <div class="table-responsive mb-5">
                                                <div class="row px-3" style="max-height: 460px">
                                                    <asp:GridView ID="gvMat" runat="server" AutoGenerateColumns="False"
                                                        ShowFooter="True" CssClass=" table-striped table-hover table-bordered grvContentarea"
                                                        OnRowDataBound="gvMat_RowDataBound">

                                                        <Columns>
                                                            <asp:CommandField ShowEditButton="True" Visible="False" />
                                                            <asp:TemplateField HeaderText="Sl.">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvSl1" runat="server" Style="text-transform: capitalize; text-align: left"
                                                                        Text='<%# Convert.ToDouble(Container.DataItemIndex+1).ToString("00")+"." %>'
                                                                        Width="20px"></asp:Label>
                                                                </ItemTemplate>

                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Itmno" Visible="False">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvItmNo1" runat="server" Style="font-size: 10px; text-transform: capitalize; text-align: left"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rsircode")) %>'
                                                                        Width="33px"></asp:Label>
                                                                    <asp:Label ID="lblgvspcfcode" runat="server" Style="font-size: 10px; text-transform: capitalize; text-align: left"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "spcfcode")) %>'
                                                                        Width="33px"></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="COMPONENT NAME">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="txtgvComponent" runat="server" Style="font-size: 11px; text-transform: capitalize; text-align: left"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "compdesc")) %>'
                                                                        Width="100px" BackColor="Transparent" BorderStyle="None"></asp:Label>
                                                                    <asp:Label ID="lblcomponent" Visible="false" runat="server" Style="font-size: 11px; text-transform: capitalize; text-align: left"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "compcode")) %>'
                                                                        Width="100px" BackColor="Transparent" BorderStyle="None"></asp:Label>
                                                                </ItemTemplate>

                                                                <HeaderStyle HorizontalAlign="Left" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="MATERIAL NAME">
                                                                <ItemTemplate>

                                                                    <asp:LinkButton ID="txtgvItmDesccost" OnClick="txtgvItmDesccost_Click" runat="server" Style="font-size: 11px; text-transform: capitalize; text-align: left"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rsirdesc")) %>'
                                                                        Width="280px" BackColor="Transparent" BorderStyle="None"></asp:LinkButton>
                                                                </ItemTemplate>

                                                                <HeaderStyle HorizontalAlign="Left" />
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="COLOR ">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvcolor" runat="server" Style="font-size: 11px; text-transform: capitalize"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "color")).Trim() %>'
                                                                        Width="40px" BackColor="Transparent" BorderStyle="None"></asp:Label>
                                                                </ItemTemplate>

                                                                <HeaderStyle HorizontalAlign="Center" />
                                                                <ItemStyle HorizontalAlign="Left" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Req Type " Visible="false">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvReqType" runat="server" Style="font-size: 11px; text-transform: capitalize"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "reqtype")).Trim() %>'
                                                                        Width="40px" BackColor="Transparent" BorderStyle="None"></asp:Label>
                                                                </ItemTemplate>

                                                                <HeaderStyle HorizontalAlign="Center" />
                                                                <ItemStyle HorizontalAlign="Left" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="PROBABLE SUPPLIER NAME">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="txtgvssirdesc" OnClick="txtgvssirdesc_Click" runat="server" Style="font-size: 11px; text-transform: capitalize; text-align: left"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "ssirdesc")) %>'
                                                                        Width="200px" BackColor="Transparent" BorderStyle="None"></asp:LinkButton>

                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                <ItemStyle Width="200px" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="SUPPLIER ARTICLE">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtsuparticle" runat="server" BackColor="Transparent" BorderColor="#99CCFF" BorderStyle="Solid"
                                                                        BorderWidth="1px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "suparticle")).Trim() %>'
                                                                        Width="60px"></asp:TextBox>
                                                                </ItemTemplate>

                                                                <HeaderStyle HorizontalAlign="Center" />
                                                                <ItemStyle HorizontalAlign="Left" />
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="COUNTRY OF <br> ORIGIN ">
                                                                <ItemTemplate>

                                                                    <%--                                                                     <asp:label ID="lblOrigin" runat="server" Style="font-size: 11px; text-align: left"
                                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "country")) %>'
                                                                                Width="100px" BackColor="Transparent" BorderStyle="None"></asp:label>
                                                                    --%>
                                                                    <asp:DropDownList ID="ddlOrigin" CssClass="inputTxt chzn-select" runat="server"></asp:DropDownList>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                                <ItemStyle Width="90px" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="TOTAL <br>ORDER">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtgvItmQty01" runat="server" BackColor="Transparent" BorderColor="#99CCFF" BorderStyle="Solid"
                                                                        BorderWidth="1px"
                                                                        Style="font-size: 11px; text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "rstdqty")).ToString("###0;(###0); ") %>'
                                                                        Width="60px"></asp:TextBox>
                                                                </ItemTemplate>
                                                                <FooterTemplate>
                                                                    <asp:Label ID="LblFqty1" runat="server" Style="text-align: right" Width="60px"></asp:Label>
                                                                </FooterTemplate>
                                                                <ItemStyle HorizontalAlign="Right" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="RATE" Visible="false">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtgvMatRate" runat="server" BackColor="Transparent" BorderColor="#99CCFF" BorderStyle="None"
                                                                        BorderWidth="1px" Enabled="false"
                                                                        Style="font-size: 11px; text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "rate")).ToString("###0.000000;(###0.000000); ") %>'
                                                                        Width="60px"></asp:TextBox>
                                                                </ItemTemplate>

                                                                <ItemStyle HorizontalAlign="Right" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="AMOUNT" Visible="false">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtgvAmount" runat="server" BackColor="Transparent" BorderColor="#99CCFF" BorderStyle="None"
                                                                        BorderWidth="1px" Enabled="false"
                                                                        Style="font-size: 11px; text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "stdamt")).ToString("###0.00;(###0.00); ") %>'
                                                                        Width="60px"></asp:TextBox>
                                                                </ItemTemplate>
                                                                <FooterTemplate>
                                                                    <asp:Label ID="LblFAmount" runat="server" Style="text-align: right" Width="60px"></asp:Label>
                                                                </FooterTemplate>
                                                                <ItemStyle HorizontalAlign="Right" />
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="UNIT ">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtgvItmUnit01" runat="server" Style="font-size: 11px; text-transform: capitalize"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "runit")).Trim() %>'
                                                                        Width="40px" BackColor="Transparent" BorderStyle="None"></asp:TextBox>
                                                                </ItemTemplate>

                                                                <HeaderStyle HorizontalAlign="Center" />
                                                                <ItemStyle HorizontalAlign="Left" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="SPECIFICATION">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="txtgvspcfdesc" runat="server" Style="font-size: 11px; text-transform: capitalize"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "spcfdesc")).Trim() %>'
                                                                        Width="200px" BackColor="Transparent" BorderStyle="None"></asp:Label>
                                                                </ItemTemplate>

                                                                <HeaderStyle HorizontalAlign="Center" />
                                                                <ItemStyle HorizontalAlign="Left" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Size " Visible="false">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvSize" runat="server" Style="font-size: 11px; text-transform: capitalize"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "size")).Trim() %>'
                                                                        Width="50px" BackColor="Transparent" BorderStyle="None"></asp:Label>
                                                                </ItemTemplate>

                                                                <HeaderStyle HorizontalAlign="Center" />
                                                                <ItemStyle HorizontalAlign="Left" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="COMPOSITION" Visible="false">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtgvComposition" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                                                        BorderWidth="1px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "cmposition")).Trim() %>'
                                                                        Width="60px" BackColor="Transparent"></asp:TextBox>
                                                                </ItemTemplate>

                                                                <HeaderStyle HorizontalAlign="Center" />
                                                                <ItemStyle HorizontalAlign="Left" />
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="NOMI.RECM <br> BY BUYER (N/R)">
                                                                <ItemTemplate>
                                                                    <asp:DropDownList ID="ddlnominated" CssClass="inputTxt chzn-select" runat="server"></asp:DropDownList>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                                <ItemStyle Width="80px" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="REMARKS ">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtgvRemrks" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                                                        BorderWidth="1px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "remarks")).Trim() %>'
                                                                        Width="50px" BackColor="Transparent"></asp:TextBox>
                                                                </ItemTemplate>

                                                                <HeaderStyle HorizontalAlign="Center" />
                                                                <ItemStyle HorizontalAlign="Left" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField Visible="false">
                                                                <ItemTemplate>
                                                                    <asp:HyperLink Target="_blank" ID="lbtnsizedet" class="btn btn-xs btn-info" runat="server" NavigateUrl='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sizedetails")) %>'><span class="fa fa-eye"></span></asp:HyperLink>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <%--<asp:TemplateField HeaderText="Rate(FC)">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtgvItmRat01" runat="server" BackColor="Transparent" BorderColor="#99CCFF" BorderStyle="Solid"
                                                        BorderWidth="1px"
                                                                    Style="font-size: 11px; text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "itmrat")).ToString("###0.000000;(###0.000000); ") %>'
                                                                    Width="70px"></asp:TextBox>
                                                            </ItemTemplate>
                                                            <FooterStyle HorizontalAlign="Right" />
                                                            <HeaderStyle HorizontalAlign="Center" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Amount">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblgvAmount4" runat="server" BackColor="Transparent" BorderStyle="None" Style="font-size: 11px; text-align: right"
                                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "itmamt")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                                    Width="80px"></asp:Label>
                                                            </ItemTemplate>
                                                                <FooterTemplate>
                                                                <asp:Label ID="LblFamt" runat="server" Style="text-align:right" Width="80px"></asp:Label>
                                                            </FooterTemplate>
                                                            <ItemStyle HorizontalAlign="Right" />
                                                        </asp:TemplateField>--%>
                                                            <asp:TemplateField HeaderText="PUR. TYPE">
                                                                <ItemTemplate>
                                                                    <asp:DropDownList ID="ddlpurtype" CssClass="inputTxt chzn-select" runat="server">
                                                                        <asp:ListItem Value="" Selected="True">Select</asp:ListItem>
                                                                        <asp:ListItem Value="L">Local</asp:ListItem>
                                                                        <asp:ListItem Value="F">Foreign</asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                                <ItemStyle Width="50px" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="(%)">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvprcnt" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "prcnt")).ToString("###0.00;(###0.00); ")+"%"%>'></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                                <ItemStyle Width="50px" />
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Size">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="lbtnupload" CssClass="text-primary" OnClick="lbtnupload_Click" runat="server">
                                                                    <span class="fa  fa-upload"></span>
                                                                    </asp:LinkButton>
                                                                    <asp:HyperLink Target="_blank" ID="LInkbtsizeup" class="text-blue" runat="server" NavigateUrl='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sizedetails")) %>'><span class="fa fa-eye"></span></asp:HyperLink>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
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
                                        <asp:View ID="View4" runat="server">
                                            <div class="table-responsive mb-5">
                                                <div class="row">
                                                    <asp:GridView ID="gvmatlocal" runat="server" AutoGenerateColumns="False"
                                                        ShowFooter="True" CssClass=" table-striped table-hover table-bordered grvContentarea"
                                                        OnRowDataBound="gvmatlocal_RowDataBound">

                                                        <Columns>
                                                            <asp:CommandField ShowEditButton="True" Visible="False" />
                                                            <asp:TemplateField HeaderText="Sl.">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvSl" runat="server" Style="text-transform: capitalize; text-align: left"
                                                                        Text='<%# Convert.ToDouble(Container.DataItemIndex+1).ToString("00")+"." %>'
                                                                        Width="30px"></asp:Label>
                                                                </ItemTemplate>

                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Itmno" Visible="False">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvrsircode" runat="server" Style="font-size: 10px; text-transform: capitalize; text-align: left"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rsircode")) %>'
                                                                        Width="33px"></asp:Label>
                                                                    <asp:Label ID="lblspcfcod" Visible="false" runat="server" Style="font-size: 10px; text-transform: capitalize; text-align: left"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "spcfcode")) %>'
                                                                        Width="33px"></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="DEPARTMENT">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="txtgvDept" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "procname")) %>'
                                                                        Width="120px" BackColor="Transparent" BorderStyle="None"></asp:Label>
                                                                    <asp:Label ID="lblprocode" Visible="false" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "procode")) %>'
                                                                        Width="120px" BackColor="Transparent" BorderStyle="None"></asp:Label>
                                                                </ItemTemplate>

                                                                <HeaderStyle HorizontalAlign="Left" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="MATERIAL NAME">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="txtgvItem" OnClick="txtgvItem_Click" runat="server" Style="font-size: 11px; text-transform: capitalize; text-align: left"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rsirdesc")) %>'
                                                                        Width="150px" BackColor="Transparent" BorderStyle="None"></asp:LinkButton>
                                                                </ItemTemplate>

                                                                <HeaderStyle HorizontalAlign="Left" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="ReqType" Visible="false">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvreqtypeLocal" runat="server" Style="font-size: 11px; text-transform: capitalize; text-align: left"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "reqtype")) %>'
                                                                        Width="150px" BackColor="Transparent" BorderStyle="None"></asp:Label>
                                                                </ItemTemplate>

                                                                <HeaderStyle HorizontalAlign="Left" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="SPECIFICATION">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="txtgvspcfdesc1" runat="server" Style="font-size: 11px; text-transform: capitalize"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "spcfdesc")).Trim() %>'
                                                                        Width="90px" BackColor="Transparent" BorderStyle="None"></asp:Label>
                                                                </ItemTemplate>

                                                                <HeaderStyle HorizontalAlign="Center" />
                                                                <ItemStyle HorizontalAlign="Left" />
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="PROBABLE <br> SUPPLIER NAME">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="lblgvLocssirdesc" OnClick="lblgvLocssirdesc_Click" runat="server" Style="font-size: 11px; text-transform: capitalize; text-align: left"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "ssirdesc")) %>'
                                                                        Width="130px" BackColor="Transparent" BorderStyle="None"></asp:LinkButton>

                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                                <ItemStyle Width="100px" />
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="COUNTRY OF <br> ORIGIN ">
                                                                <ItemTemplate>
                                                                    <asp:DropDownList ID="ddlOriginName" CssClass="inputTxt chzn-select" Width="90px" runat="server"></asp:DropDownList>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                                <ItemStyle Width="90px" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="TOTAL QUANTITY">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtgvrstdqty" runat="server" BackColor="Transparent" BorderColor="#99CCFF" BorderStyle="None"
                                                                        BorderWidth="1px"
                                                                        Style="font-size: 11px; text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "rstdqty")).ToString("###0;(###0); ") %>'
                                                                        Width="70px"></asp:TextBox>
                                                                </ItemTemplate>
                                                                <FooterTemplate>
                                                                    <asp:Label ID="LblFqty2" runat="server" Style="text-align: right" Width="70px"></asp:Label>
                                                                </FooterTemplate>
                                                                <ItemStyle HorizontalAlign="Right" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="RATE" Visible="false">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtgvRate" runat="server" BackColor="Transparent" BorderColor="#99CCFF" BorderStyle="None"
                                                                        BorderWidth="1px" Enabled="false"
                                                                        Style="font-size: 11px; text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "rate")).ToString("###0.000000;(###0.000000); ") %>'
                                                                        Width="70px"></asp:TextBox>
                                                                </ItemTemplate>

                                                                <ItemStyle HorizontalAlign="Right" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="AMOUNT" Visible="false">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtgvAmountLoc" runat="server" BackColor="Transparent" BorderColor="#99CCFF" BorderStyle="None"
                                                                        BorderWidth="1px" Enabled="false"
                                                                        Style="font-size: 11px; text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "stdamt")).ToString("###0.00;(###0.00); ") %>'
                                                                        Width="70px"></asp:TextBox>
                                                                </ItemTemplate>
                                                                <FooterTemplate>
                                                                    <asp:Label ID="LblFAmount1" runat="server" Style="text-align: right" Width="70px"></asp:Label>
                                                                </FooterTemplate>
                                                                <ItemStyle HorizontalAlign="Right" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="UOM">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtgvsirunit" runat="server" Style="font-size: 11px; text-transform: capitalize"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "runit")).Trim() %>'
                                                                        Width="60px" BackColor="Transparent" BorderStyle="None"></asp:TextBox>
                                                                </ItemTemplate>

                                                                <HeaderStyle HorizontalAlign="Center" />
                                                                <ItemStyle HorizontalAlign="Left" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="NOMINATED  <br> BY BUYER(Yes/No)">
                                                                <ItemTemplate>
                                                                    <asp:DropDownList ID="ddlnominated1" CssClass="form-control chzn-select" runat="server"></asp:DropDownList>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                                <ItemStyle Width="70px" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="REMARKS ">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtgvRemrk" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                                                        BorderWidth="1px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "remarks")).Trim() %>'
                                                                        Width="50px" BackColor="Transparent"></asp:TextBox>
                                                                </ItemTemplate>

                                                                <HeaderStyle HorizontalAlign="Center" />
                                                                <ItemStyle HorizontalAlign="Left" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField Visible="false">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblreqtype" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "reqtype")) %>'></asp:Label>
                                                                    <asp:HyperLink Target="_blank" ID="lbtnsizedetLoc" class="btn btn-xs btn-info" runat="server" NavigateUrl='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sizedetails")) %>'><span class="fa fa-eye"></span></asp:HyperLink>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <%--<asp:TemplateField HeaderText="Rate(FC)">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtgvItmRat01" runat="server" BackColor="Transparent" BorderColor="#99CCFF" BorderStyle="Solid"
                                                BorderWidth="1px"
                                                            Style="font-size: 11px; text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "itmrat")).ToString("###0.000000;(###0.000000); ") %>'
                                                            Width="70px"></asp:TextBox>
                                                    </ItemTemplate>
                                                    <FooterStyle HorizontalAlign="Right" />
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Amount">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvAmount4" runat="server" BackColor="Transparent" BorderStyle="None" Style="font-size: 11px; text-align: right"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "itmamt")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                            Width="80px"></asp:Label>
                                                    </ItemTemplate>
                                                        <FooterTemplate>
                                                        <asp:Label ID="LblFamt" runat="server" Style="text-align:right" Width="80px"></asp:Label>
                                                    </FooterTemplate>
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </asp:TemplateField>--%>
                                                            <asp:TemplateField HeaderText="PUR. TYPE">
                                                                <ItemTemplate>
                                                                    <asp:DropDownList ID="ddlpurtypelocal" CssClass="form-control chzn-select" runat="server">
                                                                        <asp:ListItem Value="" Selected="True">Select</asp:ListItem>
                                                                        <asp:ListItem Value="L">Local</asp:ListItem>
                                                                        <asp:ListItem Value="F">Foreign</asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                                <ItemStyle Width="50px" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Size">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="lblgvSizeupload" CssClass="text-danger" OnClick="lblgvSizeupload_Click1" runat="server">
                                                            <span class="fa fa-upload"></span>
                                                                    </asp:LinkButton>
                                                                    <asp:HyperLink Target="_blank" ID="LInkbtsize" class="text-info" runat="server" NavigateUrl='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sizedetails")) %>'><span class="fa fa-eye"></span></asp:HyperLink>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
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
                                        <asp:View ID="View5" runat="server">
                                            <div class="row">
                                                <div class="col-md-3">
                                                    <div class="form-group">
                                                        <asp:Label ID="Label10" runat="server" CssClass="label" Text="Component Name"></asp:Label>
                                                        <div class="d-flex">
                                                            <div>
                                                                <asp:DropDownList ID="ddlComponent" runat="server" CssClass="form-control form-control-sm chzn-select"></asp:DropDownList>
                                                            </div>
                                                            <div>
                                                                <asp:LinkButton ID="Lbtnaddcomp" CssClass="btn btn-sm btn-outline-success" OnClick="Lbtnaddcomp_Click" runat="server"><span class="fa fa-plus-circle"></span></asp:LinkButton>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="col-md-2">
                                                    <div class="form-group">
                                                        <asp:LinkButton ID="lblProcess0" runat="server" CssClass="label" OnClick="lblProcess0_Click" Text="MATERIALS NAME"></asp:LinkButton>
                                                        <asp:DropDownList ID="ddlResourcesCost" runat="server" OnSelectedIndexChanged="ddlResourcesCost_SelectedIndexChanged" AutoPostBack="true" CssClass="form-control form-control-sm chzn-select">
                                                        </asp:DropDownList>
                                                    </div>

                                                </div>
                                                <div class="col-md-2">
                                                    <div class="form-group">
                                                        <asp:Label ID="Label7" runat="server" CssClass="label">Specifications</asp:Label>


                                                        <div class="input-group input-group-sm input-group-alt">
                                                            <asp:DropDownList ID="DdlSpecres" runat="server" OnSelectedIndexChanged="DdlSpecres_SelectedIndexChanged" AutoPostBack="true" CssClass="form-control form-control-sm chzn-select">
                                                            </asp:DropDownList>
                                                           
                                                        </div>
                                                    </div>

                                                </div>
                                                <div class="col-md-1">
                                                    <div class="form-group">
                                                        <asp:Label ID="lblfgsize" runat="server" CssClass="" Text="FG Size"></asp:Label>
                                                        <asp:DropDownList ID="ddlfgsize" runat="server" CssClass="form-control form-control-sm chzn-select">
                                                        </asp:DropDownList>
                                                    </div>

                                                </div>
                                                <div class="col-md-3">
                                                    <div class="form-group">
                                                        <asp:Label ID="Label4" runat="server" CssClass="" Text="Supplier Name"></asp:Label>
                                                        <asp:DropDownList ID="ddlSupliAddiImp" runat="server" CssClass="form-control form-control-sm chzn-select">
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                                <div class="col-md-1">
                                                    <div class="form-group" style="margin-top: 20px">
                                                        <asp:LinkButton ID="lnkAddResouctCost" runat="server" Text="Add Table" OnClick="lnkAddResouctCost_Click" CssClass="btn btn-primary btn-sm " TabIndex="1">Add</asp:LinkButton>
                                                    </div>
                                                </div>

                                            </div>

                                            <div class="table-responsive mb-5">
                                                <div class="row" style="max-height: 460px">
                                                    <asp:GridView ID="gvaddimport" runat="server" AutoGenerateColumns="False"
                                                        ShowFooter="True" CssClass=" table-striped table-hover table-bordered grvContentarea"
                                                        OnRowDataBound="gvaddimport_RowDataBound" OnRowDeleting="gvaddimport_RowDeleting">

                                                        <Columns>

                                                            <asp:CommandField ShowEditButton="True" Visible="False" />
                                                            <asp:CommandField ShowDeleteButton="True" ControlStyle-CssClass="text-red" DeleteText="<span class='fa fa-trash'></span>" />
                                                            <asp:TemplateField HeaderText="SL.">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvimpSl" runat="server" Style="text-transform: capitalize; text-align: left"
                                                                        Text='<%# Convert.ToDouble(Container.DataItemIndex+1).ToString("00")+"." %>'
                                                                        Width="20px"></asp:Label>
                                                                </ItemTemplate>

                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Itmno" Visible="False">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="addlblgvItmNo1" runat="server" Style="font-size: 10px; text-transform: capitalize; text-align: left"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rsircode")) %>'
                                                                        Width="33px"></asp:Label>
                                                                    <asp:Label ID="lblgvspcfcodIMport" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "spcfcode")) %>'></asp:Label>
                                                                    <asp:Label ID="lblgvprocodeimport" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "procode")) %>'></asp:Label>

                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="COMPONENT NAME">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="AddtxtgvComponent" runat="server" Style="font-size: 11px; text-transform: capitalize; text-align: left"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "compdesc")) %>'
                                                                        Width="70px" BackColor="Transparent" BorderStyle="None"></asp:TextBox>
                                                                </ItemTemplate>

                                                                <HeaderStyle HorizontalAlign="Left" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="MATERIAL <br>NAME">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtgvItmDesccostadd" runat="server" Style="font-size: 11px; text-transform: capitalize; text-align: left"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rsirdesc")) %>'
                                                                        Width="80px" BackColor="Transparent" BorderStyle="None"></asp:TextBox>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Left" />
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="COLOR ">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="Addlblgvcolor" runat="server" Style="font-size: 11px; text-transform: capitalize"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "color")).Trim() %>'
                                                                        Width="40px" BackColor="Transparent" BorderStyle="None"></asp:Label>
                                                                </ItemTemplate>

                                                                <HeaderStyle HorizontalAlign="Center" />
                                                                <ItemStyle HorizontalAlign="Left" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="PROBABLE <br> SUPPLIER <br> NAME">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblSupliAddiImp" runat="server" Style="font-size: 11px; text-transform: capitalize"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "ssirdesc")).Trim() %>'
                                                                        Width="200px" BackColor="Transparent" BorderStyle="None"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                                <ItemStyle Width="200px" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="SUPPLIER ARTICLE">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="Addtxtsuparticle" runat="server" BackColor="Transparent" BorderColor="#99CCFF" BorderStyle="Solid"
                                                                        BorderWidth="1px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "suparticle")).Trim() %>'
                                                                        Width="60px"></asp:TextBox>
                                                                </ItemTemplate>

                                                                <HeaderStyle HorizontalAlign="Center" />
                                                                <ItemStyle HorizontalAlign="Left" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="COUNTRY <br> OF <br> ORIGIN ">
                                                                <ItemTemplate>
                                                                    <asp:DropDownList ID="ddlimportOrigin" Width="60px" CssClass=" chzn-select" runat="server"></asp:DropDownList>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                                <ItemStyle Width="80px" />
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="TOTAL <br> ORDER">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtgvtorderadd" runat="server" BackColor="Transparent" BorderColor="#99CCFF" BorderStyle="None"
                                                                        BorderWidth="1px"
                                                                        Style="font-size: 11px; text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "rstdqty")).ToString("###0;(###0); ") %>'
                                                                        Width="60px"></asp:TextBox>
                                                                </ItemTemplate>
                                                                <FooterTemplate>
                                                                    <asp:Label ID="LblFqty3" runat="server" Style="text-align: right" Width="60px"></asp:Label>
                                                                </FooterTemplate>
                                                                <ItemStyle HorizontalAlign="Right" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="RATE">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtgvimpRate" runat="server" BackColor="Transparent" BorderColor="#99CCFF" BorderStyle="None"
                                                                        BorderWidth="1px"
                                                                        Style="font-size: 11px; text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "rate")).ToString("###0.000000;(###0.000000); ") %>'
                                                                        Width="60px"></asp:TextBox>
                                                                </ItemTemplate>

                                                                <ItemStyle HorizontalAlign="Right" />
                                                            </asp:TemplateField>




                                                            <asp:TemplateField HeaderText="AMOUNT">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtgvAmountAddim" runat="server" BackColor="Transparent" BorderColor="#99CCFF" BorderStyle="None"
                                                                        BorderWidth="1px" Enabled="false"
                                                                        Style="font-size: 11px; text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "stdamt")).ToString("###0.00;(###0.00); ") %>'
                                                                        Width="90px"></asp:TextBox>
                                                                </ItemTemplate>
                                                                <FooterTemplate>
                                                                    <asp:Label ID="LblFAmount2" runat="server" Style="text-align: right" Width="60px"></asp:Label>
                                                                </FooterTemplate>
                                                                <ItemStyle HorizontalAlign="Right" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="UNIT ">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtgvimpItmUnit01" runat="server" Style="font-size: 11px; text-transform: capitalize"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "runit")).Trim() %>'
                                                                        Width="40px" BackColor="Transparent" BorderStyle="None"></asp:TextBox>
                                                                </ItemTemplate>

                                                                <HeaderStyle HorizontalAlign="Center" />
                                                                <ItemStyle HorizontalAlign="Left" />
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Size " Visible="false">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvimpSize" runat="server" Style="font-size: 11px; text-transform: capitalize"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "size")).Trim() %>'
                                                                        Width="50px" BackColor="Transparent" BorderStyle="None"></asp:Label>
                                                                </ItemTemplate>

                                                                <HeaderStyle HorizontalAlign="Center" />
                                                                <ItemStyle HorizontalAlign="Left" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="SPECIFICATION">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="txtgvspcfdescadd" runat="server" Style="font-size: 11px; text-transform: capitalize"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "spcfdesc")).Trim() %>'
                                                                        Width="200px" BackColor="Transparent" BorderStyle="None"></asp:Label>
                                                                </ItemTemplate>

                                                                <HeaderStyle HorizontalAlign="Center" />
                                                                <ItemStyle HorizontalAlign="Left" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="COMPS.">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtgvCompositionadd" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                                                        BorderWidth="1px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "cmposition")).Trim() %>'
                                                                        Width="50px" BackColor="Transparent"></asp:TextBox>
                                                                </ItemTemplate>

                                                                <HeaderStyle HorizontalAlign="Center" />
                                                                <ItemStyle HorizontalAlign="Left" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="CONS.">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtgvaddiCons" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                                                        BorderWidth="1px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "addicons")).Trim() %>'
                                                                        Width="50px" BackColor="Transparent"></asp:TextBox>
                                                                </ItemTemplate>

                                                                <HeaderStyle HorizontalAlign="Center" />
                                                                <ItemStyle HorizontalAlign="Left" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="WESTAGE %.">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtgvaddiwestpc" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                                                        BorderWidth="1px" Style="font-size: 11px; text-align: right;" 
                                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "addiwestpc")).ToString("###0;(###0); ") %>'
                                                                        Width="50px" BackColor="Transparent"></asp:TextBox>
                                                                </ItemTemplate>

                                                                <HeaderStyle HorizontalAlign="Center" />
                                                                <ItemStyle HorizontalAlign="Right" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="NOMI.REC.<br>BY BUYER <br> (N/R)">
                                                                <ItemTemplate>
                                                                    <asp:DropDownList ID="ddlimpnominated" CssClass="form-control chzn-select" runat="server"></asp:DropDownList>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                                <ItemStyle Width="70px" />
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="REMARKS ">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtgvRemrksAddIm" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                                                        BorderWidth="1px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "remarks")).Trim() %>'
                                                                        Width="50px" BackColor="Transparent"></asp:TextBox>
                                                                </ItemTemplate>

                                                                <HeaderStyle HorizontalAlign="Center" />
                                                                <ItemStyle HorizontalAlign="Left" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="PUR. TYPE">
                                                                <ItemTemplate>
                                                                    <asp:DropDownList ID="ddlpurtypeaddimport" CssClass="form-control chzn-select" runat="server">
                                                                        <asp:ListItem Value="" Selected="True">Select</asp:ListItem>
                                                                        <asp:ListItem Value="L">Local</asp:ListItem>
                                                                        <asp:ListItem Value="F">Foreign</asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                                <ItemStyle Width="50px" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Size">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="lblgvSizeuploadimport" CssClass="text-primary" OnClick="lblgvSizeuploadimport_Click" runat="server">
                                        <span class="fa fa-upload"></span>
                                                                    </asp:LinkButton>
                                                                    <asp:HyperLink Target="_blank" ID="LInkbtnimport" class="text-danger" runat="server" NavigateUrl='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sizedetails")) %>'><span class="fa fa-eye"></span></asp:HyperLink>


                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
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
                                        <asp:View ID="View6" runat="server">
                                            <div class="row">
                                                <div class="col-md-3">
                                                    <div class="form-group">
                                                        <asp:Label ID="Label2" runat="server" CssClass="label" Text="DEPARTMENT NAME"></asp:Label>
                                                        <div class="d-flex">
                                                            <div>
                                                                <asp:DropDownList ID="ddldept" runat="server" CssClass="form-control form-control-sm chzn-select"></asp:DropDownList>
                                                            </div>
                                                            <div>
                                                                <asp:LinkButton ID="LbtnAddDept" CssClass="input-group-text text-success" OnClick="LbtnAddDept_Click" runat="server"><span class="fa fa-plus-circle"></span></asp:LinkButton>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="col-md-3">
                                                    <div class="form-group">
                                                        <asp:Label ID="Label3" runat="server" CssClass="label" Text="Materials Name"></asp:Label>
                                                        <asp:DropDownList ID="ddlResources" runat="server" CssClass="form-control form-control-sm chzn-select">
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                                <div class="col-md-3">
                                                    <div class="form-group">
                                                        <asp:Label ID="Label6" runat="server" CssClass="label" Text="Supplier Name"></asp:Label>
                                                        <asp:DropDownList ID="ddlSupliAddiLoc" runat="server" CssClass="form-control form-control-sm chzn-select">
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                                <div class="col-md-1 ">
                                                    <div class="form-group" style="margin-top: 20px;">
                                                        <asp:LinkButton ID="LbtnAddRestoLocal" runat="server" Text="Add Table" OnClick="LbtnAddRestoLocal_Click" CssClass="btn btn-primary btn-sm " TabIndex="1">Add</asp:LinkButton>
                                                    </div>

                                                </div>
                                            </div>

                                            <div class="table-responsive mb-5">
                                                <div class="row" style="max-height: 460px">
                                                    <asp:GridView ID="gvaddlocal" runat="server" AutoGenerateColumns="False"
                                                        ShowFooter="True" CssClass=" table-striped table-hover table-bordered grvContentarea"
                                                        OnRowDataBound="gvaddlocal_RowDataBound" OnRowDeleting="gvaddlocal_RowDeleting">

                                                        <Columns>
                                                            <asp:CommandField ShowDeleteButton="True" ControlStyle-CssClass="btn btn-xs btn-danger ml-2" DeleteText="<i class='fa fa-trash'></i>" />
                                                            <asp:CommandField ShowEditButton="True" Visible="False" />
                                                            <asp:TemplateField HeaderText="Sl.">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvSlloc" runat="server" Style="text-transform: capitalize; text-align: left"
                                                                        Text='<%# Convert.ToDouble(Container.DataItemIndex+1).ToString("00")+"." %>'
                                                                        Width="30px"></asp:Label>
                                                                </ItemTemplate>

                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Itmno" Visible="False">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvrsircodeloc" runat="server" Style="font-size: 10px; text-transform: capitalize; text-align: left"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rsircode")) %>'
                                                                        Width="33px"></asp:Label>
                                                                    <asp:Label ID="lblgvspcfcodlocal" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "spcfcode")) %>'></asp:Label>
                                                                    <asp:Label ID="lblgvprocode" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "procode")) %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="DEPARTMENT">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="txtgvDeptloc" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "procname")) %>'
                                                                        Width="120px" BackColor="Transparent" BorderStyle="None"></asp:Label>
                                                                </ItemTemplate>

                                                                <HeaderStyle HorizontalAlign="Left" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="MATERIAL NAME">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="txtgvItemloc" runat="server" Style="font-size: 11px; text-transform: capitalize; text-align: left"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rsirdesc")) %>'
                                                                        Width="150px" BackColor="Transparent" BorderStyle="None"></asp:Label>
                                                                </ItemTemplate>

                                                                <HeaderStyle HorizontalAlign="Left" />
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="SPECIFICATION">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="txtgvspcfdesc1loc" runat="server" Style="font-size: 11px; text-transform: capitalize"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "spcfdesc")).Trim() %>'
                                                                        Width="200px" BackColor="Transparent" BorderStyle="None"></asp:Label>
                                                                </ItemTemplate>

                                                                <HeaderStyle HorizontalAlign="Center" />
                                                                <ItemStyle HorizontalAlign="Left" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="PROBABLE <br>SUPPLIER NAME">
                                                                <ItemTemplate>
                                                                    <asp:DropDownList ID="ddlsupplierListloc" CssClass="inputTxt chzn-select" Width="200px" runat="server"></asp:DropDownList>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                                <ItemStyle Width="200px" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="COUNTRY OF<br> ORIGIN ">
                                                                <ItemTemplate>
                                                                    <asp:DropDownList ID="ddlOriginNameADD" CssClass="inputTxt chzn-select" Width="90px" runat="server"></asp:DropDownList>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                                <ItemStyle Width="90px" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="TOTAL QUANTITY">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtgvrstdqtyloc" runat="server" BackColor="Transparent" BorderColor="#99CCFF" BorderStyle="Solid"
                                                                        BorderWidth="1px"
                                                                        Style="font-size: 11px; text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "rstdqty")).ToString("###0;(###0); ") %>'
                                                                        Width="70px"></asp:TextBox>
                                                                </ItemTemplate>
                                                                <FooterTemplate>
                                                                    <asp:Label ID="LblFqty4" runat="server" Style="text-align: right" Width="70px"></asp:Label>
                                                                </FooterTemplate>
                                                                <ItemStyle HorizontalAlign="Right" />
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="RATE">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtgvRateloc" runat="server" BackColor="Transparent" BorderColor="#99CCFF" BorderStyle="Solid"
                                                                        BorderWidth="1px"
                                                                        Style="font-size: 11px; text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "rate")).ToString("###0.000000;(###0.000000); ") %>'
                                                                        Width="60px"></asp:TextBox>
                                                                </ItemTemplate>

                                                                <ItemStyle HorizontalAlign="Right" />
                                                            </asp:TemplateField>
                                                              <asp:TemplateField HeaderText="CONS.">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtgvlocaddiCons" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                                                        BorderWidth="1px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "addicons")).Trim() %>'
                                                                        Width="50px" BackColor="Transparent"></asp:TextBox>
                                                                </ItemTemplate>

                                                                <HeaderStyle HorizontalAlign="Center" />
                                                                <ItemStyle HorizontalAlign="Right" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="WESTAGE %.">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtgvlocaddiwestpc" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                                                        BorderWidth="1px" Style="font-size: 11px; text-align: right;" 
                                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "addiwestpc")).ToString("###0;(###0); ") %>'
                                                                        Width="50px" BackColor="Transparent"></asp:TextBox>
                                                                </ItemTemplate>

                                                                <HeaderStyle HorizontalAlign="Center" />
                                                                <ItemStyle HorizontalAlign="Right" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Amount" Visible="false">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtgvAmountAddloc" runat="server" BackColor="Transparent" BorderColor="#99CCFF" BorderStyle="Solid"
                                                                        BorderWidth="1px" Enabled="false"
                                                                        Style="font-size: 11px; text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "stdamt")).ToString("###0.00;(###0.00); ") %>'
                                                                        Width="70px"></asp:TextBox>
                                                                </ItemTemplate>
                                                                <FooterTemplate>
                                                                    <asp:Label ID="LblFAmount3" runat="server" Style="text-align: right" Width="80px"></asp:Label>
                                                                </FooterTemplate>
                                                                <ItemStyle HorizontalAlign="Right" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="UOM">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtgvsirunitloc" runat="server" Style="font-size: 11px; text-transform: capitalize"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "runit")).Trim() %>'
                                                                        Width="50px" BackColor="Transparent" BorderStyle="None"></asp:TextBox>
                                                                </ItemTemplate>

                                                                <HeaderStyle HorizontalAlign="Center" />
                                                                <ItemStyle HorizontalAlign="Left" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Nominated  <br> By Buyer (Yes/No)">
                                                                <ItemTemplate>
                                                                    <asp:DropDownList ID="ddlnominated1loc" CssClass="form-control chzn-select" runat="server"></asp:DropDownList>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                                <ItemStyle Width="70px" />
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="REMARKS ">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtgvRemrkloc" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                                                        BorderWidth="1px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "remarks")).Trim() %>'
                                                                        Width="50px" BackColor="Transparent"></asp:TextBox>
                                                                </ItemTemplate>

                                                                <HeaderStyle HorizontalAlign="Center" />
                                                                <ItemStyle HorizontalAlign="Left" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="PUR. TYPE">
                                                                <ItemTemplate>
                                                                    <asp:DropDownList ID="ddlpurtypeaddlocal" CssClass="form-control chzn-select" runat="server">
                                                                        <asp:ListItem Value="" Selected="True">Select</asp:ListItem>
                                                                        <asp:ListItem Value="L">Local</asp:ListItem>
                                                                        <asp:ListItem Value="F">Foreign</asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                                <ItemStyle Width="50px" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Size">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="lblgvSizeupload" CssClass="btn btn-xs btn-success" OnClick="lblgvSizeupload_Click" runat="server">
                                                                                <span class="fa fa-upload"></span>
                                                                    </asp:LinkButton>
                                                                    <asp:HyperLink Target="_blank" ID="LInkbtn" class="btn btn-xs btn-info" runat="server" NavigateUrl='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sizedetails")) %>'><span class="fa fa-eye"></span></asp:HyperLink>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>
                                                            <%--<asp:TemplateField HeaderText="Rate(FC)">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtgvItmRat01" runat="server" BackColor="Transparent" BorderColor="#99CCFF" BorderStyle="Solid"
                                                BorderWidth="1px"
                                                            Style="font-size: 11px; text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "itmrat")).ToString("###0.000000;(###0.000000); ") %>'
                                                            Width="70px"></asp:TextBox>
                                                    </ItemTemplate>
                                                    <FooterStyle HorizontalAlign="Right" />
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Amount">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvAmount4" runat="server" BackColor="Transparent" BorderStyle="None" Style="font-size: 11px; text-align: right"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "itmamt")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                            Width="80px"></asp:Label>
                                                    </ItemTemplate>
                                                        <FooterTemplate>
                                                        <asp:Label ID="LblFamt" runat="server" Style="text-align:right" Width="80px"></asp:Label>
                                                    </FooterTemplate>
                                                    <ItemStyle HorizontalAlign="Right" />
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
                                        </asp:View>
                                        <asp:View ID="View7" runat="server">
                                            <div class="table-responsive overflow-x-hidden mb-5">
                                                <div class="row mb-5" style="max-height: 460px">
                                                    <asp:GridView ID="gvCommonCost" runat="server" AutoGenerateColumns="False"
                                                        ShowFooter="True" CssClass=" table-striped table-hover table-bordered grvContentarea">

                                                        <Columns>
                                                            <asp:CommandField ShowEditButton="True" Visible="False" />
                                                            <asp:TemplateField HeaderText="Sl.">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvSlcommloc" runat="server" Style="text-transform: capitalize; text-align: left"
                                                                        Text='<%# Convert.ToDouble(Container.DataItemIndex+1).ToString("00")+"." %>'
                                                                        Width="30px"></asp:Label>
                                                                </ItemTemplate>

                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="MATERIAL NAME">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="txtgvcommItemloc" runat="server" Style="font-size: 11px; text-transform: capitalize; text-align: left"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rsirdesc")) %>'
                                                                        Width="250px" BackColor="Transparent" BorderStyle="None"></asp:Label>
                                                                </ItemTemplate>

                                                                <HeaderStyle HorizontalAlign="Left" />
                                                            </asp:TemplateField>


                                                            <%--                                                    <asp:TemplateField HeaderText="PROBABLE <br>SUPPLIER NAME">
                                                        <ItemTemplate>
                                                            <asp:DropDownList ID="ddlsupplierListloc" CssClass="inputTxt chzn-select" Width="90px" runat="server"></asp:DropDownList>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        <ItemStyle Width="90px" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="COUNTRY OF<br> ORIGIN ">
                                                        <ItemTemplate>
                                                            <asp:DropDownList ID="ddlOriginName" CssClass="inputTxt chzn-select" Width="90px" runat="server"></asp:DropDownList>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        <ItemStyle Width="90px" />
                                                    </asp:TemplateField>--%>
                                                            <%--     <asp:TemplateField HeaderText="TOTAL QUANTITY">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtgvrstdqtyloc" runat="server" BackColor="Transparent" BorderColor="#99CCFF" BorderStyle="Solid"
                                                                BorderWidth="1px"
                                                                Style="font-size: 11px; text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "rstdqty")).ToString("###0.00;(###0.00); ") %>'
                                                                Width="70px"></asp:TextBox>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label ID="LblFqty" runat="server" Style="text-align: right" Width="70px"></asp:Label>
                                                        </FooterTemplate>
                                                        <ItemStyle HorizontalAlign="Right" />
                                                    </asp:TemplateField>--%>
                                                            <%--    <asp:TemplateField HeaderText="RATE" >
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtgvRateloc" runat="server" BackColor="Transparent" BorderColor="#99CCFF" BorderStyle="Solid"
                                                                BorderWidth="1px"
                                                                Style="font-size: 11px; text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "rate")).ToString("###0.0000;(###0.0000); ") %>'
                                                                Width="60px"></asp:TextBox>
                                                        </ItemTemplate>

                                                        <ItemStyle HorizontalAlign="Right" />
                                                    </asp:TemplateField>--%>
                                                            <asp:TemplateField HeaderText="Amount">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtgvAmountloc" runat="server" BackColor="Transparent" BorderColor="#99CCFF" BorderStyle="None"
                                                                        BorderWidth="1px" Enabled="false"
                                                                        Style="font-size: 11px; text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "stdamt")).ToString("###0.00;(###0.00); ") %>'
                                                                        Width="70px"></asp:TextBox>
                                                                </ItemTemplate>
                                                                <FooterTemplate>
                                                                    <asp:Label ID="LblFAmount4" runat="server" Style="text-align: right" Width="80px"></asp:Label>
                                                                </FooterTemplate>
                                                                <ItemStyle HorizontalAlign="Right" />
                                                            </asp:TemplateField>
                                                            <%--    <asp:TemplateField HeaderText="UOM">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtgvsirunitloc" runat="server" Style="font-size: 11px; text-transform: capitalize"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "runit")).Trim() %>'
                                                                Width="50px" BackColor="Transparent" BorderStyle="None"></asp:TextBox>
                                                        </ItemTemplate>

                                                        <HeaderStyle HorizontalAlign="Center" />
                                                        <ItemStyle HorizontalAlign="Left" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Nominated  <br> By Buyer (Yes/No)">
                                                        <ItemTemplate>
                                                            <asp:DropDownList ID="ddlnominated1loc" CssClass="form-control chzn-select" runat="server"></asp:DropDownList>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        <ItemStyle Width="70px" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="REMARKS ">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtgvRemrkloc" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                                                BorderWidth="1px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "remarks")).Trim() %>'
                                                                Width="50px" BackColor="Transparent"></asp:TextBox>
                                                        </ItemTemplate>

                                                        <HeaderStyle HorizontalAlign="Center" />
                                                        <ItemStyle HorizontalAlign="Left" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="PUR. TYPE">
                                                        <ItemTemplate>
                                                            <asp:DropDownList ID="ddlpurtypeaddlocal" CssClass="form-control chzn-select" runat="server">
                                                                <asp:ListItem Value="" Selected="True">Select</asp:ListItem>
                                                                <asp:ListItem Value="L">Local</asp:ListItem>
                                                                <asp:ListItem Value="F">Foreign</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                        <ItemStyle Width="50px" />
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
                                        </asp:View>
                                    </asp:MultiView>
                                    <%-- <div class="tab-pane fade active show" id="tab2primary">
                                                    
                                                </div>
                                                <div class="tab-pane fade" id="tab3primary">
                                                   
                                                </div>

                                                <div class="tab-pane fade" id="AdditionImport">

                                                    
                                                </div>
                                                <div class="tab-pane fade" id="AdditionLocal">

                                                    
                                                </div>
                                                <div class="tab-pane fade" id="CommonCost">


                                                  
                                                </div>--%>
                                </div>
                            </div>
                        </asp:View>
                        <asp:View ID="View2" runat="server">
                            <div class="col-md-6">
                                <label class="label label-success " id="lblBomCost" runat="server" visible="false" style="font-size: 12px;">BOM Cost  Details</label>
                                <asp:GridView ID="gvtotalbom" runat="server" AutoGenerateColumns="False"
                                    ShowFooter="True" CssClass=" table-striped table-hover table-bordered grvContentarea"
                                    Width="40px" OnRowDataBound="gvtotalbom_RowDataBound">

                                    <Columns>
                                        <asp:CommandField ShowEditButton="True" Visible="False" />
                                        <asp:TemplateField HeaderText="Sl.">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvSlno" runat="server" Style="text-transform: capitalize; text-align: left"
                                                    Text='<%# Convert.ToDouble(Container.DataItemIndex+1).ToString("00")+"." %>'
                                                    Width="20px"></asp:Label>
                                            </ItemTemplate>

                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Itmno" Visible="False">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvItmNo12" runat="server" Style="font-size: 10px; text-transform: capitalize; text-align: left"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rsircode")) %>'
                                                    Width="33px"></asp:Label>
                                                <asp:Label ID="lblgvspcfcode1" runat="server" Style="font-size: 10px; text-transform: capitalize; text-align: left"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "spcfcod")) %>'
                                                    Width="33px"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="MATERIAL NAME">
                                            <ItemTemplate>

                                                <asp:Label ID="txtgvMatname" runat="server" Style="font-size: 11px; text-transform: capitalize; text-align: left"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rsirdesc")) %>'
                                                    Width="340px" BackColor="Transparent" BorderStyle="None"></asp:Label>
                                            </ItemTemplate>

                                            <HeaderStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="TOTAL <br>ORDER">
                                            <ItemTemplate>
                                                <asp:Label ID="txtgvtotalOrder" runat="server" BackColor="Transparent"
                                                    Style="font-size: 11px; text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "itmqty")).ToString("###0;(###0); ") %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="LblFqty5" runat="server" Style="text-align: right" Width="80px"></asp:Label>
                                            </FooterTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="AMOUNT">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtgvAmountTotalBom" runat="server" BackColor="Transparent" BorderColor="#99CCFF" BorderStyle="None"
                                                    BorderWidth="1px" Enabled="false"
                                                    Style="font-size: 11px; text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "itmamt")).ToString("###0.0000;(###0.0000); ") %>'
                                                    Width="80px"></asp:TextBox>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="LblFAmount5" runat="server" Style="text-align: right" Width="80px"></asp:Label>
                                            </FooterTemplate>
                                            <ItemStyle HorizontalAlign="Right" />

                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="(%)">
                                            <ItemTemplate>
                                                <asp:Label ID="txtgvbompernt1" runat="server"
                                                    Enabled="false"
                                                    Style="font-size: 11px; text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "percnt")).ToString("###0.0000;(###0.0000); ") %>'
                                                    Width="50px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="LblFPercnt1" runat="server" Style="text-align: right" Width="50px"></asp:Label>
                                            </FooterTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>



                                    </Columns>
                                    <FooterStyle CssClass="grvFooter" />
                                    <EditRowStyle />
                                    <AlternatingRowStyle />
                                    <PagerStyle CssClass="gvPagination" />
                                    <HeaderStyle CssClass="grvHeader" />
                                </asp:GridView>
                            </div>
                            <div class="col-md-6">
                                <label class="label label-success " id="lblcolorwise" runat="server" visible="false" style="font-size: 12px;">Color wise Details</label>
                                <asp:GridView ID="gvtopshet" runat="server" AutoGenerateColumns="False"
                                    ShowFooter="True" CssClass=" table-striped table-hover table-bordered grvContentarea"
                                    Width="40px">

                                    <Columns>
                                        <asp:CommandField ShowEditButton="True" Visible="False" />
                                        <asp:TemplateField HeaderText="SL.">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvSerail" runat="server" Style="text-transform: capitalize; text-align: left"
                                                    Text='<%# Convert.ToDouble(Container.DataItemIndex+1).ToString("00")+"." %>'
                                                    Width="20px"></asp:Label>
                                            </ItemTemplate>

                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="STYLE NAME">
                                            <ItemTemplate>

                                                <asp:Label ID="txtgvItmDesc" runat="server" Style="font-size: 11px; text-transform: capitalize; text-align: left"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "styledesc")) %>'
                                                    Width="100px" BackColor="Transparent" BorderStyle="None"></asp:Label>
                                            </ItemTemplate>

                                            <HeaderStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="COLOR ">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvcolortopsheet" runat="server" Style="font-size: 11px; text-transform: capitalize"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gendata")).Trim() %>'
                                                    Width="40px" BackColor="Transparent" BorderStyle="None"></asp:Label>
                                            </ItemTemplate>

                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="CONFIRM PRICE">
                                            <ItemTemplate>
                                                <asp:Label ID="txtgvConfirmPrice" runat="server" BackColor="Transparent"
                                                    Style="font-size: 11px; text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "confrmprice")).ToString("###0.0000;(###0.0000); ") %>'
                                                    Width="50px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="ORDER <br> QTY">
                                            <ItemTemplate>
                                                <asp:Label ID="txtgvOrderqty" runat="server" BackColor="Transparent"
                                                    Enabled="false"
                                                    Style="font-size: 11px; text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ordrqty")).ToString("###0;(###0); ") %>'
                                                    Width="40px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="LblFqty" runat="server" Style="text-align: right" Width="40px"></asp:Label>
                                            </FooterTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="CONS <br>PRICE">
                                            <ItemTemplate>
                                                <asp:Label ID="txtgvConPrice" runat="server" BackColor="Transparent"
                                                    Enabled="false"
                                                    Style="font-size: 11px; text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "consprice")).ToString("###0.0000;(###0.0000); ") %>'
                                                    Width="40px"></asp:Label>
                                            </ItemTemplate>

                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="BOM <br>PRICE">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtgvbomprice" runat="server" BackColor="Transparent" BorderColor="#99CCFF" BorderStyle="None"
                                                    BorderWidth="1px" Enabled="false"
                                                    Style="font-size: 11px; text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "bomprice")).ToString("###0.0000;(###0.0000); ") %>'
                                                    Width="40px"></asp:TextBox>
                                            </ItemTemplate>

                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="COST <br> AMOUNT">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtgvbomamount1" runat="server" BackColor="Transparent" BorderColor="#99CCFF" BorderStyle="None"
                                                    BorderWidth="1px" Enabled="false"
                                                    Style="font-size: 11px; text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "bomcost")).ToString("###0.0000;(###0.0000); ") %>'
                                                    Width="70px"></asp:TextBox>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="LblFAmount6" runat="server" Style="text-align: right" Width="70px"></asp:Label>
                                            </FooterTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="REVENUE">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtgvrevenue" runat="server" BackColor="Transparent" BorderColor="#99CCFF" BorderStyle="None"
                                                    BorderWidth="1px" Enabled="false"
                                                    Style="font-size: 11px; text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "revenue")).ToString("###0.0000;(###0.0000); ") %>'
                                                    Width="60px"></asp:TextBox>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="LblFRevenue" runat="server" Style="text-align: right" Width="60px"></asp:Label>
                                            </FooterTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="PROFIT<br> LOSS">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtgvProflosss" runat="server" BackColor="Transparent" BorderColor="#99CCFF" BorderStyle="None"
                                                    BorderWidth="1px" Enabled="false"
                                                    Style="font-size: 11px; text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "profloss")).ToString("###0.00;(###0.00); ") %>'
                                                    Width="60px"></asp:TextBox>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="LblFProfLoss" runat="server" Style="text-align: right" Width="60px"></asp:Label>
                                            </FooterTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>


                                    </Columns>
                                    <FooterStyle CssClass="grvFooter" />
                                    <EditRowStyle />
                                    <AlternatingRowStyle />
                                    <PagerStyle CssClass="gvPagination" />
                                    <HeaderStyle CssClass="grvHeader" />
                                </asp:GridView>
                                <label class="label label-success" id="lblDircost" runat="server" visible="false" style="font-size: 12px;">Direct Cost Details</label>
                                <asp:GridView ID="gvDirectCost" runat="server" AutoGenerateColumns="False"
                                    ShowFooter="True" CssClass=" table-striped table-hover table-bordered grvContentarea"
                                    Width="40px">

                                    <Columns>
                                        <asp:CommandField ShowEditButton="True" Visible="False" />
                                        <asp:TemplateField HeaderText="SL.">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvslno1" runat="server" Style="text-transform: capitalize; text-align: left"
                                                    Text='<%# Convert.ToDouble(Container.DataItemIndex+1).ToString("00")+"." %>'
                                                    Width="20px"></asp:Label>
                                            </ItemTemplate>

                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="DIRECT MATERIAL ">
                                            <ItemTemplate>
                                                <asp:Label ID="lblrsirdesc" runat="server" Style="font-size: 11px; text-transform: capitalize"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rsirdesc")).Trim() %>'
                                                    Width="300px" BackColor="Transparent" BorderStyle="None"></asp:Label>
                                            </ItemTemplate>

                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="AMOUNT">
                                            <ItemTemplate>
                                                <asp:Label ID="txtgvbomamount" runat="server" BackColor="Transparent" BorderColor="#99CCFF" BorderStyle="None"
                                                    BorderWidth="1px" Enabled="false"
                                                    Style="font-size: 11px; text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "stdamt")).ToString("###0.0000;(###0.0000); ") %>'
                                                    Width="100px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="LblFAmount7" runat="server" Style="text-align: right" Width="100px"></asp:Label>
                                            </FooterTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="(%)">
                                            <ItemTemplate>
                                                <asp:Label ID="txtgvbompernt" runat="server"
                                                    Enabled="false"
                                                    Style="font-size: 11px; text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "percnt")).ToString("###0.0000;(###0.0000); ") %>'
                                                    Width="50px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="LblFPercnt" runat="server" Style="text-align: right" Width="50px"></asp:Label>
                                            </FooterTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
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

                    <asp:HiddenField ID="TabName" runat="server" />
                </div>
            </div>

            <!-----------------------------modal----------------------->
            <div id="myModal" class="modal animated slideInLeft" role="dialog">
                <div class="modal-dialog modal-sm">
                    <div class="modal-content  ">
                        <div class="modal-header">

                            <button type="button" style="background-color: red;" class="close btn btn-xs" data-dismiss="modal"><span class="fa fa-close"></span></button>
                            <h4 class="modal-title">
                                <span class="fa fa-table"></span>Size Allocation Input </h4>
                        </div>
                        <div class="modal-body">
                            <div class="row-fluid">
                                <asp:Label ID="lblMaterial" runat="server" Visible="false"></asp:Label>
                                <asp:Label ID="lblSpcfcod" runat="server" Visible="false"></asp:Label>
                                <asp:Label ID="lblprocesscod" runat="server" Visible="false"></asp:Label>
                                <asp:Label ID="lblsupplier" runat="server" Visible="false"></asp:Label>
                                <asp:Label ID="lblreqtype" runat="server" Visible="false"></asp:Label>
                                <asp:Panel ID="UploadPanel" runat="server">
                                    <div class="row">
                                        <div class="form-group">

                                            <cc1:AsyncFileUpload OnClientUploadError="uploadError"
                                                OnClientUploadComplete="uploadComplete" runat="server"
                                                ID="AsyncFileUpload1" UploaderStyle="Modern"
                                                CompleteBackColor="White"
                                                UploadingBackColor="#CCFFFF" ThrobberID="imgLoader"
                                                OnUploadedComplete="FileUploadComplete" />

                                        </div>

                                    </div>
                                </asp:Panel>
                            </div>


                        </div>
                        <div class="modal-footer ">
                            <asp:LinkButton ID="lblbtnSave" runat="server" CssClass="btn btn-sm btn-success" OnClientClick="CLoseMOdal();"><span class="glyphicon glyphicon-save"></span> Update </asp:LinkButton>

                            <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>


                        </div>
                    </div>
                </div>
            </div>
            <!----------------------------------------------------------->
            <div id="myModalSupli" class="modal animated slideInLeft" role="dialog">
                <div class="modal-dialog modal-sm">
                    <div class="modal-content  ">
                        <div class="modal-header">

                            <%--<button type="button" style="background-color: red;" class="close btn btn-xs" data-dismiss="modal"><span class="fa fa-close"></span></button>--%>
                            <h4 class="modal-title">
                                <span class="fa fa-table"></span>Select Supplier</h4>
                        </div>
                        <div class="modal-body">
                            <asp:Label ID="lblhiddenIndex" Visible="false" runat="server"></asp:Label>
                            <asp:DropDownList ID="ddlsupplier" CssClass="chzn-select" Width="200px" runat="server"></asp:DropDownList>

                        </div>
                        <div class="modal-footer ">
                            <asp:LinkButton ID="saveSupplier" runat="server" CssClass="btn btn-sm btn-success" OnClick="saveSupplier_Click" OnClientClick="CLoseModalSupli();"><span class="glyphicon glyphicon-save"></span> Update </asp:LinkButton>

                            <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                        </div>
                    </div>
                </div>
            </div>

            <!----------------------------------------------------------->
            <div id="myModalSupliLoc" class="modal animated slideInLeft" role="dialog">
                <div class="modal-dialog modal-sm">
                    <div class="modal-content  ">
                        <div class="modal-header">

                            <%--<button type="button" style="background-color: red;" class="close btn btn-xs" data-dismiss="modal"><span class="fa fa-close"></span></button>--%>
                            <h4 class="modal-title">
                                <span class="fa fa-table"></span>Select Supplier</h4>
                        </div>
                        <div class="modal-body">
                            <asp:Label ID="Label5" Visible="false" runat="server"></asp:Label>
                            <asp:DropDownList ID="ddlsupplierList" CssClass="inputTxt chzn-select" Width="200px" runat="server"></asp:DropDownList>


                        </div>
                        <div class="modal-footer ">
                            <asp:LinkButton ID="saveSupplierLoc" runat="server" CssClass="btn btn-sm btn-success" OnClick="saveSupplierLoc_Click" OnClientClick="CLoseModalSupliLoc();"><span class="glyphicon glyphicon-save"></span> Update </asp:LinkButton>

                            <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                        </div>
                    </div>
                </div>
            </div>
            <!-------------------change specification modal------------------------------>
            <div id="SpecificationModal" class="modal animated slideInLeft" role="dialog">
                <div class="modal-dialog">
                    <div class="modal-content ">
                        <div class="modal-header">
                            <button type="button" style="background-color: red;" class="close btn btn-xs" data-dismiss="modal"><span class="fa fa-close"></span></button>
                            <h4 class="modal-title"><span class="fa fa-table"></span>
                                <asp:Label ID="ModalHead" runat="server"></asp:Label>
                            </h4>
                        </div>
                        <div class="modal-body form-horizontal">
                            <asp:MultiView ID="ModalMultiview" runat="server">
                                <asp:View ID="ChngSpecification" runat="server">
                                    <asp:Label ID="lblHelper" runat="server" Visible="false"></asp:Label>
                                    <asp:Label ID="lblRtype" runat="server" Visible="false"></asp:Label>
                                    <div class="form-group">
                                        <label class="col-md-4">Specifications</label>
                                        <div class="col-md-7">
                                            <asp:DropDownList ID="ddlSpecification" CssClass="form-control" runat="server">
                                            </asp:DropDownList>

                                        </div>
                                        <div class="col-md-1">
                                            <a class="btn btn-success btn-xs" data-toggle="collapse" href="#collapseExample" role="button" aria-expanded="false" aria-controls="collapseExample">
                                                <span class="glyphicon glyphicon-plus-sign"></span>
                                            </a>
                                        </div>

                                    </div>
                                    <div class="form-group">
                                        <div class="collapse" id="collapseExample">
                                            <div class="card card-body">
                                                <div class="row">
                                                    <div class="form-group">
                                                        <label class="col-md-4">Thikness/Size</label>
                                                        <div class="col-md-8">
                                                            <asp:TextBox ID="TxtThikness" runat="server" CssClass="form-control"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label class="col-md-4">Width/Length</label>
                                                        <div class="col-md-8">
                                                            <asp:TextBox ID="txtlength" runat="server" CssClass="form-control"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label class="col-md-4">Color</label>
                                                        <div class="col-md-8">
                                                            <asp:DropDownList ID="ddlModalColor" runat="server" CssClass="form-control"></asp:DropDownList>
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label class="col-md-4">Brand</label>
                                                        <div class="col-md-8">
                                                            <asp:TextBox ID="txtbrand" runat="server" CssClass="form-control"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label class="col-md-4">Remarks</label>
                                                        <div class="col-md-8">
                                                            <asp:TextBox ID="txtRemarks" runat="server" CssClass="form-control"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <div class="pull-right">
                                            <asp:LinkButton ID="LbtnChngSpcf" runat="server" OnClick="LbtnChngSpcf_Click" CssClass="btn btn-sm btn-success" OnClientClick="CLoseMOdal();"><span class="glyphicon glyphicon-save"></span> Update </asp:LinkButton>

                                        </div>
                                    </div>


                                </asp:View>
                                <asp:View ID="AddSupplier" runat="server">
                                    <div class="form-group">
                                        <label class="col-md-4">Supplier Name</label>
                                        <div class="col-md-8">
                                            <asp:TextBox ID="TxtSupllier" runat="server" CssClass="form-control"></asp:TextBox>

                                        </div>

                                    </div>
                                    <div class="form-group">
                                        <div class="pull-right">
                                            <asp:LinkButton ID="lbtnSaveSupplier" runat="server" OnClick="lbtnSaveSupplier_Click" CssClass="btn btn-sm btn-success" OnClientClick="CLoseMOdal();"><span class="glyphicon glyphicon-save"></span> Update </asp:LinkButton>

                                        </div>
                                    </div>
                                </asp:View>
                                <asp:View ID="AddComponent" runat="server">
                                    <div class="form-group">
                                        <label class="col-md-4">Component Name</label>
                                        <div class="col-md-8">
                                            <asp:TextBox ID="txtcompname" runat="server" CssClass="form-control"></asp:TextBox>

                                        </div>

                                    </div>
                                    <div class="form-group">
                                        <div class="pull-right">
                                            <asp:LinkButton ID="LbtnSaveComp" runat="server" OnClick="LbtnSaveComp_Click" CssClass="btn btn-sm btn-success" OnClientClick="CLoseMOdal();"><span class="glyphicon glyphicon-save"></span> Update </asp:LinkButton>

                                        </div>
                                    </div>
                                </asp:View>
                                <asp:View ID="AddDepartment" runat="server">
                                    <div class="form-group">
                                        <label class="col-md-4">Process Name</label>
                                        <div class="col-md-8">
                                            <asp:TextBox ID="txtDeptname" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <div class="pull-right">
                                            <asp:LinkButton ID="LbtnSaveDept" runat="server" OnClick="LbtnSaveDept_Click" CssClass="btn btn-sm btn-success" OnClientClick="CLoseMOdal();"><span class="glyphicon glyphicon-save"></span> Update </asp:LinkButton>
                                        </div>
                                    </div>
                                </asp:View>

                            </asp:MultiView>
                        </div>
                        <div class="modal-footer ">
                            <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                        </div>
                    </div>
                </div>
            </div>

        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>
