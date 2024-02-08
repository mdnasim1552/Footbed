<%@ Page Title="" Language="C#" MasterPageFile="~/SPE.Master" AutoEventWireup="true" CodeBehind="AccOnlinePaymnt.aspx.cs" Inherits="SPEWEB.F_15_DPayReg.AccOnlinePaymnt" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script language="javascript" type="text/javascript">
        $(document).ready(function () {
            //For navigating using left and right arrow of the keyboard
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
        .grvHeader th {
            text-align: center;
        }
    </style>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="card card-fluid">
                <div class="card-body">
                    <div class="row">
                        <div class="col-md-1 col-sm-1 col-lg-1">
                            <div class="form-group">
                                <asp:Label ID="Label14" runat="server" CssClass="label">From Date</asp:Label>
                                <asp:TextBox ID="txtfDate" runat="server" CssClass="form-control form-control-sm small"></asp:TextBox>
                                <cc1:CalendarExtender ID="txtfDate_CalendarExtender2" runat="server" Format="dd-MMM-yyyy" TargetControlID="txtfDate"></cc1:CalendarExtender>
                            </div>
                        </div>
                         <div class="col-md-1 col-sm-1 col-lg-1">
                            <div class="form-group">
                                <asp:Label ID="Label15" runat="server" CssClass="label">To Date</asp:Label>
                                <asp:TextBox ID="txttDate" runat="server" CssClass="form-control form-control-sm small"></asp:TextBox>
                                <cc1:CalendarExtender ID="txttDate_CalendarExtender2" runat="server" Format="dd-MMM-yyyy" TargetControlID="txttDate"></cc1:CalendarExtender>
                            </div>
                        </div>
                         <div class="col-md-1 col-sm-1 col-lg-1">
                            <div class="form-group">
                                <asp:Label ID="Label13" runat="server" CssClass="label">Receive Date</asp:Label>
                                <asp:TextBox ID="txtReceiveDate" runat="server" CssClass="form-control form-control-sm small"></asp:TextBox>
                                <cc1:CalendarExtender ID="txtReceiveDate_CalendarExtender2" runat="server" Format="dd-MMM-yyyy" TargetControlID="txtReceiveDate"></cc1:CalendarExtender>
                            </div>
                        </div>

                        <div class="col-md-2 col-sm-2 col-lg-2 ">
                            <div class="form-group">
                                <asp:Label ID="Label6" runat="server" CssClass="label">Resource Head</asp:Label>
                                <asp:DropDownList ID="ddlResourceHead" runat="server" OnSelectedIndexChanged="ddlResourceHead_SelectedIndexChanged" CssClass="chzn-select form-control form-control-sm" AutoPostBack="true"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-2 col-sm-2 col-lg-2">
                            <div class="form-group">
                                <asp:LinkButton ID="LinkButton1" runat="server" CssClass="label" OnClick="ibtnBillNo_Click">Bill No</asp:LinkButton>
                                <asp:LinkButton ID="ibtnSync" runat="server" CssClass="label" OnClick="ibtnSync_Click">Sync</asp:LinkButton>
                                <asp:DropDownList ID="ddlBillList" runat="server" CssClass="chzn-select form-control form-control-sm"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-2 col-sm-2 col-lg-2 ">
                            <div class="form-group" style="margin-top: 20px;">
                                <asp:CheckBox ID="chkSinglIssue" runat="server" Text="Multi Bill" Checked="false" Style="margin-left: 30px" CssClass=" checkbox" AutoPostBack="true" OnCheckedChanged="chkSinglIssue_CheckedChanged" />

                            </div>


                        </div>
                        <div class="col-md-2 col-sm-2 col-lg-2 ">
                            <div class="form-group" style="margin-top: 20px;">
                                <asp:LinkButton ID="lbtnAddTable" runat="server" CssClass="btn btn-primary btn-sm pull-left" OnClick="lbtnAddTable_Click">Add Bill</asp:LinkButton>
                                <asp:LinkButton ID="btnAllBill" runat="server" CssClass="btn btn-primary btn-sm pull-left" OnClick="btnAllBill_Click">All Bill</asp:LinkButton>
 
                                </div>
                        </div>
                      
                        <div class="col-md-1 col-sm-1 col-lg-1 ">
                            <div class="form-group" style="margin-top: 20px;">
                                <asp:LinkButton ID="lbtnRefresh" runat="server" CssClass="btn btn-danger btn-sm pull-left" OnClick="lbtnRefresh_Click">Reset</asp:LinkButton>
                            </div>
                        </div>

                        <div class="col-md-3 col-sm-3 col-lg-3 ">
                            <asp:RadioButtonList runat="server" ID="rblpaytype" CssClass="rbtnList1 chkBoxControl pull-right" RepeatDirection="Horizontal" Visible="false">


                                <asp:ListItem Value="Resource" Selected="True">Resource</asp:ListItem>
                            </asp:RadioButtonList>

                            <asp:Label ID="Label3" runat="server" CssClass="lblTxt lblName" Visible="false" Text="Total Payment"></asp:Label>
                            <asp:TextBox ID="txtrecordno" runat="server" AutoCompleteType="Disabled" Visible="false" CssClass="inputTxt inputDateBox"></asp:TextBox>



                            <asp:Label ID="lblmslnum" runat="server" Visible="False"></asp:Label>
                            <asp:Label ID="lblslnum" runat="server" Visible="False"></asp:Label>

                        </div>

                    </div>
                </div>
            </div>




               <div class="card card-fluid">
                <div class="card-body">
            <div class="row" style="min-height:350px;" > 

                <asp:GridView ID="gvPayment" runat="server" AutoGenerateColumns="False" ShowFooter="True"
                    Style="margin-top: 0px" Width="831px" CssClass=" table-striped table-hover table-bordered grvContentarea"
                    OnRowCancelingEdit="gvPayment_RowCancelingEdit"
                    OnRowEditing="gvPayment_RowEditing" OnRowUpdating="gvPayment_RowUpdating"
                    OnRowDeleting="gvPayment_RowDeleting" OnRowDataBound="gvPayment_RowDataBound">
                    <RowStyle />
                    <Columns>
                        <asp:TemplateField HeaderText="Sl">
                            <ItemTemplate>
                                <asp:Label ID="serialnoid" runat="server" Style="text-align: right" Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>'
                                    Width="15px"></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle Font-Bold="True" Font-Size="12px" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Bill No">
                            <ItemTemplate>
                                <asp:Label ID="lbgvbillno" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                    BorderWidth="0px" Style="text-align: Left; background-color: Transparent" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "billno1")) %>'
                                    Width="75px"></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle Font-Bold="True" Font-Size="12px" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Bill Id">
                            <ItemTemplate>
                                <asp:Label ID="lbgvmslnum" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                    BorderWidth="0px" Style="text-align: Left; background-color: Transparent" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mslnum1")) %>'
                                    Width="65px"></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle Font-Bold="True" Font-Size="12px" />
                        </asp:TemplateField>


                        <asp:TemplateField HeaderText="Payment Id">
                            <ItemTemplate>
                                <asp:Label ID="lbgvslnum" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                    BorderWidth="0px" Style="text-align: Left; background-color: Transparent" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "slnum")) %>'
                                    Width="65px"></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle Font-Bold="True" Font-Size="12px" />
                        </asp:TemplateField>


                        <asp:CommandField CancelText="Can" ShowEditButton="False" UpdateText="Up" />


                        <asp:TemplateField HeaderText="Value Date">
                            <ItemTemplate>
                                <asp:Label ID="lblgvValdate" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                    BorderWidth="0px" Font-Size="11px" Style="text-align: Left; background-color: Transparent"
                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "valdate")) %>'
                                    Width="65px"></asp:Label>


                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Right" />
                            <HeaderStyle Font-Bold="True" Font-Size="12px" />
                        </asp:TemplateField>



                        <asp:TemplateField HeaderText="Received Date">
                        
                            <ItemTemplate>
                                <asp:Label ID="txtgvrcvdate" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                    BorderWidth="0px" Style="text-align: Left; background-color: Transparent; font-size: 11px;" Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "rcvdate")).ToString("dd-MMM-yyy") %>'
                                    Width="80px"></asp:Label>
                            
                            </ItemTemplate>
                            <HeaderStyle Font-Bold="True" Font-Size="12px" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Head of Accounts">

                            <EditItemTemplate>
                                <table style="width: 500px;">
                                    <tr>
                                        <td>
                                            <asp:Panel ID="PnlBill" runat="server" Width="442px">
                                                <div class="form-group">
                                                    <asp:Label ID="Label2" runat="server" CssClass="lblTxt lblName" Text="Receive Date:"></asp:Label>
                                                    <asp:TextBox ID="txteditReceiveDate" runat="server" AutoCompleteType="Disabled"
                                                        AutoPostBack="True" CssClass="inputTxt inputDateBox"></asp:TextBox>

                                                    <cc1:CalendarExtender ID="CalendarExtender1" runat="server"
                                                        Enabled="True" Format="dd.MM.yyyy"
                                                        TargetControlID="txteditReceiveDate"></cc1:CalendarExtender>

                                                    <asp:Label ID="Label4" runat="server" CssClass=" smLbl_to" Text="Bill Ref. No"></asp:Label>
                                                    <asp:TextBox ID="txteditRefno" runat="server" AutoCompleteType="Disabled" CssClass="inputTxt inputDateBox"></asp:TextBox>

                                                    <div class="clearfix"></div>
                                                </div>
                                                <div class="form-group">
                                                    <div class="col-md-3 pading5px asitCol3">
                                                        <asp:Label ID="Label5" runat="server" CssClass="lblTxt lblName" Text="Head of Accounts"></asp:Label>
                                                        <asp:TextBox ID="txtsrchProject" runat="server" AutoCompleteType="Disabled" CssClass="inputTxt inputDateBox" Visible="false"></asp:TextBox>

                                                        <asp:LinkButton ID="ibtnFindProject" CssClass="btn btn-primary srearchBtn" runat="server" Visible="false" OnClick="ibtnFindProject_Click" TabIndex="6"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>


                                                        <div class="clearfix"></div>
                                                    </div>

                                                    <div class="col-md-4 pading5px asitCol3">
                                                        <asp:DropDownList ID="ddlProject" runat="server" AutoPostBack="True" CssClass="inputTxt chzn-select" Width="100%"
                                                            OnSelectedIndexChanged="ddlProject_SelectedIndexChanged" TabIndex="7">
                                                        </asp:DropDownList>
                                                    </div>

                                                </div>

                                                <div class="form-group">
                                                    <div class="col-md-3 pading5px asitCol3">
                                                        <asp:Label ID="lblDetHead" runat="server" CssClass="lblTxt lblName" Text="Details Head:"></asp:Label>
                                                        <asp:TextBox ID="txtsrchRes" runat="server" AutoCompleteType="Disabled" CssClass="inputTxt inputDateBox" Visible="false"></asp:TextBox>

                                                        <asp:LinkButton ID="ibtnRes" CssClass="btn btn-primary srearchBtn" runat="server" Visible="false" OnClick="ibtnRes_Click" TabIndex="6"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>


                                                        <div class="clearfix"></div>
                                                    </div>
                                                    <div class="col-md-4 pading5px asitCol3">
                                                        <asp:DropDownList ID="ddlRescode" runat="server" AutoPostBack="True" CssClass="inputTxt chzn-select" Width="100%" TabIndex="7">
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <div class="col-md-12 pading5px">
                                                        <asp:Label ID="Label6" runat="server" CssClass="lblTxt lblName" Text="Appr. Amount"></asp:Label>
                                                        <asp:TextBox ID="txteditBillAmount" runat="server" AutoCompleteType="Disabled" CssClass="inputTxt inputDateBox"></asp:TextBox>

                                                        <asp:Label ID="Label8" runat="server" CssClass=" smLbl_to" Text="Adv. Amount" Visible="false"></asp:Label>
                                                        <asp:TextBox ID="txteditAdvAmt" runat="server" AutoCompleteType="Disabled" CssClass="inputTxt inputDateBox" Visible="false"></asp:TextBox>



                                                    </div>
                                                    <div class="clearfix"></div>
                                                </div>

                                                <div class="form-group">
                                                    <div class="col-md-12 pading5px">
                                                        <asp:Label ID="Label9" runat="server" CssClass="lblTxt lblName" Text="Value Date"></asp:Label>
                                                        <asp:Label ID="lbleditValDate" runat="server" AutoPostBack="true" AutoCompleteType="Disabled" CssClass="inputTxt inputDateBox"></asp:Label>



                                                        <asp:Label ID="Label10" runat="server" CssClass=" smLbl_to" Text="Pay Date"></asp:Label>
                                                        <asp:TextBox ID="txteditpaymentdate" runat="server" AutoCompleteType="Disabled" CssClass="inputTxt inputDateBox"
                                                            AutoPostBack="True"></asp:TextBox>
                                                        <cc1:CalendarExtender ID="txteditpaymentdate_CalendarExtender" runat="server"
                                                            Enabled="True" Format="dd.MM.yyyy"
                                                            TargetControlID="txteditpaymentdate"></cc1:CalendarExtender>


                                                    </div>
                                                    <div class="clearfix"></div>
                                                </div>

                                                <div class="form-group" style="display: none;">
                                                    <div class="col-md-3 pading5px asitCol3">
                                                        <asp:Label ID="Label11" runat="server" CssClass="lblTxt lblName" Text="Pay To"></asp:Label>
                                                        <asp:TextBox ID="txtSrhParty" runat="server" AutoCompleteType="Disabled" CssClass="inputTxt inputDateBox"></asp:TextBox>

                                                        <asp:LinkButton ID="ibtnFindParty" CssClass="btn btn-primary srearchBtn" runat="server" OnClick="ibtnFindParty_Click" TabIndex="2"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>



                                                    </div>
                                                    <div class="col-md-3 pading5px asitCol3">
                                                        <asp:DropDownList ID="ddlPartyName" runat="server" CssClass=" ddlistPull" Width="100%">
                                                        </asp:DropDownList>

                                                    </div>
                                                    <div class="clearfix"></div>
                                                </div>
                                                <div class="form-group" style="display: none;">
                                                    <div class="col-md-3 pading5px asitCol3">
                                                        <asp:Label ID="Label12" runat="server" CssClass="lblTxt lblName" Text="Bill Nature"></asp:Label>
                                                        <asp:TextBox ID="txtsrchnature" runat="server" AutoCompleteType="Disabled" CssClass="inputTxt inputDateBox"></asp:TextBox>

                                                        <asp:LinkButton ID="ibtnnature" CssClass="btn btn-primary srearchBtn" runat="server" OnClick="ibtnnature_Click" TabIndex="2"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>



                                                    </div>
                                                    <div class="col-md-3 pading5px asitCol3">
                                                        <asp:DropDownList ID="ddlBillNature" runat="server" CssClass=" ddlistPull" Width="100%">
                                                        </asp:DropDownList>

                                                    </div>
                                                    <div class="clearfix"></div>
                                                </div>
                                                <div class="form-group">
                                                    <asp:LinkButton ID="lbtnGrdUpdate" runat="server"
                                                        OnClick="lbtnGrdUpdate_Click"
                                                        CssClass="btn btn-danger primaryBtn"
                                                        TabIndex="14">Add</asp:LinkButton>
                                                </div>

                                            </asp:Panel>
                                        </td>
                                    </tr>
                                </table>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lbgvactdesc" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                    BorderWidth="0px" Style="text-align: Left; background-color: Transparent" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc")) %>'
                                    Width="120px"></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle Font-Bold="True" Font-Size="12px" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Details Head">

                            <ItemTemplate>
                                <asp:Label ID="lbgvResdesc" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                    BorderWidth="0px" Style="text-align: Left; background-color: Transparent" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "resdesc")) %>'
                                    Width="170px"></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle Font-Bold="True" Font-Size="12px" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Ref. No">
                            <ItemTemplate>
                                <asp:TextBox ID="txtgvref" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                    BorderWidth="0px" Font-Size="11px" Style="text-align: Left; background-color: Transparent"
                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "refno")) %>'
                                    Width="50px"></asp:TextBox>
                            </ItemTemplate>
                            <HeaderStyle Font-Bold="True" Font-Size="12px" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Bill Amount">
                            <FooterTemplate>
                                <asp:Label ID="txtFTotal" runat="server" ForeColor="#000"></asp:Label>
                            </FooterTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblgvbillamt" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                    BorderWidth="0px" Font-Size="11px" Style="text-align: right; background-color: Transparent"
                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "billamt1")).ToString("#,##0;(#,##0); ") %>'
                                    Width="70px"></asp:Label>
                            </ItemTemplate>
                            <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="right" VerticalAlign="Middle" />
                            <ItemStyle HorizontalAlign="Right" />
                        </asp:TemplateField>



                        <asp:TemplateField HeaderText="">
                            <ItemTemplate>
                                <asp:LinkButton ID="lbok" runat="server" CommandArgument="lbok"
                                    OnClick="Add_Click" Width="30px">Add</asp:LinkButton>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                        </asp:TemplateField>


                        <asp:TemplateField HeaderText="Payment Date">
                            <ItemTemplate>
                                <asp:TextBox ID="txtgvpaymentdate" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                    BorderWidth="0px" Font-Size="11px" Style="text-align: Left; background-color: Transparent"
                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "apppaydate")) %>'
                                    Width="70px"></asp:TextBox>
                                <cc1:CalendarExtender ID="txtgvpaymentdate_CalendarExtender" runat="server"
                                    Enabled="True" Format="dd-MMM-yyyy"
                                    TargetControlID="txtgvpaymentdate"></cc1:CalendarExtender>


                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Right" />
                            <HeaderStyle Font-Bold="True" Font-Size="12px" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Payment Amount">
                            <FooterTemplate>
                                <asp:Label ID="lblgvFtopayamt" runat="server" ForeColor="#000"></asp:Label>
                            </FooterTemplate>
                            <ItemTemplate>
                                <asp:TextBox ID="txtgvpayamt" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                    BorderWidth="0px" Font-Size="11px" Style="text-align: right; background-color: Transparent"
                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt")).ToString("#,##0;(#,##0); ") %>'
                                    Width="70px"></asp:TextBox>
                            </ItemTemplate>
                            <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="right" VerticalAlign="Middle" />
                            <ItemStyle HorizontalAlign="Right" />
                        </asp:TemplateField>



                        <asp:TemplateField HeaderText="Adv. Amt." Visible="false">
                            <FooterTemplate>
                                <asp:Label ID="txtFAdvTotal" runat="server" ForeColor="Black"></asp:Label>
                            </FooterTemplate>
                            <ItemTemplate>
                                <asp:TextBox ID="txtgvAdvamt" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                    BorderWidth="0px" Font-Size="11px" Style="text-align: right; background-color: Transparent"
                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "advamt")).ToString("#,##0;(#,##0); ") %>'
                                    Width="70px"></asp:TextBox>
                            </ItemTemplate>
                            <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="right" VerticalAlign="Middle" />
                            <ItemStyle HorizontalAlign="Right" />
                        </asp:TemplateField>



                        <asp:TemplateField HeaderText="Net Amt." Visible="false">
                            <FooterTemplate>
                                <asp:Label ID="txtFNetTotal" runat="server" ForeColor="Black"></asp:Label>
                            </FooterTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblgvNetamt" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                    BorderWidth="0px" Font-Size="11px" Style="text-align: right; background-color: Transparent"
                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "netamt")).ToString("#,##0;(#,##0); ") %>'
                                    Width="70px"></asp:Label>
                            </ItemTemplate>
                            <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="right" VerticalAlign="Middle" />
                            <ItemStyle HorizontalAlign="Right" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Bill Nature" Visible="false">

                            <ItemTemplate>
                                <asp:Label ID="lbgvbillnaturedsc" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                    BorderWidth="0px" Style="text-align: Left; background-color: Transparent" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "billndesc")) %>'
                                    Width="80px"></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle Font-Bold="True" Font-Size="12px" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Party Name" Visible="false">

                            <ItemTemplate>
                                <asp:Label ID="lbgvpartyname" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                    BorderWidth="0px" Style="text-align: Left; background-color: Transparent" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "paydesc")) %>'
                                    Width="70px"></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle Font-Bold="True" Font-Size="12px" />
                        </asp:TemplateField>
                      




                        <asp:CommandField ShowDeleteButton="True" ControlStyle-ForeColor="Red" DeleteText="<span class='fa fa-trash'></span>" />

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

