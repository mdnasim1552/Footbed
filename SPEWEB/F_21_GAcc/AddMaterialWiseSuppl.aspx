<%@ Page Title="" Language="C#" MasterPageFile="~/SPE.Master" AutoEventWireup="true" CodeBehind="AddMaterialWiseSuppl.aspx.cs" Inherits="SPEWEB.F_21_GAcc.AddMaterialWiseSuppl" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

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




    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="nahidProgressbar">
                <asp:UpdateProgress ID="UpdateProgress2" runat="server" AssociatedUpdatePanelID="UpdatePanel1" DisplayAfter="30">
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
                <div class="card-body">
                    <div class="row">
                        <div class="col-md-2 col-sm-2 col-lg-2 ">
                            <div class="form-group">
                                <asp:Label ID="Label1" runat="server" CssClass="label">Information for</asp:Label>
                                <asp:DropDownList ID="ddlSurveyType" runat="server" CssClass="chzn-select form-control form-control-sm">

                                    <asp:ListItem Value="1">Market Survey Report</asp:ListItem>
                                    <asp:ListItem Value="2">Material Wise Suppliers List</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-1 col-sm-1 col-lg-1 ">
                            <div class="form-group">
                                <asp:Label ID="lblPage" runat="server" CssClass="label">Page Size</asp:Label>
                                <asp:DropDownList ID="ddlpagesize" runat="server" CssClass="chzn-select form-control form-control-sm" AutoPostBack="True" TabIndex="2" OnSelectedIndexChanged="ddlpagesize_SelectedIndexChanged">
                                    <asp:ListItem>10</asp:ListItem>
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
                                    <asp:ListItem>1100</asp:ListItem>
                                    <asp:ListItem>1200</asp:ListItem>
                                    <asp:ListItem>1500</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-2 col-sm-2 col-lg-2 ">
                            <div class="form-group" style="margin-top: 20px;">
                                <asp:CheckBox ID="ChckCopy" runat="server" Text="Copy remaining all specification" CssClass="checkbox" />

                            </div>
                        </div>
                        <div class="col-md-1 col-sm-1 col-lg-1 ">
                            <div class="form-group" style="margin-top: 20px;">
                                <asp:LinkButton ID="lbtnCopy" runat="server" CssClass="btn btn-primary btn-sm pull-left" OnClick="lbtnCopy_Click">Copy</asp:LinkButton>
                            </div>
                        </div>
                    </div>


                </div>
            </div>




            <div class="card card-fluid">
                <div class="card-body" style="min-height: 450px">

                    <asp:MultiView ID="MultiView1" runat="server" ActiveViewIndex="0">

                        <asp:View ID="View1" runat="server">
                            <div class="row">
                                <div class="col-md-2 col-sm-2 col-lg-2 ">
                                    <div class="form-group">
                                        <asp:LinkButton ID="ImgbtnFindRes1" runat="server" CssClass="label" OnClick="ImgbtnFindRes1_Click">Materials</asp:LinkButton>
                                        <asp:DropDownList ID="ddlResList1" runat="server" OnSelectedIndexChanged="ddlResList1_SelectedIndexChanged" CssClass="chzn-select form-control form-control-sm" AutoPostBack="true" TabIndex="2"></asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-3 col-sm-3 col-lg-3">
                                    <div class="form-group">
                                        <asp:LinkButton ID="ImgbtnFindSpecification" runat="server" CssClass="label" OnClick="ImgbtnFindSpecification_Click">Specification</asp:LinkButton>
                                        <asp:DropDownList ID="ddlSpecification" runat="server" CssClass="chzn-select form-control form-control-sm"></asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-1 col-sm-1 col-lg-1 ">
                                    <div class="form-group" style="margin-top: 20px;">
                                        <asp:LinkButton ID="lbtnSelectRes1" runat="server" CssClass="btn btn-primary btn-sm pull-left" OnClick="lbtnSelectRes1_Click">Ok</asp:LinkButton>
                                    </div>
                                </div>
                                <div class="col-md-3 col-sm-3 col-lg-3 ">
                                    <div class="form-group">
                                        <asp:LinkButton ID="ImgbtnFindSupl1" runat="server" CssClass="label" OnClick="ImgbtnFindSupl1_Click">Supplier</asp:LinkButton>
                                        <asp:HyperLink ID="HyperLink2" runat="server" CssClass="btnok pull-left" ToolTip="Create Supplier" Target="_blank"
                                            NavigateUrl="~/F_21_GAcc/AccSubCodeBook?InputType=ResCodePrint"><span class="fa fa-plus-circle"></span></asp:HyperLink>
                                        <asp:DropDownList ID="ddlSupl1" runat="server" CssClass="chzn-select form-control form-control-sm" AutoPostBack="true" TabIndex="2"></asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-1 col-sm-1 col-lg-1 ">
                                    <div class="form-group" style="margin-top: 20px;">
                                        <asp:LinkButton ID="lbtnSelectSupl1" runat="server" CssClass="btn btn-primary btn-sm pull-left" OnClick="lbtnSelectSupl1_Click">Select</asp:LinkButton>
                                    </div>
                                </div>
                                <div class="col-md-1 col-sm-1 col-lg-1 ">
                                    <div class="form-group">
                                        <asp:LinkButton ID="btnCurr" runat="server" CssClass="label" OnClick="btnCurr_Click">Currency</asp:LinkButton>
                                        <asp:HyperLink ID="HyperLink1" runat="server" CssClass="btnok pull-left" ToolTip="Create List" Target="_blank"
                                            NavigateUrl="~/F_17_Acc/AccConversion.aspx"><span class="fa fa-plus-circle"></span></asp:HyperLink>
                                        <asp:DropDownList ID="ddlCurrency" runat="server" CssClass="chzn-select form-control form-control-sm" AutoPostBack="True" OnSelectedIndexChanged="ddlCurrency_SelectedIndexChanged"></asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-1 col-sm-1 col-lg-1 ">
                                    <div class="form-group">
                                        <asp:Label ID="Label10" runat="server" CssClass="label">Con. Rate</asp:Label>
                                        <asp:TextBox ID="lblConRate" runat="server" CssClass="inputTxt inputName inpPixedWidth " ReadOnly="true"></asp:TextBox>

                                    </div>
                                </div>

                            </div>







                            <div class="row">


                                <asp:GridView ID="gvSuplInfo" runat="server" AllowPaging="True" OnRowDeleting="gvSuplInfo_RowDeleting" CssClass="table-striped table-hover table-bordered grvContentarea table-responsive"
                                    AutoGenerateColumns="False" OnPageIndexChanging="gvSuplInfo_PageIndexChanging"
                                    ShowFooter="True" OnRowDataBound="gvSuplInfo_RowDataBound">
                                    <PagerSettings NextPageText="Next" PreviousPageText="Previous" Position="Bottom"
                                        Mode="NumericFirstLast" />

                                    <Columns>
                                        <asp:TemplateField HeaderText="Sl">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvSlNo0" runat="server" Height="16px" Style="text-align: right"
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="35px"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:CommandField ShowDeleteButton="True" ControlStyle-ForeColor="Red" DeleteText="<span class='fa fa-trash'></span>" />

                                        <asp:TemplateField HeaderText="Res Code" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvResCod0" runat="server" Height="16px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rsircode")) %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Supplier Code" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvSuplCod0" runat="server" Height="16px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "ssircode")) %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Spcfcode Code" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvspcfcod" runat="server" Height="16px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "spcfcod")) %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Supplier Name">

                                            <ItemTemplate>
                                                <asp:Label ID="lblgvspcfcods" runat="server" Height="16px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "ssirdesc1")) %>'
                                                    Width="350px"></asp:Label>

                                            </ItemTemplate>
                                            <FooterStyle HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Unit">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvrsirunit" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                                    BorderWidth="0px" Font-Size="11px" Style="text-align: left; background-color: Transparent"
                                                    Text='<%# DataBinder.Eval(Container.DataItem, "rsirunit").ToString() %>' Width="50px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle HorizontalAlign="Center" />
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Pro Position </br> Qty">

                                            <ItemTemplate>
                                                <asp:Label ID="lblgvpropqty" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                                    BorderWidth="0px" Font-Size="11px" Style="text-align: right; background-color: Transparent"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "propqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle HorizontalAlign="Center" />
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Rate">

                                            <ItemTemplate>
                                                <asp:TextBox ID="txtgvRate1" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                                    BorderWidth="0px" Font-Size="11px" Style="text-align: right; background-color: Transparent"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "rate")).ToString("#,##0.0000;(#,##0.0000); ") %>'
                                                    Width="80px"></asp:TextBox>
                                            </ItemTemplate>
                                            <FooterStyle HorizontalAlign="Center" />
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Conv. Rate">

                                            <ItemTemplate>
                                                <asp:Label ID="lblgvconrate" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                                    BorderWidth="0px" Font-Size="11px" Style="text-align: right; background-color: Transparent"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "conrate")).ToString("#,##0.0000;(#,##0.0000); ") %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle HorizontalAlign="Center" />
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Currency">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvcurdesc" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                                    BorderWidth="0px" Font-Size="11px" Style="text-align: left; background-color: Transparent"
                                                    Text='<%# DataBinder.Eval(Container.DataItem, "curdesc").ToString() %>' Width="50px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle HorizontalAlign="Center" />
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Remarks">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtgvSuplRemarks" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                                    BorderWidth="0px" Font-Size="11px" Style="text-align: left; background-color: Transparent"
                                                    Text='<%# DataBinder.Eval(Container.DataItem, "rmrks").ToString() %>' Width="120px"></asp:TextBox>
                                            </ItemTemplate>
                                            <FooterStyle HorizontalAlign="Center" />
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Recommended </br> of BOM ?">
                                            <ItemTemplate>
                                                <asp:DropDownList ID="ddlactive" runat="server" Width="90px">
                                                    <asp:ListItem Value="false">False</asp:ListItem>
                                                    <asp:ListItem Value="true">True</asp:ListItem>

                                                </asp:DropDownList>
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

                        </asp:View>

                    </asp:MultiView>


                </div>
            </div>




        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

