<%@ Page Title="" Language="C#" MasterPageFile="~/SPE.Master" AutoEventWireup="true" CodeBehind="EmpLoanInfo.aspx.cs" Inherits="SPEWEB.F_81_Hrm.F_85_Lon.EmpLoanInfo" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript" language="javascript">

        $(document).ready(function () {

            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);

        });

        function pageLoaded() {


            $('.chzn-select').chosen({ search_contains: true });

        }

    </script>




    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>

            <div class="card card-fluid">
                <div class="card-body">
                    <div class="row">
                        <div class="col-md-2 col-sm-2 col-lg-2 ">
                            <div class="form-group">
                                <asp:Label ID="Label6" runat="server" CssClass="label">Employee Type</asp:Label>
                                <asp:DropDownList ID="ddlWstation" runat="server" OnSelectedIndexChanged="ddlWstation_SelectedIndexChanged" CssClass="chzn-select form-control form-control-sm" AutoPostBack="true"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-2 col-sm-2 col-lg-2">
                            <div class="form-group">
                                <asp:Label ID="Label7" runat="server" CssClass="label">Division</asp:Label>
                                <asp:DropDownList ID="ddlDivision" runat="server" OnSelectedIndexChanged="ddlDivision_SelectedIndexChanged" CssClass="chzn-select form-control form-control-sm" AutoPostBack="true"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-2 col-sm-2 col-lg-2 ">
                            <div class="form-group">
                                <asp:Label ID="Label8" runat="server" CssClass="label">Department</asp:Label>
                                <asp:DropDownList ID="ddlDept" runat="server" OnSelectedIndexChanged="ddlDept_SelectedIndexChanged" CssClass="chzn-select form-control form-control-sm" AutoPostBack="true"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-2 col-sm-2 col-lg-2 ">
                            <div class="form-group">
                                <asp:Label ID="Label11" runat="server" CssClass="label">Section</asp:Label>
                                <asp:DropDownList ID="ddlSection" runat="server" CssClass="chzn-select form-control form-control-sm" OnSelectedIndexChanged="ddlSection_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-2 col-sm-2 col-lg-2 ">
                            <div class="form-group">
                                <asp:LinkButton ID="ibtnEmpList" runat="server" CssClass="label" OnClick="ibtnEmpList_Click">Emp. Name</asp:LinkButton>
                                <asp:DropDownList ID="ddlEmpList" runat="server" CssClass="chzn-select form-control form-control-sm"></asp:DropDownList>
                                <asp:Label ID="lblEmpName" runat="server" Visible="false" Width="280" CssClass="form-control inputTxt pull-left"></asp:Label>

                            </div>
                        </div>


                    </div>
                    <div class="row">
                        <div class="col-md-2 col-sm-2 col-lg-2 ">
                            <div class="form-group">
                                <asp:Label ID="Label9" runat="server" CssClass="label">Loan Date</asp:Label>
                                <asp:TextBox ID="txtCurDate" runat="server" CssClass="form-control form-control-sm "></asp:TextBox>
                                <cc1:CalendarExtender ID="txtCurDate_CalendarExtender" runat="server" Format="dd-MMM-yyyy" TargetControlID="txtCurDate"></cc1:CalendarExtender>
                            </div>
                        </div>
                        <div class="col-md-2 col-sm-2 col-lg-2 ">
                            <div class="form-group">
                                <asp:Label ID="trnsferNo" runat="server" CssClass="label">Loan No</asp:Label>
                                <asp:Label ID="lblCurNo1" runat="server" CssClass=" smLbl_to"></asp:Label>
                                <asp:Label ID="lblCurNo2" runat="server" CssClass="form-control form-control-sm "></asp:Label>

                            </div>
                        </div>
                        <div class="col-md-2 col-sm-2 col-lg-2 ">
                            <div class="form-group">
                                <asp:Label ID="Label10" runat="server" CssClass="label">Loan Type</asp:Label>
                                <asp:DropDownList ID="ddlLoanType" runat="server" CssClass="chzn-select form-control form-control-sm"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-1 col-sm-1 col-lg-1 ">
                            <div class="form-group" style="margin-top: 20px;">
                                <asp:LinkButton ID="lbtnOk" runat="server" CssClass="btn btn-primary btn-sm pull-left" OnClick="lbtnOk_Click">Ok</asp:LinkButton>
                            </div>
                        </div>
                        <div class="col-md-2 col-sm-2 col-lg-2 ">
                            <div class="form-group">
                                <asp:LinkButton ID="lbtnPrevLoanList" runat="server" CssClass="control-label" OnClick="lbtnPrevLoanList_Click">Previous List</asp:LinkButton>
                                <asp:DropDownList ID="ddlPrevLoanList" runat="server" CssClass="form-control form-control-sm">
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>


                </div>
            </div>
            <div class="card card-fluid">
                <div class="card-body" style="min-height: 350px;">
                    <asp:Panel ID="pnlloan" runat="server" Visible="False">
                        <div class="row">

                            <div class="col-md-12 col-sm-12 col-lg-12 ">
                                <div class=" form-group">
                                    <div class="col-md-12 pading5px">
                                        <asp:Label ID="Label1" runat="server" CssClass="lblTxt lblName">Total Amount</asp:Label>
                                        <asp:TextBox ID="txtToamt" runat="server" CssClass="inputTxt inputName inpPixedWidth"></asp:TextBox>
                                        <asp:Label ID="Label2" runat="server" CssClass=" smLbl_to">Ins. Amount</asp:Label>
                                        <asp:TextBox ID="txtinsamt" runat="server" CssClass="inputTxt inputName inpPixedWidth"></asp:TextBox>
                                        <asp:Label ID="Label3" runat="server" CssClass=" smLbl_to">Start Date</asp:Label>
                                        <asp:TextBox ID="txtstrdate" runat="server" CssClass="inputTxt inputName inpPixedWidth"></asp:TextBox>
                                        <cc1:CalendarExtender ID="txtstrdate_CalendarExtender" runat="server"
                                            Format="dd-MMM-yyyy" TargetControlID="txtstrdate"></cc1:CalendarExtender>
                                        <asp:Label ID="Label4" runat="server" CssClass=" smLbl_to">Duration</asp:Label>
                                        <asp:DropDownList ID="ddlMonth" runat="server" CssClass="ddlPage" AppendDataBoundItems="True"
                                            Width="100px">
                                            <asp:ListItem Value="1">1 Month</asp:ListItem>
                                            <asp:ListItem Value="2">2 Month</asp:ListItem>
                                            <asp:ListItem Value="3 ">3 Month</asp:ListItem>
                                            <asp:ListItem Value="4">4 Month</asp:ListItem>
                                            <asp:ListItem Value="5 ">5 Month</asp:ListItem>
                                            <asp:ListItem Value="6">6 Month</asp:ListItem>
                                            <asp:ListItem Value="7">7  Month</asp:ListItem>
                                            <asp:ListItem Value="8">8  Month</asp:ListItem>
                                            <asp:ListItem Value="9">9  Month</asp:ListItem>
                                            <asp:ListItem Value="10">10  Month</asp:ListItem>
                                            <asp:ListItem Value="11">11  Month</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:LinkButton ID="lbtnGenerate" runat="server" CssClass="btn btn-danger btn-sm pull-left" OnClick="lbtnGenerate_Click">Generate</asp:LinkButton>
                                    </div>
                                </div>

                            </div>


                        </div>
                    </asp:Panel>

                    <div class="row">
                        <div class="col-md-12">
                            <div class="form-group">
                                <asp:CheckBox ID="chkVisible" runat="server" AutoPostBack="True" CssClass=" checkbox"
                                    OnCheckedChanged="chkVisible_CheckedChanged" Text="Gen. Installment"
                                    Visible="False" />
                                <asp:CheckBox ID="chkAddIns" runat="server" AutoPostBack="True"
                                    Text="Add.Installment" CssClass=" checkbox"
                                    Visible="False" OnCheckedChanged="chkAddIns_CheckedChanged" />
                                <asp:LinkButton ID="lbtnAddInstallment" runat="server" OnClick="lbtnAddInstallment_Click"
                                    Visible="False" CssClass="btn btn-primary primaryBtn">Add</asp:LinkButton>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <asp:GridView ID="gvloan" runat="server" AutoGenerateColumns="False"
                            ShowFooter="True" CssClass="table-striped table-hover table-bordered grvContentarea">
                            <RowStyle />
                            <Columns>
                                <asp:TemplateField HeaderText="Sl.No." FooterText="Total ">
                                  
                                    <ItemTemplate>
                                        <asp:Label ID="serialnoid0" runat="server" Style="text-align: right"
                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle Font-Bold="True" Font-Size="12px" ForeColor="#000" />

                                </asp:TemplateField>



                                <asp:TemplateField HeaderText="Installment Date.">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtgvinstdate" runat="server" Style="text-align: left; font-size: 11px;"
                                            Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "lndate")).ToString("dd-MMM-yyyy") %>'
                                            Width="80px" BackColor="Transparent" BorderStyle="None"></asp:TextBox>
                                        <cc1:CalendarExtender ID="txtgvinstdate_CalendarExtender" runat="server"
                                            Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txtgvinstdate"></cc1:CalendarExtender>
                                    </ItemTemplate>
                                 <%--   <FooterTemplate>
                                        <asp:LinkButton ID="lbtnFinalUpdate" runat="server" Font-Bold="True"
                                            CssClass="btn btn-danger primaryBtn" OnClick="lbtnFinalUpdate_Click">Update</asp:LinkButton>
                                    </FooterTemplate>--%>

                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Installment Amt.">
                                    <FooterTemplate>
                                        <asp:Label ID="gvlFToamt" runat="server" Font-Bold="True" Font-Size="12px"
                                            ForeColor="#000" Style="text-align: right" Width="120px"></asp:Label>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:TextBox ID="gvtxtamt" runat="server" Style="text-align: right"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "lnamt")).ToString("#,##0;(#,##0); ") %>'
                                            Width="120px" BackColor="Transparent" BorderStyle="None" Font-Size="11px"></asp:TextBox>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="right" />
                                    <FooterStyle HorizontalAlign="Right" />

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

