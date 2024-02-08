<%@ Page Title="" Language="C#" MasterPageFile="~/SPE.Master" AutoEventWireup="true" CodeBehind="ProdReqSemi.aspx.cs" Inherits="SPEWEB.F_15_Pro.ProdReqSemi" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript">

        $(document).ready(function () {
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);
        });
        function pageLoaded() {

            var gvBudgetRpt = $('#<%=this.gvBudgetRpt.ClientID %>');
            gvBudgetRpt.Scrollable();

            var gvCost = $('#<%=this.gvCost.ClientID %>');
            gvCost.Scrollable();

            $('.chzn-select').chosen({ search_contains: true });
        }

    </script>

    <script language="javascript" type="text/javascript">
        function GoToNextTextBox(currentTxtId, e) {

            if (e.keyCode == 13 || e.keyCode == 40) {

                var number = parseInt(currentTxtId.id.substring(46));
                //alert((currentTxtId.id));
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
    </script>



    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="card card-fluid">
                <div class="card-body">
                    <div class="row">
                        <div class="col-md-1 col-sm-1 col-lg-1 ">
                            <div class="form-group">
                                <asp:Label ID="lblProcess2" runat="server" CssClass="label">Date</asp:Label>
                                <asp:TextBox ID="txtbgddate" runat="server" CssClass="form-control form-control-sm small"></asp:TextBox>
                                <cc1:calendarextender id="Calendarextender1" runat="server" format="dd-MMM-yyyy" targetcontrolid="txtbgddate"></cc1:calendarextender>
                            </div>
                        </div>
                        <div class="col-md-2 col-sm-2 col-lg-2 ">
                            <div class="form-group">
                                <asp:Label ID="lblcurVounum" runat="server" CssClass="label">DPR No</asp:Label>
                                <div class="form-inline">
                                    <asp:Label ID="lblCurReqNo1" Text="DPR00-" runat="server" Style="width: 50%;" CssClass="form-control form-control-sm small" ReadOnly="True"></asp:Label>
                                    <asp:TextBox ID="txtCurReqNo2" Text="00000" runat="server" Style="width: 50%;" CssClass="form-control form-control-sm  small" ReadOnly="True"></asp:TextBox>

                                    <asp:TextBox ID="txtbatchno1" runat="server" CssClass="inputTxt inputName inpPixedWidth" Visible="false"></asp:TextBox>
                                    <asp:LinkButton ID="imgbtnPreDPR0" runat="server" CssClass="btn btn-primary srearchBtn colMdbtn" Visible="false"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-2 col-sm-2 col-lg-2">
                            <div class="form-group">
                                <asp:LinkButton ID="Batsearch_Click" runat="server" CssClass="label" OnClick="imgbtnBatsearch_Click">Batch No</asp:LinkButton>
                                <asp:HyperLink ID="Semiwip" runat="server" Target="_blank" NavigateUrl="~/F_34_Mgt/AccWIPCode.aspx?Type=FgSeWIP" CssClass="fa fa-plus-circle"></asp:HyperLink>

                                <asp:DropDownList ID="ddlBatch" runat="server" CssClass="chzn-select form-control form-control-sm"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-1 col-sm-1 col-lg-1 ">

                            <div class="form-group">
                                <asp:LinkButton ID="imgbtnsearch" OnClick="imgbtnsearch_Click" runat="server" CssClass="label">Bgd. No</asp:LinkButton>
                                <asp:DropDownList ID="ddlProdNo" runat="server" CssClass="chzn-select form-control form-control-sm"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-1 col-sm-1 col-lg-1 ">
                            <div class="form-group" style="margin-top: 20px;">
                                <asp:LinkButton ID="lbtnOk" runat="server" CssClass="btn btn-primary btn-sm pull-left" OnClick="lbtnOk_Click">Ok</asp:LinkButton>

                            </div>
                        </div>
                        <div class="col-md-2 col-sm-2 col-lg-2">

                            <div class="form-group">

                                <asp:LinkButton ID="ibtnFindPrv" runat="server" CssClass="control-label" OnClick="imgbtnPreDPR_Click">Previous List</asp:LinkButton>
                                <asp:DropDownList ID="ddlDPR" runat="server" CssClass="form-control form-control-sm" OnSelectedIndexChanged="ddlDPR_SelectedIndexChanged" AutoPostBack="true">
                                </asp:DropDownList>
                            </div>


                        </div>


                    </div>
                </div>
            </div>
            <div class="card card-fluid" style="min-height: 400px;">
                <div class="card-body">
                    <asp:MultiView ID="MultiView1" runat="server">



                        <asp:View ID="ViewbudgetReport" runat="server">
                            <asp:Panel runat="server" ID="Paneldd" Visible="false">
                                <fieldset class="scheduler-border fieldset_A">
                                    <div class="form-horizontal">
                                        <div class="form-group">
                                            <div class="col-md-3 pading5px asitCol3">
                                                <asp:Label ID="lblproduct0" runat="server" CssClass="lblTxt lblName">Produce</asp:Label>
                                                <asp:TextBox ID="txtsearchAllProduct" runat="server" CssClass="inputTxt inputName inpPixedWidth"></asp:TextBox>
                                                <asp:LinkButton ID="imgbtnsrchAllProduct" runat="server" CssClass="btn btn-primary srearchBtn colMdbtn" OnClick="imgbtnsrchAllProduct_Click"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>

                                            </div>

                                            <div class="col-md-4 pading5px ">
                                                <asp:DropDownList ID="ddlAllProduct" runat="server" CssClass="form-control inputTxt chzn-select">
                                                </asp:DropDownList>
                                            </div>

                                            <div class="col-md-2 pading5px">
                                                <div class="colMdbtn pading5px">
                                                    <asp:LinkButton ID="lbtnSelect1" runat="server" CssClass="btn btn-primary okBtn" OnClick="lbtnSelect1_Click">ok</asp:LinkButton>

                                                </div>



                                            </div>

                                        </div>

                                    </div>
                                </fieldset>
                            </asp:Panel>
                            <div class="row">
                                <div class="col-md-6">
                                    <asp:GridView ID="gvBudgetRpt" runat="server"
                                        AutoGenerateColumns="False" CssClass="table-striped table-hover table-bordered grvContentarea"
                                        ShowFooter="True" OnRowDataBound="gvBudgetRpt_RowDataBound">
                                        <RowStyle />
                                        <Columns>
                                            <asp:TemplateField HeaderText="Sl">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvSlNo1" runat="server" Font-Bold="True" Style="text-align: right"
                                                        Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="15px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Description Of Item">
                                                <ItemTemplate>

                                                    <asp:Label ID="lblgvResDescRpt" runat="server" AutoCompleteType="Disabled" BackColor="Transparent"
                                                        BorderStyle="None" Font-Size="10px"
                                                        Text='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "rsirdesc")).Trim() %>'
                                                        Width="220px"></asp:Label>

                                                    <%--<asp:Label ID="lblgvResDescRpt" runat="server" AutoCompleteType="Disabled" BackColor="Transparent"
                                                        BorderStyle="None" Font-Size="10px"
                                                        Text='<%#  "<B>"+ Convert.ToString(DataBinder.Eval(Container.DataItem, "grpdesc")) + "</B>"+
                                                                        (DataBinder.Eval(Container.DataItem, "prodesc").ToString().Trim().Length>0 ? 
                                                                         (Convert.ToString(DataBinder.Eval(Container.DataItem, "grpdesc")).Trim().Length>0 ?  "<br>" : "")+                                                             
                                                                         "&nbsp;&nbsp;&nbsp;&nbsp;"+ 
                                                                         "<B>"+Convert.ToString(DataBinder.Eval(Container.DataItem, "prodesc")).Trim()+"</B>": "")  + 
                                                                         (DataBinder.Eval(Container.DataItem, "rsirdesc").ToString().Trim().Length>0 ? 
                                                                          (Convert.ToString(DataBinder.Eval(Container.DataItem, "grpdesc")).Trim().Length>0 ?   "<br>" 
                                                                          :(Convert.ToString(DataBinder.Eval(Container.DataItem, "prodesc")).Trim().Length>0 ? "<br>":"")) + 
                                                                         "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"+ 
                                                                         Convert.ToString(DataBinder.Eval(Container.DataItem, "rsirdesc")).Trim(): "")
                                                                    %>'
                                                        Width="180px"></asp:Label>--%>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Unit">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvResUnitRpt" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rsirunit")) %>'
                                                        Width="35px"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Budgeted</br> Quantity">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvRBgdqty" runat="server" Font-Size="9px" Style="text-align: right;"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "bgdqty")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="45px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lblgvFRBgdqty" runat="server" Font-Bold="True"
                                                        Font-Size="9px" ForeColor="Black" Style="text-align: right" Width="45px"></asp:Label>
                                                </FooterTemplate>
                                                <ItemStyle HorizontalAlign="Right" />
                                                <HeaderStyle Font-Size="10px" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Upto </br>Date</br> Position">
                                                <%-- <FooterTemplate>
                                                    <asp:LinkButton ID="lbtnFinalProUpdate" runat="server" CssClass="btn btn-danger primaryBtn" OnClick="lbtnFinalProUpdate_Click">Update</asp:LinkButton>
                                                </FooterTemplate>--%>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvRIssqty" runat="server" Font-Size="9px" Style="text-align: right;"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "issqty")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="45px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lblgvFRIssqty" runat="server" Font-Bold="True"
                                                        Font-Size="9px" ForeColor="Black" Style="text-align: right" Width="45px"></asp:Label>
                                                </FooterTemplate>
                                                <ItemStyle HorizontalAlign="Right" />
                                                <HeaderStyle Font-Size="10px" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Budget</br> Balance">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvRBbqty" runat="server" Font-Size="9px" Style="text-align: right;"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "bbqty")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="45px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lblgvFRBbqty" runat="server" Font-Bold="True"
                                                        Font-Size="9px" ForeColor="Black" Style="text-align: right" Width="45px"></asp:Label>
                                                </FooterTemplate>
                                                <ItemStyle HorizontalAlign="Right" />
                                                <HeaderStyle Font-Size="10px" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Requisition </br> Quantity">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="lblgvRProdqty" runat="server" Font-Size="10px" Style="text-align: right;" OnKeyUp="GoToNextTextBox(this, event); return false;"
                                                        BackColor="Transparent" BorderStyle="None" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "cresqty")).ToString("#,##0;(#,##0); ") %>'
                                                        Width="45px"></asp:TextBox>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lblgvFRProdqty" runat="server" Font-Bold="True"
                                                        Font-Size="10px" ForeColor="Black" Style="text-align: right" Width="45px"></asp:Label>
                                                </FooterTemplate>
                                                <ItemStyle HorizontalAlign="Right" ForeColor="Red" />
                                                <HeaderStyle ForeColor="Red" Font-Size="10px" />
                                                <FooterStyle ForeColor="Red" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lblgvchlnqty" runat="server"  OnClick="lblgvchlnqty_Click"><span class="fa fa-times text-red btn-xs"></span></asp:LinkButton>
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
                                <div class="col-md-6">
                                    <asp:GridView ID="gvCost" runat="server" AutoGenerateColumns="False" CssClass="table-striped table-hover table-bordered grvContentarea" ShowFooter="True"
                                        OnRowDataBound="gvCost_RowDataBound">
                                        <RowStyle />
                                        <Columns>
                                            <asp:TemplateField HeaderText="Sl">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvSlNo1c" runat="server" Font-Bold="True" Style="text-align: right"
                                                        Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="15px"></asp:Label>
                                                </ItemTemplate>
                                                <FooterStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Description Of Item">
                                                <FooterTemplate>
                                                    <asp:LinkButton ID="lbtnFinalMatUpdate" Visible="false" runat="server" CssClass="btn btn-danger primaryBtn" OnClick="lbtnFinalMatUpdate_Click">Final Update</asp:LinkButton>
                                                </FooterTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvcResDescRpt" runat="server" AutoCompleteType="Disabled" BackColor="Transparent"
                                                        BorderStyle="None" Font-Size="9px" Text='<%# "<B>"+ Convert.ToString(DataBinder.Eval(Container.DataItem, "grpdesc")) + "</B>"+
                                                                         (DataBinder.Eval(Container.DataItem, "prodesc").ToString().Trim().Length>0 ? 
                                                                         (Convert.ToString(DataBinder.Eval(Container.DataItem, "grpdesc")).Trim().Length>0 ?  "<br>" : "")+                                                             
                                                                         "&nbsp;&nbsp;&nbsp;"+ 
                                                                         "<B>"+Convert.ToString(DataBinder.Eval(Container.DataItem, "prodesc")).Trim()+"</B>": "")  + 
                                                                         (DataBinder.Eval(Container.DataItem, "rsirdesc").ToString().Trim().Length>0 ? 
                                                                          (Convert.ToString(DataBinder.Eval(Container.DataItem, "grpdesc")).Trim().Length>0 ?   "<br>" 
                                                                          :(Convert.ToString(DataBinder.Eval(Container.DataItem, "prodesc")).Trim().Length>0 ? "<br>":"")) + 
                                                                         "&nbsp;&nbsp;&nbsp;&nbsp;"+ 
                                                                         Convert.ToString(DataBinder.Eval(Container.DataItem, "rsirdesc")).Trim(): "")
                                                                    %>'
                                                        Width="190px"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Specification">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvspcfdesc" runat="server" Font-Size="8px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "spcfdesc")) %>'
                                                        Width="120px"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Unit">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvcResUnitRpt" runat="server" Font-Size="8px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rsirunit")) %>'
                                                        Width="25px"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Budgeted</br> Quantity">
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
                                                    <asp:Label ID="lblgvcRIssqty" runat="server" Font-Size="11px" Style="text-align: right;"
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
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Requisition</br> Quantity">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtgvcRProdqty" runat="server" Font-Size="10px" Style="text-align: right;"
                                                        BackColor="Transparent" BorderStyle="None"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "cresqty")).ToString("#,##0.000000;(#,##0.000000); ") %>'
                                                        Width="50px"></asp:TextBox>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Right" ForeColor="Green" />
                                                <HeaderStyle ForeColor="Green" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Stock </br> Quantity">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvcstockqty" runat="server" Font-Size="9px" Style="text-align: right;"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "stockqty")).ToString("#,##0.0000;(#,##0.0000); ") %>'
                                                        Width="60px"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Right" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Balance">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvbalqty" runat="server" Font-Size="9px" Style="text-align: right;"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "balqty")).ToString("#,##0.0000;(#,##0.0000); ") %>'
                                                        Width="70px"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Right" />
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
                        </asp:View>

                    </asp:MultiView>


                </div>
            </div>








        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

