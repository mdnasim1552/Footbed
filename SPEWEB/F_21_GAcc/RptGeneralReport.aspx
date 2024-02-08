<%@ Page Title="" Language="C#" MasterPageFile="~/SPE.Master" AutoEventWireup="true" CodeBehind="RptGeneralReport.aspx.cs" Inherits="SPEWEB.F_21_GAcc.RptGeneralReport" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript" language="javascript">


        function Search_Gridview(strKey, cellNr) {
            try {

                var strData = strKey.value.toLowerCase().split(" ");
                tblData = document.getElementById("<%=this.gvgenreq.ClientID %>");


                var rowData;
                for (var i = 0; i < tblData.rows.length; i++) {
                    rowData = tblData.rows[i].cells[cellNr].innerHTML;
                    var styleDisplay = 'none';
                    for (var j = 0; j < strData.length; j++) {
                        if (rowData.toLowerCase().indexOf(strData[j]) >= 0)
                            styleDisplay = '';
                        else {
                            styleDisplay = 'none';
                            break;
                        }
                    }
                    tblData.rows[i].style.display = styleDisplay;
                }
            }

            catch (e) {
                alert(e.message);
            }

        }

        $(document).ready(function () {

            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);

        });

        function pageLoaded() {


            $('.chzn-select').chosen({ search_contains: true });;


            var gv = $('#<%=this.gvgenreq.ClientID %>');
            gv.Scrollable();

        }

        function SetTarget(type) {
            window.open('../RDLCViewerWin.aspx?PrintOpt=' + type, '_blank');
        }

    </script>

    <style>
        .rbtnList1 table tbody tr td input[type=radio] {
            display: initial !important;
            margin-top: 2px !important;
            position: relative !important;
            margin-left: 10px !important;
        }

        .rbtnList1 table tbody tr td label {
           margin-left: 5px;
        }
    </style>



    <asp:UpdatePanel ID="UpdatePanel1" runat="server">

        <ContentTemplate>
            <div class="RealProgressbar">
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

                        <div class="col-1 col-md-1 col-lg-1">
                            <div class="form-group">
                                <asp:Label ID="lbldateTo" runat="server" Text="From"></asp:Label>
                                <asp:TextBox ID="txtDatfrom" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
                                <cc1:CalendarExtender ID="txtDatfrom_CalendarExtender0" runat="server"
                                    Format="dd-MMM-yyyy" TargetControlID="txtDatfrom"></cc1:CalendarExtender>
                            </div>
                        </div>

                        <div class="col-1 col-md-1 col-lg-1">
                            <div class="form-group">
                                <asp:Label ID="lbldatefrom" runat="server" Text="To" Visible="true"></asp:Label>
                                <asp:TextBox ID="txttodate" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
                                <cc1:CalendarExtender ID="txttodate_CalendarExtender" runat="server"
                                    Format="dd-MMM-yyyy" TargetControlID="txttodate"></cc1:CalendarExtender>
                            </div>
                        </div>

                        <div class="col-1">
                            <asp:LinkButton ID="lbtnOk" runat="server" OnClick="lbtnOk_Click" Style="margin-top: 21px;" CssClass="btn btn-sm btn-primary">Ok</asp:LinkButton>
                        </div>


                        <div class="col-3 rbtnList1" style="margin-top: 21px">
                            <asp:RadioButtonList ID="rbtappoval" RepeatDirection="Horizontal" CssClass="form-control form-control-sm" runat="server">
                                <asp:ListItem Selected="True">First Approval</asp:ListItem>
                                <asp:ListItem>Final Approval</asp:ListItem>
                                <asp:ListItem>Both</asp:ListItem>
                            </asp:RadioButtonList>
                        </div>

                        <div>
                            <asp:Label ID="lblPage" runat="server" CssClass=" smLbl_to " Visible="false">Page Size</asp:Label>
                            <asp:DropDownList ID="ddlpagesize" runat="server" AutoPostBack="True" CssClass="ddlPage" Width="76" OnSelectedIndexChanged="ddlpagesize_SelectedIndexChanged" Visible="false">
                                <asp:ListItem>10</asp:ListItem>
                                <asp:ListItem>15</asp:ListItem>
                                <asp:ListItem>20</asp:ListItem>
                                <asp:ListItem>30</asp:ListItem>
                                <asp:ListItem>50</asp:ListItem>
                                <asp:ListItem>100</asp:ListItem>
                                <asp:ListItem>150</asp:ListItem>
                                <asp:ListItem>200</asp:ListItem>
                                <asp:ListItem>300</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                </div>
            </div>

            <div class="card card-fluid">
                <div class="card-body" style="min-height: 500px;">
                    <div class="row">
                        <asp:GridView ID="gvgenreq" runat="server" AutoGenerateColumns="False" CssClass=" table-striped table-hover table-bordered grvContentarea"
                            ShowFooter="True" AllowPaging="false" OnPageIndexChanging="gvgenreq_PageIndexChanging">
                            <PagerSettings Position="Top" />
                            <RowStyle />
                            <Columns>
                                <asp:TemplateField HeaderText="Sl.No.">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvSlNo0" runat="server" Font-Bold="True" Height="16px"
                                            Style="text-align: right"
                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>


                                <asp:TemplateField HeaderText="Project Name">
                                    <HeaderTemplate>
                                        <asp:TextBox ID="txtSearchpactdesc" SortExpression="mrfno" BackColor="Transparent" BorderStyle="None" runat="server" Width="170" onkeyup="Search_Gridview(this,1)" placeholder="Project Name"></asp:TextBox>
                                    </HeaderTemplate>


                                    <ItemTemplate>
                                        <asp:Label ID="lblprojacts" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc")) %>' Width="160px"></asp:Label>
                                    </ItemTemplate>


                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Requisition No">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvcode" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "reqno1")) %>' Width="90px"></asp:Label>
                                    </ItemTemplate>

                                </asp:TemplateField>




                                <asp:TemplateField HeaderText="Req Date">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvreqdat" runat="server" Style="text-align: right"
                                            Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "reqdat")).ToString("dd-MMM-yyyy") %>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFreqdat" runat="server" Font-Bold="True" Font-Size="12px"
                                            ForeColor="Black" Style="text-align: right"></asp:Label>
                                    </FooterTemplate>
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    <ItemStyle HorizontalAlign="right" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Mrf No">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvmrfno" runat="server" Style="text-align: right"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mrfno")) %>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFmrfno" runat="server" Font-Bold="True"
                                            Font-Size="12px" ForeColor="Black" Style="text-align: right">Total : </asp:Label>
                                    </FooterTemplate>
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    <ItemStyle HorizontalAlign="right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Requistion </br>Amount">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvreqamt" runat="server" Style="text-align: right"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "proamt")).ToString("#,##0;(#,##0); ") %>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFreqamt" runat="server" Font-Bold="True" Font-Size="12px"
                                            ForeColor="Black" Style="text-align: right"></asp:Label>
                                    </FooterTemplate>
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    <ItemStyle HorizontalAlign="right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Approved </br>Amount">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvreqamt" runat="server" Style="text-align: right"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "appamt")).ToString("#,##0;(#,##0); ") %>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFlgvreqamt" runat="server" Font-Bold="True"
                                            Font-Size="12px" ForeColor="Black" Style="text-align: right"></asp:Label>
                                    </FooterTemplate>
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    <ItemStyle HorizontalAlign="right" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Payment">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvpayamt" runat="server" Style="text-align: right"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "payment")).ToString("#,##0;(#,##0); ") %>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFlgvpayamt" runat="server" Font-Bold="True"
                                            Font-Size="12px" ForeColor="Black" Style="text-align: right"></asp:Label>
                                    </FooterTemplate>
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    <ItemStyle HorizontalAlign="right" />
                                </asp:TemplateField>


                                <asp:TemplateField HeaderText="Requisition Create">
                                    <ItemTemplate>
                                        <asp:Label ID="lblreqcreate" runat="server" Style="text-align: left"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "reqname")) %>'
                                            Width="130px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFreqcreate" runat="server" Font-Bold="True"
                                            Font-Size="12px" ForeColor="Black" Style="text-align: right"></asp:Label>
                                    </FooterTemplate>
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    <ItemStyle HorizontalAlign="left" />
                                </asp:TemplateField>



                                <asp:TemplateField HeaderText="First Approval">
                                    <ItemTemplate>
                                        <asp:Label ID="lblFirst" runat="server" Style="text-align: left"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "aprname")) %>'
                                            Width="130px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFfrista" runat="server" Font-Bold="True"
                                            Font-Size="12px" ForeColor="Black" Style="text-align: right"></asp:Label>
                                    </FooterTemplate>
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    <ItemStyle HorizontalAlign="left" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Final Approval">
                                    <ItemTemplate>
                                        <asp:Label ID="lblFinal" runat="server" Style="text-align: left"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "aprfname")) %>'
                                            Width="130px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFlblFinal" runat="server" Font-Bold="True"
                                            Font-Size="12px" ForeColor="Black" Style="text-align: left"></asp:Label>
                                    </FooterTemplate>
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    <ItemStyle HorizontalAlign="left" />
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

