<%@ Page Title="" Language="C#" MasterPageFile="~/SPE.Master" AutoEventWireup="true" CodeBehind="ExportSample.aspx.cs" Inherits="SPEWEB.F_19_EXP.ExportSample" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">


    <script type="text/javascript" language="javascript">

        $(document).ready(function () {

            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);

        });
        function openModal() {
            $('#myModal').modal('toggle');
        }
        function CLoseMOdal() {
            $('#myModal').modal('hide');

        }
        function pageLoaded() {


            $('.chzn-select').chosen({ search_contains: true });

        }

    </script>


    <style>
        .moduleItemWrpper {
            overflow: initial !important;
        }
    </style>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="card card-fluid">
                <div class="card-body">
                    <div class="row">
                        <div class="col-md-10 col-sm-10 col-lg-10 ">
                            <div class="form-group">
                                <asp:RadioButtonList ID="RadioButtonList1" CssClass="small checkbox" runat="server" AutoPostBack="true" RepeatDirection="Horizontal">
                                    <asp:ListItem Value="0" Selected="True">Commercial Invoice</asp:ListItem>
                                    <asp:ListItem Value="1" style="display:none">Forwarding Letter</asp:ListItem>
                                    <asp:ListItem Value="2">Packing List</asp:ListItem>
                                    <asp:ListItem Value="3" style="display:none">Bill of Exchange</asp:ListItem>
                                    <asp:ListItem Value="4" style="display:none">GSP Format</asp:ListItem>
                                    <asp:ListItem Value="5" style="display:none">Beneficiary's Declaration</asp:ListItem>
                                    <asp:ListItem Value="6" style="display:none">Beneficiary's Certificate</asp:ListItem>

                                </asp:RadioButtonList>
                            </div>
                        </div>
                        <div class="col-md-2">
                            <div class="form-group">
                                <asp:LinkButton ID="imgPreVious" runat="server" CssClass="control-label" OnClick="imgPreVious_Click">Previous List</asp:LinkButton>
                                <asp:DropDownList ID="ddlPrevList" runat="server" CssClass="form-control form-control-sm" TabIndex="2">
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-1 col-sm-1 col-lg-1 ">
                            <div class="form-group">
                                <asp:Label ID="lblinvdate" runat="server" CssClass="label">Inv Date</asp:Label>
                                <asp:TextBox ID="txtdate" runat="server" CssClass="form-control form-control-sm small "></asp:TextBox>
                                <cc1:CalendarExtender ID="txtdate_CalendarExtender" runat="server" Format="dd-MMM-yyyy" TargetControlID="txtdate"></cc1:CalendarExtender>
                            </div>
                        </div>
                        <div class="col-md-1 col-sm-1 col-lg-1 ">
                            <div class="form-group">
                                <asp:Label ID="trnsferNo" runat="server" CssClass="label small">Inv ID</asp:Label>
                                <asp:Label ID="lblCurNo1" runat="server" Text="INV00-" ReadOnly="True" CssClass=" smLbl_to small"></asp:Label>
                                <asp:TextBox ID="txtCurNo2" runat="server" ReadOnly="True" CssClass="form-control form-control-sm ">00000</asp:TextBox>

                            </div>
                        </div>
                        <div class="col-md-1 col-sm-1 col-lg-1 ">
                            <div class="form-group">
                                <asp:Label ID="Label18" runat="server" CssClass="label">Invoice No</asp:Label>
                                <asp:TextBox ID="txtRefNo" runat="server" CssClass="form-control form-control-sm "></asp:TextBox>

                            </div>
                        </div>
                        <div class="col-md-2 col-sm-2 col-lg-2 ">
                            <div class="form-group">
                                <asp:Label ID="Label2" runat="server" CssClass="label">Mode of Transport</asp:Label>
                                <asp:DropDownList ID="DDLShipmentType" runat="server" CssClass="chzn-select form-control form-control-sm" AutoPostBack="true" TabIndex="2"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-1 col-sm-1 col-lg-1 ">
                            <div class="form-group">
                                   <asp:Label ID="Label10" runat="server" CssClass="label">Agent Name</asp:Label>
                                <asp:DropDownList ID="DdlAgent" runat="server" OnSelectedIndexChanged="DdlAgent_SelectedIndexChanged" CssClass="chzn-select form-control form-control-sm" AutoPostBack="true" TabIndex="2"></asp:DropDownList>
                      
                          </div>
                        </div>
                        <div class="col-md-1 col-sm-1 col-lg-1 ">
                            <div class="form-group" style="margin-top: 20px;">
                                <asp:LinkButton ID="lbtnOk" runat="server" CssClass="btn btn-primary btn-sm pull-left" OnClick="lbtnOk_Click">Ok</asp:LinkButton>
                            </div>
                        </div>
                          <div class="col-md-1 col-sm-1 col-lg-1 ">
                                <div class="form-group">                                    
                                        <asp:Label ID="lblUser1" runat="server" CssClass="control-label">User Name</asp:Label>
                                        <asp:TextBox ID="txtUserSearch1"  runat="server" style="display:none;" CssClass=" inputTxt inputName inpPixedWidth"></asp:TextBox>
                                     <asp:DropDownList ID="ddlUserList" runat="server" CssClass="form-control inputTxt pull-left chzn-select" AutoPostBack="true" TabIndex="2">
                                        </asp:DropDownList>
                                    </div>
                           </div>
                        <div class="col-md-1 col-sm-1 col-lg-1 ">
                            <div class="form-group">
                                <asp:Label ID="lblorqty" runat="server" CssClass="label">Order Qty:</asp:Label>
                                <asp:TextBox ID="txtordqty" runat="server" CssClass="inputTxt inputName inpPixedWidth" Width="70px"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-1 col-sm-1 col-lg-1 ">
                            <div class="form-group">
                                <asp:Label ID="lblreqty" runat="server" CssClass="label">Rem. Qty:</asp:Label>
                                <asp:TextBox ID="txtexpqty" runat="server" CssClass="inputTxt inputName inpPixedWidth" Width="70px"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-2 col-sm-2 col-lg-2 ">
                            <div class="form-group" style="margin-top: 20px;">
                                <asp:LinkButton ID="LbtnShowAll" runat="server" CssClass="btn btn-primary btn-sm pull-left" OnClick="LbtnShowAll_Click">Order Details</asp:LinkButton>
                                <asp:LinkButton ID="RefBtn" runat="server" CssClass="btn btn-primary btn-sm pull-left" OnClick="RefBtn_Click">Refresh</asp:LinkButton>

                            </div>
                        </div>

                    </div>

                    <div class="row" id="addsec" runat="server" visible="false">
                         <div class="col-md-2 col-sm-2 col-lg-2 ">
                            <div class="form-group">
                           <asp:Label ID="Label3" runat="server" CssClass="label">Buyer Name</asp:Label>
                                <asp:DropDownList ID="ddlBuyer" runat="server" OnSelectedIndexChanged="ddlBuyer_SelectedIndexChanged" CssClass="chzn-select form-control form-control-sm" AutoPostBack="true" TabIndex="2"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-2 col-sm-2 col-lg-2 ">
                            <div class="form-group">
                                <asp:Label ID="Label4" runat="server" CssClass="label">Master L/C</asp:Label>
                                <asp:DropDownList ID="ddlmlccode" runat="server" OnSelectedIndexChanged="ddlmlccode_SelectedIndexChanged" CssClass="chzn-select form-control form-control-sm" AutoPostBack="true" TabIndex="2"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-2 col-sm-2 col-lg-2">
                            <div class="form-group">
                                <asp:Label ID="Label16" runat="server" CssClass="label">Order Type</asp:Label>
                                <asp:DropDownList ID="dllorderType" runat="server" OnSelectedIndexChanged="dllorderType_SelectedIndexChanged" CssClass="chzn-select form-control form-control-sm" AutoPostBack="true" TabIndex="2"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-2 col-sm-2 col-lg-2 ">
                            <div class="form-group">
                                <asp:Label ID="Label17" runat="server" CssClass="label">Style Name</asp:Label>
                                <asp:DropDownList ID="ddlprocode" runat="server" OnSelectedIndexChanged="ddlprocode_SelectedIndexChanged" CssClass="chzn-select form-control form-control-sm" AutoPostBack="true" TabIndex="2"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-4 col-sm-4 col-lg-4 ">
                            <div class="form-group" style="margin-top: 20px;">
                                <asp:LinkButton ID="Add" runat="server" CssClass="btn btn-primary btn-sm pull-left" OnClick="Add_Click">Add</asp:LinkButton>
                                <asp:LinkButton ID="AddAll" runat="server" CssClass="btn btn-primary btn-sm pull-left" OnClick="AddAll_Click">Add All</asp:LinkButton>

                            </div>
                        </div>



                    </div>
                </div>
            </div>

            <div class="card card-fluid" >
                <div class="card-body" >
                    <div class="row" style="min-height:300px;">
                        <asp:GridView ID="gvSalCon" runat="server" ShowFooter="true" OnRowDeleting="gvSalCon_RowDeleting" AutoGenerateColumns="False" CssClass=" table-striped table-hover table-bordered grvContentarea">
                            <Columns>
                                <asp:CommandField ShowDeleteButton="True" ControlStyle-ForeColor="Red" DeleteText="<span class='fa fa-trash'></span>" />
                                <asp:TemplateField HeaderText="Sl">
                                    <ItemTemplate>
                                        <asp:Label ID="serialnoid" runat="server" Style="text-align: right" Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>'
                                            Width="15px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Font-Bold="True" Font-Size="12px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Order">
                                    <ItemTemplate>
                                        <asp:Label ID="lbgvMlcdesc" runat="server" BorderColor="#99CCFF" BorderStyle="Solid" Font-Size="10px"
                                            BorderWidth="0px" Style="text-align: Left; background-color: Transparent" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mlcdesc")) %>'
                                            Width="120px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Font-Bold="True" Font-Size="12px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Style">
                                    <ItemTemplate>
                                        <asp:Label ID="lbgvstyle" runat="server" BorderColor="#99CCFF" BorderStyle="Solid" Font-Size="10px"
                                            BorderWidth="0px" Style="text-align: Left; background-color: Transparent" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "styledesc")) %>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Font-Bold="True" Font-Size="12px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Order No">
                                    <ItemTemplate>
                                        <asp:Label ID="txtgvArtno" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                            BorderWidth="0px" Font-Size="11px" Style="text-align: right; background-color: transparent"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "ordrno")) %>'
                                            Width="60px" placeholder="Limit (12 Digit)"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Color">
                                    <ItemTemplate>
                                        <asp:Label ID="lbgvColor" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                            BorderWidth="0px" Style="text-align: Left; background-color: Transparent" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "colordesc")) %>'
                                            Width="50px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="txtFTotal" runat="server" ForeColor="Black" Text="Total"></asp:Label>
                                    </FooterTemplate>
                                    <HeaderStyle Font-Bold="True" Font-Size="12px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Size">
                                    <ItemTemplate>
                                        <asp:Label ID="lbgvsize" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                            BorderWidth="0px" Style="text-align: Left; background-color: Transparent" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sizedesc")) %>'
                                            Width="40px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Font-Bold="True" Font-Size="12px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Order Qty" Visible="false">
                                    <FooterTemplate>
                                        <asp:Label ID="txtFNetTotal" runat="server" ForeColor="Black"></asp:Label>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="txtgvordrqty" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                            BorderWidth="0px" Font-Size="11px" Style="text-align: right; background-color: Transparent"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ordrqty")).ToString("#,##0;(#,##0); ") %>'
                                            Width="60px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="right" VerticalAlign="Middle" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Prod Qty">
                                    <FooterTemplate>
                                        <asp:Label ID="txtPrdTotal" runat="server" ForeColor="Black"></asp:Label>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="txtgvprdqty" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                            BorderWidth="0px" Font-Size="11px" Style="text-align: right; background-color: Transparent"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "prdqty")).ToString("#,##0;(#,##0); ") %>'
                                            Width="50px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="right" VerticalAlign="Middle" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Bal Qty">
                                    <FooterTemplate>
                                        <asp:Label ID="txtbalqtyTotal" runat="server" ForeColor="Black"></asp:Label>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="txtgvbalqty" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                            BorderWidth="0px" Font-Size="11px" Style="text-align: right; background-color: Transparent"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "balqty")).ToString("#,##0;(#,##0); ") %>'
                                            Width="50px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="right" VerticalAlign="Middle" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="PRS/CTN">
                                    <%--<FooterTemplate>
                                            <asp:Label ID="txtFpperctnqty" runat="server" ForeColor="Black"></asp:Label>
                                        </FooterTemplate>--%>
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtgvpperctnqty" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                            BorderWidth="1px" Font-Size="11px" Style="text-align: right; background-color: Transparent"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "pperctnqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="50px"></asp:TextBox>
                                    </ItemTemplate>
                                    <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="right" VerticalAlign="Middle" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Total <br> PRS">
                                    <FooterTemplate>
                                        <asp:Label ID="txtFtotlprs" runat="server" ForeColor="Black"></asp:Label>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtgvtotlprs" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                            BorderWidth="1px" Font-Size="11px" Style="text-align: right; background-color: Transparent"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "totlprs")).ToString("#,##0;(#,##0); ") %>'
                                            Width="50px"></asp:TextBox>
                                    </ItemTemplate>
                                    <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="right" VerticalAlign="Middle" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Total <br> CTNS">
                                    <FooterTemplate>
                                        <asp:Label ID="txtFtotlctn" runat="server" ForeColor="Black"></asp:Label>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtgvtotlctn" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                            BorderWidth="0px" Font-Size="11px" Style="text-align: right; background-color: Transparent"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "totlctn")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="50px"></asp:TextBox>
                                    </ItemTemplate>
                                    <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="right" VerticalAlign="Middle" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="G.W/ CTNS">
                                    <FooterTemplate>
                                        <asp:Label ID="txtFgwperctn" runat="server" ForeColor="Black"></asp:Label>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtgvgwperctn" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                            BorderWidth="1px" Font-Size="11px" Style="text-align: right; background-color: Transparent"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "gwperctn")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="60px"></asp:TextBox>
                                    </ItemTemplate>
                                    <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="right" VerticalAlign="Middle" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Total G.W">
                                    <FooterTemplate>
                                        <asp:Label ID="txtFtotalgw" runat="server" ForeColor="Black"></asp:Label>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtgvtotalgw" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                            BorderWidth="0px" Font-Size="11px" Style="text-align: right; background-color: Transparent"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "totalgw")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="60px"></asp:TextBox>
                                    </ItemTemplate>
                                    <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="right" VerticalAlign="Middle" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="NetW/ <br> CTN">
                                    <FooterTemplate>
                                        <asp:Label ID="txtFnwperctn" runat="server" ForeColor="Black"></asp:Label>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtnwperctn" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                            BorderWidth="1px" Font-Size="11px" Style="text-align: right; background-color: Transparent"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "nwperctn")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="50px"></asp:TextBox>
                                    </ItemTemplate>
                                    <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="right" VerticalAlign="Middle" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Total N.W">
                                    <FooterTemplate>
                                        <asp:Label ID="txtFtotalNw" runat="server" ForeColor="Black"></asp:Label>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lbltotlnw" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                            BorderWidth="0px" Font-Size="11px" Style="text-align: right; background-color: Transparent"
                                            Text='<%# (Convert.ToDouble(DataBinder.Eval(Container.DataItem, "totlctn"))*Convert.ToDouble(DataBinder.Eval(Container.DataItem, "nwperctn"))).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="60px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="right" VerticalAlign="Middle" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Dimension <br>of CTNS">

                                    <ItemTemplate>
                                        <asp:TextBox ID="txtdimenctn" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                            BorderWidth="1px" Font-Size="11px" Style="text-align: right; background-color: Transparent"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "dimenctn")) %>'
                                            Width="80px"></asp:TextBox>
                                    </ItemTemplate>
                                    <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="right" VerticalAlign="Middle" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="CBM">
                                    <FooterTemplate>
                                        <asp:Label ID="txtFcbm" runat="server" ForeColor="Black"></asp:Label>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtcbm" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                            BorderWidth="1px" Font-Size="11px" Style="text-align: right; background-color: Transparent"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "cbm")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="50px"></asp:TextBox>
                                    </ItemTemplate>
                                    <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="right" VerticalAlign="Middle" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Rate">

                                    <ItemTemplate>
                                        <asp:TextBox ID="lblRate" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                            BorderWidth="1px" Font-Size="11px" Style="text-align: right; background-color: Transparent"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "rate")).ToString("#,##0.0000;(#,##0.0000); ") %>'
                                            Width="50px"></asp:TextBox>
                                    </ItemTemplate>
                                    <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="right" VerticalAlign="Middle" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="FC </br>Amount">
                                    <FooterTemplate>
                                        <asp:Label ID="txtFAmt" runat="server" ForeColor="Black"></asp:Label>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblAmt" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                            BorderWidth="0px" Font-Size="11px" Style="text-align: right; background-color: Transparent"
                                            Text='<%# (Convert.ToDouble(DataBinder.Eval(Container.DataItem, "rate"))*Convert.ToDouble(DataBinder.Eval(Container.DataItem, "totlprs"))).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="60px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="right" VerticalAlign="Middle" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>




                            </Columns>

                            <AlternatingRowStyle />
                            <FooterStyle CssClass="grvFooter" />
                            <PagerStyle CssClass="gvPagination" />
                            <HeaderStyle CssClass="grvHeader" />
                        </asp:GridView>

                    </div>
          
                    <asp:Panel ID="Panel2" runat="server" Visible="False">
                        <div class="row" >

                            <div class="col-md-7 col-sm-7 col-lg-7 ">
                                <div class="row">
                                    <div class="col-md-4 col-sm-4 col-lg-4 ">
                                        <div class="form-group">
                                            <asp:Label ID="lblReqNarr" runat="server" CssClass="label">Narration</asp:Label>
                                            <asp:TextBox ID="txtBillNarr" runat="server" CssClass="form-control form-control-sm " TextMode="MultiLine" cols="20" Rows="4"></asp:TextBox>

                                        </div>
                                    </div>
                                    <div class="col-md-3 col-sm-3 col-lg-3 ">
                                        <div class="form-group">
                                            <asp:Label ID="Label1" runat="server" CssClass="label">Country</asp:Label>
                                            <asp:DropDownList ID="ddlCountry" runat="server" CssClass="chzn-select form-control form-control-sm" TabIndex="2"></asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-md-2 col-sm-2 col-lg-2">
                                        <div class="form-group">
                                            <asp:Label ID="Label19" runat="server" CssClass="label small">Port of Loading</asp:Label>
                                            <asp:DropDownList ID="ddlPortLoad" runat="server" CssClass="chzn-select form-control form-control-sm" TabIndex="2"></asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-md-3 col-sm-3 col-lg-3 ">
                                        <div class="form-group">
                                            <asp:Label ID="Label20" runat="server" CssClass="label small">Port of Discharge</asp:Label>
                                            <asp:DropDownList ID="ddlPortDis" runat="server" CssClass="chzn-select form-control form-control-sm" TabIndex="2"></asp:DropDownList>
                                        </div>
                                    </div>


                                </div>

                                <div class="row">
                                    <div class="col-md-2 col-sm-2 col-lg-2 ">
                                        <div class="form-group">
                                            <asp:Label ID="Label7" runat="server" CssClass="label">Mode</asp:Label>
                                            <asp:DropDownList ID="ddlDelMode" runat="server" CssClass="chzn-select form-control form-control-sm" TabIndex="2"></asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-md-2 col-sm-2 col-lg-2 ">
                                        <div class="form-group">
                                            <asp:Label ID="Label5" runat="server" CssClass="label">Date</asp:Label>
                                            <asp:TextBox ID="txtMDate" runat="server" CssClass="form-control form-control-sm small"></asp:TextBox>
                                            <cc1:CalendarExtender ID="txtMDate_CalendarExtender1" runat="server" Format="dd-MMM-yyyy" TargetControlID="txtMDate"></cc1:CalendarExtender>
                                        </div>
                                    </div>
                                    <div class="col-md-2 col-sm-2 col-lg-2 ">
                                        <div class="form-group">
                                            <asp:Label ID="Label13" runat="server" CssClass="label">Ex-Fact Date</asp:Label>
                                            <asp:TextBox ID="txtExFact" runat="server" CssClass="form-control form-control-sm "></asp:TextBox>
                                            <cc1:CalendarExtender ID="txtExFact_CalendarExtender" runat="server" Format="dd-MMM-yyyy" TargetControlID="txtExFact"></cc1:CalendarExtender>
                                        </div>
                                    </div>
                                    <div class="col-md-2 col-sm-2 col-lg-2 ">
                                        <div class="form-group">
                                            <asp:Label ID="Label11" runat="server" CssClass="label">Exp No</asp:Label>
                                            <asp:TextBox ID="txtExpNo" runat="server" CssClass="form-control form-control-sm "></asp:TextBox>

                                        </div>
                                    </div>
                                    <div class="col-md-2 col-sm-2 col-lg-2 ">
                                        <div class="form-group">
                                            <asp:Label ID="Label14" runat="server" CssClass="label">Exp Date</asp:Label>
                                            <asp:TextBox ID="txtExpDate" runat="server" CssClass="form-control form-control-sm "></asp:TextBox>
                                            <cc1:CalendarExtender ID="txtExpDate_CalendarExtender1" runat="server" Format="dd-MMM-yyyy" TargetControlID="txtExpDate"></cc1:CalendarExtender>
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <div class="col-md-3 col-sm-3 col-lg-3 ">
                                <div class="row">

                                    <div class="col-md-12 col-sm-12 col-lg-12 ">
                                        <div class="row">
                                            <div class="col-md-5 col-sm-5 col-lg-5 ">
                                                <div class="form-group">
                                                    <asp:Label ID="Label8" runat="server" CssClass="label">Less Discount %</asp:Label>


                                                </div>
                                            </div>
                                            <div class="col-md-2 col-sm-2 col-lg-2 ">
                                                <div class="form-group">
                                                    <asp:TextBox ID="txtDisPer" runat="server" CssClass="form-control form-control-sm ">0</asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-md-5 col-sm-5 col-lg-5 ">
                                                <div class="form-group">
                                                    <asp:TextBox ID="txtDis" runat="server" Style="text-align: right" CssClass="form-control form-control-sm "></asp:TextBox>
                                                </div>
                                            </div>

                                        </div>
                                    </div>
                                    <div class="col-md-12 col-sm-12 col-lg-12 ">
                                        <div class="row">
                                            <div class="col-md-7 col-sm-7 col-lg-7 ">
                                                <div class="form-group">
                                                    <asp:Label ID="Label12" runat="server" CssClass="label">Grand Total</asp:Label>


                                                </div>
                                            </div>
                                            <div class="col-md-5 col-sm-5 col-lg-5 ">
                                                <div class="form-group">
                                                    <asp:Label ID="Label9" runat="server" CssClass="label"></asp:Label>
                                                    <asp:TextBox ID="txtGTotal" runat="server" Style="text-align: right" CssClass="form-control form-control-sm "></asp:TextBox>


                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>

                        </div>

                    </asp:Panel>


                </div>
            </div>




            <div class="col-md-3  pading5px" style="display: none;">
                <asp:Label ID="Label6" runat="server" CssClass="lblTxt lblName" Text=" Shipment:"></asp:Label>

                <asp:DropDownList ID="DDLShipment" runat="server" CssClass=" inputTxt chzn-select" Style="width: 180px;"></asp:DropDownList>


            </div>
            <div class="form-group" id="export" runat="server" visible="false">
            </div>

            <div id="myModal" class="modal animated slideInLeft" role="dialog">
                <div class="modal-dialog ">
                    <div class="modal-content  ">
                        <div class="modal-header">
                            <button type="button" style="background-color: red;" class="close btn btn-xs" data-dismiss="modal"><span class="fa fa-close"></span></button>
                            <h4 class="modal-title">
                                <span class="fa fa-table"></span>Style Wise Order Details </h4>
                        </div>
                        <div class="modal-body">

                            <div class="row">
                                <div class="col-md-12">
                                    <asp:GridView ID="gvsizes" runat="server" AutoGenerateColumns="False" Height="1px" PageSize="15"
                                        ShowFooter="True" Width="253px" CssClass=" table-striped table-hover table-bordered grvContentarea" Font-Size="11px">

                                        <Columns>
                                            <asp:TemplateField HeaderText="Style">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvStyleID" runat="server" Style="text-transform: capitalize" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "styledesc")) %>'
                                                        Width="60px"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Color">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvColorID" runat="server" Style="text-transform: capitalize" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "colordesc")) %>'
                                                        Width="60px"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>


                                            <asp:TemplateField HeaderText="Size-01" Visible="False">
                                                <ItemTemplate>
                                                    <asp:Label ID="txtgvF1" runat="server" BackColor="Transparent"
                                                        Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s1")).ToString("###0;(###0); ") %>'
                                                        Width="40px"></asp:Label>
                                                </ItemTemplate>

                                                <FooterStyle HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Size-02" Visible="False">
                                                <ItemTemplate>
                                                    <asp:Label ID="txtgvF2" runat="server" Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s2")).ToString("###0;(###0); ") %>'
                                                        Width="40px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterStyle HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Size-03" Visible="False">
                                                <ItemTemplate>
                                                    <asp:Label ID="txtgvF3" runat="server"
                                                        Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s3")).ToString("###0;(###0); ") %>'
                                                        Width="40px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterStyle HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Size-04" Visible="False">
                                                <ItemTemplate>
                                                    <asp:Label ID="txtgvF4" runat="server"
                                                        Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s4")).ToString("###0;(###0); ") %>'
                                                        Width="40px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterStyle HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Size-05" Visible="False">
                                                <ItemTemplate>
                                                    <asp:Label ID="txtgvF5" runat="server"
                                                        Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s5")).ToString("###0;(###0); ") %>'
                                                        Width="40px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterStyle HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Size-06" Visible="False">
                                                <ItemTemplate>
                                                    <asp:Label ID="txtgvF6" runat="server"
                                                        Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s6")).ToString("###0;(###0); ") %>'
                                                        Width="40px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterStyle HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Size-07" Visible="False">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtgvF7" runat="server" BackColor="Transparent" BorderColor="#6acc39"
                                                        BorderStyle="Solid" BorderWidth="1px"
                                                        Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s7")).ToString("###0;(###0); ") %>'
                                                        Width="40px"></asp:TextBox>
                                                </ItemTemplate>
                                                <FooterStyle HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Size-08" Visible="False">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtgvF8" runat="server" BackColor="Transparent" BorderColor="#6acc39"
                                                        BorderStyle="Solid" BorderWidth="1px"
                                                        Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s8")).ToString("###0;(###0); ") %>'
                                                        Width="40px"></asp:TextBox>
                                                </ItemTemplate>
                                                <FooterStyle HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Size-09" Visible="False">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtgvF9" runat="server" BackColor="Transparent" BorderColor="#6acc39"
                                                        BorderStyle="Solid" BorderWidth="1px"
                                                        Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s9")).ToString("###0;(###0); ") %>'
                                                        Width="40px"></asp:TextBox>
                                                </ItemTemplate>
                                                <FooterStyle HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Size-10" Visible="False">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtgvF10" runat="server" BackColor="Transparent" BorderColor="#6acc39"
                                                        BorderStyle="Solid" BorderWidth="1px"
                                                        Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s10")).ToString("###0;(###0); ") %>'
                                                        Width="40px"></asp:TextBox>
                                                </ItemTemplate>
                                                <FooterStyle HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Size-11" Visible="False">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtgvF11" runat="server" BackColor="Transparent" BorderColor="#6acc39"
                                                        BorderStyle="Solid" BorderWidth="1px"
                                                        Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s11")).ToString("###0;(###0); ") %>'
                                                        Width="40px"></asp:TextBox>
                                                </ItemTemplate>
                                                <FooterStyle HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Size-12" Visible="False">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtgvF12" runat="server" BackColor="Transparent" BorderColor="#6acc39"
                                                        BorderStyle="Solid" BorderWidth="1px"
                                                        Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s12")).ToString("###0;(###0); ") %>'
                                                        Width="40px"></asp:TextBox>
                                                </ItemTemplate>
                                                <FooterStyle HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Size-13" Visible="False">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtgvF13" runat="server" BackColor="Transparent" BorderColor="#6acc39"
                                                        BorderStyle="Solid" BorderWidth="1px"
                                                        Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s13")).ToString("###0;(###0); ") %>'
                                                        Width="40px"></asp:TextBox>
                                                </ItemTemplate>
                                                <FooterStyle HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Size-14" Visible="False">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtgvF14" runat="server" BackColor="Transparent" BorderColor="#6acc39"
                                                        BorderStyle="Solid" BorderWidth="1px"
                                                        Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s14")).ToString("###0;(###0); ") %>'
                                                        Width="40px"></asp:TextBox>
                                                </ItemTemplate>
                                                <FooterStyle HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Size-15" Visible="False">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtgvF15" runat="server" BackColor="Transparent" BorderColor="#6acc39"
                                                        BorderStyle="Solid" BorderWidth="1px"
                                                        Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s15")).ToString("###0;(###0); ") %>'
                                                        Width="40px"></asp:TextBox>
                                                </ItemTemplate>
                                                <FooterStyle HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Total">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvTotal1" runat="server" Style="font-size: 11px; text-align: right"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "totalqty")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="40px"></asp:Label>

                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="FLblgvTotal" runat="server"></asp:Label>
                                                </FooterTemplate>
                                                <FooterStyle HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <%--  <asp:TemplateField HeaderText="Trial Order QTY">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvColTotal1" Font-Bold="true" runat="server" Style="font-size: 11px; text-align: right"
                                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "colqty")).ToString("#,##0;(#,##0); ") %>'
                                                                        Width="80px"></asp:Label>

                                                                </ItemTemplate>
                                                                   <FooterTemplate>
                                                                    <asp:Label ID="FLblgvColTotal" Font-Bold="true" runat="server"></asp:Label>
                                                                </FooterTemplate>
                                                                <FooterStyle HorizontalAlign="Right" />
                                                                <HeaderStyle HorizontalAlign="Center" />
                                                            </asp:TemplateField>
                                            --%>
                                        </Columns>
                                        <FooterStyle CssClass="grvFooter" />
                                        <EditRowStyle />
                                        <AlternatingRowStyle />
                                        <PagerStyle CssClass="gvPagination" />
                                        <HeaderStyle CssClass="grvHeader" />
                                    </asp:GridView>
                                </div>
                            </div>

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
