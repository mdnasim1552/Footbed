<%@ Page Title="" Language="C#" MasterPageFile="~/SPE.Master" AutoEventWireup="true" CodeBehind="ExportReturn.aspx.cs" Inherits="SPEWEB.F_19_EXP.ExportReturn" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link href="../Content/chosen.css" rel="stylesheet" />
    <script src="../Scripts/chosen.jquery.js"></script>
    <script language="javascript" type="text/javascript">

        $(document).ready(function () {
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);

        });

        function pageLoaded() {

            $("input, select").bind("keydown", function (event) {
                var k1 = new KeyPress();
                k1.textBoxHandler(event);
            });

            $('.chzn-select').chosen({ search_contains: true });
        };
    </script>


    <style>
        .chzn-single {
            border-radius: 3px !important;
            height: 29px !important;
        }
    </style>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="card card-fluid">
                <div class="card-body">
                    <div class="row">

                        <div class="col-md-0" style="margin-left: 10px">
                            <div class="form-group">
                                <asp:Label ID="lblCurDate" runat="server" CssClass="lblTxt lblDate" Text="MRR Date"></asp:Label>
                                <asp:TextBox ID="txtCurMRRDate" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
                                <cc1:CalendarExtender ID="txtCurMRRDate_CalendarExtender" runat="server"
                                    Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txtCurMRRDate"></cc1:CalendarExtender>
                            </div>
                        </div>

                        <div class="col-md-1">
                            <div class="form-group">
                                <asp:Label ID="Label1" runat="server" CssClass="form-label">Return No.</asp:Label>
                                <asp:Label ID="lblCurNo1" runat="server" CssClass="form-control form-control-sm" Text="RET00-" ReadOnly="True" TabIndex="2"></asp:Label>
                            </div>
                        </div>

                        <div class="col-md-1" style="margin-top: 20px; margin-left: -5px">
                            <asp:TextBox ID="txtCurNo2" runat="server" CssClass="form-control form-control-sm" ReadOnly="True" TabIndex="3">00000</asp:TextBox>
                        </div>

                        <div class="col-md-2">
                            <div class="form-group">
                                <asp:Label ID="lblOrderList" runat="server" CssClass="lblTxt lblDate" Text="Order No."></asp:Label>
                                <asp:DropDownList ID="ddlmlccode" runat="server" CssClass="form-control form-control-sm chzn-select" AutoPostBack="True" OnSelectedIndexChanged="ddlmlccode_SelectedIndexChanged"></asp:DropDownList>
                            </div>
                        </div>

                        <div class="col-md-3">
                            <div class="form-group">
                                <asp:Label ID="Label2" runat="server" CssClass="lblTxt lblDate" Text="Invoice List"></asp:Label>
                                <asp:DropDownList ID="ddlInvoiceList" runat="server" CssClass="form-control form-control-sm chzn-select" AutoPostBack="True" OnSelectedIndexChanged="ddlInvoiceList_SelectedIndexChanged"></asp:DropDownList>
                            </div>
                        </div>

                        <div class="col-md-1 mt-3" style="margin-left: 15px; padding-top: 5px">

                            <asp:LinkButton ID="lbtnSelectInv" runat="server" CssClass="btn btn-sm btn-primary" OnClick="lbtnSelectInv_Click">Select</asp:LinkButton>
                            <asp:LinkButton ID="lbtnSelectResAll" runat="server" CssClass="btn btn-primary checkBox hidden">Select All</asp:LinkButton>

                        </div>

                        <div class="col-md-2" style="margin-right: auto;">
                            <asp:LinkButton ID="imgPreVious" runat="server" CssClass="form-label" OnClick="imgPreVious_Click" TabIndex="3">Previous List</asp:LinkButton>
                            <asp:DropDownList ID="ddlPrevList" runat="server" CssClass="form-control form-control-sm chzn-select" AutoPostBack="True" OnSelectedIndexChanged="ddlPrevList_SelectedIndexChanged">
                            </asp:DropDownList>
                        </div>
                    </div>
                </div>
            </div>

            <div class="card card-fluid">
                <div class="card-body" style="min-height: 500px;">
                    <div class="row">
                        <asp:GridView ID="gvExpRet" runat="server" AutoGenerateColumns="False" CssClass=" table-striped table-hover table-bordered grvContentarea"
                            ShowFooter="True" Width="689px">

                            <Columns>

                                <asp:TemplateField HeaderText="SL.">
                                    <ItemTemplate>
                                        <asp:Label ID="serialnoid" runat="server"
                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center"/>
                                    <HeaderStyle Font-Bold="True" Font-Size="12px" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="A/c Code" Visible="False">
                                    <ItemTemplate>
                                        <asp:Label ID="lblAccCod" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mlccod")) %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>


                                <asp:TemplateField HeaderText="Invvoice No">
                                    <ItemTemplate>
                                        <asp:Label ID="lblTrnno" runat="server" ForeColor="Black" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "invno")) %>' Width="120px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Order Description" ItemStyle-Font-Size="9px">
                                    <ItemTemplate>
                                        <asp:Label ID="lblAccdesc1" runat="server" Font-Size="11px"
                                            Text='<%# "<B>"+ Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc")) + "</B>" %>'
                                            Width="160px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle Font-Size="11px" />
                                </asp:TemplateField>


                                <asp:TemplateField HeaderText="Style" ItemStyle-Font-Size="9px">
                                    <ItemTemplate>
                                        <asp:Label ID="lblstyledesc" runat="server"
                                            Font-Size="11px"
                                            Text='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "styledesc"))  %>'
                                            Width="180px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle Font-Size="11px" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Color" ItemStyle-Font-Size="9px">
                                    <ItemTemplate>
                                        <asp:Label ID="lblcolordesc" runat="server"
                                            Font-Size="11px"
                                            Text='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "colordesc"))  %>'
                                            Width="90px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle Font-Size="11px" HorizontalAlign="Center"/>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Size" ItemStyle-Font-Size="9px">
                                    <ItemTemplate>
                                        <asp:Label ID="lblResdescd" runat="server"
                                            Font-Size="11px"
                                            Text='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "sizedesc"))  %>'
                                            Width="50px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <ItemStyle Font-Size="11px" HorizontalAlign="Center" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Invoice Qty" ItemStyle-Font-Size="11px">
                                    <FooterTemplate>
                                        <asp:Label ID="lblInvQtyf" runat="server" BackColor="Transparent"
                                            BorderColor="Transparent" BorderStyle="None" BorderWidth="1px"
                                            Font-Bold="True" Font-Size="12px" ForeColor="White"
                                            Height="22px" Width="70px"></asp:Label>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="txtgvinvqtyd" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                            BorderWidth="0px" Font-Size="11px" Style="text-align: right; background-color: Transparent"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "invqty")).ToString("#,##0;(#,##0); ") %>'
                                            Width="60px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Font-Size="11px" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Previous Return Qty" ItemStyle-Font-Size="11px">
                                    <FooterTemplate>
                                        <asp:Label ID="lblInvQtyf" runat="server" BackColor="Transparent"
                                            BorderColor="Transparent" BorderStyle="None" BorderWidth="1px"
                                            Font-Bold="True" Font-Size="12px" ForeColor="White"
                                            Height="22px" Width="80px"></asp:Label>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="txtgvinvqtysa" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                            BorderWidth="0px" Font-Size="11px" Style="text-align: right; background-color: Transparent"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "retqty")).ToString("#,##0;(#,##0); ") %>'
                                            Width="60px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Font-Size="11px" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Return Qty" ItemStyle-Font-Size="11px">
                                    <FooterTemplate>
                                        <asp:Label ID="lblTgvrtFqty" runat="server" BackColor="Transparent"
                                            BorderColor="Transparent" BorderStyle="None" BorderWidth="1px"
                                            Font-Bold="True" Font-Size="12px" ForeColor="White"
                                            Height="22px" Width="70px"></asp:Label>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:TextBox ID="retQty" Width="70px" runat="server" BorderColor="Tomato"></asp:TextBox>
                                    </ItemTemplate>
                                    <ItemStyle Font-Size="11px" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Remarks" ItemStyle-Font-Size="11px">
                                    <ItemTemplate>
                                        <asp:TextBox ID="lblgvRemarks" runat="server" BackColor="Transparent"
                                            BorderColor="Transparent" BorderStyle="None" BorderWidth="1px"
                                            Height="22px" ForeColor="Tomato"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "trnrmrk")) %>'
                                            Width="200px"></asp:TextBox>
                                    </ItemTemplate>
                                    <ItemStyle Font-Size="11px" />
                                    <ItemStyle HorizontalAlign="Left" />
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
            </div>

        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

