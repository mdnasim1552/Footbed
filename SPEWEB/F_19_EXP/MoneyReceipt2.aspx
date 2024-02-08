<%@ Page Title="" Language="C#" MasterPageFile="~/SPE.Master" AutoEventWireup="true" CodeBehind="MoneyReceipt2.aspx.cs" Inherits="SPEWEB.F_19_EXP.MoneyReceipt2" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">


    <script type="text/javascript" language="javascript">

        $(document).ready(function () {

            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);

        });

        function OpenAdvmodal() {

            $('#Advmodal').modal('show');
        }
        function CLoseMOdal() {

            $('#Advmodal').modal('hide');
        }

        function SelectAllCheckboxes(chk) {
            $('#<%=gvAdvDetails.ClientID %>').find("input:checkbox").each(function () {
                if ((this).disabled == false) {
                    if (this != chk) {
                        this.checked = chk.checked;
                    }
                }
            });
        }

        function pageLoaded() {


            $('.chzn-select').chosen({ search_contains: true });

        }

    </script>


    <style type="text/css">
        .chzn-single {
    border-radius: 3px !important;
    height: 29px !important;
    
}
       
   
        .moduleItemWrpper {
            overflow: initial !important;
        }

        .hrpro {
            padding: 0px;
            display: block;
            margin: 4px 0px;
            margin-left: auto;
            margin-right: auto;
            border-style: inset;
            border-width: 1px;
            border-color: #DFF0D8;
        }
    </style>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>


            <div class="card card-fluid">
                <div class="card-body">
                    <div class="row">

                        <div class="col-md-2 col-sm-2 col-lg-2 ">
                            <div class="form-group">
                                <asp:Label ID="Label5" runat="server" CssClass="label">Buyer Name</asp:Label>
                                <asp:DropDownList ID="ddlBuyer" runat="server" OnSelectedIndexChanged="ddlBuyer_SelectedIndexChanged" CssClass="chzn-select form-control form-control-sm" AutoPostBack="true" TabIndex="2"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-2 col-sm-2 col-lg-2 d-none">
                            <div class="form-group">
                                <asp:Label ID="Label7" runat="server" CssClass="label">Master L/C</asp:Label>
                                <asp:DropDownList ID="ddlmlccode" runat="server"  CssClass="chzn-select form-control form-control-sm" AutoPostBack="true" TabIndex="2"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-2 col-sm-2 col-lg-2">
                            <div class="form-group">
                                <asp:Label ID="Label8" runat="server" CssClass="label">Invoice No</asp:Label>
                                <asp:DropDownList ID="ddlInvno" runat="server" CssClass="chzn-select form-control form-control-sm"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-1 col-sm-1 col-lg-1 ">
                            <div class="form-group" style="margin-top: 20px;">
                                <asp:LinkButton ID="lbtnOk" runat="server" CssClass="btn btn-primary btn-sm pull-left" OnClick="lbtnOk_Click">Ok</asp:LinkButton>
                            </div>
                        </div>
                        <div class="col-md-1 col-sm-1 col-lg-1 ">
                            <div class="form-group">
                                <asp:Label ID="lblorqty" runat="server" CssClass="label">Advance</asp:Label>
                                <asp:Label ID="lblkbal" runat="server" CssClass="form-control form-control-sm bg-twitter"></asp:Label>
                            </div>
                        </div>
                        <div class="col-md-1 col-sm-1 col-lg-1 ">
                            <div class="form-group" style="margin-top: 20px;">
                                <asp:LinkButton ID="Add" runat="server" CssClass="btn btn-primary btn-sm pull-left" OnClick="Add_Click"><span class="fa fa-check"></span></asp:LinkButton>
                                <asp:LinkButton ID="AddAll" runat="server" CssClass="btn btn-primary btn-sm pull-left" OnClick="AddAll_Click"><span class="fa fa-check-double"></span></asp:LinkButton>

                            </div>
                        </div>

                          <div class="col-md-1 col-sm-1 col-lg-1 ">
                                <div class="form-group">
                                    <asp:Label ID="Label10" runat="server" CssClass="label">Currency</asp:Label>
                                    <asp:HyperLink ID="HyperLink1" runat="server" CssClass="btnok" ToolTip="Create List" Target="_blank"
                                        NavigateUrl="~/F_34_Mgt/AccConversion.aspx"><span class="glyphicon glyphicon-plus"></span></asp:HyperLink>
                                    <asp:DropDownList ID="ddlCurrency" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlCurrency_SelectedIndexChanged" CssClass="chzn-select form-control form-control-sm"></asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-md-1 col-sm-1 col-lg-1 ">
                                <div class="form-group">
                                    <asp:Label ID="Label11" runat="server" CssClass="label">Con. Rate</asp:Label>
                                    <asp:TextBox ID="lblConRate" runat="server" CssClass="form-control form-control-sm "></asp:TextBox>
                                </div>
                            </div>

                         <div class="col-md-1 col-sm-1 col-lg-1 ">
                                <div class="form-group">
                                    <asp:Label ID="Label9" runat="server" CssClass="label">Pay Type</asp:Label>
                                    <asp:DropDownList ID="ddlBatchGrp" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlBatchGrp_SelectedIndexChanged" CssClass="chzn-select form-control form-control-sm"></asp:DropDownList>
                                </div>
                            </div>

                    </div>

                    <asp:Panel ID="PanelCollection" runat="server" Visible="false">
                        <div class="row">
                            <div class="col-md-1 col-sm-1 col-lg-1 ">
                                <div class="form-group">

                                    <asp:Label ID="Label1" runat="server" CssClass="label">Date</asp:Label>
                                    <asp:TextBox ID="txtEntryDate" runat="server" CssClass="form-control form-control-sm small"></asp:TextBox>
                                    <cc1:CalendarExtender ID="txtEntryDate_CalendarExtender" runat="server" Format="dd-MMM-yyyy" TargetControlID="txtEntryDate"></cc1:CalendarExtender>
                                </div>
                            </div>
                            <div class="col-md-1 col-sm-1 col-lg-1 ">
                                <div class="form-group">
                                    <asp:Label ID="lblLcName" runat="server" CssClass="label">Auto Ref</asp:Label>
                                    <div class="form-inline">
                                        <asp:DropDownList ID="ddltype" Width="50%"  style="padding:0px !important" CssClass="form-control form-control-sm" Enabled="false" runat="server">
                                            <asp:ListItem Value="MR" Selected="True">MR</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:TextBox ID="txtissueno" runat="server" ReadOnly="True" Width="50%" CssClass="form-control form-control-sm"></asp:TextBox>
                                    </div>

                                </div>
                            </div>
                            <div class="col-md-1 col-sm-1 col-lg-1 ">
                                <div class="form-group">
                                    <asp:Label ID="Label18" runat="server" CssClass="label">Ref No</asp:Label>
                                    <asp:TextBox ID="txtothref" runat="server" CssClass="form-control form-control-sm "></asp:TextBox>

                                </div>
                            </div>
                           
                           <div class="col-md-1 col-sm-1 col-lg-1 ">
                                <div class="form-group">

                                    <asp:Label ID="Label13" runat="server" CssClass="label small">Realization Date</asp:Label>
                                    <asp:TextBox ID="txtChqDate" runat="server" CssClass="form-control form-control-sm small"></asp:TextBox>
                                    <cc1:CalendarExtender ID="txtChqDate_CalendarExtender2" runat="server" Format="dd-MMM-yyyy" TargetControlID="txtChqDate"></cc1:CalendarExtender>
                                </div>
                            </div>
                             <div class="col-md-4 col-sm-4 col-lg-4 ">
                                <div class="form-group">
                                    <asp:Label ID="Label14" runat="server" CssClass="label">Bank Name</asp:Label>
                                    <asp:DropDownList ID="ddlBankList" runat="server" OnSelectedIndexChanged="ddlBatchGrp_SelectedIndexChanged" CssClass="chzn-select form-control form-control-sm" AutoPostBack="true" TabIndex="2"></asp:DropDownList>
                                </div>
                            </div>
                               <div class="col-md-2 col-sm-2 col-lg-2 ">
                                <div class="form-group">
                                    <asp:Label ID="Label15" runat="server" CssClass="label">Realization No</asp:Label>
                                    <asp:TextBox ID="txtrefnum" runat="server" CssClass="form-control form-control-sm "></asp:TextBox>

                                </div>
                            </div>
                            <div class="col-md-2 col-sm-2 col-lg-2 ">
                                <div class="form-group">
                                    <asp:Label ID="Label16" runat="server" CssClass="label">Branch</asp:Label>
                                    <asp:TextBox ID="txtBranch" runat="server" CssClass="form-control form-control-sm "></asp:TextBox>

                                </div>
                            </div>

                        </div>

                      

                    </asp:Panel>


                </div>
            </div>

            <div class="card card-fluid">
                <div class="card-body" style="min-height:450px">
                    <div class="row">

                        <asp:GridView ID="gvTransaction" runat="server" AutoGenerateColumns="False" OnRowDeleting="gvTransaction_RowDeleting"
                            ShowFooter="True" CssClass="table-striped table-hover table-bordered grvContentarea pull-right" OnRowDataBound="gvTransaction_RowDataBound">
                            <RowStyle />
                            <Columns>
                                <asp:CommandField ShowDeleteButton="True" ControlStyle-ForeColor="Red" DeleteText="<span class='fa fa-trash'></span>"></asp:CommandField>
                                <asp:TemplateField HeaderText="Sl">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvDSlNo" runat="server" Font-Bold="True" Style="text-align: right"
                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="10px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Invoice</br> Date">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvvoudate" runat="server"
                                            Font-Size="10px"
                                            Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "voudat")).ToString("dd-MMM-yyyy")  %>'
                                            Width="70px"></asp:Label>

                                    </ItemTemplate>
                                    <FooterStyle HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Inv No">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvinvrefno" runat="server"
                                            Font-Size="10px"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "invrefno"))  %>'
                                            Width="90px"></asp:Label>

                                    </ItemTemplate>

                                    <FooterStyle HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Ref. No">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvbillno" runat="server"
                                            Font-Size="10px"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "isunum"))  %>'
                                            Width="80px"></asp:Label>

                                    </ItemTemplate>

                                    <FooterStyle HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Left" />
                                </asp:TemplateField>



                                <asp:TemplateField HeaderText="FC">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvcurdesc" runat="server" Font-Size="9px"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "curdesc")) %>'
                                            Width="20px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="FC <br>Rate">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvconvrate" runat="server" Font-Size="9px"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "convrate")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="30px"></asp:Label>
                                    </ItemTemplate>



                                    <HeaderStyle HorizontalAlign="Left" />
                                </asp:TemplateField> 

                                <asp:TemplateField HeaderText="Invoice<br> Amount<br> (FC)">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvbillamt" runat="server" BackColor="Transparent"
                                            BorderColor="Transparent" BorderStyle="None" BorderWidth="0px" Font-Size="10px"
                                            ForeColor="Black" Style="text-align: right" TabIndex="81"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "billam")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="80px"></asp:Label>
                                         <asp:Label ID="lblgvinvbdtamt" runat="server" BackColor="Transparent"
                                            BorderColor="Transparent" BorderStyle="None" BorderWidth="0px" Font-Size="10px"
                                            ForeColor="Black" Style="text-align: right" TabIndex="81"
                                            Text='<%# ("<i class=\"bold text-red\">&#2547;</i> ").ToString() +Convert.ToDouble(DataBinder.Eval(Container.DataItem, "invbdtamt")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="80px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lblgvFbillamt" runat="server" BackColor="Transparent"
                                            BorderColor="Transparent" BorderStyle="None"
                                            Font-Size="10px" ForeColor="#000" ReadOnly="True" Style="text-align: right"
                                            Width="70px"></asp:Label>
                                          <asp:Label ID="lblgvFinvbdtamt" runat="server" BackColor="Transparent"
                                            BorderColor="Transparent" BorderStyle="None"
                                            Font-Size="12px" ForeColor="#000" ReadOnly="True" Style="text-align: right"
                                            Width="70px"></asp:Label>
                                    </FooterTemplate>
                                    <FooterStyle ForeColor="White" HorizontalAlign="right" />
                                    <ItemStyle HorizontalAlign="right" Width="80px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Dues<br> (FC)">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvdues" runat="server" BackColor="Transparent"
                                            BorderColor="Transparent" BorderStyle="None" BorderWidth="0px" Font-Size="9px"
                                            ForeColor="Black" Style="text-align: right" TabIndex="81"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "hbalam")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="60px"></asp:Label>
                                    </ItemTemplate>

                                    <FooterStyle ForeColor="White" HorizontalAlign="right" />
                                    <ItemStyle HorizontalAlign="right" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Outstanding<br> Balance <br> (FC)">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvbalamt" runat="server" BackColor="Transparent"
                                            BorderColor="Transparent" BorderStyle="None" BorderWidth="0px" Font-Size="9px"
                                            ForeColor="Black" Style="text-align: right" TabIndex="81"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "balam")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lblgvFbalamt" runat="server" BackColor="Transparent"
                                            BorderColor="Transparent" BorderStyle="None"
                                            Font-Size="10px" ForeColor="#000" ReadOnly="True" Style="text-align: right"
                                            Width="70px"></asp:Label>
                                    </FooterTemplate>
                                    <FooterStyle ForeColor="White" HorizontalAlign="right" />
                                    <ItemStyle HorizontalAlign="right" />
                                </asp:TemplateField>




                                <asp:TemplateField HeaderText="Realize<br> (FC)">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtgvreceiptam" runat="server" BackColor="Transparent"
                                            BorderColor="blue" BorderStyle="Solid" BorderWidth="1px" Font-Size="10px"
                                            ForeColor="Black" Style="text-align: right" TabIndex="81"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "receiptam")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="90px"></asp:TextBox>
                                        <asp:Label ID="lblgvsgdam" runat="server" BackColor="Transparent"
                                            BorderColor="Transparent" BorderStyle="None" BorderWidth="0px" Font-Size="10px"
                                            ForeColor="Black" Style="text-align: right" TabIndex="81"
                                            Text='<%#  ("<i class=\"bold text-red\">&#2547;</i> ").ToString() + Convert.ToDouble(DataBinder.Eval(Container.DataItem, "bdtamount")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="80px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lblgvFreceiptam" runat="server" BackColor="Transparent"
                                            BorderColor="Transparent" BorderStyle="None"
                                            Font-Size="10px" ForeColor="#000" ReadOnly="True" Style="text-align: right"
                                            Width="90px"></asp:Label>
                                        <asp:Label ID="lblgvFsgdam" runat="server" BackColor="Transparent"
                                            BorderColor="Transparent" BorderStyle="None"
                                            Font-Size="10px" ForeColor="#000" ReadOnly="True" Style="text-align: right"
                                            Width="80px"></asp:Label>
                                    </FooterTemplate>
                                    <FooterStyle ForeColor="White" HorizontalAlign="right" />
                                    <ItemStyle HorizontalAlign="right" Width="90px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Adjustment<br> (FC)">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtgvfcadjamt" runat="server" BackColor="Transparent"
                                            BorderColor="blue" BorderStyle="Solid" BorderWidth="1px" Font-Size="10px"
                                            ForeColor="Black" Style="text-align: right" TabIndex="81"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "fcadjamt")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="80px"></asp:TextBox>
                                         <asp:Label ID="lblgvadjamt" runat="server" BackColor="Transparent"
                                            BorderColor="Transparent" BorderStyle="None" BorderWidth="0px" Font-Size="10px"
                                            ForeColor="Black" Style="text-align: right" TabIndex="81"
                                            Text='<%# ("<i class=\"bold text-red\">&#2547;</i> ").ToString()+ Convert.ToDouble(DataBinder.Eval(Container.DataItem, "adjamt")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="80px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lblgvFfcadjamt" runat="server" BackColor="Transparent"
                                            BorderColor="Transparent" BorderStyle="None"
                                            Font-Size="10px" ForeColor="#000" ReadOnly="True" Style="text-align: right"
                                            Width="80px"></asp:Label>

                                        <asp:Label ID="lblgvFadjamt" runat="server" BackColor="Transparent"
                                            BorderColor="Transparent" BorderStyle="None"
                                            Font-Size="10px" ForeColor="#000" ReadOnly="True" Style="text-align: right"
                                            Width="80px"></asp:Label>
                                    </FooterTemplate>
                                    <FooterStyle ForeColor="White" HorizontalAlign="right" />
                                    <ItemStyle HorizontalAlign="right"  Width="80px"/>
                                </asp:TemplateField>


                                <asp:TemplateField HeaderText="Bank <br>Charge <br> (FC)">
                                    <ItemTemplate>
                                        <asp:TextBox ID="lblgvsvatamt" runat="server" BackColor="Transparent"
                                            BorderColor="blue" BorderStyle="Solid" BorderWidth="1" Font-Size="10px"
                                            ForeColor="Black" Style="text-align: right" TabIndex="81"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "fcbnkcharge")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="70px"></asp:TextBox>
                                         <asp:Label ID="lblgvbdtbankcge" runat="server" BackColor="Transparent"
                                            BorderColor="Transparent" BorderStyle="0" BorderWidth="0" Font-Size="10px"
                                            ForeColor="Black" Style="text-align: right" TabIndex="81"
                                            Text='<%# ("<i class=\"bold text-red\">&#2547;</i> ").ToString()+Convert.ToDouble(DataBinder.Eval(Container.DataItem, "vatamt")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>

                                    <FooterTemplate>
                                        <asp:Label ID="lblgvFsvatamt" runat="server" BackColor="Transparent"
                                            BorderColor="Transparent" BorderStyle="None" BorderWidth="1px" Font-Bold="True"
                                            Font-Size="10px" ForeColor="#000" ReadOnly="True" Style="text-align: right"
                                            Width="70px"></asp:Label>
                                        <asp:Label ID="lblgvFbdtbankcge" runat="server" BackColor="Transparent"
                                            BorderColor="Transparent" BorderStyle="None" BorderWidth="1px" Font-Bold="True"
                                            Font-Size="10px" ForeColor="#000" ReadOnly="True" Style="text-align: right"
                                            Width="70px"></asp:Label>
                                    </FooterTemplate>
                                    <FooterStyle ForeColor="White" HorizontalAlign="right" />
                                    <ItemStyle HorizontalAlign="right" Width="70px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Short <br>Fall <br> (FC)">
                                    <ItemTemplate>
                                        <asp:TextBox ID="lblgvShortFall" runat="server" BackColor="Transparent"
                                            BorderColor="blue" BorderStyle="Solid" BorderWidth="1" Font-Size="10px"
                                            ForeColor="Black" Style="text-align: right" TabIndex="81"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "shortfallfc")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="70px"></asp:TextBox>
                                         <asp:Label ID="lblgvbdShortFall" runat="server" BackColor="Transparent"
                                            BorderColor="Transparent" BorderStyle="0" BorderWidth="0" Font-Size="10px"
                                            ForeColor="Black" Style="text-align: right" TabIndex="81"
                                            Text='<%# ("<i class=\"bold text-red\">&#2547;</i> ").ToString()+Convert.ToDouble(DataBinder.Eval(Container.DataItem, "shortfallbdt")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>

                                    <FooterTemplate>
                                        <asp:Label ID="lblgvFsvShortFall" runat="server" BackColor="Transparent"
                                            BorderColor="Transparent" BorderStyle="None" BorderWidth="1px" Font-Bold="True"
                                            Font-Size="10px" ForeColor="#000" ReadOnly="True" Style="text-align: right"
                                            Width="70px"></asp:Label>
                                        <asp:Label ID="lblgvFbdShortFall" runat="server" BackColor="Transparent"
                                            BorderColor="Transparent" BorderStyle="None" BorderWidth="1px" Font-Bold="True"
                                            Font-Size="10px" ForeColor="#000" ReadOnly="True" Style="text-align: right"
                                            Width="70px"></asp:Label>
                                    </FooterTemplate>
                                    <FooterStyle ForeColor="White" HorizontalAlign="right" />
                                    <ItemStyle HorizontalAlign="right" Width="70px" />
                                </asp:TemplateField>

                                 <asp:TemplateField HeaderText="AIT <br> (Source Tax)">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtgvaitamt" runat="server" BackColor="Transparent"
                                            BorderColor="blue" BorderStyle="Solid" BorderWidth="1" Font-Size="10px"
                                            ForeColor="Black" Style="text-align: right" TabIndex="81"
                                          Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "aitamt")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="70px"></asp:TextBox>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lblgvAitamt" runat="server" BackColor="Transparent"
                                            BorderColor="Transparent" BorderStyle="None"
                                            Font-Size="10px" ForeColor="#000" ReadOnly="True" Style="text-align: right"
                                            Width="70px"></asp:Label>
                                    </FooterTemplate>
                                    <FooterStyle ForeColor="White" HorizontalAlign="right" />
                                    <ItemStyle HorizontalAlign="right" />
                                </asp:TemplateField>
                                 <asp:TemplateField HeaderText="Comm. (Com)">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtgvcomamt" runat="server" BackColor="Transparent"
                                            BorderColor="blue" BorderStyle="Solid" BorderWidth="1" Font-Size="10px"
                                            ForeColor="Black" Style="text-align: right" TabIndex="81"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "commamt")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="70px"></asp:TextBox>
                                    </ItemTemplate>

                                    <FooterTemplate>
                                        <asp:Label ID="lblgvComamt" runat="server" BackColor="Transparent"
                                            BorderColor="Transparent" BorderStyle="None"
                                            Font-Size="10px" ForeColor="#000" ReadOnly="True" Style="text-align: right"
                                            Width="70px"></asp:Label>
                                    </FooterTemplate>
                                    <FooterStyle ForeColor="White" HorizontalAlign="right" />
                                    <ItemStyle HorizontalAlign="right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Other <br> Charges">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtgvOtherCharges" runat="server" BackColor="Transparent"
                                            BorderColor="blue" BorderStyle="Solid" BorderWidth="1" Font-Size="10px"
                                            ForeColor="Black" Style="text-align: right" TabIndex="81"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "othcharge")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="60px"></asp:TextBox>
                                    </ItemTemplate>

                                    <FooterTemplate>
                                        <asp:Label ID="lblgvOthrChargs" runat="server" BackColor="Transparent"
                                            BorderColor="Transparent" BorderStyle="None"
                                            Font-Size="10px" ForeColor="#000" ReadOnly="True" Style="text-align: right"
                                            Width="60px"></asp:Label>
                                    </FooterTemplate>
                                    <FooterStyle ForeColor="White" HorizontalAlign="right" />
                                    <ItemStyle HorizontalAlign="right" />
                                </asp:TemplateField>
                                 <asp:TemplateField HeaderText="Over<br>Due <br>Interest">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtgvOverDueInter" runat="server" BackColor="Transparent"
                                            BorderColor="blue" BorderStyle="Solid" BorderWidth="1" Font-Size="10px"
                                            ForeColor="Black" Style="text-align: right" TabIndex="81"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ovrdueintrst")).ToString("#,##0.00;-#,##0.00; ") %>'
                                            Width="70px"></asp:TextBox>
                                    </ItemTemplate>

                                    <FooterTemplate>
                                        <asp:Label ID="lblgvFOverDueInterst" runat="server" BackColor="Transparent"
                                            BorderColor="Transparent" BorderStyle="None"
                                            Font-Size="10px" ForeColor="#000" ReadOnly="True" Style="text-align: right"
                                            Width="70px"></asp:Label>
                                    </FooterTemplate>
                                    <FooterStyle ForeColor="White" HorizontalAlign="right" />
                                    <ItemStyle HorizontalAlign="right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Gain /<br> Loss">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtgvcglamt" runat="server" BackColor="Transparent"
                                            BorderColor="blue" BorderStyle="Solid" BorderWidth="1" Font-Size="10px"
                                            ForeColor="Black" Style="text-align: right" TabIndex="81"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "cglamt")).ToString("#,##0.00;-#,##0.00; ") %>'
                                            Width="70px"></asp:TextBox>
                                    </ItemTemplate>

                                    <FooterTemplate>
                                        <asp:Label ID="lblgvFcglamt" runat="server" BackColor="Transparent"
                                            BorderColor="Transparent" BorderStyle="None"
                                            Font-Size="10px" ForeColor="#000" ReadOnly="True" Style="text-align: right"
                                            Width="70px"></asp:Label>
                                    </FooterTemplate>
                                    <FooterStyle ForeColor="White" HorizontalAlign="right" />
                                    <ItemStyle HorizontalAlign="right" />
                                </asp:TemplateField>

                                <asp:TemplateField Visible="False">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkbill" runat="server"
                                            Checked='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "chk"))=="True" %>'
                                            Width="20px" />
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Adj">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="LbtnAdvUpdate" OnClick="LbtnAdvUpdate_Click" CssClass="btn btn-xs btn-default" runat="server"><span class="fa fa-check"></span></asp:LinkButton>
                                    </ItemTemplate>
                                    <ItemStyle Width="60px" HorizontalAlign="Center" />
                                </asp:TemplateField>



                            </Columns>
                            <EditRowStyle />
                            <AlternatingRowStyle />
                            <PagerStyle CssClass="gvPagination" />
                            <HeaderStyle CssClass="grvHeader" />
                            <FooterStyle CssClass="grvFooter" />
                        </asp:GridView>
                    </div>

                    <div class="footertd" id="FooterSegment" runat="server" visible="false">

                        <hr class="hrpro" />
                        <div class="row" >


                            <div class="col-md-3 col-sm-3 col-lg-3 ">
                                <div class="row">

                                    <div class="col-md-9 col-sm-9 col-lg-9 ">
                                        <div class="form-group">
                                            <asp:LinkButton ID="lnkAcccode" runat="server" OnClick="lnkAcccode_Click" CssClass="label">Head of Account</asp:LinkButton>
                                            <asp:DropDownList ID="ddlacccode" runat="server"  CssClass="chzn-select form-control form-control-sm"  TabIndex="2"></asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-md-3 col-sm-3 col-lg-3 ">
                                        <div class="form-group" style="margin-top: 20px;">
                                            <asp:LinkButton ID="lnkadd" runat="server" CssClass="btn btn-primary btn-sm pull-left" OnClick="lnkadd_OnClick">Add</asp:LinkButton>
                                        </div>
                                    </div>

                                    <div class="col-md-12 col-sm-12 col-lg-12 ">
                                        <div class="form-group">
                                            <asp:Label ID="lblReqNarr" runat="server" CssClass="label">Narration</asp:Label>
                                            <asp:TextBox ID="txtNar2" runat="server" CssClass="form-control form-control-sm " TextMode="MultiLine" cols="20" Rows="4"></asp:TextBox>

                                        </div>
                                    </div>



                                </div>
                            </div>

                            <div class="col-md-7 col-sm-7 col-lg-7 ">

                                <div class="row" style="min-width:500px;">
                                    <asp:Label ID="lblInword" runat="server" CssClass="lblTxt lblName pull-right " Style="width: 600px; color: blue; text-align: right;"></asp:Label>

                                    <asp:GridView ID="dgv1" runat="server" AutoGenerateColumns="False" CssClass="table-striped table-bordered grvContentarea "
                                        ShowFooter="True" Width="800px" OnRowDataBound="dgv1_RowDataBound" OnRowDeleting="dgv1_RowDeleting">
                                        <RowStyle />
                                        <Columns>
                                            <asp:CommandField ShowDeleteButton="True" ControlStyle-Width="15px" DeleteText="&lt;span class='glyphicon glyphicon-remove asitGlyp'&gt;&lt;span&gt;">
                                                <ControlStyle Width="15px" />
                                                <HeaderStyle Width="15px" />
                                                <ItemStyle Width="15px" />
                                            </asp:CommandField>
                                            <asp:TemplateField HeaderText="Sl" HeaderStyle-Width="15px">
                                                <ItemTemplate>
                                                    <asp:Label ID="serialnoid" runat="server"
                                                        Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="15px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle Font-Size="13px" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="A/c Code" Visible="False">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblAccCod" runat="server"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actcode")) %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="rescode" Visible="False">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblResCod" runat="server"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rescode")) %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderStyle-Width="500px">
                                                <HeaderTemplate>
                                                    <asp:Label ID="lblas" Style="text-align: left !important;" runat="server">Head of Accounts</asp:Label>

                                                </HeaderTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lnkTotal" runat="server" Font-Bold="True">Total :</asp:Label>
                                                </FooterTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblAccdesc" runat="server"
                                                        Font-Size="11px"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc")) %>'
                                                        Width="100%"></asp:Label>

                                                    <asp:Label ID="lblresdesc1" runat="server"
                                                        Font-Size="11px"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "resdesc1")) %>'
                                                        Width="100%"></asp:Label>
                                                </ItemTemplate>


                                                <FooterStyle HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>



                                            <asp:TemplateField HeaderText="FC Amount" HeaderStyle-Width="50px">

                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtgfcamt" runat="server" BackColor="Transparent" AutoPostBack="True"
                                                        BorderColor="blue" BorderStyle="Solid" OnTextChanged="txtgfcamt_OnTextChanged"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "fcamt")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                        Width="80px" Font-Size="12px" ForeColor="Black" Style="text-align: right"
                                                        TabIndex="79"></asp:TextBox>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="gvlablfcamt" runat="server" BackColor="Transparent"
                                                        BorderColor="Transparent" BorderStyle="None" BorderWidth="1px"
                                                        Width="80px" Font-Size="12px" ReadOnly="True" ForeColor="Black"
                                                        Style="text-align: right" TabIndex="80"></asp:Label>
                                                </FooterTemplate>
                                                <HeaderStyle Width="50px" />
                                                <ItemStyle HorizontalAlign="right" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Rate">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtcrate" runat="server" BackColor="Transparent" AutoPostBack="True"
                                                        BorderColor="blue" BorderStyle="Solid" OnTextChanged="txtcrate_TextChanged"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "convrate")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                        Width="80px" Font-Size="12px" ForeColor="Black" Style="text-align: right"
                                                        TabIndex="79"></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>


                                            <asp:TemplateField HeaderText="BDT Amount" HeaderStyle-Width="50px">
                                                <ItemTemplate>
                                                    <asp:Label ID="txtgvbdtamt" runat="server" BackColor="Transparent"
                                                        BorderColor="Transparent" BorderStyle="None" BorderWidth="1px"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "bdtamt")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                        Width="80px" Font-Size="12px" ReadOnly="True" ForeColor="Black"
                                                        Style="text-align: right" TabIndex="80"></asp:Label>
                                                </ItemTemplate>

                                                <FooterTemplate>
                                                    <asp:Label ID="gvlablBDTamt" runat="server" BackColor="Transparent"
                                                        BorderColor="Transparent" BorderStyle="None" BorderWidth="1px"
                                                        Width="80px" Font-Size="12px" ReadOnly="True" ForeColor="Black"
                                                        Style="text-align: right" TabIndex="80"></asp:Label>
                                                </FooterTemplate>
                                                <HeaderStyle Width="50px" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>


                                            <asp:TemplateField HeaderText="Gain/Loss">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtglamt" runat="server" BackColor="Transparent"
                                                        BorderColor="blue" BorderStyle="Solid"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "lgamt")).ToString("#,##0.00;-#,##0.00; ") %>'
                                                        Width="80px" Font-Size="12px" ForeColor="Black" Style="text-align: right"
                                                        TabIndex="79"></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            
                                            <asp:TemplateField HeaderText="Total Expense">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtTotalExpense" runat="server" BackColor="Transparent"
                                                        BorderColor="blue" BorderStyle="Solid"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "totlexpnse")).ToString("#,##0.00;-#,##0.00; ") %>'
                                                        Width="80px" Font-Size="12px" ForeColor="Black" Style="text-align: right"
                                                        TabIndex="79"></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Total Amount" HeaderStyle-Width="50px">
                                                <ItemTemplate>
                                                    <asp:Label ID="txttamt" runat="server" BackColor="Transparent"
                                                        BorderColor="Transparent" BorderStyle="None" BorderWidth="1px"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "tamt")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                        Width="80px" Font-Size="12px" ReadOnly="True" ForeColor="Black"
                                                        Style="text-align: right" TabIndex="80"></asp:Label>
                                                </ItemTemplate>

                                                <FooterTemplate>
                                                    <asp:Label ID="txtFtamt" runat="server" BackColor="Transparent"
                                                        BorderColor="Transparent" BorderStyle="None" BorderWidth="1px"
                                                        Width="80px" Font-Size="12px" ReadOnly="True" ForeColor="Black"
                                                        Style="text-align: right" TabIndex="80"></asp:Label>
                                                </FooterTemplate>
                                                <HeaderStyle Width="50px" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Remarks">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="lblgvRemarks" runat="server" BackColor="Transparent"
                                                        BorderColor="Transparent" BorderStyle="None" BorderWidth="1px"
                                                        Font-Size="12px"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "trnrmrk")) %>'
                                                        Width="80px" ForeColor="Black" TabIndex="83"></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>


                                        </Columns>
                                        <FooterStyle BackColor="#F5F5F5" Height="30px" />
                                        <EditRowStyle />
                                        <AlternatingRowStyle />
                                        <PagerStyle CssClass="gvPagination" />
                                        <HeaderStyle CssClass="grvHeader" />
                                    </asp:GridView>


                                </div>
                            </div>
                            <div class="clearfix"></div>

                        </div>

                    </div>




                </div>
            </div>

            <div class="col-md-3">
                <asp:DropDownList ID="ddlPreMrr" runat="server" CssClass="inputTxt inpPixedWidth" Visible="false">
                </asp:DropDownList>
            </div>


            <div id="Advmodal" class="modal" role="dialog">
                <div class="modal-dialog modal-md">
                    <div class="modal-content ">
                        <div class="modal-header">

                            <h4 class="modal-title"><span class="fa fa-table"></span>Advance Adjustment </h4>
                            <button type="button" class="close btn btn-xs" data-dismiss="modal"><span class="fa fa-arrow-left"></span></button>

                            <asp:Label ID="lblinvnomr" runat="server" Visible="false"></asp:Label>
                        </div>
                        <div class="modal-body form-horizontal">

                            <asp:GridView ID="gvAdvDetails" ClientIDMode="Static" runat="server" AutoGenerateColumns="False"
                                ShowFooter="True" CssClass=" table-striped table-hover table-bordered grvContentarea">
                                <PagerSettings Visible="False" />

                                <Columns>

                                    <asp:TemplateField HeaderText="SL">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvSlNo" runat="server" Height="16px" Style="text-align: right"
                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="25px"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Voucher">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvVoucher" runat="server"
                                                Font-Size="10px"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "vounum")) %>'
                                                Width="70px"></asp:Label>

                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Advance</br> Date">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvvoudate" runat="server"
                                                Font-Size="10px"
                                                Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "voudat")).ToString("dd-MMM-yyyy")  %>'
                                                Width="70px"></asp:Label>

                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Advance<br> Amount">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvtrnam" runat="server" BackColor="Transparent"
                                                BorderColor="Transparent" BorderStyle="None" BorderWidth="0px" Font-Size="10px"
                                                ForeColor="Black" Style="text-align: right" TabIndex="81"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "trnam")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="80px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lblgvFtrnam" runat="server" BackColor="Transparent"
                                                BorderColor="Transparent" BorderStyle="None"
                                                Font-Size="10px" ForeColor="#000" ReadOnly="True" Style="text-align: right"
                                                Width="80px"></asp:Label>
                                        </FooterTemplate>
                                        <FooterStyle ForeColor="White" HorizontalAlign="right" />
                                        <ItemStyle HorizontalAlign="right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Adjustment<br> Amount">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvadjamt" runat="server" BackColor="Transparent"
                                                BorderColor="blue" BorderStyle="Solid" BorderWidth="1px" Font-Size="10px"
                                                ForeColor="Black" Style="text-align: right" TabIndex="81"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "adjamt")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="90px"></asp:TextBox>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lblgvFadjamt" runat="server" BackColor="Transparent"
                                                BorderColor="Transparent" BorderStyle="None"
                                                Font-Size="10px" ForeColor="#000" ReadOnly="True" Style="text-align: right"
                                                Width="90px"></asp:Label>
                                        </FooterTemplate>
                                        <FooterStyle ForeColor="White" HorizontalAlign="right" />
                                        <ItemStyle HorizontalAlign="right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Ref. No">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvinvno" runat="server"
                                                Font-Size="10px"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "invno"))  %>'
                                                Width="90px"></asp:Label>

                                        </ItemTemplate>

                                        <FooterStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                            <asp:CheckBox ID="ChkAll" ClientIDMode="Static" runat="server" onclick="javascript:SelectAllCheckboxes(this);" />
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkack" ClientIDMode="Static" runat="server" CssClass="chkack"
                                                Checked='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "approved"))=="True" ? true : false %>'
                                                Enabled='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "approved"))=="True" ? false : true%>'
                                                Width="20px" />
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" />
                                        <FooterStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>



                                    <asp:TemplateField HeaderText=" Id" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvvounum" runat="server" Visible="false" Height="16px" Font-Size="11px"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "vounum")) %>'
                                                Width="80px"></asp:Label>



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
                        <div class="modal-footer ">
                            <asp:LinkButton ID="ModalUpdateBtn" OnClick="ModalUpdateBtn_Click" OnClientClick="CLoseMOdal();"
                                runat="server" CssClass="btn btn-primary"> <span class="glyphicon glyphicon-saved"></span> Update</asp:LinkButton>
                            <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                        </div>
                    </div>
                </div>
            </div>


        </ContentTemplate>
    </asp:UpdatePanel>


</asp:Content>

