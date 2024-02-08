<%@ Page Title="" Language="C#" MasterPageFile="~/SPE.Master" AutoEventWireup="true" CodeBehind="AccResourceCode.aspx.cs" Inherits="SPEWEB.F_21_GAcc.AccResourceCode" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script src="../Scripts/table2excel.js"></script>
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
        function openModal() {
            $('#myModal').modal('toggle', {
                backdrop: 'static',
                keyboard: false
            });
        }
        function ExcelPrint() {

            var xl = new Table2Excel();
            xl.export(document.getElementById("<%=grvacc.ClientID %>"));
            alert('ok');
        }
        function openModalSup() {
            $('#myModal2').modal('toggle', {
                backdrop: 'static',
                keyboard: false
            });
        }

        function CLoseMOdal() {
            $('#myModal').modal('hide');
        }
        $(document).ready(function () {

            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);
        });
        function pageLoaded() {

            var gv = $('#<%=this.grvacc.ClientID %>');
            gv.Scrollable({
                ScrollHeight: 500,

            });

            $('.chzn-select').chosen({ search_contains: true });
        }

        function Search_Gridview(strKey, cellNr, gvName) {

            //var strKey = document.getElementById("testdata").value;
            // alert(strKey);
            var tblData = document.getElementById("<%=grvacc.ClientID %>");
            var strData = strKey.value.toLowerCase().split(" ");
            //  alert(strData);

            var rowData;
            for (var i = 1; i < tblData.rows.length; i++) {
                rowData = tblData.rows[i].innerHTML;

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


    </script>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>

            <div class="nahidProgressbar">
                <asp:UpdateProgress ID="UpdateProgress2" runat="server" AssociatedUpdatePanelID="UpdatePanel1" DisplayAfter="30">
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
                <div class="card-body">
                    <div class="row">
                        <div class="col-md-2">
                            <div class="form-group">
                                <asp:Label ID="LblBookName1" runat="server" CssClass="lblTxt lblName" Text="Select Code Book:" Width="120"></asp:Label>
                                <asp:DropDownList ID="ddlOthersBook" AutoPostBack="true" OnSelectedIndexChanged="ddlOthersBook_SelectedIndexChanged" runat="server" CssClass="form-control form-control-sm chzn-select">
                                </asp:DropDownList>
                                <asp:Label ID="lbalterofddl" runat="server" Visible="False" CssClass="form-control form-control-sm"></asp:Label>
                            </div>
                        </div>

                        <div class="col-md-2">
                            <div class="form-group">
                                <asp:Label ID="LblSubgroup" runat="server" Text="Select Group"></asp:Label>
                                <asp:DropDownList ID="ddlOthersBookSegment" AutoPostBack="true" OnSelectedIndexChanged="ddlOthersBookSegment_SelectedIndexChanged" CssClass="form-control form-control-sm chzn-select" runat="server">
                                </asp:DropDownList>
                                <asp:Label ID="lbalterofddl0" runat="server" Visible="False" CssClass="form-control form-control-sm"></asp:Label>
                            </div>
                        </div>
                        <div class="col-md-2">
                            <div class="form-group">
                                <asp:Label ID="Label2" runat="server" Text="Select Materials"></asp:Label>
                                <asp:DropDownList ID="ddlmaterials" CssClass="form-control form-control-sm chzn-select" runat="server">
                                    <asp:ListItem Selected="True" Value="000000000000" Text="----Select All---"></asp:ListItem>
                                </asp:DropDownList>
                                <asp:Label ID="Label3" runat="server" Visible="False" CssClass="form-control form-control-sm"></asp:Label>
                            </div>
                        </div>
                        <div class="col-md-1">
                            <div class="form-group" style="margin-top: 20px">
                                <asp:LinkButton ID="lnkok" runat="server" Text="Ok" OnClick="lnkok_Click" CssClass="btn btn-primary btn-sm"></asp:LinkButton>
                            </div>

                        </div>
                        <div class="col-md-2">
                            <div class="form-group">
                                <asp:Label ID="Label1" runat="server" CssClass="label" Text="Search"></asp:Label>

                                <div class="input-group input-group-sm input-group-alt">

                                    <asp:TextBox type="text" runat="server" ID="testdata" class="form-control rp_search" onkeyup="Search_Gridview()"></asp:TextBox>

                                    <div class="input-group-append">
                                        <asp:LinkButton ID="ibtnSrch" runat="server" OnClick="ibtnSrch_Click" CssClass="input-group-text"><span class="fa fa-search"></span></asp:LinkButton>

                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="col-md-1">
                            <div class="form-group">
                                <asp:TextBox ID="txtsrch" runat="server" Visible="false" CssClass="inputTxt hidden"></asp:TextBox>
                                <asp:Label ID="lblPage" runat="server" CssClass="lblTxt lblName" Text="Page Size" Visible="False"></asp:Label>
                                <asp:DropDownList ID="ddlpagesize" runat="server" AutoPostBack="True" CssClass="form-control form-control-sm"
                                    OnSelectedIndexChanged="ddlpagesize_SelectedIndexChanged" Visible="False">
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
                                    <asp:ListItem>1200</asp:ListItem>
                                    <asp:ListItem>1500</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="card card-fluid">
                <div class="card-body" style="min-height: 550px;">

                    <asp:MultiView ID="mvMaterial" runat="server">

                        <asp:View ID="CodeOpeningView" runat="server">

                            <div class="table-responsive">

                                <asp:GridView ID="grvacc" runat="server" AllowPaging="True"
                                    AutoGenerateColumns="False" OnRowCancelingEdit="grvacc_RowCancelingEdit" OnRowEditing="grvacc_RowEditing"
                                    OnRowUpdating="grvacc_RowUpdating" PageSize="15" OnPageIndexChanging="grvacc_PageIndexChanging" OnRowDataBound="grvacc_RowDataBound"
                                    CssClass="table-striped table-hover table-bordered grvContentarea">
                                    <PagerSettings NextPageText="Next" PreviousPageText="Previous" Position="Top"
                                        Mode="NumericFirstLast" />
                                    <FooterStyle BackColor="#5F9467" />
                                    <Columns>

                                        <asp:TemplateField HeaderText="Sl">
                                            <ItemTemplate>
                                                <asp:HyperLink ID="hlnkSerialbtn1" runat="server" Target="_blank" Font-Underline="false" Font-Size="9px"
                                                    Width="30px" Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' OnClick="lnkSerialbtn_Click"></asp:HyperLink>
                                            </ItemTemplate>
                                            <HeaderStyle />
                                            <ItemStyle />
                                        </asp:TemplateField>

                                        <asp:CommandField DeleteText="" HeaderText="Edit" InsertText="" NewText="" SelectText="" ShowEditButton="True">
                                            <HeaderStyle />
                                            <ItemStyle />
                                        </asp:CommandField>
                                        <asp:TemplateField HeaderText=" ">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lbtnAdd" Visible="false" runat="server" Text='Add' OnClick="lbtnAdd_Click" Width="20px"></asp:LinkButton>
                                            </ItemTemplate>
                                            <ItemStyle Font-Size="9px" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText=" ">
                                            <EditItemTemplate>
                                                <asp:Label ID="lbgrcode" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sircode2"))+"-" %>'
                                                    Width="20px"></asp:Label>
                                                <asp:Label ID="lblsircode1" runat="server" Visible="false"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sircode")) %>'
                                                    Width="20px"></asp:Label>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvsircode" runat="server" Visible="false"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sircode")) %>'
                                                    Width="20px"></asp:Label>
                                                <asp:Label ID="lblgrcode" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sircode2"))+"-" %>'
                                                    Width="20px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Font-Size="12px" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Code">
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtgrcode" ReadOnly="true" runat="server" Font-Size="9px"
                                                    MaxLength="13"
                                                    Style="border-top-style: none; border-right-style: none; border-left-style: none; border-bottom-style: none; font-size: 12px; border-left-color: midnightblue; border-bottom-color: midnightblue; border-top-color: midnightblue; border-right-color: midnightblue;"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sircode4")) %>'
                                                    Width="60px"></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:HyperLink ID="lbgrcod3" runat="server" Font-Size="9px" Font-Underline="false" ForeColor="Black"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sircode4")) %>'
                                                    Width="60px"></asp:HyperLink>
                                            </ItemTemplate>

                                            <ItemStyle Font-Size="9px" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Item code">
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtgridsirtype" runat="server" Font-Size="9px" MaxLength="20" Enabled="false"
                                                    Style="border-top-style: none; border-right-style: none; border-left-style: none; border-bottom-style: none"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sirtype")) %>'
                                                    Width="90px"></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lbltype" runat="server" Font-Size="9px"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sirtype")) %>'
                                                    Width="90px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle Font-Bold="True" Font-Size="12px" />
                                            <ItemStyle Font-Size="12px" HorizontalAlign="Center" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Description of Code" HeaderStyle-Width="120px">
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtgvDesc" runat="server" Font-Size="9px" MaxLength="250"
                                                    Style="border-style: none;"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sirdesc")) %>'
                                                    Width="120px"></asp:TextBox>
                                            </EditItemTemplate>

                                            <ItemTemplate>
                                                <asp:HyperLink ID="hlnkgvdesc" runat="server" BorderColor="#99CCFF" BorderStyle="none"
                                                    Font-Size="9px" Style="text-align: left; background-color: Transparent; color: Black;"
                                                    Font-Underline="false" Target="_blank" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sirdesc"))  %>'
                                                    Width="120px">
                                                </asp:HyperLink>
                                            </ItemTemplate>
                                            <HeaderStyle />
                                            <ItemStyle Width="120px" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="">

                                            <ItemTemplate>
                                                <asp:LinkButton ID="lbtnAddSpcf" runat="server" Text='Click' OnClick="lbtnAddSpcf_Click" Width="20px"></asp:LinkButton>

                                                <asp:HyperLink ID="lnkSpcf" Visible="false" runat="server" Target="_blank" Font-Underline="false" Font-Size="9px"
                                                    Width="30px" Text="Click"></asp:HyperLink>

                                            </ItemTemplate>
                                            <ItemStyle Font-Size="9px" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Spc. Code">
                                            <ItemTemplate>
                                                <asp:Label ID="lblspcfcode" Font-Size="10px" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "spcfcod")) %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" Width="70px" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Thickness/Size">
                                            <HeaderTemplate>
                                                <asp:TextBox ID="txtSrchThickns" BackColor="Transparent" BorderStyle="None" runat="server" Width="110px" placeholder="Size/Thickness" onkeyup="Search_Gridview(this, 8, 'grvacc')"></asp:TextBox><br />
                                            </HeaderTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtgvsize" runat="server" MaxLength="100" Font-Size="9px"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "size")) %>'
                                                    Width="70px"></asp:TextBox>
                                                <asp:Label ID="LblSpcfcod" runat="server" Visible="false" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "spcfcod")) %>'></asp:Label>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblspcf" runat="server" Visible="false" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "spcfcod")) %>'></asp:Label>

                                                <asp:Label ID="lblsize" Font-Size="9px" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "size")) %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" Width="80px" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="LxW">
                                            <HeaderTemplate>
                                                <asp:TextBox ID="txtSrchLxw" BackColor="Transparent" BorderStyle="None" runat="server" Width="110px" placeholder="Lxw" onkeyup="Search_Gridview(this, 9, 'grvacc')"></asp:TextBox><br />
                                            </HeaderTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtgvThickness" runat="server" MaxLength="100"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "thickness")) %>'
                                                    Width="40px"></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblthickness" Font-Size="9px" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "thickness")) %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" Width="10px" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Color">
                                            <HeaderTemplate>
                                                <asp:TextBox ID="txtSrchClr" BackColor="Transparent" BorderStyle="None" runat="server" Width="110px" placeholder="Color" onkeyup="Search_Gridview(this, 10, 'grvacc')"></asp:TextBox><br />
                                            </HeaderTemplate>
                                            <EditItemTemplate>
                                                <asp:DropDownList ID="txtgvColor" Font-Size="9px" runat="server" CssClass="chzn-select" Width="100px"></asp:DropDownList>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblcolor" Font-Size="9px" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "color")) %>'
                                                    Width="60px"></asp:Label>

                                                <asp:Label ID="lblcolorid" Visible="false" Font-Size="9px" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "colorid")) %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" Width="10px" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Color <br> Code" Visible="false">
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtgvColorCode" runat="server" MaxLength="100" Font-Size="9px"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "desc5")) %>'
                                                    Width="40px"></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblcolorCode" Font-Size="9px" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "desc5")) %>'
                                                    Width="40px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" Width="10px" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Brand">
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtgvBrand" runat="server" MaxLength="100" Font-Size="9px"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "brand")) %>'
                                                    Width="40px"></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblBrand" runat="server" Font-Size="9px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "brand")) %>'
                                                    Width="40px"></asp:Label>
                                            </ItemTemplate>

                                            <ItemStyle HorizontalAlign="Center" Width="10px" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Other">
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtgvOther" runat="server" MaxLength="100" Font-Size="9px"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "other")) %>'
                                                    Width="70px"></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblOther" runat="server" Font-Size="9px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "other")) %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" Width="70px" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Allow">
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtgvAllowance" runat="server" MaxLength="100" Font-Size="9px"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "Allowance")) %>'
                                                    Width="30px"></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblAllowance" runat="server" Font-Size="9px" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "Allowance")).ToString("#,##0.00;(#,##0.00); ")  %>'
                                                    Width="30px"></asp:Label>
                                            </ItemTemplate>

                                            <ItemStyle HorizontalAlign="Center" Width="30px" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Unit" Visible="false">
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

                                        <asp:TemplateField HeaderText="Unit">
                                            <EditItemTemplate>
                                                <asp:DropDownList ID="ddlunit" runat="server" CssClass="ddlPage62 inputTxt chzn-select" Width="60" AutoPostBack="true" TabIndex="2">
                                                </asp:DropDownList>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblunit2" runat="server" Font-Size="9px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sirunit")) %>'
                                                    Width="40px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" Width="8px" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Std.Rate">
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtgvsirval" runat="server" Font-Size="9px" MaxLength="100"
                                                    Style="border-style: none; text-align: right !important"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem,  "sirval")).ToString("#,##0.000000;(#,##0.000000); ") %>'
                                                    Width="50px"></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblsirval" runat="server" Font-Size="9px"
                                                    Style="font-size: 10px; text-align: right;"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem,  "sirval")).ToString("#,##0.000000;(#,##0.000000); ") %>'
                                                    Width="50px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle HorizontalAlign="left" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Bond" HeaderStyle-Width="50">
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtgvsirtdesc" runat="server" Font-Size="9px"
                                                    Style="border-top-style: none; border-right-style: none; border-left-style: none; border-bottom-style: none"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sirtdes")) %>'
                                                    Width="50px"></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lbltypedesc" runat="server" Font-Size="9px"
                                                    Style="font-size: 9px"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sirtdes")) %>'
                                                    Width="50px"></asp:Label>
                                            </ItemTemplate>

                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Incoterms" HeaderStyle-Width="50">
                                            <EditItemTemplate>

                                                <asp:DropDownList ID="ddlgvinco" runat="server" CssClass="ddlPage62 inputTxt chzn-select" Width="60" AutoPostBack="true" TabIndex="2">
                                                </asp:DropDownList>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvinco" runat="server" Font-Size="9px"
                                                    Style="font-size: 9px"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sirtdes")) %>'
                                                    Width="50px"></asp:Label>
                                            </ItemTemplate>

                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="C&F (%)">
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtgvCfPercnt" runat="server" Font-Size="9px" MaxLength="100"
                                                    Style="border-style: none;"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem,  "mark")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="30px"></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblCfPercnt" runat="server" Font-Size="9px"
                                                    Style="font-size: 9px"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem,  "mark")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="30px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" Width="30px" />
                                            <ItemStyle HorizontalAlign="left" Width="30px" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Full Name" HeaderStyle-Width="160">
                                            <ItemTemplate>
                                                <asp:Label ID="lblFullName" runat="server" Font-Size="9px"
                                                    Style="font-size: 9px"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "fullname")) %>'
                                                    Width="160px"></asp:Label>
                                            </ItemTemplate>

                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>

                                        <asp:TemplateField>
                                            <HeaderTemplate>
                                                <asp:LinkButton ID="lbtnallinone" OnClick="lbtnallinone_Click" runat="server">All</asp:LinkButton>

                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:LinkButton ID="LbtnGenerat" OnClick="LbtnGenerat_Click" runat="server"><span class="fa fa-barcode"></span></asp:LinkButton>
                                            </ItemTemplate>
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

                                        <asp:TemplateField HeaderText="Size?">
                                            <ItemTemplate>
                                                <asp:Label ID="lbgvSizeble" runat="server"
                                                    Text='<%# (Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "sizeble"))==true)?"True":"False" %>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:DropDownList ID="DdlSizelble" runat="server" Width="90px">
                                                    <asp:ListItem Value="1">True</asp:ListItem>
                                                    <asp:ListItem Selected="True" Value="0">False</asp:ListItem>
                                                </asp:DropDownList>
                                            </EditItemTemplate>
                                        </asp:TemplateField>
                                        
                                        <asp:TemplateField HeaderText="Convertible?">
                                            <ItemTemplate>
                                                <asp:Label ID="lbgvConvertible" runat="server"
                                                    Text='<%# (Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "convertible"))==true)?"True":"False" %>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:DropDownList ID="DdlConvertible" runat="server" Width="90px">
                                                    <asp:ListItem Value="1">True</asp:ListItem>
                                                    <asp:ListItem Selected="True" Value="0">False</asp:ListItem>
                                                </asp:DropDownList>
                                            </EditItemTemplate>
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

                        </asp:View>

                        <asp:View ID="PriceSummaryView" runat="server">

                            <div class="table-responsive">

                                <div class="table-responsive">
                                    <asp:GridView ID="gvMatPriceSumm" runat="server" AllowPaging="True"
                                        AutoGenerateColumns="False"
                                        PageSize="15" OnPageIndexChanging="gvMatPriceSumm_PageIndexChanging"
                                        CssClass="table-striped table-hover table-bordered grvContentarea">
                                        <PagerSettings NextPageText="Next" PreviousPageText="Previous" Position="Top"
                                            Mode="NumericFirstLast" />
                                        <FooterStyle BackColor="#5F9467" />
                                        <Columns>

                                            <asp:TemplateField HeaderText="Sl">
                                                <ItemTemplate>
                                                    <asp:HyperLink ID="hlnkSerialbtn1" runat="server" Target="_blank" Font-Underline="false" Font-Size="11px" CssClass="text-center"
                                                        Width="30px" Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' OnClick="lnkSerialbtn_Click"></asp:HyperLink>
                                                </ItemTemplate>
                                                <HeaderStyle />
                                                <ItemStyle />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Material">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvMpsMatDesc" runat="server"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sirdesc")) %>'
                                                        Width="250px"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Font-Size="12px" />
                                            </asp:TemplateField>

                                            <%--<asp:TemplateField HeaderText="Thickness">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvMpsThickness" runat="server"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "thickness")) %>'
                                                        Width="200px"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Font-Size="12px" />
                                            </asp:TemplateField>--%>

                                            <asp:TemplateField HeaderText="Thickness/Size">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvMpsSize" runat="server"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "size")) %>'
                                                        Width="150px"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Font-Size="12px" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Unit">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvMpsUnit" runat="server"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sirunit")) %>'
                                                        Width="60px"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Font-Size="12px" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Incoterms">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvMpsSirtdes" runat="server"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sirtdes")) %>'
                                                        Width="80px"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Font-Size="12px" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Rate">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvMpsSirval" runat="server" CssClass="text-right"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "sirval")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                        Width="70px"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Font-Size="12px" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Allowance">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvMpsAllowance" runat="server"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "Allowance")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                        Width="70px"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Font-Size="12px" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="C&F(%)">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvMpsMark" runat="server" CssClass="text-right"
                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "mark")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                        Width="70px"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Font-Size="12px" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Other">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvMpsOther" runat="server"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "other")) %>'
                                                        Width="80px"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Font-Size="12px" />
                                            </asp:TemplateField>

                                            <%--<asp:TemplateField HeaderText="Incoterms">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvMpsIncoterms" runat="server"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "incoterms")) %>'
                                                        Width="60px"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Font-Size="12px" />
                                            </asp:TemplateField>--%>

                                            <%--<asp:TemplateField HeaderText="Untcod">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvMpsUntcod" runat="server"
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "untcod")) %>'
                                                        Width="60px"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Font-Size="12px" />
                                            </asp:TemplateField>--%>
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
                        </asp:View>


                    </asp:MultiView>

                    <br />
                    <br />
                </div>
            </div>

            <div id="myModal" class="modal animated slideInLeft" role="dialog" data-keyboard="false" data-backdrop="static">
                <div class="modal-dialog modal-lg">
                    <div class="modal-content  ">
                        <div class="modal-header">
                            <h4 class="modal-title">
                                <span class="fa fa-table mr-2"></span>Add New Materials
                            </h4>
                        </div>
                        <div class="modal-body form-horizontal">
                            <div class="row-fluid">
                                <asp:Label ID="lblSircode" runat="server" Visible="false"></asp:Label>
                                <asp:Label ID="lalplug" Text="resource" runat="server" Visible="false"></asp:Label>
                                <div id="Recourcepanel" class="row" runat="server">
                                    <div class="col-md-12">
                                        <div class="form-group" id="mantname" runat="server" visible="false">
                                            <label class="label">Material Name</label>

                                            <asp:TextBox ID="TxtMatName" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-12">
                                        <div class="form-group">
                                            <label class="label">Name </label>
                                            <asp:TextBox ID="txtDescription" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-4">
                                        <div class="form-group">
                                            <label class="label">Unit </label>
                                            <asp:DropDownList ID="ddlUnit" runat="server" CssClass="form-control form-control-sm chzn-select"></asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-md-4">
                                        <div class="form-group">
                                            <label class="label">Value </label>

                                            <asp:TextBox ID="TxtValue" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-4">
                                        <div class="form-group">
                                            <label class="label">C&F%</label>

                                            <asp:TextBox ID="txtCF" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="col-md-12">
                                        <div class="form-group">
                                            <label class="label">Bond Name </label>
                                            <asp:TextBox ID="txtBondName" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <asp:GridView ID="gvSpcfinfo" runat="server" AutoGenerateColumns="False" CssClass=" table-striped table-hover table-bordered grvContentarea"
                                    ShowFooter="True" OnRowDataBound="gvSpcfinfo_RowDataBound" OnRowDeleting="gvSpcfinfo_RowDeleting">
                                    <RowStyle />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sl">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvSlNo0" runat="server"
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:CommandField ShowDeleteButton="True" Visible="false" ControlStyle-CssClass="btn btn-xs btn-danger" DeleteText="<span class='glyphicon glyphicon-remove'></span>" />

                                        <asp:TemplateField HeaderText="Thickness/Size">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtgvMSize" runat="server" AutoCompleteType="Disabled"
                                                    BackColor="Transparent" BorderStyle="solid" BorderWidth="1px" BorderColor="#6acc39"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "spcfdesc")) %>'
                                                    Width="150px"></asp:TextBox>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="LxW">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtgMThikness" runat="server" AutoCompleteType="Disabled"
                                                    BackColor="Transparent" BorderStyle="solid" BorderWidth="1px" BorderColor="#6acc39"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "desc1")) %>'
                                                    Width="80px"></asp:TextBox>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Short Code" Visible="false">
                                            <ItemTemplate>
                                                <asp:TextBox ID="TxtProdCode" runat="server" AutoCompleteType="Disabled"
                                                    BackColor="Transparent" BorderStyle="solid" BorderWidth="1px" BorderColor="#6acc39"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "desc5")) %>'
                                                    Width="100px"></asp:TextBox>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Color">
                                            <ItemTemplate>
                                                <asp:DropDownList ID="txtgvMColor" runat="server" CssClass="chzn-select" Width="100px"></asp:DropDownList>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Brand">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtMBrand" runat="server" AutoCompleteType="Disabled"
                                                    BackColor="Transparent" BorderStyle="solid" BorderWidth="1px" BorderColor="#6acc39"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "desc3")) %>'
                                                    Width="120px"></asp:TextBox>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Price">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtPrice" runat="server" AutoCompleteType="Disabled"
                                                    BackColor="Transparent" BorderStyle="solid" BorderWidth="1px" BorderColor="#6acc39"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sirval")) %>'
                                                    Width="90px" CssClass="text-right"></asp:TextBox>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Remarks">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtgMOther" runat="server" AutoCompleteType="Disabled"
                                                    Font-Size="11px" BorderStyle="solid" BorderWidth="1px" BorderColor="#6acc39"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "desc4")) %>'
                                                    Width="180px"></asp:TextBox>
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


                        </div>
                        <div class="modal-footer ">
                            <asp:LinkButton ID="lblbtnSave" runat="server" CssClass="btn btn-sm btn-success" OnClientClick="CLoseMOdal();" OnClick="lblbtnSave_Click"><span class="glyphicon glyphicon-save"></span> Update </asp:LinkButton>

                            <button type="button" class="btn btn-sm btn-default border-secondary" data-dismiss="modal">Close</button>


                        </div>
                    </div>
                </div>
            </div>


        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
