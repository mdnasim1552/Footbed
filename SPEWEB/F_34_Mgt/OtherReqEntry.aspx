<%@ Page Title="" Language="C#" MasterPageFile="~/SPE.Master" AutoEventWireup="true" CodeBehind="OtherReqEntry.aspx.cs" Inherits="SPEWEB.F_34_Mgt.OtherReqEntry" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <style type="text/css" >
        .chzn-container-single .chzn-single {
            height: 30px;
        }

        .payment-type input {
            margin-top: 4px;
        }

        .payment-type label{
            margin-left: 6px;
            margin-right: 20px;
        }
    </style>

    <script type="text/javascript">
        $(document).ready(function () {
            //For navigating using left and right arrow of the keyboard
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);
        });
        function pageLoaded() {
            $('.chzn-select').chosen({ search_contains: true });
        };
    </script>
    <%--    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>--%>
    <div class="container-fluid">
        <div class="card card-fluid mb-1">
            <div class="card-body">
                <div class="row">
                    <div class="form-group col-md-1 pr-0">
                        <asp:Label ID="lblCurDate" runat="server" CssClass="col-form-label text-dark font-size-sm" Text="Req.Date"></asp:Label>
                        <asp:TextBox ID="txtCurReqDate" runat="server" AutoCompleteType="Disabled" CssClass="form-control form-control-sm"></asp:TextBox>
                        <cc1:CalendarExtender ID="txtCurReqDate_CalendarExtender" runat="server" Enabled="True" Format="dd.MM.yyyy" TargetControlID="txtCurReqDate"></cc1:CalendarExtender>
                    </div>
                    <div class="form-group col-md-2">
                        <asp:Label ID="lblCurNo" CssClass="col-form-label text-dark font-size-sm" runat="server" Text="Req. No."></asp:Label>
                        <div class="row mx-0">
                            <asp:Label ID="lblCurReqNo1" CssClass="col-4 form-control form-control-sm" runat="server" Width="">000</asp:Label>
                            <asp:TextBox ID="txtCurReqNo2" runat="server" CssClass="col-8 form-control form-control-sm" Width=""></asp:TextBox>
                        </div>
                    </div>
                    <div class="form-group col-md-2">
                        <asp:Label ID="lblmrfno" CssClass="col-form-label text-dark font-size-sm" runat="server">Source Ref.</asp:Label>
                        <asp:TextBox ID="txtMRFNo" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>                        
                    </div>
                    <div class="form-group col-md-1">
                        <asp:Label ID="Label6" CssClass="col-form-label text-dark font-size-sm" runat="server">Bill No</asp:Label>
                        <asp:TextBox ID="txtBillno" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
                    </div>
                    <div class="form-group col-md-1 pr-0">
                        <asp:Label ID="Label1" CssClass="col-form-label text-dark font-size-sm" runat="server">Bill Date</asp:Label>
                        <asp:TextBox ID="txtBillDate" runat="server" AutoCompleteType="Disabled" CssClass="form-control form-control-sm pr-0"></asp:TextBox>
                        <cc1:CalendarExtender ID="CalendarExtender2" runat="server" Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txtBillDate"></cc1:CalendarExtender>
                    </div>
                    <div class="form-group col-md-1">
                        <asp:LinkButton ID="lbtnOk" runat="server" CssClass="btn btn-sm btn-primary ml-2" style="margin-top:20px;" OnClick="lbtnOk_Click">Ok</asp:LinkButton>
                    </div>
                    <div class="form-group col-md-2">
                        <asp:TextBox ID="txtSrchMrfNo" runat="server" Visible="false" CssClass=" inputtextbox"></asp:TextBox>
                        <asp:LinkButton ID="lbtnPrevReqList" runat="server" CssClass="text-primary font-size-sm" OnClick="lbtnPrevReqList_Click">Req. List:</asp:LinkButton>
                        <asp:DropDownList ID="ddlPrevReqList" runat="server" Font-Bold="True" CssClass="form-control form-control-sm">
                        </asp:DropDownList>
                        <asp:Label ID="lblmsg1" runat="server" CssClass="btn btn-danger primaryBtn"></asp:Label>
                    </div>
                    
                    
                </div>

                <div class="row">
                    <div class="form-group col-md-1">
                        <asp:Label ID="Label5" CssClass="col-form-label text-dark font-size-sm" runat="server">Order No</asp:Label>
                        <asp:TextBox ID="txtOrdNo" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
                    </div>
                    <div class="form-group col-md-1 pr-0">
                        <asp:Label ID="Label4" CssClass="col-form-label text-dark font-size-sm" runat="server">Order Date</asp:Label>
                        <asp:TextBox ID="txtOrdDate" runat="server" AutoCompleteType="Disabled" CssClass="form-control form-control-sm pr-0"></asp:TextBox>
                        <cc1:CalendarExtender ID="CalendarExtender1" runat="server" Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txtOrdDate"></cc1:CalendarExtender>
                    </div>
                    <div class="form-group col-md-1">
                        <asp:Label ID="Label7" CssClass="col-form-label text-dark font-size-sm" runat="server">MRR No</asp:Label>
                        <asp:TextBox ID="txtMrrno" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
                    </div>
                        
                    <div class="form-group col-md-1 pr-0">
                        <asp:Label ID="Label8" CssClass="col-form-label text-dark font-size-sm" runat="server">MRR Date</asp:Label>
                        <asp:TextBox ID="TxtMrrDate" runat="server" AutoCompleteType="Disabled" CssClass="form-control form-control-sm pr-0"></asp:TextBox>
                        <cc1:CalendarExtender ID="CalendarExtender3" runat="server" Enabled="True" Format="dd-MMM-yyyy" TargetControlID="TxtMrrDate"></cc1:CalendarExtender>
                    </div>
                        
                    <div class="form-group col-md-2">
                        <asp:Label ID="Label9" CssClass="col-form-label text-dark font-size-sm" runat="server">Advance</asp:Label>
                        <asp:TextBox ID="txtAdvamt" runat="server" CssClass="form-control form-control-sm" style="text-align:right;"></asp:TextBox>
                    </div>
                </div>
            </div>
        </div>

        <div class="card card-fluid mt-1" style="height: 600px;">
            <div class="card-body">
                    <asp:Panel ID="Panel1" runat="server" Visible="False">
                        <asp:Panel ID="pnlnew" runat="server" Visible="False">

                            <div class="row">
                                <div class="col-md-3 form-group">
                                    <asp:LinkButton ID="lblProjectname" runat="server" OnClick="lnkAcccode_Click" CssClass="col-form-label text-dark font-size-sm" Text="Head of Account"></asp:LinkButton>
                                        <asp:HyperLink ID="HyperLink4" runat="server" Target="_blank" NavigateUrl="~/F_21_GAcc/AccCodeBook?InputType=Accounts" CssClass="fa fa-plus-circle"></asp:HyperLink>

                                    <asp:DropDownList ID="ddlProjectName" runat="server" Font-Bold="True" AutoPostBack="true" OnSelectedIndexChanged="ddlProjectName_SelectedIndexChanged" CssClass="form-control form-control-sm chzn-select" >
                                    </asp:DropDownList>
                                </div>
                                <div class="col-md-3 form-group">
                                    <asp:LinkButton ID="lblMatGroup" runat="server" OnClick="ImgbtnFindGroup_Click" CssClass="col-form-label text-dark font-size-sm" Text="Sub of Account"></asp:LinkButton>
                                    <asp:TextBox ID="txtResgroup" runat="server" TabIndex="2" CssClass="form-control form-control-sm" Visible="false"></asp:TextBox>
                                    <asp:LinkButton ID="ImgbtnFindGroup" runat="server" CssClass="btn btn-primary srearchBtn" Visible="false" ><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>
                                        <asp:HyperLink ID="HylSubAcc" runat="server" Target="_blank" NavigateUrl="~/F_21_GAcc/AccSubCodeBook?InputType=ResCodePrint" CssClass="fa fa-plus-circle"></asp:HyperLink>
                                    
                                    <div class="row mx-0">
                                        <asp:DropDownList ID="ddlMatGrp" runat="server" OnSelectedIndexChanged="ddlMatGrp_SelectedIndexChanged" AutoPostBack="true" CssClass="form-control form-control-sm chzn-select" Width="" TabIndex="7">
                                        </asp:DropDownList>
                                      
                                    </div>
                                </div>
                                 <div class="col-md-1 ">
                                    <div class="form-group">
                                        <div class="form-inline" style="padding-top:20px;">
                                            <asp:LinkButton ID="lbtnGroupSelect" runat="server" CssClass="btn btn-primary btn-sm" OnClick="lbtnGroupSelect_Click" ToolTip="Select Single"><span class="fa fa-check"></span></asp:LinkButton>
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <div class="row">

                                <div class="col-md-3 form-group" style="display:none">
                                    <asp:Label ID="lblResList" runat="server" Font-Size="11px" CssClass="lblName lblTxt" Text="Non Materials List:"></asp:Label>
                                    <asp:TextBox ID="txtResSearch" runat="server" TabIndex="2" CssClass="inputtextbox"></asp:TextBox>

                                    <asp:LinkButton ID="ImgbtnFindRes" runat="server" CssClass="btn btn-primary srearchBtn" OnClick="ImgbtnFindRes_Click"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>
                                </div>
                                <div class="col-md-3 form-group" style="display:none">
                                    <asp:DropDownList ID="ddlResList" runat="server"
                                        OnSelectedIndexChanged="ddlMatGrp_SelectedIndexChanged" CssClass="form-control inputTxt"
                                        TabIndex="7">
                                    </asp:DropDownList>
                                </div>
                                <div class="col-md-3 form-group" style="display:none">
                                    <asp:LinkButton ID="lbtnSelectRes" runat="server" CssClass="btn btn-primary primaryBtn" OnClick="lbtnSelectRes_Click">Select</asp:LinkButton>
                                </div>                                
                            </div>

                            <hr class="hrline" />
                        </asp:Panel>

                        
                        
                        

                        
                        <asp:Panel ID="pnlNarr" runat="server">
                            <div class="row px-2">
                                <div class="col-md-5 card bg-secondary">
                                    <div class="row">
                                        <div class="col-md-7 form-group">
                                            <asp:Label ID="Label2" CssClass="col-form-label text-dark font-size-sm" runat="server">Transaction Type</asp:Label>
                                            <asp:RadioButtonList runat="server" ID="rblpaytype" CssClass="payment-type" RepeatDirection="Horizontal" AutoPostBack="true" OnSelectedIndexChanged="rblpaytype_SelectedIndexChanged">
                                                <asp:ListItem Value="Payable">Payable</asp:ListItem>
                                                <asp:ListItem Value="Cash">Cash</asp:ListItem>
                                                <asp:ListItem Value="Cheque">Cheque</asp:ListItem>
                                                
                                            </asp:RadioButtonList>
                                        </div>
                                        <div class="col-md-5 form-group">
                                            <asp:Label ID="Label10" runat="server" CssClass="col-form-label text-dark font-size-sm" Text="Cheque/Ref No"></asp:Label>                                            
                                            <asp:TextBox ID="txtRefno" runat="server" class="form-control form-control-sm"></asp:TextBox>
                                           
                                        </div>
                                       
                                    </div>

                                    <div class="row">
                                        <div class="col-md-6 form-group">
                                            <asp:Label runat="server" CssClass="col-form-label text-dark font-size-sm" Text="Supplier Name"></asp:Label>
                                            <asp:DropDownList ID="ddlSupplier" runat="server" CssClass="form-control form-control-sm inputTxt  chzn-select" TabIndex="7"></asp:DropDownList>
                                        </div>

                                        <div class="col-md-6 form-group">
                                            <asp:Label runat="server" CssClass="col-form-label text-dark font-size-sm" Text="Bank"></asp:Label>
                                            <asp:DropDownList ID="ddlBankName" runat="server" CssClass="form-control form-control-sm inputTxt chzn-select" TabIndex="7"></asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="row">
                                     <div class="col-md-12 form-group">
                                            <asp:Label ID="Label3" runat="server" CssClass="col-form-label text-dark font-size-sm" Text="Pay To"></asp:Label>                                            
                                            <asp:TextBox ID="txtPayto" runat="server" class="form-control form-control-sm"></asp:TextBox>
                                            <cc1:AutoCompleteExtender ID="txtPayto_AutoCompleteExtender"
                                                runat="server" CompletionListCssClass="AutoExtender"
                                                CompletionListHighlightedItemCssClass="AutoExtenderHighlight"
                                                CompletionListItemCssClass="AutoExtenderList" CompletionSetCount="15"
                                                DelimiterCharacters="" Enabled="True" FirstRowSelected="True"
                                                MinimumPrefixLength="0" ServiceMethod="GetRecandPayDetails"
                                                ServicePath="~/AutoCompleted.asmx" TargetControlID="txtPayto">
                                            </cc1:AutoCompleteExtender>
                                        </div>

                                        </div>
                                    <div class="row">
                                        <div class="col-md-12 form-group">
                                            <asp:Label ID="lblReqNarr" runat="server" CssClass="col-form-label text-dark font-size-sm" Text="Narration:"></asp:Label>
                                            <asp:TextBox ID="txtReqNarr" runat="server" class="form-control" Rows="4" TextMode="MultiLine"></asp:TextBox>                                    
                                        </div>
                                    </div>                                

                                </div>

                                <div class="col-md-7 card px-3">
                                    <asp:GridView ID="gvOtherReq" runat="server" AutoGenerateColumns="False" CssClass="my-4 table-striped table-hover table-bordered grvContentarea"
                                        Width="542px" ShowFooter="True" OnRowDeleting="gvOtherReq_RowDeleting" OnRowCancelingEdit="gvOtherReq_RowCancelingEdit" OnRowEditing="gvOtherReq_RowEditing" OnRowUpdating="gvOtherReq_RowUpdating">
                                        <RowStyle />
                                        <Columns>
                                            <asp:CommandField ShowDeleteButton="True" ControlStyle-Width="20px" ItemStyle-HorizontalAlign="Center" ControlStyle-ForeColor="Red" DeleteText="<span class='fa fa-trash'></span>" />

                                            <asp:CommandField ShowEditButton="True" ControlStyle-Width="20px" ItemStyle-HorizontalAlign="Center" CancelText="<span class='fa fa-times'></span>" EditText="<span class='fa fa-pen'></span>" UpdateText="<span class='fa fa-save'></span>"/>

                                            <asp:TemplateField HeaderText="Sl">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvSlNo0" runat="server" Height="16px"
                                                        Style="text-align: right"
                                                        Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Res Code" Visible="False">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvResCod" runat="server"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rsircode")) %>'
                                                        Width="80px"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>


                                            <asp:TemplateField HeaderText="Account Description">

                                                <FooterTemplate>
                                                    <table style="width: 30%;">
                                                        <tr>
                                                            <td class="style77">
                                                                <%-- <asp:LinkButton ID="lbtnTotal" runat="server" OnClick="lbtnTotal_Click" CssClass="btn  btn-primary primarygrdBtn"
                                                                            >Total</asp:LinkButton>--%>
                                                            </td>
                                                            <td class="style65"></td>
                                                            <td>&nbsp;</td>
                                                        </tr>
                                                    </table>
                                                </FooterTemplate>

                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvactdesc" runat="server"
                                                        Text='<%# "<B>" + Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc")) + "</B>" +
                                                                                            (DataBinder.Eval(Container.DataItem, "sirdesc").ToString().Trim().Length>0 ? 
                                                                                            (Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc")).Trim().Length>0 ? "<br>" : "") + 
                                                                                            "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + 
                                                                                            Convert.ToString(DataBinder.Eval(Container.DataItem, "sirdesc")).Trim(): "")   %>'
                                                        Width="300px">  
                                                
                                                
                                                                ></asp:Label>
                                                </ItemTemplate>

                                                <EditItemTemplate>

                                                    <fieldset class="scheduler-border fieldset_A">

                                                        <div class="form-horizontal">

                                                            <div class="form-group">

                                                                <div class="col-md-3 pading5px asitCol3">
                                                                    <asp:Label ID="lblcontrolAccHead" runat="server" CssClass="lblTxt lblName">Accounts Head</asp:Label>
                                                                    <div class="col-md-3 pading5px">

                                                                        <asp:DropDownList ID="ddlgrdacccode" runat="server" CssClass="form-control chzn-select"
                                                                            TabIndex="28" Style="width: 213px;" AutoPostBack="True" OnSelectedIndexChanged="ddlgrdacccode_SelectedIndexChanged">
                                                                        </asp:DropDownList>
                                                                    </div>



                                                                    <%-- <asp:TextBox ID="txtgrdserceacc" runat="server" CssClass="inputtextbox" TabIndex="26"></asp:TextBox>--%>

                                                                    <%--<div class="colMdbtn">
                                                                    <asp:LinkButton ID="ibtngrdFindAccCode" runat="server" CssClass="btn btn-primary srearchBtn"  TabIndex="27"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>

                                                                </div>--%>

                                                            <

                                                            <%-- <div class="col-md-3 pading5px">
                                                

                                                            </div--%>>
                                                                </div>
                                                            </div>

                                                            <div class="form-group">

                                                                <div class="col-md-3 pading5px asitCol3">
                                                                    <asp:Label ID="lblgvreshead" runat="server" CssClass="lblTxt lblName">Details Head</asp:Label>

                                                                    <div class="col-md-3 pading5px">
                                                                        <asp:DropDownList ID="ddlrgrdesuorcecode" runat="server" CssClass="chzn-select"
                                                                            TabIndex="31" Style="width: 213px;">
                                                                        </asp:DropDownList>

                                                                    </div>
                                                                    <%--<asp:TextBox ID="txtgrdserresource" runat="server" CssClass="inputtextbox" TabIndex="29"></asp:TextBox>--%>


                                                                    <%--<div class="colMdbtn">
                                                                    <asp:LinkButton ID="ibtngrdFindResource" runat="server" CssClass="btn btn-primary srearchBtn"  TabIndex="30"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>

                                                                </div>--%>
                                                                </div>


                                                            </div>

                                                        </div>
                                                    </fieldset>
                                                </EditItemTemplate>

                                                <FooterStyle HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                            </asp:TemplateField>






                                            <asp:TemplateField HeaderText="Bgd. Amount" Visible="false">
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFBgdamt" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000" Width="70px"></asp:Label>
                                                </FooterTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvBgdAmt" runat="server" Style="text-align: right"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "bgdamt")).ToString("#,##0;-#,##0; ") %>'
                                                        Width="70px"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Right" />
                                                <FooterStyle HorizontalAlign="Right" />
                                                <HeaderStyle VerticalAlign="Top" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Paid Amount"  Visible="false">
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFPaidAmtxx" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000" Width="70px"></asp:Label>
                                                </FooterTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvPaidAmtxx" runat="server"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ppdamt")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="70px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Right" />
                                                <ItemStyle HorizontalAlign="Right" />
                                                <FooterStyle HorizontalAlign="Right" />
                                                <HeaderStyle VerticalAlign="Top" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Budget Allocation" Visible="false">
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFPaidamt" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000" Width="70px"></asp:Label>
                                                </FooterTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvPaAmt" runat="server"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "trnamt")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="70px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Right" />
                                                <ItemStyle HorizontalAlign="Right" />
                                                <FooterStyle HorizontalAlign="Right" />
                                                <HeaderStyle VerticalAlign="Top" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Bal. Amount"  Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvBalAmt" runat="server"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "balamt")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="70px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFBalamt" runat="server" ForeColor="#000"
                                                        Width="70px" Font-Bold="True" Font-Size="12px"></asp:Label>
                                                </FooterTemplate>
                                                <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Right" />
                                                <ItemStyle HorizontalAlign="Right" />
                                                <HeaderStyle VerticalAlign="Top" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="QTY">

                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtgvQtamt" runat="server" BackColor="Transparent" Font-Size="11px"
                                                        BorderStyle="None" Style="text-align: right"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                        Width="70px"></asp:TextBox>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Right" />

                                                <HeaderStyle VerticalAlign="Top" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Rate">

                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtgvRate" runat="server" BackColor="Transparent" Font-Size="11px"
                                                        BorderStyle="None" Style="text-align: right"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "rate")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                        Width="70px"></asp:TextBox>
                                                </ItemTemplate>
                                                <ItemStyle Width="70px" HorizontalAlign="Right" />

                                                <HeaderStyle VerticalAlign="Top" />
                                            </asp:TemplateField>



                                            <asp:TemplateField HeaderText="Proposed Amount">
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFProposedamt" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000" Width="70px"></asp:Label>
                                                </FooterTemplate>
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtgvProposedamt" runat="server" BackColor="Transparent" Font-Size="11px"
                                                        BorderStyle="None" Style="text-align: right"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "proamt")).ToString("#,##0.00;-#,##0.00; ") %>'
                                                        Width="70px"></asp:TextBox>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Right" />
                                                <FooterStyle HorizontalAlign="Right" />
                                                <HeaderStyle VerticalAlign="Top" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Approved Amount">
                                                <FooterTemplate>
                                                    <asp:Label ID="lgvFAppamt" runat="server" Font-Bold="True" Font-Size="12px"
                                                        ForeColor="#000" Width="70px"></asp:Label>
                                                </FooterTemplate>
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtgvApamt" runat="server" BackColor="White" Font-Size="11px"
                                                        BorderStyle="None" Style="text-align: right"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "appamt")).ToString("#,##0.00;-#,##0.00; ") %>'
                                                        Width="70px"></asp:TextBox>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Right" />
                                                <FooterStyle HorizontalAlign="Right" />
                                                <HeaderStyle VerticalAlign="Top" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Bill/Chalan No" Visible="true">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="lblgvBillno" runat="server" BackColor="Transparent"
                                                        BorderColor="Transparent" BorderStyle="None" BorderWidth="1px"
                                                        CssClass="GridTextboxL" Font-Size="12px"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "billno")) %>'
                                                        Width="100px" ForeColor="Black" TabIndex="99"></asp:TextBox>
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
                            </div>
                            
                        </asp:Panel>
                        
                        <asp:Panel runat="server" ID="pnlAttacDeocx">
                            <div class="row">
                                <div class="col-md-3 card bg-secondary py-4">

                                    <div id="dropzone" class="fileinput-dropzone">
                                        <span>click to upload.</span>
                                        <cc1:AsyncFileUpload OnClientUploadError="uploadError"
                                            OnClientUploadComplete="uploadComplete" runat="server"
                                            ID="AsyncFileUpload1" UploaderStyle="Modern"
                                            ThrobberID="imgLoader"
                                            OnUploadedComplete="FileUploadComplete" />
                                    </div>
                                    <%--<cc1:AsyncFileUpload OnClientUploadError="uploadError"
                                        OnClientUploadComplete="uploadComplete" runat="server"
                                        ID="AsyncFileUpload1" UploaderStyle="Modern"
                                        CompleteBackColor=""
                                        UploadingBackColor="" ThrobberID="imgLoader"
                                        OnUploadedComplete="FileUploadComplete" />--%>
                                    <%--<asp:Image ID="imgLoader" CssClass="" runat="server" Visible="false" ImageUrl="~/images/Wait.gif" />--%>
                                    <br />
                                    <asp:Label ID="lblMesg" runat="server" Text=""></asp:Label>

                                </div>
                                <div class="col-md-9">
                                    <div class="card">
                                        <div class="card-header bg-primary">
                                            <div class="row justify-content-between">
                                                <div class="text-light">
                                                    <i class="fa fa-images mr-3"></i> Uploaded Files
                                                </div>
       
                                                <div class="pull-right">
                                                    <asp:Button ID="btnShowimg" runat="server" CssClass="btn btn-success btn-xs" Text="Show Image" OnClick="btnShowimg_OnClick" />
                                                    <asp:LinkButton ID="btnDelall" runat="server" OnClick="btnDelall_OnClick"  CssClass=" btn btn-xs btn-danger">Delete</asp:LinkButton>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="card-body" style="height:86px;">
                                            <div class="row">
                                                <div class="col-lg-12">
                                                    <asp:ListView ID="ListViewEmpAll" runat="server" ItemPlaceholderID="itemplaceholder" OnItemDataBound="ListViewEmpAll_ItemDataBound">
                                                        <LayoutTemplate>
                                                            <asp:PlaceHolder ID="itemplaceholder" runat="server" />
                                                        </LayoutTemplate>
                                                        <ItemTemplate>
                                                            <div class="col-xs-12 col-sm-4 col-md-2 listDiv" style="padding: 0 5px;">
                                                                <div id="EmpAll" runat="server">

                                                                    <asp:Label ID="ImgLink" Visible="False" runat="server" Text='<%# Eval("itemsurl") %>'></asp:Label>
                                                                    <asp:Label ID="reqno" Visible="False" runat="server" Text='<%# Eval("reqno") %>'></asp:Label>

                                                                    <a href="../Upload/ReqDoc/<%# Eval("itemsurl") %>" class="uploadedimg" target="_blank">
                                                                        <asp:Image ID="GetImg" runat="server" CssClass="image img img-responsive img-thumbnail" />
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
                        </asp:Panel>
                    </asp:Panel>
            </div>
        </div>        
    </div>

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
    <%-- </ContentTemplate>
    </asp:UpdatePanel>--%>
</asp:Content>

