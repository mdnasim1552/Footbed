<%@ Page Title="" Language="C#" MasterPageFile="~/SPEMaster.Master" AutoEventWireup="true" CodeBehind="AccDepJournal.aspx.cs" Inherits="SPEWEB.F_21_GAcc.AccDepJournal" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">



    <link href="../CSS/Style.css" rel="stylesheet" type="text/css" />

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript" language="javascript" src="../Scripts/jquery-1.4.1.min.js"></script>
    <script src="../Scripts/jquery.keynavigation.js" type="text/javascript"></script>


    <script language="javascript" type="text/javascript">
        $(document).ready(function () {
            //For navigating using left and right arrow of the keyboard
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);
        });
        function pageLoaded() {


            var gridview = $('#<%=this.dgv2.ClientID %>');
            $.keynavigation(gridview);


            var dgv2 = $('#<%=this.dgv2.ClientID %>');
            dgv2.gridviewScroll({
                width: 750,
                height: 350,
                arrowsize: 30,
                railsize: 16,
                barsize: 8,
                varrowtopimg: "../Image/arrowvt.png",
                varrowbottomimg: "../Image/arrowvb.png",
                harrowleftimg: "../Image/arrowhl.png",
                harrowrightimg: "../Image/arrowhr.png",
                freezesize: 7
            });

            $('.chzn-select').chosen({ search_contains: true });
        };
    </script>






    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="container moduleItemWrpper">
                <div class="contentPart">
                    <div class="row">
                        <asp:Panel ID="Panel1" runat="server">

                            <fieldset class="scheduler-border fieldset_A">

                                <div class="form-horizontal">
                                    <div class="form-group">
                                        <div class="col-md-3 pading5px asitCol3">
                                            <asp:Label ID="lblcurVounum" runat="server" CssClass="lblTxt lblName">Current Voucher</asp:Label>
                                            <asp:TextBox ID="txtcurrentvou" runat="server" CssClass="smltxtBox" ReadOnly="True"></asp:TextBox>
                                            <asp:TextBox ID="txtCurrntlast6" runat="server" CssClass="smltxtBox60px" ToolTip="You Can Change Voucher Number." ReadOnly="True"></asp:TextBox>
                                        </div>
                                        <div class="col-md-4 pading5px">

                                            <asp:Label ID="lbltxtDate" runat="server" CssClass="lblTxt lblName"> Voucher Date</asp:Label>
                                            <asp:Label ID="lbldate" runat="server" CssClass="inputDateBox"></asp:Label>
                                            <%-- <cc1:CalendarExtender ID="txtdate_CalendarExtender" runat="server"
                                                Enabled="True" Format="dd-MMM-yyyy ddd" TargetControlID="txtdate">
                                            </cc1:CalendarExtender>--%>
                                            <%-- <div class="colMdbtn pading5px">
                                                <asp:LinkButton ID="lbtnOk" runat="server" CssClass="btn btn-primary okBtn" OnClick="lbtnOk_Click">Ok</asp:LinkButton>

                                            </div>--%>
                                        </div>

                                        <div class="col-md-3 pading5px pull-right">
                                            <div class="msgHandSt">
                                                <asp:Label ID="lblmsg" CssClass="btn-danger btn primaryBtn" runat="server" Visible="false"></asp:Label>
                                            </div>
                                        </div>
                                    </div>

                                </div>
                            </fieldset>
                        </asp:Panel>
                    </div>

                    <asp:Panel ID="pnlBill" runat="server">
                        <div class="row">
                            <fieldset class="scheduler-border fieldset_A">
                                <div class="form-horizontal">
                                    <div class="form-group">
                                        <div class="col-md-3 pading5px asitCol3">
                                            <asp:Label ID="Label1" runat="server" CssClass="lblTxt lblName" Text="Head of Acc"></asp:Label>
                                            <asp:TextBox ID="txtAccSch" runat="server" CssClass="inputtextbox"></asp:TextBox>
                                            <div class="colMdbtn">
                                                <asp:LinkButton ID="imgbtnAcc" runat="server" CssClass="btn btn-primary srearchBtn" OnClick="imgbtnAcc_Click"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>

                                            </div>

                                        </div>

                                        <div class="col-md-4 pading5px ">
                                            <asp:DropDownList ID="ddlActCode" runat="server" CssClass="form-control inputTxt chzn-select" AutoPostBack="true" OnSelectedIndexChanged="ddlActCode_SelectedIndexChanged">
                                            </asp:DropDownList>

                                        </div>
                                        <div class="col-md-3 pading5px asitCol3">



                                            <div class="colMdbtn pading5px">
                                                <asp:LinkButton ID="lbtnSelectTrns" runat="server" CssClass="btn btn-primary primaryBtn" OnClick="lbtnSelectTrns_Click">Select</asp:LinkButton>

                                            </div>



                                        </div>

                                    </div>
                                    <div class="form-group">
                                        <div class="col-md-3 pading5px asitCol3">
                                            <asp:Label ID="lbldetails" runat="server" CssClass="lblTxt lblName" Text="Details of Acc"></asp:Label>
                                            <asp:TextBox ID="txtserchReCode" runat="server" CssClass="inputtextbox"></asp:TextBox>
                                            <div class="colMdbtn">
                                                <asp:LinkButton ID="lnkRescode" runat="server" CssClass="btn btn-primary srearchBtn"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>

                                            </div>

                                        </div>

                                        <div class="col-md-4 pading5px ">
                                            <asp:DropDownList ID="ddlresuorcecode" runat="server" CssClass="form-control inputTxt chzn-select">
                                            </asp:DropDownList>

                                        </div>

                                    </div>

                                </div>
                            </fieldset>


                            <asp:GridView ID="dgv2" runat="server" AutoGenerateColumns="False" CssClass="table-striped table-hover table-bordered grvContentarea"
                                BorderStyle="Solid" BorderWidth="2px"
                                ShowFooter="True" Width="689px" OnRowDeleting="dgv2_RowDeleting">

                                <Columns>
                                    <asp:TemplateField HeaderText="Sl">
                                        <ItemTemplate>
                                            <asp:Label ID="serialnoid" runat="server"
                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Font-Bold="True" Font-Size="12px" />
                                    </asp:TemplateField>
                                    <asp:CommandField ShowDeleteButton="True" ControlStyle-CssClass="btn btn-xs btn-danger" DeleteText="<span class='glyphicon glyphicon-remove'></span>" />

                                    <asp:TemplateField HeaderText="A/c Code" Visible="False">
                                        <ItemTemplate>
                                            <asp:Label ID="lblAccCod" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actcode")) %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Sub Code" Visible="False">
                                        <ItemTemplate>
                                            <asp:Label ID="lblResCod" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "subcode")) %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Spcl Code" Visible="False">
                                        <ItemTemplate>
                                            <asp:Label ID="lblSpclCod" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "spclcode")) %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="A/C Description" ItemStyle-Font-Size="9px">
                                        <ItemTemplate>
                                            <asp:Label ID="lblAccdesc1" runat="server"
                                                Font-Names="Verdana" Font-Size="11px"
                                                Text='<%# "<B>"+ Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc")) + "</B>"+
                                                                         (DataBinder.Eval(Container.DataItem, "subdesc").ToString().Trim().Length>0 ? 
                                                                         (Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc")).Trim().Length>0 ? "<br>" : "") + 
                                                                         "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + 
                                                                         Convert.ToString(DataBinder.Eval(Container.DataItem, "subdesc")).Trim(): "")  + 
                                                                         (DataBinder.Eval(Container.DataItem, "spcldesc").ToString().Trim().Length>0 ? 
                                                                         " [" + Convert.ToString(DataBinder.Eval(Container.DataItem, "spcldesc")).Trim() + "]": "") %>'
                                                Width="350px"></asp:Label>

                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" />
                                        <ItemStyle Font-Size="11px" />
                                    </asp:TemplateField>




                                    <asp:TemplateField HeaderText="Dr.Amount" ItemStyle-Font-Size="11px">
                                        <FooterTemplate>
                                            <asp:TextBox ID="txtTgvDrAmt" runat="server" BackColor="Transparent" ReadOnly="true"
                                                BorderColor="Transparent" BorderStyle="None" BorderWidth="1px"
                                                Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                                Width="80px"></asp:TextBox>
                                        </FooterTemplate>
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvDrAmt" runat="server" BackColor="Transparent" Style="text-align: right"
                                                BorderColor="Transparent" BorderStyle="None" BorderWidth="1px"
                                                Height="22px" ForeColor="Black"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "Dr")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="80px"></asp:TextBox>
                                        </ItemTemplate>
                                        <ItemStyle Font-Size="11px" HorizontalAlign="Right" />
                                        <FooterStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Cr.Amount" ItemStyle-Font-Size="11px">
                                        <FooterTemplate>
                                            <asp:TextBox ID="txtTgvCrAmt" runat="server" BackColor="Transparent"
                                                BorderColor="Transparent" BorderStyle="None" BorderWidth="1px"
                                                Font-Bold="True" Font-Size="12px" ForeColor="Black" ReadOnly="true"
                                                Width="80px"></asp:TextBox>
                                        </FooterTemplate>
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvCrAmt" runat="server" BackColor="Transparent" Style="text-align: right"
                                                BorderColor="Transparent" BorderStyle="None" BorderWidth="1px"
                                                Height="22px" ForeColor="Black"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "Cr")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="80px"></asp:TextBox>
                                        </ItemTemplate>
                                        <ItemStyle Font-Size="11px" HorizontalAlign="Right" />
                                        <FooterStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>





                                </Columns>
                                <EditRowStyle />
                                <AlternatingRowStyle />
                                <PagerStyle CssClass="gvPagination" />
                                <HeaderStyle CssClass="grvHeader" />
                                <FooterStyle CssClass="grvFooter" />
                            </asp:GridView>
                            <fieldset class="scheduler-border fieldset_Nar">
                                <div class="form-horizontal">

                                    <div class="form-group">
                                        <div class="col-md-2 pading5px asitCol2 ">
                                            <asp:Label ID="lblRefNum" runat="server" CssClass="lblTxt lblName" Text="Ref./CheqNo"></asp:Label>
                                            <asp:TextBox ID="txtRefNum" runat="server" CssClass="inputtextbox"></asp:TextBox>

                                        </div>
                                        <div class="col-md-4 pading5px">

                                            <asp:Label ID="lblSrInfo" runat="server" CssClass="lblTxt lblName" Text="Other ref"></asp:Label>
                                            <asp:TextBox ID="txtSrinfo" runat="server" CssClass="inputtextbox"></asp:TextBox>




                                        </div>



                                    </div>
                                    <div class="form-group">
                                        <div class="col-md-6 pading5px">
                                            <div class="input-group">
                                                <span class="input-group-addon glypingraddon">
                                                    <asp:Label ID="lblNaration" runat="server" CssClass="lblTxt lblName" Text="Narration:"></asp:Label>
                                                </span>
                                                <asp:TextBox ID="txtNarration" runat="server" class="form-control" Rows="2" TextMode="MultiLine"></asp:TextBox>
                                            </div>
                                        </div>


                                        <%--<div class="colMdbtn pading5px">

                                            
                                             <asp:LinkButton ID="lnkFinalUpdate" runat="server" CssClass="btn btn-danger primaryBtn" OnClick="lnkFinalUpdate_Click" >Final Update</asp:LinkButton>

                                        </div>--%>
                                    </div>

                                </div>

                            </fieldset>
                            <%--   <tr>
                                <td class="style199">&nbsp;<asp:Label ID="lblRefNum" runat="server" CssClass="label2"
                                    Text="Ref./Cheq No/Slip No." Width="120px"></asp:Label>
                                </td>
                                <td class="style190">&nbsp;<asp:TextBox ID="txtRefNum" runat="server" AutoCompleteType="Disabled"
                                    CssClass="ddl" Width="166px"></asp:TextBox>
                                </td>
                                <td class="style191">&nbsp;<asp:Label ID="lblSrInfo" runat="server" CssClass="label2"
                                    Text="Other ref.(if any)" Width="120px"></asp:Label>
                                </td>
                                <td class="style209">&nbsp;<asp:TextBox ID="txtSrinfo" runat="server" AutoCompleteType="Disabled"
                                    CssClass="ddl" Width="265px"></asp:TextBox>
                                </td>
                                <td>&nbsp;<asp:LinkButton ID="lnkFinalUpdate" runat="server" CssClass="button"
                                    Font-Bold="True" Font-Size="12px" OnClick="lnkFinalUpdate_Click" Width="100px">Final Update</asp:LinkButton>
                                </td>
                                <td class="style178">&nbsp;</td>
                                <td class="style192">&nbsp;</td>
                                <td class="style193">&nbsp;</td>
                            </tr>

                            <tr>
                                <td class="style199" style="text-align: right; vertical-align: top">
                                    <asp:Label ID="lblNaration0" runat="server" CssClass="label2" Text="Narration"
                                        Width="120px"></asp:Label>
                                </td>
                                <td class="style190" colspan="3">
                                    <asp:TextBox ID="txtNarration" runat="server" AutoCompleteType="Disabled"
                                        CssClass="ddl" Height="42px" TextMode="MultiLine" Width="596px"></asp:TextBox>
                                </td>
                                <td>&nbsp;</td>
                                <td class="style178">&nbsp;</td>
                                <td class="style192">&nbsp;</td>
                                <td class="style193"></td>
                            </tr>
                        </table>--%>
                        </div>
                    </asp:Panel>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>

