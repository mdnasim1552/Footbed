<%@ Page Title="" Language="C#" MasterPageFile="~/SPE.Master" AutoEventWireup="true" CodeBehind="MiniStockInput.aspx.cs" Inherits="SPEWEB.F_11_RawInv.MiniStockInput" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <style type="text/css">
        .chzn-container .chzn-drop {
            width: 100% !important;
        }

        .chzn-search input {
            width: 100% !important;
        }

        .chzn-container-single .chzn-single {
            height: 28px !important;
        }
    </style>

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

    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
        <ContentTemplate>

            <div class="card card-fluid mb-1">
                <div class="card-body pb-3">
                    <div class="row">

                        <div class="col-md-2">
                            <asp:Label runat="server" ID="lblStore">Store</asp:Label>
                            <asp:DropDownList runat="server" ID="ddlStore" CssClass="form-control form-control-sm chzn-select">
                            </asp:DropDownList>
                        </div>

                        <div class="col-md-2">
                            <asp:Label runat="server" ID="lblCodeBook">Code Book</asp:Label>
                            <asp:DropDownList runat="server" ID="ddlCodeBook" CssClass="form-control form-control-sm chzn-select" OnSelectedIndexChanged="ddlCodeBook_SelectedIndexChanged" AutoPostBack="true">
                            </asp:DropDownList>
                        </div>

                        <div class="col-md-2">
                            <asp:Label runat="server" ID="lblGroup">Group</asp:Label>
                            <asp:DropDownList runat="server" ID="ddlGroup" CssClass="form-control form-control-sm chzn-select">
                            </asp:DropDownList>
                        </div>

                        <div class="col-md-1">
                            <asp:LinkButton runat="server" ID="lnkbtnOk" CssClass="btn btn-sm btn-primary" Style="margin-top: 20px;" Text="Ok" OnClick="lnkbtnOk_Click"></asp:LinkButton>
                        </div>

                    <%-- <div class="col-md-1">
                            <asp:CheckBox runat="server" ID="chkShowAll" CssClass="form-control form-control-sm bg-secondary" Style="margin-top: 20px;" Text="Show All" />
                        </div>--%>

                    </div>
                </div>
            </div>

            <div class="card card-fluid">
                <div class="card-body" style="min-height: 450px;">

                    <asp:MultiView runat="server" ID="stkLvlMultiview">
                        <asp:View runat="server" ID="viewMiniStkInput">

                            <div class="table-responsive">
                                <asp:GridView ID="gvMiniStock" runat="server" AutoGenerateColumns="False" ShowFooter="True" CssClass="table-striped table-hover table-bordered grvContentarea"
                                    Width="450px" OnRowDeleting="gvMiniStock_RowDeleting">
                                    <RowStyle />
                                    <Columns>
                                        <asp:TemplateField HeaderStyle-Font-Bold="true" HeaderStyle-Font-Size="12px" HeaderStyle-ForeColor="#333333"
                                            HeaderText="SL" ItemStyle-Font-Size="12px">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvsl" runat="server" Text='<%# Convert.ToInt32(Container.DataItemIndex+1).ToString("00")+"." %>'
                                                    Width="40px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle Font-Bold="True" Font-Size="12px" ForeColor="#333333" />
                                            <ItemStyle Font-Size="12px" />
                                        </asp:TemplateField>
                                        
                                        <asp:CommandField ShowDeleteButton="True" ControlStyle-CssClass="text-red" DeleteText="<span class='fa fa-trash'></span>" />
                                        
                                        <asp:TemplateField HeaderStyle-Font-Bold="false" HeaderStyle-Font-Size="12px" HeaderStyle-ForeColor="#333333"
                                            HeaderText="Item Code" ItemStyle-Font-Size="12px" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvitemcode" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rescode")) %>'
                                                    Width="90px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle Font-Bold="True" Font-Size="12px" ForeColor="#333333" />
                                            <ItemStyle Font-Size="12px" HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        
                                        <asp:TemplateField HeaderStyle-Font-Bold="false" HeaderStyle-Font-Size="12px" HeaderStyle-ForeColor="#333333"
                                            HeaderText="" ItemStyle-Font-Size="12px">
                                            <HeaderTemplate>
                                                <table>
                                                    <tr>
                                                        <td>Item Desceription</td>
                                                        <td>
                                                            <asp:HyperLink runat="server" ID="lnkExcel" CssClass="btn btn-sm btn-success"><i class="fa fa-file-excel"></i></asp:HyperLink>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvitem" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "resdesc")) %>'
                                                    Width="180px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle Font-Bold="True" Font-Size="12px" ForeColor="#333333" />
                                            <ItemStyle Font-Size="12px" HorizontalAlign="Left" />
                                            <FooterStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        
                                        <asp:TemplateField HeaderStyle-Font-Bold="false" HeaderStyle-Font-Size="12px" HeaderStyle-ForeColor="#333333"
                                            HeaderText="Unit" ItemStyle-Font-Size="12px">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvunit" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "resunit")) %>'
                                                    Width="50px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle Font-Bold="True" Font-Size="12px" ForeColor="#333333" />
                                            <ItemStyle Font-Size="12px" />
                                        </asp:TemplateField>
                                        
                                        <asp:TemplateField HeaderStyle-Font-Bold="false" HeaderStyle-Font-Size="12px" HeaderStyle-ForeColor="#333333"
                                            HeaderText="Spcfcod" ItemStyle-Font-Size="12px" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvSpcfCod" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "spcfcod")) %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle Font-Bold="True" Font-Size="12px" ForeColor="#333333" />
                                            <ItemStyle Font-Size="12px" />
                                        </asp:TemplateField>
                                        
                                        <asp:TemplateField HeaderStyle-Font-Bold="false" HeaderStyle-Font-Size="12px" HeaderStyle-ForeColor="#333333"
                                            HeaderText="Specificaftion" ItemStyle-Font-Size="12px">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvSpcf" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "spcfdesc")) %>'
                                                    Width="250px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle Font-Bold="True" Font-Size="12px" ForeColor="#333333" />
                                            <ItemStyle Font-Size="12px" />
                                        </asp:TemplateField>
                                        
                                        <asp:TemplateField HeaderStyle-Font-Bold="false" HeaderStyle-Font-Size="12px" HeaderStyle-ForeColor="#333333"
                                            HeaderText="Minmum Stock" ItemStyle-Font-Size="12px">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtgvqty" runat="server" BorderStyle="None"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "mstdqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="80px" Style="text-align: right"></asp:TextBox>
                                            </ItemTemplate>
                                            <ItemStyle Font-Size="12px" HorizontalAlign="Right" />
                                            <HeaderStyle Font-Bold="True" Font-Size="12px" ForeColor="#333333" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-Font-Bold="false" HeaderStyle-Font-Size="12px" HeaderStyle-ForeColor="#333333"
                                            HeaderText="Re-order </Br> Level" ItemStyle-Font-Size="12px">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtgvrestkqty" runat="server" BorderStyle="None"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "restkqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="80px" Style="text-align: right"></asp:TextBox>
                                            </ItemTemplate>
                                            <ItemStyle Font-Size="12px" HorizontalAlign="Right" />
                                            <HeaderStyle Font-Bold="True" Font-Size="12px" ForeColor="#333333" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-Font-Bold="false" HeaderStyle-Font-Size="12px" HeaderStyle-ForeColor="#333333"
                                            HeaderText="Maximum Stock" ItemStyle-Font-Size="12px">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtgvmaxqty" runat="server" BorderStyle="None"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "maxqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="80px" Style="text-align: right"></asp:TextBox>
                                            </ItemTemplate>
                                            <ItemStyle Font-Size="12px" HorizontalAlign="Right" />
                                            <HeaderStyle Font-Bold="True" Font-Size="12px" ForeColor="#333333" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-Font-Bold="false" HeaderStyle-Font-Size="12px" HeaderStyle-ForeColor="#333333"
                                            HeaderText="EOQ" ItemStyle-Font-Size="12px">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtgveoq" runat="server" BorderStyle="None"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "eoq")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="80px" Style="text-align: right"></asp:TextBox>
                                            </ItemTemplate>
                                            <ItemStyle Font-Size="12px" HorizontalAlign="Right" />
                                            <HeaderStyle Font-Bold="True" Font-Size="12px" ForeColor="#333333" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-Font-Bold="false" HeaderStyle-Font-Size="12px" HeaderStyle-ForeColor="#333333"
                                            HeaderText="Delivery </Br> Period (Day)" ItemStyle-Font-Size="12px">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtgvdelper" runat="server" BorderStyle="None"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "delper")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="80px" Style="text-align: right"></asp:TextBox>
                                            </ItemTemplate>
                                            <ItemStyle Font-Size="12px" HorizontalAlign="Right" />
                                            <HeaderStyle Font-Bold="True" Font-Size="12px" ForeColor="#333333" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderStyle-Font-Bold="false" HeaderStyle-Font-Size="12px" HeaderStyle-ForeColor="#333333"
                                            HeaderText="Active" ItemStyle-Font-Size="12px">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkvmrno" runat="server"
                                                    Checked='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "active"))=="True" %>'
                                                    Width="20px" />
                                            </ItemTemplate>
                                            <ItemStyle Font-Size="12px" HorizontalAlign="Right" />
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
                    </asp:MultiView>

                </div>
            </div>

        </ContentTemplate>
    </asp:UpdatePanel>


</asp:Content>
