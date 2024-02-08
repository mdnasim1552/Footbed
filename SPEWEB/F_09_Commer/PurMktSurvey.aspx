<%@ Page Title="" Language="C#" MasterPageFile="~/SPE.Master" AutoEventWireup="true" CodeBehind="PurMktSurvey.aspx.cs" Inherits="SPEWEB.F_09_Commer.PurMktSurvey" %>

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
                        <div class="col-md-3 col-sm-3 col-lg-3 ">
                            <div class="form-group">
                                <asp:Label ID="lblInformation" runat="server" CssClass="label">Information for</asp:Label>
                                <asp:DropDownList ID="ddlSurveyType" runat="server" AutoPostBack="True" CssClass="chzn-select form-control form-control-sm" TabIndex="6" OnSelectedIndexChanged="ddlSurveyType_SelectedIndexChanged">
                                    <asp:ListItem Value="1">Market Survey Report</asp:ListItem>
                                    <asp:ListItem>Material Wise Suppliers List</asp:ListItem>
                                    <asp:ListItem Value="3">Supplier Wise Materials List</asp:ListItem>
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



                    </div>
                </div>
            </div>
            <div class="card card-fluid">
                <div class="card-body" style="min-height: 300px;">

                    <asp:MultiView ID="MultiView1" runat="server" ActiveViewIndex="0">
                        <asp:View ID="View1" runat="server">
                            <div class="row">
                                <fieldset class="scheduler-border fieldset_A">
                                    <div class="form-horizontal">

                                        <div class="form-group">

                                            <div class="col-md-3 pading5px">
                                                <asp:Label ID="lblcontrolAccHead" runat="server" CssClass="lblTxt lblName" Text="Survey No."></asp:Label>
                                                <asp:Label ID="lblCurMSRNo1" runat="server" CssClass="ddlPage62" Text="MSR00-"></asp:Label>
                                                <asp:TextBox ID="txtCurMSRNo2" runat="server" CssClass="inputtextbox">00000</asp:TextBox>
                                            </div>
                                            <div class="col-md-3 pading5px asitCol3">
                                                <asp:Label ID="lbl211" runat="server" CssClass="smLbl_to" Text="Date"></asp:Label>
                                                <asp:TextBox ID="txtCurMSRDate" runat="server" AutoCompleteType="Disabled" CssClass="inputTxt inputDateBox" ToolTip="(dd.mm.yyyy)"></asp:TextBox>
                                                <cc1:CalendarExtender ID="txtCurMSRDate_CalendarExtender" runat="server"
                                                    Enabled="True" Format="dd.MM.yyyy " TargetControlID="txtCurMSRDate"></cc1:CalendarExtender>
                                                <asp:LinkButton ID="lbtnMSROk" runat="server" CssClass="btn btn-primary okBtn" OnClick="lbtnMSROk_Click" TabIndex="2">Ok</asp:LinkButton>

                                            </div>
                                        </div>

                                        <div class="form-group">
                                            <div class="col-md-3 pading5px asitCol3">
                                                <asp:Label ID="lblPreMrList" runat="server" CssClass="lblTxt lblName" Text="Previous MSR List"></asp:Label>
                                                <asp:TextBox ID="txtPreMSRSearch" runat="server" CssClass="inputTxt inpPixedWidth" TabIndex="4"></asp:TextBox>

                                                <div class="colMdbtn">
                                                    <asp:LinkButton ID="ImgbtnFindPreMR" CssClass="btn btn-primary srearchBtn" runat="server" OnClick="ImgbtnFindPreMR_Click" TabIndex="5"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>
                                                </div>
                                            </div>
                                            <div class="col-md-4 pading5px">
                                                <asp:DropDownList ID="ddlPrevMSRList" runat="server" CssClass="form-control inputTxt chzn-select" TabIndex="6">
                                                </asp:DropDownList>

                                            </div>




                                        </div>
                                    </div>
                                </fieldset>
                            </div>
                            <asp:Panel ID="Panel1" runat="server" Visible="False">
                                <div class="row">
                                    <asp:Panel ID="Panel4" runat="server">
                                        <fieldset class="scheduler-border fieldset_A">
                                            <div class="form-horizontal">
                                                <div class="form-group">
                                                    <div class="col-md-3 pading5px asitCol3">
                                                        <asp:Label ID="Label1" runat="server" CssClass="lblTxt lblName" Text="Materials List"></asp:Label>
                                                        <asp:TextBox ID="txtMSRResSearch" runat="server" CssClass="inputTxt inpPixedWidth" TabIndex="4"></asp:TextBox>

                                                        <div class="colMdbtn">
                                                            <asp:LinkButton ID="ImgbtnFindMat" CssClass="btn btn-primary srearchBtn" runat="server" OnClick="ImgbtnFindMat_Click" TabIndex="5"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-4 pading5px ">
                                                        <asp:DropDownList ID="ddlMSRRes" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlMSRRes_SelectedIndexChanged" CssClass="form-control inputTxt chzn-select" TabIndex="6">
                                                        </asp:DropDownList>
                                                    </div>
                                                    <div class="col-md-1 pading5px">
                                                        <asp:LinkButton ID="lbtnMSRSelect" runat="server" CssClass="btn btn-primary primarygrdBtn" OnClick="lbtnMSRSelect_Click" TabIndex="2">Select</asp:LinkButton>

                                                    </div>
                                                </div>

                                                <div class="form-group" style="display: none;">
                                                    <div class="col-md-3 pading5px asitCol3">
                                                        <asp:Label ID="lblspecificationms" runat="server" CssClass="lblTxt lblName" Text="Specification"></asp:Label>
                                                        <asp:TextBox ID="txtsrchSpecification3" runat="server" CssClass="inputTxt inpPixedWidth" TabIndex="4"></asp:TextBox>

                                                        <div class="colMdbtn">
                                                            <asp:LinkButton ID="ImgbtnFindSpecificationms" CssClass="btn btn-primary srearchBtn" runat="server" OnClick="ImgbtnFindSpecificationms_Click" TabIndex="5"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-4 pading5px">
                                                        <asp:DropDownList ID="ddlSpecificationms" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlSpecificationms_SelectedIndexChanged" CssClass="form-control inputTxt chzn-select" TabIndex="6">
                                                        </asp:DropDownList>

                                                    </div>



                                                </div>

                                                <div class="form-group">
                                                    <div class="col-md-3 pading5px asitCol3">
                                                        <asp:Label ID="Label2" runat="server" CssClass="lblTxt lblName" Text="Supplier List"></asp:Label>
                                                        <asp:TextBox ID="txtMSRSupSearch" runat="server" CssClass="inputTxt inpPixedWidth" TabIndex="4"></asp:TextBox>

                                                        <div class="colMdbtn">
                                                            <asp:LinkButton ID="ImgbtnFindSup" CssClass="btn btn-primary srearchBtn" runat="server" OnClick="ImgbtnFindSup_Click" TabIndex="5"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-4 pading5px">
                                                        <asp:DropDownList ID="ddlMSRSupl" runat="server" CssClass="form-control inputTxt chzn-select" TabIndex="6">
                                                        </asp:DropDownList>

                                                    </div>


                                                </div>
                                            </div>
                                        </fieldset>
                                    </asp:Panel>

                                    <asp:GridView ID="gvMSRInfo" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                                        ShowFooter="True" Width="900px" CssClass="table-striped table-hover table-bordered grvContentarea">
                                        <PagerSettings Visible="False" />
                                        <RowStyle />
                                        <Columns>
                                            <asp:TemplateField HeaderText="Sl">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvMSRSlNo" runat="server" Height="16px" Style="text-align: right"
                                                        Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Res Code" Visible="False">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvMSRResCod" runat="server" Height="16px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rsircode")) %>'
                                                        Width="60px"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Spcf Code" Visible="False">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvMSRSpcfCod" runat="server" Height="16px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "spcfcod")) %>'
                                                        Width="60px"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Supplier Code" Visible="False">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvMSRSuplCod" runat="server" Height="16px"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "ssircode")) %>'
                                                        Width="60px"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText=" Materials Description ">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvMSRResDesc" runat="server"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rsirdesc1")) %>'
                                                        Width="130px">
                                                              
                                                                
                                                    </asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:LinkButton ID="lbtnMSRTotal" runat="server" CssClass="btn btn-primary primaryBtn" OnClick="lbtnMSRTotal_Click">Total</asp:LinkButton>

                                                </FooterTemplate>
                                                <HeaderStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Req. Qty" Visible="false">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtgvMSRqty" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                                        BorderWidth="0px" Font-Bold="True" Font-Size="11px" Style="text-align: right; background-color: Transparent"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "reqqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                        Width="55px"></asp:TextBox>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Supplier Name">
                                                <FooterTemplate>
                                                    <asp:LinkButton ID="lbtnMSRUpdate" runat="server" CssClass="btn btn-danger primaryBtn" OnClick="lbtnMSRUpdate_Click">Final Update</asp:LinkButton>

                                                </FooterTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvMSRSuplDesc" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "ssirdesc1")) %>'
                                                        Width="120px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterStyle HorizontalAlign="Center" />
                                                <HeaderStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>


                                            <asp:TemplateField HeaderText="Specification" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvspcfdesc" runat="server"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "spcfdesc")) %>'
                                                        Width="100px">
                                                              
                                                                
                                                    </asp:Label>
                                                </ItemTemplate>

                                                <HeaderStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>


                                            <asp:TemplateField HeaderText="Unit">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvMSRResUnit" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rsirunit")) %>'
                                                        Width="30px"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Last Purchase Rate">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvLRate" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                                        BorderWidth="0px" Font-Bold="True" Font-Size="11px" Style="text-align: right; background-color: Transparent"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "maxrate")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                        Width="60px"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Current Price">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtgvMSRRate" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                                        BorderWidth="0px" Font-Bold="True" Font-Size="11px" Style="text-align: right; background-color: Transparent"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "resrate")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                        Width="60px"></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Conv. Rate">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvconrate" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                                        BorderWidth="0px" Font-Bold="True" Font-Size="11px" Style="text-align: right; background-color: Transparent"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "conrate")).ToString("#,##0.0000;(#,##0.0000); ") %>'
                                                        Width="40px"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Currency">
                                                <ItemTemplate>
                                                    <asp:Label ID="lnlgvcurdesc" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                                        BorderWidth="0px" Font-Bold="False" Font-Size="11px" Style="text-align: right; background-color: Transparent"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "curdesc")) %>'
                                                        Width="40px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Amount BDT">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvtotalamt" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                                        BorderWidth="0px" Font-Bold="True" Font-Size="11px" Style="text-align: right; background-color: Transparent"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "totalamt")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                        Width="60px"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Pro <br>Position<br> Qty">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvpropqty" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                                        BorderWidth="0px" Font-Bold="True" Font-Size="11px" Style="text-align: right; background-color: Transparent"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "propqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                        Width="60px"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Credit<br> Period<br> (Day)">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtgvMSRPayment" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                                        BorderWidth="0px" Font-Bold="False" Font-Size="11px" Style="text-align: right; background-color: Transparent"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "payment")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                        Width="60px"></asp:TextBox>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Delivery <br>Period <br>(Day)">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtgvMSRDel" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                                        BorderWidth="0px" Font-Bold="False" Font-Size="11px" Style="text-align: right; background-color: Transparent"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "delivery")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                        Width="60px"></asp:TextBox>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Concern  Person">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvMSRCperson" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "cperson")) %>'
                                                        Width="80px"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Mode of Transport">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvtrasport" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "trasport")) %>'
                                                        Width="60px"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Telephone" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvMSRPhone" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "phone")) %>'
                                                        Width="70px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Mobile">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvMSRMobile" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mobile")) %>'
                                                        Width="70px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Remarks">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtgvMSRRemarks" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                                        BorderWidth="0px" Font-Size="11px" Style="text-align: left; background-color: Transparent"
                                                        Text='<%# DataBinder.Eval(Container.DataItem, "msrrmrk").ToString() %>' Width="100px"></asp:TextBox>
                                                </ItemTemplate>
                                                <FooterStyle HorizontalAlign="Center" />
                                                <HeaderStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                        </Columns>
                                        <FooterStyle CssClass="grvFooter" />
                                        <EditRowStyle />
                                        <AlternatingRowStyle />
                                        <PagerStyle CssClass="gvPagination" />
                                        <HeaderStyle CssClass="grvHeader" />
                                    </asp:GridView>
                                    <fieldset class="scheduler-border fieldset_Nar">
                                        <div class="form-horizontal">

                                            <div class="form-group">
                                                <div class="col-md-3 pading5px asitCol3 ">
                                                    <asp:Label ID="lblApprovedBy" runat="server" CssClass="lblTxt lblName" Text="Approved By:" Visible="False"></asp:Label>
                                                    <asp:Label ID="lblApprovalDate" runat="server" CssClass="lblTxt lblName" Text="Approv.Date" Visible="False"></asp:Label>
                                                </div>
                                                <div class="col-md-4 pading5px">

                                                    <asp:Label ID="lblPreparedBy" runat="server" CssClass="lblTxt lblName" Text="Prepared By" Visible="false"></asp:Label>
                                                    <asp:TextBox ID="txtSrinfo" runat="server" CssClass="inputtextbox" Visible="false"></asp:TextBox>

                                                </div>

                                            </div>
                                            <div class="form-group">
                                                <div class="col-md-6 pading5px">
                                                    <div class="input-group">
                                                        <span class="input-group-addon glypingraddon">
                                                            <asp:Label ID="lblReqNarr" runat="server" CssClass="lblTxt lblName" Text="Selected Vendor Justification::"></asp:Label>
                                                        </span>
                                                        <asp:TextBox ID="txtMSRNarr" runat="server" class="form-control" Rows="2" TextMode="MultiLine"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <div class="col-md-2 pading5px asitCol2">
                                                    <asp:TextBox ID="txtApprovedBy" runat="server" class="form-control" Visible="false"></asp:TextBox>
                                                </div>
                                                <div class="col-md-2 pading5px asitCol2">
                                                    <asp:TextBox ID="txtApprovalDate" runat="server" class="form-control" ToolTip="(dd.mm.yyyy)" Visible="False"></asp:TextBox>
                                                </div>
                                                <div class="col-md-2 pading5px asitCol2">
                                                    <asp:TextBox ID="txtPreparedBy" runat="server" class="form-control" Visible="false"></asp:TextBox>
                                                </div>
                                                </td>
                                            </div>
                                        </div>

                                    </fieldset>
                                </div>

                            </asp:Panel>
                        </asp:View>
                        <asp:View ID="View2" runat="server">
                            <div class="row">


                                <div class="col-md-3 col-sm-3 col-lg-3">
                                    <div class="form-group">
                                        <div class="form-group">
                                            <asp:LinkButton ID="LinkGrpButton" OnClick="LinkGrpButton_Click" runat="server" CssClass="lblTxt lblName">Group</asp:LinkButton>
                                            <asp:DropDownList ID="ddlGroup" Width="60%" OnSelectedIndexChanged="ddlGroup_SelectedIndexChanged" AutoPostBack="true" runat="server" CssClass="chzn-select form-control form-control-sm">
                                            </asp:DropDownList>
                                            <asp:TextBox ID="txtGrpSrch" Visible="false" runat="server" Width="120%" CssClass="inputTxt inpPixedWidth" TabIndex="3"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>


                                <div class="col-md-3 col-sm-3 col-lg-3" style="margin-left: -140px;">
                                    <div class="form-group">
                                        <asp:LinkButton ID="ImgbtnFindRes1" OnClick="ImgbtnFindRes1_Click" runat="server" CssClass="lblTxt lblName" Text="Materials"></asp:LinkButton>
                                        <asp:DropDownList ID="ddlResList1" runat="server" Width="70%" CssClass="form-control inputTxt chzn-select" AutoPostBack="true" TabIndex="3" OnSelectedIndexChanged="ddlResList1_SelectedIndexChanged">
                                        </asp:DropDownList>
                                        <asp:TextBox ID="txtResSearch1" Visible="false" runat="server" Width="120%" CssClass="inputTxt inpPixedWidth" TabIndex="3"></asp:TextBox>
                                    </div>
                                </div>


                                <div class="col-md-3 col-sm-3 col-lg-3" style="margin-left: -100px;">
                                    <div class="form-group">
                                        <asp:LinkButton ID="ImgbtnFindSpecification" runat="server" CssClass="lblTxt lblName" OnClick="ImgbtnFindSpecification_Click" Text="Specification"></asp:LinkButton>
                                        <asp:DropDownList ID="ddlSpecification" runat="server" Width="70%" CssClass="form-control inputTxt chzn-select" TabIndex="6">
                                        </asp:DropDownList>
                                    </div>
                                </div>


                                <div class="col-md-1 col-sm-1 col-lg-1 ">
                                    <div class="form-group" style="margin-top: 20px; margin-left: -100px;">
                                        <asp:LinkButton ID="lbtnSelectRes1" runat="server" CssClass="btn btn-primary btn-sm  primarygrdBtn" OnClick="lbtnSelectRes1_Click" TabIndex="2">Ok</asp:LinkButton>

                                    </div>
                                </div>

                                <div class="col-md-2 col-sm-2 col-lg-2 ">
                                </div>

                                <div class="col-md-3 col-sm-3 col-lg-3">
                                    <div class="form-group">
                                        <asp:LinkButton ID="ImgbtnFindSupl1" Visible="false" OnClick="ImgbtnFindSupl1_Click" runat="server" CssClass="lblTxt lblName" Text="Supplier"></asp:LinkButton>
                                        <asp:DropDownList ID="ddlSupl1" runat="server" Width="70%" Visible="false" CssClass="form-control inputTxt chzn-select" TabIndex="6">
                                        </asp:DropDownList>
                                        <asp:TextBox ID="txtSuplSearch1" Visible="false" runat="server" Width="120%" CssClass="inputTxt inpPixedWidth" TabIndex="4"></asp:TextBox>
                                    </div>
                                </div>


                                <div class="col-md-1 col-sm-1 col-lg-1 " style="margin-left: -100px;">
                                    <div class="form-group">
                                        <asp:LinkButton ID="LinkButtonCrcy" runat="server" Visible="false" CssClass="lblTxt lblName" OnClick="btnCurr_Click">Currency</asp:LinkButton>
                                        <asp:HyperLink ID="HyperLinkCrcy" runat="server" Visible="false" ToolTip="Create List" Target="_blank" NavigateUrl="~/F_34_Mgt/AccConversion.aspx"><span class="fa fa-plus-circle"></span></asp:HyperLink>

                                        <asp:DropDownList ID="ddlCurList" runat="server" Visible="false" CssClass="chzn-select form-control form-control-sm" AutoPostBack="True" OnSelectedIndexChanged="ddlCurList_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-1 col-sm-1 col-lg-1 ">

                                    <div class="form-group">
                                        <asp:Label ID="LabelCRate" runat="server" Visible="false" CssClass="lblTxt lblName">Con. Rate</asp:Label>

                                        <asp:TextBox ID="TextBoxCRate" runat="server" Visible="false" Font-Size="9" Width="100%" CssClass="inputTxt inpPixedWidth" TabIndex="4" ReadOnly="true"></asp:TextBox>
                                    </div>
                                </div>

                                <div class="col-md-1 col-sm-1 col-lg-1 " style="margin-left: 30px;">
                                    <div class="form-group" style="margin-top: 20px;">
                                        <asp:LinkButton ID="lbtnSelectSupl1" runat="server" Visible="false" CssClass="btn btn-primary btn-sm  primarygrdBtn" OnClick="lbtnSelectSupl1_Click" TabIndex="2">Select</asp:LinkButton>

                                    </div>
                                </div>


                            </div>



                            <asp:Panel ID="Panel2" runat="server" Visible="False">
                                <div class="row">

                                    <div class="col-md-12 d-flex">

                                        <asp:GridView ID="gvSuplInfo" runat="server" AllowPaging="True" OnRowDeleting="gvSuplInfo_RowDeleting" CssClass="table-striped table-hover table-bordered grvContentarea table-responsive"
                                            AutoGenerateColumns="False" OnPageIndexChanging="gvSuplInfo_PageIndexChanging"
                                            ShowFooter="True" Width="809px">
                                            <PagerSettings NextPageText="Next" PreviousPageText="Previous" Position="Bottom"
                                                Mode="NumericFirstLast" />

                                            <Columns>
                                                <asp:TemplateField HeaderText="Sl">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvSlNo0" runat="server" Height="16px" Style="text-align: right"
                                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="35px"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:CommandField ShowDeleteButton="True" />

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
                                                        <asp:Label ID="lblgvSuplDesc1" runat="server" Text='<%# "<B>" + Convert.ToString(DataBinder.Eval(Container.DataItem, "ssirdesc1")) + "</B>" +
                                                                         (DataBinder.Eval(Container.DataItem, "spcfdesc").ToString().Trim().Length>0 ? 
                                                                         (Convert.ToString(DataBinder.Eval(Container.DataItem, "ssirdesc1")).Trim().Length>0 ? "<br>" : "") + 
                                                                         "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + 
                                                                         Convert.ToString(DataBinder.Eval(Container.DataItem, "spcfdesc")).Trim(): "")   %>'
                                                            Width="250px"> </asp:Label>
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
                                                    <%--<FooterTemplate>
                                                    <asp:LinkButton ID="lbtnSuplUpdate" runat="server" CssClass="btn btn-danger primaryBtn" OnClick="lbtnSuplUpdate_Click">Final Update</asp:LinkButton>

                                                </FooterTemplate>--%>
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtgvRate1" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                                            BorderWidth="0px" Font-Size="11px" Style="text-align: right; background-color: Transparent"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "rate")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                            Width="80px"></asp:TextBox>
                                                    </ItemTemplate>
                                                    <FooterStyle HorizontalAlign="Center" />
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Conv. Rate">

                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvconrate" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                                            BorderWidth="0px" Font-Size="11px" Style="text-align: right; background-color: Transparent"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "conrate")).ToString("#,##0.00;(#,##0.00); ") %>'
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
                                            </Columns>
                                            <FooterStyle CssClass="grvFooter" />
                                            <EditRowStyle />
                                            <AlternatingRowStyle />
                                            <PagerStyle CssClass="gvPagination" />
                                            <HeaderStyle CssClass="grvHeader" />
                                        </asp:GridView>

                                        <asp:GridView ID="grvSurveArchDate" runat="server" AllowPaging="True" CssClass="table-striped table-hover table-bordered grvContentarea table-responsive ml-2" ShowHeader="false"
                                            AutoGenerateColumns="False" Width="809px">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Res Code">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lblgvDate" CssClass="btn btn-sm btn-primary" runat="server" Text='<%# (Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "dayid")).ToString("dd-MMM-yyyy")) %>'
                                                            Width="120px" OnClick="lblgvDate_Click"></asp:LinkButton>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                            </Columns>
                                            <HeaderStyle CssClass="grvHeader" />
                                        </asp:GridView>

                                    </div>

                                </div>
                            </asp:Panel>
                        </asp:View>
                        <asp:View ID="View3" runat="server">

                            <div class="row">
                                <div class="col-md-3 col-sm-3 col-lg-3 ">
                                    <div class="form-group">
                                        <asp:LinkButton ID="ImgbtnFindSupl2" runat="server" CssClass="label" OnClick="ImgbtnFindSupl2_Click">Supplier</asp:LinkButton>
                                        <asp:DropDownList ID="ddlSupl2" runat="server" CssClass="chzn-select form-control form-control-sm">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-1 col-sm-1 col-lg-1 ">
                                    <div class="form-group" style="margin-top: 20px;">
                                        <asp:LinkButton ID="lbtnSelectSupl2" runat="server" CssClass="btn btn-primary btn-sm pull-left" OnClick="lbtnSelectSupl2_Click">Ok</asp:LinkButton>
                                    </div>
                                </div>

                                <div class="col-md-3 col-sm-3 col-lg-3 ">

                                    <div class="form-group">
                                        <asp:Label ID="LaLinkButtonbel1" runat="server" CssClass="label">Requisition</asp:Label>
                                        <asp:DropDownList ID="ddlRequision" runat="server" CssClass="chzn-select form-control form-control-sm">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-1 col-sm-1 col-lg-1 ">
                                    <div class="form-group" style="margin-top: 20px;">
                                        <asp:LinkButton ID="lbtnreqsurv" runat="server" CssClass="btn btn-danger btn-sm pull-left" OnClick="lbtnreqsurv_Click">Survey</asp:LinkButton>
                                    </div>
                                </div>
                            </div>
                            <div class="row" id="Panel3" runat="server" visible="False">
                                <div class="col-md-3 col-sm-3 col-lg-3 ">
                                    <div class="form-group">
                                        <asp:LinkButton ID="ImgbtnFindRes2" runat="server" CssClass="label" OnClick="ImgbtnFindRes2_Click">Material List</asp:LinkButton>
                                        <asp:DropDownList ID="ddlResList2" runat="server" CssClass="chzn-select form-control form-control-sm" AutoPostBack="True" OnSelectedIndexChanged="ddlResList2_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-3 col-sm-3 col-lg-3 ">
                                    <div class="form-group">
                                        <asp:LinkButton ID="ImgbtnFindSpecification2" runat="server" CssClass="label" OnClick="ImgbtnFindSpecification2_Click">Specification</asp:LinkButton>
                                        <asp:DropDownList ID="ddlSpecification02" runat="server" CssClass="chzn-select form-control form-control-sm">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-3 col-sm-3 col-lg-3 ">

                                    <div class="form-group" style="margin-top: 20px;">
                                        <asp:LinkButton ID="lbtnSelectRes2" runat="server" CssClass="btn btn-primary btn-sm pull-left small" OnClick="lbtnSelectRes2_Click">Select</asp:LinkButton>
                                        <asp:LinkButton ID="lbtnSelectResAll" runat="server" CssClass="btn btn-primary btn-sm pull-left small" OnClick="lbtnSelectResAll_Click">Select All(Spec)</asp:LinkButton>
                                        <asp:LinkButton ID="lbtnSelectReaSpesAll" runat="server" CssClass="btn btn-primary btn-sm pull-left small" OnClick="lbtnSelectReaSpesAll_Click">Select All(Mat)</asp:LinkButton>

                                    </div>
                                </div>
                                <div class="col-md-1 col-sm-1 col-lg-1 ">
                                    <div class="form-group">
                                        <asp:LinkButton ID="btnCurr" runat="server" CssClass="label" OnClick="btnCurr_Click">Currency</asp:LinkButton>
                                        <asp:HyperLink ID="HyperLink1" runat="server" Target="_blank" NavigateUrl="~/F_21_GAcc/AccConversion" CssClass="fa fa-plus-circle"></asp:HyperLink>

                                        <asp:DropDownList ID="ddlCurrency" runat="server" CssClass="chzn-select form-control form-control-sm" AutoPostBack="True" OnSelectedIndexChanged="ddlCurrency_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-1 col-sm-1 col-lg-1 ">

                                    <div class="form-group">
                                        <asp:Label ID="Label3" runat="server" CssClass="label">Con. Rate</asp:Label>

                                        <asp:TextBox ID="lblConRate" runat="server" CssClass="form-control form-control-sm " ReadOnly="true"></asp:TextBox>
                                    </div>
                                </div>

                            </div>
                            <div class="row">

                                <asp:GridView ID="gvResInfo" runat="server" AllowPaging="True" OnRowDeleting="gvResInfo_RowDeleting" CssClass="table-striped table-hover table-bordered grvContentarea table-responsive"
                                    AutoGenerateColumns="False" OnPageIndexChanging="gvResInfo_PageIndexChanging"
                                    ShowFooter="True" Width="831px">
                                    <PagerSettings NextPageText="Next" PreviousPageText="Previous" Position="Bottom"
                                        Mode="NumericFirstLast" />

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
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="35px"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:CommandField ShowDeleteButton="True" ControlStyle-ForeColor="Red" DeleteText="<span class='fa fa-trash'></span>" />
                                        <asp:TemplateField HeaderText="Res Code" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvResCod1" runat="server" Height="16px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rsircode")) %>'
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
                                        <asp:TemplateField HeaderText="Description of Materials">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvResDesc1" runat="server"
                                                    Text='<%# "<B>" + Convert.ToString(DataBinder.Eval(Container.DataItem, "rsirdesc1")) + "</B>" +
                                                                         (DataBinder.Eval(Container.DataItem, "spcfdesc").ToString().Trim().Length>0 ? 
                                                                         (Convert.ToString(DataBinder.Eval(Container.DataItem, "rsirdesc1")).Trim().Length>0 ? "<br>" : "") + 
                                                                         "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + 
                                                                         Convert.ToString(DataBinder.Eval(Container.DataItem, "spcfdesc")).Trim(): "")   %>'
                                                    Width="350px">
                                                                    
                                                                    
                                                </asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                            </FooterTemplate>
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
                                        <asp:TemplateField HeaderText="Pro Position </br> Qty" Visible="false">

                                            <ItemTemplate>
                                                <asp:Label ID="lblgvpropqty" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                                    BorderWidth="0px" Font-Size="11px" Style="text-align: right; background-color: Transparent"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "propqty")).ToString("#,##0.0000;(#,##0.0000); ") %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle HorizontalAlign="Center" />
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Rate">
                                            <FooterTemplate>
                                                <%--<asp:LinkButton ID="lbtnResUpdate" runat="server" CssClass="btn btn-danger primaryBtn" OnClick="lbtnResUpdate_Click">Final Update</asp:LinkButton>--%>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtgvRate" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                                    BorderWidth="0px" Font-Size="11px" Style="text-align: right; background-color: Transparent"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "rate")).ToString("#,##0.000000;(#,##0.000000); ") %>'
                                                    Width="80px"></asp:TextBox>
                                            </ItemTemplate>
                                            <FooterStyle HorizontalAlign="Center" />
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Conv. Rate">

                                            <ItemTemplate>
                                                <asp:Label ID="lblgvconrate" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                                    BorderWidth="0px" Font-Size="11px" Style="text-align: right; background-color: Transparent"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "conrate")).ToString("#,##0.00;(#,##0.00); ") %>'
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
                                                <asp:TextBox ID="txtgvResRemarks1" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                                    BorderWidth="0px" Font-Size="11px" Style="text-align: left; background-color: Transparent"
                                                    Text='<%# DataBinder.Eval(Container.DataItem, "rmrks").ToString() %>' Width="150px"></asp:TextBox>
                                            </ItemTemplate>
                                            <FooterStyle HorizontalAlign="Center" />
                                            <HeaderStyle HorizontalAlign="Center" />
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

            </br></br>


        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
