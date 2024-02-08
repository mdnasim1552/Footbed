<%@ Page Title="" Language="C#" MasterPageFile="~/SPE.Master" AutoEventWireup="true" CodeBehind="ProProcessEdit.aspx.cs" Inherits="SPEWEB.F_15_Pro.ProProcessEdit" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript">
        function Search_Gridview(strKey, cellNr) {
            //alert(cellNr);
                var strData = strKey.value.toLowerCase().split(" ");
                var tblData = document.getElementById("<%=gvProdProess.ClientID %>");
                var rowData;
                for (var i = 1; i < tblData.rows.length; i++) {
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


        $(document).ready(function () {
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);

        });
        function pageLoaded() {

          <%--  var gv1 = $('#<%=this.gvProdProess.ClientID %>');
            gv1.Scrollable();--%>
            $("input, select").bind("keydown", function (event) {
                var k1 = new KeyPress();
                k1.textBoxHandler(event);


            });

            $('.chzn-select').chosen({ search_contains: true });

           


        };




    </script>


    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
             <div class="card card-fluid">
                <div class="card-body" style="min-height:500px; padding-bottom:50px;">
                    <div class="row">
                    <div class="col-md-2">
                        <div class="form-group">
                              <asp:Label ID="lCurReqdate" runat="server" CssClass="control-label" Text="Date:"></asp:Label>
                         <asp:TextBox ID="txtCurReqDate" runat="server" AutoCompleteType="Disabled" CssClass="form-control form-control-sm" ToolTip="(dd.mm.yyyy)"></asp:TextBox>
                                        <cc1:CalendarExtender ID="txtCurReqDate_CalendarExtender" runat="server"
                                            Enabled="True" Format="dd.MM.yyyy" TargetControlID="txtCurReqDate"></cc1:CalendarExtender>
    
                        </div>
                    </div>
                        <div class="col-md-1">
                            <div class="form-group">
                                   <asp:Label ID="Label12" runat="server" CssClass="control-label" Text="Search"></asp:Label>
                                            <asp:TextBox ID="txtSrcRequisition01" runat="server" CssClass="form-control form-control-sm" TabIndex="1"></asp:TextBox>
                           
                            </div>
                            </div>
                          <div class="col-md-1">
                              <div class="form-group">
                                      <asp:LinkButton ID="imgbtnFindReqno01" runat="server" CssClass="btn btn-primary btn-sm" style="margin-top:22px;" TabIndex="2" OnClick="imgbtnFindReqno01_Click"><span class="fa fa-search"> </span></asp:LinkButton>

                                  </div>
                              </div>
                         <div class="col-md-5">
                        <div class="form-group">
                            <asp:Label ID="Label1" runat="server" CssClass="control-label" Text="Requision Number"></asp:Label>
                             <asp:DropDownList ID="ddlReqNo01" runat="server" CssClass="custom-select chzn-select form-control" Style="width: 456px;">
                                            </asp:DropDownList>
                            </div>
                             </div>
                            <div class="col-md-2">
                                <div class="form-group">
                                     <asp:Label ID="Label2" runat="server" CssClass="control-label" Text="Colors indicated Sizes"></asp:Label>
                                             <asp:LinkButton ID="lbtnOk" runat="server" CssClass="btn btn-primary btn-sm" OnClick="lbtnOk_Click">Ok</asp:LinkButton>

                                </div>
                            </div>

                            <div class="form-horizontal">

                               
                                <div class="form-horizontal">
                                 
                                       

                                        <div class="col-md-3 pading5px pull-right">
                                            <asp:Label ID="lblmsg1" runat="server" Visible="false" CssClass="btn btn-danger primaryBtn"></asp:Label>


                                            <asp:Label ID="lblvalvounum" runat="server" CssClass="lblTxt lblName" Visible="false"></asp:Label>
                                        </div>
                                    </div>

                                </div>

                            </div>
                       
                  
          
                    <asp:GridView ID="gvProdProess" runat="server" AutoGenerateColumns="False"
                         CssClass="table-striped table-hover table-bordered grvContentarea" OnRowDeleting="gvProdProess_RowDeleting" OnRowCancelingEdit="gvProdProess_RowCancelingEdit" OnRowEditing="gvProdProess_RowEditing" OnRowUpdating="gvProdProess_RowUpdating" OnRowDataBound="gvProdProess_RowDataBound">
                        <PagerSettings Position="Top" />
                        <RowStyle />
                        <Columns>
                            <asp:TemplateField HeaderText="Sl">
                                <ItemTemplate>
                                    <asp:Label ID="lblgvSlNo3" runat="server" Font-Bold="True" Height="16px" Style="text-align: right"
                                        Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>
                            <%--<span class='glyphicon glyphicon-remove'></span>--%>
                        <asp:CommandField ShowDeleteButton="true" ControlStyle-CssClass="btn btn-xs" ItemStyle-CssClass="DeleteBtn"    DeleteText="<i class='fa fa-trash'></i>" />

                            <asp:TemplateField HeaderText="Res Code" Visible="False">
                                <ItemTemplate>
                                    <asp:Label ID="lblgvResCod" runat="server" Height="16px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "styleid")) %>'
                                        Width="60px"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Supl Code" Visible="False">
                                <ItemTemplate>
                                    <asp:Label ID="lblgvResCod1" runat="server" Height="16px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "colorid")) %>'
                                        Width="60px"></asp:Label>
                                    <asp:Label ID="lblFromProcessCode" runat="server" Height="16px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "fprostep")) %>'
                                        Width="60px"></asp:Label>
                                    <asp:Label ID="lblToProcessCode" runat="server" Height="16px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "tprostep")) %>'
                                        Width="60px"></asp:Label>
                                     <asp:Label ID="lblprocessreqno" runat="server" Height="16px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "preqno")) %>'
                                        Width="60px"></asp:Label>
                                     <asp:Label ID="lblprocessppnno" runat="server" Height="16px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "ppnno")) %>'
                                        Width="60px"></asp:Label>
                                    <asp:Label ID="lblprocessordrno" runat="server" Height="16px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "orderno")) %>'
                                        Width="60px"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Spcf Code" Visible="False">
                                <ItemTemplate>
                                    <asp:Label ID="lblgvSpcfCod" runat="server" Height="16px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sizeid")) %>'
                                        Width="60px"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>


                            <asp:TemplateField HeaderText="Production Number">
                                <ItemTemplate>
                                    <asp:Label ID="lgvGenNo" runat="server" Font-Size="12px" Text='<%# "<B>"+Convert.ToString(DataBinder.Eval(Container.DataItem, "ppndesc")) %>'
                                        Width="240px"></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="left" />
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>

                          <%--  <asp:TemplateField HeaderText="">
                                <ItemTemplate>
                                    <asp:LinkButton ID="btnDelMat" OnClick="btnDelMat_Click" OnClientClick="javascript:return FunConfirm();" runat="server"><span style="color:red" class="glyphicon glyphicon-floppy-remove"></span> </asp:LinkButton>


                                </ItemTemplate>
                             
                           
                            </asp:TemplateField>--%>




                            <asp:TemplateField HeaderText="Date">
                                <ItemTemplate>
                                    <asp:Label ID="lgvAppDat0" runat="server" Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "rdate")).ToString("dd-MMM-yyyy") %>'
                                        Width="80px"></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="left" />
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>
                            <%--<asp:TemplateField HeaderText="Ref. No">
                                <ItemTemplate>
                                    <asp:Label ID="lgvgrefno" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "refno")) %>'
                                        Width="70px"></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="left" />
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />

                             

                            </asp:TemplateField>--%>
                            <asp:TemplateField HeaderText="Product Name">
                                  <HeaderTemplate>
                                           <asp:TextBox ID="txtSearhProduct" BackColor="Transparent" BorderStyle="None"  runat="server"  Width="140px" placeholder="Product Name" onkeyup="Search_Gridview(this,3)"></asp:TextBox><br />
                                    </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lgvMaterials3" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "prodesc")) %>'
                                        Width="170px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />

                            </asp:TemplateField>
                            <%--<asp:TemplateField HeaderText="Unit ">
                                <ItemTemplate>
                                    <asp:Label ID="lgvUnit2" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rsirunit")) %>'
                                        Width="25px"></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="left" />
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>--%>

                            <asp:CommandField CancelText="Can" ShowEditButton="True" />

                            <asp:TemplateField HeaderText="From Process">
                                   <HeaderTemplate>
                                           <asp:TextBox ID="txtSearchFromProcess" BackColor="Transparent" BorderStyle="None"  runat="server"  Width="120px" placeholder="From Process" onkeyup="Search_Gridview(this,5)"></asp:TextBox><br />
                                    </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lgvFromProcess" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "fromprocess")) %>'
                                        Width="120px"></asp:Label>
                                </ItemTemplate>


                                <EditItemTemplate>
                                    <asp:DropDownList ID="ddlFromProcess" runat="server" Width="120px">
                                    </asp:DropDownList>
                                </EditItemTemplate>


                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>


                            <asp:TemplateField HeaderText="Current Process">
                                  <HeaderTemplate>
                                           <asp:TextBox ID="txtSearchCurrentProcess" BackColor="Transparent" BorderStyle="None"  runat="server"  Width="140px" placeholder="Current Process" onkeyup="Search_Gridview(this,6)"></asp:TextBox><br />
                                    </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lgvSupplier01" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "toprocess")) %>'
                                        Width="140px"></asp:Label>
                                </ItemTemplate>

                                <EditItemTemplate>
                                    <asp:DropDownList ID="ddlCurProces" runat="server" Width="150px">
                                    </asp:DropDownList>
                                </EditItemTemplate>

                                <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Qty">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtgvreqty01" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                        Width="60px" BorderStyle="Solid" BorderColor="#5aad6e" BorderWidth="1"></asp:TextBox>
                                </ItemTemplate>
                                <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Rejection Qty"  >
                                <ItemTemplate>
                                    <asp:TextBox ID="txtgvRejection" runat="server" Style="text-align: right; " BorderStyle="Solid"  BorderColor="#5aad6e" BorderWidth="1" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "rejectionqty")).ToString("#,##0.0000;(#,##0.0000); ") %>'
                                        Width="60px"></asp:TextBox>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:TemplateField>


                            <asp:TemplateField HeaderText="Repair Qty">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtgvRepairQty" BorderStyle="Solid" BorderColor="#5aad6e" BorderWidth="1" runat="server" Style="text-align: right; " Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "repairqty")).ToString("#,##0.0000;(#,##0.0000); ") %>'
                                        Width="60px"></asp:TextBox>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:TemplateField>
                          <%--  <asp:TemplateField HeaderText="Amount">
                                <ItemTemplate>
                                    <asp:Label ID="lgvamount" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt")).ToString("#,##0.00;(#,##0.00); ") %>'
                                        Width="60px"></asp:Label>
                                </ItemTemplate>
                                <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:TemplateField>--%>

                     
                        </Columns>
                     
                        <EditRowStyle />
                        <AlternatingRowStyle />
                        <PagerStyle CssClass="gvPagination" />
                        <HeaderStyle CssClass="grvHeader" />
                    </asp:GridView>




                </div>

            </div>

        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>


