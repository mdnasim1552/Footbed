<%@ Page Title="" Language="C#" MasterPageFile="~/SPE.Master" AutoEventWireup="true" CodeBehind="AccCodeBook.aspx.cs" Inherits="SPEWEB.F_21_GAcc.AccCodeBook" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">


    <script type="text/javascript">
        $(document).ready(function () {
            //For navigating using left and right arrow of the keyboard
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);
        });

        function loadModal() {
            $('#AddAccCode').modal('toggle', {
                backdrop: 'static',
                keyboard: false
            });
        }

        function CloseModal() {
            $('#AddAccCode').modal('hide');
        }




        function pageLoaded() {

            $("input, select").bind("keydown", function (event) {
                var k1 = new KeyPress();
                k1.textBoxHandler(event);
            });
            $('.chzn-select').chosen({ search_contains: true });


            $('#Chboxchild').change(function () {

                var result = $('#Chboxchild').is(':checked');
                var description = result ? "Add Child" : "Add Group";
                $('#lblchild').html(description);


            });

        };
    </script>


    <style>
        .switch {
            position: relative;
            display: inline-block;
            width: 40px;
            height: 20px;
        }

            .switch input {
                opacity: 0;
                width: 0;
                height: 0;
            }

        .slider {
            position: absolute;
            cursor: pointer;
            top: 0;
            left: 0;
            right: 0;
            bottom: 0;
            background-color: #ccc;
            -webkit-transition: .4s;
            transition: .4s;
        }

            .slider:before {
                position: absolute;
                content: "";
                height: 18px;
                width: 18px;
                left: 1px;
                bottom: 1px;
                background-color: white;
                -webkit-transition: .4s;
                transition: .4s;
            }

        input:checked + .slider {
            background-color: #2196F3;
        }

        input:focus + .slider {
            box-shadow: 0 0 1px #2196F3;
        }

        input:checked + .slider:before {
            -webkit-transform: translateX(18px);
            -ms-transform: translateX(18px);
            transform: translateX(18px);
        }

        /* Rounded sliders */
        .slider.round {
            border-radius: 20px;
        }

            .slider.round:before {
                border-radius: 50%;
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
                                <asp:DropDownList ID="ddlCodeBook" runat="server" CssClass="chzn-select form-control form-control-sm" AutoPostBack="true" OnSelectedIndexChanged="ddlCodeBook_SelectedIndexChanged"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-2 col-sm-2 col-lg-2">
                            <div class="form-group">
                                <asp:Label ID="Label5" runat="server" CssClass="label">Group</asp:Label>
                                <asp:DropDownList ID="ddlCodeBookSegment" CssClass="chzn-select form-control form-control-sm" runat="server">
                                    <asp:ListItem Value="2">Main Code</asp:ListItem>
                                    <asp:ListItem Value="4">Sub Code-1</asp:ListItem>
                                    <asp:ListItem Value="8">Sub Code-2</asp:ListItem>
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
                                </asp:DropDownList>
                            </div>
                        </div>

                    </div>

                </div>
            </div>





            <div class="card card-fluid">
                <div class="card-body">
                    <div class="row" style="min-height: 300px;">
                        <asp:GridView ID="grvacc" runat="server" AllowPaging="True" CssClass="table-striped table-hover table-bordered grvContentarea" AutoGenerateColumns="False" OnRowCancelingEdit="grvacc_RowCancelingEdit" OnRowEditing="grvacc_RowEditing"
                            OnRowUpdating="grvacc_RowUpdating" PageSize="15"
                            OnPageIndexChanging="grvacc_PageIndexChanging" ShowFooter="True" BorderStyle="None" OnRowDataBound="grvacc_RowDataBound">
                            <PagerSettings NextPageText="Next" PreviousPageText="Previous" Position="Top"
                                Mode="NumericFirstLast" />
                            <FooterStyle BackColor="#5F9467" />
                            <Columns>
                                <asp:TemplateField HeaderText="Sl">
                                    <ItemTemplate>
                                        <asp:Label ID="serialnoid" runat="server" Style="text-align: right"
                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                    </ItemTemplate>

                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="+">

                                    <ItemTemplate>
                                        <asp:LinkButton ID="lbtnAdd" runat="server" CssClass="btn btn-xs btn-default" ToolTip="Add New Code" BackColor="Transparent" Visible="false" OnClick="lbtnAdd_Click"><span class="fa fa-plus" aria-hidden="true"></span></asp:LinkButton>

                                    </ItemTemplate>
                                    <HeaderStyle Font-Bold="True" Font-Size="16px" Width="20px" HorizontalAlign="Center" />

                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:TemplateField>
                                    <asp:CommandField ShowEditButton="True" ControlStyle-Width="30px"  CancelText="<span class='fa fa-times'></span>" EditText="<span class='fa fa-pen'></span>" UpdateText="<span class='fa fa-save'></span>" />

                              <%--  <asp:CommandField DeleteText="" HeaderText="Edit" InsertText="" NewText=""
                                    SelectText="" ShowEditButton="True" EditText="&lt;i class=&quot;fa fa-pencil-square-o&quot; aria-hidden=&quot;true&quot;&gt;&lt;/i&gt;"></asp:CommandField>--%>
                                <asp:TemplateField HeaderText=" ">
                                    <EditItemTemplate>
                                        <asp:Label ID="lbgrcode" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actcode2"))+"-" %>'></asp:Label>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="Label7" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actcode2"))+"-" %>'
                                            Width="30px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Acc. Code">
                                    <EditItemTemplate>
                                        <asp:Label ID="txtgrcode" runat="server" Height="16px" MaxLength="12"
                                            Style="border-top-style: none; border-right-style: none; border-left-style: none; border-bottom-style: none; font-size: 12px; border-left-color: #151B54; border-bottom-color: #151B54; border-top-color: #151B54; border-right-color: #151B54;"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actcode4")) %>'
                                            Width="90px"></asp:Label>
                                        <asp:Label ID="lbgrcod1" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actcode3")) %>'
                                            Visible="False"></asp:Label>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lbtncod" runat="server" Font-Underline="false" Enabled="false" 
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actcode4")) %>'
                                            Width="80px"></asp:LinkButton>
                                        <%--                                            <asp:Label ID="lbgrcod" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actcode4")) %>'
                                                Width="70px"></asp:Label>--%>
                                    </ItemTemplate>

                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Description of Accounts">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtgvDesc" runat="server"
                                            Style="border-top-style: none; left: auto; border-right-style: none; border-left-style: none; border-bottom-style: none; font-size: 12px; border-left-color: #151B54; border-bottom-color: #151B54; border-top-color: #151B54; border-right-color: #151B54;"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc")) %>'
                                            Width="300px"></asp:TextBox>
                                    </EditItemTemplate>
                                    <HeaderTemplate>

                                        <table style="width: 300px;">

                                            <tr>
                                                <th class="">Head of Accounts                                                              
                                                </th>
                                                <th class="pull-right">
                                                    <asp:HyperLink ID="hlbtntbCdataExel" runat="server" BackColor="#000066" ToolTip="Export Excel"
                                                        BorderColor="White" BorderStyle="Solid" BorderWidth="1px" Font-Bold="True"
                                                        ForeColor="White" Style="text-align: center; margin-left: 10px;" Width="20px"><span class="fa fa-file-excel"></span></asp:HyperLink>
                                                </th>
                                            </tr>

                                        </table>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="Label3" runat="server" Style="font-size: 12px; text-align: left;"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc")) %>'
                                            Width="300px"></asp:Label>
                                    </ItemTemplate>

                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Level">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtgridlevel" runat="server" MaxLength="10"
                                            Style="border-top-style: none; border-right-style: none; border-left-style: none; border-bottom-style: none"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actelev")) %>'
                                            Width="40px"></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="Label6" runat="server" Font-Size="12px"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actelev")) %>'
                                            Width="40px" Style="text-align: center"></asp:Label>
                                    </ItemTemplate>

                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Type">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtgvTypeCode" runat="server" Font-Size="12px" MaxLength="20"
                                            Style="border-top-style: none; border-right-style: none; border-left-style: none; border-bottom-style: none"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "acttype")) %>'
                                            Width="60px"></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="Label4" runat="server" Style="font-size: 12px"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "acttype")) %>'
                                            Width="60px"></asp:Label>
                                    </ItemTemplate>

                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Type Description">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtgvTypeDesc" runat="server" Font-Size="12px" MaxLength="100"
                                            Style="border-top-style: none; border-right-style: none; border-left-style: none; border-bottom-style: none"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "acttdesc")) %>'
                                            Width="100px"></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="Label5" runat="server" Style="font-size: 12px"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "acttdesc")) %>'
                                            Width="100px"></asp:Label>
                                    </ItemTemplate>

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

            <%--Modal  --%>

            <div id="AddAccCode" class="modal animated slideInLeft " role="dialog" data-keyboard="false" data-backdrop="static">
                <div class="modal-dialog">
                    <div class="modal-content  ">
                        <div class="modal-header">


                            <h4 class="modal-title">
                                <span class="fa fa-table"></span>Add New Code  </h4>

                        </div>
                        <div class="modal-body">
                            <div class="row">
                                <asp:Label ID="lblactcode" runat="server" Visible="false"></asp:Label>

                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label class="label">Accounts Code</label>

                                        <asp:TextBox ID="txtacountcode" runat="server" CssClass="form-control form-control-sm">
                                        </asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-12">
                                    <div class="form-group">
                                        <label class="label">Accounts Head</label>

                                        <asp:TextBox ID="txtaccounthead" runat="server" CssClass="form-control form-control-sm">
                                        </asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label class="label">Level</label>

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
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label class="label">Type</label>

                                        <asp:TextBox ID="txttype" runat="server" CssClass="form-control form-control-sm">
                                        </asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label class="label">Type Description</label>

                                        <asp:TextBox ID="txtshort" runat="server" CssClass="form-control form-control-sm">
                                        </asp:TextBox>
                                    </div>
                                </div>
                                <%--<div class="form-group" runat="server">
                                    <label class="col-md-4"></label>


                                    <div class="col-md-8">
                                        <asp:TextBox ID="txtacountcode" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>--%>

                                <%-- <div class="form-group" runat="server">
                                    <label class="col-md-4">Accounts Head</label>


                                    <div class="col-md-8">
                                        <asp:TextBox ID="" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>--%>
                                <%--<div class="form-group">
                                    <label class="col-md-4">Level </label>
                                    <div class="col-md-5">
                                        <asp:TextBox ID="txtlevel" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>




                                    <div class="col-md-3">
                                        <label id="chkbod" runat="server" class="switch">
                                            <asp:CheckBox ID="Chboxchild" runat="server" ClientIDMode="Static" />
                                            <span class="btn btn-xs slider round"></span>
                                        </label>
                                        <asp:Label ID="lblchild" runat="server" Text="Add Child" CssClass="btn btn-xs" ClientIDMode="Static"></asp:Label>
                                    </div>
                                </div>--%>
                                <%-- <div class="form-group">
                                    <label class="col-md-4">Type </label>
                                    <div class="col-md-8">
                                        <asp:TextBox ID="txttype" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="col-md-4">Type Description </label>
                                    <div class="col-md-8">
                                        <asp:TextBox ID="txtshort" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                                --%>
                            </div>


                        </div>
                        <div class="modal-footer ">
                            <asp:LinkButton ID="lbtnAddCode" runat="server" CssClass="btn btn-sm btn-success" OnClientClick="CloseModal();" OnClick="lbtnAddCode_Click"><span class="glyphicon glyphicon-save"></span> Update </asp:LinkButton>

                            <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                            <%--<button type="button" style="background-color: red;" class="close btn btn-xs" data-dismiss="modal"><span class="fa fa-close"></span></button>--%>
                        </div>
                    </div>
                </div>
            </div>

        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>


