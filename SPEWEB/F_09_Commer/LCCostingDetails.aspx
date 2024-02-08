<%@ Page Title="" Language="C#" MasterPageFile="~/SPE.Master" AutoEventWireup="true" CodeBehind="LCCostingDetails.aspx.cs" Inherits="SPEWEB.F_09_Commer.LCCostingDetails" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="DropCheck" Namespace="xMilk" TagPrefix="dchk1" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script language="javascript" type="text/javascript">
        $(document).ready(function () {
            //For navigating using left and right arrow of the keyboard
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);
        });
        function pageLoaded() {

            $('.chzn-select').chosen({ search_contains: true });
            $('#tblrpprodetails').Scrollable({
            });



        };

    </script>
    <style>
        .btnlgn {
            margin-bottom: 2px;
        }

        fieldset > legend:first-of-type {
            -webkit-margin-top-collapse: separate;
            margin-bottom: 20px;
        }
        .chzn-single {
    border-radius: 3px !important;
    height: 29px !important;
    
}
    </style>



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
                <div class="card-body">

                    <div class="row">
                        <div class="col-md-3 col-sm-3 col-lg-3 ">
                            <div class="form-group">
                                <asp:Label ID="lbllc" runat="server" CssClass="label">LC Number</asp:Label>
                                <asp:DropDownList ID="ddlLcCode" runat="server" CssClass="chzn-select form-control form-control-sm" OnSelectedIndexChanged="ddlLcCode_SelectedIndexChanged" AutoPostBack="true">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-3 col-sm-3 col-lg-3 ">

                            <div class="form-group">
                                <asp:Label ID="Label4" runat="server" CssClass="label">Store ID</asp:Label>
                                <asp:DropDownList ID="ddlStorid" runat="server" CssClass="chzn-select form-control form-control-sm" OnSelectedIndexChanged="ddlStorid_SelectedIndexChanged" AutoPostBack="true">
                                </asp:DropDownList>
                            </div>
                        </div>

                        <div class="col-md-3 col-sm-3 col-lg-3 ">

                            <div class="form-group">
                                <asp:Label ID="Label5" runat="server" CssClass="label">GRN No</asp:Label>                              
                                <asp:DropDownList ID="ddlgenno" runat="server" AutoPostBack="true" OnSelectedIndexChanged ="ddlgenno_SelectedIndexChanged" CssClass="chzn-select form-control form-control-sm">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-1 col-sm-1 col-lg-1 ">
                            <div class="form-group" style="margin-top: 20px;">
                                <asp:LinkButton ID="lnkOk" runat="server" CssClass="btn btn-primary btn-sm pull-left" OnClick="lnkOk_Click">Ok</asp:LinkButton>
                            </div>
                        </div>
                         <div class="col-md-2 col-sm-2 col-lg-2 ">
                            <div class="form-group" style="margin-top: 20px;">
                           <asp:RadioButtonList ID="RadioButtonList2" AutoPostBack="true" OnSelectedIndexChanged="RadioButtonList2_SelectedIndexChanged" runat="server"  RepeatDirection="Horizontal">
                                <asp:ListItem Selected="True" Value="0">Proposed Wise</asp:ListItem>
                                <asp:ListItem Style="margin-left:20px" Value="1">Acc Wise</asp:ListItem>
                            </asp:RadioButtonList>
                                 </div>
                        </div>
                    </div>
                </div>
            </div>


            <div class="card card-fluid">
                <div class="card-body" style="min-height: 500px;">
                    <div class="row">

                        <div class="col-md-7 col-sm-7 col-lg-7 ">

                            <div class="log-divider" id="pnlPro" runat="server" visible="False">
                                <span>
                                    <i class="fa fa-fw fa-dollar-sign"></i>Product Details</span>
                            </div>
                            <div class="row">
                                <asp:GridView ID="dgvReceive" runat="server" AllowPaging="false"
                                    AutoGenerateColumns="False" ShowFooter="true"
                                    CssClass="table-striped table-hover table-bordered grvContentarea">
                                    <PagerSettings NextPageText="Next" PreviousPageText="Previous" Position="Top"
                                        Mode="NumericFirstLast" />

                                    <Columns>
                                        <asp:TemplateField HeaderText="SL" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvSlNo1" runat="server" Font-Bold="False" Font-Size="12px" Style="text-align: center"
                                                    Text='<%# Convert.ToInt32(Container.DataItemIndex+1).ToString("00")+"." %>' Width="40px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle Font-Bold="True" Font-Size="12px" ForeColor="#333333" />
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderStyle-Font-Size="12px" HeaderStyle-ForeColor="#333333" HeaderText="Res.Code"
                                            ItemStyle-HorizontalAlign="Left" Visible="true">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvResCode1" runat="server" Font-Size="11px" Width="70px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rescod")) %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle Font-Bold="True" Font-Size="12px" ForeColor="#333333" />
                                            <HeaderStyle Font-Size="12px" ForeColor="#333333" />
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderStyle-Font-Size="12px" HeaderStyle-ForeColor="#333333" HeaderText="Item Code"
                                            ItemStyle-HorizontalAlign="Left" Visible="true">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvscode" runat="server" Font-Size="10px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "scode")) %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle Font-Bold="True" Font-Size="12px" ForeColor="#333333" />
                                            <HeaderStyle Font-Size="12px" ForeColor="#333333" />
                                            <ItemStyle HorizontalAlign="Center" Width="70px" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Resource Description" HeaderStyle-Font-Size="12px"
                                            HeaderStyle-ForeColor="#333333" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvResdesc1" runat="server" Font-Size="10px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sirdesc")) %>'></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lbTotalLcCost" runat="server" Font-Size="11px" ForeColor="Black">Total</asp:Label>

                                            </FooterTemplate>
                                            <HeaderStyle Font-Bold="True" Font-Size="12px" ForeColor="#333333" />
                                            <ItemStyle HorizontalAlign="Left" />
                                            <FooterStyle HorizontalAlign="Right" Font-Bold="true" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Specification" HeaderStyle-Font-Size="12px"
                                            HeaderStyle-ForeColor="#333333" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvSpc" runat="server" Font-Size="10px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "spcfdesc")) %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle Font-Bold="True" Font-Size="12px" ForeColor="#333333" />
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Rate">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvTrate" runat="server" Font-Size="12px" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "trate")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="65px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle Font-Bold="True" Font-Size="12px" ForeColor="#333333" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Received Qty">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvordqty" runat="server" Font-Size="12px" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "rcvqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lblgvFordqty" runat="server" Font-Size="11px" Font-Bold="True"
                                                    Width="75px" Style="text-align: right"></asp:Label>
                                            </FooterTemplate>
                                            <HeaderStyle Font-Bold="True" Font-Size="12px" ForeColor="#333333" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>

                                    </Columns>


                                    <FooterStyle BackColor="#F5F5F5" />
                                    <EditRowStyle />
                                    <AlternatingRowStyle />
                                    <PagerStyle CssClass="gvPagination" />
                                    <HeaderStyle CssClass="grvHeader" />
                                </asp:GridView>
                            </div>
                        </div>

                        <div class="col-md-5 col-sm-5 col-lg-5 ">
                            <div class="log-divider" id="pnlCosting" runat="server" visible="False">
                                <span>
                                    <i class="fa fa-fw fa-dollar-sign"></i>Costing Details</span>
                            </div>
                            <div class="row">


                                <asp:GridView ID="gvlccost" runat="server" AllowPaging="false"
                                    AutoGenerateColumns="False" ShowFooter="true"
                                    CssClass="table-striped table-hover table-bordered grvContentarea">
                                    <PagerSettings NextPageText="Next" PreviousPageText="Previous" Position="Top"
                                        Mode="NumericFirstLast" />

                                    <Columns>
                                        <asp:TemplateField HeaderText="SL" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:Label ID="Label1" runat="server" Font-Bold="False" Font-Size="12px" Style="text-align: center"
                                                    Text='<%# Convert.ToInt32(Container.DataItemIndex+1).ToString("00")+"." %>' Width="40px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle Font-Bold="True" Font-Size="12px" ForeColor="#333333" />
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderStyle-Font-Size="12px" HeaderStyle-ForeColor="#333333" HeaderText="Res.Code"
                                            ItemStyle-HorizontalAlign="Left" Visible="true">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvResCodelc" runat="server" Font-Size="11px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rescod")) %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle Font-Bold="True" Font-Size="12px" ForeColor="#333333" />
                                            <HeaderStyle Font-Size="12px" ForeColor="#333333" />
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Resource Description" HeaderStyle-Font-Size="12px"
                                            HeaderStyle-ForeColor="#333333" ItemStyle-HorizontalAlign="Left">
                                            <FooterTemplate>
                                                <asp:Label ID="lbTotalLcCost" runat="server" Font-Size="11px" ForeColor="Black">Total</asp:Label>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvResdesclc" runat="server" Font-Size="10px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "resdesc")) %>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle Font-Bold="True" Font-Size="12px" ForeColor="#333333" />
                                            <ItemStyle HorizontalAlign="Left" />
                                            <FooterStyle HorizontalAlign="Right" Font-Bold="true" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Total L/C Cost" HeaderStyle-Font-Size="12px" HeaderStyle-ForeColor="#333333">
                                            <FooterTemplate>
                                                <asp:Label ID="lblgrvFtolcCost" runat="server" Font-Bold="True" Font-Size="11px"
                                                    Width="70px" Style="text-align: right"></asp:Label>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvtolcCost" runat="server" Font-Size="12px" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "tolccost")).ToString("#,##0.00;-#,##0.00; ") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle Font-Size="12px" ForeColor="#333333" />
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText=" Previous Cost" HeaderStyle-Font-Size="12px" HeaderStyle-ForeColor="#333333">
                                            <FooterTemplate>
                                                <asp:Label ID="lblgrvFprelcCost" runat="server" Font-Bold="True" Font-Size="11px"
                                                    Width="70px" Style="text-align: right"></asp:Label>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvprelcCost" runat="server" Font-Size="12px" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "utorecamt")).ToString("#,##0.00;-#,##0.00; ") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle Font-Size="12px" ForeColor="#333333" />
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Received Cost" HeaderStyle-Font-Size="12px" HeaderStyle-ForeColor="Red">
                                            <FooterTemplate>
                                                <asp:Label ID="lblgrvFcurlcCost" runat="server" Font-Bold="True" Font-Size="11px"
                                                    Width="70px" Style="text-align: right"></asp:Label>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtgvcurlcCostt" runat="server" Font-Size="12px" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "recamt")).ToString("#,##0.00;-#,##0.00; ") %>'
                                                    Width="70px" BorderStyle="Solid" BorderColor="#889ae0" BorderWidth="1px" Style="text-align: right; background-color: transparent;"></asp:TextBox>
                                            </ItemTemplate>
                                            <HeaderStyle Font-Size="12px" ForeColor="Red" />
                                            <ItemStyle HorizontalAlign="Right" ForeColor="Red" />
                                            <FooterStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText=" Balance" HeaderStyle-Font-Size="12px" HeaderStyle-ForeColor="#333333">
                                            <FooterTemplate>
                                                <asp:Label ID="lblgrvFlcbalance" runat="server" Font-Bold="True" Font-Size="11px"
                                                    Width="70px" Style="text-align: right"></asp:Label>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvlcbalance" runat="server" Font-Size="12px" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "balamt")).ToString("#,##0.00;-#,##0.00; ") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle Font-Size="12px" ForeColor="#333333" />
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>


                                    </Columns>


                                    <FooterStyle BackColor="#F5F5F5" />
                                    <EditRowStyle />
                                    <AlternatingRowStyle />
                                    <PagerStyle CssClass="gvPagination" />
                                    <HeaderStyle CssClass="grvHeader" />
                                </asp:GridView>

                            </div>
                        </div>

                    </div>


                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>

