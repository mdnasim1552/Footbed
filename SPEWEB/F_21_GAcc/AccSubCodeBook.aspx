<%@ Page Title="" Language="C#" MasterPageFile="~/SPE.Master" AutoEventWireup="true" CodeBehind="AccSubCodeBook.aspx.cs" Inherits="SPEWEB.F_21_GAcc.AccSubCodeBook" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript">
        $(document).ready(function () {
            //For navigating using left and right arrow of the keyboard
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);
        });
        function loadModal() {
            $('#detialsinfo').modal('toggle');
        }

        function CloseModal() {
            $('#detialsinfo').modal('hide');
        }


        function loadModalAddCode() {
            $('#AddResCode').modal('toggle', {
                backdrop: 'static',
                keyboard: false
            });
        }

        function CloseModalAddCode() {
            $('#AddResCode').modal('hide');

        }
        function pageLoaded() {

            $('#Chboxchild').change(function () {
                var result = $('#Chboxchild').is(':checked');
                var description = result ? "Add Child" : "Add Group";
                $('#lblchild').html(description);
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
            <div class="card card-fluid">
                <div class="card-body">
                    <div class="row">
                        <div class="col-md-3 col-sm-3 col-lg-3 ">
                            <div class="form-group">
                                <asp:Label ID="Label1" runat="server" CssClass="label">Select Code Book</asp:Label>
                                <asp:DropDownList ID="ddlOthersBook" runat="server" CssClass="chzn-select form-control form-control-sm" AutoPostBack="true" OnSelectedIndexChanged="ddlOthersBook_SelectedIndexChanged"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-2 col-sm-2 col-lg-2">
                            <div class="form-group">
                                <asp:Label ID="Label5" runat="server" CssClass="label">Group</asp:Label>
                                <asp:DropDownList ID="ddlOthersBookSegment" CssClass="chzn-select form-control form-control-sm" runat="server">
                                    <asp:ListItem Value="2">Main Code</asp:ListItem>
                                    <asp:ListItem Value="4">Sub Code-1</asp:ListItem>
                                    <asp:ListItem Value="7">Sub Code-2</asp:ListItem>
                                    <asp:ListItem Value="9">Sub Code-3</asp:ListItem>
                                    <asp:ListItem Selected="True" Value="12">Details Code</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-1 col-sm-1 col-lg-1 ">
                            <div class="form-group" style="margin-top: 20px;">
                                <asp:LinkButton ID="lnkok" runat="server" CssClass="btn btn-primary btn-sm pull-left" OnClick="lnkok_Click">Ok</asp:LinkButton>
                            </div>
                        </div>
                        <div class="col-md-2 col-sm-2 col-lg-2">
                            <div class="form-group">
                                <asp:Label ID="Label2" runat="server" CssClass="label">Catagory</asp:Label>
                                <asp:DropDownList ID="ddlcatagory" runat="server" CssClass="chzn-select form-control form-control-sm">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-2 col-sm-2 col-lg-2">
                            <div class="form-group">
                                <asp:Label ID="lblConTrolCode" runat="server" CssClass="control-label">Search:</asp:Label>
                                <div class="input-group input-group-alt">
                                    <asp:TextBox runat="server" ID="txtsrch" CssClass="form-control form-control-sm ">
                                    </asp:TextBox>
                                    <div class="input-group-append">
                                        <asp:LinkButton ID="ibtnSrch" CssClass="input-group-text" runat="server" OnClick="ibtnSrch_Click"><span class="fa fa-search"> </span></asp:LinkButton>
                                    </div>
                                </div>
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
                                    <asp:ListItem>2000</asp:ListItem>
                                    <asp:ListItem>3000</asp:ListItem>
                                    <asp:ListItem>5000</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>

                    </div>

                </div>
            </div>

            <div class="card card-fluid">
                <div class="card-body">
                    <div class="row" style="min-height: 300px;">
                        <asp:GridView ID="grvacc" runat="server" AllowPaging="True"
                            AutoGenerateColumns="False" OnRowCancelingEdit="grvacc_RowCancelingEdit" OnRowEditing="grvacc_RowEditing"
                            OnRowUpdating="grvacc_RowUpdating" PageSize="15" OnPageIndexChanging="grvacc_PageIndexChanging"
                            CssClass="table-striped table-hover table-bordered grvContentarea" OnRowDataBound="grvacc_RowDataBound">
                            <PagerSettings NextPageText="Next" PreviousPageText="Previous" Position="Top"
                                Mode="NumericFirstLast" />
                            <FooterStyle BackColor="#5F9467" />
                            <Columns>
                                <asp:TemplateField HeaderText="Sl">
                                    <ItemTemplate>
                                        <asp:Label ID="lblserialnoid" runat="server"
                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle />
                                    <ItemStyle />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="+">

                                    <ItemTemplate>
                                        <asp:LinkButton ID="lbtnAdd" runat="server" CssClass="btn btn-xs btn-default" ToolTip="Add New Code" BackColor="Transparent" Visible="false" OnClick="lbtnAdd_Click"><span class="fa fa-plus" aria-hidden="true"></span></asp:LinkButton>

                                    </ItemTemplate>
                                    <HeaderStyle Font-Bold="True" Font-Size="16px" Width="20px" HorizontalAlign="Center" />

                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:TemplateField>

                                <asp:CommandField ShowEditButton="True" ControlStyle-Width="30px" CancelText="<span class='fa fa-times'></span>" EditText="<span class='fa fa-pen'></span>" UpdateText="<span class='fa fa-save'></span>" />

                                <asp:TemplateField HeaderText=" ">
                                    <EditItemTemplate>
                                        <asp:Label ID="lbgrcode" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sircode2"))+"-" %>'
                                            Width="20px"></asp:Label>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblgrcode" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sircode2"))+"-" %>'
                                            Width="20px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Font-Size="12px" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Code">
                                    <HeaderTemplate>
                                        <table>
                                            <tr>
                                                <td>
                                                    Code
                                                </td>
                                                <td>
                                                    <asp:HyperLink runat="server" ID="lnkExcel" CssClass="btn btn-sm btn-success text-light"><i class="fa fa-file-excel"></i></asp:HyperLink>
                                                </td>
                                            </tr>
                                        </table>
                                    </HeaderTemplate>
                                    <EditItemTemplate>
                                        <asp:Label ID="txtgrcode" runat="server" Font-Size="12px" Height="16px"
                                            MaxLength="13"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sircode4")) %>'
                                            Width="90px"></asp:Label>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:HyperLink ID="lbgrcod3" runat="server" Font-Size="12px" Font-Underline="false"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sircode4")) %>'
                                            Width="90px"></asp:HyperLink>
                                    </ItemTemplate>

                                    <ItemStyle Font-Size="12px" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Item code">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtgridsirtype" runat="server" Font-Size="12px" MaxLength="20"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sirtype")) %>'
                                            Width="60px"></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lbltype" runat="server" Font-Size="12px"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sirtype")) %>'
                                            Width="60px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Font-Bold="True" Font-Size="12px" />
                                    <ItemStyle Font-Size="12px" HorizontalAlign="Center" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderStyle-Width="300px">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtgvDesc" runat="server" Font-Size="12px" MaxLength="250"
                                            Style="border-style: none;"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sirdesc")) %>'
                                            Width="300px"></asp:TextBox>
                                    </EditItemTemplate>

                                    <ItemTemplate>
                                        <asp:HyperLink ID="hlnkgvdesc" runat="server" BorderColor="#99CCFF" BorderStyle="none"
                                            Font-Size="11px" Style="text-align: left; background-color: Transparent;"
                                            Font-Underline="false" Target="_blank" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sirdesc"))  %>'
                                            Width="300px">                                            
                                        </asp:HyperLink>

                                    </ItemTemplate>
                                    <HeaderStyle />
                                    <ItemStyle />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Unit">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtgvsirunit" runat="server" MaxLength="100"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sirunit")) %>'
                                            Width="40px"></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblunit" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sirunit")) %>'
                                            Width="40px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" Width="10px" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Unit Code" Visible="false">
                                    <EditItemTemplate>
                                        <asp:DropDownList ID="ddlunit" runat="server" CssClass="ddlPage62 inputTxt chzn-select" Width="60">
                                        </asp:DropDownList>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblunit2" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sirunit")) %>'
                                            Width="40px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" Width="10px" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Std.Rate">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtgvsirval" runat="server" Font-Size="12px" MaxLength="100"
                                            Style="border-style: none;"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem,  "sirval")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="60px"></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblsirval" runat="server" Font-Size="12px"
                                            Style="font-size: 12px"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem,  "sirval")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="60px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="left" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Allowance" Visible="false">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtgvAllowance" runat="server" MaxLength="100"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "allowance")) %>'
                                            Width="40px"></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvAllowance" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "allowance")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="40px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" Width="10px" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="C&F" Visible="false">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtgvCnF" runat="server" MaxLength="100"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mark")) %>'
                                            Width="40px"></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvCnF" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "mark")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="40px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" Width="10px" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Incoterms" Visible="false">
                                    <EditItemTemplate>
                                        <asp:DropDownList ID="ddlIncoterms" runat="server" CssClass="ddlPage62 inputTxt chzn-select" Width="60">
                                        </asp:DropDownList>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblIncoterms" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "incotermsdesc")) %>'
                                            Width="40px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" Width="10px" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderStyle-Width="200">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtgvsirtdesc" runat="server" Font-Size="12px"
                                            Style="border-top-style: none; border-right-style: none; border-left-style: none; border-bottom-style: none"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sirtdes")) %>'
                                            Width="200px"></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lbltypedesc" runat="server" Font-Size="12px"
                                            Style="font-size: 12px"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sirtdes")) %>'
                                            Width="200px"></asp:Label>
                                    </ItemTemplate>

                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="L/C Code" Visible="false">
                                    <EditItemTemplate>
                                        <asp:Panel ID="Panel2" runat="server">
                                            <asp:DropDownList ID="ddlAccCode" runat="server" CssClass="chzn-select form-control"
                                                TabIndex="31" Style="width: 213px;">
                                            </asp:DropDownList>

                                        </asp:Panel>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvProNames" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc")) %>'
                                            Width="200px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Hidden Column" Visible="False">
                                    <EditItemTemplate>
                                        <asp:Label ID="lbgrcod1" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sircode3")) %>'
                                            Visible="False"></asp:Label>
                                    </EditItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Entry User Name" Visible="false">
                                    <EditItemTemplate>
                                        <asp:Label ID="tlblgvUsr" runat="server" Font-Size="12px" MaxLength="100"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "userdesc")) %>'
                                            Width="90px"></asp:Label>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="Label5" runat="server" Style="font-size: 12px"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "userdesc")) %>'
                                            Width="90px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Font-Bold="True" Font-Size="16px" HorizontalAlign="Left" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="">

                                    <ItemTemplate>
                                        <asp:HyperLink ID="lnkSpcf" runat="server" Target="_blank" Font-Underline="false" Font-Size="9px"
                                            Width="60px" Text="Specification"></asp:HyperLink>
                                    </ItemTemplate>
                                    <ItemStyle Font-Size="12px" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="কোডের বর্ণনা" Visible="false">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtgvsirdescb" runat="server" Font-Size="12px" MaxLength="100"
                                            Style="border-top-style: none; border-right-style: none; border-left-style: none; border-bottom-style: none; font-size: 12px; border-left-color: midnightblue; border-bottom-color: midnightblue; border-top-color: midnightblue; border-right-color: midnightblue;"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sirdescb")) %>'
                                            Width="250px"></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lbgvsirdescb" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sirdescb")) %>'
                                            Width="250px" Style="text-align: left"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Production Process">
                                    <EditItemTemplate>
                                        <asp:DropDownList ID="ddlPProces" runat="server" CssClass="chzn-select form-control form-control-sm"></asp:DropDownList>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblPProces" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "prodprocessdesc")) %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Font-Size="12px" Width="200px" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Group Code" Visible="false">

                                    <EditItemTemplate>
                                        <asp:DropDownList ID="ddlMaterialGorup" runat="server" CssClass="ddlPage62 inputTxt chzn-select" Width="90">
                                        </asp:DropDownList>
                                    </EditItemTemplate>

                                    <ItemTemplate>
                                        <asp:Label ID="LblMaterialGorup" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "deptdesc")) %>'
                                            Width="90px"></asp:Label>
                                    </ItemTemplate>

                                    <ItemStyle HorizontalAlign="Center" Width="10px" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Std.Qty" Visible="false">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtgvsinfqty" runat="server" Font-Size="12px" MaxLength="100"
                                            Style="border-style: none;"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem,  "sinfqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="60px"></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblsinfqty" runat="server" Font-Size="12px"
                                            Style="font-size: 12px"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem,  "sinfqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="60px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="left" />
                                </asp:TemplateField>

                                
                                <asp:TemplateField HeaderText="Prod. Process Code" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblProcessCode" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "prodprocess")) %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Font-Size="12px" Width="120px" />
                                </asp:TemplateField>
                                
                            </Columns>

                            <RowStyle />
                            <EditRowStyle />
                            <SelectedRowStyle />
                            <PagerStyle CssClass="gvPagination" />
                            <HeaderStyle CssClass="grvHeader" />
                            <FooterStyle CssClass="grvFooter" />
                            <AlternatingRowStyle BackColor="" />
                        </asp:GridView>

                    </div>
                </div>
            </div>

            </br>   </br>   </br>

              <div class="modal fade " id="detialsinfo" role="dialog">
                  <div class="modal-dialog  modal-lg ">
                      <div class="modal-content ">
                          <div class="modal-header">
                              <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                              <h4 class="modal-title text-center">Details Information</h4>
                          </div>
                          <div class="modal-body">
                              <div class="form-group">
                                  <label class="control-label">Details:</label>
                                  <asp:TextBox ID="txtrsircode" runat="server" TextMode="MultiLine" CssClass="form-control" Visible="false"></asp:TextBox>
                                  <asp:TextBox ID="txtDetails" runat="server" TextMode="MultiLine" CssClass="form-control"></asp:TextBox>

                              </div>

                          </div>
                          <div class="modal-footer">
                              <%--    <button class="btn btn-success" data-dismiss="modal" aria-hidden="true" >Update</button>--%>
                              <asp:LinkButton ID="lbtnUpdateDetails" runat="server" class="btn btn-success" aria-hidden="true" OnClientClick="CloseModal();" OnClick="lbtnUpdateDetails_Click">Update</asp:LinkButton>
                              <button class="btn btn-info" data-dismiss="modal" aria-hidden="true">Close</button>
                          </div>
                      </div>
                  </div>
              </div>



            <%--Modal  --%>

            <div id="AddResCode" class="modal animated slideInLeft " role="dialog" data-keyboard="false" data-backdrop="static">
                <div class="modal-dialog">
                    <div class="modal-content  ">
                        <div class="modal-header">


                            <h4 class="modal-title">
                                <span class="fa fa-table"></span>Add New Code  </h4>
                        </div>
                        <div class="modal-body">
                            <div class="row">
                                <asp:Label ID="lblsircode" runat="server" Visible="false"></asp:Label>

                                <div class="col-md-12">
                                    <div class="form-group">
                                        <label class="label">Resource Code</label>

                                        <asp:TextBox ID="txtresourcecode" runat="server" CssClass="form-control form-control-sm">
                                        </asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-12">
                                    <div class="form-group">
                                        <label class="label">Description</label>

                                        <asp:TextBox ID="txtresourcehead" runat="server" CssClass="form-control form-control-sm">
                                        </asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-12" id="grdescb" runat="server">
                                    <div class="form-group">
                                        <label class="label">কোডের বর্ণনা</label>

                                        <asp:TextBox ID="txtresourceheadB" runat="server" CssClass="form-control form-control-sm">
                                        </asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label class="label">Item Code</label>

                                        <asp:TextBox ID="txtlevel" runat="server" CssClass="form-control form-control-sm">
                                        </asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="form-group" style="margin-top: 20px;">
                                        <asp:Label ID="lblchild" runat="server" Text="Add Child" CssClass="btn btn-xs" ClientIDMode="Static"></asp:Label>

                                        <label id="chkbod" runat="server" class="switch">
                                            <asp:CheckBox ID="Chboxchild" runat="server" ClientIDMode="Static" />
                                            <span class="btn btn-xs slider round"></span>
                                        </label>

                                    </div>
                                </div>
                                <div class="col-md-6" id="grunit" runat="server">
                                    <div class="form-group">
                                        <label class="label">Unit</label>

                                        <asp:TextBox ID="txtunit" runat="server" CssClass="form-control form-control-sm">
                                        </asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-6" id="grucode" runat="server">
                                    <div class="form-group">
                                        <label id="lblddlunit" runat="server" class="form-control">Unit Code</label>

                                        <asp:DropDownList ID="ddlunit" runat="server" CssClass=""></asp:DropDownList>

                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label class="label">Standard Rate</label>

                                        <asp:TextBox ID="txtstdrate" runat="server" CssClass="form-control form-control-sm">
                                        </asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label class="label">Bond Name</label>

                                        <asp:TextBox ID="txtbrand" runat="server" CssClass="form-control form-control-sm">
                                        </asp:TextBox>
                                    </div>
                                </div>


                                <%--<div class="form-group" runat="server">
                                    <label class="col-md-4">Resource Code </label>


                                    <div class="col-md-8">
                                        <asp:TextBox ID="txtresourcecode" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>

                                <div class="form-group" runat="server">
                                    <label class="col-md-4">Description</label>


                                    <div class="col-md-8">
                                        <asp:TextBox ID="txtresourcehead" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="form-group" runat="server" id="grdescb" runat="server">
                                    <label class="col-md-4">কোডের বর্ণনা </label>


                                    <div class="col-md-8">
                                        <asp:TextBox ID="txtresourceheadB" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="form-group" runat="server">
                                    <label class="col-md-4">Item Code </label>
                                    <div class="col-md-5">
                                        <asp:TextBox ID="TextBox1" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                    <div class="col-md-3">
                                        <label id="chkbod" runat="server" class="switch">
                                            <asp:CheckBox ID="Chboxchild" runat="server" ClientIDMode="Static" />
                                            <span class="btn btn-xs slider round"></span>
                                        </label>
                                        <asp:Label ID="lblchild" runat="server" Text="Add Child" CssClass="btn btn-xs" ClientIDMode="Static"></asp:Label>
                                    </div>
                                </div>
                                <div class="form-group" id="grunit" runat="server">
                                    <label class="col-md-4">Unit </label>
                                    <div class="col-md-8">
                                        <asp:TextBox ID="txtunit" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="form-group" id="grucode" runat="server">
                                    <label id="lblddlunit" runat="server" class="col-md-4">Unit Code</label>
                                    <div class="col-md-8">
                                        <asp:DropDownList ID="ddlunit" runat="server" CssClass="form-control"></asp:DropDownList>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="col-md-4">Standard Rate </label>
                                    <div class="col-md-8">
                                        <asp:TextBox ID="txtstdrate" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="col-md-4">Bond Name</label>
                                    <div class="col-md-8">
                                        <asp:TextBox ID="txtbrand" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>--%>
                            </div>


                        </div>
                        <div class="modal-footer ">
                            <asp:LinkButton ID="lbtnAddCode" runat="server" CssClass="btn btn-sm btn-success" OnClientClick="CloseModalAddCode();" OnClick="lbtnAddCode_Click"><span class="glyphicon glyphicon-save"></span> Update </asp:LinkButton>

                            <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>

                            <%--<button type="button" style="background-color: red;" class="close btn btn-xs" data-dismiss="modal"><span class="fa fa-close"></span></button>--%>
                        </div>
                    </div>
                </div>
            </div>


        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>
