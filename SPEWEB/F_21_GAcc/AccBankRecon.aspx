<%@ Page Title="" Language="C#" MasterPageFile="~/SPE.Master" AutoEventWireup="true" CodeBehind="AccBankRecon.aspx.cs" Inherits="SPEWEB.F_21_GAcc.AccBankRecon" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript">

        $(document).ready(function () {
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);

        });
        function pageLoaded() {


            $("input, select").bind("keydown", function (event) {
                var k1 = new KeyPress();
                k1.textBoxHandler(event);

            });

            $('.chzn-select').chosen({ search_contains: true });

            var gv = $('#<%=this.gv1.ClientID %>');
            gv.Scrollable();
        }
    </script>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="nahidProgressbar">
                <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1" DisplayAfter="30">
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
                <div class="card-body" style =" min-height : 80px">
                    <div class="row">
                       
                        <div class="col-md-1 col-sm-1 col-lg-1">
                          <asp:Label ID="Label6" runat="server" 
                                Text="From" CssClass="smLbl_to" Visible="true"></asp:Label>

                            <asp:TextBox ID="TxtDate1" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
                            <cc1:CalendarExtender ID="ClndrExtDate1" runat="server"
                                Format="dd-MMM-yyyy" TargetControlID="TxtDate1"></cc1:CalendarExtender>



                        </div> 
                        <div class="col-md-1 col-sm-1 col-lg-1">
                         
                            <asp:Label ID="Label7" runat="server" 
                                Text="To" CssClass="smLbl_to" Visible="true"></asp:Label>

                            <asp:TextBox ID="TxtDate2" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
                            <cc1:CalendarExtender ID="ClndrExtDate2" runat="server"
                                Format="dd-MMM-yyyy" TargetControlID="TxtDate2"></cc1:CalendarExtender>

                        </div>
                         <div class="col-md-2 col-sm-2 col-lg-2">
                            <asp:Label ID="Label1" runat="server"
                                Text="Bank Name" CssClass="lblTxt lblName"></asp:Label>

                            <asp:DropDownList ID="DDListBank" runat="server" AutoPostBack="True" CssClass="chzn-select form-control form-control-sm">
                            </asp:DropDownList>

                        </div>  
                        <div class="col-md-1 col-sm-1 col-lg-1" style="margin-top: 20px;">
                            <asp:LinkButton ID="lbtnGetData" runat="server"
                                OnClick="lbtnGetData_Click" CssClass="btn btn-sm btn-primary primaryBtn">Ok</asp:LinkButton>
                        </div> 
                        <div class="col-md-2 col-sm-2 col-lg-2" >
                             <asp:Label ID="lblChqNo" runat="server" CssClass="lblTxt lblName"
                                Text="Cheque No"></asp:Label>

                            <asp:TextBox ID="txtChqSearch" runat="server" CssClass="inputtextbox"></asp:TextBox>
                        </div>
                        <div class="col-md-1 col-sm-1 col-lg-1" >
                           
                            <asp:Label ID="lblPage" runat="server" CssClass="smLbl_to" Font-Bold="True"
                                Text="Size:"></asp:Label>

                            <asp:DropDownList ID="ddlpagesize" runat="server" AutoPostBack="True" CssClass="chzn-select form-control form-control-sm"
                                OnSelectedIndexChanged="ddlpagesize_SelectedIndexChanged" >
                                <asp:ListItem Value="10">10</asp:ListItem>
                                <asp:ListItem Value="15">15</asp:ListItem>
                                <asp:ListItem Value="20">20</asp:ListItem>
                                <asp:ListItem Value="30">30</asp:ListItem>
                                <asp:ListItem Value="50">50</asp:ListItem>
                                <asp:ListItem Value="100">100</asp:ListItem>
                                <asp:ListItem Value="150">150</asp:ListItem>
                                <asp:ListItem Value="200">200</asp:ListItem>
                                <asp:ListItem Value="300">300</asp:ListItem>
                            </asp:DropDownList>
                        </div>

                    </div>


                </div>
            </div>


            <div class="card card-fluid">
                <div class="card-body">
                    <asp:GridView ID="gv1" runat="server" AllowPaging="True"
                        CssClass=" table-striped table-hover table-bordered grvContentarea"
                        AutoGenerateColumns="False" OnPageIndexChanging="gv1_PageIndexChanging" OnRowDataBound="gv1_RowDataBound"
                        ShowFooter="True" Width="104px" PageSize="20">
                        <RowStyle />


                        <Columns>
                            <asp:TemplateField HeaderText="Sl.">
                                <ItemTemplate>
                                    <asp:Label ID="serialnoid" runat="server"
                                        Style="text-align: right; font-size: 11px;"
                                        Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle Font-Bold="True" Font-Size="14px" />
                            </asp:TemplateField>



                            <asp:TemplateField HeaderText="Sub Code" Visible="False">
                                <ItemTemplate>
                                    <asp:Label ID="lblSUBCODE" runat="server" Font-Size="11px"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rescode")) %>'
                                        Width="20px"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="VOUNUM" Visible="False">
                                <ItemTemplate>


                                    <asp:HyperLink ID="lblVOUNUM" runat="server" Font-Size="12px"
                                        CssClass="GridLebelL" Font-Underline="False" Target="_blank"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "vounum")) %>'
                                        Width="75px"></asp:HyperLink>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Recon.Date (dd.mm.yyyy)">
                                <EditItemTemplate>
                                </EditItemTemplate>
                                <ItemTemplate>


                                    <asp:TextBox ID="txtgvRECNDT" runat="server" Font-Size="11px"
                                        Text='<%# (Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "recndt")).Year==1900? "" :Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "recndt")).ToString("dd.MM.yyyy")) %>'
                                        Width="85px" BackColor="Transparent" BorderStyle="None"></asp:TextBox>

                                    <cc1:CalendarExtender ID="txtgvRECNDT_CalendarExtender" runat="server"
                                        Enabled="True" Format="dd.MM.yyyy" TargetControlID="txtgvRECNDT"></cc1:CalendarExtender>

                                </ItemTemplate>

                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Cheq.">
                                
                                <ItemTemplate>
                                    <asp:Label ID="lblREFNO" runat="server" Font-Size="11px"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "refnum")) %>'
                                        Width="80px"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField> 
                            <asp:TemplateField HeaderText=" Ref. No.">
                                
                                <ItemTemplate>
                                    <asp:Label ID="lblsrinfo" runat="server" Font-Size="11px"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "srinfo")) %>'
                                        Width="80px"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Vou.Date">
                                <ItemTemplate>


                                    <asp:Label ID="lblVOUDAT" runat="server" Font-Size="11px"
                                        Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "voudat")).ToString("dd-MMM-yyyy") %>'
                                        Width="66px"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Vou.No.">
                                <ItemTemplate>


                                    <asp:HyperLink ID="lblVOUNUM1" runat="server" Font-Size="12px"
                                        CssClass="GridLebelL" Font-Underline="False" Target="_blank"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "vounum1")) %>'
                                        Width="75px"></asp:HyperLink>

                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Deposit">
                                <ItemTemplate>
                                    <asp:Label ID="lblgvPayment" runat="server" Font-Size="11px"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "depam")).ToString("#,##0.00;(#,##0.00); ") %>'
                                        Width="70px"></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:TemplateField>



                            <asp:TemplateField HeaderText="Payment">
                                <ItemTemplate>
                                    <asp:Label ID="lblgvDeposit" runat="server" Font-Size="11px"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "payam")).ToString("#,##0.00;(#,##0.00); ") %>'
                                        Width="70px"></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Accounts Head">

                                <ItemTemplate>
                                    <asp:Label ID="lblTRANSDES" runat="server" Font-Size="11px"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc")) %>'
                                        Width="160px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Left" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Details Head">
                                <ItemTemplate>
                                    <asp:Label ID="lblgvDetailsHead" runat="server" Font-Size="11px"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "resdesc1")) %>'
                                        Width="200px"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Narration">
                                <ItemTemplate>
                                    <asp:Label ID="lblgvVarnar" runat="server" Font-Size="11px"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "venar")) %>'
                                        Width="220px"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Rpcode" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblgvRpCode" runat="server" Font-Size="11px"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rpcode")) %>'
                                        Width="100px"></asp:Label>
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
            </div>

        </ContentTemplate>

    </asp:UpdatePanel>
</asp:Content>

