<%@ Page Title="" Language="C#" MasterPageFile="~/SPE.Master" AutoEventWireup="true" CodeBehind="DayWiseSalesEntry.aspx.cs" Inherits="SPEWEB.F_19_EXP.DayWiseSalesEntry" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <style>
        .switch {
            position: relative;
            display: inline-block;
            width: 56px;
            height: 30px;
        }

            .switch input {
                opacity: 0;
                width: 0;
                height: 0;
            }

        .slider {
            position: absolute;
            cursor: pointer;
            top: 0;
            left: 0;
            right: 0;
            bottom: 0;
            background-color: #ccc;
            -webkit-transition: .4s;
            transition: .4s;
        }

            .slider:before {
                position: absolute;
                content: "";
                height: 22px;
                width: 22px;
                left: 4px;
                bottom: 4px;
                background-color: white;
                -webkit-transition: .4s;
                transition: .4s;
            }

        input:checked + .slider {
            background-color: #2196F3;
        }

        input:focus + .slider {
            box-shadow: 0 0 1px #2196F3;
        }

        input:checked + .slider:before {
            -webkit-transform: translateX(26px);
            -ms-transform: translateX(26px);
            transform: translateX(26px);
        }

        /* Rounded sliders */
        .slider.round {
            border-radius: 34px;
        }

            .slider.round:before {
                border-radius: 50%;
            }
    </style>
    <script type="text/javascript" language="javascript">
        $(document).ready(function () {

            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);
        });

        function pageLoaded() {
            $(function () {
                $('[id*=ddlBuyer]').multiselect({
                    includeSelectAllOption: true
                })
            });



            //    $('#gvSales').prepend('<thead><tr><th colspan="12">Hello world</th><th colspan="12">Hello world</th></tr></thead>');

            var gvSalesreport = $('#<%=this.gvSalesreport.ClientID %>');

            gvSalesreport.gridviewScroll({
                width: 1200,
                height: 420,
                arrowsize: 30,
                railsize: 16,
                barsize: 9,
                varrowtopimg: "../../Image/arrowvt.png",
                varrowbottomimg: "../../Image/arrowvb.png",
                harrowleftimg: "../../Image/arrowhl.png",
                harrowrightimg: "../../Image/arrowhr.png",
                freezesize: 9
            });

            var gvSales = $('#<%=this.gvSales.ClientID %>');

            gvSales.gridviewScroll({

                width: 1250,
                height: 420,
                arrowsize: 30,
                railsize: 16,
                barsize: 8,
                varrowtopimg: "../Image/arrowvt.png",
                varrowbottomimg: "../Image/arrowvb.png",
                harrowleftimg: "../Image/arrowhl.png",
                harrowrightimg: "../Image/arrowhr.png",
                freezesize: 3
            });
        }


    </script>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="card card-fluid">
                <div class="card-body" style="min-height: 100px;">
                    <div class="row">
                        <div class="col-md-1">
                            <asp:Label ID="lblprodate" runat="server" CssClass=" smLbl_to"
                                EnableViewState="False" Text="Date"></asp:Label>
                            <asp:TextBox ID="txtfromdate" runat="server" CssClass="form-control form-control-sm"
                                Width="94px"></asp:TextBox>
                            <cc1:CalendarExtender ID="txtrcvdate_CalendarExtender" runat="server"
                                Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txtfromdate"></cc1:CalendarExtender>
                        </div>
                        <div class="col-md-1">
                            <asp:Label ID="Label1" runat="server" CssClass=" smLbl_to"
                                EnableViewState="False" Text="To"></asp:Label>
                            <asp:TextBox ID="txttodate" runat="server" CssClass="form-control form-control-sm"
                                Width="94px"></asp:TextBox>
                            <cc1:CalendarExtender ID="CalendarExtender1" runat="server"
                                Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txttodate"></cc1:CalendarExtender>
                        </div>

                        <label class="switch" style="margin-top: 18px">
                            <asp:CheckBox ID="ChckStatus" runat="server" />
                            <span class="slider round" cssclass="form-check-input form-check-input-sm"></span>
                        </label>

                        <div class="col-md-2 pading5px ">




                            <asp:Label ID="lbltxtpartyname" runat="server" CssClass="smLbl_to">Buyer Name</asp:Label>
                            <div style="border: 1px solid #c6c9d5 !important; border-radius: 5px; height: 28px;">
                                <asp:ListBox ID="ddlBuyer" runat="server" CssClass=" form-control" SelectionMode="Multiple"></asp:ListBox>

                            </div>
                            
                        </div>
                        <div class="col-md-1" style="margin-top:19px">
                            <asp:LinkButton ID="lbtnOk" CssClass="btn btn-primary btn-sm" runat="server" OnClick="lbtnOk_Click" TabIndex="2"> Ok </asp:LinkButton>

                        </div>
                        <div class="col-md-2 pading5px">
                            <asp:Label ID="lblExel" runat="server" CssClass="lblTxt lblName txtAlgRight" Text="Exel File"></asp:Label>
                            <div class="uploadFile">
                                <asp:FileUpload ID="fileuploadExcel" runat="server" onchange="submitform();" />
                            </div>
                        </div>
                        <div class="col-md-1" style="margin-top:18px">
                            <asp:LinkButton ID="LbtnAdjust" CssClass="btn btn-success  btn-xs " runat="server" OnClick="LbtnAdjust_OnClick" TabIndex="2">ADJUST</asp:LinkButton>

                        </div>
                        <div class="col-md-2" style="margin-top:18px">
                            <asp:HyperLink runat="server" CssClass="btn btn-xs btn-warning" runat="server" ID="HypFormat" NavigateUrl="~/Format/Salesentry.xlsx"> <span class="glyphicon glyphicon-file"></span>Format Download</asp:HyperLink>
                        </div>
                        <div class="col-md-1">
                            <asp:HyperLink runat="server" Visible="false" CssClass="btn btn-xs btn-danger" runat="server" ID="HypExport" NavigateUrl="~/RptViewer.aspx?PrintOpt=GRIDTOEXCEL"> <span class="glyphicon glyphicon-export"></span>Export</asp:HyperLink>


                        </div>
                    </div>
                </div>
                </div>
            </div>


           
                    <asp:MultiView ID="Multivew" runat="server">
                        <asp:View ID="SaleEntry" runat="server">
                            <div class="card card-fluid">
                                <div class="card-body" style="min-height: 300px;">
                                    <div class="table-responsive">
                                        <asp:GridView ID="gvSales" ClientIDMode="Static" AutoGenerateColumns="False" runat="server" CssClass=" table-striped table-hover table-bordered grvContentarea">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Sl.No.">
                                                    <ItemTemplate>
                                                        <asp:Label ID="PlblgvSlNo" runat="server" Font-Bold="True" Height="16px"
                                                            Style="text-align: right"
                                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="">
                                                    <HeaderTemplate>
                                                        <table style="border: none;">
                                                            <tr>
                                                                <td style="border: none;">
                                                                    <asp:Label ID="Label4" runat="server" Font-Bold="True"
                                                                        Text="INV ID" Width="50"></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:HyperLink ID="hlbtntbCdataExelE" CssClass="btn btn-xs btn-danger" runat="server">Export &nbsp;<span class="fa fa-file-excel-o"></span></asp:HyperLink>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lvgrrno" runat="server"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "invno")) %>'
                                                            Width="95px"></asp:Label>
                                                    </ItemTemplate>

                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Invoice No">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lgcInvRefno" runat="server"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "invrefno")) %>'
                                                            Width="100px"></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle Font-Size="10px" />
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    <FooterStyle HorizontalAlign="Center" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Invoice Date">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lgcbatchcode" runat="server"
                                                            Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "invdate")).ToString("dd-MMM-yyyy") %>'
                                                            Width="80px"></asp:Label>
                                                    </ItemTemplate>

                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    <FooterStyle HorizontalAlign="Center" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Buyer">

                                                    <ItemTemplate>
                                                        <asp:Label ID="lgcBuyerDesc" runat="server"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "buyername")) %>'
                                                            Width="50px"></asp:Label>
                                                    </ItemTemplate>

                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    <FooterStyle HorizontalAlign="Center" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Product">

                                                    <ItemTemplate>
                                                        <asp:Label ID="lgcStorDesc" runat="server"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "styledesc")) %>'
                                                            Width="70px"></asp:Label>
                                                    </ItemTemplate>

                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    <FooterStyle HorizontalAlign="Center" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="QTY Prs">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lgcbatchDesc" runat="server"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "totlprs")).ToString("#,##0;(#,##0); ") %>'
                                                            Width="70px"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    <FooterStyle HorizontalAlign="Center" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Inv Amt">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lgcRefno" runat="server"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "fcamt")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                            Width="80px"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    <FooterStyle HorizontalAlign="Center" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Ex Rate">

                                                    <ItemTemplate>
                                                        <asp:Label ID="lgcIsuno" runat="server"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "exrate")).ToString("#,##0.000;(#,##0.000); ") %>'
                                                            Width="70px"></asp:Label>
                                                    </ItemTemplate>

                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Inv AMt <br>(Taka)">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lgvIsuQty" runat="server" Style="font-size: 11px; text-align: right"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "bdtamt")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                            Width="70px"></asp:Label>
                                                    </ItemTemplate>

                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    <ItemStyle HorizontalAlign="right" />

                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Ex-Factory <br> Date">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lgcExfactoryDaet" runat="server"
                                                            Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "exfacdt")).ToString("dd-MMM-yyyy") %>'
                                                            Width="80px"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Bl/AWB <br> NO">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="lgcBlawbno" runat="server" BorderColor="#d67364" BorderStyle="Solid" BorderWidth="1px" BackColor="Transparent"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "blawbno")) %>'
                                                            Width="80px"></asp:TextBox>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Bl/AWB <br> DT">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="TxtBlawbdt" runat="server" BorderColor="#d67364" BorderStyle="Solid" BorderWidth="1px" BackColor="Transparent"
                                                            Text='<%# (Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "blawbdt")).Year==1900? "" :Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "blawbdt")).ToString("dd-MMM-yyyy")) %>'
                                                            Width="80px"></asp:TextBox>
                                                        <cc1:CalendarExtender ID="TxtBlawbdt_CalendarExtender" runat="server" Enabled="True" Format="dd-MMM-yyyy" TargetControlID="TxtBlawbdt"></cc1:CalendarExtender>

                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Bl/AWB <br>PAYMENT <br> MATURITY DT">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="TxtBlawbrldt" runat="server" BorderColor="#d67364" BorderStyle="Solid" BorderWidth="1px" BackColor="Transparent"
                                                            Text='<%# (Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "blawbrldt")).Year==1900? "" : Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "blawbrldt")).ToString("dd-MMM-yyyy")) %>'
                                                            Width="80px"></asp:TextBox>
                                                        <cc1:CalendarExtender ID="TxtBlawbrldt_CalendarExtender" runat="server" Enabled="True" Format="dd-MMM-yyyy" TargetControlID="TxtBlawbrldt"></cc1:CalendarExtender>

                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="HO<br>CREDIT DT">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="TxtHoCrdt" runat="server" BorderColor="#d67364" BorderStyle="Solid" BorderWidth="1px" BackColor="Transparent"
                                                            Text='<%# (Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "hocrdt")).Year==1900? "" :Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "hocrdt")).ToString("dd-MMM-yyyy")) %>'
                                                            Width="80px"></asp:TextBox>
                                                        <cc1:CalendarExtender ID="TxtHoCrdt_CalendarExtender" runat="server" Enabled="True" Format="dd-MMM-yyyy" TargetControlID="TxtHoCrdt"></cc1:CalendarExtender>

                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="RLZD DT">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="Txtrizddt" runat="server" BorderColor="#d67364" BorderStyle="Solid" BorderWidth="1px" BackColor="Transparent"
                                                            Text='<%# (Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "rlzddt")).Year==1900? "" :Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "rlzddt")).ToString("dd-MMM-yyyy")) %>'
                                                            Width="80px"></asp:TextBox>
                                                        <cc1:CalendarExtender ID="Txtrizddt_CalendarExtender" runat="server" Enabled="True" Format="dd-MMM-yyyy" TargetControlID="Txtrizddt"></cc1:CalendarExtender>

                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                </asp:TemplateField>


                                                <asp:TemplateField HeaderText="C&F">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="Txtgvcf" runat="server" Style="font-size: 11px; text-align: right" BorderColor="#d67364" BorderStyle="Solid" BorderWidth="1px" BackColor="Transparent"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "cf")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                            Width="70px"></asp:TextBox>
                                                    </ItemTemplate>

                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    <ItemStyle HorizontalAlign="right" />

                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Truk">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="Txtgvtruk" runat="server" Style="font-size: 11px; text-align: right" BorderColor="#d67364" BorderStyle="Solid" BorderWidth="1px" BackColor="Transparent"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "truk")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                            Width="70px"></asp:TextBox>
                                                    </ItemTemplate>

                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    <ItemStyle HorizontalAlign="right" />

                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Documents <br> Submit DT">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtDocSubDate" runat="server" Style="font-size: 11px; text-align: right" BorderColor="#d67364" BorderStyle="Solid" BorderWidth="1px" BackColor="Transparent"
                                                            Text='<%# (Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "docsubdt")).Year==1900? "" :Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "docsubdt")).ToString("dd-MMM-yyyy")) %>'
                                                            Width="70px"></asp:TextBox>
                                                        <cc1:CalendarExtender ID="txtDocSubDate_CalendarExtender" runat="server" Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txtDocSubDate"></cc1:CalendarExtender>

                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    <ItemStyle HorizontalAlign="right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="DHL NO">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="TxtgvDhlno" runat="server" Style="font-size: 11px; text-align: right" BorderColor="#d67364" BorderStyle="Solid" BorderWidth="1px" BackColor="Transparent"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "dhlno")) %>'
                                                            Width="70px"></asp:TextBox>
                                                    </ItemTemplate>

                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    <ItemStyle HorizontalAlign="right" />

                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="DHL DT">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtDhlDate" runat="server" Style="font-size: 11px; text-align: right" BorderColor="#d67364" BorderStyle="Solid" BorderWidth="1px" BackColor="Transparent"
                                                            Text='<%# (Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "dhldt")).Year==1900? "" :Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "dhldt")).ToString("dd-MMM-yyyy")) %>'
                                                            Width="70px"></asp:TextBox>
                                                        <cc1:CalendarExtender ID="txtDhlDate_CalendarExtender" runat="server" Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txtDhlDate"></cc1:CalendarExtender>

                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    <ItemStyle HorizontalAlign="right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Cost For <br> GSP">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="TxtgvCostGsp" runat="server" Style="font-size: 11px; text-align: right" BorderColor="#d67364" BorderStyle="Solid" BorderWidth="1px" BackColor="Transparent"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "gspcost")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                            Width="70px"></asp:TextBox>
                                                    </ItemTemplate>

                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    <ItemStyle HorizontalAlign="right" />

                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Cost For<br> BL">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="TxtgvBlCost" runat="server" Style="font-size: 11px; text-align: right" BorderColor="#d67364" BorderStyle="Solid" BorderWidth="1px" BackColor="Transparent"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "blcost")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                            Width="70px"></asp:TextBox>
                                                    </ItemTemplate>

                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    <ItemStyle HorizontalAlign="right" />

                                                </asp:TemplateField>

                                            </Columns>
                                            <PagerStyle CssClass="gvPagination" />
                                            <HeaderStyle CssClass="grvHeader" />
                                        </asp:GridView>
                                    </div>
                                </div>
                            </div>
                        </asp:View>
                        <asp:View ID="SalesReprt" runat="server">
                            <div class="card card-fluid">
                                <div class="card-body" style="min-height: 300px;">
                                    <div class="table-responsive">
                                        <asp:GridView ID="gvSalesreport" AutoGenerateColumns="False" runat="server" CssClass="table-striped table-hover table-bordered grvContentarea">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Sl.No.">
                                                    <ItemTemplate>
                                                        <asp:Label ID="PlblgvSlNo" runat="server" Font-Bold="True" Height="16px"
                                                            Style="text-align: right"
                                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Term">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lgcTermDesc" runat="server"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "termdesc")) %>'
                                                            Width="40px"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Year">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lgcInvYear" runat="server"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "invyear")) %>'
                                                            Width="30px"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="">
                                                    <HeaderTemplate>
                                                        <table style="border: none;">
                                                            <tr>
                                                                <td style="border: none;">
                                                                    <asp:Label ID="Label4" runat="server" Font-Bold="True"
                                                                        Text="INV"></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:HyperLink ID="hlbtntbCdataExel" CssClass="btn btn-xs btn-danger" runat="server"><span class="fa fa-file-excel-o"></span></asp:HyperLink>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lvgrrno" runat="server"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "invno1")) %>'
                                                            Width="40px"></asp:Label>
                                                    </ItemTemplate>

                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Invoice <br> Date">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lgcbatchcode" runat="server"
                                                            Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "invdate")).ToString("dd-MMM-yyyy") %>'
                                                            Width="70px"></asp:Label>
                                                    </ItemTemplate>

                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Buyer">

                                                    <ItemTemplate>
                                                        <asp:Label ID="lgcBuyerDesc" runat="server"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "buyername")) %>'
                                                            Width="50px"></asp:Label>
                                                    </ItemTemplate>

                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Country">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lgcCountryDesc" runat="server"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "countryname")) %>'
                                                            Width="50px"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Agent">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lgcAgentDesc" runat="server"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "agentname")) %>'
                                                            Width="50px"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Product">

                                                    <ItemTemplate>
                                                        <asp:Label ID="lgcStorDesc" runat="server"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "styledesc")) %>'
                                                            Width="70px"></asp:Label>
                                                    </ItemTemplate>

                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    <FooterStyle HorizontalAlign="Center" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="QTY Prs">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lgcbatchDesc" runat="server"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "totlprs")).ToString("#,##0;(#,##0); ") %>'
                                                            Width="70px"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Inv Amt">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lgcRefno" runat="server"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "fcamt")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                            Width="80px"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Ex Rate">

                                                    <ItemTemplate>
                                                        <asp:Label ID="lgcIsuno" runat="server"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "exrate")).ToString("#,##0.000;(#,##0.000); ") %>'
                                                            Width="70px"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Inv AMt <br>(Taka)">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lgvIsuQty" runat="server" Style="font-size: 11px; text-align: right"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "bdtamt")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                            Width="70px"></asp:Label>
                                                    </ItemTemplate>

                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    <ItemStyle HorizontalAlign="right" />

                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Ex-Factory <br> Date">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lgcExfactoryDaet" runat="server"
                                                            Text='<%# (Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "exfacdt")).Year==1900? "":Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "exfacdt")).ToString("dd-MMM-yyyy")) %>'
                                                            Width="80px"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Bl/AWB <br> NO">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lgcBlawbno" runat="server"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "blawbno")) %>'
                                                            Width="80px"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Bl/AWB <br> DT">
                                                    <ItemTemplate>
                                                        <asp:Label ID="TxtBlawbdt" runat="server"
                                                            Text='<%# (Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "blawbdt")).Year==1900? "":Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "blawbdt")).ToString("dd-MMM-yyyy")) %>'
                                                            Width="80px"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Bl/AWB <br>PAYMENNT <br>MATURITY DT">
                                                    <ItemTemplate>
                                                        <asp:Label ID="TxtBlawbrldt" runat="server"
                                                            Text='<%# (Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "blawbrldt")).Year==1900? "":Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "blawbrldt")).ToString("dd-MMM-yyyy")) %>'
                                                            Width="80px"></asp:Label>

                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="ETA Date">
                                                    <ItemTemplate>
                                                        <asp:Label ID="TxtEtaDate" runat="server"
                                                            Text='<%# (Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "etadate")).Year==1900? "":Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "etadate")).ToString("dd-MMM-yyyy")) %>'
                                                            Width="80px"></asp:Label>

                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Desp">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lgcShipmentDesc" runat="server"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "shipmntdesc")) %>'
                                                            Width="50px"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Mode">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lgcModeDesc" runat="server"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "modedesc")) %>'
                                                            Width="50px"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="AGV Rate">
                                                    <ItemTemplate>
                                                        <asp:Label ID="LblgvAvgRate" runat="server" Style="font-size: 11px; text-align: right"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "avgrate")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                            Width="70px"></asp:Label>
                                                    </ItemTemplate>

                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    <ItemStyle HorizontalAlign="right" />

                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Days">
                                                    <ItemTemplate>
                                                        <asp:Label ID="LblgvDays" runat="server" Style="font-size: 11px; text-align: right"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "mdays")).ToString("#,##0;(#,##0); ") %>'
                                                            Width="50px"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    <ItemStyle HorizontalAlign="right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="PAYMENT <br>DUE DT">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblDueDate" runat="server"
                                                            Text='<%# (Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "duedate")).Year==1900? "":Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "duedate")).ToString("dd-MMM-yyyy")) %>'
                                                            Width="80px"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="HO<br>CREDIT DT">
                                                    <ItemTemplate>
                                                        <asp:Label ID="TxtHoCrdt" runat="server"
                                                            Text='<%# (Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "hocrdt")).Year==1900? "":Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "hocrdt")).ToString("dd-MMM-yyyy")) %>'
                                                            Width="80px"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="RLZD DT">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Txtrizddt" runat="server"
                                                            Text='<%# (Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "rlzddt")).Year==1900? "":Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "rlzddt")).ToString("dd-MMM-yyyy")) %>'
                                                            Width="80px"></asp:Label>

                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Rlzd Amt">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Txtgvrizdfcamt" runat="server" Style="font-size: 11px; text-align: right"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "rlzdfcamt")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                            Width="70px"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    <ItemStyle HorizontalAlign="right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Deduct">
                                                    <ItemTemplate>
                                                        <asp:Label ID="TxtgvDeductfc" runat="server" Style="font-size: 11px; text-align: right"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "deductfc")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                            Width="70px"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    <ItemStyle HorizontalAlign="right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Deduct Taka">
                                                    <ItemTemplate>
                                                        <asp:Label ID="TxtgvDeductbdt" runat="server" Style="font-size: 11px; text-align: right"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "deductbdt")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                            Width="70px"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    <ItemStyle HorizontalAlign="right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Rlzd Taka">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Txtgvrizdbdtamt" runat="server" Style="font-size: 11px; text-align: right"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "rlzdbdtamt")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                            Width="70px"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    <ItemStyle HorizontalAlign="right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Delay">
                                                    <ItemTemplate>
                                                        <asp:Label ID="TxtgvDelays" runat="server" Style="font-size: 11px; text-align: right"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "delays")).ToString("#,##0;(#,##0); ") %>'
                                                            Width="70px"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    <ItemStyle HorizontalAlign="right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Agent com (%)">
                                                    <ItemTemplate>
                                                        <asp:Label ID="TxtgvAgntcomfc" runat="server" Style="font-size: 11px; text-align: right"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "agntcomfc")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                            Width="70px"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    <ItemStyle HorizontalAlign="right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Royality <br> com (%)">
                                                    <ItemTemplate>
                                                        <asp:Label ID="TxtgvRoyalcomfc" runat="server" Style="font-size: 11px; text-align: right"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "roylcomfc")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                            Width="70px"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    <ItemStyle HorizontalAlign="right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Incentive (%)">
                                                    <ItemTemplate>
                                                        <asp:Label ID="TxtgvIncentivefc" runat="server" Style="font-size: 11px; text-align: right"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "incentivefc")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                            Width="70px"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    <ItemStyle HorizontalAlign="right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Nett Relzd">
                                                    <ItemTemplate>
                                                        <asp:Label ID="TxtgvIncentivefc" runat="server" Style="font-size: 11px; text-align: right"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "netrlzd")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                            Width="70px"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    <ItemStyle HorizontalAlign="right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Freight">
                                                    <ItemTemplate>
                                                        <asp:Label ID="TxtgvFreight" runat="server" Style="font-size: 11px; text-align: right"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "freight")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                            Width="70px"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    <ItemStyle HorizontalAlign="right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="C&F">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Txtgvcf" runat="server" Style="font-size: 11px; text-align: right"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "cf")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                            Width="70px"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    <ItemStyle HorizontalAlign="right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Truk">
                                                    <ItemTemplate>
                                                        <asp:Label ID="Txtgvtruk" runat="server" Style="font-size: 11px; text-align: right"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "truk")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                            Width="70px"></asp:Label>
                                                    </ItemTemplate>

                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    <ItemStyle HorizontalAlign="right" />

                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Documents <br> Submit DT">
                                                    <ItemTemplate>
                                                        <asp:Label ID="txtDocSubDate" runat="server" Style="font-size: 11px; text-align: right"
                                                            Text='<%# (Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "docsubdt")).Year==1900? "":Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "docsubdt")).ToString("dd-MMM-yyyy")) %>'
                                                            Width="70px"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    <ItemStyle HorizontalAlign="right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="DHL NO">
                                                    <ItemTemplate>
                                                        <asp:Label ID="TxtgvDhlno" runat="server"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "dhlno")) %>'
                                                            Width="70px"></asp:Label>
                                                    </ItemTemplate>

                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    <ItemStyle HorizontalAlign="right" />

                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="DHL DT">
                                                    <ItemTemplate>
                                                        <asp:Label ID="LblDhlDate" runat="server" Style="font-size: 11px; text-align: right"
                                                            Text='<%# (Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "dhldt")).Year==1900? "":Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "dhldt")).ToString("dd-MMM-yyyy")) %>'
                                                            Width="70px"></asp:Label>

                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    <ItemStyle HorizontalAlign="right" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Bank Ref">
                                                    <ItemTemplate>
                                                        <asp:Label ID="LblBankRef" runat="server"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "refno")) %>'
                                                            Width="70px"></asp:Label>

                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    <ItemStyle HorizontalAlign="right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Bank">
                                                    <ItemTemplate>
                                                        <asp:Label ID="LblBankName" runat="server"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "cactdesc")) %>'
                                                            Width="100px"></asp:Label>

                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    <ItemStyle HorizontalAlign="right" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Cost For <br> GSP">
                                                    <ItemTemplate>
                                                        <asp:Label ID="TxtgvCostGsp" runat="server" Style="font-size: 11px; text-align: right"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "gspcost")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                            Width="70px"></asp:Label>
                                                    </ItemTemplate>

                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    <ItemStyle HorizontalAlign="right" />

                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Cost For<br> BL">
                                                    <ItemTemplate>
                                                        <asp:Label ID="TxtgvBlCost" runat="server" Style="font-size: 11px; text-align: right"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "blcost")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                            Width="70px"></asp:Label>
                                                    </ItemTemplate>

                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    <ItemStyle HorizontalAlign="right" />

                                                </asp:TemplateField>
                                                <%-- <asp:TemplateField HeaderText="Order Type">
                                    <ItemTemplate>
                                        <asp:Label ID="LblOrderType" runat="server" 
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "ordrtype")) %>'
                                            Width="70px" ></asp:Label>
                                    </ItemTemplate>

                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    <ItemStyle HorizontalAlign="right" />

                                </asp:TemplateField> --%>
                                            </Columns>
                                            <PagerStyle CssClass="gvPagination" />
                                            <HeaderStyle CssClass="grvHeader" />
                                        </asp:GridView>
                                    </div>
                                </div>
                            </div>
                        </asp:View>
                    </asp:MultiView>


               
        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>





