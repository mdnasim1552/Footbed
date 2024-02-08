<%@ Page Title="" Language="C#" MasterPageFile="~/SPE.Master" AutoEventWireup="true" CodeBehind="ImportApproval.aspx.cs" Inherits="SPEWEB.F_10_Procur.ImportApproval" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script language="javascript" type="text/javascript">
        $(document).ready(function () {
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);
        });

        function pageLoaded() {

            $('.chzn-select').chosen({ search_contains: true });
        }

        function SpcfChangModal() {
            $('#SpecificationModal').modal('toggle');
        }

        function CLoseMOdal() {
            $('#SpecificationModal').modal('hide');

        }

        function LoadRdlcVIewer(comcod, orderno, supcode, msrno, reqno) {
            window.open(`../F_10_Procur/PuchasePrint?Type=OrderSavePdf&comcod=${comcod}&orderno=${orderno}&supcode=${supcode}&msrno=${msrno}&reqno=${reqno}&dayid=${orderno}`, '_blank');
        }

    </script>
    <style>
        .chzn-single {
            border-radius: 3px !important;
            height: 29px !important;
        }

        .lcpo .form-group {
            margin-bottom: 0.3rem;
        }
    </style>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" Visible="True">
        <ContentTemplate>
            <div class="card card-fluid">
                <div class="card-body">
                    <div class="row">
                        <div class="col-md-1 ">
                            <asp:Label ID="Label1" runat="server" CssClass="label">Date</asp:Label>
                            <asp:TextBox ID="txtDatefrom" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
                            <cc1:CalendarExtender ID="datefrom" runat="server" Format="dd-MMM-yyyy" TargetControlID="txtDatefrom"></cc1:CalendarExtender>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <asp:Label ID="Label3" runat="server" CssClass=" label">Select Supplier</asp:Label>
                                <asp:DropDownList ID="ddlsupplier" CssClass="form-control form-control-sm chzn-select" runat="server"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-1">
                            <div class="form-group">
                                <asp:LinkButton ID="lbtnOk" runat="server" Text="Ok" OnClick="lbtnOk_Click" Style="margin-top: 20px;" CssClass="btn btn-primary btn-sm" TabIndex="4"></asp:LinkButton>
                            </div>
                        </div>
                        <div class="col-md-7">
                            <asp:Panel ID="panel2" CssClass="row" runat="server" Visible="false">
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <asp:Label ID="Label2" runat="server" CssClass="label">PO No</asp:Label>
                                        <asp:Label ID="lblSyspo" Style="display: none" runat="server" CssClass="label"></asp:Label>

                                        <asp:TextBox ID="txtPoNo" Enabled="false" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>

                                    </div>
                                </div>
                                <div class="col-md-7">
                                    <div class="form-group">

                                        <asp:Label ID="Label6" runat="server" CssClass="label">Name of the Customer</asp:Label>
                                        <asp:TextBox ID="nameCust" runat="server" ReadOnly="true" CssClass="form-control form-control-sm"></asp:TextBox>
                                    </div>
                                </div>

                                <div class="col-md-3">
                                    <div class="form-group">
                                        <asp:Label ID="Label7" runat="server" CssClass="label">Production Start Date</asp:Label>
                                        <asp:TextBox ID="proStartDate" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
                                        <cc1:CalendarExtender ID="startDate" runat="server" Format="dd-MMM-yyyy" TargetControlID="proStartDate"></cc1:CalendarExtender>
                                    </div>
                                </div>
                            </asp:Panel>
                        </div>

                    </div>
                </div>
            </div>

            <div class="row">
                <div class="col-md-8">
                    <div class="card card-fluid">
                        <div class="card-body" style="min-height: 450px;">
                            <div class="row">
                                    <div class="table-responsive">
                                        <div class="row" style="max-height: 360px"">
                                    <asp:GridView ID="gvsurveyinfo" runat="server" AutoGenerateColumns="False" CssClass=" table-striped table-hover table-bordered grvContentarea"
                                        ShowFooter="True" OnRowDataBound="gvsurveyinfo_RowDataBound">
                                        <RowStyle />
                                        <Columns>
                                            <asp:TemplateField HeaderText="Sl">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvSlNo0" runat="server" Font-Bold="True"
                                                        Style="text-align: right" ToolTip="Click for Details Input"
                                                        Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Material Code">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvbuyer" runat="server" Font-Size="9px" AutoCompleteType="Disabled"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "matcode")) %>'
                                                        Width="90px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="MSR NO" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvItmCodc" runat="server" AutoCompleteType="Disabled"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rsircode")) %>'
                                                        Width="130px"></asp:Label>
                                                    <asp:Label ID="lblgvinqno" runat="server" AutoCompleteType="Disabled"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "msrno")) %>'
                                                        Width="130px"></asp:Label>

                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Description <br> of Goods">

                                                <ItemTemplate>
                                                    <asp:Label ID="txtgvStyle" runat="server" Font-Size="9px"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rsirdesc")) %>'
                                                        Width="140px"></asp:Label>
                                                </ItemTemplate>

                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Specification">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lblgArtno" Font-Size="9px" runat="server" OnClick="lblgvSpfDesc10_Click"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "spcfdesc")) %>'
                                                        Width="120px"></asp:LinkButton>
                                                    <asp:Label ID="lblSpcfcod" runat="server" Visible="false"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "spcfcod")) %>'
                                                        Width="120px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Unit">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvunitcode" runat="server" Font-Size="9px" Visible="false"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "unitcode")) %>'
                                                        Width="90px"></asp:Label>
                                                    <asp:Label ID="lblgvUnit" runat="server" Font-Size="9px"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "unit")) %>'
                                                        Width="40px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Quantity">
                                                <ItemTemplate>
                                                    <asp:Label ID="txtgvQtyc" Enabled="false" runat="server" BackColor="Transparent"
                                                        Style="text-align: right" Font-Size="9px"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "reqqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                        Width="50px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                <ItemStyle HorizontalAlign="Right" />
                                                <FooterStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Con Unit">
                                                <ItemTemplate>
                                                    <asp:DropDownList Width="80px" ID="ddlConUnit" CssClass="form-control form-control-sm " runat="server">
                                                    </asp:DropDownList>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Con Qty">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtgvconreqqty" runat="server" BackColor="Transparent"
                                                        Style="text-align: right" Font-Size="9px" BorderStyle="None"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "conunitqty")).ToString("#,##0.00000;(#,##0.00000); ") %>'
                                                        Width="40px"></asp:TextBox>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="SQM Rate" Visible="false">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtgvSQM"  runat="server" BackColor="Transparent"
                                                        Style="text-align: right" Font-Size="9px" BorderStyle="None"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "sqmrate")).ToString("#,##0.00000;(#,##0.00000); ") %>'
                                                        Width="40px"></asp:TextBox>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Price">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtgvPrice" runat="server" BackColor="Transparent"
                                                        Style="text-align: right" Font-Size="9px" CssClass="bg-twitter" BorderStyle="None"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "resrate")).ToString("#,##0.00000;(#,##0.00000); ") %>'
                                                        Width="40px"></asp:TextBox>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>
                                            
                                            <asp:TemplateField HeaderText="Total <br> Price">
                                                <FooterTemplate>
                                                    <asp:Label ID="lblgvFConfirmPrice" runat="server" Width="60px" ForeColor="Black"></asp:Label>
                                                </FooterTemplate>
                                                <FooterStyle Font-Bold="True" Font-Size="9px" HorizontalAlign="Right" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvConfirmPrice" runat="server" Style="text-align: right" Font-Size="11px"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "reqamt")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                        Width="60px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                <ItemStyle HorizontalAlign="Right" />

                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Selection">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="TxtSelection" runat="server"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "selection")) %>'
                                                        Width="80px"></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Crust/Finished">
                                                <ItemTemplate>
                                                    <asp:DropDownList ID="DdlCstFinished" CssClass="form-control form-control-sm " runat="server">
                                                        <asp:ListItem Value="Crust">Crust</asp:ListItem>
                                                        <asp:ListItem Value="Finished">Finished</asp:ListItem>
                                                        <asp:ListItem Value="">None</asp:ListItem>
                                                    </asp:DropDownList>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Remarks">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvCurrency" runat="server"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "msrrmrk")) %>'
                                                        Width="80px"></asp:Label>
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
                                <div class="col-md-12">
                                    <div class="log-divider" id="lblcharging" runat="server" visible="false">
                                        <span>
                                            <i class="fa fa-fw fa-dollar-sign"></i>Charging(Addition/Deduction) Details Information</span>
                                    </div>

                                    <asp:GridView ID="gvcharging" runat="server" AutoGenerateColumns="False" CssClass=" table-striped table-hover table-bordered grvContentarea"
                                        ShowFooter="true">
                                        <RowStyle />
                                        <Columns>
                                            <asp:TemplateField HeaderText="Sl">
                                                <ItemTemplate>
                                                    <asp:Label ID="CHlblgvSlNo0" runat="server" Font-Bold="True"
                                                        Style="text-align: right" ToolTip="Click for Details Input"
                                                        Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            </asp:TemplateField>


                                            <asp:TemplateField HeaderText="Description of Charging">

                                                <ItemTemplate>
                                                    <asp:Label ID="ChtxtgvStyle" runat="server"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rsirdesc")) %>'
                                                        Width="620px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lbltotal" runat="server">Total</asp:Label>
                                                </FooterTemplate>
                                                <FooterStyle Font-Bold="true" HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Amount">

                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtChrAmount" runat="server" Style="text-align: right" Font-Size="11px" BorderStyle="Solid" BorderColor="Blue" BorderWidth="1"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amount")).ToString("#,##0.00;-#,##0.00; ") %>'
                                                        Width="80px"></asp:TextBox>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lblFchamt" runat="server" Style="text-align: right;"></asp:Label>
                                                </FooterTemplate>
                                                <FooterStyle Font-Bold="true" HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                <ItemStyle HorizontalAlign="Right" />

                                            </asp:TemplateField>

                                        </Columns>

                                        <EditRowStyle />
                                        <AlternatingRowStyle />
                                        <FooterStyle CssClass="grvFooter" />
                                        <PagerStyle CssClass="gvPagination" />
                                        <HeaderStyle CssClass="grvHeader" />

                                    </asp:GridView>
                                </div>




                                <div class="col-md-12">
                                    <div class="log-divider" id="TermsConditions" runat="server" visible="false">
                                        <span>
                                            <i class="fa fa-fw fa-comment-alt"></i>Terms And Conditions</span>
                                    </div>


                                    <div class="table-responsive">
                                        <asp:GridView ID="gvOrderTerms" runat="server" AllowPaging="True"
                                            AutoGenerateColumns="False" PageSize="15" ShowFooter="true"
                                            CssClass=" table-striped table-hover table-bordered grvContentarea">
                                            <PagerSettings NextPageText="Next" PreviousPageText="Previous" Position="Top"
                                                Mode="NumericFirstLast" />
                                            <Columns>
                                                <asp:TemplateField HeaderText="Sl">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvSlNo0" runat="server"
                                                            Style="text-align: right"
                                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="10px"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Terms ID" Visible="False">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvTermsID" runat="server" Height="16px"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "slno")) %>'
                                                            Width="80px"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Subject" Visible="False">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtgvSubject" runat="server" BorderColor="#99CCFF"
                                                            BorderStyle="Solid" BorderWidth="0px" Font-Size="9px"
                                                            Style="text-align: left; background-color: Transparent"
                                                            Text='<%# DataBinder.Eval(Container.DataItem, "termstitle").ToString() %>'
                                                            Width="120px"></asp:TextBox>
                                                    </ItemTemplate>
                                                    <FooterStyle HorizontalAlign="Center" />
                                                    <HeaderStyle HorizontalAlign="Left" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText=" ">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvColon" runat="server" Font-Bold="true" Font-Size="10px"
                                                            Text=" : "></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterStyle HorizontalAlign="Center" />
                                                    <HeaderStyle HorizontalAlign="Left" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Description">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtgvDesc" runat="server" BorderColor="#99CCFF"
                                                            BorderStyle="Solid" BorderWidth="0px" Font-Size="9px"
                                                            Style="text-align: left; background-color: Transparent"
                                                            Text='<%# DataBinder.Eval(Container.DataItem, "termsdetails").ToString() %>'
                                                            Width="620px"></asp:TextBox>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Left" />
                                                </asp:TemplateField>
                                                <%--  <asp:TemplateField HeaderText="Remarks">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtgvRemarks" runat="server" BorderColor="#99CCFF"
                                                                    BorderStyle="Solid" BorderWidth="0px" Font-Size="11px"
                                                                    Style="text-align: left; background-color: Transparent"
                                                                    Text='<%# DataBinder.Eval(Container.DataItem, "termsdetails").ToString() %>'
                                                                    Width="100px"></asp:TextBox>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Left" />
                                                        </asp:TemplateField>--%>
                                                <asp:TemplateField HeaderText="">
                                                    <HeaderTemplate>
                                                        <asp:LinkButton ID="lbAddMore" runat="server" CommandArgument="lbAddMore"
                                                            OnClick="AddMore_Click" Width="30px" CssClass="text-info"><i class="fa fa-plus"></i></asp:LinkButton>
                                                    </HeaderTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                </asp:TemplateField>
                                            </Columns>
                                            <FooterStyle BackColor="#F5F5F5" />
                                            <EditRowStyle />
                                            <AlternatingRowStyle />
                                            <PagerStyle CssClass="gvPagination" />
                                            <HeaderStyle CssClass="grvHeader" />
                                        </asp:GridView>

                                    </div>
                                </div>
                            </div>


            </div>
            </div>
                </div>

                <!---------------second column----------------------------->
                <div class="col-md-4 lcpo">
                    <div class="card card-fluid">
                        <div class="card-body" style="min-height: 700px;">
                            <asp:Panel ID="panel1" runat="server" Visible="false">
                                <div class="row">

                                    <div class="col-md-7">
                                        <div class="form-group">
                                            <asp:Label ID="Label4" runat="server" CssClass="label">Name of Vendor:</asp:Label>
                                            <asp:DropDownList ID="ddlVenCode" runat="server" CssClass="form-control form-control-sm chzn-select">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <asp:LinkButton ID="btnCurr" runat="server" CssClass="label" Text="Currency:"></asp:LinkButton>
                                            <div class="input-group input-group-sm input-group-alt">
                                                <asp:DropDownList ID="ddlCurrency" CssClass="form-control form-control-sm" runat="server" AutoPostBack="True"></asp:DropDownList>

                                                <div class="input-group-append">

                                                    <asp:HyperLink ID="HyperLink1" runat="server" CssClass="input-group-text text-success" ToolTip="Create List" Target="_blank"
                                                        NavigateUrl="~/F_34_Mgt/AccConversion"><span class="fa fa-plus"></span></asp:HyperLink>


                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <asp:CheckBox ID="CheckBox_Summary" runat="server" CssClass="ml-4 chk-summary" Text="Summary" />
                                        </div>
                                    </div>

                                    <div class="col-md-9">
                                        <div class="form-group">
                                            <asp:Label ID="Label22" runat="server" CssClass="label">PO Reference</asp:Label>
                                            <asp:TextBox ID="txtPoRef" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
                                        </div>
                                    </div>

                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <asp:Label ID="Label26" runat="server" CssClass="smLbl_to">Print Type</asp:Label>
                                            <asp:DropDownList ID="ddlReportLevel" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlReportLevel_SelectedIndexChanged" CssClass="chzn-select form-control form-control-sm">
                                                <asp:ListItem Value="1">Leather</asp:ListItem>
                                                <asp:ListItem Value="2">Accessories</asp:ListItem>
                                                <asp:ListItem Value="3">Outsole</asp:ListItem>
                                                <asp:ListItem Value="4">Master Carton</asp:ListItem>
                                                <asp:ListItem Value="5">Double Unit</asp:ListItem>
                                                <asp:ListItem Selected="True" Value="4">None</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>

                                    <div class="col-md-12" style="display: none;">
                                        <div class="form-group">
                                            <asp:Label ID="Label9" runat="server" CssClass="label">Beneficiary's Address:</asp:Label>
                                            <asp:TextBox ID="address" runat="server" CssClass="form-control form-control-sm" ReadOnly="true"></asp:TextBox>
                                        </div>
                                    </div>

                                    <div class="col-md-4" style="display: none;">
                                        <div class="form-group">
                                            <asp:Label ID="Label5" runat="server" CssClass="label">New Vendor?</asp:Label>
                                            <br />
                                            <asp:CheckBox ID="checkboxven" Checked="true" CssClass="checkbox" Enabled="false" runat="server" />
                                        </div>
                                    </div>

                                    <div class="col-md-4 col-sm-4" style="display: none;">
                                        <div class="form-group">

                                            <asp:Label ID="Label19" runat="server" CssClass="label">Payment Terms:</asp:Label>
                                            <br />
                                            <asp:CheckBox ID="payterm" Checked="true" CssClass="checkbox" Enabled="false" runat="server" />
                                        </div>
                                    </div>

                                    <div class="col-md-4" style="display: none;">
                                        <div class="form-group">
                                            <asp:Label ID="Label20" runat="server" CssClass="label">Delivery Terms:</asp:Label>
                                            <br />
                                            <asp:CheckBox ID="delterm" Checked="true" CssClass="checkbox" Enabled="false" runat="server" />
                                        </div>
                                    </div>

                                    <div class="col-md-4" style="display: none;">
                                        <div class="form-group">
                                            <asp:Label ID="Label21" runat="server" CssClass="smLbl_to">Delivery Mode:</asp:Label>
                                            <br />
                                            <asp:CheckBox ID="delmod" Checked="true" CssClass="checkbox" Enabled="false" runat="server" />
                                        </div>
                                    </div>

                                    <div class="col-md-4">
                                        <div class="form-group">
                                            <asp:Label ID="Label8" runat="server" CssClass="label small"> Inco/Shiping Terms:</asp:Label>
                                            <asp:DropDownList ID="DDLIncoTerms" runat="server" CssClass="form-control form-control-sm chzn-select">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                      
                                    <div class="col-md-4">
                                        <div class="form-group">
                                            <asp:Label ID="Label27" runat="server" CssClass="label small">Expected Delivery on:</asp:Label>
                                            <asp:TextBox ID="TextBox1" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
                                            <cc1:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd-MMM-yyyy" TargetControlID="expectedDateOfDelivery"></cc1:CalendarExtender>
                                        </div>
                                    </div>
                                    <div class="col-md-4">
                                        <div class="form-group">
                                            <asp:Label ID="Label12" runat="server" CssClass="label small">Year of Incotemrs:</asp:Label>
                                            <asp:TextBox ID="yearOfIncotemrs" runat="server" CssClass="form-control form-control-sm "></asp:TextBox>
                                        </div>
                                    </div>
                                    
                                    <div class="col-md-4">
                                        <div class="form-group">
                                            <asp:Label ID="Label14" runat="server" CssClass="label small">Expected Delivery on:</asp:Label>
                                            <asp:TextBox ID="expectedDateOfDelivery" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
                                            <cc1:CalendarExtender ID="deliveryDate" runat="server" Format="dd-MMM-yyyy" TargetControlID="expectedDateOfDelivery"></cc1:CalendarExtender>
                                        </div>
                                    </div>

                                    <div class="col-md-4" style="display: none;">
                                        <div class="form-group">
                                            <asp:Label ID="Label13" runat="server" CssClass="label small">Delivery Lead Time:</asp:Label>
                                            <asp:TextBox ID="deliveryLeadTime" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
                                            <cc1:CalendarExtender ID="leadTime" runat="server" Format="dd-MMM-yyyy" TargetControlID="deliveryLeadTime"></cc1:CalendarExtender>
                                        </div>
                                    </div>

                                    <div class="col-md-4">
                                        <div class="form-group">
                                            <asp:Label ID="Label15" runat="server" CssClass="label small">Expected Arrival on:</asp:Label>
                                            <asp:TextBox ID="expectedDateOfArrival" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
                                            <cc1:CalendarExtender ID="arrivalDate" runat="server" Format="dd-MMM-yyyy" TargetControlID="expectedDateOfArrival"></cc1:CalendarExtender>
                                        </div>  
                                    </div>
                                    <div class="col-md-4" style="margin-top:20px">
                                        <div class="form-group">
                                            <asp:CheckBox ID="checkAmt" runat="server" CssClass="ml-4 chk-summary" Text="With Amount" Visible="false"/>
                                        </div>
                                    </div>

                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <asp:Label ID="Label10" runat="server" CssClass="label">Port of Loading:</asp:Label>
                                            <asp:DropDownList ID="portOfLoading" runat="server" CssClass="form-control form-control-sm chzn-select">
                                            </asp:DropDownList>
                                        </div>
                                    </div>

                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <asp:Label ID="Label11" runat="server" CssClass="smLbl_to">Port of Discharge:</asp:Label>
                                            <asp:DropDownList ID="portOfDischarge" runat="server" CssClass="form-control form-control-sm chzn-select">
                                            </asp:DropDownList>
                                        </div>
                                    </div>

                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <asp:Label ID="Label17" runat="server" CssClass="label">L/C Opening Bank:</asp:Label>
                                            <asp:DropDownList ID="lcOpeningBank" runat="server" CssClass="form-control form-control-sm chzn-select"></asp:DropDownList>
                                        </div>
                                    </div>

                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <asp:Label ID="Label18" runat="server" CssClass="label">Swift Code:</asp:Label>
                                            <asp:DropDownList ID="swiftCode" runat="server" CssClass="form-control form-control-sm"></asp:DropDownList>
                                        </div>
                                    </div>

                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <asp:Label ID="Label16" runat="server" CssClass="label">Mode of Payment:</asp:Label>
                                            <asp:DropDownList ID="DdlModeOfPayment" runat="server" CssClass="form-control form-control-sm chzn-select"></asp:DropDownList>
                                        </div>
                                    </div>

                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <asp:Label ID="Label24" runat="server" CssClass="label">Shipping Mode</asp:Label>
                                            <asp:DropDownList ID="ddlShipMode" runat="server" CssClass="form-control form-control-sm chzn-select"></asp:DropDownList>
                                        </div>
                                    </div>

                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <asp:Label ID="LblImpRequistion" Visible="false" runat="server">Select Requisition</asp:Label>
                                            <div class="input-group input-group-sm input-group-alt">
                                                <asp:DropDownList ID="DdlImportRequistion" Visible="false" runat="server" CssClass="form-control chzn-select"></asp:DropDownList>
                                                <div class="input-group-append">
                                                    <asp:LinkButton ID="btnMerge" Visible="false" runat="server" OnClientClick="return confirm('Do you want to push item in this Purchase Order')" Text="Push Requistion" OnClick="btnMerge_Click" CssClass="input-group-text text-success " TabIndex="1"><span class="fa fa-link"></span></asp:LinkButton>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-md-12">
                                        <div class="form-group">
                                            <asp:Label ID="Label25" runat="server" CssClass="label">Special Note</asp:Label>
                                            <asp:TextBox ID="txtSpNote" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
                                        </div>
                                    </div>

                                    <div class="col-md-12">
                                        <div class="form-group">
                                            <asp:Label ID="Label23" runat="server" CssClass="label">Narration</asp:Label>
                                            <asp:TextBox ID="txtNar" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
                                        </div>
                                    </div>

                                    <div class="col-md-12 mt-2">
                                        <div class="form-group">
                                            <asp:Button ID="btnSendMail" runat="server" CssClass="btn btn-sm btn-success" Text="Send Mail" OnClick="btnSendMail_Click" />
                                        </div>
                                    </div>

                                </div>
                            </asp:Panel>
                        </div>
                    </div>
                </div>
            </div>
            <!------------specification change model------------->
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
                            <asp:Label ID="lblHelper" runat="server" Visible="false"></asp:Label>
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



                        </div>
                        <div class="modal-footer ">
                            <asp:LinkButton ID="LbtnChngSpcf" runat="server" OnClick="LbtnChngSpcf_Click" CssClass="btn btn-sm btn-success" OnClientClick="CLoseMOdal();"><span class="glyphicon glyphicon-save"></span> Update </asp:LinkButton>

                            <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                        </div>
                    </div>
                </div>
            </div>

        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
