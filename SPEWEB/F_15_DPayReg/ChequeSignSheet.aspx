<%@ Page Title="" Language="C#" MasterPageFile="~/SPE.Master" AutoEventWireup="true" CodeBehind="ChequeSignSheet.aspx.cs" Inherits="SPEWEB.F_15_DPayReg.ChequeSignSheet" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript" language="javascript">
        $(document).ready(function () {
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);
        });
        function pageLoaded() {




            $("input, select").bind("keydown", function (event) {
                var k1 = new KeyPress();
                k1.textBoxHandler(event);

            });
            $('.chzn-select').chosen({ search_contains: true });

        }

    </script>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="card card-fluid">
                <div class="card-body">
                    <div class="row">
                        <div class="col-md-1 col-sm-1 col-lg-1 ">
                            <div class="form-group">
                                <asp:Label ID="Label13" runat="server" CssClass="label">Vou Date</asp:Label>
                                <asp:TextBox ID="txtdate" runat="server" CssClass="form-control form-control-sm small"></asp:TextBox>
                                <cc1:CalendarExtender ID="txtdateCalendarExtender2" runat="server" Format="dd-MMM-yyyy" TargetControlID="txtdate"></cc1:CalendarExtender>
                            </div>
                        </div>
                        <div class="col-md-1 col-sm-1 col-lg-1 ">

                            <div class="form-group">
                                <asp:Label ID="lblConTrolCode" runat="server" CssClass="control-label">Ref. No</asp:Label>

                                <div class="input-group input-group-alt">

                                    <asp:TextBox runat="server" ID="txtserchmrf" CssClass="form-control form-control-sm ">
                                    </asp:TextBox>

                                </div>
                            </div>
                        </div>
                        <div class="col-md-2 col-sm-2 col-lg-2 ">
                            <div class="form-group">
                                <asp:LinkButton ID="imgbtnSrchBank" runat="server" CssClass="label" OnClick="imgbtnSrchBank_Click">Bank Name</asp:LinkButton>
                                <asp:DropDownList ID="ddlBankName" runat="server" OnSelectedIndexChanged="ddlBankName_SelectedIndexChanged" CssClass="chzn-select form-control form-control-sm" AutoPostBack="true"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-2 col-sm-2 col-lg-2">
                            <div class="form-group">
                                <asp:LinkButton ID="ImgbtnFindProjectName" runat="server" CssClass="label" OnClick="ImgbtnFindProjectName_Click">Accounts Head</asp:LinkButton>
                                <asp:DropDownList ID="ddlProject" runat="server" CssClass="chzn-select form-control form-control-sm"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-1 col-sm-1 col-lg-1 ">
                            <div class="form-group" style="margin-top: 20px;">
                                <asp:LinkButton ID="lnkOk" runat="server" CssClass="btn btn-primary btn-sm pull-left" OnClick="lnkOk_Click">Ok</asp:LinkButton>
                            </div>
                        </div>


                        <div class="col-md-3 col-sm-3 col-lg-2 ">
                            <div class="form-group">
                                 <asp:Label ID="Label4" runat="server" CssClass="label">Payment Id</asp:Label>
                                <asp:DropDownList ID="ddlBillList" runat="server" AutoPostBack="True" OnSelectedIndexChanged ="lstBillList_SelectedIndexChanged" CssClass="chzn-select form-control form-control-sm"></asp:DropDownList>

                            </div>
                        </div>



                    </div>
                    <div class="row">
                        <div class="col-md-5 col-sm-5 col-lg-5 ">
                            <div class="form-group" style="margin-top: 20px;">
                                <asp:CheckBox ID="chkCrVou" runat="server" Text="Current Voucher" Checked="true" CssClass=" checkbox" />
                                <asp:CheckBox ID="chkPrint" runat="server" Text="Cheque Print" CssClass=" checkbox" />
                                <asp:CheckBox ID="ChboxPayee" runat="server" Text="A/C Payee" CssClass=" checkbox" />

                            </div>

                        </div>
                        <div class="col-md-1 col-sm-1 col-lg-1 ">
                            <div class="form-group">

                                <div class="col-md-4 pading5px">

                                    <asp:Label ID="lblisunum" runat="server" CssClass=" smLbl" Visible="False"></asp:Label>
                                    <asp:Label ID="lblVoun" runat="server" CssClass=" smLbl" Visible="False"></asp:Label>

                                    <asp:Label ID="lblVounum" runat="server" Visible="False"></asp:Label>
                                </div>


                            </div>
                        </div>
                    </div>

                </div>
            </div>
            <div class="card card-fluid">
                <div class="card-body" style="min-height: 350px;">
                    <div class="row" >

                        <asp:GridView ID="dgv1" runat="server" AutoGenerateColumns="False" OnRowDataBound="dgv1_RowDataBound"
                            ShowFooter="True" PageSize="100" CssClass="col-md-12 table-striped table-hover table-bordered grvContentarea fade-in "
                            OnRowCancelingEdit="dgv1_RowCancelingEdit" OnRowEditing="dgv1_RowEditing" OnRowUpdating="dgv1_RowUpdating">
                            <RowStyle />
                            <Columns>
                                <asp:TemplateField HeaderText="Sl">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvSlNo" runat="server" Font-Bold="True" Style="text-align: right"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rowid")) %>'
                                            Width="20px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="slnum" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvslNum" runat="server" Height="16px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "slnum")) %>'
                                            Width="80px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Actcode Code" Visible="False">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvActCod" runat="server" Height="16px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actcode")) %>'
                                            Width="80px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Res Code" Visible="False">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvResCod" runat="server" Height="16px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rescode")) %>'
                                            Width="80px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Cactcode Code" Visible="False">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvCactCod" runat="server" Height="16px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "cactcode")) %>'
                                            Width="80px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Description">
                                    <ItemTemplate>
                                        <asp:HyperLink ID="hlnkAccdesc1" runat="server" Target="_blank" Text='<%# "<B>" + Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc")) + "</B>" +
                                                                         (DataBinder.Eval(Container.DataItem, "resdesc").ToString().Trim().Length>0 ? 
                                                                         (Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc")).Trim().Length>0 ? "<br>" : "") + 
                                                                         "<span class=gvdesc>"+
                                                                         Convert.ToString(DataBinder.Eval(Container.DataItem, "resdesc")).Trim(): "") + "</span>"  %>'
                                            Width="300px">
                                                            
                                                            
                                        </asp:HyperLink>
                                    </ItemTemplate>
                                    <FooterStyle HorizontalAlign="Center" />
                                    <HeaderStyle HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Approved Amount" Visible="false">
                                    <FooterTemplate>
                                        <asp:LinkButton ID="lbtnResFooterTotal" runat="server" Font-Bold="True" Font-Size="14px"
                                            OnClick="lbtnResFooterTotal_Click" CssClass="btn btn-primary primaryBtn">Total :</asp:LinkButton>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvAppAmt" runat="server" Font-Size="11px" Style="text-align: right"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "apamt")).ToString("#,##0;(#,##0); ") %>'
                                            Width="55px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:CommandField CancelText="Can" ShowEditButton="True" UpdateText="Up" />
                                <asp:TemplateField HeaderText="Bank Name">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvCactDesc" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "cactdesc").ToString() %>'
                                            Width="150px"></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:DropDownList ID="ddlCactcode" runat="server" Width="150px">
                                        </asp:DropDownList>
                                    </EditItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" />
                                    <FooterStyle HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Cheque </br> Amount">


                                    <FooterTemplate>
                                        <asp:Label ID="lblgvFchqamt" runat="server" ForeColor="#000"></asp:Label>
                                    </FooterTemplate>



                                    <ItemTemplate>
                                        <asp:TextBox ID="txtgvAmount" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                            BorderWidth="0px" Font-Size="11px" Style="text-align: right; background-color: Transparent"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amount")).ToString("#,##0;-#,##0; ") %>'
                                            Width="60px"></asp:TextBox>
                                    </ItemTemplate>

                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle HorizontalAlign="Right" />

                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Cheque No" Visible="false">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtgvChqNo" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                            BorderWidth="0px" Font-Size="11px" Style="text-align: right; background-color: Transparent"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "chequeno")) %>'
                                            Width="60px"></asp:TextBox>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                    </FooterTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Cheque </br> Date">
                                    <ItemTemplate>
                                        <asp:TextBox ID="lblgvChqdate" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                            BorderWidth="0px" Font-Size="11px" Style="text-align: left; background-color: Transparent"
                                            Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "chqdate")).ToString("dd-MMM-yyyy") %>'
                                            Width="60px"></asp:TextBox>
                                        <cc1:CalendarExtender ID="lblgvChqdate_CalendarExtender" runat="server"
                                        Enabled="True" Format="dd-MMM-yyyy" TargetControlID="lblgvChqdate"></cc1:CalendarExtender>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Voucher </br>Number">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvNewVoNum" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "newvocnum")) %>'
                                            Width="85px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Ref No">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvRefno" runat="server" Height="16px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "refno")) %>'
                                            Width="60px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Remarks">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvRemarks" runat="server" Height="16px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "remarks")) %>'
                                            Width="90px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                            </Columns>


                            <FooterStyle CssClass="grvFooter" />
                            <EditRowStyle />
                            <AlternatingRowStyle />
                            <PagerStyle CssClass="gvPagination" />
                            <HeaderStyle CssClass="grvHeader" />
                        </asp:GridView>


                    </div>
                    </br>
                    <div class="row">

                        <asp:Panel ID="PnlNarration" Visible="false" runat="server">
                            <div class="row">
                                <div class="col-md-2 col-sm-2 col-lg-2 ">
                                    <div class="form-group">
                                        <asp:Label ID="lblchelist" runat="server" CssClass="label">Cheque List</asp:Label>
                                        <asp:DropDownList ID="ddlcheque" runat="server" OnSelectedIndexChanged="ddlcheque_SelectedIndexChanged" CssClass="chzn-select form-control form-control-sm" AutoPostBack="true"></asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-2 col-sm-2 col-lg-2 ">

                                    <div class="form-group">
                                        <asp:Label ID="Label1" runat="server" CssClass="control-label">Cheque No</asp:Label>

                                        <div class="input-group input-group-alt">

                                            <asp:TextBox runat="server" ID="txtRefNum" CssClass="form-control form-control-sm ">
                                            </asp:TextBox>

                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-4 col-sm-4 col-lg-4 ">

                                    <div class="form-group">
                                        <asp:Label ID="Label3" runat="server" CssClass="control-label">Pay To</asp:Label>

                                        <div class="input-group input-group-alt">

                                            <asp:TextBox runat="server" ID="txtPayto" CssClass="form-control form-control-sm ">
                                            </asp:TextBox>

                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-4 col-sm-4 col-lg-4 ">

                                    <div class="form-group">
                                        <asp:Label ID="Label2" runat="server" CssClass="control-label">Narration</asp:Label>

                                        <div class="input-group input-group-alt">

                                            <asp:TextBox runat="server" ID="txtNarration" CssClass="form-control form-control-sm " Rows="2" TextMode="MultiLine">
                                            </asp:TextBox>

                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-1 col-sm-1 col-lg-1 ">
                                    <div class="form-group" style="margin-top: 20px;">
                                      <%--  <asp:LinkButton ID="btnUpdate" runat="server" CssClass="btn btn-primary btn-sm pull-left" OnClick="btnUpdate_Click">Update</asp:LinkButton>--%>
                                        <asp:LinkButton ID="LinkButton3" runat="server" CssClass="btn btn-primary btn-sm pull-left" OnClick="lnkOk_Click" Visible="false">Confirm</asp:LinkButton>

                                    </div>
                                </div>


                            </div>

                        </asp:Panel>
                    </div>
                </div>
            </div>












        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

