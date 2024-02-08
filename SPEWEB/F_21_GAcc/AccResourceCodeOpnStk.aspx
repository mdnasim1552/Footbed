<%@ Page Title="" Language="C#" MasterPageFile="~/SPE.Master" AutoEventWireup="true" CodeBehind="AccResourceCodeOpnStk.aspx.cs" Inherits="SPEWEB.F_21_GAcc.AccResourceCodeOpnStk" %>

<%--<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>--%>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style>
        .chzn-drop {
            width: 200px !important;
        }

            .chzn-drop .chzn-search input[type="text"] {
                width: 100% !important;
            }

        .navbar-fixed-bottom, .navbar-fixed-top {
            z-index: 1;
        }

        .chzn-single {
            border-radius: 3px !important;
            height: 29px !important;
        }

        .Progressbar {
            position: absolute;
            top: 15%;
            left: 45%;
            right: 35%;
            z-index: 599;
        }
    </style>
    <script type="text/javascript">
        function openModal() {
            $('#myModal').modal('toggle', {
                backdrop: 'static',
                keyboard: false
            });
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
            gv.Scrollable();

            $('.chzn-select').chosen({ search_contains: true });
        }

        function Search_Gridview() {

            var strKey = document.getElementById("testdata").value;
            // alert(strKey);
            var tblData = document.getElementById("<%=grvacc.ClientID %>");
            var strData = strKey.toLowerCase().split(" ");
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

            <div class="Progressbar">
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

                    <div class="row pb-3">

                        <div class="col-md-2">
                            <asp:Label ID="LblBookName1" runat="server" CssClass="" Text="Select Code Book:"></asp:Label>
                            <asp:DropDownList ID="ddlOthersBook" AutoPostBack="true" OnSelectedIndexChanged="ddlOthersBook_SelectedIndexChanged" runat="server" CssClass="form-control form-control-sm chzn-select">
                            </asp:DropDownList>
                            <%--<asp:Label ID="lbalterofddl" runat="server" Visible="False" CssClass="form-control inputTxt"></asp:Label>--%>
                        </div>

                        <div class="col-md-2">
                            <asp:Label ID="lblGroup" runat="server" CssClass="" Text="Group"></asp:Label>
                            <asp:DropDownList ID="ddlOthersBookSegment" CssClass="form-control form-control-sm chzn-select" runat="server">
                            </asp:DropDownList>
                            <%--<asp:Label ID="lbalterofddl0" runat="server" Visible="False" CssClass="form-control inputTxt"></asp:Label>--%>
                        </div>

                        <div class="col-md-1">
                            <asp:LinkButton ID="lnkok" runat="server" Text="Ok" OnClick="lnkok_Click" CssClass="btn btn-sm btn-primary" Style="margin-top: 20px;"></asp:LinkButton>
                        </div>
                        <div class="col-md-2 ">
                            <div class="form-group">
                            <asp:Label ID="LblBookName2" runat="server" Text="Search Option:"></asp:Label>

                           
                             <div class="input-group input-group-sm input-group-alt">
                                 <asp:TextBox ID="txtsrch" runat="server" CssClass="form-control form-control-sm "></asp:TextBox>


                                                            <div class="input-group-append">
                                                                  <asp:LinkButton ID="ibtnSrch" runat="server" OnClick="ibtnSrch_Click" CssClass="btn btn-success btn-sm">
                                <span class="fa fa-search"></span></asp:LinkButton>



                                                            </div>
                                                        </div>
                          
                        </div>
                             </div>
                        <div class="col-md-2">
                            <asp:Label ID="Label2" runat="server" CssClass="">Center Name</asp:Label>
                            <asp:DropDownList ID="ddlProject" runat="server" CssClass="form-control form-control-sm chzn-select ">
                            </asp:DropDownList>
                        </div>

                        <div class="col-md-1">
                            <asp:Label ID="Label1" runat="server" CssClass="" Text="Filter"></asp:Label>
                            <input type="text" id="testdata" class="form-control form-control-sm" onkeyup="Search_Gridview()" />
                        </div>

                        
                        <div class="col-md-1">
                            <asp:Label ID="lblPage" runat="server" CssClass="" Text="Page Size" Visible="False"></asp:Label>
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
                            </asp:DropDownList>
                        </div>

                    </div>

                </div>
            </div>

            <div class="card card-fluid" style="min-height: 450px;">
                <div class="card-body">

                    <asp:GridView ID="grvacc" runat="server" AllowPaging="True"
                        AutoGenerateColumns="False"
                        PageSize="15" OnPageIndexChanging="grvacc_PageIndexChanging" OnRowDataBound="grvacc_RowDataBound"
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

                            <asp:TemplateField HeaderText=" ">
                                 <HeaderTemplate>
                                    <asp:Label runat="server" ID="lblCode" Text="Code" CssClass="mr-3"></asp:Label>
                                    <asp:HyperLink runat="server" ID="hyplnkExcel" CssClass="btn btn-sm btn-success">
                                        <i class="fa fa-file-excel"></i>
                                    </asp:HyperLink>
                                </HeaderTemplate>
                               
                                <ItemTemplate>
                                    <asp:Label ID="lblsircode" runat="server" 
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sircode")) %>'
                                        Width="90px"></asp:Label>
                                    <asp:Label ID="lblgrcode" runat="server" Visible="false"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sircode2"))+"-" %>'
                                        Width="90px"></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Font-Size="12px" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="" Visible="false">

                               

                                <EditItemTemplate>
                                    <asp:TextBox ID="txtgrcode" runat="server" Font-Size="12px" MaxLength="13"
                                        Style="border-top-style: none; border-right-style: none; border-left-style: none; border-bottom-style: none"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sircode4")) %>'
                                        Width="80px"></asp:TextBox>
                                </EditItemTemplate>

                                <ItemTemplate>
                                    <asp:HyperLink ID="lbgrcod3" runat="server" Font-Size="12px" Font-Underline="false" ForeColor="Black"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sircode4")) %>'
                                        Width="80px"></asp:HyperLink>
                                </ItemTemplate>

                                <ItemStyle Font-Size="12px" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Item code">
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtgridsirtype" runat="server" Font-Size="12px" MaxLength="20"
                                        Style="border-top-style: none; border-right-style: none; border-left-style: none; border-bottom-style: none"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sirtype")) %>'
                                        Width="150px"></asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lbltype" runat="server" Font-Size="12px"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sirtype")) %>'
                                        Width="150px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle Font-Bold="True" Font-Size="12px" />
                                <ItemStyle Font-Size="12px" HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Description of Code" HeaderStyle-Width="200px">


                                <ItemTemplate>
                                    <asp:TextBox ID="txtgvDesc" runat="server" Font-Size="12px" MaxLength="250"
                                        BorderColor="ForestGreen" BorderStyle="Solid" BorderWidth="1"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sirdesc")) %>'
                                        Width="200px"></asp:TextBox>



                                </ItemTemplate>
                                <HeaderStyle />
                                <ItemStyle Width="180px" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Spc. Code">

                                <ItemTemplate>
                                    <asp:Label ID="lblspcfcode" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "spcfcod")) %>'></asp:Label>
                                </ItemTemplate>

                                <ItemStyle HorizontalAlign="Center" Width="100px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="THICKNESS/SIZE">

                                <ItemTemplate>
                                    <asp:TextBox ID="txtgvsize" runat="server" MaxLength="100"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "size")) %>'
                                        Width="100px"></asp:TextBox>
                                    <asp:Label ID="LblSpcfcod" runat="server" Visible="false" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "spcfcod")) %>'></asp:Label>


                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" Width="100px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="WIDTH <br>LEN">

                                <ItemTemplate>
                                    <asp:TextBox ID="txtgvWidth" runat="server" MaxLength="100"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "thickness")) %>'
                                        Width="40px"></asp:TextBox>
                                </ItemTemplate>

                                <ItemStyle HorizontalAlign="Center" Width="10px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Color">

                                <ItemTemplate>
                                    <asp:DropDownList ID="txtgvColor" runat="server" CssClass="chzn-select" Width="70px"></asp:DropDownList>



                                </ItemTemplate>

                                <ItemStyle HorizontalAlign="Center" Width="10px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Color <br> Code" Visible="false">
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtgvColorCode" runat="server" MaxLength="100"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "desc5")) %>'
                                        Width="40px"></asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblcolorCode" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "desc5")) %>'
                                        Width="40px"></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" Width="10px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Brand">

                                <ItemTemplate>
                                    <asp:TextBox ID="txtgvBrand" runat="server" MaxLength="100"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "brand")) %>'
                                        Width="50px"></asp:TextBox>
                                </ItemTemplate>

                                <ItemStyle HorizontalAlign="Center" Width="10px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Other">

                                <ItemTemplate>
                                    <asp:TextBox ID="txtgvOther" runat="server" MaxLength="100"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "other")) %>'
                                        Width="50px"></asp:TextBox>
                                </ItemTemplate>

                                <ItemStyle HorizontalAlign="Center" Width="10px" />
                            </asp:TemplateField>


                            <asp:TemplateField HeaderText="Unit">
                                <EditItemTemplate>
                                    <asp:DropDownList ID="ddlunit" runat="server" CssClass="ddlPage62 inputTxt chzn-select" Width="60" AutoPostBack="true" TabIndex="2">
                                    </asp:DropDownList>


                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblunit2" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sirunit")) %>'
                                        Width="40px"></asp:Label>
                                </ItemTemplate>

                                <ItemStyle HorizontalAlign="Center" Width="10px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Qty">

                                <ItemTemplate>
                                    <asp:TextBox ID="txtgvQty" runat="server" Font-Size="12px"
                                        Style="border-top-style: none; border-right-style: none; border-left-style: none; border-bottom-style: none"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty")).ToString() %>'
                                        Width="50px"></asp:TextBox>
                                </ItemTemplate>

                                <ItemStyle HorizontalAlign="Center" Width="10px" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Std.Rate">

                                <ItemTemplate>
                                    <asp:TextBox ID="txtgvsirval" runat="server" Font-Size="12px" MaxLength="100"
                                        Style="border-style: none;"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem,  "sirval")).ToString("#,##0.00;(#,##0.00); ") %>'
                                        Width="50px"></asp:TextBox>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="left" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Bond Name">

                                <ItemTemplate>
                                    <asp:TextBox ID="txtgvsirtdesc" runat="server" Font-Size="12px"
                                        Style="border-top-style: none; border-right-style: none; border-left-style: none; border-bottom-style: none"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sirtdes")) %>'
                                        Width="50px"></asp:TextBox>
                                </ItemTemplate>

                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>





                            <asp:TemplateField HeaderText="Sizeble?">
                                <ItemTemplate>
                                    <asp:DropDownList ID="DdlSizelble" runat="server" Width="50px">
                                        <asp:ListItem Value="1">True</asp:ListItem>
                                        <asp:ListItem Value="0">False</asp:ListItem>
                                    </asp:DropDownList>
                                </ItemTemplate>
                                <EditItemTemplate>
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
            </div>

        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>
