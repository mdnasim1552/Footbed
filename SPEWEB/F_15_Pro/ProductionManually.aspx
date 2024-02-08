<%@ Page Title="" Language="C#" MasterPageFile="~/SPE.Master" AutoEventWireup="true" CodeBehind="ProductionManually.aspx.cs" Inherits="SPEWEB.F_15_Pro.ProductionManually" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
     <style type="text/css">
       .chzn-single {
           border-radius: 3px !important;
           height: 29px !important;
       }
       .production-info .form-group {
     margin-bottom: 0rem !important; 
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
    <script language="javascript" type="text/javascript">
        function GoToNextTextBox(currentTxtId, e) {
            if (e.keyCode == 13 || e.keyCode == 40) {
                var number = parseInt(currentTxtId.id.substring(41));
                var nextId = number + 1;
                var nextIdString = "ContentPlaceHolder1_Productrep_lblordqty_" + nextId.toString();
                var x = document.getElementById(nextIdString);
                x.focus();
            }
            else
                if (e.keyCode == 38) {
                    var number = parseInt(currentTxtId.id.substring(41));
                    var nextId = number - 1;

                    var nextIdString = "ContentPlaceHolder1_Productrep_lblordqty_" + nextId.toString();

                    var x = document.getElementById(nextIdString);
                    x.focus();
                }

        }
    </script>


    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
             <div class="nahidProgressbar">
                <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1" DisplayAfter="30">
                    <ProgressTemplate>

                        <%--  <div class="loader"></div> --%>
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
                                    <div class="col-md-2 ">
                                        <div class="form-group">
                                        <asp:Label ID="Label1" runat="server" CssClass="label"> PRODM NO</asp:Label>
                                            <div class="form-inline">
                                        <asp:Label ID="lblCurNo1" style="width:50%" runat="server" CssClass="form-control form-control-sm" Text="GRM00-" ReadOnly="True" TabIndex="2"></asp:Label>
                                        <asp:TextBox ID="txtCurNo2" style="width:50%" runat="server" CssClass="form-control form-control-sm" ReadOnly="True" TabIndex="3">00000</asp:TextBox>
                                        </div>
                                            </div>
                                    </div>
                                    <div class="col-md-1">
                                        <div class="form-group">
                                        <asp:Label ID="lblrefNo" runat="server" CssClass="label" Text="Ref. No"></asp:Label>
                                        <asp:TextBox ID="txtRefno" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>

                                        </div>
                                        </div>
                          <div class="col-md-1">
                                <div class="form-group">
                                        <asp:Label ID="lblDate" runat="server" CssClass="label " Text="Date"></asp:Label>
                                        <asp:TextBox ID="txtCurDate" runat="server" CssClass="form-control form-control-sm small" ToolTip="(dd.mm.yyyy)"></asp:TextBox>
                                        <cc1:CalendarExtender ID="txtCurDate_CalendarExtender" runat="server"
                                            Enabled="True" Format="dd.MM.yyyy" TargetControlID="txtCurDate"></cc1:CalendarExtender>
                              </div>  
                               </div> 
                        <div class="col-md-3">
                                <div class="form-group">
                              <asp:LinkButton ID="LbtnBatch" OnClick="LbtnBatch_Click"  runat="server" CssClass="label">Master Order/Article</asp:LinkButton>
                                                                           
                                            <asp:DropDownList ID="ddlbatchno" runat="server" CssClass=" form-control form-control-sm chzn-select" AutoPostBack="true" OnSelectedIndexChanged="ddlbatchno_SelectedIndexChanged">
                                            </asp:DropDownList>

                                        </div>
                                    </div>
                                    <div class="col-md-1">
                                       <div class="form-group" style="margin-top:20px">
                                            <asp:LinkButton ID="lbtnOk" runat="server" CssClass="btn btn-primary btn-sm" OnClick="lbtnOk_Click">Ok</asp:LinkButton>
                                        </div>
                                    </div>
                                    <div class="col-md-1">
                                         <div class="form-group">
                                        <asp:Label ID="lblPage" runat="server" CssClass="label">Page Size</asp:Label>
                                        <asp:DropDownList ID="ddlpagesize" runat="server" AutoPostBack="True" CssClass="form-control form-control-sm" OnSelectedIndexChanged="ddlpagesize_SelectedIndexChanged">

                                            <asp:ListItem>10</asp:ListItem>
                                            <asp:ListItem>30</asp:ListItem>
                                            <asp:ListItem>50</asp:ListItem>
                                            <asp:ListItem>100</asp:ListItem>
                                            <asp:ListItem>150</asp:ListItem>
                                            <asp:ListItem>200</asp:ListItem>
                                            <asp:ListItem>300</asp:ListItem>
                                            <asp:ListItem>500</asp:ListItem>
                                            <asp:ListItem>700</asp:ListItem>
                                            <asp:ListItem>900</asp:ListItem>
                                            <asp:ListItem>1100</asp:ListItem>
                                        </asp:DropDownList>
                                             </div>
                                     
                                    </div>
                         <div class="col-md-3">
                                        <asp:LinkButton ID="imgPreVious" runat="server" CssClass="label" OnClick="imgPreVious_Click"
                                            TabIndex="3">Previous GRM</asp:LinkButton>
                                        <asp:DropDownList ID="ddlPrevList" runat="server" CssClass="form-control form-control-sm" TabIndex="6">
                                        </asp:DropDownList>

                                    </div>
                                                            
                                    <div class="col-md-3">
                                        <div class="form-group">
                                        <asp:Label ID="lblClientName" runat="server" CssClass="label">Store</asp:Label>
                                        <asp:DropDownList ID="ddlProjectName" runat="server"  CssClass="form-control form-control-sm chzn-select" TabIndex="12" AutoPostBack="True">
                                        </asp:DropDownList>
                                    </div>
                                          </div>
                                    <div class="col-md-2">
                                          <div class="form-group">
                                        <asp:Label ID="lbltxtpartyname" runat="server" CssClass="label">Buyer Name</asp:Label>
                                        <asp:DropDownList ID="ddlMSRSupl" runat="server" CssClass="form-control form-control-sm chzn-select" TabIndex="6" >
                                        </asp:DropDownList>

                                    </div>
                                         </div>
                        <div class="col-md-7">
                            <asp:Panel ID="PnlSub" runat="server" Visible="False">
                                <div class="row">
                                        <div class="col-md-5">
                                            <div class="form-group">
                                            <asp:Label ID="Label2" runat="server" CssClass="label">Order Wise Style Color</asp:Label>
                                      
                                            <asp:DropDownList ID="ddlStyleColor" AutoPostBack="true" OnSelectedIndexChanged="ddlStyleColor_SelectedIndexChanged" runat="server" CssClass="form-control form-control-sm chzn-select" TabIndex="15">
                                            </asp:DropDownList>
                                        </div>
                                            </div>
                                        <div class="col-md-2">
                                            <div class="form-group" style="margin-top:20px;">
                                                <asp:LinkButton ID="btnview" runat="server" CssClass="btn btn-primary btn-sm" OnClick="btnview_Click">Select</asp:LinkButton>


                                            </div>
                                        </div>
                                        <div class="col-md-2">
                                             <div class="form-group">
                                            <asp:LinkButton ID="btnCurr" runat="server" CssClass="label" Text="Currency:"></asp:LinkButton>

                                            <asp:HyperLink ID="HyperLink1" runat="server" CssClass="btnok pull-left" ToolTip="Create List" Target="_blank"
                                                NavigateUrl="~/F_34_Mgt/AccConversion.aspx"><span class="fa fa-plus"></span></asp:HyperLink>

                                            <asp:DropDownList ID="ddlCurrency" CssClass="form-control form-control-sm" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlCurrency_SelectedIndexChanged"></asp:DropDownList>
                                            </div>
                                            </div>
                                     <div class="col-md-2">
                                             <div class="form-group">
                                            <asp:Label ID="Label22" runat="server" CssClass=" label" Text="Con. Rate:"></asp:Label>
                                            <asp:TextBox ID="lblConRate" runat="server" CssClass="form-control form-control-sm "></asp:TextBox>

                                        </div>
                                      </div>
                        </div>
                             
                        </asp:Panel>
                            </div>
                                   
                                </div>



                            </div>
                        
                        </div>
             <div class="card card-fluid">
                <div class="card-body" style="min-height:450px;">
                        <div class="row">
                            <div class="col-md-7 production-info">
                                <asp:GridView ID="gvOrdernfo" runat="server" AllowPaging="True" CssClass="table-striped table-hover table-bordered grvContentarea" OnRowDataBound="gvOrdernfo_RowDataBound"
                                    AutoGenerateColumns="False" ShowFooter="True" Width="16px" OnRowDeleting="gvOrdernfo_RowDeleting" OnPageIndexChanging="gvOrdernfo_PageIndexChanging">
                                    <PagerSettings Position="Bottom" />
                                    <AlternatingRowStyle />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sl">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvSlNo0" runat="server" Height="16px"
                                                    Style="text-align: right"
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="35px"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:CommandField ShowDeleteButton="True" ControlStyle-CssClass="text-red" DeleteText="<span class='fa fa-trash'><span>" />
                                        <asp:TemplateField HeaderText="Order No">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvOdrno" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "orderno")) %>'
                                                    Width="100px">
                                                </asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle HorizontalAlign="Center" />
                                            <HeaderStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Description">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lblgvResDesc" runat="server" OnClick="lblgArtno_Click"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "styledesc")) %>'
                                                    Width="170px">
                                                </asp:LinkButton>
                                                <asp:Label ID="lblgvItmCodc" runat="server" Visible="false"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "styleid")) %>'>
                                                </asp:Label>
                                                <asp:Label ID="LblDayid" runat="server" Visible="false"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "dayid")) %>'>
                                                </asp:Label>
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
                                        <asp:TemplateField HeaderText="Qty.">

                                            <ItemTemplate>
                                                <asp:TextBox ID="txtgvQty" runat="server" BackColor="Transparent" BorderColor="#a5f29b" BorderWidth="1px"
                                                    BorderStyle="Solid" Font-Size="11px" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty")).ToString("#,##0;-#,##0; ") %>'
                                                    Width="60px"></asp:TextBox>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lblgvFooterTqty" Style="text-align: right" runat="server" Font-Bold="True"
                                                    Font-Size="12px" Width="60px"></asp:Label>
                                            </FooterTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Rate">
                                            <%--  <FooterTemplate>
                                        <asp:LinkButton ID="lbtnUpdate" runat="server" CssClass="btn btn-danger primaryBtn" OnClick="lbtnUpdate_Click"
                                            Style="text-align: center;" Width="70px">Update</asp:LinkButton>
                                    </FooterTemplate>--%>
                                            <ItemTemplate>
                                                <asp:Label ID="txtgvRate" runat="server" BackColor="Transparent" BorderColor="#a5f29b" BorderWidth="1px"
                                                    BorderStyle="Solid" Font-Size="11px" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "prorate")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="50px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="FC Amount">
                                            <ItemTemplate>
                                                <asp:Label ID="txtgvFCAmt" runat="server" BackColor="Transparent"
                                                    Font-Size="11px" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "fcamt")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lblgvFooterFCTAmt" runat="server" Font-Bold="True"
                                                    Font-Size="12px" Width="80px"></asp:Label>
                                            </FooterTemplate>
                                            <FooterStyle Font-Bold="True" Font-Size="11px" HorizontalAlign="Right" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="BDT Amount">
                                            <ItemTemplate>
                                                <asp:Label ID="txtgvAmt" runat="server" BackColor="Transparent"
                                                    Font-Size="11px" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "proamt")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lblgvFooterTAmt" runat="server" Font-Bold="True"
                                                    Font-Size="12px" Width="80px"></asp:Label>
                                            </FooterTemplate>
                                            <FooterStyle Font-Bold="True" Font-Size="11px" HorizontalAlign="Right" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                         <asp:TemplateField HeaderText="Location">
                                              <HeaderTemplate>
                                                  <asp:DropDownList ID="ddlLocHead" AutoPostBack="true" OnSelectedIndexChanged="ddlLocHead_SelectedIndexChanged" CssClass="form-control form-control-sm" Width="150px" runat="server">
                                                    <%--  <asp:ListItem Value="Yes" Selected="True">Yes</asp:ListItem>
                                                    <asp:ListItem Value="No">No</asp:ListItem>--%>
                                                </asp:DropDownList>
                                            </HeaderTemplate>  
                                             <ItemTemplate>
                                                    <div class="form-group">
                                                        <div class="col-md-6">

                                                            <asp:DropDownList ID="ddlval" runat="server" CssClass=" ddlPage62 inputTxt chzn-select" Width="150" TabIndex="2">
                                                            </asp:DropDownList>



                                                        </div>
                                                    </div>
                                                </ItemTemplate>  
                                              
                                            </asp:TemplateField>
                                    </Columns>
                                    <FooterStyle CssClass="grvFooter" />
                                    <EditRowStyle />
                                    <AlternatingRowStyle />
                                    <PagerStyle CssClass="gvPagination" />
                                    <HeaderStyle CssClass="grvHeader" />
                                </asp:GridView>
                                <div class=" clearfix"></div>
                                <asp:Panel ID="Panel2" runat="server" Visible="False">
                                    <fieldset class="scheduler-border fieldset_Nar">
                                        <div class="form-horizontal">
                                            <div class="form-group">
                                                <div class="col-md-6 pading5px">
                                                    <div class="input-group">
                                                        <span class="input-group-addon glypingraddon">
                                                            <asp:Label ID="lblReqNarr" runat="server" CssClass="lblTxt lblName" Text="Narration:"></asp:Label>
                                                        </span>
                                                        <asp:TextBox ID="txtBillNarr" runat="server" class="form-control" TextMode="MultiLine" Height="25px"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <%--<div class="col-md-3 pading5px pull-right"> Price Quotation of Star Paradise Products.
                                            <a class=" btn btn-primary primaryBtn nextPrev" href='<%=this.ResolveUrl("~/DeafultMenu.aspx?Type=22")%>' style="margin: 0 0 0 5px;">Next</a>
                                        </div>--%>
                                            </div>

                                        </div>

                                    </fieldset>

                                </asp:Panel>
                            </div>
                            <div class="col-md-5">
                                <div class="table-responsive">
                                    <asp:GridView ID="gv1" runat="server" AutoGenerateColumns="False" Height="1px" PageSize="15"
                                        ShowFooter="True" Width="253px" CssClass=" table-striped table-hover table-bordered grvContentarea" Font-Size="11px">

                                        <Columns>
                                            <asp:TemplateField HeaderText="Style ID" Visible="False">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvStyleID" runat="server" Style="text-transform: capitalize" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "styleid")) %>'
                                                        Width="51px"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Color ID" Visible="False">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvColorID" runat="server" Style="text-transform: capitalize" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "colorid")) %>'
                                                        Width="51px"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Category and <br> Article Number">

                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvStyleDesc0" runat="server" Style="text-transform: capitalize; text-align: center;"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "description")) %>'
                                                        Width="160px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterStyle HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="Justify" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Color">
                                                <%-- <FooterTemplate>
                                                                    <asp:LinkButton ID="lbtnTotal" runat="server" Font-Bold="True" Font-Size="12px"
                                                                        OnClick="lbtnTotal_Click" CssClass="btn btn-primary primarygrdBtn">Total</asp:LinkButton>
                                                                </FooterTemplate>--%>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvColorDesc0" runat="server" Style="text-transform: capitalize"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "ColorDesc")) %>'
                                                        Width="91px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterStyle HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Unit" Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvStyleUnit" runat="server" Style="text-transform: capitalize"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "StyleUnit")) %>'
                                                        Width="40px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Size-01">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtgvF1" runat="server" BackColor="Transparent" BorderColor="#6acc39"
                                                        BorderStyle="Solid" BorderWidth="1px"
                                                        Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s1")).ToString("###0;(###0); ") %>'
                                                        Width="40px"></asp:TextBox>
                                                </ItemTemplate>

                                                <FooterStyle HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Size-02">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtgvF2" runat="server" BackColor="Transparent" BorderColor="#6acc39"
                                                        BorderStyle="Solid" BorderWidth="1px" Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s2")).ToString("###0;(###0); ") %>'
                                                        Width="40px"></asp:TextBox>
                                                </ItemTemplate>
                                                <FooterStyle HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Size-03">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtgvF3" runat="server" BackColor="Transparent" BorderColor="#6acc39"
                                                        BorderStyle="Solid" BorderWidth="1px"
                                                        Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s3")).ToString("###0;(###0); ") %>'
                                                        Width="40px"></asp:TextBox>
                                                </ItemTemplate>
                                                <FooterStyle HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Size-04">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtgvF4" runat="server" BackColor="Transparent" BorderColor="#6acc39"
                                                        BorderStyle="Solid" BorderWidth="1px"
                                                        Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s4")).ToString("###0;(###0); ") %>'
                                                        Width="40px"></asp:TextBox>
                                                </ItemTemplate>
                                                <FooterStyle HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Size-05">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtgvF5" runat="server" BackColor="Transparent" BorderColor="#6acc39"
                                                        BorderStyle="Solid" BorderWidth="1px"
                                                        Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s5")).ToString("###0;(###0); ") %>'
                                                        Width="40px"></asp:TextBox>
                                                </ItemTemplate>
                                                <FooterStyle HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Size-06" Visible="False">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtgvF6" runat="server" BackColor="Transparent" BorderColor="#6acc39"
                                                        BorderStyle="Solid" BorderWidth="1px"
                                                        Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s6")).ToString("###0;(###0); ") %>'
                                                        Width="40px"></asp:TextBox>
                                                </ItemTemplate>
                                                <FooterStyle HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Size-07" Visible="False">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtgvF7" runat="server" BackColor="Transparent" BorderColor="#6acc39"
                                                        BorderStyle="Solid" BorderWidth="1px"
                                                        Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s7")).ToString("###0;(###0); ") %>'
                                                        Width="40px"></asp:TextBox>
                                                </ItemTemplate>
                                                <FooterStyle HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Size-08" Visible="False">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtgvF8" runat="server" BackColor="Transparent" BorderColor="#6acc39"
                                                        BorderStyle="Solid" BorderWidth="1px"
                                                        Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s8")).ToString("###0;(###0); ") %>'
                                                        Width="40px"></asp:TextBox>
                                                </ItemTemplate>
                                                <FooterStyle HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Size-09" Visible="False">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtgvF9" runat="server" BackColor="Transparent" BorderColor="#6acc39"
                                                        BorderStyle="Solid" BorderWidth="1px"
                                                        Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s9")).ToString("###0;(###0); ") %>'
                                                        Width="40px"></asp:TextBox>
                                                </ItemTemplate>
                                                <FooterStyle HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Size-10" Visible="False">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtgvF10" runat="server" BackColor="Transparent" BorderColor="#6acc39"
                                                        BorderStyle="Solid" BorderWidth="1px"
                                                        Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s10")).ToString("###0;(###0); ") %>'
                                                        Width="40px"></asp:TextBox>
                                                </ItemTemplate>
                                                <FooterStyle HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Size-11" Visible="False">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtgvF11" runat="server" BackColor="Transparent" BorderColor="#6acc39"
                                                        BorderStyle="Solid" BorderWidth="1px"
                                                        Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s11")).ToString("###0;(###0); ") %>'
                                                        Width="40px"></asp:TextBox>
                                                </ItemTemplate>
                                                <FooterStyle HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Size-12" Visible="False">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtgvF12" runat="server" BackColor="Transparent" BorderColor="#6acc39"
                                                        BorderStyle="Solid" BorderWidth="1px"
                                                        Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s12")).ToString("###0;(###0); ") %>'
                                                        Width="40px"></asp:TextBox>
                                                </ItemTemplate>
                                                <FooterStyle HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Size-13" Visible="False">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtgvF13" runat="server" BackColor="Transparent" BorderColor="#6acc39"
                                                        BorderStyle="Solid" BorderWidth="1px"
                                                        Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s13")).ToString("###0;(###0); ") %>'
                                                        Width="40px"></asp:TextBox>
                                                </ItemTemplate>
                                                <FooterStyle HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Size-14" Visible="False">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtgvF14" runat="server" BackColor="Transparent" BorderColor="#6acc39"
                                                        BorderStyle="Solid" BorderWidth="1px"
                                                        Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s14")).ToString("###0;(###0); ") %>'
                                                        Width="40px"></asp:TextBox>
                                                </ItemTemplate>
                                                <FooterStyle HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Size-15" Visible="False">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtgvF15" runat="server" BackColor="Transparent" BorderColor="#6acc39"
                                                        BorderStyle="Solid" BorderWidth="1px"
                                                        Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s15")).ToString("###0;(###0); ") %>'
                                                        Width="40px"></asp:TextBox>
                                                </ItemTemplate>
                                                <FooterStyle HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Total">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvTotal1" runat="server" Style="font-size: 11px; text-align: right"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "totalqty")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="40px"></asp:Label>

                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="FLblgvTotal" runat="server"></asp:Label>
                                                </FooterTemplate>
                                                <FooterStyle HorizontalAlign="Right" />
                                                <HeaderStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Color Wise ORD QTY">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvColTotal1" Font-Bold="true" runat="server" Style="font-size: 11px; text-align: right"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "colqty")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="40px"></asp:Label>

                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="FLblgvColTotal" Font-Bold="true" runat="server"></asp:Label>
                                                </FooterTemplate>
                                                <FooterStyle HorizontalAlign="Right" />
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
                                <asp:Panel ID="MatAddPanel" runat="server" Visible="false">
                                    <div class="col-md-10">
                                         <asp:DropDownList ID="ddlResourcesCost" Width="440px"  runat="server" CssClass="form-control chzn-select" >
                                            </asp:DropDownList>
                                    </div>
                                    <div class="col-md-2">
                                            <asp:LinkButton ID="lnkAddResouctCost" runat="server" Text="Add" OnClick="lnkAddResouctCost_Click" CssClass="btn btn-primary primaryBtn " TabIndex="1">Add</asp:LinkButton>

                                    </div>
                                    <div class="clearfix"></div>
                                    <br />
                                </asp:Panel>
                                <asp:GridView ID="gvCost" runat="server" AutoGenerateColumns="False" ShowFooter="True" CssClass="table-striped table-hover table-bordered grvContentarea"
                                    Width="479px">
                                    <Columns>
                                        <asp:TemplateField HeaderStyle-Font-Bold="true" HeaderStyle-Font-Size="12px" HeaderStyle-ForeColor="#333333"
                                            HeaderText="SL" ItemStyle-Font-Size="12px">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvsl0" runat="server" Text='<%# Convert.ToInt32(Container.DataItemIndex+1).ToString("00")+"." %>'
                                                    Width="10px" Style="text-align: left"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle Font-Bold="True" Font-Size="12px" ForeColor="#333333" />
                                            <ItemStyle Font-Size="12px" />
                                        </asp:TemplateField>



                                        <asp:TemplateField HeaderStyle-Font-Bold="true" HeaderStyle-Font-Size="10px" HeaderStyle-ForeColor="#333333"
                                            HeaderText="MATERIALS NAME" ItemStyle-Font-Size="10px" ItemStyle-HorizontalAlign="Left">

                                            <ItemTemplate>

                                                <asp:Label ID="lblgvDesc" runat="server" BorderStyle="None" Style="text-align: left" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "itmdesc")) %>'
                                                    Width="130px"></asp:Label>

                                            </ItemTemplate>
                                            <HeaderStyle Font-Bold="True" Font-Size="10px" ForeColor="#333333" />
                                            <ItemStyle Font-Size="9px" />
                                            <FooterStyle HorizontalAlign="left" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderStyle-Font-Bold="true" HeaderStyle-Font-Size="10px" HeaderStyle-ForeColor="#333333"
                                            HeaderText="SPECIFICATION NAME" ItemStyle-Font-Size="10px">

                                            <ItemTemplate>
                                                <asp:Label ID="lblgvspcfcode" runat="server" Visible="false" BorderStyle="None" Style="text-align: left" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "spcfcod")) %>'
                                                    Width="0px"></asp:Label>
                                                <asp:Label ID="lblgvspcfdesc" runat="server" BorderStyle="None" Style="text-align: left" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "spcfdesc")) %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle Font-Bold="True" Font-Size="10px" ForeColor="#333333" />
                                            <ItemStyle Font-Size="9px" />
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderStyle-Font-Bold="true" HeaderStyle-Font-Size="10px" HeaderStyle-ForeColor="#333333"
                                            HeaderText="UNIT" ItemStyle-Font-Size="10px">

                                            <ItemTemplate>
                                                <asp:Label ID="txtgvunit0" runat="server" BorderStyle="None" Style="text-align: left" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "itmunit")) %>'
                                                    Width="30px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle Font-Bold="True" Font-Size="10px" ForeColor="#333333" />
                                            <ItemStyle Font-Size="10px" />
                                        </asp:TemplateField>



                                        <asp:TemplateField HeaderStyle-Font-Bold="true" HeaderStyle-Font-Size="10px" HeaderStyle-ForeColor="#333333" ItemStyle-HorizontalAlign="Right"
                                            HeaderText="Con. QTY">
                                            <ItemTemplate>
                                                <asp:Label ID="txtgvqtyCost" runat="server" BorderStyle="None" CssClass="GridItmTextBoxRight"
                                                    Font-Size="10px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "conqty")).ToString("#,##0.000000;(#,##0.000000); ") %>'
                                                    Width="50px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lblgvttlqty" runat="server" Font-Bold="True" Font-Size="10px" ForeColor="#CC0066"
                                                    Style="text-align: right" Width="50px"></asp:Label>
                                            </FooterTemplate>
                                            <HeaderStyle Font-Bold="True" Font-Size="10px" ForeColor="#333333" />
                                        </asp:TemplateField>
                                         <asp:TemplateField HeaderStyle-Font-Bold="true" HeaderStyle-Font-Size="10px" HeaderStyle-ForeColor="#333333" ItemStyle-HorizontalAlign="Right"
                                            HeaderText="Act.QTY">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtgvitmqty" runat="server" BorderStyle="Solid" BorderColor="LightGreen" BorderWidth="1px"
                                                    Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "itmqty")).ToString("#,##0.000000;(#,##0.000000); ") %>'
                                                    Width="60px"></asp:TextBox>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lblgvitmqty" runat="server" Font-Bold="True" Font-Size="10px" ForeColor="#CC0066"
                                                    Style="text-align: right" Width="50px"></asp:Label>
                                            </FooterTemplate>
                                            <HeaderStyle Font-Bold="True" Font-Size="10px" ForeColor="#333333" />
                                        </asp:TemplateField>
                                        <asp:TemplateField Visible="false" HeaderStyle-Font-Bold="true" HeaderStyle-Font-Size="12px" HeaderStyle-ForeColor="#333333"
                                            ItemStyle-Font-Size="10px" ItemStyle-HorizontalAlign="Right" HeaderText="Con. AMOUNT">
                                            <ItemTemplate>
                                                <asp:Label ID="txtgvqrateCost" runat="server" BorderStyle="None" Font-Size="12px"
                                                    Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "conamt")).ToString("#,##0.000000;(#,##0.000000); ") %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle Font-Bold="True" Font-Size="10px" ForeColor="#333333" />
                                            <ItemStyle Font-Size="10px" />
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
                <!-- End of Content Part-->
           
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>


