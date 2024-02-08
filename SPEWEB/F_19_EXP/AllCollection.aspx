<%@ Page Title="" Language="C#" MasterPageFile="~/SPE.Master" AutoEventWireup="true" CodeBehind="AllCollection.aspx.cs" Inherits="SPEWEB.F_19_EXP.AllCollection" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript" language="javascript">
        $(document).ready(function () {

            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);
        });

        function pageLoaded() {
            $(function () {
                $('[id*=ddlBuyer]').multiselect({
                    includeSelectAllOption: true,
                    enableCaseInsensitiveFiltering: true
                })
            });
            var gv1 = $('#<%=this.gvCollectionAll.ClientID %>');
            gv1.Scrollable();

            <%--var gvCollectionAll = $('#<%=this.gvCollectionAll.ClientID %>');

            gvCollectionAll.gridviewScroll({
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
            });--%>
        }
    </script>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>

            <div class="card card-fluid">
                <div class="card-body" style="min-height: 100px">
                    <div class="form-inline">
                        <div class="col-md-1">
                            <asp:Label ID="lblprodate" runat="server" CssClass=" smLbl_to" EnableViewState="False" Text="Date"></asp:Label>
                            <asp:TextBox ID="txtfromdate" runat="server" CssClass="form-control form-control-sm" Width="94px"></asp:TextBox>
                            <cc1:CalendarExtender ID="txtrcvdate_CalendarExtender" runat="server" Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txtfromdate"></cc1:CalendarExtender>
                        </div>
                        <div class="col-md-1">
                            <asp:Label ID="Label1" runat="server" CssClass=" smLbl_to" EnableViewState="False" Text="To"></asp:Label>
                            <asp:TextBox ID="txttodate" runat="server" CssClass="form-control form-control-sm" Width="94px"></asp:TextBox>
                            <cc1:CalendarExtender ID="CalendarExtender1" runat="server" Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txttodate"></cc1:CalendarExtender>

                        </div>
                        <div class=" col-md-1">
                            <asp:LinkButton ID="lbtnOk" runat="server" Style="margin-top: 20px;" CssClass="btn btn-sm btn-primary okBtn" OnClick="lbtnOk_Click">Ok</asp:LinkButton>
                        </div>
                        <div class="col-md-2 pading5px asitCol4">
                            <asp:Label ID="Label2" runat="server" CssClass="lblTxt lblName" Text="Approved"></asp:Label>
                            <asp:TextBox ID="txtAppDate" runat="server" AutoCompleteType="Disabled" CssClass="form-control form-control-sm"></asp:TextBox>
                            <cc1:CalendarExtender ID="CalendarExtender2" runat="server"
                                Format="dd-MMM-yyyy" TargetControlID="txtAppDate" Enabled="true"></cc1:CalendarExtender>
                        </div>

                        <div class="col-md-2 ">
                            <div class="form-group">
                                <label class="control-label" for="ToBuyername">Buyer Name</label>

                                <%--  <select multiple id="ddlBuyer" class="form-control" runat="server">
                                                </select>--%>
                                <div class="Multidropdown" style="border: 1px solid #c6c9d5 !important; border-radius: 5px; height: 30px;">
                                    <asp:ListBox ID="ddlBuyer" SelectionMode="Multiple" CssClass="form-control form-control-sm " runat="server"></asp:ListBox>

                                </div>
                            </div>
                            <%-- <div class="col-md-1">
                                            <asp:LinkButton ID="" CssClass="btn btn-primary  primaryBtn" runat="server" OnClick="lbtnOk_Click" TabIndex="2"> Ok </asp:LinkButton>

                                        </div>--%>
                        </div>



                    </div>
                </div>
            </div>
            <div class="card card-fluid">
                <div class="card-body" style="min-height: 450px">
                    <div class="table table-responsive">
                        <asp:MultiView ID="Multivew" runat="server">
                            <asp:View ID="ProdTop1" runat="server">
                                <asp:GridView ID="gvCollectionAll" runat="server" AutoGenerateColumns="False" OnRowDataBound="gvCollectionAll_RowDataBound"
                                    ShowFooter="True" Width="689px" CssClass="table-striped table-hover table-bordered grvContentarea">
                                    <RowStyle />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sl">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvDSlNo" runat="server" Font-Bold="True" Style="text-align: right"
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="Voucher#" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvvoucher" runat="server" Font-Bold="True" Style="text-align: right"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "vounum"))%>' Width="30px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="A/C No" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvcentrid" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "centrid")) %>'
                                                    Width="90px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Cust Id" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvcustid" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "custid")) %>'
                                                    Width="90px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Ref #" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvMemono" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "memono")) %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="Date">
                                            <ItemTemplate>
                                                <asp:Label ID="lnkgvdate" runat="server"
                                                    Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "mrdat")).ToString("dd-MMM-yyyy") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Order Description">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvcactdesc" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "centrdesc") %>'
                                                    Width="280px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Ref #">
                                            <HeaderTemplate>
                                                <asp:TextBox ID="txtSearchRef" BackColor="Transparent" BorderStyle="None" runat="server" Width="70px" placeholder="Ref #" onkeyup="Search_Gridview(this,3, 'gvCollectionAll')"></asp:TextBox><br />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvrefno" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "memono1")) %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvresdesc" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "name")) %>'
                                                    Width="100px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="FC </br>Amount">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvfcamt" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "fcamt")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="75px" Style="text-align: right"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFfcamt" runat="server" Font-Bold="True" Font-Size="12px"
                                                    Style="text-align: right" Width="75px"></asp:Label>
                                            </FooterTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="BDT </br>Amount">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvtrnamount" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amount")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="75px" Style="text-align: right"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFtrnamount" runat="server" Font-Bold="True" Font-Size="12px"
                                                    Style="text-align: right" Width="75px"></asp:Label>
                                            </FooterTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="FC </br>Bank Charge">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvfcbnkcharge" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "fcbnkcharge")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="75px" Style="text-align: right"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFfcbnkcharge" runat="server" Font-Bold="True" Font-Size="12px"
                                                    Style="text-align: right" Width="75px"></asp:Label>
                                            </FooterTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="BDT </br>Bank Charge">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvvatamt" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "vatamt")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="75px" Style="text-align: right"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFvatamt" runat="server" Font-Bold="True" Font-Size="12px"
                                                    Style="text-align: right" Width="75px"></asp:Label>
                                            </FooterTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="BDT </br>Gain/Loss">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvcglamt" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "cglamt")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="65px" Style="text-align: right"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFcglamt" runat="server" Font-Bold="True" Font-Size="12px"
                                                    Style="text-align: right" Width="65px"></asp:Label>
                                            </FooterTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Pay Type" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvpaytype" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "paytype")) %>'
                                                    Width="80px" Font-Bold="true"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Cheque No" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvchequeno" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "chqno")) %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Bank Name" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvbnknam" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "bnknam")) %>'
                                                    Width="150px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Branch Name" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvbbranch" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "bbranch")) %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Pay Date" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lnkgvpaydat" runat="server"
                                                    Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "paydat")).ToString("dd-MMM-yyyy") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="User">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvuser" Font-Size="8px" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "username")) %>'
                                                    Width="50px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="">
                                            <ItemTemplate>
                                                <asp:HyperLink ID="lbtnEdit" runat="server" CssClass="btn btn-xs btn-success"><span class="glyphicon glyphicon-edit"></span></asp:HyperLink>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="">
                                            <ItemTemplate>
                                                <asp:HyperLink ID="LbtnPrint" runat="server" CssClass="btn btn-xs btn-success"><span class="glyphicon glyphicon-print"></span></asp:HyperLink>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="">
                                            <ItemTemplate>

                                                <asp:LinkButton OnClick="lnkCheck_Click" OnClientClick="return confirm('Do You Agree to Approve?')" ID="lnkCheck" runat="server" CssClass="btn btn-xs btn-success"><span class="fa fa-check"></span></asp:LinkButton>

                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Remarks">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvremarks" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "remarks")) %>'
                                                    Width="120px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                    </Columns>
                                    <EditRowStyle />
                                    <AlternatingRowStyle />
                                    <PagerStyle CssClass="gvPagination" />
                                    <HeaderStyle CssClass="grvHeader" />
                                    <FooterStyle CssClass="grvFooter" />
                                </asp:GridView>
                            </asp:View>

                        </asp:MultiView>


                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>





