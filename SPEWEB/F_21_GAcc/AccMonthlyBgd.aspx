<%@ Page Title="" Language="C#" MasterPageFile="~/SPE.Master" AutoEventWireup="true" CodeBehind="AccMonthlyBgd.aspx.cs" Inherits="SPEWEB.F_21_GAcc.AccMonthlyBgd" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript">
        $(document).ready(function () {
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);
        });

        function pageLoaded() {

            $('.chzn-select').chosen({ search_contains: true });
        }
    </script>

    <style type="text/css">
        /*.style32 {
            width: 74px;
        }

        .  {
            color: #FFFFFF;
        }

        .style22 {
            width: 50px;
        }

        .style27 {
            width: 12px;
        }

        .style33 {
            width: 261px;
        }

        .style34 {
            width: 124px;
        }

        .style35 {
            width: 256px;
        }

        .style36 {
            width: 35px;
        }

        .style38 {
            width: 73px;
        }

        .style39 {
            width: 18px;
        }

        .style40 {
            width: 29px;
        }

        .style41 {
            width: 70px;
        }

        .style42 {
            width: 94px;
        }

        .style44 {
            width: 51px;
        }

        .style45 {
            width: 213px;
        }

        .style47 {
            width: 2815px;
        }*/

        .chzn-single {
            border-radius: 3px !important;
            height: 29px !important;
        }
    </style>

    <%--    <table style="width: 91%;">
        <tr>
            <td class="style35">
                <asp:Label ID="Label2" runat="server" Font-Bold="True" Font-Size="18px"
                    ForeColor="Yellow"
                    Style="border-bottom: 1px solid WHITE; border-top: 1px solid WHITE;"
                    Text="MONTHLY BUDGET INFORMATION VIEW/EDIT" Width="500px"></asp:Label>
            </td>
            <td>
                <asp:Label ID="lbljavascript" runat="server"></asp:Label>
            </td>
            <td>
                <asp:DropDownList ID="DDPrintOpt" runat="server" Font-Names="Tahoma"
                    Style="font-size: 11px" Width="130px">
                    <asp:ListItem Selected="True" Value="PDF">Adobe Acrobat (PDF)</asp:ListItem>
                    <asp:ListItem Value="HTML">HTML</asp:ListItem>
                    <asp:ListItem Value="WORD">MS Word</asp:ListItem>
                    <asp:ListItem Value="EXCEL">MS Excel</asp:ListItem>
                </asp:DropDownList>
            </td>
            <td class="style38">
                <asp:LinkButton ID="lbtnPrint" runat="server" CssClass="button"
                    Font-Bold="True" OnClick="lbtnPrint_Click" Style="color: #FFFFFF">PRINT</asp:LinkButton>
            </td>
        </tr>
    </table>--%>


    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>

            <div class="card card-fluid mb-1">
                <div class="card-body pt-2 pb-3">
                    <div class="row">
                        <div class="col-md-2">
                            <asp:Label ID="Label8" runat="server" CssClass=" " Text="Budget No."></asp:Label>
                            <div class="d-flex">
                                <asp:Label ID="lblCurBgdNo1" runat="server" Style="" CssClass="form-control form-control-sm" Text="BGD00-"></asp:Label>
                                <asp:TextBox ID="txtCurBgdNo2" runat="server" ReadOnly="True" CssClass="form-control form-control-sm" >00000</asp:TextBox>
                            </div>
                        </div>

                        <div class="col-md-1">
                            <asp:Label ID="Label7" runat="server" CssClass=" " Text="Date"></asp:Label>
                            <asp:TextBox ID="txtCurDate" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
                            <cc1:CalendarExtender ID="txtCurDate_CalendarExtender" runat="server"
                                Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txtCurDate"></cc1:CalendarExtender>
                        </div>

                        <div class="col-md-1">
                            <asp:LinkButton ID="lbtnOk" runat="server" style="margin-top:20px;" CssClass="btn btn-sm btn-success" OnClick="lbtnOk_Click" Text="Ok"></asp:LinkButton>
                        </div>

                        <div class="col-md-2">
                            <asp:LinkButton ID="lbtnPrevBudget" runat="server" CssClass="text-primary" OnClick="lbtnPrevBudget_Click">
                                <i class="fa fa-search mr-1"></i> Prev. Budget
                            </asp:LinkButton>
                            <asp:DropDownList ID="ddlPrevBgdList" runat="server" CssClass="form-control form-control-sm chzn-select">
                            </asp:DropDownList>
                        </div>

                        <div class="col-md-1">
                            <asp:Label ID="lblmsg" runat="server" BackColor="Red" Font-Bold="True"
                                ForeColor="White"></asp:Label>
                        </div>

                    </div>
                </div>
            </div>

            <div class="card card-fluid">
                <div class="card-body" style="min-height: 500px;">
                    <asp:MultiView ID="MultiView1" runat="server">

                        <asp:View ID="MainCode" runat="server">
                            <div class="row">
                                <div class="col-md-2">
                                    <asp:Label ID="lblacccode1" runat="server" CssClass="" Text="Accounts Code:" ></asp:Label>
                                    <asp:TextBox ID="txtFilter" runat="server" AutoCompleteType="Disabled" CssClass="form-control form-control-sm"></asp:TextBox>
                                </div>
                                <div class="col-md-1">
                                    <asp:LinkButton ID="ibtnAccCode" runat="server" style="margin-top:20px;" CssClass="btn btn-sm btn-info" OnClick="ibtnAccCode_Click" >
                                        <i class="fa fa-search"></i>
                                    </asp:LinkButton>
                                </div>
                            </div>

                            <div class="table-responsive">
                                <asp:GridView ID="dgv2" runat="server" AllowPaging="True"
                                    AutoGenerateColumns="False" BackColor="#99CCCC" BorderColor="#7FBF41"
                                    BorderStyle="Solid" BorderWidth="2px" OnRowCommand="dgv2_RowCommand"
                                    OnRowCreated="dgv2_RowCreated" PagerSettings-Position="Bottom"
                                    PagerSettings-Visible="false" PagerStyle-BackColor="#4A89BC"
                                    PagerStyle-HorizontalAlign="Center" PageSize="12" RowStyle-Font-Size="12px"
                                    ShowFooter="True" Width="910px">
                                    <PagerSettings Visible="False" />
                                    <RowStyle Font-Size="12px" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sl.No.">
                                            <ItemTemplate>
                                                <asp:Label ID="lblserialnoid" runat="server" Style="text-align: right"
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle Font-Bold="True" Font-Size="16px" />
                                            <ItemStyle Font-Size="12px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="ActCode" Visible="False">
                                            <ItemTemplate>
                                                <asp:Label ID="lblAccCod" runat="server" CssClass="GridLebel"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actcode")) %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Head of Accounts">
                                            <FooterTemplate>
                                                <table style="width: 50%;">
                                                    <tr>
                                                        <td class="style20">
                                                            <asp:DropDownList ID="dgv2ddlPageNo" runat="server" AutoPostBack="True"
                                                                Font-Bold="True" Font-Size="14px"
                                                                OnSelectedIndexChanged="dgv2ddlPageNo_SelectedIndexChanged"
                                                                Style="border-right: navy 1px solid; border-top: navy 1px solid; border-left: navy 1px solid; border-bottom: navy 1px solid"
                                                                Width="180px">
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td class="style21">&nbsp;</td>
                                                        <td>&nbsp;</td>
                                                        <td>&nbsp;</td>
                                                        <td class="style22">
                                                            <asp:LinkButton ID="lnkFinalUpdate" runat="server" CssClass="UpdateButton"
                                                                OnClick="lnkFinalUpdate_Click" onmouseout="style.color='White'"
                                                                onmouseover="style.color='#FF9999'" Text="Final Upate" Width="90px"
                                                                BackColor="Black" BorderColor="White" BorderStyle="Solid" BorderWidth="1px"
                                                                Font-Bold="True"></asp:LinkButton>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblAccdesc" runat="server" CssClass="GridLebelL"
                                                    Font-Size="11px"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc")) %>'
                                                    Width="400px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle HorizontalAlign="Center" />
                                            <HeaderStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField FooterStyle-HorizontalAlign="Center" HeaderText="Level"
                                            ItemStyle-HorizontalAlign="Center">
                                            <FooterTemplate>
                                                <asp:LinkButton ID="LnkfTotal" runat="server" OnClick="LnkfTotal_Click"
                                                    Style="color: #FFFFFF; font-weight: 700">Total :</asp:LinkButton>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:LinkButton ID="gvlnkLevel" runat="server" OnClick="gvlnkLevel_Click"
                                                    onmouseout="style.color='White'" onmouseover="style.color='#FF9999'"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actelev")) %>'
                                                    Width="50px"></asp:LinkButton>
                                            </ItemTemplate>
                                            <FooterStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Dr.Amount" ItemStyle-HorizontalAlign="Right">
                                            <FooterTemplate>
                                                <asp:TextBox ID="txtTgvDrAmt" runat="server" BackColor="Transparent"
                                                    BorderColor="Transparent" BorderStyle="None" BorderWidth="1px"
                                                    CssClass="GridTextbox" Font-Bold="True" ReadOnly="True" Width="103px"></asp:TextBox>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtgvDrAmt" runat="server" BackColor="Transparent"
                                                    BorderColor="Transparent" BorderStyle="None" BorderWidth="1px"
                                                    CssClass="GridTextbox"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "Dr")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="103px"></asp:TextBox>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle HorizontalAlign="right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Cr.Amount" ItemStyle-HorizontalAlign="Right">
                                            <FooterTemplate>
                                                <asp:TextBox ID="txtTgvCrAmt" runat="server" BackColor="Transparent"
                                                    BorderColor="Transparent" BorderStyle="None" BorderWidth="1px"
                                                    CssClass="GridTextbox" Font-Bold="True" ReadOnly="True" Width="103px"></asp:TextBox>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtgvCrAmt" runat="server" BackColor="Transparent"
                                                    BorderColor="Transparent" BorderStyle="None" BorderWidth="1px"
                                                    CssClass="GridTextbox"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "Cr")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="103px"></asp:TextBox>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle HorizontalAlign="right" />
                                        </asp:TemplateField>
                                    </Columns>
                                    <FooterStyle BackColor="#5E7BAE" />
                                    <PagerStyle BackColor="#4A89BC" HorizontalAlign="Center" />
                                    <HeaderStyle BackColor="#5E7BAE" BorderStyle="Solid" BorderWidth="2px"
                                        Font-Bold="True" Font-Size="14px" ForeColor="White" />
                                    <AlternatingRowStyle BackColor="#6A8B92" BorderColor="#FF66CC"
                                        BorderStyle="Solid" BorderWidth="1px" />
                                </asp:GridView>
                            </div>


                        </asp:View>

                        <asp:View ID="DetailsCode" runat="server">

                            <div class="row">
                                <div class="col-md-2">
                                    <asp:Label ID="lblacccode" runat="server" CssClass="label2"
                                        Text="Accounts Code:" Width="100px"></asp:Label>
                                    <asp:TextBox ID="txtActcode" runat="server" BackColor="Black"
                                        BorderColor="White" BorderStyle="Solid" BorderWidth="1px" Font-Bold="True"
                                        Font-Size="12px" ForeColor="Yellow" Width="400px"></asp:TextBox>
                                </div>

                                <div class="col-md-1">
                                    <asp:Label ID="lblPage" runat="server" Font-Bold="True" Font-Size="14px"
                                        ForeColor="White" Style="text-align: right;" Text="Page Size" Width="100px"></asp:Label>
                                    <asp:DropDownList ID="ddlpagesize" runat="server" AutoPostBack="True"
                                        BackColor="#CCFFCC" Font-Bold="True" Font-Size="12px" ForeColor="#3366FF"
                                        OnSelectedIndexChanged="ddlpagesize_SelectedIndexChanged">
                                        <asp:ListItem>10</asp:ListItem>
                                        <asp:ListItem>20</asp:ListItem>
                                        <asp:ListItem>30</asp:ListItem>
                                        <asp:ListItem>50</asp:ListItem>
                                        <asp:ListItem>100</asp:ListItem>
                                        <asp:ListItem>150</asp:ListItem>
                                        <asp:ListItem>200</asp:ListItem>
                                        <asp:ListItem>300</asp:ListItem>
                                        <asp:ListItem>600</asp:ListItem>
                                        <asp:ListItem>900</asp:ListItem>
                                    </asp:DropDownList>
                                </div>

                                <div class="col-md-1">
                                    <asp:LinkButton ID="lnkSubmit" runat="server" CssClass="button" Height="16px"
                                        OnClick="lnkSubmit_Click" onmouseout="style.color='White'"
                                        onmouseover="style.color='#FF9999'" Width="77px">Home</asp:LinkButton>
                                </div>

                                <div class="col-md-2">
                                    <asp:Label ID="Label5" runat="server" Font-Size="12px" ForeColor="White"
                                        Style="font-weight: 700; text-align: right;" Text="Resource Code:"
                                        Width="100px"></asp:Label>
                                    <asp:TextBox ID="txtResSearch" runat="server" AutoCompleteType="Disabled"
                                        BorderStyle="None" EnableTheming="True" Width="100px"></asp:TextBox>
                                </div>

                                <div class="col-md-1">
                                    <asp:ImageButton ID="ibtnDetailsCode" runat="server" Height="16px"
                                        ImageUrl="~/Image/search-button-on.gif" OnClick="ibtnDetailsCode_Click"
                                        Width="41px" />
                                </div>
                            </div>

                            <div class="table-responsive">
                                <asp:GridView ID="dgv3" runat="server" AllowPaging="True"
                                    AutoGenerateColumns="False" OnPageIndexChanging="dgv3_PageIndexChanging"
                                    ShowFooter="True" Width="831px">
                                    <PagerSettings Position="TopAndBottom" />
                                    <RowStyle BackColor="#D2FFF7" Font-Size="11px" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sl.No.">
                                            <ItemTemplate>
                                                <asp:Label ID="lblserialnoid0" runat="server" Style="text-align: right"
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle Font-Bold="True" Font-Size="16px" />
                                            <ItemStyle Font-Size="12px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Code">
                                            <ItemTemplate>
                                                <asp:Label ID="gvlblrescode" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rescode")) %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField FooterStyle-HorizontalAlign="Right"
                                            HeaderText="Res.Description">
                                            <ItemTemplate>
                                                <asp:Label ID="gvlblResDesc" runat="server" Font-Size="12px"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "resdesc")) %>'
                                                    Width="350px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Unit">
                                            <ItemTemplate>
                                                <asp:Label ID="gvlblresunit" runat="server" Font-Size="11px"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "resunit")) %>'
                                                    Width="40px"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Quantity" ItemStyle-HorizontalAlign="Center">
                                            <FooterTemplate>
                                                <asp:LinkButton ID="lnkbtnUpdateRes" runat="server" Font-Bold="True"
                                                    Font-Size="12px" OnClick="lnkbtnUpdateRes_Click" BorderStyle="None"
                                                    BorderWidth="1px" CssClass="UpdateButton">Update</asp:LinkButton>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:TextBox ID="gvtxtQty" runat="server" BackColor="Transparent"
                                                    BorderColor="Transparent" BorderStyle="None" BorderWidth="1px"
                                                    CssClass="GridTextbox"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="106px"></asp:TextBox>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Rate" ItemStyle-HorizontalAlign="Right">
                                            <FooterTemplate>
                                                <asp:LinkButton ID="gvlnkFTotal" runat="server" Font-Bold="True"
                                                    ForeColor="White" OnClick="gvlnkFTotal_Click">Total 
                                                                    :</asp:LinkButton>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="gvlblRate" runat="server" BackColor="Transparent"
                                                    BorderColor="Transparent" BorderStyle="None" BorderWidth="1px"
                                                    CssClass="GridTextbox"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "rate")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="106px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle HorizontalAlign="Right" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField FooterStyle-Font-Bold="true" FooterStyle-Font-Size="14px"
                                            FooterStyle-HorizontalAlign="Right" HeaderText="Dr. Amount"
                                            ItemStyle-HorizontalAlign="Right">
                                            <ItemTemplate>
                                                <asp:TextBox ID="gvtxtDrAmt" runat="server" BackColor="Transparent"
                                                    BorderColor="Transparent" BorderStyle="None" BorderWidth="1px"
                                                    CssClass="GridTextbox"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "Dr")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="106px"></asp:TextBox>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:TextBox ID="gvtxtftDramt" runat="server" BackColor="Transparent"
                                                    BorderColor="Transparent" BorderStyle="None" CssClass="GridTextbox"
                                                    Font-Bold="True" Font-Size="11px" ForeColor="Beige" ReadOnly="True"
                                                    Width="116px"></asp:TextBox>
                                            </FooterTemplate>
                                            <FooterStyle Font-Bold="True" Font-Size="14px" HorizontalAlign="Right" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField FooterStyle-Font-Bold="true" FooterStyle-Font-Size="14px"
                                            FooterStyle-HorizontalAlign="Right" HeaderText="Cr. Amount"
                                            ItemStyle-HorizontalAlign="Right">
                                            <ItemTemplate>
                                                <asp:TextBox ID="gvtxtCrAmt" runat="server" BackColor="Transparent"
                                                    BorderColor="Transparent" BorderStyle="None" BorderWidth="1px"
                                                    CssClass="GridTextbox"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "Cr")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="106px"></asp:TextBox>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:TextBox ID="gvtxtftCramt" runat="server" BackColor="Transparent"
                                                    BorderColor="Transparent" BorderStyle="None" CssClass="GridTextbox"
                                                    Font-Bold="True" Font-Size="11px" ForeColor="Beige" Width="106px"></asp:TextBox>
                                            </FooterTemplate>
                                            <FooterStyle Font-Bold="True" Font-Size="14px" HorizontalAlign="Right" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                    </Columns>
                                    <PagerStyle ForeColor="White" HorizontalAlign="Left" />
                                    <FooterStyle BackColor="#333300" BorderStyle="None" />
                                    <HeaderStyle BackColor="#333300" Font-Bold="True" Font-Size="12px"
                                        ForeColor="#FFFFCC" Height="20px" HorizontalAlign="Center" />
                                    <EditRowStyle BackColor="#E2D5CD" VerticalAlign="Top" />
                                    <AlternatingRowStyle BackColor="#CAE4FF" Font-Size="11px" />
                                </asp:GridView>
                            </div>

                        </asp:View>

                    </asp:MultiView>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

