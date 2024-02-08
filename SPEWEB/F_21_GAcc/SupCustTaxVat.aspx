<%@ Page Title="" Language="C#" MasterPageFile="~/SPEMaster.Master" AutoEventWireup="true" CodeBehind="SupCustTaxVat.aspx.cs" Inherits="SPEWEB.F_21_GAcc.SupCustTaxVat" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">


    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <script type="text/javascript" language="javascript">
                $(document).ready(function () {
                    $("input, select").bind("keydown", function (event) {
                        var k1 = new KeyPress();
                        k1.textBoxHandler(event);

                    });

                });
                $(document).ready(function () {

                    Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);

                });

                function pageLoaded() {


                    $('.chzn-select').chosen({ search_contains: true });

                }

            </script>
            <div class="container moduleItemWrpper">
                <div class="contentPart">
                    <fieldset class="scheduler-border fieldset_A">
                        <div class="form-horizontal">
                            <div class="form-group">
                                <div class="col-md-4">
                                    <asp:Label ID="Label4" runat="server" CssClass="lblTxt lblName ">Report Type</asp:Label>
                                    <asp:RadioButtonList runat="server" ID="rbttype" CssClass="rbtnList1"  AutoPostBack="true" OnSelectedIndexChanged="rbttype_SelectedIndexChanged" RepeatDirection="Horizontal">
                                        <asp:ListItem Value="1" Selected="True">Details</asp:ListItem>
                                        <asp:ListItem Value="2">All</asp:ListItem>
                                    </asp:RadioButtonList>
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="col-md-3 pading5px">
                                    <asp:Label ID="lblVouDate" runat="server" CssClass="lblTxt lblName ">Date</asp:Label>
                                    <asp:TextBox ID="txfdate" runat="server" CssClass=" inputtextbox"></asp:TextBox>
                                    <cc1:CalendarExtender ID="txfdate_CalendarExtender" runat="server"
                                        Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txfdate"></cc1:CalendarExtender>

                                    <asp:Label ID="Label1" runat="server" CssClass="  smLbl_to ">To</asp:Label>
                                    <asp:TextBox ID="txtdate" runat="server" CssClass=" inputtextbox"></asp:TextBox>
                                    <cc1:CalendarExtender ID="txtdate_CalendarExtender" runat="server"
                                        Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txtdate"></cc1:CalendarExtender>


                                </div>
                                <div class="col-md-4 pading5px">
                                    <asp:Label ID="Label2" runat="server" CssClass="smLbl_to">Accounts Head</asp:Label>

                                    <asp:DropDownList ID="ddlConAccHead" runat="server" Width="315px" CssClass="form-control inputTxt chzn-select" AutoPostBack="true">
                                    </asp:DropDownList>



                                </div>
                                <div runat="server" id="suplsection"  visible="false"  class="col-md-3 pading5px">
                                    <asp:Label ID="Label3" runat="server" CssClass=" smLbl_to">Supplier</asp:Label>

                                    <asp:DropDownList ID="ddlSupCust" runat="server" Width="235px" CssClass="form-control inputTxt chzn-select" >
                                    </asp:DropDownList>
                                </div>
                                <div class="col-md-1 pading5px">
                                    <asp:LinkButton ID="lbtnOk" runat="server" OnClick="lbtnOk_Click" TabIndex="5" CssClass="btn btn-primary okBtn">Ok</asp:LinkButton>

                                </div>
                            </div>
                        </div>
                    </fieldset>

                    <asp:Panel ID="pnlDealis" runat="server">

                    <asp:GridView ID="dgv2" runat="server" AutoGenerateColumns="False" CssClass="table-striped table-hover table-bordered grvContentarea"
                        BorderStyle="Solid" BorderWidth="2px"
                        ShowFooter="True" Width="689px">

                        <Columns>
                            <asp:TemplateField HeaderText="Sl">
                                <ItemTemplate>
                                    <asp:Label ID="serialnoid" runat="server"
                                        Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle Font-Bold="True" Font-Size="12px" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="A/c Code" Visible="False">
                                <ItemTemplate>
                                   


                                    <asp:Label ID="lblAccCod" runat="server"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actcode")) %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Voucher No" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblResCod" runat="server"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "vounum")) %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Voucher No">
                                <ItemTemplate>
                                    <asp:Label ID="lblvounum" runat="server"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "vounum1")) %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>


                            <asp:TemplateField HeaderText="Date">
                                <ItemTemplate>
                                    <asp:Label ID="lbldate" runat="server" Width="100px"
                                        Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "voudat")).ToString("dd-MMM-yyyy") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Opening Amount" ItemStyle-Font-Size="11px">
                                <FooterTemplate>
                                    <asp:Label ID="FOTgvopnam" runat="server" BackColor="Transparent" ReadOnly="true"
                                        BorderColor="Transparent" BorderStyle="None" BorderWidth="1px"
                                        Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                        Width="80px"></asp:Label>
                                </FooterTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="txtgvDrAmt" runat="server" BackColor="Transparent" Style="text-align: right"
                                        BorderColor="Transparent" BorderStyle="None" BorderWidth="1px"
                                        Height="22px" ForeColor="Black"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "opamt")).ToString("#,##0.00;(#,##0.00); ") %>'
                                        Width="80px"></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Font-Size="11px" HorizontalAlign="Right" />
                                <FooterStyle HorizontalAlign="Right" />
                            </asp:TemplateField>


                            <asp:TemplateField HeaderText="Dr. Amount" ItemStyle-Font-Size="11px">
                                <FooterTemplate>
                                    <asp:Label ID="FOTgvDrAmt" runat="server" BackColor="Transparent" ReadOnly="true"
                                        BorderColor="Transparent" BorderStyle="None" BorderWidth="1px"
                                        Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                        Width="80px"></asp:Label>
                                </FooterTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="txtgvDrAmt" runat="server" BackColor="Transparent" Style="text-align: right"
                                        BorderColor="Transparent" BorderStyle="None" BorderWidth="1px"
                                        Height="22px" ForeColor="Black"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "dram")).ToString("#,##0.00;(#,##0.00); ") %>'
                                        Width="80px"></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Font-Size="11px" HorizontalAlign="Right" />
                                <FooterStyle HorizontalAlign="Right" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Cr. Amount" ItemStyle-Font-Size="11px">
                                <FooterTemplate>
                                    <asp:Label ID="FOTgvCrAmtDB" runat="server" BackColor="Transparent"
                                        BorderColor="Transparent" BorderStyle="None" BorderWidth="1px"
                                        Font-Bold="True" Font-Size="12px" ForeColor="Black" ReadOnly="true"
                                        Width="80px"></asp:Label>
                                </FooterTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="txtgvCrAmt" runat="server" BackColor="Transparent" Style="text-align: right"
                                        BorderColor="Transparent" BorderStyle="None" BorderWidth="1px"
                                        Height="22px" ForeColor="Black"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "cram")).ToString("#,##0.00;(#,##0.00); ") %>'
                                        Width="80px"></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Font-Size="11px" HorizontalAlign="Right" />
                                <FooterStyle HorizontalAlign="Right" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Closing Amount" ItemStyle-Font-Size="11px">
                                <FooterTemplate>
                                    <asp:Label ID="FotTgvclsamt" runat="server" BackColor="Transparent"
                                        BorderColor="Transparent" BorderStyle="None" BorderWidth="1px"
                                        Font-Bold="True" Font-Size="12px" ForeColor="Black" ReadOnly="true"
                                        Width="80px"></asp:Label>
                                </FooterTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="txtgvclsamt" runat="server" BackColor="Transparent" Style="text-align: right"
                                        BorderColor="Transparent" BorderStyle="None" BorderWidth="1px"
                                        Height="22px" ForeColor="Black"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "clsamt")).ToString("#,##0.00;(#,##0.00); ") %>'
                                        Width="80px"></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Font-Size="11px" HorizontalAlign="Right" />
                                <FooterStyle HorizontalAlign="Right" />
                            </asp:TemplateField>



                            <asp:TemplateField HeaderText="Narration " ItemStyle-Font-Size="9px">
                                <ItemTemplate>
                                    <asp:Label ID="lblAccvenar1" runat="server"
                                        Font-Names="Verdana" Font-Size="11px"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "venar1")) %>'
                                        Width="400px"></asp:Label>

                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle Font-Size="11px" />
                            </asp:TemplateField>


                            <asp:TemplateField HeaderText="Ref #" ItemStyle-Font-Size="9px">
                                <ItemTemplate>
                                    <asp:Label ID="lblAcrefnum" runat="server"
                                        Font-Names="Verdana" Font-Size="11px"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "refnum")) %>'
                                        Width="100px"></asp:Label>

                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Left" />
                                <ItemStyle Font-Size="11px" />
                            </asp:TemplateField>

                        </Columns>
                        <EditRowStyle />
                        <AlternatingRowStyle />
                        <PagerStyle CssClass="gvPagination" />
                        <HeaderStyle CssClass="grvHeader" />
                        <FooterStyle CssClass="grvFooter" />
                    </asp:GridView>
                    </asp:Panel>

                    <asp:Panel ID="pnlAll" runat="server" Visible="false">
                        <asp:GridView ID="grvDTB" runat="server" AutoGenerateColumns="False" ShowFooter="True" OnRowDataBound="grvDTB_RowDataBound" CssClass="table-striped table-hover table-bordered grvContentarea grvCenter">
                            <Columns>
                                <asp:TemplateField HeaderText="Ac Code" Visible="false">
                                    <ItemTemplate>
                                         <asp:Label ID="lblComcod" runat="server"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "comcod")) %>'></asp:Label>
                                        <asp:Label ID="lblgvAccode" runat="server" CssClass="GridLebel"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actcode4")) %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Code">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvcode" runat="server" CssClass="GridLebel"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rescode")) %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                </asp:TemplateField>
                                <asp:TemplateField FooterStyle-Font-Bold="true" FooterStyle-Font-Size="12px"
                                    FooterStyle-HorizontalAlign="Right" HeaderText="Descryption of Account" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="gvAcDesc" runat="server" Font-Underline="False" Style="font-size: 12px"
                                            Target="_blank" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc4")) %>'
                                            Width="300px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:TemplateField FooterStyle-Font-Bold="true" FooterStyle-Font-Size="12px"
                                    FooterStyle-HorizontalAlign="Right" HeaderText="Descryption of Account" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="gvResDesc" runat="server" Font-Underline="False" Style="font-size: 12px"
                                            Target="_blank" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "resdesc")) %>'
                                            Width="300px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:TemplateField FooterStyle-Font-Bold="true" FooterStyle-Font-Size="12px"
                                    FooterStyle-HorizontalAlign="Right" FooterText="Dr. &lt;br&gt; Cr."
                                    HeaderText="Descryption of Account">
                                    <ItemTemplate>
                                        <asp:HyperLink ID="HLgvDesc" runat="server" Font-Underline="False" Style="font-size: 12px"
                                            Target="_blank" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "resdesc")) %>'
                                            Width="300px"></asp:HyperLink>
                                    </ItemTemplate>
                                    <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:TemplateField FooterStyle-HorizontalAlign="Right" HeaderText="Opening Amt"
                                    ItemStyle-HorizontalAlign="Right">
                                    <FooterTemplate>
                                        <asp:Label ID="lblfopnamt" runat="server" CssClass="GridLebel"></asp:Label>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvopenamt" runat="server" CssClass="GridLebel"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "opnam")).ToString("#,##0.00;(#,##0.00); ") %>'></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle HorizontalAlign="Right" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField FooterStyle-HorizontalAlign="Right" HeaderText="Dr. Amount"
                                    ItemStyle-HorizontalAlign="Right">
                                    <FooterTemplate>
                                        <asp:Label ID="lblfDramt" runat="server" CssClass="GridLebel"></asp:Label>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblDramt" runat="server" CssClass="GridLebel"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "dram")).ToString("#,##0.00;(#,##0.00); ") %>'></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle HorizontalAlign="Right" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField FooterStyle-HorizontalAlign="Right" HeaderText="Cr. Amount"
                                    ItemStyle-HorizontalAlign="Right">
                                    <FooterTemplate>
                                        <asp:Label ID="lblfCramt" runat="server" CssClass="GridLebel"></asp:Label>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvCramt" runat="server" CssClass="GridLebel"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "cram")).ToString("#,##0.00;(#,##0.00); ") %>'></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle HorizontalAlign="Right" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField FooterStyle-HorizontalAlign="Right" HeaderText="Closing Amt"
                                    ItemStyle-HorizontalAlign="Right">
                                    <FooterTemplate>
                                        <asp:Label ID="lblfcloamt" runat="server" CssClass="GridLebel"></asp:Label>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblClosingamt" runat="server" CssClass="GridLebel"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "closam")).ToString("#,##0.00;(#,##0.00); ") %>'></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle HorizontalAlign="Right" />
                                    <ItemStyle HorizontalAlign="Right" />
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
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

