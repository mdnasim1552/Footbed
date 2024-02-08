<%@ Page Title="" Language="C#" MasterPageFile="~/SPE.Master" AutoEventWireup="true" CodeBehind="MatRetInWIP.aspx.cs" Inherits="SPEWEB.F_15_Pro.MatRetInWIP" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <link href="../Content/chosen.css" rel="stylesheet" />
    <script src="../Scripts/chosen.jquery.js"></script>

    <script type="text/javascript">
        $(document).ready(function () {
            //For navigating using left and right arrow of the keyboard
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);


        });
        function pageLoaded() {
            $(function () {
                $('[id*=ddlwip]').multiselect({
                    includeSelectAllOption: true,
                    enableFiltering: true,
                    enableCaseInsensitiveFiltering: true,
                    includeFilterClearBtn: false
                })
            });

            $("input, select").bind("keydown", function (event) {
                var k1 = new KeyPress();
                k1.textBoxHandler(event);
            });

            $('.chzn-select').chosen({ search_contains: true });
        };

        function SelectAllCheckboxes(chk) {
            var tblData1 = document.getElementById("<%=gvmetinf.ClientID %>");

            var i = 0
            $('#<%=gvmetinf.ClientID %>').find("input:checkbox").each(function () {
                if ((this).disabled == false && tblData1.rows[i].style.display != "none") {
                    if (this != chk) {
                        this.checked = chk.checked;
                    }
                }
                i++;
            });
        };

    </script>
    <script language="javascript" type="text/javascript">
        function GoToNextTextBox(currentTxtId, e) {


            if (e.keyCode == 13 || e.keyCode == 40) {

                var number = parseInt(currentTxtId.id.substring(41));

                var nextId = number + 1;

                var nextIdString = "ContentPlaceHolder1_gvmetinf_txtgvRetqty_" + nextId.toString();

                var x = document.getElementById(nextIdString);
                x.focus();
            }
            else
                if (e.keyCode == 38) {
                    var number = parseInt(currentTxtId.id.substring(41));
                    var nextId = number - 1;

                    var nextIdString = "ContentPlaceHolder1_gvmetinf_txtgvRetqty_" + nextId.toString();

                    var x = document.getElementById(nextIdString);
                    x.focus();
                }
        }
    </script>

    <style>
        .gvTopHeader tr:nth-child(1) {
            height: 14px !important;
            font-size: 12px !important;
            font-weight: bold !important;
        }

        .listbox button {
            border: 1px solid lightgray !important;
            font-size: 12px !important;
            padding-top: 4px !important;
            padding-bottom: 4px !important;
            overflow: hidden;
            height: 29px;
        }

        .multiselect-native-select div:first-of-type {
            width: 96%;
        }

        .multiselect-search {
            height: 28px;
        }

        .input-group-addon {
            padding: 0.25rem !important;
        }

        .multiselect-container {
            position: absolute;
            list-style-type: none;
            margin: 0;
            padding: 0;
            width: 100% !important;
        }

            .multiselect-container > li {
                padding: 0px;
                margin-left: 0px;
            }

        .chzn-single {
            height: 29px !important;
            border-radius: 4px !important;
        }
    </style>


    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>


            <div class="card card-fluid mb-1">
                <div class="card-body" style="min-height: 150px;">

                    <div class="row mb-3">

                        <div class="col-md-2">
                            <asp:Label ID="Label15" runat="server" CssClass="">Return No.</asp:Label>
                            <asp:TextBox ID="txtRetnum" runat="server" CssClass="form-control form-control-sm" ReadOnly="True"></asp:TextBox>
                        </div>

                        <%--<div class="col-md-1">
                            <asp:Label ID="lblrefNo0" runat="server" CssClass="" Text="Ref. No" Visible="false"></asp:Label>
                            <asp:TextBox ID="txtRefno" runat="server" CssClass="form-control form-control-sm" Visible="false"></asp:TextBox>
                        </div>--%>

                        <div class="col-md-1">
                            <asp:Label ID="lblDate" runat="server" CssClass="" Text="Return Date"></asp:Label>
                            <asp:TextBox ID="txtCurDate" runat="server" CssClass="form-control form-control-sm" ToolTip="(dd-mm-yyyy)"></asp:TextBox>
                            <cc1:CalendarExtender ID="txtCurDate_CalendarExtender" runat="server" Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txtCurDate"></cc1:CalendarExtender>
                        </div>

                        <div class="col-md-1">
                            <asp:Label ID="lblSeason" runat="server">Season</asp:Label>
                            <asp:DropDownList ID="DdlSeason" CssClass="form-control form-control-sm chzn-select" runat="server" AutoPostBack="true" 
                                 OnSelectedIndexChanged="DdlSeason_SelectedIndexChanged"></asp:DropDownList>
                        </div>

                        <div class="col-md-5 listbox">
                            <asp:Label ID="lblStore" runat="server" CssClass="">Work In Progress</asp:Label>
                            <asp:ListBox ID="ddlwip" runat="server" CssClass="form-control form-control-sm multiselect" SelectionMode="Multiple"></asp:ListBox>
                        </div>

                        <div class="col-md-1">
                            <asp:LinkButton ID="lbtnOk" runat="server" Style="margin-top: 21px;" CssClass="btn btn-sm btn-primary" OnClick="lbtnOk_Click">Ok</asp:LinkButton>
                        </div>

                        <div class="col-md-1">
                            <asp:Label ID="lblPageSize" runat="server" CssClass="">Page Size</asp:Label>
                            <asp:DropDownList runat="server" ID="ddlPageSize" CssClass="form-control form-control-sm" AutoPostBack="true" OnSelectedIndexChanged="ddlPageSize_SelectedIndexChanged">
                                <asp:ListItem Value="10" Text="10"></asp:ListItem>
                                <asp:ListItem Value="20" Text="20"></asp:ListItem>
                                <asp:ListItem Value="50" Text="50"></asp:ListItem>
                                <asp:ListItem Value="100" Text="100"></asp:ListItem>
                                <asp:ListItem Value="150" Text="150"></asp:ListItem>
                                <asp:ListItem Value="200" Text="200"></asp:ListItem>
                                <asp:ListItem Value="250" Text="250"></asp:ListItem>
                                <asp:ListItem Value="300" Text="300"></asp:ListItem>
                            </asp:DropDownList>
                        </div>
         
                    </div>

                    <div class="row">
                 
                        <div class="col-md-2">
                            <asp:Label ID="LblMat" runat="server">Select Material</asp:Label>
                            <asp:DropDownList ID="ddlMatList" CssClass="form-control form-control-sm chzn-select" AutoPostBack="true" runat="server" OnSelectedIndexChanged="ddlMatList_SelectedIndexChanged"></asp:DropDownList>
                        </div>

                        <div class="col-md-2">
                            <asp:Label ID="Label1" runat="server">Material Group</asp:Label>
                            <asp:DropDownList ID="ddlMatGroup" CssClass="form-control form-control-sm chzn-select" runat="server"></asp:DropDownList>
                        </div>

                        <div class="col-md-2">
                            <asp:LinkButton ID="imgPreVious" runat="server" CssClass="" OnClick="imgPreVious_Click" TabIndex="3">
                                <i class="fa fa-search mr-1"></i>Previous List
                            </asp:LinkButton>
                            <asp:DropDownList ID="ddlPrevList" runat="server" CssClass="form-control form-control-sm chzn-select" TabIndex="6" AutoPostBack="True" OnSelectedIndexChanged="ddlPrevList_SelectedIndexChanged">
                            </asp:DropDownList>
                        </div>

                    </div>



                    <%--<div class="col-md-4 pading5px">
                            <asp:Label ID="lblmsg" runat="server" CssClass="btn-danger btn primaryBtn" Visible="false"></asp:Label>
                        </div>--%>

                    <div class="col-md-3" style="display: none;">
                        <asp:Label ID="lblSalesOrder" runat="server" CssClass="">Store</asp:Label>
                        <asp:DropDownList ID="ddlStore" runat="server" CssClass="form-control form-control-sm chzn-select" TabIndex="6" AutoPostBack="True"
                            OnSelectedIndexChanged="ddlStore_SelectedIndexChanged">
                        </asp:DropDownList>
                    </div>

                    <div class="col-md-3" style="display: none;">
                        <asp:Label ID="lblClientName" runat="server" CssClass="">Issue No</asp:Label>
                        <asp:DropDownList ID="ddlissuno" runat="server" CssClass="form-control form-control-sm chzn-select">
                        </asp:DropDownList>
                    </div>


                </div>
            </div>


            <div class="card card-fluid">
                <div class="card-body" style="min-height: 500px;">
                    <div class="table-responsive">

                        <asp:GridView ID="gvmetinf" runat="server" AllowPaging="True" CssClass="table-striped table-hover table-bordered grvContentarea gvTopHeader"
                            AutoGenerateColumns="False"
                            ShowFooter="True" OnPageIndexChanging="gvmetinf_PageIndexChanging">
                            <PagerSettings Position="Bottom" />
                            <AlternatingRowStyle />
                            <Columns>
                                <asp:TemplateField HeaderText="Sl.">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvSlNo2" runat="server" Height="16px"
                                            Style="text-align: right"
                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="25px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Wip Name">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvwip" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "bactdesc")) %>'
                                            Width="200px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Issue No">
                                    <ItemTemplate>
                                        <asp:Label ID="lgIssue" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "misuno")) %>'
                                            Width="100px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Store Name">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvstore" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "storedesc")) %>'
                                            Width="180px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Rsircode" Visible="False">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvprocode" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rsircode")) %>'
                                            Width="80px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Description" Visible="true">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvbatchcode" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sirdesc")) %>'
                                            Width="210px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Specification" Visible="true">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvspcfdesc" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "spcfdesc")) %>'
                                            Width="120px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Left" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="QTY">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvSdelno1" runat="server"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty")).ToString("#,##0.0000;(#,##0.0000); ") %>'
                                            Width="90px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Left" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Rate">
                                    <ItemTemplate>
                                        <asp:Label ID="lblinvrate" runat="server" BackColor="Transparent"
                                            BorderStyle="None" Font-Size="11px" Style="text-align: right"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "rate")).ToString("#,##0.0000;(#,##0.0000); ") %>'
                                            Width="60px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Amount">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvBatDesc" runat="server" Text='<%#  Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt")).ToString("#,##0.00;(#,##0.00); ") %>' Width="80px">
                                        </asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle HorizontalAlign="Center" />
                                    <HeaderStyle HorizontalAlign="Left" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Return QTY_">
                                    <HeaderTemplate>
                                        <asp:Label runat="server" Text="Return QTY_"></asp:Label>
                                        <asp:LinkButton runat="server" ID="btnClearF" OnClick="btnClearF_Click"><span class="fa fa-eraser border"></span></asp:LinkButton>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtgvRetqty" runat="server" OnKeyUp="GoToNextTextBox(this, event); return false;" TextMode="Number" 
                                            CssClass="form-control form-control-sm text-right text-red" Font-Size="11px"
                                            Text='<%#  Convert.ToDouble(DataBinder.Eval(Container.DataItem, "rqty")).ToString("#,##0.0000;(#,##0.0000); ") %>'
                                            Width="100px"></asp:TextBox>
                                    </ItemTemplate>

                                    <FooterTemplate>
                                        <asp:Label ID="LblFgvRetQty" runat="server" Font-Bold="true" Font-Size="11px" Style="text-align: center"></asp:Label>
                                    </FooterTemplate>

                                    <FooterStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Right" ForeColor="Red" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Amount">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvInvamt" runat="server" BackColor="Transparent"
                                            BorderStyle="None" Font-Size="11px" Style="text-align: right"
                                            Text='<%#  Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ramt")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="80px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="LblFgvRetamt" runat="server" Font-Bold="true" Style="text-align: right"></asp:Label>
                                    </FooterTemplate>
                                    <FooterStyle HorizontalAlign="Right" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="">
                                    <HeaderTemplate>
                                        <asp:CheckBox runat="server" ID="chkbxAll" onclick="javascript:SelectAllCheckboxes(this);" ClientIDMode="Static" />
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:CheckBox runat="server" ID="chkbxMaterial" Width="40px" />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                </asp:TemplateField>

                            </Columns>
                            <FooterStyle CssClass="grvFooter" />
                            <EditRowStyle />
                            <AlternatingRowStyle />
                            <PagerStyle CssClass="gvPagination" />
                            <HeaderStyle CssClass="grvHeader" />
                        </asp:GridView>
                        <br />

                        <%--                        <asp:Panel ID="Panel2" runat="server" Visible="False">
                            <fieldset class="scheduler-border fieldset_Nar">
                                <div class="form-horizontal">
                                    <div class="form-group">
                                        <div class="col-md-6 pading5px">
                                            <div class="input-group">
                                                <span class="input-group-addon glypingraddon">
                                                    <asp:Label ID="lblReqNarr" runat="server" CssClass="lblTxt lblName" Text="Narration:"></asp:Label>
                                                </span>
                                                <asp:TextBox ID="txtBillNarr" runat="server" class="form-control" Rows="2" TextMode="MultiLine"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="col-md-6 pading5px">
                                            <asp:Label ID="Label6" runat="server" CssClass="smLbl_to" Style="margin-left: 31px;">Overall Discount</asp:Label>
                                            <asp:TextBox ID="txtOvDis" runat="server" CssClass="inputtextbox" ReadOnly="true"></asp:TextBox>
                                            <asp:Label ID="Label1" runat="server" CssClass="smLbl_to" Style="margin-left: 31px;">Return Discount</asp:Label>
                                            <asp:TextBox ID="txtRetOvDis" runat="server" CssClass="inputtextbox" ReadOnly="true"></asp:TextBox>
                                        </div>
                                    </div>






                                </div>

                            </fieldset>
                        </asp:Panel>
                        <div class="row">
                            <div class="col-md-12 col-sm-12 col-lg-12">
                                <fieldset class="scheduler-border fieldset_B">
                                    <div class="form-horizontal">
                                        <asp:Panel ID="pnlpro" runat="server" Visible="False" TabIndex="22">


                                            <div class="form-group">
                                                <div class="col-md-3 pading5px asitCol3">
                                                    <asp:Label ID="lblheding" runat="server" CssClass=" dataLblview" Text="Product Details"></asp:Label>


                                                </div>
                                            </div>

                                        </asp:Panel>

                                    </div>
                                </fieldset>
                                <asp:Repeater ID="rpprodetails" runat="server">

                                    <HeaderTemplate>
                                        <table id="tblrpprodetails" class="table-striped table-hover table-bordered grvContentarea saletb">
                                            <tr>
                                                <th>SL</th>
                                                                                 <th>Model</th>

                                                <th>M_IMEI</th>
                                                <th>S_IMEI</th>


                                            </tr>
                                    </HeaderTemplate>
                                    <ItemTemplate>

                                        <tr>
                                            <td>
                                                <asp:Label ID="lblrpSlNo" runat="server" Font-Bold="True"
                                                    Style="text-align: right"
                                                    Text='<%# Convert.ToString(Container.ItemIndex+1)+"." %>' Width="15px"></asp:Label>
                                            </td>

                                            <td>
                                                <asp:Label ID="Label3" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rsirdesc")) %>'
                                                    Width="180px"></asp:Label>
                                            </td>

                                            <td>
                                                <asp:Label ID="lblrpmimei" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mimei")) %>'
                                                    Width="100px"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblrpsimei" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "simei")) %>'
                                                    Width="100px"></asp:Label>
                                            </td>

                                        </tr>

                                    </ItemTemplate>

                                    <FooterTemplate>

                                        <tr>

                                            <th></th>


                                            <th></th>
                                            <th></th>
                                            <th></th>

                                        </tr>


                                        </table>
                                    </FooterTemplate>





                                </asp:Repeater>
                            </div>

                        </div>--%>
                    </div>
                </div>
            </div>

        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>


