<%@ Page Title="" Language="C#" MasterPageFile="~/SPE.Master" AutoEventWireup="true" CodeBehind="AccOpening.aspx.cs" Inherits="SPEWEB.F_21_GAcc.AccOpening" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript">

        $(document).ready(function () {
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);
        });


        function pageLoaded() {

            let options = { ScrollHeight: 400 };
            let gv1 = $('#<%=this.dgv3.ClientID %>');
            gv1.Scrollable(options);

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
                                <asp:Label ID="lblopndate" runat="server" CssClass="label">Opening Date</asp:Label>
                                <asp:TextBox ID="txtdate" runat="server" CssClass="form-control form-control-sm small "></asp:TextBox>
                                <cc1:CalendarExtender ID="txtdate_CalendarExtender1" runat="server" Format="dd-MMM-yyyy" TargetControlID="txtdate"></cc1:CalendarExtender>
                            </div>
                        </div>
                        <div class="col-md-2 col-sm-2 col-lg-2 ">


                            <div class="form-group">
                                <asp:Label ID="lblacccode1" runat="server" CssClass="control-label">Accounts Code</asp:Label>

                                <div class="input-group input-group-alt">




                                    <asp:TextBox runat="server" ID="txtFilter" CssClass="form-control form-control-sm ">
                                    </asp:TextBox>
                                    <div class="input-group-append">
                                        <asp:LinkButton ID="ImageButton1" CssClass="input-group-text" runat="server" OnClick="ImageButton1_Click"><span class="fa fa-search"> </span></asp:LinkButton>
                                    </div>
                                </div>
                            </div>
                        </div>

                    </div>

                    <div class="row">

                        <asp:GridView ID="dgv2" runat="server" AllowPaging="True"
                            AutoGenerateColumns="False" OnRowCreated="dgv2_RowCreated"
                            PagerSettings-Position="Bottom" PagerStyle-BackColor="#4A89BC"
                            PagerSettings-Visible="false"
                            PagerStyle-HorizontalAlign="Center" RowStyle-Font-Size="12px" ShowFooter="True"
                            Width="600px" OnRowCommand="dgv2_RowCommand" PageSize="15" CssClass="table-striped table-hover table-bordered grvContentarea">
                            <PagerSettings Visible="False" />
                            <RowStyle />
                            <Columns>
                                <asp:TemplateField HeaderText=" ">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lbtndelete" runat="server" OnClick="lbtndelete_Click"><span class="fa fa-trash"> </span></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Sl">
                                    <ItemTemplate>
                                        <asp:Label ID="lblserialnoid" runat="server" Style="text-align: right"
                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                    </ItemTemplate>

                                    <ItemStyle Font-Size="12px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="ActCode" Visible="False">
                                    <ItemTemplate>
                                        <asp:Label ID="lblAccCod" runat="server"
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
                                                    <asp:LinkButton ID="lnkFinalUpdate" runat="server"
                                                        OnClick="lnkFinalUpdate_Click"
                                                        Width="90px" CssClass="btn btn-danger  btn-sm pull-left">Final Update</asp:LinkButton>
                                                </td>
                                            </tr>
                                        </table>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblAccdesc" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc")) %>'
                                            Font-Size="11px" Width="400px"></asp:Label>
                                    </ItemTemplate>

                                </asp:TemplateField>
                                <asp:TemplateField FooterStyle-HorizontalAlign="Center" HeaderText="Level"
                                    ItemStyle-HorizontalAlign="Center">
                                    <FooterTemplate>
                                        <asp:LinkButton ID="LnkfTotal" runat="server" OnClick="LnkfTotal_Click" CssClass="btn btn-primary btn-sm pull-left">Total :</asp:LinkButton>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="gvlnkLevel" runat="server" OnClick="gvlnkLevel_Click"
                                            onmouseover="style.color='#FF9999'" onmouseout="style.color='blue'"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actelev")) %>'
                                            Width="50px"></asp:LinkButton>
                                    </ItemTemplate>
                                    <FooterStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Dr.Amount">
                                    <FooterTemplate>
                                        <asp:TextBox ID="txtTgvDrAmt" runat="server" BackColor="Transparent"
                                            BorderColor="Transparent" BorderStyle="None" Width="103px" Font-Bold="True" ReadOnly="True" Style="text-align: right"></asp:TextBox>
                                        <asp:TextBox ID="lblgvDr" runat="server" BackColor="Transparent" ForeColor="Red" ToolTip="Difference"
                                            BorderColor="Transparent" BorderStyle="None" Width="103px" Font-Bold="True" ReadOnly="True" Style="text-align: right"></asp:TextBox>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtgvDrAmt" runat="server" BackColor="Transparent"
                                            BorderStyle="None" BorderWidth="1px" Style="text-align: right"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "Dr")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="103px"></asp:TextBox>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle HorizontalAlign="right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Cr.Amount">
                                    <FooterTemplate>
                                        <asp:TextBox ID="txtTgvCrAmt" runat="server" BackColor="Transparent"
                                            BorderColor="Transparent" BorderStyle="None"
                                            Width="103px" ReadOnly="True" Font-Bold="True" Style="text-align: right"></asp:TextBox>
                                        <asp:TextBox ID="lblgvCr" runat="server" BackColor="Transparent"
                                            BorderColor="Transparent" BorderStyle="None" ForeColor="Red" ToolTip="Difference"
                                            Width="103px" ReadOnly="True" Font-Bold="True" Style="text-align: right"></asp:TextBox>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtgvCrAmt" runat="server" BackColor="Transparent"
                                            BorderStyle="None" Style="text-align: right"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "Cr")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="103px"></asp:TextBox>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle HorizontalAlign="right" />
                                </asp:TemplateField>

                            </Columns>
                            <FooterStyle CssClass="grvFooter" />
                            <EditRowStyle />
                            <AlternatingRowStyle />
                            <PagerStyle CssClass="gvPagination" />
                            <HeaderStyle CssClass="grvHeader" />
                        </asp:GridView>

                    </div>


                    <asp:Panel ID="pnlsub" runat="server">
                        <%--<div class="row">
                            <asp:Label ID="lblacccode2" runat="server" Font-Bold="True" Font-Names="Verdana" Font-Size="16px" Text="Resource Entry Screen"></asp:Label>
                        </div>--%>

                        <div class="row">



                            <div class="col-md-2 col-sm-2 col-lg-2 ">
                                <div class="form-group">
                                    <asp:Label ID="lblacccode" runat="server" CssClass="label">Accounts Code</asp:Label>

                                    <asp:TextBox ID="txtActcode" runat="server" Style="width: 100%;" CssClass="form-control form-control-sm small" ReadOnly="True"></asp:TextBox>


                                </div>
                            </div>
                            <div class="col-md-2 col-sm-2 col-lg-2 ">


                                <div class="form-group">
                                    <asp:Label ID="Label1" runat="server" CssClass="control-label">Resource Code</asp:Label>
                                    <div class="input-group input-group-alt">
                                        <asp:TextBox runat="server" ID="txtResSearch" CssClass="form-control form-control-sm ">
                                        </asp:TextBox>
                                        <div class="input-group-append">
                                            <asp:LinkButton ID="ImageButton2" CssClass="input-group-text" runat="server" OnClick="ImageButton2_Click"><span class="fa fa-search"> </span></asp:LinkButton>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-1 col-sm-1 col-lg-1 ">
                                <div class="form-group" style="margin-top: 20px;">
                                    <asp:LinkButton ID="lnkSubmit" runat="server" CssClass="btn btn-primary btn-sm pull-left" OnClick="lnkSubmit_Click">Back</asp:LinkButton>
                                </div>
                            </div>
                            <div class="col-md-1 col-sm-1 col-lg-1 ">
                                <div class="form-group">
                                    <asp:Label ID="lblPage" runat="server" CssClass="label"> Page</asp:Label>
                                    <asp:DropDownList ID="ddlpagesize" runat="server" AutoPostBack="True" CssClass="chzn-select form-control form-control-sm" OnSelectedIndexChanged="ddlpagesize_SelectedIndexChanged">
                                        <asp:ListItem>10</asp:ListItem>
                                        <asp:ListItem>15</asp:ListItem>
                                        <asp:ListItem>20</asp:ListItem>
                                        <asp:ListItem>30</asp:ListItem>
                                        <asp:ListItem>50</asp:ListItem>
                                        <asp:ListItem>100</asp:ListItem>
                                        <asp:ListItem>150</asp:ListItem>
                                        <asp:ListItem>200</asp:ListItem>
                                        <asp:ListItem>300</asp:ListItem>
                                        <asp:ListItem>900</asp:ListItem>
                                        <asp:ListItem>1500</asp:ListItem>
                                        <asp:ListItem>3000</asp:ListItem>
                                        <asp:ListItem>5000</asp:ListItem>

                                    </asp:DropDownList>
                                </div>
                            </div>

                        </div>


                        <div class="row py-5">
                            <asp:GridView ID="dgv3" runat="server" AllowPaging="True"
                                AutoGenerateColumns="False" OnPageIndexChanging="dgv3_PageIndexChanging" OnRowDeleting="dgv3_RowDeleting"
                                ShowFooter="True" Width="831px" CssClass="table-striped table-hover table-bordered grvContentarea">
                                <PagerSettings Position="Bottom" />


                                <Columns>
                                    <asp:CommandField ShowDeleteButton="True" ControlStyle-ForeColor="Red" DeleteText="<span class='fa fa-trash'></span>" />
                                    <asp:TemplateField HeaderText="Sl">
                                        <ItemTemplate>
                                            <asp:Label ID="lblserialnoid" runat="server" Style="text-align: right"
                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Font-Bold="True" Font-Size="16px" />
                                        <ItemStyle Font-Size="12px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Code">
                                        <ItemTemplate>
                                            <asp:Label ID="gvlblrescode" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rescode")) %>' Width="80px"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Res.Description"
                                        FooterStyle-HorizontalAlign="Right">

                                        <ItemTemplate>
                                            <asp:Label ID="gvlblResDesc" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "resdesc")) %>'
                                                Width="250px" Font-Size="12px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Unit">
                                        <ItemTemplate>
                                            <asp:Label ID="gvlblresunit" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "resunit")) %>'
                                                Width="40px" Font-Size="11px"></asp:Label>
                                        </ItemTemplate>

                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderText="Specification"
                                        FooterStyle-HorizontalAlign="Right">

                                        <ItemTemplate>
                                            <asp:Label ID="gvlblgvspecification" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "spcfdesc")) %>'
                                                Width="180px" Font-Size="12px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Size"
                                        FooterStyle-HorizontalAlign="Right" Visible="false">

                                        <ItemTemplate>
                                            <asp:Label ID="gvlblsizedesc" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sizedesc")) %>'
                                                Width="40px" Font-Size="12px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Quantity" ItemStyle-HorizontalAlign="Center">
                                        <FooterTemplate>
                                            <asp:LinkButton ID="lnkbtnUpdateRes" runat="server" Font-Bold="True" CssClass="btn btn-danger  btn-sm pull-left"
                                                OnClick="lnkbtnUpdateRes_Click">Update</asp:LinkButton>
                                        </FooterTemplate>
                                        <ItemTemplate>
                                            <asp:TextBox ID="gvtxtQty" runat="server" BackColor="Transparent"
                                                BorderColor="Transparent" BorderStyle="None" BorderWidth="1px"
                                                CssClass="GridTextbox"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="106px" Style="text-align: right !important"></asp:TextBox>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Rate" ItemStyle-HorizontalAlign="Right">
                                        <FooterTemplate>
                                            <asp:LinkButton ID="gvlnkFTotal" runat="server" CssClass="btn btn-primary btn-sm pull-left" OnClick="gvlnkFTotal_Click">Total 
                                                                    :</asp:LinkButton>
                                        </FooterTemplate>
                                        <ItemTemplate>
                                            <asp:TextBox ID="gvtxtRate" runat="server" BackColor="Transparent"
                                                BorderColor="Transparent" BorderStyle="None" BorderWidth="1px"
                                                CssClass="GridTextbox"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "rate")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="106px" Style="text-align: right !important"></asp:TextBox>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Dr. Amount">
                                        <ItemTemplate>
                                            <asp:TextBox ID="gvtxtDrAmt" runat="server" BackColor="Transparent"
                                                BorderColor="Transparent" BorderStyle="None" BorderWidth="1px"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "Dr")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="106px" Style="text-align: right"></asp:TextBox>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:TextBox ID="gvtxtftDramt" runat="server" BackColor="Transparent"
                                                Font-Bold="True"
                                                BorderColor="Transparent" BorderStyle="None"
                                                Width="116px" ReadOnly="True" Style="text-align: right">></asp:TextBox>
                                        </FooterTemplate>
                                        <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                        <FooterStyle HorizontalAlign="Right" />
                                        <ItemStyle HorizontalAlign="Right" />

                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Cr. Amount">
                                        <ItemTemplate>
                                            <asp:TextBox ID="gvtxtCrAmt" runat="server" BackColor="Transparent"
                                                BorderColor="Transparent" BorderStyle="None"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "Cr")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="106px" Style="text-align: right"></asp:TextBox>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:TextBox ID="gvtxtftCramt" runat="server" BackColor="Transparent"
                                                BorderStyle="None"
                                                Width="106px" Style="text-align: right">></asp:TextBox>
                                        </FooterTemplate>
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
                        </div>

                    </asp:Panel>


                </div>
            </div>





            <!-- End of Container-->



        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>

