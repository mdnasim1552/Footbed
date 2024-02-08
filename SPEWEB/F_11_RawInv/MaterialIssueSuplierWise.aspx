<%@ Page Title="" Language="C#" MasterPageFile="~/SPE.Master" AutoEventWireup="true" CodeBehind="MaterialIssueSuplierWise.aspx.cs" Inherits="SPEWEB.F_11_RawInv.MaterialIssueSuplierWise" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript" language="javascript">

        function OpenBatchModal() {

            $('#BatchModal').modal('show');
        }
        function OpenModal() {

            $('#SizeModal').modal('show');
        }

        function CLoseMOdal() {

            $('#BatchModal').modal('hide');
            $('#SizeModal').modal('hide');
        }


        $(document).ready(function () {
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


    <style>
         .chzn-single {
    border-radius: 3px !important;
    height: 29px !important;
    
}
    </style>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
          <div class="card card-fluid">
                <div class="card-body">
                    <div class="row">    
                                    <div class="col-md-1">
                                          <div class="form-group">
                                        <asp:Label ID="Label6" runat="server" CssClass="label"> Date</asp:Label>
                                        <asp:TextBox ID="txtCurDate" runat="server" CssClass="form-control form-control-sm small"></asp:TextBox>
                                        <cc1:CalendarExtender ID="txtCurDate_CalendarExtender" runat="server"
                                            Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txtCurDate"></cc1:CalendarExtender>
                                              </div>
                                        </div>
                        <div class="col-md-1">
                                          <div class="form-group">
                                        <asp:Label ID="lblrefNo" runat="server" CssClass="label">Ref. No.:</asp:Label>
                                        <asp:TextBox ID="txtlSuRefNo" runat="server" CssClass="form-control form-control-sm small" ></asp:TextBox>
   </div>
                                        </div>
                        <div class="col-md-3">
                               <div class="form-group">
                                        <asp:Label ID="lblRefNo2" runat="server" CssClass="label" Text="Issue No:"></asp:Label>
                                        <div class="form-inline">
                                        <asp:Label ID="lblCurNo1" runat="server" Text="ISU00-" CssClass="form-control form-control-sm small"></asp:Label>
                                        <asp:Label ID="lblCurNo2" runat="server" Text="00000" CssClass="form-control form-control-sm small" ></asp:Label>
                                       &nbsp; <asp:LinkButton ID="lbtnOk" runat="server" CssClass="btn btn-primary btn-sm" OnClick="lbtnOk_Click">Ok</asp:LinkButton>
                                        &nbsp;<asp:LinkButton ID="LbtnModalBreakDown" runat="server" OnClick="LbtnModalBreakDown_Click" Text="Trial Order" CssClass="btn btn-success btn-xs"></asp:LinkButton>
                                        
                                      
                                        </div>
                                    </div>
                             </div>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                        <asp:Label ID="Label10" runat="server" CssClass="label">Order :</asp:Label>
                                        <asp:TextBox ID="txtSrcPro" runat="server" CssClass="form-control form-control-sm" Visible="false"></asp:TextBox>

                                        <asp:LinkButton ID="ibtnFindProject" runat="server" CssClass="btn btn-primary btn-sm" Visible="false" OnClick="ibtnFindProject_Click"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>
                                            <asp:DropDownList ID="ddlStoreName" runat="server" CssClass="form-control form-control-sm chzn-select">
                                            </asp:DropDownList>
                                             <asp:Label ID="lblStoredesc" runat="server" CssClass="form-control form-control-sm" Visible="false" >Order</asp:Label>
                                        <asp:LinkButton ID="Label4" runat="server" Visible="false" CssClass="smLbl_to" OnClick="imgbtnStorid_Click">Store :</asp:LinkButton>
                                       
                                            <asp:DropDownList ID="ddlStore" Visible="false" runat="server" CssClass="form-control form-control-sm chzn-select" >
                                            </asp:DropDownList>
                                        </div>
                                          </div>
                                         <div class="col-md-2">
                                        <div class="form-group">
                                            <asp:Label ID="Label5" runat="server" CssClass="label">Supplier List</asp:Label>
                                           
                                                <asp:DropDownList ID="DdlSupplier" OnSelectedIndexChanged="DdlSupplier_SelectedIndexChanged" AutoPostBack="true"  runat="server" CssClass="form-control form-control-sm chzn-select">
                                                </asp:DropDownList>
                                            </div>
                                             </div>
                                       
                                 
                                    <div class="col-md-2">
                                        <div class="form-group">
                                        <asp:LinkButton ID="ibtnPreIssueList" runat="server" CssClass="label" OnClick="ibtnPreIssueList_Click"
                                            TabIndex="3">Previous List</asp:LinkButton>
                                        <asp:DropDownList ID="ddlPrevIssueList" runat="server" CssClass="form-control form-control-sm" TabIndex="6">
                                        </asp:DropDownList>

                                    </div>
                                        </div>
                                </div>                  
                    <asp:Panel ID="panel11" runat="server" Visible="false">
                          <div class="row">     
                                        <div class="col-md-2">
                                               <div class="form-group">
                                            <asp:Label ID="lblReqList" runat="server" CssClass="label">Req. Number</asp:Label>
                                            <asp:TextBox ID="txtResSearch" runat="server" CssClass="form-control form-control-sm" Visible="false"></asp:TextBox>
                                            <asp:LinkButton ID="ImgbtnFindReqList" runat="server" Visible="false" CssClass="btn btn-primary btn-sm" OnClick="ImgbtnFindReqList_Click"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>
                                           
                                                <asp:DropDownList ID="ddlReqList" OnSelectedIndexChanged="ddlReqList_SelectedIndexChanged"  runat="server" CssClass="form-control form-control-sm chzn-select" AutoPostBack="True">
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="col-md-2">
                                            <div class="form-group">
                                            <asp:Label ID="Label1" runat="server" CssClass="label">Process List</asp:Label>
                                           
                                                <asp:DropDownList ID="ddlprocess" OnSelectedIndexChanged="ddlprocess_SelectedIndexChanged" AutoPostBack="true"  runat="server" CssClass="form-control form-control-sm chzn-select">
                                                </asp:DropDownList>
                                            </div>

                                        </div>
                              <div class="col-md-2">
                                   <div class="form-group">
                                            <asp:Label ID="lblMatList" runat="server" CssClass="label">Material List</asp:Label>
                                            <asp:TextBox ID="txtMatSearch" runat="server" CssClass=" inputTxt inputName inpPixedWidth" Visible="false"></asp:TextBox>
                                                                                           <asp:LinkButton ID="ImgbtnFindMatList" runat="server" CssClass="btn btn-primary srearchBtn" Visible="false" OnClick="ImgbtnFindMatList_Click"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>
                                                                                       <asp:DropDownList ID="ddlMatList" OnSelectedIndexChanged="ddlMatList_SelectedIndexChanged" AutoPostBack="true"  runat="server" CssClass="form-control form-control-sm chzn-select">
                                                </asp:DropDownList>

                                            </div>

                                        </div>
                               <div class="col-md-2">
                                    <div class="form-group">
                                            <asp:Label ID="Label3" runat="server" CssClass="label">Specification</asp:Label>
                                            <asp:DropDownList ID="ddlspcflist"  runat="server" CssClass="form-control form-control-sm chzn-select">
                                                </asp:DropDownList>  
                                   </div>
                               </div>
                                <div class="col-md-1">
                                        <div class="form-group">
                                            <asp:Label ID="Label2" runat="server" CssClass=" smLbl_to" Text="Con Unit:"></asp:Label>


                                            <asp:DropDownList ID="ddlunit" runat="server" Width="95px" CssClass="smDropDown inputTxt chzn-select"></asp:DropDownList>
                                        </div>
                                      </div>
                                        <div class="col-md-2 ">
                                            <div class="form-group" style="margin-top:20px;">
                                            <asp:LinkButton ID="lbtnSelectReqList" runat="server" CssClass="btn btn-primary btn-sm"
                                                OnClick="lbtnSelectReqList_Click"><span class="fa fa-check"></span></asp:LinkButton>

                                            <asp:LinkButton ID="lnkSelectAll" runat="server" CssClass="btn btn-primary btn-sm" OnClick="lnkSelectAll_Click"><span class="fa fa-check-double"></span></asp:LinkButton>
                                                    <asp:CheckBox ID="CheckBoxStockalert" AutoPostBack="true" OnCheckedChanged="CheckBoxStockalert_CheckedChanged" runat="server" Text="Stock Alert" />
                                        </div>
                                            </div>
                                        <div class="col-md-1">
                                             <div class="form-group">
                                            <asp:Label ID="lblPage" runat="server" CssClass="label" Visible="false">Page size</asp:Label>
                                            <asp:DropDownList ID="ddlpagesize" runat="server" AutoPostBack="True"
                                                CssClass="form-control form-control-sm" OnSelectedIndexChanged="ddlpagesize_SelectedIndexChanged" Visible="False">
                                                <asp:ListItem Value="10">10</asp:ListItem>
                                                <asp:ListItem Value="15">15</asp:ListItem>
                                                <asp:ListItem Value="20">20</asp:ListItem>
                                                <asp:ListItem Value="30">30</asp:ListItem>
                                                <asp:ListItem Value="50">50</asp:ListItem>
                                                <asp:ListItem Value="100">100</asp:ListItem>
                                                <asp:ListItem Value="150">150</asp:ListItem>
                                                <asp:ListItem Value="200">200</asp:ListItem>
                                                <asp:ListItem Value="300">300</asp:ListItem>
                                            </asp:DropDownList>
                                                  </div>
                                        </div>
                                                                       
                                </div>
                            

                        </asp:Panel>  
                    </div>
              </div>
             <div class="card card-fluid">
                  <div class="card-body" style="min-height:400px;">
                    <div class="row">
                        
                        <asp:Panel ID="IssuePanel" runat="server">
                            <asp:GridView ID="gvMatIssue" runat="server" AllowPaging="True" OnRowDataBound="gvMatIssue_RowDataBound"
                                AutoGenerateColumns="False" OnPageIndexChanging="gvMatIssue_PageIndexChanging" OnRowDeleting="gvMatIssue_RowDeleting"
                                ShowFooter="True" CssClass="table-striped table-hover table-bordered grvContentarea">
                                <PagerSettings Position="Top" />
                                <RowStyle />
                                <Columns>
                                    <asp:TemplateField HeaderText="Sl">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvSlNo" runat="server" Font-Bold="True" Height="16px"
                                                Style="text-align: right"
                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:CommandField ShowDeleteButton="True" ControlStyle-CssClass="text-red" DeleteText="<span class='fa fa-trash'></span>" />
                                    <asp:TemplateField HeaderText="Req No1" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lgcIsuno1" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "preqno")) %>'
                                                Width="100px"></asp:Label>
                                        </ItemTemplate>

                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Req No">
                                        <ItemTemplate>
                                            <asp:Label ID="lgcIsuno" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "preqno1")) %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>

                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Order Description" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lgcBatDesc" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "bactdesc")) %>'
                                                Width="150px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />

                                    </asp:TemplateField>
                                      <asp:TemplateField HeaderText="Supplier">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvSupplier" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "ssirdesc")) %>'
                                                Width="100px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Store Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvActdesc" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc")) %>'
                                                Width="150px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Process Description">
                                        <ItemTemplate>
                                            <asp:Label ID="lgcproDesc" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "prodesc")) %>'
                                                Width="130px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Materials Description">
                                        <ItemTemplate>
                                            <asp:Label ID="lgcMatDesc" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rsirdesc")) %>'
                                                Width="150px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        <FooterStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Specification">
                                        <ItemTemplate>
                                            <asp:Label ID="lgcspcfDesc" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "spcfdesc")) %>'
                                                Width="80px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Cons <br> P. Pair">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvConPair" runat="server" Style="text-align: right"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "consppair")).ToString("#,##0.000000;(#,##0.000000); ") %>'
                                                Width="50px"></asp:Label>
                                        </ItemTemplate>

                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        <ItemStyle HorizontalAlign="right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Inv. Unit ">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvlbUnit" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rsirunit")) %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="left" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Inv. Qty">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvstockqty" runat="server" Style="text-align: right"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "stockqty")).ToString("#,##0.0000;(#,##0.0000); ") %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>

                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        <ItemStyle HorizontalAlign="right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Req. Qty">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvcBalqty" runat="server" Style="text-align: right"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "reqqty")).ToString("#,##0.0000;(#,##0.0000); ") %>'
                                                Width="60px"></asp:Label>
                                        </ItemTemplate>

                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        <ItemStyle HorizontalAlign="right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Rem. Issue Qty" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvcRemqty" runat="server" Style="text-align: right"
                                                Text='<%# (Convert.ToDouble(DataBinder.Eval(Container.DataItem, "reqqty"))-Convert.ToDouble(DataBinder.Eval(Container.DataItem, "conqty"))).ToString("#,##0.0000;(#,##0.0000); ") %>'
                                                Width="60px"></asp:Label>
                                        </ItemTemplate>

                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        <ItemStyle HorizontalAlign="right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Issue Qty">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvconqty" runat="server" BorderStyle="Solid" BorderWidth="1" BorderColor="#8492ed"
                                                Style="text-align: right; font-size: 11px;"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "conqty")).ToString("#,##0.0000;(#,##0.0000); ") %>'
                                                Width="60px"></asp:TextBox>
                                        </ItemTemplate>


                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Con Unit">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvConunit" runat="server"
                                                Style="font-size: 11px; text-align: center;"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "conuntdesc")) %>'
                                                Width="50px"></asp:Label>
                                            <asp:Label ID="lblConunt" Visible="false" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "conunt")) %>'></asp:Label>
                                            <asp:Label ID="lbluntcod" Visible="false" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "untcod")) %>'></asp:Label>

                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>



                                    <asp:TemplateField HeaderText="Conv. Qty">
                                        <ItemTemplate>

                                            <asp:TextBox ReadOnly="true" ID="lgvIsuQty" runat="server" Style="font-size: 11px; text-align: right"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "isuqty")).ToString("#,##0.0000;(#,##0.0000); ") %>'
                                                Width="70px" BorderStyle="None"></asp:TextBox>
                                        </ItemTemplate>

                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        <ItemStyle HorizontalAlign="right" />

                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Issue Rate" Visible="false">
                                        <ItemTemplate>

                                            <asp:TextBox ID="lgvIsuRate" runat="server" Style="font-size: 11px; text-align: right"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "isurate")).ToString("#,##0.0000;(#,##0.0000); ") %>'
                                                Width="70px" BorderStyle="None"></asp:TextBox>
                                        </ItemTemplate>

                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        <ItemStyle HorizontalAlign="right" />

                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Bal Stock">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvbalstkqty" runat="server" Style="text-align: right"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "balstkqty")).ToString("#,##0.0000;(#,##0.0000); ") %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>

                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        <ItemStyle HorizontalAlign="right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Batch">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="LbtnBatchUpdate" OnClick="LbtnBatchUpdate_Click" CssClass="text-green" runat="server"><span class="fa fa-edit"></span></asp:LinkButton>
                                        </ItemTemplate>
                                        <ItemStyle Width="30px" HorizontalAlign="Center" />
                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderText=" Id" Visible="false">
                                        <ItemTemplate>

                                            <asp:Label ID="lblrsircode" runat="server" Visible="false" Height="16px" Font-Size="11px"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rsircode")) %>'
                                                Width="80px"></asp:Label>
                                            <asp:Label ID="lblspcfcod" runat="server" Height="16px" Font-Size="11px"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "spcfcod")) %>'
                                                Width="80px"></asp:Label>



                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>



                                </Columns>
                                <FooterStyle CssClass="grvFooter" />
                                <EditRowStyle />
                                <AlternatingRowStyle />
                                <PagerStyle CssClass="gvPagination" />
                                <HeaderStyle CssClass="grvHeader" />
                            </asp:GridView>
                        </asp:Panel>
                        <asp:Panel ID="StockAlertPanel" runat="server" Visible="false">
                            <asp:GridView ID="gvStockAlert" runat="server" AutoGenerateColumns="False"
                                ShowFooter="True" CssClass="table-striped table-hover table-bordered grvContentarea">
                                <PagerSettings Position="Top" />
                                <RowStyle />
                                <Columns>
                                    <asp:TemplateField HeaderText="Sl">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvSlNo" runat="server" Font-Bold="True" Height="16px"
                                                Style="text-align: right"
                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Req No1" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lgcIsuno1" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "preqno")) %>'
                                                Width="100px"></asp:Label>
                                        </ItemTemplate>

                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Req No" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lgcIsuno" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "preqno1")) %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>

                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Order Description" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lgcBatDesc" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "bactdesc")) %>'
                                                Width="150px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />

                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Store Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvActdesc" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc")) %>'
                                                Width="100px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Process Description" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lgcproDesc" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "prodesc")) %>'
                                                Width="130px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Materials Description">
                                        <ItemTemplate>
                                            <asp:Label ID="lgcMatDesc" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rsirdesc")) %>'
                                                Width="150px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        <FooterStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Specification">
                                        <ItemTemplate>
                                            <asp:Label ID="lgcspcfDesc" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "spcfdesc")) %>'
                                                Width="80px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Inv. Unit ">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvlbUnit" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rsirunit")) %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="left" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Inv. Qty">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvstockqty" runat="server" Style="text-align: right"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "stockqty")).ToString("#,##0.0000;(#,##0.0000); ") %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>

                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        <ItemStyle HorizontalAlign="right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Req. Qty">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvcBalqty" runat="server" Style="text-align: right"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "isuqty")).ToString("#,##0.0000;(#,##0.0000); ") %>'
                                                Width="60px"></asp:Label>
                                        </ItemTemplate>

                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        <ItemStyle HorizontalAlign="right" />
                                    </asp:TemplateField>




                                    <asp:TemplateField HeaderText=" Id" Visible="false">
                                        <ItemTemplate>

                                            <asp:Label ID="lblrsircode" runat="server" Visible="false" Height="16px" Font-Size="11px"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rsircode")) %>'
                                                Width="80px"></asp:Label>
                                            <asp:Label ID="lblspcfcod" runat="server" Height="16px" Font-Size="11px"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "spcfcod")) %>'
                                                Width="80px"></asp:Label>



                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>



                                </Columns>
                                <FooterStyle CssClass="grvFooter" />
                                <EditRowStyle />
                                <AlternatingRowStyle />
                                <PagerStyle CssClass="gvPagination" />
                                <HeaderStyle CssClass="grvHeader" />
                            </asp:GridView>
                        </asp:Panel>

                        <asp:Panel ID="PnlProRemarks" runat="server" Visible="False">
                            <fieldset class="scheduler-border fieldset_A">

                                <div class="form-horizontal">
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <asp:Label ID="Label12" runat="server" CssClass="lblName lblTxt"
                                                Text="Remarks:"></asp:Label>
                                            <asp:TextBox ID="txtRemarks" runat="server" CssClass="form-control" Height="45px"
                                                TextMode="MultiLine" Width="400px"></asp:TextBox>

                                        </div>
                                    </div>
                                </div>
                            </fieldset>




                        </asp:Panel>
                    </div>

                </div>
            </div>


            <div id="SizeModal" class="modal animated slideInLeft" role="dialog">
                <div class="modal-dialog UpdateMOdel modal-lg">
                    <div class="modal-content ">
                        <div class="modal-header">
                            <button type="button" style="background-color: red;" class="close btn btn-xs" data-dismiss="modal"><span class="fa fa-close"></span></button>
                            <h4 class="modal-title"><span class="fa fa-table"></span>
                                <asp:Label ID="ModalHead" runat="server"></asp:Label>
                            </h4>
                        </div>
                        <div class="modal-body form-horizontal table-responsive">
                            <asp:GridView ID="gvsizes" runat="server" AutoGenerateColumns="False" Height="1px" PageSize="15"
                                ShowFooter="True" Width="253px" CssClass=" table-striped table-hover table-bordered grvContentarea" Font-Size="11px">

                                <Columns>
                                    <asp:TemplateField HeaderText="Style ID" Visible="False">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvStyleID" runat="server" Style="text-transform: capitalize" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "styleid")) %>'
                                                Width="51px"></asp:Label>
                                            <asp:Label ID="mlblgvSlnum" runat="server" Height="16px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "slnum")) %>'
                                                Width="30px"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Color ID" Visible="False">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvColorID" runat="server" Style="text-transform: capitalize" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "styleid")) %>'
                                                Width="51px"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="TOD Date">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvSipmentdate" Font-Bold="true" runat="server" Style="font-size: 11px;"
                                                Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "shimentdate")).ToString("dd-MMM-yyyy") %>'
                                                Width="100px"></asp:Label>

                                        </ItemTemplate>

                                        <FooterStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    
                                    <asp:TemplateField HeaderText="Unit" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvStyleUnit" runat="server" Style="text-transform: capitalize"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "StyleUnit")) %>'
                                                Width="40px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Size-01" Visible="False">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvF1" runat="server" BackColor="Transparent" BorderColor="#6acc39"
                                                BorderStyle="Solid" BorderWidth="1px"
                                                Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s1")).ToString("###0;(###0); ") %>'
                                                Width="40px"></asp:TextBox>
                                        </ItemTemplate>

                                        <FooterStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Size-02" Visible="False">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvF2" runat="server" BackColor="Transparent" BorderColor="#6acc39"
                                                BorderStyle="Solid" BorderWidth="1px" Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s2")).ToString("###0;(###0); ") %>'
                                                Width="40px"></asp:TextBox>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Size-03" Visible="False">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvF3" runat="server" BackColor="Transparent" BorderColor="#6acc39"
                                                BorderStyle="Solid" BorderWidth="1px"
                                                Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s3")).ToString("###0;(###0); ") %>'
                                                Width="40px"></asp:TextBox>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Size-04" Visible="False">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvF4" runat="server" BackColor="Transparent" BorderColor="#6acc39"
                                                BorderStyle="Solid" BorderWidth="1px"
                                                Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s4")).ToString("###0;(###0); ") %>'
                                                Width="40px"></asp:TextBox>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Size-05" Visible="False">
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


                                    <asp:TemplateField HeaderText="Size-16" Visible="False">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvF16" runat="server" BackColor="Transparent" BorderColor="#6acc39"
                                                BorderStyle="Solid" BorderWidth="1px"
                                                Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s16")).ToString("###0;(###0); ") %>'
                                                Width="40px"></asp:TextBox>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Size-17" Visible="False">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvF17" runat="server" BackColor="Transparent" BorderColor="#6acc39"
                                                BorderStyle="Solid" BorderWidth="1px"
                                                Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s17")).ToString("###0;(###0); ") %>'
                                                Width="40px"></asp:TextBox>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Size-18" Visible="False">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvF18" runat="server" BackColor="Transparent" BorderColor="#6acc39"
                                                BorderStyle="Solid" BorderWidth="1px"
                                                Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s18")).ToString("###0;(###0); ") %>'
                                                Width="40px"></asp:TextBox>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Size-19" Visible="False">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvF19" runat="server" BackColor="Transparent" BorderColor="#6acc39"
                                                BorderStyle="Solid" BorderWidth="1px"
                                                Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s19")).ToString("###0;(###0); ") %>'
                                                Width="40px"></asp:TextBox>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Size-20" Visible="False">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvF20" runat="server" BackColor="Transparent" BorderColor="#6acc39"
                                                BorderStyle="Solid" BorderWidth="1px"
                                                Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s20")).ToString("###0;(###0); ") %>'
                                                Width="40px"></asp:TextBox>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Size-21" Visible="False">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvF21" runat="server" BackColor="Transparent" BorderColor="#6acc39"
                                                BorderStyle="Solid" BorderWidth="1px"
                                                Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s21")).ToString("###0;(###0); ") %>'
                                                Width="40px"></asp:TextBox>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Size-22" Visible="False">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvF22" runat="server" BackColor="Transparent" BorderColor="#6acc39"
                                                BorderStyle="Solid" BorderWidth="1px"
                                                Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s22")).ToString("###0;(###0); ") %>'
                                                Width="40px"></asp:TextBox>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Size-23" Visible="False">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvF23" runat="server" BackColor="Transparent" BorderColor="#6acc39"
                                                BorderStyle="Solid" BorderWidth="1px"
                                                Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s23")).ToString("###0;(###0); ") %>'
                                                Width="40px"></asp:TextBox>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Size-24" Visible="False">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvF24" runat="server" BackColor="Transparent" BorderColor="#6acc39"
                                                BorderStyle="Solid" BorderWidth="1px"
                                                Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s24")).ToString("###0;(###0); ") %>'
                                                Width="40px"></asp:TextBox>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Size-25" Visible="False">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvF25" runat="server" BackColor="Transparent" BorderColor="#6acc39"
                                                BorderStyle="Solid" BorderWidth="1px"
                                                Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s25")).ToString("###0;(###0); ") %>'
                                                Width="40px"></asp:TextBox>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Size-26" Visible="False">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvF26" runat="server" BackColor="Transparent" BorderColor="#6acc39"
                                                BorderStyle="Solid" BorderWidth="1px"
                                                Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s26")).ToString("###0;(###0); ") %>'
                                                Width="40px"></asp:TextBox>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Size-27" Visible="False">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvF27" runat="server" BackColor="Transparent" BorderColor="#6acc39"
                                                BorderStyle="Solid" BorderWidth="1px"
                                                Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s27")).ToString("###0;(###0); ") %>'
                                                Width="40px"></asp:TextBox>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Size-28" Visible="False">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvF28" runat="server" BackColor="Transparent" BorderColor="#6acc39"
                                                BorderStyle="Solid" BorderWidth="1px"
                                                Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s28")).ToString("###0;(###0); ") %>'
                                                Width="40px"></asp:TextBox>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Size-29" Visible="False">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvF29" runat="server" BackColor="Transparent" BorderColor="#6acc39"
                                                BorderStyle="Solid" BorderWidth="1px"
                                                Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s29")).ToString("###0;(###0); ") %>'
                                                Width="40px"></asp:TextBox>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Size-30" Visible="False">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvF30" runat="server" BackColor="Transparent" BorderColor="#6acc39"
                                                BorderStyle="Solid" BorderWidth="1px"
                                                Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s30")).ToString("###0;(###0); ") %>'
                                                Width="40px"></asp:TextBox>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Size-31" Visible="False">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvF31" runat="server" BackColor="Transparent" BorderColor="#6acc39"
                                                BorderStyle="Solid" BorderWidth="1px"
                                                Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s31")).ToString("###0;(###0); ") %>'
                                                Width="40px"></asp:TextBox>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Size-32" Visible="False">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvF32" runat="server" BackColor="Transparent" BorderColor="#6acc39"
                                                BorderStyle="Solid" BorderWidth="1px"
                                                Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s32")).ToString("###0;(###0); ") %>'
                                                Width="40px"></asp:TextBox>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Size-33" Visible="False">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvF33" runat="server" BackColor="Transparent" BorderColor="#6acc39"
                                                BorderStyle="Solid" BorderWidth="1px"
                                                Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s33")).ToString("###0;(###0); ") %>'
                                                Width="40px"></asp:TextBox>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Size-34" Visible="False">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvF34" runat="server" BackColor="Transparent" BorderColor="#6acc39"
                                                BorderStyle="Solid" BorderWidth="1px"
                                                Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s34")).ToString("###0;(###0); ") %>'
                                                Width="40px"></asp:TextBox>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Size-35" Visible="False">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvF35" runat="server" BackColor="Transparent" BorderColor="#6acc39"
                                                BorderStyle="Solid" BorderWidth="1px"
                                                Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s35")).ToString("###0;(###0); ") %>'
                                                Width="40px"></asp:TextBox>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Size-36" Visible="False">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvF36" runat="server" BackColor="Transparent" BorderColor="#6acc39"
                                                BorderStyle="Solid" BorderWidth="1px"
                                                Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s36")).ToString("###0;(###0); ") %>'
                                                Width="40px"></asp:TextBox>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Size-37" Visible="False">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvF37" runat="server" BackColor="Transparent" BorderColor="#6acc39"
                                                BorderStyle="Solid" BorderWidth="1px"
                                                Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s37")).ToString("###0;(###0); ") %>'
                                                Width="40px"></asp:TextBox>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Size-38" Visible="False">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvF38" runat="server" BackColor="Transparent" BorderColor="#6acc39"
                                                BorderStyle="Solid" BorderWidth="1px"
                                                Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s38")).ToString("###0;(###0); ") %>'
                                                Width="40px"></asp:TextBox>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Size-39" Visible="False">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvF39" runat="server" BackColor="Transparent" BorderColor="#6acc39"
                                                BorderStyle="Solid" BorderWidth="1px"
                                                Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s39")).ToString("###0;(###0); ") %>'
                                                Width="40px"></asp:TextBox>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Size-40" Visible="False">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvF40" runat="server" BackColor="Transparent" BorderColor="#6acc39"
                                                BorderStyle="Solid" BorderWidth="1px"
                                                Font-Size="11px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "s40")).ToString("###0;(###0); ") %>'
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
                                    <asp:TemplateField HeaderText="Trial Order QTY">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvColTotal1" Font-Bold="true" runat="server" Style="font-size: 11px; text-align: right"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "colqty")).ToString("#,##0;(#,##0); ") %>'
                                                Width="80px"></asp:Label>

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
                        <div class="modal-footer ">
                            
                            <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                        </div>
                    </div>
                </div>
            </div>



            <!-- line modal -->
            <div id="BatchModal" class="modal animated slideInLeft" role="dialog">
                <div class="modal-dialog modal-lg">
                    <div class="modal-content ">
                        <div class="modal-header">
                            <button type="button" style="background-color: red;" class="close btn btn-xs" data-dismiss="modal"><span class="fa fa-close"></span></button>
                            <h4 class="modal-title"><span class="fa fa-table"></span>Details Information </h4>
                        </div>
                        <div class="modal-body form-horizontal">

                            <asp:GridView ID="gvBatchDetails" runat="server" AutoGenerateColumns="False"
                                ShowFooter="True" CssClass=" table-striped table-hover table-bordered grvContentarea">
                                <PagerSettings Visible="False" />

                                <Columns>

                                    <asp:TemplateField HeaderText="SL">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvSlNo" runat="server" Height="16px" Style="text-align: right"
                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="25px"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Lot/Batch">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvbatch1" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "batch1")) %>'
                                                Width="90px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Date">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvindate1" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "indate1")) %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterStyle HorizontalAlign="Center" />
                                        <HeaderStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <%-- <asp:TemplateField HeaderText="Unit">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvResUnit" runat="server"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rsirunit")) %>'
                                                                Width="25px"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>--%>



                                    <asp:TemplateField HeaderText="Stock Qty">
                                        <ItemTemplate>
                                            <asp:Label ID="txtgvbalqty" Style="text-align: right;" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "balqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="75px"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Issue Qty">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvqty" Style="text-align: right;" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="75px"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>

                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkack" runat="server"
                                                Checked='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "approved"))=="True" ? true : false %>'
                                                Enabled='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "approved"))=="True" ? false : true%>'
                                                Width="20px" />
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="Center" />
                                        <FooterStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>



                                    <asp:TemplateField HeaderText=" Id" Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvbatch" runat="server" Visible="false" Height="16px" Font-Size="11px"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "batch")) %>'
                                                Width="80px"></asp:Label>
                                            <asp:Label ID="lblgvstoreid" runat="server" Visible="false" Height="16px" Font-Size="11px"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "storeid")) %>'
                                                Width="80px"></asp:Label>
                                            <asp:Label ID="lblrsircode" runat="server" Visible="false" Height="16px" Font-Size="11px"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rsircode")) %>'
                                                Width="80px"></asp:Label>
                                            <asp:Label ID="lblspcfcod" runat="server" Height="16px" Font-Size="11px"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "spcfcod")) %>'
                                                Width="80px"></asp:Label>



                                        </ItemTemplate>
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
                        <div class="modal-footer ">
                            <asp:LinkButton ID="ModalUpdateBtn" OnClick="ModalUpdateBtn_Click" OnClientClick="CLoseMOdal();"
                                runat="server" CssClass="btn btn-primary"> <span class="glyphicon glyphicon-saved"></span> Update</asp:LinkButton>
                            <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                        </div>
                    </div>
                </div>
            </div>


        </ContentTemplate>
    </asp:UpdatePanel>
    <%--  </asp:UpdatePanel>--%>
</asp:Content>

