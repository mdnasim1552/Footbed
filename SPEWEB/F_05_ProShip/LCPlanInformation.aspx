<%@ Page Title="" Language="C#" MasterPageFile="~/SPE.Master" AutoEventWireup="true" CodeBehind="LCPlanInformation.aspx.cs" Inherits="SPEWEB.F_05_ProShip.LCPlanInformation" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <%--<script type="text/javascript" language="javascript" src="../Scripts/jquery-1.4.1.min.js"></script>--%>



    <script type="text/javascript">
        $(document).ready(function () {
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);
        });
        function pageLoaded() {
            $('.chzn-select').chosen({ search_contains: true });
        }
        function ShowModal(e) {
            var code = e.target.parentNode.parentNode.childNodes[2].childNodes[1].innerHTML;
            if (code == "01099") {
                openModal();
            }
        }
        function openModal() {
            $('#myModal').modal('toggle');
        }

        function CloseModal() {
            $('#myModal').modal('hide');
        }

        function SelectAllCheckboxes(gridName, chk) {

            switch (gridName) {

                case "grvProcess":

                    $('#<%=grvProcess.ClientID %>').find("input:checkbox").each(function () {

                        if ($(this).closest('tr').attr('class') == "grvRows") {

                            if ($(this).closest('tr').css('display') != "none") {

                                if ((this).disabled == false) {
                                    if (this != chk) {
                                        this.checked = chk.checked;
                                    }
                                }
                            }
                        }
                    });

                    break;

            }

        }

    </script>
    <style>
        .chzn-single {
            border-radius: 3px !important;
            height: 29px !important;
        }

        .no-arrows::-webkit-inner-spin-button,
        .no-arrows::-webkit-outer-spin-button {
            -webkit-appearance: none;
            margin: 0; /* Removes the arrow buttons */
        }
    </style>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>

            <div class="card card-fluid">
                <div class="card-body">
                    <div class="row">

                        <div class="col-md-1">
                            <div class="form-group">
                                <asp:Label ID="LblSeason" runat="server" class="label" for="ToDate">Season</asp:Label>
                                <asp:DropDownList ID="DdlSeason" AutoPostBack="true" OnSelectedIndexChanged="DdlSeason_SelectedIndexChanged" CssClass="form-control form-control-sm chzn-select" runat="server"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-5">
                            <%--<asp:Label ID="lbltxtLCName" runat="server" CssClass="lblTxt lblName"  Text="LC Code" Width="75" ></asp:Label>--%>

                            <asp:TextBox ID="txtSrcLC" runat="server" CssClass="inputTxt inpPixedWidth hidden" TabIndex="1" Style="display: none;"></asp:TextBox>
                            <asp:LinkButton ID="ibtnFindLC" CssClass="lblTxt lblName" runat="server" OnClick="ibtnFindLC_Click" Text="Article No"> </asp:LinkButton>

                            <asp:DropDownList ID="ddlLCName" runat="server" CssClass="chzn-select form-control inputTxt" AutoPostBack="True" TabIndex="3"></asp:DropDownList>

                            <asp:Label ID="lblLCdesc" runat="server" Visible="False" CssClass="form-control form-control-sm inputTxt"></asp:Label>
                        </div>

                        <div class="col-md-2" id="cellDdlProcess" runat="server" visible="false">
                            <div class="form-group">
                                <asp:Label ID="Label1" runat="server" CssClass="small">Process</asp:Label>
                                <asp:DropDownList ID="DdlProcess" CssClass="form-control form-control-sm chzn-select" AutoPostBack="True" runat="server"></asp:DropDownList>
                            </div>
                        </div>

                        <div class="col-md-1">
                            <asp:LinkButton ID="lbtnOk" runat="server" Style="margin-top: 20px" Text="Ok" OnClick="lbtnOk_Click" CssClass="btn btn-sm btn-primary " TabIndex="4"></asp:LinkButton>

                        </div>



                    </div>
                </div>
            </div>

            <div class="card card-fluid" style="min-height: 500px;">
                <div class="card-body row">
                    <asp:MultiView ID="Multiview1" runat="server">

                        <asp:View ID="CapcacityPlan" runat="server">
                            <div class="col-md-5">
                                <asp:GridView ID="gvLcpjInfo" runat="server" AutoGenerateColumns="False" CssClass=" table-striped table-hover table-bordered grvContentarea"
                                    ShowFooter="True" OnRowDataBound="gvLcpjInfo_RowDataBound">
                                    <RowStyle />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sl.">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvSlNo0" runat="server" Font-Bold="True" Height="16px"
                                                    Style="text-align: right"
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="25px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Code">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvItmCode" runat="server" Height="16px"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gcod")) %>'
                                                    Width="49px"></asp:Label>
                                            </ItemTemplate>
                                            <%-- <FooterTemplate>
                                        <asp:LinkButton ID="lUpdatInfo" runat="server" CssClass="btn btn-danger btn-sm" OnClick="lUpdatInfo_Click">Update</asp:LinkButton>
                                    </FooterTemplate>--%>
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Description">
                                            <ItemTemplate>

                                                <asp:Label ID="lgcResDesc1" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gdesc")) %>' Width="200px"></asp:Label>

                                                <asp:LinkButton ID="btnDesc1" OnClick="btnDesc1_Click" runat="server" CssClass="btn btn-xs btn-default" Visible="false"
                                                     Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gdesc")) %>' Font-Size="12px" style="padding:0px;color:blue"/>

                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lblDescHourlyCapacity" runat="server" Width="200px" CssClass="font-weight-bold" Text="Hourly Capacity"></asp:Label>
                                            </FooterTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Center" Width="200px" />
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>

                                        <asp:TemplateField Visible="False">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvgval" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gval")) %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Cutting">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtgvVal1" runat="server" BackColor="Transparent"
                                                    BorderStyle="None" Font-Size="11px"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gdesc2")) %>'
                                                    Width="100px"></asp:TextBox>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterTemplate>
                                                <asp:Label ID="lblCutHourlyCapacity" runat="server" Width="100px" CssClass="font-weight-bold"></asp:Label>
                                            </FooterTemplate>
                                            <FooterStyle HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Sewing">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtgvVal" runat="server" BackColor="Transparent"
                                                    BorderStyle="None" Font-Size="11px"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gdesc1")) %>'
                                                    Width="100px"></asp:TextBox>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterTemplate>
                                                <asp:Label ID="lblSewHourlyCapacity" runat="server" Width="100px" CssClass="font-weight-bold"></asp:Label>
                                            </FooterTemplate>
                                            <FooterStyle HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Lasting">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtgvVal2" runat="server" BackColor="Transparent"
                                                    BorderStyle="None" Font-Size="11px"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gdesc3")) %>'
                                                    Width="100px"></asp:TextBox>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterTemplate>
                                                <asp:Label ID="lblLastHourlyCapacity" runat="server" Width="100px" CssClass="font-weight-bold"></asp:Label>
                                            </FooterTemplate>
                                            <FooterStyle HorizontalAlign="Left" />
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

                            <div class="col-md-3" runat="server" id="impfrmsmvcell">
                                <asp:Button runat="server" ID="btnImport" OnClick="btnImport_Click" CssClass="btn btn-warning btn-sm" Text="Import From SMV" Font-Bold="true" />
                            </div>

                            <div class="col-md-4">
                                <asp:GridView ID="gvordanacost" runat="server" AutoGenerateColumns="False" CssClass=" table-striped table-hover table-bordered grvContentarea"
                                    ShowFooter="True" OnRowDataBound="gvordanacost_RowDataBound">
                                    <RowStyle BackColor="WhiteSmoke" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sl">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvSlNo" runat="server" Font-Bold="True" Height="16px"
                                                    Style="text-align: right"
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Description of Item">
                                            <ItemTemplate>
                                                <asp:Label ID="lgcResDesc1" runat="server" AutoCompleteType="Disabled"
                                                    BackColor="Transparent" BorderStyle="None" Font-Size="11px"
                                                    Text='<%# "<B>"+ Convert.ToString(DataBinder.Eval(Container.DataItem, "gpdesc")) + "</B>"+
                                                                         (DataBinder.Eval(Container.DataItem, "itmdesc").ToString().Trim().Length>0 ? 
                                                                         (Convert.ToString(DataBinder.Eval(Container.DataItem, "gpdesc")).Trim().Length>0 ?  "<br>" : "")+                                                             
                                                                         "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"+ 
                                                                         Convert.ToString(DataBinder.Eval(Container.DataItem, "itmdesc")).Trim(): "") 
                                                                         
                                                                    %>'
                                                    Width="200px">
                                               
                                                     
                                                     
                                                </asp:Label>
                                                <asp:HyperLink ID="hyprrr" CssClass="pull-right" runat="server" NavigateUrl='<%#Eval("IMGPATH") %>' Target="_blank">
                                                    <asp:Image ID="lblImageUrl" Width="50" runat="server" ImageUrl='<%#Eval("IMGPATH") %>' class="img-responsive"></asp:Image>
                                                </asp:HyperLink>
                                                <%-- <asp:Label ID="txtgvItemdesc" runat="server" AutoCompleteType="Disabled"
                                            BackColor="Transparent" BorderStyle="None" Font-Size="11px"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "infdesc")) %>'
                                            Width="170px"></asp:Label>--%>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Unit">
                                            <ItemTemplate>
                                                <asp:Label ID="txtgUnit" runat="server" AutoCompleteType="Disabled"
                                                    BackColor="Transparent" BorderStyle="None" Font-Size="11px"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "unit")) %>'
                                                    Width="40px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Budgeted </br>Qty">


                                            <ItemTemplate>
                                                <asp:Label ID="lblgvQty" runat="server" BackColor="Transparent"
                                                    BorderStyle="None" Height="18px" Style="text-align: right" Font-Size="11px"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "bgdwqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFQty" runat="server" Font-Bold="True" Font-Size="12px"
                                                    ForeColor="#000" Style="text-align: right"></asp:Label>
                                            </FooterTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Actual </br>Qty">


                                            <ItemTemplate>
                                                <asp:Label ID="lblgvactqty" runat="server" BackColor="Transparent"
                                                    BorderStyle="None" Height="18px" Style="text-align: right" Font-Size="11px"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "actqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFactqty" runat="server" Font-Bold="True" Font-Size="12px"
                                                    ForeColor="#000" Style="text-align: right"></asp:Label>
                                            </FooterTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>

                                        <%-- <asp:TemplateField HeaderText="Amount">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvAmt" runat="server" BackColor="Transparent"
                                            BorderStyle="None" Style="text-align: right" Font-Size="11px"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt")).ToString("#,##0;(#,##0); ") %>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFoanaamtcost" runat="server" Font-Bold="True" Font-Size="12px"
                                            ForeColor="#000" Style="text-align: right"></asp:Label>
                                    </FooterTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle HorizontalAlign="right" />
                                </asp:TemplateField>--%>
                                    </Columns>


                                    <FooterStyle CssClass="grvFooter" />
                                    <EditRowStyle />
                                    <AlternatingRowStyle />
                                    <PagerStyle CssClass="gvPagination" />
                                    <HeaderStyle CssClass="grvHeader" />

                                </asp:GridView>
                            </div>

                            <div class="col-md-12">
                                <asp:Label ID="LblMessage" Visible="false" runat="server" CssClass="alert alert-info"></asp:Label>
                            </div>
                        </asp:View>

                        <asp:View ID="EficiencyPlan" runat="server">
                            <div class="col-md-5">
                                <asp:GridView ID="gvEficiency" runat="server" AutoGenerateColumns="False" CssClass=" table-striped table-hover table-bordered grvContentarea"
                                    ShowFooter="True">
                                    <RowStyle />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sl">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvSlNo0" runat="server" Font-Bold="True" Height="16px"
                                                    Style="text-align: right"
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Code" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvItmCode" runat="server" Height="16px"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gcod")) %>'
                                                    Width="49px"></asp:Label>
                                            </ItemTemplate>

                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Description">
                                            <ItemTemplate>
                                                <asp:Label ID="lgcResDesc1" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "processdesc")) %>'
                                                    Width="140px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="Lbldesc" runat="server">(Prcnt*(WH*60)*Man)/SMV</asp:Label>
                                            </FooterTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Days">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvdayname" runat="server" Width="50px"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gdesc")) %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Efficiency(%)">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtgvPercnt" runat="server" BackColor="Transparent"
                                                    BorderStyle="None" Font-Size="11px"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "percnt")).ToString("#,##") %>'
                                                    Width="50px" Style="text-align: right"></asp:TextBox>
                                            </ItemTemplate>


                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="SMV">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtgvSMV" runat="server" BackColor="Transparent"
                                                    BorderStyle="None" Font-Size="11px"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "smv")).ToString("#,##0.00") %>'
                                                    Width="50px" Style="text-align: right"></asp:TextBox>
                                            </ItemTemplate>


                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="W.Hours">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtgvWhours" runat="server" BackColor="Transparent"
                                                    BorderStyle="None" Font-Size="11px"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "whours")).ToString("#,##") %>'
                                                    Width="50px" Style="text-align: right"></asp:TextBox>
                                            </ItemTemplate>


                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="left" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="M.P.">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtgvManpower" runat="server" BackColor="Transparent"
                                                    BorderStyle="None" Font-Size="11px"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "manpower")).ToString("#,##") %>'
                                                    Width="40px" Style="text-align: right"></asp:TextBox>
                                            </ItemTemplate>

                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="left" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Qty">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtgvQty" runat="server" BackColor="Transparent"
                                                    BorderStyle="None" Font-Size="11px"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty")).ToString("#,##") %>'
                                                    Width="70px" Style="text-align: right"></asp:TextBox>
                                            </ItemTemplate>

                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="left" />
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
                            <div class="col-md-7">
                                <div class="row">

                                    <div class="col-md-4">
                                        <div class="row">
                                            <section class="card card-figure">
                                                <!-- .card-figure -->
                                                <figure class="figure">
                                                    <!-- .figure-img -->
                                                    <div class="figure-img">
                                                        <asp:Image ID="SmpleIMg" runat="server" CssClass="img-fluid" />

                                                        <%--<img class="img-fluid" src="assets/images/dummy/img-5.jpg" alt="Card image cap">--%>
                                                        <%-- <div class="figure-action">

                                                                <a data-toggle="modal" class="btn btn-sm btn-success" href="#myModal">Click for Replace Image</a>
                                                            </div>--%>
                                                    </div>
                                                    <!-- /.figure-img -->
                                                    <!-- .figure-caption -->
                                                    <figcaption class="figure-caption">
                                                        <h6 class="figure-title">
                                                            <span class="fa fa-image"></span><a target="_blank" href="#">Article Image</a>
                                                        </h6>
                                                        <p class="text-muted mb-0">Note: You can change this image </p>
                                                    </figcaption>
                                                    <!-- /.figure-caption -->
                                                </figure>
                                                <!-- /.card-figure -->
                                            </section>
                                        </div>
                                    </div>

                                    <div class="col-md-8">
                                        <asp:Table ID="tbl" runat="server" BorderStyle="Solid" CssClass="table table-bordered grvContentarea">
                                            <asp:TableRow>
                                                <asp:TableCell CssClass="bg-instagram"><span class="fa fa-hand-point-right"></span> Buyer</asp:TableCell>
                                                <asp:TableCell>
                                                    <asp:Label ID="BuyerName" runat="server"></asp:Label>
                                                </asp:TableCell>
                                                <asp:TableCell CssClass="bg-twitter"><span class="fa fa-hand-point-right"></span> BRAND</asp:TableCell>
                                                <asp:TableCell>
                                                    <asp:Label ID="lblbrand" runat="server"></asp:Label>
                                                </asp:TableCell>
                                            </asp:TableRow>
                                            <asp:TableRow>
                                                <asp:TableCell><span class="fa fa-hand-point-right"></span> Color</asp:TableCell>
                                                <asp:TableCell CssClass="bg-instagram">
                                                    <asp:Label ID="lblcolor" runat="server"></asp:Label>
                                                </asp:TableCell>
                                                <asp:TableCell><span class="fa fa-hand-point-right"></span> Article</asp:TableCell>
                                                <asp:TableCell CssClass="bg-twitter">
                                                    <asp:Label ID="lblarticle" runat="server"></asp:Label>
                                                </asp:TableCell>
                                            </asp:TableRow>
                                            <asp:TableRow>
                                                <asp:TableCell CssClass="bg-twitter"><span class="fa fa-hand-point-right"></span> Size Range</asp:TableCell>
                                                <asp:TableCell>
                                                    <asp:Label ID="lblsizernge" runat="server"></asp:Label>
                                                </asp:TableCell>
                                                <asp:TableCell CssClass="bg-instagram"><span class="fa fa-hand-point-right"></span> Total Order</asp:TableCell>
                                                <asp:TableCell>
                                                    <asp:Label ID="TotalOrder" runat="server"></asp:Label>
                                                </asp:TableCell>

                                            </asp:TableRow>
                                            <asp:TableRow>
                                                <asp:TableCell><span class="fa fa-hand-point-right"></span> Currency</asp:TableCell>
                                                <asp:TableCell CssClass="bg-twitter">
                                                    <asp:Label ID="lblCurrency" runat="server"></asp:Label>
                                                    <asp:Label ID="lblCurcode" Visible="false" runat="server"></asp:Label>
                                                </asp:TableCell>

                                                <asp:TableCell><span class="fa fa-hand-point-right"></span> Con Rate</asp:TableCell>
                                                <asp:TableCell CssClass="bg-instagram">
                                                    <asp:Label ID="lblCRate" runat="server"></asp:Label>
                                                </asp:TableCell>
                                            </asp:TableRow>
                                        </asp:Table>
                                    </div>


                                </div>
                            </div>
                        </asp:View>

                        <asp:View ID="SMVLayout" runat="server">
                            <div class="col-md-5">
                                <asp:GridView ID="GridViewSmvCal" runat="server" AutoGenerateColumns="False" CssClass=" table-striped table-hover table-bordered grvContentarea" ShowFooter="true">
                                    <RowStyle />

                                    <Columns>

                                        <asp:TemplateField HeaderText="Sl.">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvSlNo0" runat="server" Font-Bold="True"
                                                    Style="text-align: right"
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="25px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblGcod" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "gcod") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Process Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lblProcessName" runat="server" BackColor="Transparent"
                                                    BorderStyle="None" Font-Size="11px"
                                                    Text='<%# DataBinder.Eval(Container.DataItem, "gdesc") %>'
                                                    Width="310px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="left" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="MC Type">
                                            <ItemTemplate>
                                                <asp:Label ID="lblMCType" runat="server" BackColor="Transparent"
                                                    BorderStyle="None" Font-Size="11px" Text='<%# DataBinder.Eval(Container.DataItem, "mctype") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="SMV">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtgvSMV1" runat="server" BackColor="Transparent"
                                                    BorderStyle="None" Font-Size="11px"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "smv")).ToString("#.#0;") %>'
                                                    Width="60px" Style="text-align: right"></asp:TextBox>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label runat="server" ID="gvttlSMV" CssClass="font-weight-bold"></asp:Label>
                                            </FooterTemplate>
                                            <FooterStyle HorizontalAlign="right" />
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="left" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Target %">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtTarget" runat="server" BackColor="Transparent"
                                                    BorderStyle="None" Font-Size="11px" TextMode="Number" class="no-arrows"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "eftarget")) %>'
                                                    Width="60px" Style="text-align: right"></asp:TextBox>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label runat="server" ID="gvttlTarget" CssClass="font-weight-bold"></asp:Label>
                                            </FooterTemplate>
                                            <FooterStyle HorizontalAlign="right" />
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="left" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Manpower">
                                            <ItemTemplate>
                                                <asp:Label ID="lblmpower" runat="server" BackColor="Transparent"
                                                    BorderStyle="None" Font-Size="11px" TextMode="Number" class="no-arrows"
                                                    Text='<%# (Convert.ToDouble(DataBinder.Eval(Container.DataItem, "oparator")) + Convert.ToDouble(DataBinder.Eval(Container.DataItem, "helper"))).ToString("#,##0;(#,##0); ") %>'
                                                    Width="70px" Style="text-align: right"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label runat="server" ID="gvttlMP" CssClass="font-weight-bold"></asp:Label>
                                            </FooterTemplate>
                                            <FooterStyle HorizontalAlign="right" />
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Operator">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtOperator" runat="server" BackColor="Transparent"
                                                    BorderStyle="None" Font-Size="11px" TextMode="Number" class="no-arrows"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "oparator")) %>'
                                                    Width="65px" Style="text-align: right"></asp:TextBox>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label runat="server" ID="gvttlOprtr" CssClass="font-weight-bold"></asp:Label>
                                            </FooterTemplate>
                                            <FooterStyle HorizontalAlign="right" />
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="right" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Helper">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtHelper" runat="server" BackColor="Transparent"
                                                    BorderStyle="None" Font-Size="11px"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "helper")) %>'
                                                    Width="60px" Style="text-align: right"></asp:TextBox>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label runat="server" ID="gvttlHlpr" CssClass="font-weight-bold"></asp:Label>
                                            </FooterTemplate>
                                            <FooterStyle HorizontalAlign="right" />
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="right" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Operator Name">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtOprName" runat="server" BackColor="Transparent"
                                                    BorderStyle="None" Font-Size="11px"
                                                    Text='<%# DataBinder.Eval(Container.DataItem, "oprname") %>'
                                                    Width="160px" Style="text-align: left"></asp:TextBox>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="left" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Helper Name">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtHelperName" runat="server" BackColor="Transparent"
                                                    BorderStyle="None" Font-Size="11px"
                                                    Text='<%# DataBinder.Eval(Container.DataItem, "helpername") %>'
                                                    Width="160px" Style="text-align: left"></asp:TextBox>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="left" />
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

                <div id="myModal" class="modal animated slideInLeft allmaterial" role="dialog">

                    <div class="modal-dialog modal-lg">

                        <div class="modal-content  ">

                            <div class="modal-header">
                                <h4 class="modal-title">
                                    <span class="fa fa-table"></span>
                                    Process
                                </h4>
                                <button type="button" class="close btn btn-xs" data-dismiss="modal"><span class="fa fa-arrow-left"></span></button>
                            </div>
                            <div class="modal-body">

                                <div class="row">
                                        <div class="table-responsive col-lg-12" style="min-height: 350px;">

                                            <asp:GridView ID="grvProcess" runat="server" AutoGenerateColumns="False" CssClass="table-striped table-hover table-bordered grvContentarea">
                                                <RowStyle />
                                                <Columns>
                                                    <asp:TemplateField HeaderText="SL">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvSlNo0" runat="server"
                                                                Style="text-align: right"
                                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="">
                                                        <HeaderTemplate>
                                                            <asp:CheckBox ID="chkhead" CssClass="mt-2" onclick="javascript:SelectAllCheckboxes('grvProcess', this);" ClientIDMode="Static" runat="server" />
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <asp:CheckBox runat="server" ID="chkProcess" CssClass="mx-2" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Code">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblProcode" runat="server"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "procode")) %>'
                                                                Width="90px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center"/>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Process">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblProcess" runat="server"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "prodesc")) %>'
                                                                Width="250px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center"/>
                                                    </asp:TemplateField>
                                                </Columns>
                                                <HeaderStyle CssClass="grvHeader" />
                                                <RowStyle CssClass="grvRows" />
                                            </asp:GridView>
                                        </div>
                                    </div>

                            </div>
                            <div class="modal-footer ">
                                <asp:LinkButton ID="lblbtnSave" runat="server" CssClass="btn btn-sm btn-success" OnClick="lblbtnSave_Click" OnClientClick="CloseModal()">Approve</asp:LinkButton>
                                <button type="button" class="btn btn-sm btn-warning" data-dismiss="modal" style="margin-left: 10px">Close</button>
                            </div>
                        </div>

                    </div>

                </div>

            </div>

        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>





