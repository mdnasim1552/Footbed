<%@ Page Title="" Language="C#" MasterPageFile="~/SPEMaster.Master" AutoEventWireup="true" CodeBehind="EntryDailyProduction.aspx.cs" Inherits="SPEWEB.F_15_Pro.EntryDailyProduction" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <link href="../Content/chosen.css" rel="stylesheet" />
    <script src="../Scripts/chosen.jquery.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            //For navigating using left and right arrow of the keyboard
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);

        });
        function pageLoaded() {

            $('.chzn-select').chosen({ search_contains: true });
        };

        var allownumeric = new Array();
        allownumeric.push(8, 110, 46, 45, 44, 37, 38, 39, 40, 27, 9);

        function IsNumeric(e) {
            var keyCode = e.which ? e.which : e.keyCode
            var returnValue = ((keyCode >= 48 && keyCode <= 57) || allownumeric.indexOf(keyCode) != -1);

            var style = returnValue ? "none" : "inline";
            $('.ErrorField').css("display", style);
            return returnValue;
        }

    </script>

    <div class="container moduleItemWrpper">
        <div class="contentPart">


            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>

                    <div class="row">
                        <fieldset class="scheduler-border fieldset_A">
                            <asp:Panel ID="PnlSubCon" runat="server">
                                <div class="form-horizontal">
                                    <div class="form-group">
                                        <div class="col-md-4">
                                            <asp:Label ID="Label1" runat="server" CssClass=" lblName lblTxt" Text="Order"></asp:Label>
                                            <asp:DropDownList ID="ddlOrderno" runat="server" Width="290" CssClass="ddlPage chzn-select" AutoPostBack="true" OnSelectedIndexChanged="ddlOrderno_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-md-4">
                                            <asp:Label ID="lblcontrolAccHead" runat="server" CssClass=" lblName lblTxt" Text="Product Card"></asp:Label>
                                            <asp:DropDownList ID="ddlProduct" runat="server" Width="290" CssClass="ddlPage chzn-select" AutoPostBack="true" OnSelectedIndexChanged="ddlProduct_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-md-4 ">
                                            <asp:Label ID="lblDebAc" runat="server" CssClass=" lblName lblTxt" Text="Process/Floor/Line"></asp:Label>
                                            <asp:DropDownList ID="ddlLine" runat="server" Width="290" CssClass="ddlPage chzn-select">
                                            </asp:DropDownList>
                                        </div>

                                    </div>
                                    <div class="form-group">


                                        <div class="col-md-4">

                                            <asp:Label ID="lblDate" runat="server" CssClass=" lblName lblTxt" Text="Date"></asp:Label>
                                            <asp:TextBox ID="txtCurDate" runat="server" Style="margin: 0;" CssClass=" inputtextbox"></asp:TextBox>
                                            <cc1:CalendarExtender ID="txtCurDate_CalendarExtender" runat="server"
                                                Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txtCurDate"></cc1:CalendarExtender>
                                            <asp:LinkButton ID="lbtnOk" runat="server" CssClass="btn btn-primary okBtn" OnClick="lbtnOk_Click">Ok</asp:LinkButton>
                                            <span id="ErrorField" class="ErrorField pull-right" style="color: Red; display: none; font-family: Cambria;">* Numeric Field Allowed</span>
                                        </div>
                                        <div class="col-md-4 ">
                                            <asp:Label ID="lblQty" runat="server" CssClass="lblTxt lblName" ForeColor="Red" Width="121px"></asp:Label>
                                            <asp:Label ID="lblUnit" runat="server" CssClass="lblTxt lblName" ForeColor="Red"></asp:Label>


                                            <asp:Label ID="lblPage" runat="server" CssClass="lblTxt lblName" Text="Page"></asp:Label>
                                            <asp:DropDownList ID="ddlpagesize" runat="server" AutoPostBack="True" Width="68px" OnSelectedIndexChanged="ddlpagesize_SelectedIndexChanged"
                                                TabIndex="2" CssClass=" ddlPage">
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
                                        <div class="col-md-4">

                                            <asp:Label ID="lblexeqty" runat="server" CssClass="lblTxt lblName" ForeColor="Red" Width="112px"></asp:Label>
                                            <asp:Label ID="lblBalQty" runat="server" CssClass="lblTxt lblName" ForeColor="Red"></asp:Label>

                                            

                                            <div class="msgHandSt">
                                                <asp:Label ID="lblmsg01" runat="server" CssClass="btn-danger btn disabled primaryBtn" Visible="false"></asp:Label>
                                            </div>

                                        </div>
                                    </div>






                                </div>
                            </asp:Panel>
                        </fieldset>
                    </div>


                    <asp:GridView ID="gvprotar" runat="server" AllowPaging="True" CssClass="table table-striped table-hover table-bordered grvContentarea"
                        AutoGenerateColumns="False" OnPageIndexChanging="gvprotar_PageIndexChanging"
                        ShowFooter="True" Style="text-align: left" OnRowCreated="gvprotar_RowCreated">
                        <RowStyle />
                        <Columns>
                            <asp:TemplateField HeaderText="Sl">
                                <ItemTemplate>
                                    <asp:Label ID="lblgvSlNo" runat="server" Font-Bold="True" Height="16px"
                                        Style="text-align: right"
                                        Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Operation & Operator Name">
                                <ItemTemplate>
                                    <asp:Label ID="lblgvline" runat="server" AutoCompleteType="Disabled"
                                        BackColor="Transparent" BorderStyle="None" Font-Size="11px"
                                        Text='<%# "<B>"+ Convert.ToString(DataBinder.Eval(Container.DataItem, "prosdesc")) + "</B>"+
                                                                         (DataBinder.Eval(Container.DataItem, "empname").ToString().Trim().Length>0 ? 
                                                                         (Convert.ToString(DataBinder.Eval(Container.DataItem, "prosdesc")).Trim().Length>0 ?  "<br>" : "")+                                                             
                                                                         "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"+ 
                                                                         Convert.ToString(DataBinder.Eval(Container.DataItem, "empname")).Trim(): "") 
                                                                         
                                                                    %>'
                                        Width="180px">
                                                     
                                                     
                                    </asp:Label>




                                </ItemTemplate>
                                <FooterTemplate>


                                    <div class="col-md-4">
                                        <asp:LinkButton ID="lbtnTotal" runat="server" CssClass="btn btn-primary primarygrdBtn" OnClick="lbtnTotal_Click">Total</asp:LinkButton>
                                    </div>
                                    <div class="col-md-4">
                                        <asp:LinkButton ID="lbtnUpdate" runat="server" CssClass="btn btn-danger primaryBtn" OnClick="lbtnUpdate_Click">Final Update</asp:LinkButton>
                                    </div>

                                </FooterTemplate>
                                <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>



                            <asp:TemplateField HeaderText="Daily</br>Target">

                                <%-- <FooterTemplate>
                                    <asp:Label ID="lblgvFTarp" runat="server" Font-Bold="True" Font-Size="12px"
                                        ForeColor="Black"></asp:Label>
                                </FooterTemplate>--%>

                                <ItemTemplate>
                                    <asp:TextBox ID="txtgvTqty" runat="server" BackColor="Transparent" Style="text-align: right; color: red;" onkeypress="return IsNumeric(event);" ondrop="return false;" onpaste="return false;"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "tqty")).ToString("#,##0;(#,##0); ") %>'
                                        Width="50px" BorderStyle="None" Font-Size="11px"></asp:TextBox>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Right" />
                                <FooterStyle HorizontalAlign="Right" Font-Bold="True" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Total">

                                <%-- <FooterTemplate>
                                    <asp:Label ID="lblgvFTarp" runat="server" Font-Bold="True" Font-Size="12px"
                                        ForeColor="Black"></asp:Label>
                                </FooterTemplate>--%>

                                <ItemTemplate>
                                    <asp:Label ID="txtgvTotalp" runat="server" BackColor="Transparent" Style="text-align: right; color: green;"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "totalp")).ToString("#,##0;(#,##0); ") %>'
                                        Width="40px" BorderStyle="None" Font-Size="11px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Right" />
                                <FooterStyle HorizontalAlign="Right" Font-Bold="True" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="%">
                                <ItemTemplate>
                                    <asp:Label ID="txtgvPercent" runat="server" BackColor="Transparent" Style="text-align: right; color: blue;"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "expercent")).ToString("#,##0.00;(#,##0.00); ") %>'
                                        Width="40px" BorderStyle="None" Font-Size="11px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Right" />
                                <FooterStyle HorizontalAlign="Right" Font-Bold="True" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="01">
                                <FooterTemplate>
                                    <asp:Label ID="lgvFp1" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                        Style="text-align: right" Width="50px"></asp:Label>
                                </FooterTemplate>
                                <ItemTemplate>
                                    <asp:TextBox ID="txtgvp1" runat="server" BackColor="Transparent" BorderStyle="None" onkeypress="return IsNumeric(event);" ondrop="return false;" onpaste="return false;"
                                        Font-Size="11px" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p1")).ToString("#,##0;(#,##0); ") %>'
                                        Width="50px"></asp:TextBox>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Right" />
                                <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                <HeaderStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="02">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtgvp2" runat="server" BackColor="Transparent" BorderStyle="None" onkeypress="return IsNumeric(event);" ondrop="return false;" onpaste="return false;"
                                        Font-Size="11px" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p2")).ToString("#,##0;(#,##0); ") %>'
                                        Width="50px"></asp:TextBox>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:Label ID="lgvFp2" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                        Style="text-align: right" Width="50px"></asp:Label>
                                </FooterTemplate>
                                <ItemStyle HorizontalAlign="Right" />
                                <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                <HeaderStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="03">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtgvp3" runat="server" BackColor="Transparent" BorderStyle="None" onkeypress="return IsNumeric(event);" ondrop="return false;" onpaste="return false;"
                                        Font-Size="11px" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p3")).ToString("#,##0;(#,##0); ") %>'
                                        Width="50px"></asp:TextBox>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:Label ID="lgvFp3" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                        Style="text-align: right" Width="50px"></asp:Label>
                                </FooterTemplate>
                                <ItemStyle HorizontalAlign="Right" />
                                <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                <HeaderStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="04">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtgvp4" runat="server" BackColor="Transparent" BorderStyle="None" onkeypress="return IsNumeric(event);" ondrop="return false;" onpaste="return false;"
                                        Font-Size="11px" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p4")).ToString("#,##0;(#,##0); ") %>'
                                        Width="50px"></asp:TextBox>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:Label ID="lgvFp4" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                        Style="text-align: right" Width="50px"></asp:Label>
                                </FooterTemplate>
                                <ItemStyle HorizontalAlign="Right" />
                                <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                <HeaderStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="05">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtgvp5" runat="server" BackColor="Transparent" BorderStyle="None" onkeypress="return IsNumeric(event);" ondrop="return false;" onpaste="return false;"
                                        Font-Size="11px" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p5")).ToString("#,##0;(#,##0); ") %>'
                                        Width="50px"></asp:TextBox>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:Label ID="lgvFp5" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                        Style="text-align: right" Width="50px"></asp:Label>
                                </FooterTemplate>
                                <ItemStyle HorizontalAlign="Right" />
                                <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                <HeaderStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="06">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtgvp6" runat="server" BackColor="Transparent" BorderStyle="None" onkeypress="return IsNumeric(event);" ondrop="return false;" onpaste="return false;"
                                        Font-Size="11px" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p6")).ToString("#,##0;(#,##0); ") %>'
                                        Width="50px"></asp:TextBox>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:Label ID="lgvFp6" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                        Style="text-align: right" Width="50px"></asp:Label>
                                </FooterTemplate>
                                <ItemStyle HorizontalAlign="Right" />
                                <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                <HeaderStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="07">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtgvp7" runat="server" BackColor="Transparent" BorderStyle="None" onkeypress="return IsNumeric(event);" ondrop="return false;" onpaste="return false;"
                                        Font-Size="11px" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p7")).ToString("#,##0;(#,##0); ") %>'
                                        Width="50px"></asp:TextBox>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:Label ID="lgvFp7" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                        Style="text-align: right" Width="50px"></asp:Label>
                                </FooterTemplate>
                                <ItemStyle HorizontalAlign="Right" />
                                <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                <HeaderStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="08">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtgvp8" runat="server" BackColor="Transparent" BorderStyle="None" onkeypress="return IsNumeric(event);" ondrop="return false;" onpaste="return false;"
                                        Font-Size="11px" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p8")).ToString("#,##0;(#,##0); ") %>'
                                        Width="50px"></asp:TextBox>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:Label ID="lgvFp8" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                        Style="text-align: right" Width="50px"></asp:Label>
                                </FooterTemplate>
                                <ItemStyle HorizontalAlign="Right" />
                                <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                <HeaderStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="09">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtgvp9" runat="server" BackColor="Transparent" BorderStyle="None" onkeypress="return IsNumeric(event);" ondrop="return false;" onpaste="return false;"
                                        Font-Size="11px" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p9")).ToString("#,##0;(#,##0); ") %>'
                                        Width="50px"></asp:TextBox>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:Label ID="lgvFp9" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                        Style="text-align: right" Width="50px"></asp:Label>
                                </FooterTemplate>
                                <ItemStyle HorizontalAlign="Right" />
                                <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                <HeaderStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="10">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtgvp10" runat="server" BackColor="Transparent" BorderStyle="None" onkeypress="return IsNumeric(event);" ondrop="return false;" onpaste="return false;"
                                        Font-Size="11px" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p10")).ToString("#,##0;(#,##0); ") %>'
                                        Width="45px"></asp:TextBox>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:Label ID="lgvFp10" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                        Style="text-align: right" Width="45px"></asp:Label>
                                </FooterTemplate>
                                <ItemStyle HorizontalAlign="Right" />
                                <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                <HeaderStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="11">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtgvp11" runat="server" BackColor="Transparent" BorderStyle="None" onkeypress="return IsNumeric(event);" ondrop="return false;" onpaste="return false;"
                                        Font-Size="11px" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p11")).ToString("#,##0;(#,##0); ") %>'
                                        Width="45px"></asp:TextBox>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:Label ID="lgvFp11" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                        Style="text-align: right" Width="45px"></asp:Label>
                                </FooterTemplate>
                                <ItemStyle HorizontalAlign="Right" />
                                <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                <HeaderStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="12">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtgvp12" runat="server" BackColor="Transparent" BorderStyle="None" onkeypress="return IsNumeric(event);" ondrop="return false;" onpaste="return false;"
                                        Font-Size="11px" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "p12")).ToString("#,##0;(#,##0); ") %>'
                                        Width="45px"></asp:TextBox>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:Label ID="lgvFp12" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="Black"
                                        Style="text-align: right" Width="45px"></asp:Label>
                                </FooterTemplate>
                                <ItemStyle HorizontalAlign="Right" />
                                <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                <HeaderStyle HorizontalAlign="Center" />
                            </asp:TemplateField>


                            <asp:TemplateField HeaderText="Linecode" Visible="False">
                                <ItemTemplate>
                                    <asp:Label ID="lgvlinecode" runat="server"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "proscode")) %>'
                                        Width="80px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Left" />
                            </asp:TemplateField>



                            <asp:TemplateField HeaderText="ordrcode" Visible="False">
                                <ItemTemplate>
                                    <asp:Label ID="lgvprocode" runat="server"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "empid")) %>'
                                        Width="80px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Left" />
                            </asp:TemplateField>





                        </Columns>
                        <FooterStyle BackColor="#F5F5F5" />
                        <EditRowStyle />
                        <AlternatingRowStyle />
                        <PagerStyle CssClass="gvPagination" />
                        <HeaderStyle CssClass="grvHeader" />
                    </asp:GridView>



                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>

</asp:Content>






