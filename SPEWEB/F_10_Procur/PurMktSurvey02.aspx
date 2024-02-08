<%@ Page Title="" Language="C#" MasterPageFile="~/SPE.Master" AutoEventWireup="true" CodeBehind="PurMktSurvey02.aspx.cs" Inherits="SPEWEB.F_10_Procur.PurMktSurvey02" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style>
        .uploadedimg .image {
            opacity: 1;
            display: block;
            width: 100%;
            height: auto;
            transition: .5s ease;
            backface-visibility: hidden;
        }

        .uploadedimg:hover .image {
            opacity: 0.3;
        }

        .uploadedimg:hover .middle {
            opacity: 1;
        }

        .text {
            background-color: #4CAF50;
            color: white;
            font-size: 16px;
            padding: 16px 32px;
        }
    </style>



    <script type="text/javascript">
    function PrintRDLC(){
 window.open('../RDLCViewerWin.aspx?PrintOpt=PDF' , '_blank');
}
        function FnDanger() {
            alert('Sorry No Data Found of this Materials', '<span class="glyphicon glyphicon-info-sign"></span> Information', 'danger');

        }
        function openModal() {
            $('#myModal').modal('toggle');
        }

       
        function openServyModal() {
            // alert("test");

            $('#ServyModal').modal('toggle');
            $('.chzn-select').chosen({ search_contains: true });
        }
        function closeServyModal() {
            $('#ServyModal').modal('hide');
        }
        function Confirm() {
            window.onload = function () {
                var confirm_value = document.createElement("INPUT");
                confirm_value.type = "hidden";
                confirm_value.name = "confirm_value";
                if (confirm("Do you want to replace existing file?")) {
                    confirm_value.value = "Yes";
                } else {
                    confirm_value.value = "No";
                }
                document.forms[0].appendChild(confirm_value);
                document.getElementById("<%=btnConfirm.ClientID %>").click();
            }
        }
        $(document).ready(function () {

            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);
        });
        function pageLoaded() {

            $('.chzn-select').chosen({ search_contains: true });
        }
       
    </script>


    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="card card-fluid">
                <div class="card-body">
                    <div class="row">

                        <div class="col-md-1 col-lg-1  col-sm-1">
                            <div class="form-group">
                                <asp:Label ID="Label11" runat="server" CssClass=" label" Text="CS No:"></asp:Label>

                                <asp:Label ID="lblCurMSRNo1" runat="server" CssClass="label" Text="MSR00-"></asp:Label>

                                <asp:TextBox ID="txtCurMSRNo2" runat="server" CssClass="form-control form-control-sm">00000</asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-1 col-lg-1  col-sm-1">
                            <div class="form-group">
                                <asp:Label ID="Label13" runat="server" CssClass="label" Text="Date:"></asp:Label>

                                <asp:TextBox ID="txtCurMSRDate" runat="server" CssClass="form-control form-control-sm" ToolTip="(dd.mm.yyyy)"></asp:TextBox>
                                <cc1:CalendarExtender ID="txtCurMSRDate_CalendarExtender" runat="server"
                                    Enabled="True" Format="dd.MM.yyyy" TargetControlID="txtCurMSRDate"></cc1:CalendarExtender>


                            </div>
                        </div>
                        <div class="col-md-2 col-lg-2 col-sm-2 ">
                            <div class="form-group">
                                <asp:Label ID="Label1" runat="server" CssClass=" smLbl_to" Text="Req List:"></asp:Label>

                                <asp:TextBox ID="txtReqSearch" runat="server" CssClass="inputtextbox" Visible="false"></asp:TextBox>


                                <asp:LinkButton ID="lnkReqList" runat="server" CssClass="btn btn-primary primaryBtn" Visible="false" OnClick="lnkReqList_Click"><span class="glyphicon glyphicon-search asitGlyp"></span></asp:LinkButton>

                                <asp:DropDownList ID="ddlReqList" ValidationGroup="g1" runat="server" AutoPostBack="True" CssClass="form-control form-control-sm"></asp:DropDownList>


                                <asp:RequiredFieldValidator InitialValue="-1" ID="Req_ID" Display="Dynamic"
                                    ValidationGroup="g1" runat="server" ControlToValidate="ddlReqList"
                                    Text="*" ErrorMessage="ErrorMessage"></asp:RequiredFieldValidator>

                            </div>
                        </div>
                        <div class="col-md-1 col-lg-1 col-sm-1 ">
                            <div class="form-group" style="margin-top: 20px;">

                                <asp:Button ID="lbtnMSROk" runat="server" Text="Ok" CssClass="btn btn-primary btn-sm" OnClick="lbtnMSROk_Click" />
                            </div>
                        </div>
                        <div class="col-md-1 col-lg-1 col-sm-1 ">
                            <div class="form-group">

                                <asp:Label ID="Label22" runat="server" CssClass="label" Text="Con. Rate:"></asp:Label>
                                <asp:TextBox ID="lblConRate" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>

                            </div>
                        </div>
                        <div class="col-md-4 col-lg-4 col-sm-4 ">
                            <div class="form-group" style="margin-top: 20px">
                                <asp:RadioButtonList ID="RadioButtonList1" runat="server" AutoPostBack="true" OnSelectedIndexChanged="RadioButtonList1_SelectedIndexChanged" RepeatDirection="Horizontal">

                                    <asp:ListItem style="color:blueviolet" Selected="True" Value="1">Comparative Statement</asp:ListItem>
                                    <asp:ListItem style="color:red; margin-left=10px" Value="0">Best Selection</asp:ListItem>
                                    <asp:ListItem style="color:forestgreen; margin-left=10px" Value="2">Supplier Total </asp:ListItem>
                                </asp:RadioButtonList>
                            </div>
                        </div>
                        <div class="col-md-1 col-lg-1 col-sm-1 ">
                            <div class="form-group">

                                <asp:Label ID="lblPreMrList" runat="server" CssClass=" label" Text="Previous MSR:"></asp:Label>

                                <asp:TextBox ID="txtPreMSRSearch" runat="server" CssClass="inputtextbox"></asp:TextBox>


                                <asp:LinkButton ID="ImgbtnFindPreMR" runat="server" CssClass="btn btn-primary primaryBtn" OnClick="ImgbtnFindPreMR_Click"><span class="glyphicon glyphicon-search asitGlyp"></span></asp:LinkButton>

                                <asp:DropDownList ID="ddlPrevMSRList" runat="server" Width="160px" CssClass="ddlPage"></asp:DropDownList>

                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="card card-fluid">
                <div class="card-body">
                    <div class="row">
                        <asp:MultiView ID="Multiview1" runat="server">
                            <asp:View ID="ViewBestSelection" runat="server">

                                <div class="col-md-12 table-responsive">
                                    <asp:Label ID="lbltitel2" runat="server" CssClass="lblHead" Visible="false"><h5> B. Best Selection</h5> </asp:Label>
                                    <asp:GridView ID="gvBestSelect" runat="server" AllowPaging="False" AutoGenerateColumns="False" CssClass="table-striped table-hover table-bordered grvContentarea"
                                        ShowFooter="True" Width="1009px" OnRowDataBound="gvBestSelect_RowDataBound">
                                        <PagerSettings Visible="False" />
                                        <RowStyle />
                                        <Columns>
                                            <asp:TemplateField HeaderText="Supl Code" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvSuplBSel" runat="server" Height="16px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "ssircode")) %>'
                                                        Width="80px"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Sl">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvSlNo1bs" runat="server" Height="16px" Style="text-align: right"
                                                        Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="15px"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Res Code" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvResCodBSel1" runat="server" Height="16px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rsircode")) %>'
                                                        Width="80px"></asp:Label>
                                                    <asp:Label ID="lblgvSpcCodBSel1" runat="server" Height="16px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "spcfcod")) %>'
                                                        Width="80px"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Description of Goods">
                                                <ItemTemplate>
                                                    <asp:LinkButton OnClick="lblgrmet1BSel_Click" ID="lblgrmet1BSel" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rsirdesc")) %>'
                                                        Width="150px"></asp:LinkButton>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lblFAmount" runat="server">Total</asp:Label>

                                                </FooterTemplate>
                                                <ItemStyle />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Specification">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvrSpecBSel" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                                        BorderWidth="0px" Font-Size="10px" Style="text-align: left; background-color: Transparent"
                                                        Text='<%# DataBinder.Eval(Container.DataItem, "spcfdesc").ToString() %>' Width="80px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterStyle HorizontalAlign="Center" />
                                                <HeaderStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>


                                            <asp:TemplateField HeaderText="Unit">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvrUnitBSel" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                                        BorderWidth="0px" Font-Size="10px" Style="text-align: left; background-color: Transparent"
                                                        Text='<%# DataBinder.Eval(Container.DataItem, "rsirunit").ToString() %>' Width="50px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterStyle HorizontalAlign="Center" />
                                                <HeaderStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Package Size">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblGvPkgSize" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                                        BorderWidth="0px" Font-Size="10px" Style="text-align: left; background-color: Transparent"
                                                        Text='<%# DataBinder.Eval(Container.DataItem, "pkgsize").ToString() %>' Width="50px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterStyle HorizontalAlign="Center" />
                                                <HeaderStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Present <br> Stock Qty">
                                                <FooterTemplate>
                                                    <asp:Label ID="lblFstkqty" runat="server" BackColor="Transparent"
                                                        BorderColor="Transparent" BorderStyle="None" BorderWidth="1px"
                                                        Font-Bold="True" Font-Size="10px"
                                                        Width="70px" ForeColor="#000"></asp:Label>

                                                </FooterTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvstkqty" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                                        BorderWidth="0px" Font-Size="10px" Style="text-align: right; background-color: Transparent"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "stkqty")).ToString("#,##0.0000;(#,##0.0000); ") %>'
                                                        Width="70px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterStyle HorizontalAlign="right" />
                                                <HeaderStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Requirement Quantity">
                                                <FooterTemplate>

                                                    <asp:Label ID="lblFQtybs" runat="server" BackColor="Transparent"
                                                        BorderColor="Transparent" BorderStyle="None" BorderWidth="1px"
                                                        Font-Bold="True" Font-Size="10px"
                                                        Width="70px" ForeColor="#000"></asp:Label>

                                                </FooterTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvpropqtyBSel" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                                        BorderWidth="0px" Font-Size="9px" Style="text-align: right; background-color: Transparent"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "propqty")).ToString("#,##0.0000;(#,##0.0000); ") %>'
                                                        Width="70px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterStyle HorizontalAlign="right" />
                                                <HeaderStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="CS </br> Qty" HeaderStyle-BackColor="Violet" ItemStyle-BackColor="Violet">
                                                <FooterTemplate>

                                                    <asp:Label ID="lblFcsreqqty" runat="server" BackColor="Transparent"
                                                        BorderColor="Transparent" BorderStyle="None" BorderWidth="1px"
                                                        Font-Bold="True" Font-Size="10px"
                                                        Width="70px" ForeColor="#000"></asp:Label>


                                                </FooterTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvcsreqqty" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                                        BorderWidth="0px" Font-Size="10px" Style="text-align: right; background-color: Transparent"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "csreqqty")).ToString("#,##0.0000;(#,##0.0000); ") %>'
                                                        Width="70px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterStyle HorizontalAlign="right" />
                                                <HeaderStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Approved</br> Qty" Visible="false" HeaderStyle-BackColor="Yellow" ItemStyle-BackColor="Yellow">
                                                <FooterTemplate>

                                                    <asp:Label ID="lblFareqty" runat="server" BackColor="Transparent"
                                                        BorderColor="Transparent" BorderStyle="None" BorderWidth="1px"
                                                        Font-Bold="True" Font-Size="10px"
                                                        Width="50px" ForeColor="#000"></asp:Label>


                                                </FooterTemplate>
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtgvareqty" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                                        BorderWidth="0px" Font-Size="10px" Style="text-align: right; background-color: Transparent"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "areqty")).ToString("#,##0.0000;(#,##0.0000); ") %>'
                                                        Width="50px"></asp:TextBox>
                                                </ItemTemplate>
                                                <FooterStyle HorizontalAlign="right" />
                                                <HeaderStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>


                                            <asp:TemplateField HeaderText="Currency">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblCurrency" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                                        BorderWidth="0px" Font-Size="9px" Style="text-align: left; background-color: Transparent"
                                                        Text='<%# DataBinder.Eval(Container.DataItem, "curdesc").ToString() %>' Width="50px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterStyle HorizontalAlign="Center" />
                                                <HeaderStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Con. </br> Rate">

                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvConrate" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                                        BorderWidth="0px" Font-Size="9px" Style="text-align: right; background-color: Transparent"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "conrate")).ToString("#,##0.000000;(#,##0.000000); ") %>'
                                                        Width="50px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterStyle HorizontalAlign="Center" />
                                                <HeaderStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>


                                            <asp:TemplateField HeaderText="Unit </br> Price">
                                                <FooterTemplate>
                                                    <%-- <asp:LinkButton ID="lbtnResUpdate" runat="server" CssClass="btn btn-danger primaryBtn" OnClick="lbtnResUpdate_Click">Final Update</asp:LinkButton>--%>
                                                </FooterTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvRateBSel" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                                        BorderWidth="0px" Font-Size="10px" Style="text-align: right; background-color: Transparent"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "rate")).ToString("#,##0.000000;(#,##0.000000); ") %>'
                                                        Width="70px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterStyle HorizontalAlign="Center" />
                                                <HeaderStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Total </br> Amount">
                                                <FooterTemplate>

                                                    <asp:Label ID="lblFAmountbs" runat="server" BackColor="Transparent"
                                                        BorderColor="Transparent" BorderStyle="None" BorderWidth="1px"
                                                        Font-Bold="True" Font-Size="10px"
                                                        Width="70px" ForeColor="#000"></asp:Label>


                                                </FooterTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="txtgvamounteBSel" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                                        BorderWidth="0px" Font-Size="10px" Style="text-align: right; background-color: Transparent"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amount")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                        Width="70px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterStyle HorizontalAlign="right" />
                                                <HeaderStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Unit Price BDT">

                                                <ItemTemplate>

                                                    <asp:Label ID="gvlblRate" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                                        BorderWidth="0px" Font-Size="10px" Style="text-align: right; background-color: Transparent"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "bdtrate")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                        Width="70px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterStyle HorizontalAlign="right" />
                                                <HeaderStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Total Price BDT">

                                                <ItemTemplate>

                                                    <asp:Label ID="gvbtAmountbest" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                                        BorderWidth="0px" Font-Size="10px" Style="text-align: right; background-color: Transparent"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "bdtamt")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                        Width="70px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>

                                                    <asp:Label ID="lblFAmountbsBDT" runat="server" BackColor="Transparent"
                                                        BorderColor="Transparent" BorderStyle="None" BorderWidth="1px"
                                                        Font-Bold="True" Font-Size="10px"
                                                        Width="70px" ForeColor="#000"></asp:Label>


                                                </FooterTemplate>
                                                <FooterStyle HorizontalAlign="right" />
                                                <HeaderStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Last </br>Purchase</br> Rate">
                                                <FooterTemplate>
                                                    <%-- <asp:LinkButton ID="lbtnResUpdate" runat="server" CssClass="btn btn-danger primaryBtn" OnClick="lbtnResUpdate_Click">Final Update</asp:LinkButton>--%>
                                                </FooterTemplate>
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtgvlBSpurate" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                                        BorderWidth="1px" Font-Size="10px" Style="text-align: right; float: right; background-color: Transparent"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "lstpurate")).ToString("#,##0.000000;(#,##0.000000); ") %>'
                                                        Width="60px"></asp:TextBox>
                                                </ItemTemplate>
                                                <FooterStyle HorizontalAlign="right" />
                                                <HeaderStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Last </br>Purchase</br> Date">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgrlpurdate" runat="server" Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "lpurdate")).ToString("dd-MMM-yyyy") %>'
                                                        Width="80px"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Supplier">
                                                <ItemTemplate>
                                                    <asp:Label Font-Size="9px" ID="lblgrssup1BSel" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "supdesc")) %>'
                                                        Width="220px"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Payment </br>Terms" HeaderStyle-BackColor="SkyBlue" ItemStyle-BackColor="SkyBlue">
                                                <ItemTemplate>
                                                    <asp:DropDownList ID="ddlpayterms" CssClass="chzn-select" runat="server"></asp:DropDownList>
                                                </ItemTemplate>
                                                <%--    <ItemTemplate>

                                        <asp:TextBox ID="txtgvpaytype" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                            BorderWidth="0px" Font-Size="10px" Style="text-align: left; float: right; background-color: Transparent"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "paytype"))%>'
                                            Width="60px"></asp:TextBox>
                                    </ItemTemplate>--%>
                                                <FooterStyle HorizontalAlign="right" />
                                                <HeaderStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Mode Of </br>Payment" HeaderStyle-BackColor="SkyBlue" ItemStyle-BackColor="SkyBlue">
                                                <ItemTemplate>
                                                    <asp:DropDownList ID="ddlpaymode" CssClass="chzn-select" runat="server"></asp:DropDownList>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Delivery </br>Terms" HeaderStyle-BackColor="SkyBlue" ItemStyle-BackColor="SkyBlue">
                                                <ItemTemplate>
                                                    <asp:DropDownList ID="ddldeltrm" CssClass="chzn-select" runat="server"></asp:DropDownList>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Deliver </br>Mode" HeaderStyle-BackColor="SkyBlue" ItemStyle-BackColor="SkyBlue">
                                                <ItemTemplate>
                                                    <asp:DropDownList ID="ddldelmod" CssClass="chzn-select" runat="server"></asp:DropDownList>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Advance </br>Amount" HeaderStyle-BackColor="SkyBlue" ItemStyle-BackColor="SkyBlue">

                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtgvadvamt" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                                        BorderWidth="0px" Font-Size="10px" Style="text-align: right; float: right; background-color: Transparent"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "advamt")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                        Width="60px"></asp:TextBox>
                                                </ItemTemplate>
                                                <FooterStyle HorizontalAlign="right" />
                                                <HeaderStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Credit</br> Period</br> (Day)">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgPeriodbs" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "payment1")) %>'
                                                        Style="text-align: left; background-color: Transparent" Width="80px"></asp:Label>

                                                </ItemTemplate>
                                                <ItemStyle />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Concern </br>Person" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgConcernbs" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "cperson")) %>'
                                                        Width="100px"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Mobile" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvMobilebs" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mobile")) %>'
                                                        Width="100px"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Lead</br> Time" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvleadtime" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "leadtime")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="60px"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle />
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
                            <asp:View ID="ViewCS" runat="server">
                                <asp:Label ID="lbltitel1" runat="server" CssClass="lblHead" Visible="false"><h5> A. Comparative Statement</h5> </asp:Label>
                                <div class="col-md-12 table-responsive">
                                    <asp:GridView ID="gvResInfo" runat="server" AutoGenerateColumns="False" CssClass="table-striped table-hover table-bordered grvContentarea"
                                        ShowFooter="True" Width="1009px" OnRowDataBound="gvResInfo_RowDataBound">
                                        <PagerSettings Visible="False" />
                                        <RowStyle />
                                        <Columns>
                                            <asp:TemplateField HeaderText="Supl Code" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvSuplCod1" runat="server" Height="16px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "ssircode")) %>'
                                                        Width="80px"></asp:Label>
                                                    <asp:Label ID="lblrsircode" runat="server" Height="16px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rsircode")) %>'
                                                        Width="80px"></asp:Label>
                                                    <asp:Label ID="lblspcfcod" runat="server" Height="16px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "spcfcod")) %>'
                                                        Width="80px"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Sl">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvSlNo1" runat="server" Height="16px" Style="text-align: right"
                                                        Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="35px"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Res Code" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvResCod1" runat="server" Height="16px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "spcfdesc")) %>'
                                                        Width="80px"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Description of Goods">
                                                <ItemTemplate>

                                                    <asp:LinkButton OnClick="lblgrsirdescs1_Click" ID="LinkButton2" runat="server" Height="16px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rsirdesc")) %>'
                                                        Width="130px"></asp:LinkButton>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                </FooterTemplate>

                                                <ItemStyle />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Specification">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvrSpec" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                                        BorderWidth="0px" Font-Size="10px" Style="text-align: left; background-color: Transparent"
                                                        Text='<%# DataBinder.Eval(Container.DataItem, "spcfdesc").ToString() %>' Width="100px"></asp:Label>
                                                </ItemTemplate>


                                                <FooterStyle HorizontalAlign="Center" />
                                                <HeaderStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>


                                            <asp:TemplateField HeaderText="Unit">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvrsirunit" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                                        BorderWidth="0px" Font-Size="10px" Style="text-align: left; background-color: Transparent"
                                                        Text='<%# DataBinder.Eval(Container.DataItem, "rsirunit").ToString() %>' Width="50px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterStyle HorizontalAlign="Center" />
                                                <HeaderStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Package Size">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblGvPkgSizesup" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                                        BorderWidth="0px" Font-Size="10px" Style="text-align: left; background-color: Transparent"
                                                        Text='<%# DataBinder.Eval(Container.DataItem, "pkgsize").ToString() %>' Width="50px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterStyle HorizontalAlign="Center" />
                                                <HeaderStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Requirement Quantity">
                                                <FooterTemplate>

                                                    <asp:Label ID="lblFQty" runat="server" BackColor="Transparent"
                                                        BorderColor="Transparent" BorderStyle="None" BorderWidth="1px"
                                                        Font-Bold="True" Font-Size="10px"
                                                        Width="80px" ForeColor="#000"></asp:Label>


                                                </FooterTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvpropqty" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                                        BorderWidth="0px" Font-Size="10px" Style="text-align: right; background-color: Transparent"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "propqty")).ToString("#,##0.0000;(#,##0.0000); ") %>'
                                                        Width="80px"></asp:Label>
                                                    <asp:Label ID="lblgvpropqty_01" runat="server" BorderColor="#99CCFF" BorderStyle="Solid" Visible="false"
                                                        BorderWidth="0px" Font-Size="10px" Style="text-align: right; background-color: Transparent"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "propqty1")).ToString("#,##0.0000;(#,##0.0000); ") %>'
                                                        Width="80px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterStyle HorizontalAlign="right" />
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="CS </br> Qty" HeaderStyle-BackColor="Violet" ItemStyle-BackColor="Violet">
                                                <FooterTemplate>

                                                    <asp:Label ID="lblFcsreqqty" runat="server" BackColor="Transparent"
                                                        BorderColor="Transparent" BorderStyle="None" BorderWidth="1px"
                                                        Font-Bold="True" Font-Size="10px"
                                                        Width="80px" ForeColor="#000"></asp:Label>


                                                </FooterTemplate>
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtgvcsreqqty" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                                        BorderWidth="0px" Font-Size="10px" Style="text-align: right; background-color: Transparent"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "csreqqty")).ToString("#,##0.0000;(#,##0.0000); ") %>'
                                                        Width="80px"></asp:TextBox>
                                                </ItemTemplate>
                                                <FooterStyle HorizontalAlign="right" />
                                                <HeaderStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Currency">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblCurrency" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                                        BorderWidth="0px" Font-Size="10px" Style="text-align: left; background-color: Transparent"
                                                        Text='<%# DataBinder.Eval(Container.DataItem, "curdesc").ToString() %>' Width="80px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterStyle HorizontalAlign="Center" />
                                                <HeaderStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Conversion </br> Rate">

                                                <ItemTemplate>
                                                    <asp:Label ID="txtgvConrate" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                                        BorderWidth="0px" Font-Size="10px" Style="text-align: right; background-color: Transparent"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "conrate")).ToString("#,##0.000000;(#,##0.000000); ") %>'
                                                        Width="80px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterStyle HorizontalAlign="Center" />
                                                <HeaderStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Unit Price">

                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtgvRate" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                                        BorderWidth="0px" Font-Size="9px" Style="text-align: right; background-color: Transparent"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "rate")).ToString("#,##0.000000;(#,##0.000000); ") %>'
                                                        Width="70px"></asp:TextBox>
                                                </ItemTemplate>
                                                <FooterStyle HorizontalAlign="Center" />
                                                <HeaderStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="total Amount">
                                                <FooterTemplate>


                                                    <asp:Label ID="lblAmount" runat="server" BackColor="Transparent"
                                                        BorderColor="Transparent" BorderStyle="None" BorderWidth="1px"
                                                        Font-Bold="True" Font-Size="10px"
                                                        Width="80px" ForeColor="#000"></asp:Label>
                                                </FooterTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="txtgvamt" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                                        BorderWidth="0px" Font-Size="10px" Style="text-align: right; background-color: Transparent"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amount")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                        Width="80px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterStyle HorizontalAlign="right" />
                                                <HeaderStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>


                                            <asp:TemplateField HeaderText="Unit Price BDT ">

                                                <ItemTemplate>

                                                    <asp:Label ID="gvldbtRate" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                                        BorderWidth="0px" Font-Size="10px" Style="text-align: right; background-color: Transparent"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "bdtrate")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                        Width="70px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterStyle HorizontalAlign="right" />
                                                <HeaderStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Total Price BDT">

                                                <ItemTemplate>

                                                    <asp:Label ID="gvlblbdtAmount" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                                        BorderWidth="0px" Font-Size="10px" Style="text-align: right; background-color: Transparent"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "bdtamt")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                        Width="70px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterStyle HorizontalAlign="right" />
                                                <HeaderStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Supplier">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgrsirdesc1" runat="server" Height="16px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "supdesc")) %>'
                                                        Width="180px"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Select">

                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkboxgv" runat="server"
                                                        Checked='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "approved"))=="True" %>' />



                                                </ItemTemplate>
                                                <FooterStyle HorizontalAlign="Center" />
                                                <HeaderStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Last </br>Purchase</br> Rate">
                                                <FooterTemplate>
                                                </FooterTemplate>
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtgvlstpurate" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                                        BorderWidth="1px" Font-Size="10px" Style="text-align: right; float: right; background-color: Transparent"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "lstpurate")).ToString("#,##0.000000;(#,##0.000000); ") %>'
                                                        Width="80px"></asp:TextBox>
                                                </ItemTemplate>
                                                <FooterStyle HorizontalAlign="right" />
                                                <HeaderStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Last </br>Purchase</br> Date">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgrlpurdate" runat="server" Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "lpurdate")).ToString("dd-MMM-yyyy") %>'
                                                        Width="80px"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Payment </br>Terms" HeaderStyle-BackColor="SkyBlue" ItemStyle-BackColor="SkyBlue">
                                                <ItemTemplate>
                                                    <asp:DropDownList ID="ddlpaytermsc" CssClass="chzn-select" runat="server"></asp:DropDownList>
                                                </ItemTemplate>
                                                <%-- <ItemTemplate>
                                        <asp:TextBox ID="txtgvpaytypeC" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                            BorderWidth="0px" Font-Size="10px" Style="text-align: left; float: right; background-color: Transparent"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "paytype"))%>'
                                            Width="60px"></asp:TextBox>
                                    </ItemTemplate>--%>
                                                <FooterStyle HorizontalAlign="right" />
                                                <HeaderStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Mode Of </br>Payment" HeaderStyle-BackColor="SkyBlue" ItemStyle-BackColor="SkyBlue">
                                                <ItemTemplate>
                                                    <asp:DropDownList ID="ddlpaymodec" CssClass="chzn-select" runat="server"></asp:DropDownList>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Delivery </br>Terms" HeaderStyle-BackColor="SkyBlue" ItemStyle-BackColor="SkyBlue">
                                                <ItemTemplate>
                                                    <asp:DropDownList ID="ddldeltrmc" CssClass="chzn-select" runat="server"></asp:DropDownList>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Deliver </br>Mode" HeaderStyle-BackColor="SkyBlue" ItemStyle-BackColor="SkyBlue">
                                                <ItemTemplate>
                                                    <asp:DropDownList ID="ddldelmodc" CssClass="chzn-select" runat="server"></asp:DropDownList>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Advance </br>Amount" HeaderStyle-BackColor="SkyBlue" ItemStyle-BackColor="SkyBlue">

                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtgvadvamtC" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                                        BorderWidth="1px" Font-Size="10px" Style="text-align: right; float: right; background-color: Transparent"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "advamt")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                        Width="60px"></asp:TextBox>
                                                </ItemTemplate>
                                                <FooterStyle HorizontalAlign="right" />
                                                <HeaderStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Credit Period (Day)">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtgvaPeriod" BorderColor="#99CCFF" BorderStyle="Solid"
                                                        BorderWidth="1px" Font-Size="10px" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "payment1")) %>' Style="text-align: left; background-color: Transparent"
                                                        Width="80px"></asp:TextBox>
                                                </ItemTemplate>
                                                <ItemStyle />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Remarks">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="TextRemarks" BorderColor="#99CCFF" BorderStyle="Solid"
                                                        BorderWidth="1px" Style="border: none;" TextMode="MultiLine" runat="server" Font-Size="9px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "msrrmrk")) %>'
                                                        Width="100px"></asp:TextBox>
                                                </ItemTemplate>
                                                <ItemStyle />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Concern Person" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgConcern" runat="server" Font-Size="9px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "cperson")) %>'
                                                        Width="80px"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Mobile" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvMobile" runat="server" Font-Size="9px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mobile")) %>'
                                                        Width="80px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:LinkButton ID="lbtTotal1" runat="server" Visible="false" CssClass="btn btn-primary primaryBtn" OnClick="lbtTotal1_Click">Total</asp:LinkButton>

                                                </FooterTemplate>
                                                <ItemStyle />
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

                            <asp:View ID="ViewSuppTotal" runat="server">
                                <div class="col-md-6">
                                    <asp:Label ID="Label10" runat="server" CssClass="lblHead"><h5> B. Supplier Total</h5> </asp:Label>



                                    <div class="list-group list-group-bordered mb-3 " id="SupTotal" runat="server">
                                    </div>
                                    <!-- /.list-group -->
                                </div>
                            </asp:View>

                        </asp:MultiView>
                    </div>

                </div>
            </div>

            <asp:Panel ID="fotpanel" Visible="false" runat="server">

                <section class="card card-fluid">
                    <header class="card-header">
                        <ul class="nav nav-tabs card-header-tabs" id="tabContent">
                            <li class="nav-item active"><a class="nav-link" href="#tab2primary" data-toggle="tab"><span class="fa fa-comment"></span>Justification</a></li>
                            <li class="nav-item"><a class="nav-link" href="#tab3primary" data-toggle="tab"><span class="fa  fa-upload"></span>Upload</a></li>
                            <li class="nav-item"><a class="nav-link" href="#tab1primary" data-toggle="tab"><span class="fa fa-dollar-sign"></span>Charging</a></li>

                        </ul>
                    </header>

                    <div class="tab-content">
                        <div class="tab-pane active" id="tab2primary">

                            <div class="form-group">
                                <div class="col-md-12 pading5px">
                                    <asp:TextBox ID="txtMSRNarr" placeholder="Write here............" runat="server" class="form-control" Visible="false" Rows="3" TextMode="MultiLine"></asp:TextBox>

                                </div>
                                <div class="col-md-4 pading5px hidden">
                                    <div class="input-group">
                                        <span class="input-group-addon glypingraddon"></span>
                                        <asp:TextBox ID="txtMSRNarr2" placeholder="Write here............" runat="server" class="form-control" Rows="3" TextMode="MultiLine"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-4 pading5px hidden">
                                    <div class="input-group">
                                        <span class="input-group-addon glypingraddon"></span>
                                        <asp:TextBox ID="txtMSRNarr3" placeholder="Write here............" runat="server" class="form-control" Rows="3" TextMode="MultiLine"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="tab-pane   " id="tab1primary">
                            <asp:GridView ID="gvcharging" runat="server" AutoGenerateColumns="False" OnRowDataBound="gvcharging_RowDataBound" CssClass="table-striped table-hover table-bordered grvContentarea">
                                <PagerSettings Visible="False" />
                                <RowStyle />
                                <Columns>
                                    <asp:TemplateField HeaderText="Supl Code" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvSuplCod1" runat="server" Height="16px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "ssircode")) %>'
                                                Width="80px"></asp:Label>

                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Sl">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvSlNo1" runat="server" Height="16px" Style="text-align: right"
                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="25px"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Supplier Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvSupName" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "supdesc")) %>'
                                                Width="180px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Font-Size="X-Small" Width="160px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Amount">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvSupamt" Style="text-align: right;" runat="server" Height="16px" Text='<%# "("+ Convert.ToString(DataBinder.Eval(Container.DataItem, "curdesc")).ToString()+") "+Convert.ToDouble(DataBinder.Eval(Container.DataItem, "supamt")).ToString("#,##0.00;(#,##0.00); ")%>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Tax(%)" Visible="false">
                                        <ItemTemplate>
                                            <asp:TextBox ID="TxtgvTax" Style="text-align: right;" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "tax")).ToString("#,##0.00;") %>' Height="16px" Width="50px"></asp:TextBox>
                                        </ItemTemplate>
                                        <ItemStyle />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Vat (%)" Visible="false">
                                        <ItemTemplate>
                                            <asp:TextBox ID="TxtgvVat" Style="text-align: right;" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "vat")).ToString("#,##0.00;") %>' Height="16px" Width="50px"></asp:TextBox>
                                        </ItemTemplate>
                                        <ItemStyle />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="h1" Visible="false">
                                        <ItemTemplate>

                                            <asp:TextBox ID="gvText0" Style="text-align: right;" runat="server" Width="70px" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "c0")).ToString("#,##0.00;-#,##0.00; ") %>'></asp:TextBox>
                                        </ItemTemplate>
                                        <ItemStyle BorderStyle="None" Width="60px" HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="h2" Visible="false">
                                        <ItemTemplate>
                                            <asp:TextBox ID="gvText1" Style="text-align: right;" runat="server" Width="70px" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "c1")).ToString("#,##0.00;-#,##0.00; ") %>'></asp:TextBox>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="h3" Visible="false">
                                        <ItemTemplate>
                                            <asp:TextBox ID="gvText2" Style="text-align: right;" runat="server" Width="70px" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "c2")).ToString("#,##0.00;-#,##0.00; ") %>'></asp:TextBox>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="h4" Visible="false">
                                        <ItemTemplate>
                                            <asp:TextBox ID="gvText3" Style="text-align: right;" runat="server" Width="70px" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "c3")).ToString("#,##0.00;-#,##0.00; ") %>'></asp:TextBox>
                                        </ItemTemplate>

                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="h5" Visible="false">
                                        <ItemTemplate>
                                            <asp:TextBox ID="gvText4" Style="text-align: right;" runat="server" Width="70px" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "c4")).ToString("#,##0.00;-#,##0.00; ") %>'></asp:TextBox>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="h6" Visible="false">
                                        <ItemTemplate>
                                            <asp:TextBox ID="gvText5" Style="text-align: right;" runat="server" Width="70px" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "c5")).ToString("#,##0.00;-#,##0.00; ") %>'></asp:TextBox>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="h7" Visible="false">
                                        <ItemTemplate>
                                            <asp:TextBox ID="gvText6" Style="text-align: right;" runat="server" Width="70px" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "c6")).ToString("#,##0.00;-#,##0.00; ") %>'></asp:TextBox>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="h8" Visible="false">
                                        <ItemTemplate>
                                            <asp:TextBox ID="gvText7" Style="text-align: right;" runat="server" Width="70px" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "c7")).ToString("#,##0.00;-#,##0.00;") %>'></asp:TextBox>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <%-- <asp:TemplateField HeaderText="h8" Visible="false">
                                    <ItemTemplate>
                                          <asp:TextBox ID="gvText8"  Style="text-align:right;" runat="server" Width="70px" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "c8")).ToString("#,##0.00;-#,##0.00;") %>'></asp:TextBox>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>--%>
                                    <asp:TemplateField HeaderText="Total --" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblToal" Style="text-align: right;" runat="server" Width="70px" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "tamt")).ToString("#,##0.00;-#,##0.00;") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Purchase Type">
                                        <ItemTemplate>
                                            <asp:Label ID="lblpurtype" Visible="false" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "purtype")) %>'></asp:Label>
                                            <asp:DropDownList ID="ddlPurType" CssClass="form-control form-control-sm" AutoPostBack="true" runat="server"></asp:DropDownList>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="left" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="NOA Format">
                                        <ItemTemplate>
                                            <asp:HyperLink ID="HypNote" runat="server" Target="_blank" ForeColor="Blue" Font-Underline="false"><span class="glyphicon glyphicon-pencil"></span> NOA Insert Data
                                            </asp:HyperLink>
                                        </ItemTemplate>
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

                        <div class="tab-pane" id="tab3primary">

                            <div class="form-group">
                                <div class="col-md-4 col-sm-4 col-lg-4">
                                    <asp:Panel runat="server" ID="pnlQutatt" Visible="false">


                                        <div class="panel panel-primary">
                                            <div class="panel-heading">
                                                <span class="glyphicon glyphicon-upload"></span>Qutation Image Upload
   
                                            </div>
                                            <div class="panel-body">
                                                <div class="row">
                                                    <div class="col-lg-12">
                                                        <div class="row">
                                                            <div class="form-group">

                                                                <asp:Label ID="Label2" runat="server" CssClass="col-md-4" Text="Supplier Name"></asp:Label>
                                                                <div class="col-md-8">
                                                                    <asp:DropDownList ID="ddlBestSupplier" runat="server" CssClass="form-control"></asp:DropDownList>
                                                                </div>
                                                            </div>
                                                        </div>

                                                        <div class="row">


                                                            <fieldset class="alert alert-success">

                                                                <cc1:AsyncFileUpload OnClientUploadError="uploadError"
                                                                    OnClientUploadComplete="uploadComplete" runat="server"
                                                                    ID="AsyncFileUpload1" UploaderStyle="Modern"
                                                                    CompleteBackColor="White" Visible="false"
                                                                    UploadingBackColor="#CCFFFF" ThrobberID="imgLoader"
                                                                    OnUploadedComplete="FileUploadComplete" />
                                                                <asp:Image ID="imgLoader" runat="server" Visible="false" ImageUrl="~/images/Wait.gif" />
                                                                <br />
                                                                <asp:Label ID="lblMesg" runat="server" Text=""></asp:Label>
                                                            </fieldset>


                                                        </div>


                                                        <div style="display: none;">

                                                            <asp:FileUpload ID="FileUpload1" runat="server" />
                                                            <asp:Button Text="Upload" runat="server" OnClick="UploadFile" />
                                                            <asp:Button ID="btnConfirm" runat="server" OnClick="ConfirmReplace" Style="display: none" />
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </asp:Panel>

                                </div>
                                <div class="col-md-7">
                                    <div class="panel panel-primary">
                                        <div class="panel-heading">
                                            <span class="glyphicon glyphicon-picture"></span>Uploaded Files
       
                                            <div class="pull-right">
                                                <asp:Button ID="btnShowimg" runat="server" CssClass="btn btn-success btn-xs" Text="Show Image" OnClick="btnShowimg_Click" />
                                                <asp:LinkButton ID="btnDelall" runat="server" OnClick="btnDelall_OnClick" Visible="true" CssClass=" btn btn-xs btn-danger">Delete</asp:LinkButton>

                                            </div>
                                        </div>
                                        <div class="panel-body ">
                                            <div class="row">
                                                <div class="col-lg-12">
                                                    <asp:ListView ID="ListViewEmpAll" runat="server" ItemPlaceholderID="itemplaceholder" OnItemDataBound="ListViewEmpAll_ItemDataBound">
                                                        <LayoutTemplate>
                                                            <asp:PlaceHolder ID="itemplaceholder" runat="server" />
                                                        </LayoutTemplate>
                                                        <ItemTemplate>
                                                            <div class="col-xs-12 col-sm-4 col-md-2 listDiv" style="padding: 0 5px;">
                                                                <div id="EmpAll" runat="server">
                                                                    <%-- <a href="#"><i class="fa fa-archive"></i>
                                                        <asp:Label ID="Label1" runat="server" Text='<% #Bind("desig")%>'></asp:Label></a>--%>


                                                                    <asp:Label ID="ImgLink" Visible="false" runat="server" Text='<%# Eval("filePath1") %>'></asp:Label>
                                                                    <asp:Label ID="msrno" Visible="false" runat="server" Text='<%# Eval("msrno") %>'></asp:Label>
                                                                    <asp:Label ID="ssircode" Visible="false" runat="server" Text='<%# Eval("ssircode") %>'></asp:Label>

                                                                    <a href="<%# Eval("filePath1") %>" class="uploadedimg" target="_blank">
                                                                        <asp:Image ID="GetImg" runat="server" CssClass="image img img-responsive img-thumbnail" />
                                                                        <div class="middle">
                                                                            <span><%# Eval("supinfo") %></span>
                                                                        </div>

                                                                    </a>
                                                                    <div class="checkboxcls">
                                                                        <asp:CheckBox ID="ChDel" runat="server" />
                                                                    </div>


                                                                </div>

                                                            </div>
                                                        </ItemTemplate>
                                                    </asp:ListView>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>

                            </div>
                        </div>
                        <%-- <div class="tab-pane fade" id="tab4primary">Primary 4</div>
                        <div class="tab-pane fade" id="tab5primary">Primary 5</div>--%>
                    </div>
                </section>





            </asp:Panel>

            <!-- for the breakdown--->
            <div id="myModal" class="modal col-md-8 col-md-offset-2 animated slideInLeft" role="dialog">
                <div class="modal-dialog ">
                    <div class="modal-content  ">
                        <div class="modal-header">
                            <h4 class="modal-title">
                                <span class="fa fa-table"></span>
                                <asp:Label ID="lbmodalheading" runat="server">  Material Wise Issue and Purchase stock </asp:Label>
                                <%--  <asp:CheckBox runat="server" ID="chkexcl" Text="EXCEL" />
                                <asp:LinkButton ID="lbtnmisuprint" ToolTip="Print" OnClientClick="Clear()" OnClick="lbtnmisuprint_Click" runat="server" CssClass="btn btn-xs btn-success printBtn"><span class="glyphicon glyphicon-print"></span></asp:LinkButton>
                                --%>    <%--<asp:Button ID="lbtnmisuprint" runat="server"  OnClick="lbtnmisuprint_Click"  CssClass="btn btn-success printBtn okBtn" Text="Print" />--%>

                                <%--<asp:Button ID="lbtnmisuprint" runat="server"  OnClick="lbtnmisuprint_Click" data-dismiss="modal" Text="Print" />--%>
                            </h4>
                        </div>
                        <div class="modal-body">

                            <div class="row-fluid form-horizontal forgotform" id="">
                                <div class="form-group">

                                    <label class="col-md-3" style="font-weight: bold"><span class="glyphicon glyphicon-hand-right"></span>Store Name:</label>
                                    <asp:Label ID="lblstore" CssClass="col-md-9 bg-success" runat="server" BorderStyle="Solid" BorderWidth="1"></asp:Label>

                                </div>
                                <div class="form-group">

                                    <label class="col-md-3" style="font-weight: bold"><span class="glyphicon glyphicon-hand-right"></span>Material:</label>
                                    <asp:Label ID="lblmat" CssClass="col-md-9 bg-success" runat="server" BorderStyle="Solid" BorderWidth="1"></asp:Label>
                                </div>
                                <div class="form-group">
                                    <label class="col-md-3" style="font-weight: bold"><span class="glyphicon glyphicon-hand-right"></span>Specification:</label>
                                    <asp:Label ID="lblspc" CssClass="col-md-4 bg-success" runat="server" BorderStyle="Solid" BorderWidth="1"></asp:Label>
                                    <label class="col-md-2" style="font-weight: bold"><span class="glyphicon glyphicon-hand-right"></span>Unit:</label>
                                    <asp:Label ID="lblUnit" CssClass="col-md-3 bg-success" runat="server" BorderStyle="Solid" BorderWidth="1"></asp:Label>

                                </div>

                            </div>
                            <asp:GridView ID="gvMatHis" runat="server" HorizontalAlign="Center" OnRowDataBound="gvMatHis_RowDataBound" AutoGenerateColumns="false" CssClass="table-striped table-hover table-bordered grvContentarea">
                                <Columns>
                                    <asp:TemplateField HeaderText="Sl">
                                        <ItemTemplate>
                                            <asp:Label ID="mlblgvDSlNo" runat="server" Font-Bold="True" Style="text-align: right"
                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderText="Date" Visible="true">
                                        <ItemTemplate>
                                            <asp:Label ID="mlblgvexedate" runat="server" Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "exdate")).ToString("dd-MMM-yyyy") %>'
                                                Width="80px" Font-Bold="true"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="No">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgenno" Visible="false" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "isuno")) %>'
                                                Width="100px" Font-Bold="true"></asp:Label>
                                            <asp:Label ID="lblGp" Visible="false" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gp")) %>'
                                                Width="100px" Font-Bold="true"></asp:Label>
                                            <asp:Label ID="mlblgvrecno" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "isuno1")) %>'
                                                Width="100px" Font-Bold="true"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="In Qty">
                                        <ItemTemplate>
                                            <asp:Label ID="mlgvitmqty" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "inqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="80px" Style="text-align: right"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Out Qty">
                                        <ItemTemplate>
                                            <asp:Label ID="mlgvOutqty" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "outqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="80px" Style="text-align: right"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Present <br> Stock Qty">
                                        <ItemTemplate>
                                            <asp:Label ID="mlgvbalqty" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "stock")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="80px" Style="text-align: right"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="">
                                        <ItemTemplate>
                                            <asp:HyperLink ID="LbtnPrint" ToolTip="Order Print" Visible="false" Target="_blank" runat="server"><span class="glyphicon glyphicon-print"></span></asp:HyperLink>

                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>

                                </Columns>




                            </asp:GridView>




                        </div>
                        <div class="modal-footer ">

                            <asp:LinkButton ID="LbtnPrint" runat="server" CssClass="btn btn-sm btn-success" OnClick="LbtnModalPrint_Click"><span class="glyphicon glyphicon-print"></span> Print</asp:LinkButton>
                            <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>


                        </div>
                    </div>
                </div>
            </div>
            <div id="myModal" class="modal  animated slideInLeft" role="dialog">
                <div class="modal-dialog modal-lg">
                    <div class="modal-content  ">
                        <div class="modal-header">

                            <button type="button" style="background-color: red;" class="close btn btn-xs" data-dismiss="modal"><span class="fa fa-close"></span></button>
                            <h4 class="modal-title">
                                <span class="fa fa-table"></span>
                                <asp:Label ID="Label4" runat="server">  Material Wise Issue and Purchase stock </asp:Label>
                                <%--  <asp:CheckBox runat="server" ID="chkexcl" Text="EXCEL" />
                                <asp:LinkButton ID="lbtnmisuprint" ToolTip="Print" OnClientClick="Clear()" OnClick="lbtnmisuprint_Click" runat="server" CssClass="btn btn-xs btn-success printBtn"><span class="glyphicon glyphicon-print"></span></asp:LinkButton>
                                --%>    <%--<asp:Button ID="lbtnmisuprint" runat="server"  OnClick="lbtnmisuprint_Click"  CssClass="btn btn-success printBtn okBtn" Text="Print" />--%>

                                <%--<asp:Button ID="lbtnmisuprint" runat="server"  OnClick="lbtnmisuprint_Click" data-dismiss="modal" Text="Print" />--%>
                            </h4>
                        </div>
                        <div class="modal-body">
                            <asp:TextBox ID="txtflag" Style="display: none;" runat="server"></asp:TextBox>

                            <div class="row-fluid form-horizontal forgotform" id="">
                                <div class="form-group">

                                    <label class="col-md-3" style="font-weight: bold"><span class="glyphicon glyphicon-hand-right"></span>Store Name:</label>
                                    <asp:Label ID="Label7" CssClass="col-md-9 bg-success" runat="server" BorderStyle="Solid" BorderWidth="1"></asp:Label>

                                </div>
                                <div class="form-group">

                                    <label class="col-md-3" style="font-weight: bold"><span class="glyphicon glyphicon-hand-right"></span>Material:</label>
                                    <asp:Label ID="Label8" CssClass="col-md-3 bg-success" runat="server" BorderStyle="Solid" BorderWidth="1"></asp:Label>
                                    <label class="col-md-2" style="font-weight: bold"><span class="glyphicon glyphicon-hand-right"></span>Specification:</label>
                                    <asp:Label ID="Label9" CssClass="col-md-4 bg-success" runat="server" BorderStyle="Solid" BorderWidth="1"></asp:Label>

                                </div>


                            </div>

                            <div class="panel with-nav-tabs panel-default">
                                <div class="panel-heading">
                                    <ul class="nav nav-tabs">
                                        <li class="active"><a href="#tabpur" onclick="addplug('purisu');" data-toggle="tab"><span class="glyphicon glyphicon-shopping-cart"></span>Purchase And Issue</a></li>
                                        <li><a href="#tabhis" onclick="addplug('purhis');" data-toggle="tab"><span class="glyphicon glyphicon-briefcase"></span>Purchase History </a></li>

                                    </ul>
                                </div>
                                <div class="panel-body">
                                    <div class="tab-content">
                                        <div class="tab-pane fade in active" id="tabpur">
                                            <asp:GridView ID="GridView1" runat="server" HorizontalAlign="Center" OnRowDataBound="gvMatHis_RowDataBound" AutoGenerateColumns="false" CssClass="table-striped table-hover table-bordered grvContentarea">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Sl">
                                                        <ItemTemplate>
                                                            <asp:Label ID="mlblgvDSlNo" runat="server" Font-Bold="True" Style="text-align: right"
                                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>


                                                    <asp:TemplateField HeaderText="Supplier" Visible="true">
                                                        <ItemTemplate>
                                                            <asp:Label ID="mlblgvsup" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "ssirdesc")) %>'
                                                                Width="150px" Font-Bold="true"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Price">
                                                        <ItemTemplate>
                                                            <asp:Label ID="mlgvPrice" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "rate")).ToString("#,##0.0000;(#,##0.0000); ") %>'
                                                                Width="80px" Style="text-align: right"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Date" Visible="true">
                                                        <ItemTemplate>
                                                            <asp:Label ID="mlblgvexedate" runat="server" Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "exdate")).ToString("dd-MMM-yyyy") %>'
                                                                Width="80px" Font-Bold="true"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Unit">
                                                        <ItemTemplate>
                                                            <asp:Label ID="mlblgvunit" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "unit")) %>'
                                                                Width="50px" Font-Bold="true"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="No">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgenno" Visible="false" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "isuno")) %>'
                                                                Width="100px" Font-Bold="true"></asp:Label>
                                                            <asp:Label ID="lblGp" Visible="false" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gp")) %>'
                                                                Width="100px" Font-Bold="true"></asp:Label>
                                                            <asp:Label ID="mlblgvrecno" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "isuno1")) %>'
                                                                Width="80px" Font-Bold="true"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="In Qty">
                                                        <ItemTemplate>
                                                            <asp:Label ID="mlgvitmqty" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "inqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                                Width="80px" Style="text-align: right"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Out Qty">
                                                        <ItemTemplate>
                                                            <asp:Label ID="mlgvOutqty" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "outqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                                Width="80px" Style="text-align: right"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Stock Qty">
                                                        <ItemTemplate>
                                                            <asp:Label ID="mlgvbalqty" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "stock")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                                Width="80px" Style="text-align: right"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="">
                                                        <ItemTemplate>
                                                            <asp:HyperLink ID="LbtnPrint" ToolTip="Order Print" Visible="false" Target="_blank" runat="server"><span class="glyphicon glyphicon-print"></span></asp:HyperLink>

                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>

                                                </Columns>




                                            </asp:GridView>
                                        </div>
                                        <div class="tab-pane fade" id="tabhis">
                                            <asp:GridView ID="gvMatPurHis" runat="server"
                                                AutoGenerateColumns="False" CssClass="table-striped table-hover table-bordered grvContentarea"
                                                ShowFooter="True" Width="830px">
                                                <PagerSettings Position="Top" />
                                                <RowStyle />
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Sl">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvSlNo" runat="server" Font-Bold="True"
                                                                Style="text-align: right"
                                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>




                                                    <asp:TemplateField HeaderText="MRR Date">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lgvOrderDate" runat="server"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mrrdat1")) %>'
                                                                Width="70px"></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="left" />
                                                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                                    </asp:TemplateField>


                                                    <asp:TemplateField HeaderText="MRR No">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lgvorderno" runat="server"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mrrno1")) %>'
                                                                Width="70px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Req. No" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lgvreqno" runat="server"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "reqno1")) %>'
                                                                Width="70px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="MRF No" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lgvreqrefno" runat="server"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mrfno")) %>'
                                                                Width="70px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Store Name">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvprojectdesc" runat="server"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc")) %>'
                                                                Width="140px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Supplier Name">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvSupName" runat="server"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "ssirdesc")) %>'
                                                                Width="140px"></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label ID="lgvSupName" runat="server" Font-Bold="True" Font-Size="12px" Text="Total: "
                                                                ForeColor="#000" Style="text-align: right" Width="70px"></asp:Label>
                                                        </FooterTemplate>
                                                        <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                                    </asp:TemplateField>


                                                    <asp:TemplateField HeaderText="Brand Name">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lgvBrName" runat="server"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "spcfdesc")) %>'
                                                                Width="60px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                                    </asp:TemplateField>


                                                    <asp:TemplateField HeaderText="MRR Qty">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lgvmrrqty" runat="server" Style="text-align: right"
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "mrrqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                                Width="55px"></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label ID="lgvMRRQty" runat="server" Font-Bold="True" Font-Size="12px"
                                                                ForeColor="#000" Style="text-align: right" Width="55px"></asp:Label>
                                                        </FooterTemplate>
                                                        <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Rate">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lgvrate" runat="server" Style="text-align: right"
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "mrrrat")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                                Width="55px"></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Amount">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lgvAmt" runat="server" Style="text-align: right"
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "mrramt")).ToString("#,##0;(#,##0); ") %>'
                                                                Width="70px"></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label ID="lgvFAmt" runat="server" Font-Bold="True" Font-Size="12px"
                                                                ForeColor="#000" Style="text-align: right" Width="70px"></asp:Label>
                                                        </FooterTemplate>
                                                        <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
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
                            </div>





                        </div>
                        <div class="modal-footer ">

                            <asp:LinkButton ID="LinkButton3" runat="server" CssClass="btn btn-sm btn-success" OnClientClick="CLoseModal()" OnClick="LbtnModalPrint_Click"><span class="glyphicon glyphicon-print"></span> Print</asp:LinkButton>
                            <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>


                        </div>
                    </div>
                </div>
            </div>
            <div id="ServyModal" class="modal  animated slideInLeft" role="dialog">
                <div class="modal-dialog modal-lg">
                    <div class="modal-content  ">
                        <div class="modal-header">

                            <h4 class="modal-title">
                                <span class="fa fa-table"></span>
                                <asp:Label ID="Label5" runat="server"> Purchase Supplier Information Add </asp:Label>
                            </h4>
                        </div>
                        <div class="modal-body">
                            <div class="row">
                                <div class=" col-md-6 col-md-6 col-lg-6">
                                    <asp:Label ID="lblResList2" runat="server" CssClass="label " Text="Supplier"></asp:Label>
                                    <asp:DropDownList ID="ddlSupl2" runat="server" CssClass="form-control  chzn-select" TabIndex="6"></asp:DropDownList>
                                </div>
                                <div class="col-2 col-md-2 col-lg-2">
                                    <asp:LinkButton ID="btnCurr" runat="server" CssClass="label" Text="Currency:"></asp:LinkButton>

                                    <asp:HyperLink ID="HyperLink1" runat="server" ToolTip="Create List" Target="_blank"
                                        NavigateUrl="~/F_17_Acc/AccConversion.aspx"><span class="glyphicon glyphicon-plus"></span></asp:HyperLink>

                                    <asp:DropDownList ID="ddlCurrency" CssClass="form-control form-control-sm" runat="server" OnSelectedIndexChanged="ddlCurrency_SelectedIndexChanged"></asp:DropDownList>

                                </div>
                                <div class="col-md-2 col-lg-2">
                                    <div class="form-group">
                                        <asp:Label ID="Label3" runat="server" CssClass="label" Text="Con. Rate:"></asp:Label>
                                        <asp:TextBox ID="TextBox1" runat="server" CssClass="form-control form-control-sm" ReadOnly="true"></asp:TextBox>
                                    </div>
                                </div>
                                <div class=" col-md-2 col-lg-2 ">
                                    <div class="form-group">
                                        <asp:Label ID="Label6" runat="server" CssClass="label" Text="Rate:"></asp:Label>
                                        <asp:TextBox ID="TextRate" CssClass="form-control form-control-sm" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="clearfix"></div>

                        </div>





                        <div class="modal-footer ">

                            <asp:LinkButton ID="LinkButton1" runat="server" CssClass="btn btn-sm btn-success" OnClick="UpdateData_Click"><span class="glyphicon glyphicon-Save"></span> Save</asp:LinkButton>
                            <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>


                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
        <%--<Triggers>
            <asp:PostBackTrigger ControlID="AsyncFileUpload1"></asp:PostBackTrigger>
        </Triggers>--%>
    </asp:UpdatePanel>

    <script language="javascript" type="text/javascript">

                     $(document).ready(function () {
                         //For navigating using left and right arrow of the keyboard
                         Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);
                     });
                     function pageLoaded() {

                         $("input, select").bind("keydown", function (event) {
                             var k1 = new KeyPress();
                             k1.textBoxHandler(event);
                         });

            <%--var gvBestSelect = $('#<%=this.gvBestSelect.ClientID %>');
            gvBestSelect.gridviewScroll({
                width: 1220,
                height: 310,
                arrowsize: 30,
                railsize: 16,
                barsize: 8,
                varrowtopimg: "../Image/arrowvt.png",
                varrowbottomimg: "../Image/arrowvb.png",
                harrowleftimg: "../Image/arrowhl.png",
                harrowrightimg: "../Image/arrowhr.png",
                freezesize: 7
            });
            var gvResInfo = $('#<%=this.gvResInfo.ClientID %>');
            gvResInfo.gridviewScroll({
                width: 1220,
                height: 410,
                arrowsize: 30,
                railsize: 16,
                barsize: 8,
                varrowtopimg: "../Image/arrowvt.png",
                varrowbottomimg: "../Image/arrowvb.png",
                harrowleftimg: "../Image/arrowhl.png",
                harrowrightimg: "../Image/arrowhr.png",
                freezesize: 7
            });--%>
            <%-- var gvBestSelect = $('#<%=this.gvBestSelect.ClientID %>');
            gvBestSelect.Scrollable();--%>
            <%--  var gvResInfo = $('#<%=this.gvResInfo.ClientID %>');
            gvResInfo.Scrollable();--%>


            <%--var gridview = $('#<%=this.gvResInfo.ClientID %>');
             $.keynavigation(gridview);--%>
    }
    </script>
    <script type="text/javascript">
    function uploadComplete(sender) {
        $get("<%=lblMesg.ClientID%>").style.color = "green";
            $get("<%=lblMesg.ClientID%>").innerHTML = "File Uploaded Successfully";
    }

    function uploadError(sender) {
        $get("<%=lblMesg.ClientID%>").style.color = "red";
            $get("<%=lblMesg.ClientID%>").innerHTML = "File upload failed.";
    }


    </script>


</asp:Content>

