<%@ Page Title="" Language="C#" MasterPageFile="~/SPEMaster.Master" AutoEventWireup="true" CodeBehind="AccCodeBook2.aspx.cs" Inherits="SPEWEB.F_21_GAcc.AccCodeBook2" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="container moduleItemWrpper">
        <script type="text/javascript">
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



        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>


                <div class="contentPart">
                    <div class="row">


                        <fieldset class="scheduler-border">

                            <div class="form-horizontal">
                                <div class="form-group">

                                    <asp:Label ID="LblBookName1" runat="server" CssClass="col-md-2 control-label lblTxt" Text="Select Code Book:"></asp:Label>

                                    <div class="col-md-3 pading5px">
                                        <asp:DropDownList ID="ddlCodeBook" runat="server" CssClass="form-control inputTxt chzn-select">
                                        </asp:DropDownList>

                                        <cc1:ListSearchExtender ID="ddlCodeBook_ListSearchExtender" runat="server"
                                            Enabled="True" QueryPattern="Contains" TargetControlID="ddlCodeBook">
                                        </cc1:ListSearchExtender>

                                        <asp:Label ID="lbalterofddl" runat="server" Visible="False" CssClass="form-control inputTxt"></asp:Label>
                                    </div>
                                    <div class="col-md-1 pading5px">
                                        <asp:DropDownList ID="ddlCodeBookSegment" CssClass="form-control inputTxt" runat="server">
                                            <asp:ListItem Value="2">Main Code</asp:ListItem>
                                            <asp:ListItem Value="4">Sub Code-1</asp:ListItem>
                                            <asp:ListItem Value="8">Sub Code-2</asp:ListItem>
                                            <asp:ListItem Selected="True" Value="12">Details Code</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:Label ID="lbalterofddl0" runat="server" Visible="False" CssClass="form-control inputTxt"></asp:Label>
                                    </div>
                                    <div class="col-md-1 pading5px">
                                        <asp:LinkButton ID="lnkok" runat="server" Text="Ok" OnClick="lnkok_Click" CssClass="btn btn-primary okBtn"></asp:LinkButton>
                                    </div>
                                    <div class="clearfix"></div>
                                </div>

                                <div class="form-group">
                                    <asp:Label ID="LblBookName2" runat="server" CssClass="col-md-2 control-label lblTxt" Text="Search Option:"></asp:Label>

                                    <div class="col-md-2 pading5px">
                                        <asp:TextBox ID="txtsrch" runat="server" CssClass="form-control inputTxt"></asp:TextBox>
                                    </div>



                                    <div class="col-md-3 pading5px">
                                        <asp:LinkButton ID="ibtnSrch" runat="server" OnClick="ibtnSrch_Click" CssClass="btn btn-success srearchBtn" Visible="False"><span class="glyphicon glyphicon-search asitGlyp"></span></asp:LinkButton>
                                        <asp:Label ID="lblPage" runat="server" CssClass=" lblName lblTxt" Text="Page Size" Visible="False"></asp:Label>
                                        <asp:DropDownList ID="ddlpagesize" runat="server" AutoPostBack="True" CssClass=" ddlPage"
                                            OnSelectedIndexChanged="ddlpagesize_SelectedIndexChanged" Visible="False">
                                            <asp:ListItem>15</asp:ListItem>
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
                                    <div class="col-md-4 pading5px">
                                        <div class="msgHandSt">
                                            <asp:Label ID="ConfirmMessage" CssClass=" btn-success btn primaryBtn" runat="server" Visible="false"></asp:Label>
                                        </div>
                                    </div>

                                </div>

                                <div class="form-group">
                                </div>


                            </div>
                        </fieldset>
                        <div class="table-responsive">
                            <asp:GridView ID="grvacc" runat="server" AllowPaging="True" CssClass="table-striped table-hover table-bordered grvContentarea" AutoGenerateColumns="False" OnRowCancelingEdit="grvacc_RowCancelingEdit" OnRowEditing="grvacc_RowEditing"
                                OnRowUpdating="grvacc_RowUpdating" PageSize="15"
                                OnPageIndexChanging="grvacc_PageIndexChanging" ShowFooter="True" BorderStyle="None" OnRowDataBound="grvacc_RowDataBound">
                                <PagerSettings NextPageText="Next" PreviousPageText="Previous" Position="Top"
                                    Mode="NumericFirstLast" />
                                <FooterStyle BackColor="#5F9467" />
                                <Columns>
                                    <asp:TemplateField HeaderText="Sl.No.">
                                        <ItemTemplate>
                                            <asp:Label ID="serialnoid" runat="server" Style="text-align: right"
                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                        </ItemTemplate>

                                    </asp:TemplateField>
                                    <asp:CommandField DeleteText="" HeaderText="Edit" InsertText="" NewText=""
                                        SelectText="" ShowEditButton="True"></asp:CommandField>
                                    <asp:TemplateField HeaderText=" ">
                                        <EditItemTemplate>
                                            <asp:Label ID="lbgrcode" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actcode2"))+"-" %>'></asp:Label>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="Label7" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actcode2"))+"-" %>'
                                                Width="30px"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Acc. Code">
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtgrcode" runat="server" Height="16px" MaxLength="12"
                                                Style="border-top-style: none; border-right-style: none; border-left-style: none; border-bottom-style: none; font-size: 12px; border-left-color: #151B54; border-bottom-color: #151B54; border-top-color: #151B54; border-right-color: #151B54;"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actcode4")) %>'
                                                Width="90px"></asp:TextBox>
                                            <asp:Label ID="lbgrcod1" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actcode3")) %>'
                                                Visible="False"></asp:Label>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lbtncod" runat="server" Font-Underline="false" Enabled="false" ForeColor="Black"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actcode4")) %>'
                                                Width="80px"></asp:LinkButton>
                                            <%--                                            <asp:Label ID="lbgrcod" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actcode4")) %>'
                                                Width="70px"></asp:Label>--%>
                                        </ItemTemplate>

                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Description of Accounts">
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtgvDesc" runat="server"
                                                Style="border-top-style: none; left: auto; border-right-style: none; border-left-style: none; border-bottom-style: none; font-size: 12px; border-left-color: #151B54; border-bottom-color: #151B54; border-top-color: #151B54; border-right-color: #151B54;"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc")) %>'
                                                Width="300px"></asp:TextBox>
                                        </EditItemTemplate>
                                        <HeaderTemplate>
                                            
                                            <table style="width: 300px;">
                                                <tr>
                                                    <td class="style58">
                                                        <asp:Label ID="Label4" runat="server" Font-Bold="True"
                                                            Text="Head of Accounts" Width="180px"></asp:Label>
                                                    </td>
                                                    <td class="style60">&nbsp;</td>
                                                    <td>
                                                        <asp:HyperLink ID="hlbtntbCdataExel" runat="server" CssClass="btn btn-danger btn-xs fa fa-file-excel-o"> </asp:HyperLink>
                                                    </td>
                                                </tr>
                                            </table>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="Label3" runat="server" Style="font-size: 12px; text-align: left;"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc")) %>'
                                                Width="300px"></asp:Label>
                                        </ItemTemplate>

                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Level">
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtgridlevel" runat="server" MaxLength="10"
                                                Style="border-top-style: none; border-right-style: none; border-left-style: none; border-bottom-style: none"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actelev")) %>'
                                                Width="40px"></asp:TextBox>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="Label6" runat="server" Font-Size="12px"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actelev")) %>'
                                                Width="40px" Style="text-align: center"></asp:Label>
                                        </ItemTemplate>

                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Type">
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtgvTypeCode" runat="server" Font-Size="12px" MaxLength="20"
                                                Style="border-top-style: none; border-right-style: none; border-left-style: none; border-bottom-style: none"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "acttype")) %>'
                                                Width="60px"></asp:TextBox>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="Label4" runat="server" Style="font-size: 12px"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "acttype")) %>'
                                                Width="60px"></asp:Label>
                                        </ItemTemplate>

                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Type Description">
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtgvTypeDesc" runat="server" Font-Size="12px" MaxLength="100"
                                                Style="border-top-style: none; border-right-style: none; border-left-style: none; border-bottom-style: none"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "acttdesc")) %>'
                                                Width="100px"></asp:TextBox>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="Label5" runat="server" Style="font-size: 12px"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "acttdesc")) %>'
                                                Width="100px"></asp:Label>
                                        </ItemTemplate>

                                    </asp:TemplateField>

                                </Columns>

                                <RowStyle />
                                <EditRowStyle />
                                <SelectedRowStyle />
                                <PagerStyle CssClass="gvPagination" />
                                <HeaderStyle CssClass="grvHeader" />
                                <FooterStyle CssClass="grvFooter" />
                                <AlternatingRowStyle BackColor="" />
                            </asp:GridView>
                        </div>


                    </div>
                    <%--  <div class="row">
                        <fieldset class="scheduler-border fieldset_A">

                            <div class="form-horizontal">
                                <div class="form-group">

                                    <div class="formBtn ">

                                        <div class="pull-right">
                                            <asp:LinkButton ID="lnkbtnSaveSupl" runat="server" <asp:LinkButton ID="btnClose" runat="server" CssClass="btn  btn-primary primaryBtn pull-right " Style="margin: 0 5px;"  ><span class="flaticon-delete47 text-danger "></span>Close</asp:LinkButton> CssClass="btn  btn-primary primaryBtn" Style="margin: 0 5px;"><i class="fa fa-floppy-o text-primary"></i> Save</asp:LinkButton>
                                            <asp:LinkButton ID="btnClose" runat="server" CssClass="btn  btn-primary primaryBtn text-danger" OnClick="btnClose_Click" Style="margin: 0 5px;"><i class="fa fa-times text-danger"></i>Close</asp:LinkButton>

                                             <asp:HyperLink ID="lnkbtnAdd" runat="server" CssClass="btn  btn-primary primaryBtn"Style="margin: 0 5px;"  NavigateUrl="~/F_17_Acc/AccInv.aspx">Add</asp:HyperLink>
                                        </div>
                                    </div>





                                </div>



                            </div>
                        </fieldset>
                    </div>--%>
                </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
</asp:Content>

