<%@ Page Title="" Language="C#" MasterPageFile="~/SPE.Master" AutoEventWireup="true" CodeBehind="ProductionPlan.aspx.cs" Inherits="SPEWEB.F_15_Pro.ProductionPlan" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript">

        $(document).ready(function () {
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);
        });
        function pageLoaded() {
            $('.chzn-select').chosen({ search_contains: true });

            <%--var gvHourlyProd = $('#<%=this.gvHourlyProd.ClientID %>');
            gvHourlyProd.gridviewScroll({
                width: 1220,
                height: 410,
                arrowsize: 30,
                railsize: 16,
                barsize: 8,
                varrowtopimg: "../Image/arrowvt.png",
                varrowbottomimg: "../Image/arrowvb.png",
                harrowleftimg: "../Image/arrowhl.png",
                harrowrightimg: "../Image/arrowhr.png",
                freezesize: 7
            });--%>


           <%-- var gvProdu = $('#<%=this.gvProdu.ClientID %>');
            gvProdu.ScrollableGv();
            var gridview = $('#<%=this.dgv1.ClientID %>');
            gridview.ScrollableGv();--%>
        }

    </script>
    <script language="javascript" type="text/javascript">
        function GoToNextTextBox(currentTxtId, e) {


            if (e.keyCode == 13 || e.keyCode == 40) {

                var number = parseInt(currentTxtId.id.substring(37));

                var nextId = number + 1;

                var nextIdString = "ContentPlaceHolder1_gvProdu_gvtxtQty_" + nextId.toString();

                var x = document.getElementById(nextIdString);
                x.focus();
            }
            else
                if (e.keyCode == 38) {
                    var number = parseInt(currentTxtId.id.substring(37));
                    var nextId = number - 1;

                    var nextIdString = "ContentPlaceHolder1_gvProdu_gvtxtQty_" + nextId.toString();

                    var x = document.getElementById(nextIdString);
                    x.focus();
                }

        }
    </script>


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
                                <asp:Label ID="lblprodate" runat="server" CssClass="label"> Date</asp:Label>
                                <asp:TextBox ID="txtrcvdate" runat="server" CssClass="form-control form-control-sm small"></asp:TextBox>
                                <cc1:CalendarExtender ID="CalendarExtender1" runat="server"
                                    Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txtrcvdate"></cc1:CalendarExtender>
                            </div>
                        </div>



                        <div class="col-md-3">
                            <div class="form-group">
                                <asp:Label ID="lblbatchcode" runat="server" CssClass="label">WIP</asp:Label>
                                <asp:TextBox ID="txtbatchsrc" runat="server" CssClass="form-control form-control-sm" Visible="false"></asp:TextBox>

                                <asp:LinkButton ID="imgbtnbatchsrc" runat="server" CssClass="btn btn-primary btn-sm" Visible="false" OnClick="imgbtnbatchsrc_Click"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>
                                <asp:DropDownList ID="ddlbatchno" runat="server" CssClass="form-control form-control-sm chzn-select">
                                </asp:DropDownList>


                            </div>
                        </div>
                        <div class="col-md-1">
                            <div class="form-group">
                                <asp:LinkButton ID="lnkOk" runat="server" CssClass="btn btn-primary btn-sm" OnClick="lnkOk_Click">Ok</asp:LinkButton>


                            </div>
                        </div>
                        <div class="col-md-2">
                            <div class="form-group">
                                <asp:Label ID="Label1" runat="server" CssClass="label"> Batch No</asp:Label>
                                <asp:TextBox ID="txtBatch" Enabled="false" runat="server" CssClass="form-control form-control-sm small"></asp:TextBox>

                            </div>
                        </div>
                        <div class="col-md-2">
                            <div class="form-group">
                                <asp:Label ID="lblprodid" runat="server" CssClass="label"> Production Id</asp:Label>
                                <asp:TextBox ID="txtprodid" Enabled="false" runat="server" CssClass="form-control form-control-sm small"></asp:TextBox>

                            </div>
                        </div>
                        <div class="col-md-2">
                            <div class="form-group">
                                <asp:LinkButton ID="ImgbtnFindProList" runat="server" CssClass="label" OnClick="ImgbtnFindProList_Click">Previous List</asp:LinkButton>
                                <asp:DropDownList ID="ddlPrevPro" runat="server" CssClass="form-control form-control-sm" TabIndex="6">
                                </asp:DropDownList>

                            </div>
                        </div>
                    </div>

                    <div class="row" id="PnlProCode" runat="server" visible="false">
                        <div class="col-md-1">
                            <div class="form-group">
                                <asp:Label ID="Label6" runat="server" CssClass="label">Exp Date</asp:Label>
                                <asp:TextBox ID="txtExDate" runat="server" CssClass="form-control form-control-sm small"></asp:TextBox>
                                <cc1:CalendarExtender ID="CalendarExtender2" runat="server"
                                    Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txtExDate"></cc1:CalendarExtender>
                            </div>
                        </div>

                        <div class="col-md-3">
                            <div class="form-group">
                                <asp:Label ID="Label3" runat="server" CssClass="label">Product</asp:Label>
                                <asp:TextBox ID="txtCenter" runat="server" CssClass="form-control form-control-sm" Visible="false"></asp:TextBox>

                                <asp:LinkButton ID="imgbtnSelestPro" runat="server" CssClass="btn btn-primary btn-sm" Visible="false" OnClick="imgbtnProduc_Click"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>
                                <asp:DropDownList ID="DropDownList1" runat="server" CssClass="form-control form-control-sm chzn-select" AutoPostBack="True" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged">
                                </asp:DropDownList>


                            </div>
                        </div>
                        <div class="col-md-2 ">
                            <div class="form-group" style="margin-top: 20px;">
                                <asp:LinkButton ID="lbtnSelectReqList" runat="server" CssClass="btn btn-primary btn-sm"
                                    OnClick="lnkAddToTab_Click"><span class="fa fa-check"></span></asp:LinkButton>

                                <asp:LinkButton ID="lnkSelectAll" runat="server" CssClass="btn btn-primary btn-sm" OnClick="lnkAddToTabAll_Click"><span class="fa fa-check-double"></span></asp:LinkButton>
                                <asp:CheckBox runat="server" ID="chkAct" CssClass="pull-left margin5px" Text="Actual" Checked="true" />

                            </div>
                        </div>

                        <div class="col-md-2">
                            <div class="form-group">
                                <asp:Label ID="lblinputed" runat="server" CssClass="label">Inputed Qty</asp:Label>
                                <asp:TextBox ID="txtprodFqty" runat="server" CssClass="form-control form-control-sm small"></asp:TextBox>

                            </div>
                        </div>
                        <div class="col-md-1">
                            <div class="form-group">
                                <asp:Label ID="lblwastage" runat="server" CssClass="label">Wastage</asp:Label>
                                <asp:TextBox ID="lblvalwastage" runat="server" CssClass="form-control form-control-sm small"></asp:TextBox>
                                <asp:Label ID="lbluweight" runat="server" CssClass="label" Visible="false"></asp:Label>

                            </div>
                        </div>
                        <div class="col-md-1">
                            <div class="form-group">
                                <asp:Label ID="lblactual" runat="server" CssClass="label">Actual</asp:Label>
                                <asp:TextBox ID="txtactual" runat="server" CssClass="form-control form-control-sm small"></asp:TextBox>

                            </div>
                        </div>
                        <div class="col-md-1">
                            <div class="form-group">
                                <asp:Label ID="lblbstorid1" runat="server" CssClass="label">Rate</asp:Label>
                                <asp:TextBox ID="txtProrat" runat="server" CssClass="form-control form-control-sm small" ReadOnly="true"></asp:TextBox>

                            </div>
                        </div>

                        <asp:Panel ID="Pnladditional" runat="server" Visible="false">
                            <div class="form-group">
                                <div class="col-md-5 pading5px ">
                                    <asp:Label ID="LblLine" runat="server" CssClass="lblTxt lblName">Prod. Line</asp:Label>

                                    <asp:DropDownList ID="ddlmach" runat="server" AutoPostBack="True"
                                        CssClass="inputTxt chzn-select" Width="350px">
                                    </asp:DropDownList>


                                </div>
                                <div class="col-md-7">
                                    <asp:Panel ID="MachineryPanl" runat="server" Visible="False">
                                        <asp:Label ID="LblShift" runat="server" CssClass=" smLbl_to" Text="Shift"></asp:Label>
                                        <asp:DropDownList ID="ddlshift" runat="server" CssClass="inputTxt chzn-select" Width="130px">
                                        </asp:DropDownList>

                                    </asp:Panel>
                                    <asp:Panel ID="LInePanel" runat="server" Visible="False">
                                        <div class="col-md-4">
                                            <asp:Label ID="Label5" runat="server" CssClass=" smLbl_to" Text="Hour"></asp:Label>
                                            <asp:DropDownList ID="ddlhour" runat="server" Width="150" CssClass="inputTxt chzn-select">
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-md-4">
                                            <asp:Label ID="Label4" runat="server" CssClass="smLbl_to" Text="Status"></asp:Label>
                                            <asp:DropDownList ID="ddlstatus" runat="server" CssClass="inputTxt chzn-select" Width="100px">
                                            </asp:DropDownList>
                                        </div>

                                    </asp:Panel>
                                </div>
                            </div>
                        </asp:Panel>
                    </div>
                    </div>
                    <div class="card card-fluid">
                        <div class="card-body" style="min-height: 400px;">
                            <div class="row">

                                <asp:GridView ID="gvProdu" runat="server" OnRowDataBound="gvProdu_RowDataBound" AutoGenerateColumns="False" OnRowDeleting="gvProdu_RowDeleting"
                                    CellPadding="2" CssClass="table-striped table-hover table-bordered grvContentarea"
                                    GridLines="Vertical" ShowFooter="True">
                                    <RowStyle />
                                    <Columns>
                                        <asp:TemplateField HeaderText="SL">
                                            <ItemTemplate>
                                                <asp:Label ID="Label10" runat="server"
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>'
                                                    Width="12px"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:CommandField ShowDeleteButton="True" ControlStyle-CssClass="text-red" DeleteText="<span class='fa fa-trash'></span>" />


                                        <asp:TemplateField HeaderText="Product Code" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblprocode" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "procode")) %>'
                                                    Width="80px" Style="text-align: left"></asp:Label>
                                                  <asp:Label ID="LBlSpcfcod" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "spcfcod")) %>'
                                                    Width="80px" Style="text-align: left"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Product Description ">
                                            <FooterTemplate>
                                                <asp:Label ID="gvlblTotal" runat="server" Text="Label" Font-Bold="True"
                                                    Font-Size="10px" ForeColor="Blue" Width="120px" Style="text-align: right">Total:</asp:Label>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="Label12" runat="server" Font-Size="X-Small"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "prodesc")) %>'
                                                    Width="180px" Style="text-align: left"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="left" />
                                        </asp:TemplateField>

                                        <%--<asp:TemplateField HeaderText="Machinery">
                                            <FooterTemplate>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblmachinery" runat="server" Font-Size="X-Small"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "machinery")) %>'
                                                    Width="180px" Style="text-align: left"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="left" />
                                        </asp:TemplateField>--%>
                                        <asp:TemplateField HeaderText="Mach Id" Visible="false">
                                            <FooterTemplate>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblmachid" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "machid")) %>'
                                                    Width="200px" Style="text-align: left"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="left" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Req. Qty">
                                            <FooterTemplate>
                                                <asp:Label ID="gvlblFbgdqty" runat="server" Text="" Font-Bold="True"
                                                    Font-Size="10px" ForeColor="Blue" Width="70px" Style="text-align: right"></asp:Label>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="gvlblbgdqty" runat="server" BorderStyle="None"
                                                    Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "bgdqty")).ToString("#,##0.0000;(#,##0.0000); ") %>'
                                                    Width="70px" BackColor="Transparent"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />

                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Prod. Qty">
                                            <FooterTemplate>
                                                <asp:Label ID="gvlblFQty" runat="server" Text="" Font-Bold="True"
                                                    Font-Size="10px" ForeColor="Blue" Width="70px" Style="text-align: right"></asp:Label>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:TextBox ID="gvtxtQty" runat="server" BorderStyle="None" OnKeyUp="GoToNextTextBox(this, event); return false;"
                                                    Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "proqty")).ToString("#,##0.0000;(#,##0.0000); ") %>'
                                                    Width="70px" BackColor="Transparent"></asp:TextBox>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" ForeColor="Red" />
                                            <HeaderStyle ForeColor="Red" />
                                            <FooterStyle ForeColor="Red" />
                                        </asp:TemplateField>


                                      <%--  <asp:TemplateField HeaderText="Runner </br> Weight" Visible="false">
                                            <FooterTemplate>
                                                <asp:Label ID="gvlblFRunnerAmt" runat="server" Text="" Font-Bold="True"
                                                    Font-Size="10px" ForeColor="Blue" Width="70px" Style="text-align: right"></asp:Label>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:TextBox ID="gvtxtRunnerAmt" runat="server" BorderStyle="None"
                                                    Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "biqty1")).ToString("#,##0.0000;(#,##0.0000); ") %>' Width="70px" BackColor="Transparent"></asp:TextBox>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" ForeColor="Red" />
                                            <HeaderStyle ForeColor="Red" />
                                            <FooterStyle ForeColor="Red" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Rejection</br> Weight" Visible="false">
                                            <FooterTemplate>
                                                <asp:Label ID="gvlblRejAmt" runat="server" Text="" Font-Bold="True"
                                                    Font-Size="10px" ForeColor="Blue" Width="70px" Style="text-align: right"></asp:Label>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:TextBox ID="gvtxtRejAmt" runat="server" BorderStyle="None"
                                                    Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "biqty2")).ToString("#,##0.0000;(#,##0.0000); ") %>' Width="70px" BackColor="Transparent"></asp:TextBox>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" ForeColor="Red" />
                                            <HeaderStyle ForeColor="Red" />
                                            <FooterStyle ForeColor="Red" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Purging</br> Weight" Visible="false">
                                            <FooterTemplate>
                                                <asp:Label ID="gvlblpurgAmt" runat="server" Text="" Font-Bold="True"
                                                    Font-Size="10px" ForeColor="Blue" Width="70px" Style="text-align: right"></asp:Label>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:TextBox ID="gvtxtpurgAmt" runat="server" BorderStyle="None"
                                                    Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "biqty3")).ToString("#,##0.0000;(#,##0.0000); ") %>' Width="70px" BackColor="Transparent"></asp:TextBox>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" ForeColor="Red" />
                                            <HeaderStyle ForeColor="Red" />
                                            <FooterStyle ForeColor="Red" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Manpower">
                                            <FooterTemplate>
                                                <asp:Label ID="gvlblTmanqty" runat="server" Text="" Font-Bold="True"
                                                    Font-Size="10px" ForeColor="Blue" Width="70px" Style="text-align: right"></asp:Label>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:TextBox ID="gvtxtmanqty" runat="server" BorderStyle="None"
                                                    Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "manqty")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="70px" BackColor="Transparent"></asp:TextBox>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" ForeColor="Red" />
                                            <HeaderStyle HorizontalAlign="Right" ForeColor="Red" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Shift" Visible="false">
                                            <FooterTemplate>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblShift" runat="server" Font-Size="X-Small"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "shift")) %>'
                                                    Width="120px" Style="text-align: left"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="left" />
                                        </asp:TemplateField>--%>
                                        <%--<asp:TemplateField HeaderText="Status" Visible="false">
                                            <FooterTemplate>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lbllstatus" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pstatus")) %>'
                                                    Width="200px" Style="text-align: left"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="left" />
                                        </asp:TemplateField>--%>
                                        <%--<asp:TemplateField HeaderText="Status">
                                            <FooterTemplate>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lbllstatusdesc" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pstatusdesc")) %>'
                                                    Width="100px" Style="text-align: left"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="left" />
                                        </asp:TemplateField>--%>

                                        <asp:TemplateField HeaderText="Rate">
                                            <FooterTemplate>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:TextBox ID="gvtxtRat" runat="server" BorderStyle="None" ReadOnly="true"
                                                    Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "prorate")).ToString("#,##0.000000;(#,##0.000000); ") %>'
                                                    Width="70px" BackColor="Transparent"></asp:TextBox>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="right" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Amount">
                                            <FooterTemplate>
                                                <asp:Label ID="gvlblTotalAmt" runat="server" Text="Label" Font-Bold="True"
                                                    Font-Size="10px" ForeColor="Blue" Width="70px" Style="text-align: right"></asp:Label>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:TextBox ID="gvtxtAmt" runat="server" BorderStyle="None" ReadOnly="true"
                                                    Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "PROAMT")).ToString("#,##0.0000;(#,##0.0000); ") %>'
                                                    Width="70px" BackColor="Transparent"></asp:TextBox>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="right" />
                                        </asp:TemplateField>
                                        <%--<asp:TemplateField HeaderText="Unit Weight" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="gvlblUnitWeught" runat="server" BorderStyle="None"
                                                    Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "unitweight")).ToString("#,##0.000000;(#,##0.000000); ") %>' Width="70px" BackColor="Transparent"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="right" />
                                        </asp:TemplateField>--%>

                                    </Columns>
                                    <FooterStyle CssClass="grvFooter" />
                                    <EditRowStyle />
                                    <AlternatingRowStyle />
                                    <PagerStyle CssClass="gvPagination" />
                                    <HeaderStyle CssClass="grvHeader" />
                                </asp:GridView>



                                <asp:Panel ID="pnlHpro" runat="server" Visible="False" BorderColor="Indigo" BorderWidth="2px" BorderStyle="Solid" GroupingText="Hourly Production">
                                    <asp:GridView ID="gvHourlyProd" runat="server" AutoGenerateColumns="False"
                                        ShowFooter="True" CssClass="table-striped table-hover table-bordered grvContentarea">
                                        <RowStyle />
                                        <Columns>
                                            <asp:TemplateField HeaderText="Sl">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvDSlNo" runat="server" Font-Bold="True" Style="text-align: right"
                                                        Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>



                                            <asp:TemplateField HeaderText="Name">
                                                <HeaderTemplate>
                                                    <table>

                                                        <tr>

                                                            <th class="pull-right">
                                                                <asp:HyperLink ID="hlbtnRdataExel" runat="server" BackColor="#000066" ToolTip="Export Excel"
                                                                    BorderColor="White" BorderStyle="Solid" BorderWidth="1px" Font-Bold="True"
                                                                    ForeColor="White" Style="text-align: center; margin-left: 10px;" Width="80px">Name</asp:HyperLink>
                                                            </th>
                                                        </tr>
                                                    </table>
                                                </HeaderTemplate>
                                                <ItemTemplate>

                                                    <asp:Label ID="lgvcustdesc" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "prodesc"))  %>' Width="180px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Status">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblpstatus" runat="server" Font-Bold="True" Style="text-align: left"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pstatusdesc")) %>' Width="60px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterStyle HorizontalAlign="Right" Font-Bold="true" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Date">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblTotalRecv" runat="server" Font-Bold="True" Style="text-align: left"
                                                        Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "prodate")).ToString("dd-MMM-yyyy") %>' Width="75px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lbltotal" runat="server">Total</asp:Label>
                                                </FooterTemplate>
                                                <FooterStyle HorizontalAlign="Right" Font-Bold="true" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Total">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblTotalRecv" runat="server" Font-Bold="True" Style="text-align: right"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "totalrecv")).ToString("#,##0;(#,##0); ") %>' Width="80px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lblTotalFRecv" runat="server" Font-Bold="True" Font-Size="12px"
                                                        Style="text-align: right" Width="80px"></asp:Label>
                                                </FooterTemplate>
                                                <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>


                                            <asp:TemplateField HeaderText="">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblrpr01" runat="server" Text='<%#  Convert.ToDouble(DataBinder.Eval(Container.DataItem, "t1")).ToString("#,##0;(#,##0); ") %>' Width="65px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lblrpFr01" runat="server" Font-Bold="True" Font-Size="12px"
                                                        Style="text-align: right" Width="65px"></asp:Label>
                                                </FooterTemplate>
                                                <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblrpr02" runat="server" Text='<%#  Convert.ToDouble(DataBinder.Eval(Container.DataItem, "t2")).ToString("#,##0;(#,##0); ") %>' Width="65px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lblrpFr02" runat="server" Font-Bold="True" Font-Size="12px"
                                                        Style="text-align: right" Width="65px"></asp:Label>
                                                </FooterTemplate>
                                                <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblrpr03" runat="server" Text='<%#  Convert.ToDouble(DataBinder.Eval(Container.DataItem, "t3")).ToString("#,##0;(#,##0); ") %>' Width="65px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lblrpFr03" runat="server" Font-Bold="True" Font-Size="12px"
                                                        Style="text-align: right" Width="65px"></asp:Label>
                                                </FooterTemplate>
                                                <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblrpr04" runat="server" Text='<%#  Convert.ToDouble(DataBinder.Eval(Container.DataItem, "t4")).ToString("#,##0;(#,##0); ") %>' Width="65px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lblrpFr04" runat="server" Font-Bold="True" Font-Size="12px"
                                                        Style="text-align: right" Width="65px"></asp:Label>
                                                </FooterTemplate>
                                                <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblrpr05" runat="server" Text='<%#  Convert.ToDouble(DataBinder.Eval(Container.DataItem, "t5")).ToString("#,##0;(#,##0); ") %>' Width="65px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lblrpFr05" runat="server" Font-Bold="True" Font-Size="12px"
                                                        Style="text-align: right" Width="65px"></asp:Label>
                                                </FooterTemplate>
                                                <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblrpr06" runat="server" Text='<%#  Convert.ToDouble(DataBinder.Eval(Container.DataItem, "t6")).ToString("#,##0;(#,##0); ") %>' Width="65px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lblrpFr06" runat="server" Font-Bold="True" Font-Size="12px"
                                                        Style="text-align: right" Width="65px"></asp:Label>
                                                </FooterTemplate>
                                                <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblrpr07" runat="server" Text='<%#  Convert.ToDouble(DataBinder.Eval(Container.DataItem, "t7")).ToString("#,##0;(#,##0); ") %>' Width="65px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lblrpFr07" runat="server" Font-Bold="True" Font-Size="12px"
                                                        Style="text-align: right" Width="65px"></asp:Label>
                                                </FooterTemplate>
                                                <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblrpr08" runat="server" Text='<%#  Convert.ToDouble(DataBinder.Eval(Container.DataItem, "t8")).ToString("#,##0;(#,##0); ") %>' Width="65px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lblrpFr08" runat="server" Font-Bold="True" Font-Size="12px"
                                                        Style="text-align: right" Width="65px"></asp:Label>
                                                </FooterTemplate>
                                                <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>


                                            <asp:TemplateField HeaderText="">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblrpr09" runat="server" Text='<%#  Convert.ToDouble(DataBinder.Eval(Container.DataItem, "t9")).ToString("#,##0;(#,##0); ") %>' Width="65px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lblrpFr09" runat="server" Font-Bold="True" Font-Size="12px"
                                                        Style="text-align: right" Width="65px"></asp:Label>
                                                </FooterTemplate>
                                                <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblrpr10" runat="server" Text='<%#  Convert.ToDouble(DataBinder.Eval(Container.DataItem, "t10")).ToString("#,##0;(#,##0); ") %>' Width="65px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lblrpFr10" runat="server" Font-Bold="True" Font-Size="12px"
                                                        Style="text-align: right" Width="65px"></asp:Label>
                                                </FooterTemplate>
                                                <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblrpr11" runat="server" Text='<%#  Convert.ToDouble(DataBinder.Eval(Container.DataItem, "t11")).ToString("#,##0;(#,##0); ") %>' Width="65px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lblrpFr11" runat="server" Font-Bold="True" Font-Size="12px"
                                                        Style="text-align: right" Width="65px"></asp:Label>
                                                </FooterTemplate>
                                                <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblrpr12" runat="server" Text='<%#  Convert.ToDouble(DataBinder.Eval(Container.DataItem, "t12")).ToString("#,##0;(#,##0); ") %>' Width="65px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lblrpFr12" runat="server" Font-Bold="True" Font-Size="12px"
                                                        Style="text-align: right" Width="65px"></asp:Label>
                                                </FooterTemplate>
                                                <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblrpr13" runat="server" Text='<%#  Convert.ToDouble(DataBinder.Eval(Container.DataItem, "t13")).ToString("#,##0;(#,##0); ") %>' Width="65px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lblrpFr13" runat="server" Font-Bold="True" Font-Size="12px"
                                                        Style="text-align: right" Width="65px"></asp:Label>
                                                </FooterTemplate>
                                                <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblrpr14" runat="server" Text='<%#  Convert.ToDouble(DataBinder.Eval(Container.DataItem, "t14")).ToString("#,##0;(#,##0); ") %>' Width="65px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lblrpFr14" runat="server" Font-Bold="True" Font-Size="12px"
                                                        Style="text-align: right" Width="65px"></asp:Label>
                                                </FooterTemplate>
                                                <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblrpr15" runat="server" Text='<%#  Convert.ToDouble(DataBinder.Eval(Container.DataItem, "t15")).ToString("#,##0;(#,##0); ") %>' Width="65px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lblrpFr15" runat="server" Font-Bold="True" Font-Size="12px"
                                                        Style="text-align: right" Width="65px"></asp:Label>
                                                </FooterTemplate>
                                                <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblrpr16" runat="server" Text='<%#  Convert.ToDouble(DataBinder.Eval(Container.DataItem, "t16")).ToString("#,##0;(#,##0); ") %>' Width="65px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lblrpFr16" runat="server" Font-Bold="True" Font-Size="12px"
                                                        Style="text-align: right" Width="65px"></asp:Label>
                                                </FooterTemplate>
                                                <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblrpr17" runat="server" Text='<%#  Convert.ToDouble(DataBinder.Eval(Container.DataItem, "t17")).ToString("#,##0;(#,##0); ") %>' Width="65px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lblrpFr17" runat="server" Font-Bold="True" Font-Size="12px"
                                                        Style="text-align: right" Width="65px"></asp:Label>
                                                </FooterTemplate>
                                                <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblrpr18" runat="server" Text='<%#  Convert.ToDouble(DataBinder.Eval(Container.DataItem, "t18")).ToString("#,##0;(#,##0); ") %>' Width="65px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lblrpFr18" runat="server" Font-Bold="True" Font-Size="12px"
                                                        Style="text-align: right" Width="65px"></asp:Label>
                                                </FooterTemplate>
                                                <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblrpr19" runat="server" Text='<%#  Convert.ToDouble(DataBinder.Eval(Container.DataItem, "t19")).ToString("#,##0;(#,##0); ") %>' Width="65px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lblrpFr19" runat="server" Font-Bold="True" Font-Size="12px"
                                                        Style="text-align: right" Width="65px"></asp:Label>
                                                </FooterTemplate>
                                                <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblrpr20" runat="server" Text='<%#  Convert.ToDouble(DataBinder.Eval(Container.DataItem, "t20")).ToString("#,##0;(#,##0); ") %>' Width="65px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lblrpFr20" runat="server" Font-Bold="True" Font-Size="12px"
                                                        Style="text-align: right" Width="65px"></asp:Label>
                                                </FooterTemplate>
                                                <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblrpr21" runat="server" Text='<%#  Convert.ToDouble(DataBinder.Eval(Container.DataItem, "t21")).ToString("#,##0;(#,##0); ") %>' Width="65px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lblrpFr21" runat="server" Font-Bold="True" Font-Size="12px"
                                                        Style="text-align: right" Width="65px"></asp:Label>
                                                </FooterTemplate>
                                                <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblrpr22" runat="server" Text='<%#  Convert.ToDouble(DataBinder.Eval(Container.DataItem, "t22")).ToString("#,##0;(#,##0); ") %>' Width="65px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lblrpFr22" runat="server" Font-Bold="True" Font-Size="12px"
                                                        Style="text-align: right" Width="65px"></asp:Label>
                                                </FooterTemplate>
                                                <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblrpr23" runat="server" Text='<%#  Convert.ToDouble(DataBinder.Eval(Container.DataItem, "t23")).ToString("#,##0;(#,##0); ") %>' Width="65px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lblrpFr23" runat="server" Font-Bold="True" Font-Size="12px"
                                                        Style="text-align: right" Width="65px"></asp:Label>
                                                </FooterTemplate>
                                                <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblrpr24" runat="server" Text='<%#  Convert.ToDouble(DataBinder.Eval(Container.DataItem, "t24")).ToString("#,##0;(#,##0); ") %>' Width="65px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lblrpFr24" runat="server" Font-Bold="True" Font-Size="12px"
                                                        Style="text-align: right" Width="65px"></asp:Label>
                                                </FooterTemplate>
                                                <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>


                                        </Columns>
                                        <EditRowStyle />
                                        <AlternatingRowStyle />
                                        <PagerStyle CssClass="gvPagination" />
                                        <HeaderStyle CssClass="grvHeader" />
                                        <FooterStyle CssClass="grvFooter" />
                                    </asp:GridView>
                                </asp:Panel>


                                <asp:Panel ID="PnlProcess" runat="server" Style="display: none;" Visible="False" BorderColor="Indigo" BorderWidth="2px" BorderStyle="None" GroupingText="Raw Material">
                                    <asp:GridView ID="dgv1" runat="server" AutoGenerateColumns="False" Width="500px"
                                        CssClass="table-striped table-hover table-bordered grvContentarea" OnRowDataBound="dgv1_RowDataBound" ShowFooter="True">
                                        <Columns>
                                            <asp:TemplateField HeaderStyle-Font-Bold="true" HeaderStyle-Font-Size="12px"
                                                HeaderStyle-ForeColor="#333333" HeaderText="SL" ItemStyle-Font-Size="12px">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvsl" runat="server"
                                                        Text='<%# Convert.ToInt32(Container.DataItemIndex+1).ToString("00")+"." %>'
                                                        Width="12px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle Font-Bold="True" Font-Size="12px" ForeColor="#333333" />
                                                <ItemStyle Font-Size="12px" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderStyle-Font-Bold="true" HeaderStyle-Font-Size="12px" Visible="false"
                                                HeaderStyle-ForeColor="#333333" HeaderText="Code" ItemStyle-Font-Size="12px">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvcode" runat="server"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rescod")) %>'
                                                        Width="80px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle Font-Bold="True" Font-Size="12px" ForeColor="#333333" />
                                                <ItemStyle Font-Size="12px" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderStyle-Font-Bold="true" HeaderStyle-Font-Size="10px"
                                                HeaderStyle-ForeColor="#333333" HeaderText="Description"
                                                ItemStyle-Font-Size="10px" ItemStyle-HorizontalAlign="Left">
                                                <FooterTemplate>
                                                    <asp:LinkButton ID="lnkCalculat" runat="server" Font-Bold="True"
                                                        Font-Underline="true"></asp:LinkButton>
                                                </FooterTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvcodeDescription" runat="server"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "resdesc")) %>'
                                                        Width="200px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle Font-Bold="True" Font-Size="10px" ForeColor="#333333" />
                                                <ItemStyle Font-Size="10px" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderStyle-Font-Bold="false" HeaderStyle-Font-Size="10px"
                                                HeaderStyle-ForeColor="#333333" HeaderText="Unit" ItemStyle-Font-Size="10px">
                                                <FooterTemplate>
                                                    <asp:Label ID="lblfgvrate" runat="server" Font-Bold="true" Font-Size="12px"></asp:Label>
                                                </FooterTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvunit" runat="server"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "unite")) %>'
                                                        Width="70px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle Font-Bold="True" Font-Size="10px" ForeColor="#333333" />
                                                <ItemStyle Font-Size="10px" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderStyle-Font-Bold="false" HeaderStyle-Font-Size="12px"
                                                HeaderStyle-ForeColor="#333333" HeaderText="Qty" ItemStyle-Font-Size="12px"
                                                ItemStyle-HorizontalAlign="Right">
                                                <FooterTemplate>
                                                    <asp:Label ID="lblfgvrate2" runat="server" Font-Size="12px"></asp:Label>
                                                </FooterTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvqty" runat="server"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "issqty")).ToString("#,##0.0000;(#,##0.0000); ") %>'
                                                        Width="80px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle Font-Bold="True" Font-Size="12px" ForeColor="#333333" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderStyle-Font-Bold="true" HeaderStyle-Font-Size="12px"
                                                HeaderStyle-ForeColor="#333333" HeaderText="Rate" ItemStyle-Font-Size="12px"
                                                ItemStyle-HorizontalAlign="Right">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvstdpri" runat="server"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "rate")).ToString("#,##0.000000;(#,##0.000000); ") %>'
                                                        Width="80px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lblfprocost" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#FF9966" Width="60px"></asp:Label>
                                                </FooterTemplate>
                                                <HeaderStyle Font-Bold="True" Font-Size="12px" ForeColor="#333333" />
                                                <ItemStyle Font-Size="12px" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Amount" ItemStyle-HorizontalAlign="Right">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvamt" runat="server" Font-Size="12px"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "issamt")).ToString("#,##0.0000;(#,##0.0000); ")%>'
                                                        Width="80px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lblgvfamt" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#CC0066"></asp:Label>
                                                </FooterTemplate>
                                                <HeaderStyle Font-Bold="True" Font-Size="12px" ForeColor="#333333" />
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
                        </div>
                    </div>
                </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>





