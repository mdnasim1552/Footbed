<%@ Page Title="" Language="C#" MasterPageFile="~/SPE.Master" AutoEventWireup="true" CodeBehind="AddProdReq.aspx.cs" Inherits="SPEWEB.F_15_Pro.AddProdReq" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <style type="text/css">
        .chzn-single {
            border-radius: 3px !important;
            height: 29px !important;
        }

        .chzn-drop {
            width: 100% !important;
        }

        .chzn-search input {
            width: 100% !important;
        }
    </style>

    <script type="text/javascript">

        $(document).ready(function () {
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);
        });


        function pageLoaded() {
            var gvCost = $('#<%=this.gvCost.ClientID %>');
            gvCost.Scrollable();

            $('.chzn-select').chosen({
                search_contains: true,

            });
        }

    </script>

    <%--  <script language="javascript" type="text/javascript">
        function GoToNextTextBox(currentTxtId, e) {

            if (e.keyCode == 13 || e.keyCode == 40) {

                var number = parseInt(currentTxtId.id.substring(46));

                var nextId = number + 1;

                var nextIdString = "ContentPlaceHolder1_gvBudgetRpt_lblgvRProdqty_" + nextId.toString();

                var x = document.getElementById(nextIdString);
                x.focus();
            }
            else
                if (e.keyCode == 38) {
                    var number = parseInt(currentTxtId.id.substring(46));
                    var nextId = number - 1;

                    var nextIdString = "ContentPlaceHolder1_gvBudgetRpt_lblgvRProdqty_" + nextId.toString();

                    var x = document.getElementById(nextIdString);
                    x.focus();
                }

        }
    </script>--%>



    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
                     <div class="nahidProgressbar">
                <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1" DisplayAfter="30">
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

            <div class="card card-fluid mb-1">
                <div class="card-body py-3">

                    <div class="row">

                        <div class="col-md-1">
                            <asp:Label ID="lblSeason" runat="server" CssClass="">Season</asp:Label>
                            <asp:DropDownList ID="ddlSeason" runat="server" CssClass="form-control form-control-sm chzn-select" AutoPostBack="true" OnSelectedIndexChanged="ddlSeason_SelectedIndexChanged">
                            </asp:DropDownList>
                        </div>

                        <div class="col-md-4">
                            <asp:Label ID="lnkbtnMasterLc" runat="server" CssClass="" Text=" Master LC No"> </asp:Label>
                            <asp:DropDownList ID="ddlBatch" runat="server" CssClass="form-control form-control-sm chzn-select" AutoPostBack="true" OnSelectedIndexChanged="ddlBatch_SelectedIndexChanged">
                            </asp:DropDownList>
                            <%--<asp:TextBox ID="txtBatSch" Visible="false" runat="server" CssClass="inputTxt inputName inpPixedWidth"></asp:TextBox>--%>
                            <%--<asp:LinkButton ID="imgbtnBatsearch" Visible="false" runat="server" CssClass="btn btn-primary srearchBtn colMdbtn"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>--%>
                            <%--<asp:Label ID="lblBatch" runat="server" CssClass="lblTxt smLbl_to dataLblview" Width="300px" Visible="false"> </asp:Label>--%>
                        </div>

                        <div class="col-md-1">
                            <asp:Label ID="lblDate" runat="server" CssClass=""> Date</asp:Label>
                            <asp:TextBox ID="txtbgddate" runat="server" CssClass="form-control form-control-sm px-0"></asp:TextBox>

                            <asp:CalendarExtender ID="txtbgddate_CalendarExtender" runat="server"
                                Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txtbgddate"></asp:CalendarExtender>
                        </div>

                        <div class="col-md-2">
                            <asp:Label ID="lblCurReqNo1" runat="server" CssClass="">DPR</asp:Label>
                            <asp:DropDownList ID="ddlPreqno" runat="server" CssClass="form-control form-control-sm chzn-select">
                            </asp:DropDownList>
                        </div>

                        <div class="col-md-1">
                            <asp:LinkButton ID="lbtnOk" runat="server" CssClass="btn btn-sm btn-primary" Style="margin-top: 20px;" OnClick="lbtnOk_Click">Ok</asp:LinkButton>
                        </div>
                    </div>

                    <asp:Panel ID="CommonMaterialPanel" Visible="false" runat="server">
                        <div class="row" style="margin-top: 10px">

                            <div class="col-md-2">
                                <div class="form-group">
                                    <asp:Label ID="Label2" runat="server" CssClass="label">Material Group</asp:Label>
                                    <asp:DropDownList ID="ddlcatagory" OnSelectedIndexChanged="ddlcatagory_SelectedIndexChanged" AutoPostBack="true" runat="server" CssClass="chzn-select form-control form-control-sm">
                                    </asp:DropDownList>
                                </div>
                            </div>


                            <div class="col-md-3">
                                <div class="form-group">
                                    <asp:Label ID="lblResList" runat="server" CssClass="" Text="Material List"></asp:Label>
                                    <asp:DropDownList ID="ddlResList" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlResList_SelectedIndexChanged" Width="318px" CssClass="ddlPage chzn-select">
                                    </asp:DropDownList>
                                </div>
                            </div>

                            <div class="col-md-3" style="margin-left: -20px">
                                <div class="form-group">
                                    <asp:Label ID="lblSpecification" runat="server" CssClass="" Text="Specification"></asp:Label>
                                    <asp:DropDownList ID="ddlResSpcf" runat="server" Width="330px" CssClass="form-control form-control-sm chzn-select"></asp:DropDownList>
                                </div>
                            </div>

                            <div class="col-md-2" style="margin-top: 20px">
                                <div class="form-group">
                                    <asp:LinkButton ID="lbtnSelect" runat="server" CssClass="btn btn-sm btn-primary" OnClick="lbtnSelect_Click">Select</asp:LinkButton>
                                    <asp:LinkButton ID="lbtnSelectAll" runat="server" Visible="false" CssClass="btn btn-sm btn-primary">Select All</asp:LinkButton>
                                </div>
                            </div>
                        </div>
                    </asp:Panel>
                </div>
            </div>

            <div class="card card-fluid">
                <div class="card-body pb-5" style="min-height: 450px;">

                    <asp:MultiView ID="MultiView1" runat="server">

                        <asp:View ID="ViewbudgetReport" runat="server">
                            <div class="table-responsive">
                                <asp:GridView ID="gvCost" runat="server" AutoGenerateColumns="False" CssClass="table-striped table-hover table-bordered grvContentarea" ShowFooter="True"
                                    OnRowDataBound="gvBudgetRpt_RowDataBound">
                                    <RowStyle />
                                    <Columns>
                                        <asp:TemplateField HeaderText="SL">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvSlNo1c" runat="server" Font-Bold="True" Style="text-align: center"
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Material Group">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvcResCode" runat="server" Font-Size="10px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "prodesc")) %>'
                                                    Width="150px"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Description Of Item">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvcResDescRpt" runat="server" AutoCompleteType="Disabled" BackColor="Transparent"
                                                    BorderStyle="None" Font-Size="10px" Text='<%# 
                                                                         (DataBinder.Eval(Container.DataItem, "prodesc").ToString().Trim().Length>0 ? 
                                                                     "<B>"+Convert.ToString(DataBinder.Eval(Container.DataItem, "prodesc")).Trim()+"</B>": "")  +                                                                                                                                             
                                                                          (Convert.ToString(DataBinder.Eval(Container.DataItem, "prodesc")).Trim().Length>0 ? "<br>"+
                                                                          Convert.ToString(DataBinder.Eval(Container.DataItem, "rsirdesc")).Trim():Convert.ToString(DataBinder.Eval(Container.DataItem, "rsirdesc")).Trim())                                                                
                                                                        
                                                                    %>'
                                                    Width="250px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Unit">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvcResUnitRpt" runat="server" Style="text-align: center" Font-Size="10px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rsirunit")) %>'
                                                    Width="40px"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Specification">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvspcfdesc" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "spcfdesc")) %>'
                                                    Width="200px" Font-Size="9px"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>


                                        <%--     <asp:TemplateField HeaderText="Budgeted</br> Quantity">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvcRBgdqty" runat="server" Font-Size="10px" Style="text-align: right;"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "bgdqty")).ToString("#,##0.0000;(#,##0.0000); ") %>'
                                                        Width="50px"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Upto</br> Date</br> Position">
                                                <FooterTemplate>
                                                    <asp:LinkButton ID="lbtnTotalMat" Visible="false" runat="server" CssClass="btn btn-primary primaryBtn" OnClick="lbtnTotalMat_Click">Total</asp:LinkButton>
                                                </FooterTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvcRIssqty" runat="server" Font-Size="10px" Style="text-align: right;"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "issqty")).ToString("#,##0.0000;(#,##0.0000); ") %>'
                                                        Width="50px"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Budget</br> Balance">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvcRBbqty" runat="server" Font-Size="10px" Style="text-align: right;"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "bbqty")).ToString("#,##0.0000;(#,##0.0000); ") %>'
                                                        Width="50px"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>--%>

                                        <asp:TemplateField HeaderText="This</br> Requisition">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvThisReq" runat="server" Style="text-align: right" 
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "currqqty")).ToString("#,##0.0000;(#,##0.0000); ") %>'
                                                    Width="60px" Font-Size="9px"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Total</br> Requisition">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvTotalReq" runat="server" Style="text-align: right" 
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ttlreqqty")).ToString("#,##0.0000;(#,##0.0000); ") %>'
                                                    Width="60px" Font-Size="9px"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Total</br> Recutting">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvRecut" runat="server" Style="text-align: right" 
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ttlrecutqty")).ToString("#,##0.0000;(#,##0.0000); ") %>'
                                                    Width="60px" Font-Size="9px"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Total</br> Additional">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvTotEdi" runat="server" Style="text-align: right" 
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ttladition")).ToString("#,##0.0000;(#,##0.0000); ") %>'
                                                    Width="60px"  Font-Size="9px"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Grand</br> Total">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvGrndTotal" runat="server" Style="text-align: right" 
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "grandttl")).ToString("#,##0.0000;(#,##0.0000); ") %>'
                                                    Width="60px"  Font-Size="9px"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Percent (%)">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtgvcPercent" runat="server" Font-Size="10px" Style="text-align: right;"
                                                    BackColor="Transparent" BorderStyle="Solid" BorderWidth="1px" BorderColor="Tomato"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "percnt")).ToString("#,##0.0000;(#,##0.0000); ") %>'
                                                    Width="70px"></asp:TextBox>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" ForeColor="Green" />
                                            <HeaderStyle ForeColor="Green" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Requisition</br> Quantity">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtgvcRProdqty" runat="server" Font-Size="10px" Style="text-align: right;"
                                                    BackColor="Transparent" BorderStyle="Solid" BorderWidth="1px" BorderColor="Tomato"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "cresqty")).ToString("#,##0.0000;(#,##0.0000); ") %>'
                                                    Width="70px"></asp:TextBox>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" ForeColor="Green" />
                                            <HeaderStyle ForeColor="Green" />
                                        </asp:TemplateField>

                                        

                                        

                                        <%--        <asp:TemplateField HeaderText="Stock </br> Quantity">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvcstockqty" runat="server" Font-Size="9px" Style="text-align: right;"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "stockqty")).ToString("#,##0.0000;(#,##0.0000); ") %>'
                                                        Width="50px"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>--%>

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

