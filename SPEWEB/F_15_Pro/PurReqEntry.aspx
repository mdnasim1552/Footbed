<%@ Page Title="" Language="C#" MasterPageFile="~/SPEMaster.Master" AutoEventWireup="true" CodeBehind="PurReqEntry.aspx.cs" Inherits="SPEWEB.F_15_Pro.PurReqEntry" %>

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
                k1.textBoxHandler(event);
            });


            $('.chzn-select').chosen({ search_contains: true });

        }


    </script>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>

            <div class="container moduleItemWrpper">
                <div class="contentPart">
                    <div class="row">
                        <asp:Panel ID="Panel3" runat="server">
                            <fieldset class="scheduler-border fieldset_A">

                                <div class="form-horizontal">
                                    <div class="form-group">
                                        <div class="col-md-1 pading5px ">
                                            <%--<asp:Label ID="Label6" runat="server" CssClass="lblTxt lblName" Text="Project Name"></asp:Label>--%>
                                            <asp:TextBox ID="txtProjectSearch" runat="server" CssClass=" inputtextbox hidden" TabIndex="1"></asp:TextBox>
                                            <div class="colMdbtn">
                                                <asp:LinkButton ID="ImgbtnFindProjectName" CssClass="lblTxt lblName" Text="Project Name" runat="server" OnClick="ImgbtnFindProjectName_Click" TabIndex="2"></asp:LinkButton>
                                            </div>
                                        </div>

                                        <div class="col-md-4 pading5px ">
                                            <div class="ddlListPart">
                                                <asp:DropDownList ID="ddlProject" runat="server" CssClass="chzn-select form-control inputTxt" AutoPostBack="True" OnSelectedIndexChanged="ddlProject_SelectedIndexChanged" TabIndex="3"></asp:DropDownList>
                                            </div>
                                            <asp:Label ID="lblddlProject" runat="server" Visible="False" CssClass="inputlblVal" Width="337"></asp:Label>


                                        </div>
                                        <div class="col-md-3 pading5px ">

                                            <asp:Label ID="lblCurDate" runat="server" CssClass="lblTxt lblDate " Text="Req.Date"></asp:Label>

                                            <asp:TextBox ID="txtCurReqDate" runat="server" CssClass="inputtextbox" TabIndex="5" ToolTip="(dd.mm.yyyy)"></asp:TextBox>
                                            <cc1:CalendarExtender ID="txtCurReqDate_CalendarExtender" runat="server"
                                                Format="dd.MM.yyyy" TargetControlID="txtCurReqDate"></cc1:CalendarExtender>

                                            <asp:LinkButton ID="lbtnOk" runat="server" Text="Ok" OnClick="lbtnOk_Click" CssClass="btn btn-primary okBtn" TabIndex="4"></asp:LinkButton>

                                        </div>
                                        <div class="col-md-4 pading5px   pull-right">
                                            <%--<asp:Label ID="lblpreReq" runat="server" CssClass=" smLbl_to" Text="Req. List"></asp:Label>--%>
                                            <asp:TextBox ID="txtSrchMrfNo" runat="server" TabIndex="7" CssClass=" inputtextbox hidden"></asp:TextBox>
                                            <asp:LinkButton ID="lblpreReq" runat="server" Text="Req. List" Style="float: left; margin-right:5px;" OnClick="ImgbtnFindReq_Click" TabIndex="10"></asp:LinkButton>
                                            <asp:DropDownList ID="ddlPrevReqList" runat="server" CssClass="chzn-select form-control inputTxt" Width="300px" TabIndex="11">
                                            </asp:DropDownList>

                                        </div>
                                       
                                    </div>
                                    <div class="form-group">
                                        <div class="col-md-1 pading5px ">
                                            <%--<asp:Label ID="lblCurNo" runat="server" CssClass="lblTxt lblName" Text="Requisition No."></asp:Label>--%>
                                            <asp:TextBox ID="txtReqText" runat="server" TabIndex="5" CssClass="inputtextbox hidden" ></asp:TextBox>

                                            <asp:LinkButton ID="lblCurNo" CssClass="lblTxt lblName" Text="Requisition No." runat="server" OnClick="ImgbtnReqse_Click" TabIndex="6"></asp:LinkButton>

                                        </div>
                                        <div class="col-md-4 pading5px ">
                                            <asp:Label ID="lblCurReqNo1" runat="server" CssClass="smLbl" Text="REQ00"></asp:Label>

                                            <asp:TextBox ID="txtCurReqNo2" runat="server" CssClass="xsDropDow inputTxt disabled readonlyValue" ReadOnly="True" TabIndex="8">00000</asp:TextBox>
                                            <asp:Label ID="lblmrfno" runat="server" CssClass=" smLbl_to" Text="M.R.F. No."></asp:Label>
                                            <asp:TextBox ID="txtMRFNo" runat="server" TabIndex="7" CssClass="inputtextbox" Style="width: 120px;"></asp:TextBox>
                                        </div>
                                         
                                        
                                    </div>
                                </div>
                            </fieldset>
                        </asp:Panel>
                        <asp:Panel ID="Panel1" runat="server">
                            <fieldset class="scheduler-border fieldset_A">

                                <div class="form-horizontal">
                                    <div class="form-group">
                                        <div class="col-md-1 pading5px">
                                            
                                            <asp:TextBox ID="txtResSearch" runat="server" CssClass=" inputtextbox hidden" TabIndex="1"></asp:TextBox>
                                            <div class="colMdbtn">
                                                <asp:LinkButton ID="lblResList" CssClass="lblTxt lblName" Text="Materials List" runat="server" OnClick="ImgbtnFindRes_Click" TabIndex="2"></asp:LinkButton>
                                            </div>


                                        </div>
                                        <div class="col-md-2 pading5px asitCol2">

                                            <asp:DropDownList ID="ddlResList" runat="server" CssClass="form-control inputTxt chzn-select" AutoPostBack="True" OnSelectedIndexChanged="ddlResList_SelectedIndexChanged" TabIndex="3"></asp:DropDownList>

                                            <asp:Label ID="Label2" runat="server" Visible="False" CssClass="dataLblview label txtAlgLeft"></asp:Label>


                                        </div>
                                        <div class="col-md-1 pading5px ">

                                           
                                                         <asp:LinkButton ID="lblSpecification" CssClass="lblTxt lblDate " Text="Specification" runat="server" OnClick="lbtnResSpcf_Click" TabIndex="2"></asp:LinkButton>



                                        </div>
                                        <div class="col-md-2 pading5px">
                                            
                                            <asp:DropDownList ID="ddlResSpcf" runat="server" CssClass="inputTxt chzn-select" AutoPostBack="True" OnSelectedIndexChanged="ddlResList_SelectedIndexChanged" TabIndex="3"></asp:DropDownList>

                                          
                                           
                                        </div>
                                        <div class="col-md-2">
                                              <asp:LinkButton ID="lbtnSelectRes" runat="server" OnClick="lbtnSelectRes_Click" CssClass="btn btn-primary checkBox">Select</asp:LinkButton>
                                            <asp:LinkButton ID="lnkSelectAll" runat="server" CssClass="btn btn-primary primaryBtn" OnClick="lnkSelectAll_Click">Select All</asp:LinkButton>

                                        </div>

                                         <asp:Label ID="lblfloor" runat="server" CssClass="col-md-1 label pading5px" Visible="False"></asp:Label>
                                         <asp:Label ID="lblddlFloor" runat="server" CssClass="col-md-1 label pading5px" Visible="False"></asp:Label>
                                        <div class="ddlListPart">
                                            <asp:DropDownList ID="ddlFloor" runat="server" AutoPostBack="True" TabIndex="15" Visible="False" Width="120px"></asp:DropDownList>
                                        </div>
                                       
                                    </div>




                                </div>
                            </fieldset>


                            
                        </asp:Panel>


                         <asp:Panel ID="Panel2" runat="server" Visible="False">
                            <div class=" table table-responsive">
                                <asp:GridView ID="gvReqInfo"  runat="server" AllowPaging="True" 
                                            AutoGenerateColumns="False" onrowdeleting="gvReqInfo_RowDeleting" PageSize="15" ShowFooter="True" CssClass="table-striped table-hover table-bordered grvContentarea">
                                    <PagerSettings Visible="False" />
                                    <RowStyle/>
                                    <Columns>
                                                <asp:TemplateField HeaderText="Sl.No.">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvSlNo0" runat="server" Height="16px" 
                                                            style="text-align: center" 
                                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="35px"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:CommandField ShowDeleteButton="True" ControlStyle-CssClass="btn btn-xs btn-danger" DeleteText="&lt;span class=&quot;glyphicon glyphicon-remove &quot;&gt;&lt;/span&gt;" />
                                                <asp:TemplateField HeaderText="Res Code" Visible="False">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvResCod" runat="server" Height="16px" 
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rsircode")) %>' 
                                                            Width="80px"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Spcf Code" Visible="False">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvSpcfCod" runat="server" Height="16px" 
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "spcfcod")) %>' 
                                                            Width="80px"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Description of Materials">
                                                    <FooterTemplate>
                                                        <asp:DropDownList ID="ddlPageNo" runat="server" __designer:wfdid="w67" 
                                                            AutoPostBack="True" Font-Bold="True" Font-Size="14px" 
                                                            onselectedindexchanged="ddlPageNo_SelectedIndexChanged" 
                                                            style="BORDER-RIGHT: navy 1px solid; BORDER-TOP: navy 1px solid; BORDER-LEFT: navy 1px solid; BORDER-BOTTOM: navy 1px solid" 
                                                            Width="150px">
                                                        </asp:DropDownList>
                                                    </FooterTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvResDesc" runat="server" 
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rsirdesc1")) %>' 
                                                            Width="220px"></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterStyle HorizontalAlign="Center" />
                                                    <HeaderStyle HorizontalAlign="Left" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Specification">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvSpfDesc" runat="server" 
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "spcfdesc")) %>' 
                                                            Width="80px"></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterStyle HorizontalAlign="Center" />
                                                    <HeaderStyle HorizontalAlign="Left" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Unit">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvResUnit" runat="server" 
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rsirunit")) %>' 
                                                            Width="35px"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Bgd.Balance">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvBgdBal" runat="server" style="text-align: right" 
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "bbgdqty")).ToString("#,##0.00;-#,##0.00; ") %>' 
                                                            Width="70px"></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Req. Qty">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtgvReqQty" runat="server" BorderColor="#99CCFF" 
                                                            BorderStyle="Solid" BorderWidth="1px" Font-Size="11px" 
                                                            style="text-align: right; background-color: Transparent" 
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "preqty")).ToString("#,##0.00;(#,##0.00); ") %>' 
                                                            Width="70px"></asp:TextBox>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Approv.Qty">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtgvappQty" runat="server" BackColor="White" 
                                                            BorderStyle="None" style="text-align: right" 
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "areqty")).ToString("#,##0.00;(#,##0.00); ") %>' 
                                                            Width="80px"></asp:TextBox>
                                                    </ItemTemplate>
                                                     <FooterTemplate>
                                                       
                                                    </FooterTemplate>
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                               

                                                <asp:TemplateField HeaderText="FC Rate">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtgvFCRate" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                                            BorderWidth="0px" Font-Size="11px" Style="text-align: right; background-color: Transparent"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "fcrate")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                            Width="60px"></asp:TextBox>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        
                                                    </FooterTemplate>
                                                    <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Right" />
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </asp:TemplateField>


                                                <asp:TemplateField HeaderText="Conversion Rate">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtgvConRate" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                                            BorderWidth="0px" Font-Size="11px" Style="text-align: right; background-color: Transparent"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "conrate")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                            Width="60px"></asp:TextBox>
                                                    </ItemTemplate>  
                                                     <FooterTemplate>
                                                       
                                                    </FooterTemplate>                                                 
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </asp:TemplateField>

                                                 <asp:TemplateField HeaderText="App. Rate">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvResRat" runat="server" BorderColor="#99CCFF" 
                                                            BorderStyle="Solid" BorderWidth="0px" Font-Size="11px" 
                                                            style="text-align: right; background-color: Transparent" 
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "reqrat")).ToString("#,##0.00;(#,##0.00); ") %>' 
                                                            Width="70px"></asp:Label>
                                                    </ItemTemplate>
                                                    
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </asp:TemplateField>


                                                <asp:TemplateField HeaderText="Req. Amount">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvTResAmt" runat="server" 
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "preqamt")).ToString("#,##0.00;(#,##0.00); ") %>' 
                                                            Width="80px"></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label ID="lblgvFooterTReqAmt" runat="server" ForeColor="#000" 
                                                            Width="80px"></asp:Label>
                                                    </FooterTemplate>
                                                    <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Right" />
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Appr. Amount">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvTAprAmt" runat="server" 
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "areqamt")).ToString("#,##0.00;(#,##0.00); ") %>' 
                                                            Width="80px"></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label ID="lblgvFooterTAprAmt" runat="server" ForeColor="#000" 
                                                            Width="80px"></asp:Label>
                                                    </FooterTemplate>
                                                    <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Right" />
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Exp.Use.Date" Visible="false">
                                                   
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtgvUseDat" runat="server" BorderColor="#99CCFF" 
                                                            BorderStyle="Solid" BorderWidth="0px" Font-Size="11px" 
                                                            style="text-align: left; background-color: Transparent" 
                                                            Text='<%# DataBinder.Eval(Container.DataItem, "expusedt").ToString() %>' 
                                                            Width="80px"></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Stock Qty" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtgvStokQty" runat="server" BorderColor="#99CCFF" 
                                                            BorderStyle="Solid" BorderWidth="0px" Font-Size="11px" 
                                                            style="text-align: right; background-color: Transparent" 
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "pstkqty")).ToString("#,##0.00;(#,##0.00); ") %>' 
                                                            Width="70px"></asp:TextBox>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Remarks" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtgvReqNote" runat="server" BorderColor="#99CCFF" 
                                                            BorderStyle="Solid" BorderWidth="0px" Font-Size="11px" 
                                                            style="text-align: left; background-color: Transparent" 
                                                            Text='<%# DataBinder.Eval(Container.DataItem, "reqnote").ToString() %>' 
                                                            Width="100px"></asp:TextBox>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Left" />
                                                </asp:TemplateField>
                                            </Columns>
                                    <FooterStyle CssClass="grvFooter" />
                                <EditRowStyle />
                                <AlternatingRowStyle />
                                <PagerStyle CssClass="gvPagination" />
                                <HeaderStyle CssClass="grvHeader" />
                                </asp:GridView>
                            </div>
                            <fieldset class="scheduler-border fieldset_D">
                                <div class="form-horizontal">

                                    <div class="form-group">
                                        <div class="col-md-6 pading5px" style="display: none;">
                                            <div class="input-group">
                                                <span class="input-group-addon glypingraddon">
                                                    <asp:Label ID="lblReqNarr" runat="server" CssClass="lblTxt" Text="Narration:"></asp:Label>
                                                </span>
                                                <asp:TextBox ID="txtReqNarr" runat="server" class="form-control" Rows="2" TextMode="MultiLine"></asp:TextBox>
                                            </div>
                                        </div>

                                        <div class="col-md-4 pading5px">
                                            <div class="input-group">
                                                <asp:HyperLink ID="lnkCreateMat" runat="server" CssClass="btn btn-warning primaryBtn" Visible="false"
                                                    NavigateUrl="~/F_17_Acc/AccSubCodeBook.aspx?InputType=Res" Target="_blank">Create Material</asp:HyperLink>


                                            </div>
                                        </div>

                                        <div class="clearfix"></div>
                                    </div>

                                    <div class="form-group">
                                        <div class="col-md-3 pading5px">
                                            <div class="input-group">
                                                <span class="input-group-addon glypingraddon">

                                                    <asp:Label ID="lblPreparedBy" runat="server" CssClass="lblTxt" Text="Prepared By:" Visible="False"></asp:Label>
                                                </span>
                                                <asp:TextBox ID="txtPreparedBy" runat="server" Visible="False" CssClass="form-control inputTxt"></asp:TextBox>

                                            </div>
                                        </div>
                                        <div class="col-md-3 pading5px">
                                            <div class="input-group">
                                                <span class="input-group-addon glypingraddon">
                                                    <asp:Label ID="lblApprovedBy" runat="server" CssClass="lblTxt" Text="Approved By:" Visible="False"></asp:Label>
                                                </span>
                                                <asp:TextBox ID="txtApprovedBy" runat="server" Visible="False" CssClass="form-control inputTxt"></asp:TextBox>

                                            </div>
                                        </div>
                                        <div class="col-md-3 pading5px">
                                            <div class="input-group">
                                                <span class="input-group-addon glypingraddon">
                                                    <asp:Label ID="lblApprovalDate" runat="server" CssClass="lblTxt" Text="Approv.Date:" Visible="False"></asp:Label>
                                                </span>
                                                <asp:TextBox ID="txtApprovalDate" runat="server" CssClass="form-control inputTxt" ToolTip="(dd.mm.yyyy)" Visible="False"></asp:TextBox>

                                            </div>
                                        </div>
                                        <div class="col-md-3 pading5px">
                                            <div class="input-group">
                                                <span class="input-group-addon glypingraddon">
                                                    <asp:Label ID="lblExpDeliveryDate" runat="server" CssClass="lblTxt" Text="Exp.Del. Date:" Visible="False"></asp:Label>
                                                </span>
                                                <asp:TextBox ID="txtExpDeliveryDate" runat="server" CssClass="form-control inputTxt" ToolTip="(dd.mm.yyyy)" Visible="False"></asp:TextBox>

                                            </div>
                                        </div>

                                        <div class="clearfix"></div>
                                    </div>

                                </div>

                            </fieldset>
                        </asp:Panel>
                        


                    </div>
                </div>
            </div>

            <%--<asp:Panel ID="Panel3" runat="server" BorderColor="Yellow" BorderStyle="Solid" 
                BorderWidth="1px">
                    
            <table style="width:900px" >
                <tr>
                   
                    <td class="style78">
                        <asp:Label ID="Label6" runat="server" CssClass="style15" Font-Bold="True" 
                            Font-Size="12px" style="TEXT-ALIGN: left" Text="Order Name:" 
                            Width="83px"></asp:Label>
                    </td>
                    <td class="style42">
                        <asp:TextBox ID="txtProjectSearch" runat="server" BorderStyle="None" 
                            Height="18px" style="margin-left: 0px" Width="80px"></asp:TextBox>
                    </td>
                    <td class="style34" align="right">
                        <asp:ImageButton ID="ImgbtnFindProjectName" runat="server" Height="19px" 
                            ImageUrl="~/Image/find_images.jpg" Width="16px" 
                            onclick="ImgbtnFindProjectName_Click" TabIndex="1" />
                    </td>
                    <td class="style43" colspan="4">
                        <asp:DropDownList ID="ddlProject" runat="server" 
                            style="BORDER-RIGHT: midnightblue 1px solid; BORDER-TOP: midnightblue 1px solid; BORDER-LEFT: midnightblue 1px solid; BORDER-BOTTOM: midnightblue 1px solid; BACKGROUND-COLOR: #fffbf1" 
                            Width="300px" AutoPostBack="True" 
                            onselectedindexchanged="ddlProject_SelectedIndexChanged" TabIndex="2">
                        </asp:DropDownList>
                        <asp:Label ID="lblddlProject" runat="server" __designer:wfdid="w4" 
                            BackColor="White" Font-Bold="True" Font-Size="14px" ForeColor="Maroon" 
                            style="FONT-SIZE: 12px; TEXT-ALIGN: left" Visible="False" Width="300px"></asp:Label>
                    </td>
                    <td class="style34" align="right" width="85px">
                        <asp:Label ID="lblCurDate" runat="server" Font-Bold="True" Font-Size="12px" 
                            Height="16px" style="TEXT-ALIGN: right" Text="Req.Date:" 
                            CssClass="style15"></asp:Label>
                    </td>
                    <td width="125px">
                        <asp:TextBox ID="txtCurReqDate" runat="server" BorderStyle="Solid" 
                            BorderWidth="1px" Font-Bold="True" Font-Size="12px" ToolTip="(dd.mm.yyyy)" 
                            Width="124px" TabIndex="3"></asp:TextBox>
                        <cc1:CalendarExtender ID="txtCurReqDate_CalendarExtender" runat="server" 
                            Format="dd.MM.yyyy" TargetControlID="txtCurReqDate">
                        </cc1:CalendarExtender>
                    </td>
                    <td class="style47">
                        <asp:LinkButton ID="lbtnOk" runat="server" Font-Bold="True" Font-Size="16px" 
                            Height="23px" onclick="lbtnOk_Click" 
                            style="text-align: center; background-color: #99FFCC" Width="52px" 
                            TabIndex="4">Ok</asp:LinkButton>
                    </td>
                    <td>
                        &nbsp;</td>
                    <td class="style46">
                        <asp:Label ID="lblLastReqNo4" runat="server" Font-Bold="True" Font-Size="12px" 
                            style="TEXT-ALIGN: right" Text="" Width="80px"></asp:Label>
                    </td>
                    <td class="style19">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td class="style78">
                        <asp:Label ID="lblCurNo" runat="server" CssClass="style15" Font-Bold="True" 
                            Font-Size="12px" Height="16px" style="TEXT-ALIGN: left" Text=" Req. No.:" 
                            Width="83px"></asp:Label>
                    </td>
                    <td class="style42">
                        <asp:TextBox ID="txtReqText" runat="server" BorderStyle="None" 
                            Height="18px" style="margin-left: 0px" Width="80px" TabIndex="5"></asp:TextBox>
                    </td>
                    <td class="style34" align="right">
                        <asp:ImageButton ID="ImgbtnReqse" runat="server" Height="19px" 
                            ImageUrl="~/Image/find_images.jpg" Width="16px" 
                            onclick="ImgbtnReqse_Click" TabIndex="6" />
                    </td>
                    <td class="style67">
                        <asp:Label ID="lblCurReqNo1" runat="server" Font-Bold="True" Font-Size="12px" 
                            style="border: 1px solid #000000; padding: 1px 4px; TEXT-ALIGN: right; background-color: #FFFFFF;" 
                            Text="REQ00-" Width="45px"></asp:Label>
                    </td>
                    <td class="style66">
                        <asp:TextBox ID="txtCurReqNo2" runat="server" BorderStyle="Solid" 
                            BorderWidth="1px" Font-Bold="True" Font-Size="12px" ReadOnly="True" 
                            Width="45px" TabIndex="7">00000</asp:TextBox>
                    </td>
                    <td>
                        <asp:Label ID="lblmrfno" runat="server" CssClass="style15" Font-Bold="True" 
                            Font-Size="12px" style="TEXT-ALIGN: right" Text="M.R.F. No.:" Width="70px"></asp:Label>
                    </td>
                    <td class="style65">
                        <asp:TextBox ID="txtMRFNo" runat="server" BorderStyle="Solid" BorderWidth="1px" 
                            Font-Bold="True" Font-Size="12px" Width="105px" TabIndex="8"></asp:TextBox>
                    </td>
                    <td class="style36">
                        <asp:CheckBox ID="chkdupMRF" runat="server" BackColor="#000066" 
                            BorderColor="Yellow" BorderStyle="Solid" BorderWidth="1px" Font-Bold="True" 
                            Font-Size="12px" ForeColor="Yellow" Text="Dup.M.R.F" Width="80px" 
                            TabIndex="9" />
                    </td>
                    <td width="125px">
                        <asp:CheckBox ID="chkneBudget" runat="server" BackColor="#000066" 
                            BorderColor="Yellow" BorderStyle="Solid" BorderWidth="1px" Font-Bold="True" 
                            Font-Size="12px" ForeColor="Yellow" Text="Not Exceed Budget" Width="125px" 
                            TabIndex="10" />
                    </td>
                    <td>
                        &nbsp;</td>
                    <td class="style46">
                        &nbsp;</td>
                    <td class="style19">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td class="style78">
                        <asp:Label ID="lblpreReq" runat="server" CssClass="style15" Font-Bold="True" 
                            Font-Size="12px" Height="16px" style="TEXT-ALIGN: left" Text="Req. List:" 
                            Width="83px"></asp:Label>
                    </td>
                    <td class="style42">
                        <asp:TextBox ID="txtSrchMrfNo" runat="server" BorderStyle="None" 
                            Font-Bold="True" Font-Size="12px" Width="80px" TabIndex="11"></asp:TextBox>
                    </td>
                    <td class="style34" align="right">
                        <asp:ImageButton ID="ImgbtnFindReq" runat="server" Height="19px" 
                            ImageUrl="~/Image/find_images.jpg" onclick="ImgbtnFindReq_Click" 
                            Width="16px" TabIndex="12" />
                    </td>
                    <td class="style43" colspan="4">
                        <asp:DropDownList ID="ddlPrevReqList" runat="server" Font-Size="12px" 
                            Width="300px" TabIndex="13">
                        </asp:DropDownList>
                    </td>
                    <td class="style34" colspan="2">
                        <asp:Label ID="lblmsg1" runat="server" __designer:wfdid="w4" BackColor="Red" 
                            Font-Bold="True" Font-Size="12px" ForeColor="#000" Height="18px" 
                            style="FONT-SIZE: 12px; TEXT-ALIGN: left"></asp:Label>
                        <asp:Label ID="lblprintstk" runat="server"></asp:Label>
                    </td>
                    <td class="style47">
                    </td>
                    <td>
                        <asp:Label ID="lblfloor" runat="server" CssClass="style15" Font-Bold="True" 
                            Font-Size="12px" style="TEXT-ALIGN: right" Text="Floor:" Visible="False" 
                            Width="80px"></asp:Label>
                    </td>
                    <td class="style46">
                        <asp:DropDownList ID="ddlFloor" runat="server" AutoPostBack="True" 
                            style="text-transform:capitalize; BORDER-RIGHT: midnightblue 1px solid; BORDER-TOP: midnightblue 1px solid; BORDER-LEFT: midnightblue 1px solid; BORDER-BOTTOM: midnightblue 1px solid; BACKGROUND-COLOR: #fffbf1" 
                            Visible="False" Width="120px" TabIndex="14">
                        </asp:DropDownList>
                        <asp:Label ID="lblddlFloor" runat="server" __designer:wfdid="w4" 
                            BackColor="White" Font-Bold="True" Font-Size="14px" ForeColor="Maroon" 
                            style="text-transform:capitalize;FONT-SIZE: 12px; TEXT-ALIGN: left" Visible="False" Width="120px"></asp:Label>
                    </td>
                    <td class="style19">
                    </td>
                </tr>
                
            </table>
            </asp:Panel>--%>

            <%--<table style="width:100%;">
                <tr>
                    <td class="style18" colspan="13">
                        <asp:Panel ID="Panel1" runat="server" BorderColor="Yellow" BorderStyle="Solid" 
                    BorderWidth="1px">
                            <table style="width: 100%; height: 10px;">
                                <tr>
                                    <td class="style71">
                                        <asp:Label ID="lblResList" runat="server" Font-Bold="True" Font-Size="12px" 
                                            style="TEXT-ALIGN: left; color: #FFFFFF;" Text="Materials List:" 
                                            Width="85px"></asp:Label>
                                    </td>
                                    <td class="style76">
                                        <asp:TextBox ID="txtResSearch" runat="server" BorderStyle="None" 
                                            Font-Bold="True" Font-Size="12px" Width="80px" TabIndex="15"></asp:TextBox>
                                    </td>
                                    <td class="style77">
                                        <asp:ImageButton ID="ImgbtnFindRes" runat="server" Height="19px" 
                                            ImageUrl="~/Image/find_images.jpg" onclick="ImgbtnFindRes_Click" 
                                            Width="16px" BorderStyle="None" TabIndex="16" />
                                    </td>
                                    <td colspan="4">
                                        <asp:DropDownList ID="ddlResList" runat="server" AutoPostBack="True" 
                                            Font-Size="12px" onselectedindexchanged="ddlResList_SelectedIndexChanged" 
                                            style="BORDER-RIGHT: midnightblue 1px solid; BORDER-TOP: midnightblue 1px solid; BORDER-LEFT: midnightblue 1px solid; BORDER-BOTTOM: midnightblue 1px solid; BACKGROUND-COLOR: #fffbf1" 
                                            Width="385px" TabIndex="17">
                                        </asp:DropDownList>
                                    </td>
                                    <td width="80px">
                                        <asp:LinkButton ID="lbtnResSpcf" runat="server" Font-Bold="True" 
                                            Font-Size="12px" onclick="lbtnResSpcf_Click" style="text-align: right" 
                                            Width="80px" CssClass="style15" TabIndex="18">Specification:</asp:LinkButton>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlResSpcf" runat="server" Font-Size="12px" 
                                            style="BORDER-RIGHT: midnightblue 1px solid; BORDER-TOP: midnightblue 1px solid; BORDER-LEFT: midnightblue 1px solid; BORDER-BOTTOM: midnightblue 1px solid; BACKGROUND-COLOR: #fffbf1" 
                                            Width="100px" TabIndex="19">
                                        </asp:DropDownList>
                                    </td>
                                
                                    <td>
                                        <asp:LinkButton ID="lbtnSelectRes" runat="server" Font-Bold="True" 
                                            Font-Size="12px" onclick="lbtnSelectRes_Click" 
                                            style="text-align: center; " Width="54px" CssClass="style15" TabIndex="20">Select</asp:LinkButton>
                                    </td>
                                    <td class="style53">
                                        <cc1:ListSearchExtender ID="ListSearchExt1" runat="server" 
                                            QueryPattern="Contains" TargetControlID="ddlResList">
                                        </cc1:ListSearchExtender>
                                        <asp:LinkButton ID="lbtnSelectAll" runat="server" Font-Bold="True" 
                                            Font-Size="12px" ForeColor="#000" OnClick="lbtnSelectAll_Click" 
                                            style="margin-top: 0px; height: 15px;" Width="60px">Select All</asp:LinkButton>
                                    </td>
                                    <td class="style53">
                                        &nbsp;</td>
                                    <td class="style53">
                                        &nbsp;</td>
                                    <td class="style53">
                                        &nbsp;</td>
                                    <td class="style53">
                                        &nbsp;</td>
                                    <td class="style53">
                                        &nbsp;</td>
                                    <td class="style53">
                                        &nbsp;</td>
                                    <td class="style53">
                                        &nbsp;</td>
                                    <td class="style53">
                                        &nbsp;</td>
                                    <td class="style53">
                                        &nbsp;</td>
                                    <td class="style53">
                                        &nbsp;</td>
                                    <td class="style53">
                                        &nbsp;</td>
                                    <td class="style53">
                                        &nbsp;</td>
                                    <td class="style53">
                                        &nbsp;</td>
                                    <td class="style53">
                                        &nbsp;</td>
                                </tr>
                                </table>
                                </asp:Panel>
                        <asp:Panel ID="Panel2" runat="server" Visible="False">
                            <table style="width: 100%; height: 133px;">
                                <tr>
                                    <td style="height:auto;" colspan="26" valign="top">
                                        <asp:GridView ID="gvReqInfo" runat="server" AllowPaging="True" 
                                            AutoGenerateColumns="False" onrowdeleting="gvReqInfo_RowDeleting" 
                                            ShowFooter="True" Width="16px">
                                            <PagerSettings Visible="False" />
                                            <RowStyle BackColor="#D2FFF7" Font-Size="11px" />
                                            <Columns>
                                                <asp:TemplateField HeaderText="Sl.No.">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvSlNo0" runat="server" Height="16px" 
                                                            style="text-align: right" 
                                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="35px"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:CommandField ShowDeleteButton="True" />
                                                <asp:TemplateField HeaderText="Res Code" Visible="False">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvResCod" runat="server" Height="16px" 
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rsircode")) %>' 
                                                            Width="80px"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Spcf Code" Visible="False">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvSpcfCod" runat="server" Height="16px" 
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "spcfcod")) %>' 
                                                            Width="80px"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Description of Materials">
                                                    <FooterTemplate>
                                                        <asp:DropDownList ID="ddlPageNo" runat="server" __designer:wfdid="w67" 
                                                            AutoPostBack="True" Font-Bold="True" Font-Size="14px" 
                                                            onselectedindexchanged="ddlPageNo_SelectedIndexChanged" 
                                                            style="BORDER-RIGHT: navy 1px solid; BORDER-TOP: navy 1px solid; BORDER-LEFT: navy 1px solid; BORDER-BOTTOM: navy 1px solid" 
                                                            Width="150px">
                                                        </asp:DropDownList>
                                                    </FooterTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvResDesc" runat="server" 
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rsirdesc1")) %>' 
                                                            Width="220px"></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterStyle HorizontalAlign="Center" />
                                                    <HeaderStyle HorizontalAlign="Left" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Specification">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvSpfDesc" runat="server" 
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "spcfdesc")) %>' 
                                                            Width="80px"></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterStyle HorizontalAlign="Center" />
                                                    <HeaderStyle HorizontalAlign="Left" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Unit">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvResUnit" runat="server" 
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rsirunit")) %>' 
                                                            Width="35px"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Bgd.Balance">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvBgdBal" runat="server" style="text-align: right" 
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "bbgdqty")).ToString("#,##0.00;-#,##0.00; ") %>' 
                                                            Width="70px"></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Req. Qty">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtgvReqQty" runat="server" BorderColor="#99CCFF" 
                                                            BorderStyle="Solid" BorderWidth="0px" Font-Size="11px" 
                                                            style="text-align: right; background-color: Transparent" 
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "preqty")).ToString("#,##0.00;(#,##0.00); ") %>' 
                                                            Width="70px"></asp:TextBox>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Approv.Qty">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtgvappQty" runat="server" BackColor="White" 
                                                            BorderStyle="None" style="text-align: right" 
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "areqty")).ToString("#,##0.00;(#,##0.00); ") %>' 
                                                            Width="80px"></asp:TextBox>
                                                    </ItemTemplate>
                                                     <FooterTemplate>
                                                        <asp:LinkButton ID="lbtnResFooterDelete" runat="server" Font-Bold="True" 
                                                            Font-Size="14px" ForeColor="#000" onclick="lbtnResFooterDelete_Click" 
                                                            style="text-align: center; height: 17px;" Width="50px">Delete</asp:LinkButton>
                                                    </FooterTemplate>
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                               

                                                <asp:TemplateField HeaderText="FC Rate">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtgvFCRate" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                                            BorderWidth="0px" Font-Size="11px" Style="text-align: right; background-color: Transparent"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "fcrate")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                            Width="60px"></asp:TextBox>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </asp:TemplateField>


                                                <asp:TemplateField HeaderText="Conversion Rate">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtgvConRate" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                                            BorderWidth="0px" Font-Size="11px" Style="text-align: right; background-color: Transparent"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "conrate")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                            Width="60px"></asp:TextBox>
                                                    </ItemTemplate>                                                   
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </asp:TemplateField>

                                                 <asp:TemplateField HeaderText="App. Rate">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvResRat" runat="server" BorderColor="#99CCFF" 
                                                            BorderStyle="Solid" BorderWidth="0px" Font-Size="11px" 
                                                            style="text-align: right; background-color: Transparent" 
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "reqrat")).ToString("#,##0.00;(#,##0.00); ") %>' 
                                                            Width="70px"></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:LinkButton ID="lbtnResFooterTotal" runat="server" Font-Bold="True" 
                                                            Font-Size="14px" ForeColor="#000" onclick="lbtnResFooterTotal_Click" 
                                                            style="text-align: center; height: 17px;" Width="50px">Total :</asp:LinkButton>
                                                    </FooterTemplate>
                                                    <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Right" />
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </asp:TemplateField>


                                                <asp:TemplateField HeaderText="Req. Amount">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvTResAmt" runat="server" 
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "preqamt")).ToString("#,##0.00;(#,##0.00); ") %>' 
                                                            Width="80px"></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label ID="lblgvFooterTReqAmt" runat="server" ForeColor="#000" 
                                                            Width="80px"></asp:Label>
                                                    </FooterTemplate>
                                                    <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Right" />
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Appr. Amount">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvTAprAmt" runat="server" 
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "areqamt")).ToString("#,##0.00;(#,##0.00); ") %>' 
                                                            Width="80px"></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label ID="lblgvFooterTAprAmt" runat="server" ForeColor="#000" 
                                                            Width="80px"></asp:Label>
                                                    </FooterTemplate>
                                                    <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Right" />
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Exp.Use.Date">
                                                    <FooterTemplate>
                                                        <asp:LinkButton ID="lbtnUpdateResReq" runat="server" Font-Bold="True" class="UpdateButton"
                                                            Font-Size="13px" onclick="lbtnUpdateResReq_Click" 
                                                            style="text-align: center; height: 15px;" Width="80px">Final Update</asp:LinkButton>
                                                    </FooterTemplate>
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtgvUseDat" runat="server" BorderColor="#99CCFF" 
                                                            BorderStyle="Solid" BorderWidth="0px" Font-Size="11px" 
                                                            style="text-align: left; background-color: Transparent" 
                                                            Text='<%# DataBinder.Eval(Container.DataItem, "expusedt").ToString() %>' 
                                                            Width="80px"></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Stock Qty">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtgvStokQty" runat="server" BorderColor="#99CCFF" 
                                                            BorderStyle="Solid" BorderWidth="0px" Font-Size="11px" 
                                                            style="text-align: right; background-color: Transparent" 
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "pstkqty")).ToString("#,##0.00;(#,##0.00); ") %>' 
                                                            Width="70px"></asp:TextBox>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Remarks">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtgvReqNote" runat="server" BorderColor="#99CCFF" 
                                                            BorderStyle="Solid" BorderWidth="0px" Font-Size="11px" 
                                                            style="text-align: left; background-color: Transparent" 
                                                            Text='<%# DataBinder.Eval(Container.DataItem, "reqnote").ToString() %>' 
                                                            Width="100px"></asp:TextBox>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Left" />
                                                </asp:TemplateField>
                                            </Columns>
                                            <FooterStyle BackColor="#666633" />
                                            <PagerStyle HorizontalAlign="Center" />
                                            <HeaderStyle BackColor="#333300" Font-Bold="True" Font-Size="12px" 
                                                ForeColor="#FFFFCC" Height="20px" HorizontalAlign="Center" />
                                            <EditRowStyle BackColor="#E2D5CD" VerticalAlign="Top" />
                                            <AlternatingRowStyle BackColor="#CAE4FF" Font-Size="11px" />
                                        </asp:GridView>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="style71">
                                        <asp:Label ID="lblReqNarr" runat="server" CssClass="style15" Font-Bold="True" 
                                            Font-Size="12px" Height="16px" style="TEXT-ALIGN: right" Text="Narration:" 
                                            Width="80px"></asp:Label>
                                    </td>
                                    <td class="style43" colspan="5">
                                        <asp:TextBox ID="txtReqNarr" runat="server" BorderStyle="Solid" 
                                            BorderWidth="1px" Font-Bold="True" Font-Size="12px" Height="55px" 
                                            TextMode="MultiLine" Width="415px"></asp:TextBox>
                                    </td>
                                    <td align="left" valign="top">
                                        &nbsp;</td>
                                    <td>
                                        &nbsp;</td>
                                    <td>
                                        &nbsp;</td>
                                    <td>
                                        &nbsp;</td>
                                    <td colspan="15">
                                        &nbsp;</td>
                                    <td class="style53">
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td class="style71">
                                        <asp:Label ID="lblPreparedBy" runat="server" CssClass="style15" 
                                            Font-Bold="True" Font-Size="12px" Height="16px" style="TEXT-ALIGN: right" 
                                            Text="Prepared By:" Visible="False" Width="80px"></asp:Label>
                                    </td>
                                    <td class="style76">
                                        <asp:TextBox ID="txtPreparedBy" runat="server" BorderStyle="Solid" 
                                            BorderWidth="1px" Font-Bold="True" Font-Size="12px" Visible="False" 
                                            Width="90px"></asp:TextBox>
                                    </td>
                                    <td class="style74">
                                        &nbsp;</td>
                                    <td style="text-align: right">
                                        <asp:Label ID="lblApprovedBy" runat="server" CssClass="style15" 
                                            Font-Bold="True" Font-Size="12px" Height="16px" style="TEXT-ALIGN: right" 
                                            Text="Approved By:" Visible="False" Width="80px"></asp:Label>
                                    </td>
                                    <td class="style54">
                                        <asp:TextBox ID="txtApprovedBy" runat="server" BorderStyle="Solid" 
                                            BorderWidth="1px" Font-Bold="True" Font-Size="12px" Visible="False" 
                                            Width="120px"></asp:TextBox>
                                    </td>
                                    <td style="text-align: right">
                                        <asp:Label ID="lblApprovalDate" runat="server" CssClass="style15" 
                                            Font-Bold="True" Font-Size="12px" Height="16px" style="TEXT-ALIGN: right" 
                                            Text="Approv.Date:" Visible="False" Width="65px"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtApprovalDate" runat="server" BorderStyle="Solid" 
                                            BorderWidth="1px" Font-Bold="True" Font-Size="12px" ToolTip="(dd.mm.yyyy)" 
                                            Visible="False" Width="100px"></asp:TextBox>
                                    </td>
                                    <td>
                                        &nbsp;</td>
                                    <td>
                                        &nbsp;</td>
                                    <td>
                                        &nbsp;</td>
                                    <td colspan="15">
                                        &nbsp;</td>
                                    <td class="style53">
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td class="style71">
                                        <asp:Label ID="lblExpDeliveryDate" runat="server" CssClass="style15" 
                                            Font-Bold="True" Font-Size="12px" Height="16px" style="TEXT-ALIGN: right" 
                                            Text="Exp.Del. Date:" Visible="False" Width="80px"></asp:Label>
                                    </td>
                                    <td class="style76">
                                        <asp:TextBox ID="txtExpDeliveryDate" runat="server" BorderStyle="Solid" 
                                            BorderWidth="1px" Font-Bold="True" Font-Size="12px" ToolTip="(dd.mm.yyyy)" 
                                            Width="90px" Visible="False"></asp:TextBox>
                                    </td>
                                    <td class="style74">
                                        &nbsp;</td>
                                    <td style="text-align: right">
                                        &nbsp;</td>
                                    <td colspan="5">
                                        &nbsp;</td>
                                    <td>
                                        &nbsp;</td>
                                    <td colspan="15">
                                        &nbsp;</td>
                                    <td class="style53">
                                        &nbsp;</td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </td>
                    <td class="style19">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td class="style18">
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                    <td class="style42">
                        &nbsp;</td>
                    <td class="style34">
                        &nbsp;</td>
                    <td class="style55">
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                    <td class="style34">
                        &nbsp;</td>
                    <td class="style39">
                        &nbsp;</td>
                    <td class="style38">
                        &nbsp;</td>
                    <td class="style47">
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                    <td class="style46">
                        &nbsp;</td>
                    <td class="style19">
                        &nbsp;</td>
                </tr>
            </table>--%>

        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>


