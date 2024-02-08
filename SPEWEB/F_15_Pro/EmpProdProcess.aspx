<%@ Page Title="" Language="C#" MasterPageFile="~/SPEMaster.Master" AutoEventWireup="true" CodeBehind="EmpProdProcess.aspx.cs" Inherits="SPEWEB.F_15_Pro.EmpProdProcess" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript" language="javascript">
        $(document).ready(function () {

          
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);
        });
        function pageLoaded() {

            $("input, select").bind("keydown", function (event) {
                var k1 = new KeyPress();
                k1.TxtTransSearch(event);
            });
        }


    </script>






    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>

            <div class="container moduleItemWrpper">
                <div class="contentPart">
                    <div class="row">





                        <asp:GridView ID="gvEmpSetup" runat="server" AutoGenerateColumns="False" CssClass="table-striped table-hover table-bordered grvContentarea"
                            ShowFooter="True" Style="text-align: left" Width="685px" OnRowDataBound="gvEmpSetup_RowDataBound">
                            <RowStyle />
                            <Columns>
                                <asp:TemplateField HeaderText="Sl.No.">
                                    <ItemTemplate>
                                        <asp:Label ID="serialnoid" runat="server"
                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Font-Bold="True" Font-Size="12px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Process Code">

                                    <ItemTemplate>
                                        <asp:Label ID="lblsircode" runat="server" BackColor="Transparent"
                                            BorderColor="Transparent" BorderStyle="None"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sircode")) %>'
                                            Width="60px" Font-Size="12px" ForeColor="Black" TabIndex="76"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Operation Name">
                                    <%--<FooterTemplate>
                                        <asp:Label ID="lblTotal" runat="server" Font-Bold="True" Font-Size="12px"
                                            ForeColor="White" Text="Total"></asp:Label>
                                    </FooterTemplate>--%>
                                    <ItemTemplate>
                                        <asp:Label ID="lblsirdesc" runat="server"
                                            Font-Size="11px"
                                            Text='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "sirdesc"))  %>'
                                            Width="250px" TabIndex="75"></asp:Label>

                                    </ItemTemplate>
                                    <FooterStyle HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Setup">
                                    <ItemTemplate>

                                        <asp:HyperLink ID="lblgvSetup" runat="server" Target="_blank"
                                            Text='Setup'
                                            Width="80px"></asp:HyperLink>
                                    </ItemTemplate>
                                    <FooterStyle HorizontalAlign="Left" />
                                    <HeaderStyle HorizontalAlign="Left" />
                                </asp:TemplateField>


                            </Columns>
                            <FooterStyle CssClass="grvFooter" />
                            <EditRowStyle />
                            <AlternatingRowStyle />
                            <PagerStyle CssClass="gvPagination" />
                            <HeaderStyle CssClass="grvHeader" />
                        </asp:GridView>


                        <a class="btn btn-primary primaryBtn" href='<%=this.ResolveUrl("~/F_15_Pro/EntryDailyProduction.aspx?Type=Entry")%>' target="_blank" style="margin:0 0px 0 5px">Daily Target Setup</a>

                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>


</asp:Content>


