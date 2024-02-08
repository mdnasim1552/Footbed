<%@ Page Title="" Language="C#" MasterPageFile="~/SPE.Master" AutoEventWireup="true" CodeBehind="StdCostSheet.aspx.cs" Inherits="SPEWEB.F_03_CostABgd.StdCostSheet" %>

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

    <style type="text/css">
        .chzn-single {
            border-radius: 3px !important;
            height: 29px !important;
        }

        .chzn-drop {
            width: 100% !important;
        }

        .chzn-search input {
            width: 100% !important;
        }
    </style>
    



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

                        <div class="col-md-2">
                                <div class="form-group">
                                    <asp:Label ID="Label2" runat="server" CssClass="label">Material Group</asp:Label>
                                    <asp:DropDownList ID="ddlcatagory" OnSelectedIndexChanged="ddlcatagory_SelectedIndexChanged" AutoPostBack="true" runat="server" CssClass="chzn-select form-control form-control-sm">
                                    </asp:DropDownList>
                                </div>
                            </div>

                        <div class="col-md-3 col-sm-3 col-lg-3 ">
                            <div class="form-group">
                                <asp:LinkButton ID="imgbtnsearch" runat="server" CssClass="label" OnClick="imgbtnsearch_Click">Materials Name</asp:LinkButton>
                                <asp:DropDownList ID="ddlprocode" runat="server" AutoPostBack="True" CssClass="chzn-select form-control form-control-sm" OnSelectedIndexChanged="ddlprocode_SelectedIndexChanged"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-1 col-sm-1 col-lg-1 ">
                            <div class="form-group" style="margin-top: 20px;">
                                <asp:LinkButton ID="lnkOpen" runat="server" CssClass="btn btn-primary btn-sm pull-left" OnClick="lnkOpen_Click">Ok</asp:LinkButton>
                            </div>
                        </div>
                        <div class="col-md-2 ">
                            <div class="form-group" style="margin-top: 20px;">
                                <asp:Label ID="lblQty" runat="server" BackColor="Violet" Visible="false" CssClass="lblTxt lblName" Width="150px" Style="text-align: left;"> 
                                </asp:Label>
                                <asp:Label ID="lblUnit" runat="server" BackColor="SkyBlue" CssClass="lblTxt lblName" Width="150px" Style="text-align: left;">
                                </asp:Label>
                                <asp:Label ID="txtDesc" runat="server" BackColor="SkyBlue" Visible="false" CssClass="lblTxt lblName" Width="250px" Style="text-align: left;">
                                </asp:Label>
                            </div>
                        </div>
                        <div class="col-md-3 col-sm-3 col-lg-3" runat="server" id="Panel2" visible="false" style="margin-top:10px">
                            <asp:RadioButtonList ID="rbtnList1" runat="server" CssClass="rbtnList1 form-control" RepeatColumns="6" RepeatDirection="Horizontal"
                                OnSelectedIndexChanged="rbtnList1_SelectedIndexChanged" AutoPostBack="True">
                                <asp:ListItem>Revenue Target</asp:ListItem>
                                <asp:ListItem style="margin-left:10px">Cost</asp:ListItem>
                                <asp:ListItem style="margin-left:10px">Report</asp:ListItem>
                            </asp:RadioButtonList>
                        </div>
                    </div>



                </div>
            </div>




            <div class="card card-fluid" style="min-height: 400px;">
                <div class="card-body">

                    <asp:MultiView ID="MultiView1" runat="server">


                        <asp:View ID="Revenue" runat="server">
                            <div class="row">

                                <div class="col-md-3 col-sm-3 col-lg-3 ">
                                    <div class="form-group">
                                        <asp:LinkButton ID="imgbtnsearchres" runat="server" CssClass="label" OnClick="imgbtnsearchres_Click">Materials Name</asp:LinkButton>
                                        <asp:DropDownList ID="ddlProduct" runat="server" AutoPostBack="True" CssClass="chzn-select form-control form-control-sm"></asp:DropDownList>
                                    </div>
                                </div>

                                <div class="col-md-9 col-sm-9 col-lg-9">
                                    <div class="form-group">
                                        <div class="form-inline" style="padding-top: 20px;">
                                            <asp:LinkButton ID="lnkAddTable" runat="server" CssClass="btn btn-info  btn-xs"
                                                OnClick="lnkAddTable_Click" ToolTip="Add Table"><span class="fa fa-check"></span></asp:LinkButton>

                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <asp:GridView ID="gvrevenue" runat="server" AutoGenerateColumns="False" ShowFooter="True"
                                    Width="479px" OnRowDeleting="gvrevenue_RowDeleting" CssClass="table-striped table-hover table-bordered grvContentarea">
                                    <Columns>
                                        <asp:TemplateField HeaderStyle-Font-Bold="true" HeaderStyle-Font-Size="12px" HeaderStyle-ForeColor="#333333"
                                            HeaderText="SL" ItemStyle-Font-Size="12px">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvsl" runat="server" Text='<%# Convert.ToInt32(Container.DataItemIndex+1).ToString("00")+"." %>'
                                                    Width="50px" Style="text-align: left"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle Font-Bold="True" Font-Size="12px" ForeColor="#333333" />
                                            <ItemStyle Font-Size="12px" />
                                        </asp:TemplateField>
                                        <asp:CommandField ShowDeleteButton="True" DeleteText="" ControlStyle-CssClass="fa fa-times text-red btn-xs" />

                                        <asp:TemplateField HeaderStyle-Font-Bold="true" HeaderStyle-Font-Size="12px" HeaderStyle-ForeColor="#333333"
                                            HeaderText="Code" ItemStyle-Font-Size="12px">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvcode" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rescode")) %>'
                                                    Width="80px" Style="text-align: left"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle Font-Bold="True" Font-Size="12px" ForeColor="#333333" />
                                            <ItemStyle Font-Size="12px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-Font-Bold="true" HeaderStyle-Font-Size="12px" HeaderStyle-ForeColor="#333333"
                                            HeaderText="Description" ItemStyle-Font-Size="12px" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvcodeDescription" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "resdesc")) %>'
                                                    Width="350px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle Font-Bold="True" Font-Size="12px" ForeColor="#333333" />
                                            <ItemStyle Font-Size="12px" />
                                            <FooterStyle HorizontalAlign="left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-Font-Bold="true" HeaderStyle-Font-Size="12px" HeaderStyle-ForeColor="#333333"
                                            HeaderText="Unit" ItemStyle-Font-Size="12px">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvunit" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "resunit")) %>'
                                                    Width="60px" Style="text-align: left"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle Font-Bold="True" Font-Size="12px" ForeColor="#333333" />
                                            <ItemStyle Font-Size="12px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-Font-Bold="true" HeaderStyle-Font-Size="12px" HeaderStyle-ForeColor="#333333" ItemStyle-HorizontalAlign="Right"
                                            HeaderText="Qty">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtgvqty" runat="server" BorderStyle="None" CssClass="GridItmTextBoxRight"
                                                    Font-Size="12px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty")).ToString("#,##0.000000;(#,##0.000000); ") %>'
                                                    Width="80px"></asp:TextBox>
                                            </ItemTemplate>
                                            <HeaderStyle Font-Bold="True" Font-Size="12px" ForeColor="#333333" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-Font-Bold="true" HeaderStyle-Font-Size="12px" HeaderStyle-ForeColor="#333333"
                                            HeaderText="Std. Price" ItemStyle-Font-Size="12px" ItemStyle-HorizontalAlign="Right">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtgvqrate" runat="server" BorderStyle="None" Font-Size="12px" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "rate")).ToString("#,##0.000000;(#,##0.000000); ") %>'
                                                    Width="80px" Style="text-align: right"></asp:TextBox>
                                            </ItemTemplate>
                                            <HeaderStyle Font-Bold="True" Font-Size="12px" ForeColor="#333333" />
                                            <ItemStyle Font-Size="12px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Amount">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvamt" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt")).ToString("#,##0.000;(#,##0.000); ") %>'
                                                    Width="80px" Style="text-align: right" ItemStyle-Font-Size="12px" Font-Size="12px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lblgvfamt" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#CC0066"
                                                    Width="80px" Style="text-align: right"></asp:Label>
                                            </FooterTemplate>
                                            <HeaderStyle Font-Bold="True" Font-Size="12px" ForeColor="#333333" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvpercnt" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "percnt")).ToString("#,##0.00;(#,##0.00); ")+(Convert.ToDouble((DataBinder.Eval(Container.DataItem, "percnt")))>0?"%":"")%>'
                                                    Width="60px" Style="text-align: right" ItemStyle-Font-Size="12px" Font-Size="12px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lblgvfper" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#CC0066"
                                                    Width="60px" Style="text-align: right"></asp:Label>
                                            </FooterTemplate>
                                            <HeaderStyle Font-Bold="True" Font-Size="12px" ForeColor="#333333" />
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


                        <asp:View ID="Cost" runat="server">

                            <div class="row">
                                <div class="col-md-3 col-sm-3 col-lg-3 ">
                                    <div class="form-group">
                                        <asp:LinkButton ID="lblProcess" runat="server" CssClass="label" OnClick="imgbtnsearchProcess_Click">Process</asp:LinkButton>
                                        <asp:DropDownList ID="ddlProcess" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlProcess_SelectedIndexChanged" CssClass="chzn-select form-control form-control-sm"></asp:DropDownList>
                                    </div>
                                </div>
                                 <div class="col-md-3 col-sm-3 col-lg-3 ">
                                    <div class="form-group">
                                        <asp:LinkButton ID="imgbtnResourceCost" runat="server" CssClass="label" OnClick="imgbtnResourceCost_Click">Resource</asp:LinkButton>
                                        <asp:DropDownList ID="ddlResourcesCost" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlResourcesCost_SelectedIndexChanged" CssClass="chzn-select form-control form-control-sm"></asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-3 col-sm-3 col-lg-3 ">
                                    <div class="form-group">
                                        <asp:Label ID="lblSpcf" runat="server" CssClass="label">Specification</asp:Label>
                                        <asp:DropDownList ID="ddlSpcfcode" runat="server" CssClass="chzn-select form-control form-control-sm"></asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-2 ">
                                    <div class="form-group">
                                        <div class="form-inline" style="padding-top: 20px;">
                                            <asp:LinkButton ID="lnkAddResouctCost" runat="server" CssClass="btn btn-info  btn-xs"
                                                OnClick="lnkAddResouctCost_Click" ToolTip="Select Single"><span class="fa fa-check"></span></asp:LinkButton>
                                            &nbsp; 
                                            <asp:LinkButton ID="lnkAddAll" ToolTip="Select All" runat="server" CssClass="btn btn-info  btn-xs" OnClick="lnkAddAll_Click"><span class="fa fa-check-double"></span></asp:LinkButton>


                                        </div>
                                    </div>
                                </div>
                            </div>

                           

                            <div class="row">
                                <asp:GridView ID="gvCost" runat="server" AutoGenerateColumns="False" ShowFooter="True" CssClass="table-striped table-hover table-bordered grvContentarea"
                                    Width="479px" OnRowDeleting="gvCost_RowDeleting">
                                    <Columns>
                                        <asp:TemplateField HeaderStyle-Font-Bold="true" HeaderStyle-Font-Size="12px" HeaderStyle-ForeColor="#333333"
                                            HeaderText="SL" ItemStyle-Font-Size="12px">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvsl0" runat="server" Text='<%# Convert.ToInt32(Container.DataItemIndex+1).ToString("00")+"." %>'
                                                    Width="50px" Style="text-align: left"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle Font-Bold="True" Font-Size="12px" ForeColor="#333333" />
                                            <ItemStyle Font-Size="12px" />
                                        </asp:TemplateField>
                                        <asp:CommandField ShowDeleteButton="True" DeleteText="" ControlStyle-CssClass="fa fa-times text-red btn-xs" />

                                        <asp:TemplateField HeaderStyle-Font-Bold="true" HeaderStyle-Font-Size="12px" HeaderStyle-ForeColor="#333333"
                                            HeaderText="Code" ItemStyle-Font-Size="12px">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvcodeCost" runat="server" Style="text-align: left" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rescode")) %>'
                                                    Width="80px"></asp:Label>
                                                <asp:Label ID="lblSpcfcode" runat="server" Visible="false" Style="text-align: left" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "spcfcod")) %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle Font-Bold="True" Font-Size="12px" ForeColor="#333333" />
                                            <ItemStyle Font-Size="12px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-Font-Bold="true" HeaderStyle-Font-Size="12px" HeaderStyle-ForeColor="#333333"
                                            HeaderText="Description" ItemStyle-Font-Size="12px" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvcodeDescriptionCost" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "resdesc")) %>'
                                                    Width="200px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle Font-Bold="True" Font-Size="12px" ForeColor="#333333" />
                                            <ItemStyle Font-Size="12px" />
                                            <FooterStyle HorizontalAlign="left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-Font-Bold="true" HeaderStyle-Font-Size="12px" HeaderStyle-ForeColor="#333333"
                                            HeaderText="Specification" ItemStyle-Font-Size="12px">

                                            <ItemTemplate>
                                                <asp:Label ID="lblgvspcfdesc" runat="server" Style="text-align: left" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "spcfdesc")) %>'
                                                    Width="250px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle Font-Bold="True" Font-Size="12px" ForeColor="#333333" />
                                            <ItemStyle Font-Size="12px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-Font-Bold="true" HeaderStyle-Font-Size="12px" HeaderStyle-ForeColor="#333333"
                                            HeaderText="Unit" ItemStyle-Font-Size="12px">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvunit0" runat="server" Style="text-align: left" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "resunit")) %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle Font-Bold="True" Font-Size="12px" ForeColor="#333333" />
                                            <ItemStyle Font-Size="12px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-Font-Bold="true" HeaderStyle-Font-Size="10px" HeaderStyle-ForeColor="#333333" ItemStyle-HorizontalAlign="Right"
                                            HeaderText="Consumption Qty">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtgvconqty" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                                    BorderWidth="1px" CssClass="GridItmTextBoxRight"
                                                    Font-Size="10px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "conqty")).ToString("#,##0.000000;(#,##0.000000); ") %>'
                                                    Width="50px"></asp:TextBox>
                                            </ItemTemplate>

                                            <HeaderStyle Font-Bold="True" Font-Size="10px" ForeColor="#333333" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-Font-Bold="true" HeaderStyle-Font-Size="10px" HeaderStyle-ForeColor="#333333" ItemStyle-HorizontalAlign="Right"
                                            HeaderText="Wastage %">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtgvwestpc" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                                    BorderWidth="1px" CssClass="GridItmTextBoxRight"
                                                    Font-Size="10px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "westpc")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="40px"></asp:TextBox>
                                            </ItemTemplate>

                                            <HeaderStyle Font-Bold="True" Font-Size="10px" ForeColor="#333333" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-Font-Bold="true" HeaderStyle-Font-Size="12px" HeaderStyle-ForeColor="#333333" ItemStyle-HorizontalAlign="Right"
                                            HeaderText="Qty">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtgvqtyCost" runat="server" BorderStyle="None" CssClass="GridItmTextBoxRight"
                                                    Font-Size="12px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty")).ToString("#,##0.000000;(#,##0.000000); ") %>'
                                                    Width="80px"></asp:TextBox>
                                            </ItemTemplate>

                                            <HeaderStyle Font-Bold="True" Font-Size="12px" ForeColor="#333333" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-Font-Bold="true" HeaderStyle-Font-Size="12px" HeaderStyle-ForeColor="#333333"
                                            HeaderText="Std. Price" ItemStyle-Font-Size="12px" ItemStyle-HorizontalAlign="Right">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtgvqrateCost" runat="server" BorderStyle="None" Font-Size="12px"
                                                    Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "rate")).ToString("#,##0.000000;(#,##0.000000); ") %>'
                                                    Width="80px"></asp:TextBox>
                                            </ItemTemplate>
                                            <HeaderStyle Font-Bold="True" Font-Size="12px" ForeColor="#333333" />
                                            <ItemStyle Font-Size="12px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Amount">
                                            <ItemTemplate>
                                                <%-- <asp:Label ID="lblgvamtCost" runat="server" Font-Size="12px" 
                                                                             ItemStyle-Font-Size="12px" style="text-align: right" 
                                                                             Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt")).ToString("#,##0.00;(#,##0.00); ") %>' 
                                                                             Width="80px"></asp:Label>--%>
                                                <asp:TextBox ID="txtgvamtCost" runat="server" BorderStyle="None" Font-Size="12px"
                                                    Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt")).ToString("#,##0.000;(#,##0.000); ") %>'
                                                    Width="80px"></asp:TextBox>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lblgvfamtCost" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#CC0066"
                                                    Style="text-align: right" Width="80px"></asp:Label>
                                            </FooterTemplate>
                                            <HeaderStyle Font-Bold="True" Font-Size="12px" ForeColor="#333333" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="%">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvpercnt" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "percnt")).ToString("#,##0.00;(#,##0.00); ")+(Convert.ToDouble((DataBinder.Eval(Container.DataItem, "percnt")))>0?"%":"")%>'
                                                    Width="60px" Style="text-align: right" ItemStyle-Font-Size="12px" Font-Size="12px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lblgvfper" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#CC0066"
                                                    Width="60px" Style="text-align: right"></asp:Label>
                                            </FooterTemplate>
                                            <HeaderStyle Font-Bold="True" Font-Size="12px" ForeColor="#333333" />
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

                        <asp:View ID="RptStdCost" runat="server">
                            <div class="row">
                                <asp:GridView ID="gvRptCost" runat="server" AutoGenerateColumns="False" CssClass="table-striped table-hover table-bordered grvContentarea" ShowFooter="True"
                                    Width="750px" OnRowDataBound="gvRptCost_RowDataBound">
                                    <Columns>
                                        <asp:TemplateField HeaderStyle-Font-Bold="true" HeaderStyle-Font-Size="12px" HeaderStyle-ForeColor="#333333"
                                            HeaderText="SL" ItemStyle-Font-Size="12px">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvsl1" runat="server" Style="text-align: left" Text='<%# Convert.ToInt32(Container.DataItemIndex+1).ToString("00")+"." %>'
                                                    Width="25px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle Font-Bold="True" Font-Size="12px" ForeColor="#333333" />
                                            <ItemStyle Font-Size="12px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-Font-Bold="true" HeaderStyle-Font-Size="12px" HeaderStyle-ForeColor="#333333"
                                            HeaderText="Description" ItemStyle-Font-Size="12px" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvrptDescription" runat="server"
                                                    Text='<%# 
                                                                                     "<B>"+ Convert.ToString(DataBinder.Eval(Container.DataItem, "grpdesc")) + "</B>"+
                                                                         (DataBinder.Eval(Container.DataItem, "prodesc").ToString().Trim().Length>0 ? 
                                                                         (Convert.ToString(DataBinder.Eval(Container.DataItem, "grpdesc")).Trim().Length>0 ?  "<br>" : "")+                                                             
                                                                   "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"+ 
                                                                         "<B>"+Convert.ToString(DataBinder.Eval(Container.DataItem, "prodesc")).Trim()+"</B>": "")  + 
                                                                         (DataBinder.Eval(Container.DataItem, "resdesc").ToString().Trim().Length>0 ? 
                                                                          (Convert.ToString(DataBinder.Eval(Container.DataItem, "grpdesc")).Trim().Length>0 ?   "<br>" 
                                                                          :(Convert.ToString(DataBinder.Eval(Container.DataItem, "prodesc")).Trim().Length>0 ? "<br>":"")) + 
                                                                         "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"+ 
                                                                         Convert.ToString(DataBinder.Eval(Container.DataItem, "resdesc")).Trim(): "")
                                                                                      %>'
                                                    Width="250px">

                                                </asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle Font-Bold="True" Font-Size="12px" ForeColor="#333333" />
                                            <ItemStyle Font-Size="12px" />
                                            <FooterStyle HorizontalAlign="left" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderStyle-Font-Bold="true" HeaderStyle-Font-Size="12px" HeaderStyle-ForeColor="#333333"
                                            HeaderText="Specification" ItemStyle-Font-Size="12px">

                                            <ItemTemplate>
                                                <asp:Label ID="lblgvspcfdesc" runat="server" Style="text-align: left" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "spcfdesc")) %>'
                                                    Width="200px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle Font-Bold="True" Font-Size="12px" ForeColor="#333333" />
                                            <ItemStyle Font-Size="12px" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderStyle-Font-Bold="true" HeaderStyle-Font-Size="12px" HeaderStyle-ForeColor="#333333"
                                            HeaderText="Unit" ItemStyle-Font-Size="12px">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvrptunit" runat="server" Style="text-align: left" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "resunit")) %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle Font-Bold="True" Font-Size="12px" ForeColor="#333333" />
                                            <ItemStyle Font-Size="12px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-Font-Bold="true" HeaderStyle-Font-Size="12px" HeaderStyle-ForeColor="#333333"
                                            HeaderText="Consumption Qty">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvconqty" runat="server" BorderStyle="None" CssClass="GridItmTextBoxRight" Style="text-align: right;"
                                                    Font-Size="12px" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "conqty")).ToString("#,##0.0000;(#,##0.0000); ") %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle Font-Bold="True" Font-Size="12px" ForeColor="#333333" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-Font-Bold="true" HeaderStyle-Font-Size="12px" HeaderStyle-ForeColor="#333333"
                                            HeaderText="Wastage %">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvwestpc" runat="server" BorderStyle="None" CssClass="GridItmTextBoxRight" Style="text-align: right;"
                                                    Font-Size="12px" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "westpc")).ToString("#,##0.0000;(#,##0.0000); ") %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle Font-Bold="True" Font-Size="12px" ForeColor="#333333" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderStyle-Font-Bold="true" HeaderStyle-Font-Size="12px" HeaderStyle-ForeColor="#333333"
                                            HeaderText="Total Qty">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvrptqtyCost" runat="server" BorderStyle="None" CssClass="GridItmTextBoxRight" Style="text-align: right;"
                                                    Font-Size="12px" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty")).ToString("#,##0.0000;(#,##0.0000); ") %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle Font-Bold="True" Font-Size="12px" ForeColor="#333333" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-Font-Bold="true" HeaderStyle-Font-Size="12px" HeaderStyle-ForeColor="#333333"
                                            HeaderText="Std. Price" ItemStyle-Font-Size="12px" ItemStyle-HorizontalAlign="Right">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvrptqrateCost" runat="server" BorderStyle="None" Font-Size="12px"
                                                    Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "rate")).ToString("#,##0.000;(#,##0.000); ") %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle Font-Bold="True" Font-Size="12px" ForeColor="#333333" />
                                            <ItemStyle Font-Size="12px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Amount">
                                            <ItemTemplate>

                                                <asp:Label ID="lblgvrptamtCost" runat="server" BorderStyle="None" Font-Size="12px"
                                                    Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt")).ToString("#,##0.000;(#,##0.000); ") %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle Font-Bold="True" Font-Size="12px" ForeColor="#333333" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="%" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvrptpercnt" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "percnt")).ToString("#,##0.00;(#,##0.00); ")+(Convert.ToDouble((DataBinder.Eval(Container.DataItem, "percnt")))>0?"%":"")%>'
                                                    Width="60px" Style="text-align: right" ItemStyle-Font-Size="12px" Font-Size="12px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle Font-Bold="True" Font-Size="12px" ForeColor="#333333" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-Font-Bold="true" HeaderStyle-Font-Size="12px" HeaderStyle-ForeColor="#333333"
                                            HeaderText="Avg Price" ItemStyle-Font-Size="12px" ItemStyle-HorizontalAlign="Right" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvavgrate" runat="server" BorderStyle="None" Font-Size="12px"
                                                    Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "avgrate")).ToString("#,##0.000;(#,##0.000); ") %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle Font-Bold="True" Font-Size="12px" ForeColor="#333333" />
                                            <ItemStyle Font-Size="12px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Avg. Amount" Visible="false">
                                            <ItemTemplate>

                                                <asp:Label ID="lblavgamt" runat="server" BorderStyle="None" Font-Size="12px"
                                                    Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "avgamt")).ToString("#,##0.000;(#,##0.000); ") %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle Font-Bold="True" Font-Size="12px" ForeColor="#333333" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-Font-Bold="true" HeaderStyle-Font-Size="12px" HeaderStyle-ForeColor="#333333"
                                            HeaderText="Last Price" ItemStyle-Font-Size="12px" ItemStyle-HorizontalAlign="Right" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvmaxrate" runat="server" BorderStyle="None" Font-Size="12px"
                                                    Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "maxrate")).ToString("#,##0.000;(#,##0.000); ") %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle Font-Bold="True" Font-Size="12px" ForeColor="#333333" />
                                            <ItemStyle Font-Size="12px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Last Amount" Visible="false">
                                            <ItemTemplate>

                                                <asp:Label ID="lblgvmaxamt" runat="server" BorderStyle="None" Font-Size="12px"
                                                    Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "maxamt")).ToString("#,##0.000;(#,##0.000); ") %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle Font-Bold="True" Font-Size="12px" ForeColor="#333333" />
                                        </asp:TemplateField>
                                    </Columns>
                                    <FooterStyle CssClass="grvFooter" />
                                    <EditRowStyle />
                                    <AlternatingRowStyle />
                                    <PagerStyle CssClass="gvPagination" />
                                    <HeaderStyle CssClass="grvHeader" />
                                </asp:GridView>
                                <asp:Panel ID="Panel5" runat="server">
                                    <div class="form-group">
                                        <asp:Label ID="lbltxtProfit" runat="server" CssClass="lblTxt lblName" Visible="false">Standard Profit:</asp:Label>
                                        <asp:Label ID="lbltxtvalueProfit" runat="server" CssClass="lblTxt lblName" Visible="false">Standard Profit:</asp:Label>



                                    </div>

                                </asp:Panel>
                            </div>


                        </asp:View>

                    </asp:MultiView>
                </div>
            </div>



            <div class="row">
                <div class="col-md-12">
                    <asp:Label ID="lblmsgprint" runat="server"></asp:Label>
                </div>
            </div>




        </ContentTemplate>
    </asp:UpdatePanel>


</asp:Content>

