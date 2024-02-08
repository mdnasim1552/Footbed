<%@ Page Title="" Language="C#" MasterPageFile="~/SPE.Master" AutoEventWireup="true" CodeBehind="AddressDetails.aspx.cs" Inherits="SPEWEB.F_34_Mgt.AddressDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <style>
        .chzn-container-single {
            height: 34px !important;
             
        }

            .chzn-container-single .chzn-single {
                height: 36px !important;
                line-height: 36px;
            }

        /*  .project-slect  .chzn-container-single{
         width: 100px !important;
            height: 34px !important;
        
        }*/
        .profession-slect .chzn-container-single {
            height: 34px !important;
           line-height: 34px !important;
           
        }
        .chzn-container-single .chzn-single span{
            line-height: 34px !important;
        }
    </style>

    <script type="text/javascript">
        $(document).ready(function () {
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);

        });

        function pageLoaded() {
            //$('.datepicker').datepicker({
            //    format: 'mm/dd/yyyy',
            //});
            $("input, select").bind("keydown", function (event) {
                var k1 = new KeyPress();
                k1.textBoxHandler(event);
            });

            $(".chosen-select").chosen({
                search_contains: true,
                no_results_text: "Sorry, no match!",
                allow_single_deselect: true
            });


            $('.chzn-select').chosen({ search_contains: true });

        };

    </script>
    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
        <ContentTemplate>
            <div class="card card-fluid">
                <div class="card-body" style="height:600px;">


                    <div class="row hidden">



                        <div class="col-2 form-check-inline" style="font-size: 12px">
                            <div class="col-6">
                                <asp:Label ID="LblBookName1" Visible="false" runat="server" CssClass="col-md-2 control-label lblTxt" Text="District:"></asp:Label>
                            </div>
                            <div class="col-7">
                                <asp:DropDownList ID="ddlDistLst" Visible="false" runat="server" CssClass="form-control-sm W100Pixel" AutoPostBack="True" OnSelectedIndexChanged="ddlDistLst_SelectedIndexChanged">
                                </asp:DropDownList>
                                <asp:Label ID="lbalterofddl" runat="server" Visible="False" CssClass="form-control inputTxt"></asp:Label>
                            </div>
                        </div>
                        <div class="col-2 form-check-inline" style="font-size: 12px">
                            <div class="col-5">
                                <asp:Label ID="LblUpziLst" Visible="false" runat="server" Text="Upo-Zila:"></asp:Label>
                            </div>
                            <div class="col-7">
                                <asp:DropDownList ID="ddlUpziLst"  Visible="false" CssClass="form-control-sm W100Pixel" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlUpziLst_SelectedIndexChanged">
                                </asp:DropDownList>
                            </div>
                        </div>

                        <div class="col-7 form-check-inline" >

                            <asp:Label ID="lblAddPo" runat="server" CssClass="" Text="Name:"></asp:Label>

                            <asp:TextBox ID="txtAddPo" CssClass="form-control-sm" runat="server" AutoCompleteType="Disabled" Width="100px" BorderColor="gray" BorderStyle="Solid" BorderWidth="1px"></asp:TextBox>

                            <asp:Label ID="lblAddPoBN" runat="server" CssClass="" Text="Name Bangla:"></asp:Label>

                            <asp:TextBox ID="txtAddPoBN" CssClass="form-control-sm" runat="server" AutoCompleteType="Disabled" Width="100px" BorderColor="gray" BorderStyle="Solid" BorderWidth="1px"></asp:TextBox>
                            <asp:Label ID="lblAddPoCod" runat="server" CssClass="" Text="Code:"></asp:Label>
                            <asp:TextBox ID="txtAddPoCod" CssClass="form-control-sm" runat="server" AutoCompleteType="Disabled" Width="100px" BorderColor="gray" BorderStyle="Solid" BorderWidth="1px"></asp:TextBox>
                            <asp:LinkButton ID="lnkNew" Visible="false" runat="server" Text="New" OnClick="lnkok_Click" CssClass="btn btn-sm btn-primary okBtn"></asp:LinkButton>

                        </div>
                    </div>
                   <%-- <br />
                    <hr />--%>
                    <div class="row hidden">

                        <div class="col-md-2 pading5px">
                            <div class="msgHandSt">
                                <asp:Label ID="ConfirmMessage" CssClass="btn-danger btn primaryBtn" runat="server" Visible="false"></asp:Label>
                            </div>
                        </div>


                        <div class="clearfix"></div>


                    </div>
                    <div style="margin-left: 50px">
                        <asp:GridView ID="grvacc" runat="server" AllowPaging="True" CssClass="table-striped table-hover table-bordered grvContentarea"
                            AutoGenerateColumns="False" Font-Size="12px" OnRowDeleting="grvacc_RowDeleting"
                            ageSize="50" Width="60%"
                            ShowFooter="True">
                            <PagerSettings NextPageText="Next" PreviousPageText="Previous" />
                            <FooterStyle Font-Bold="True" />

                            <Columns>
                                <asp:TemplateField HeaderText="Sl">
                                    <ItemTemplate>
                                        <asp:Label ID="lblserialnoid" runat="server" Style="text-align: right"
                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Font-Bold="True" Font-Size="13px" />
                                    <ItemStyle Font-Size="12px" />
                                </asp:TemplateField>


                                <asp:TemplateField HeaderText="District">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgrcode" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "dist"))%>'
                                            Width="80px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Font-Size="12px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="District BN">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgrcode" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "distBN"))%>'
                                            Width="80px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Font-Size="12px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Upozila">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgrcode" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "upziName"))%>'
                                            Width="80px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Font-Size="12px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Upozila BN">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgrcode" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "upziNameBN"))%>'
                                            Width="80px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Font-Size="12px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Post Office">
                                    <ItemTemplate>
                                        <asp:Label ID="lbgrcod3" runat="server" Font-Size="12px"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "poName"))%>'
                                            Width="80px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Font-Bold="True" Font-Size="13px" />
                                    <ItemStyle Font-Size="12px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Post Office BN">
                                    <ItemTemplate>
                                        <asp:Label ID="lbgrcod3" runat="server" Font-Size="12px"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "poNameBN"))%>'
                                            Width="80px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Font-Bold="True" Font-Size="13px" />
                                    <ItemStyle Font-Size="12px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Postal Code">
                                    <ItemTemplate>
                                        <asp:Label ID="lbgrcod" runat="server" Font-Size="12px"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "poNameCod"))%>'
                                            Width="80px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Font-Bold="True" Font-Size="13px" />
                                    <ItemStyle Font-Size="12px" />
                                </asp:TemplateField>
                                <asp:CommandField ShowDeleteButton="True" ControlStyle-CssClass="btn btn-xs btn-danger" DeleteText="Delete" />
                            </Columns>

                            <PagerStyle CssClass="gvPagination" />
                            <HeaderStyle CssClass="grvHeader" />
                            <FooterStyle CssClass="grvFooter" />
                            <AlternatingRowStyle BackColor="" />
                        </asp:GridView>

                    </div>

                      
                  <div class="row">
                  
                    <div class="col-md-5">
                        <div class="form-horizontal">
                            <div class="form-group">
                                <div class="col-md-8 p-0">
                                    <div class="input-group input-group-alt">
                                        <div class="input-group-prepend">
                                            <button class="btn btn-secondary" type="button">Division</button>
                                        </div>
                                        <asp:DropDownList ID="ddldivision" ClientIDMode="Static" OnSelectedIndexChanged="ddldivision_SelectedIndexChanged" data-placeholder="Choose Division" runat="server" CssClass="custom-select chzn-select" AutoPostBack="true">
                                        </asp:DropDownList>
                                         <div class="input-group-prepend" id="AddDv" runat="server">
                                             <asp:LinkButton ID="addDiv" runat="server" OnClick="addDiv_Click" class="btn btn-danger">+ Add</asp:LinkButton>
                                            
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="form-group" id="frm_dis" runat="server" visible="false">
                                <div class="col-md-8 p-0">
                                    <div class="input-group input-group-alt">
                                        <div class="input-group-prepend">
                                            <button class="btn btn-secondary" type="button">District </button>
                                        </div>
                                        <asp:DropDownList ID="ddldist" ClientIDMode="Static" OnSelectedIndexChanged="ddldist_SelectedIndexChanged" data-placeholder="Choose District" runat="server" CssClass="custom-select chzn-select " AutoPostBack="true">
                                        </asp:DropDownList>
                                           <div class="input-group-prepend" id="AddDist" runat="server">
                                             <asp:LinkButton ID="lnkAdDist" runat="server" OnClick="lnkAdDist_Click" class="btn btn-danger">+ Add</asp:LinkButton>
                                            
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="form-group" id="frm_upz" runat="server" visible="false">
                                <div class="col-md-8 p-0">
                                    <div class="input-group input-group-alt">
                                        <div class="input-group-prepend">
                                            <button class="btn btn-secondary" type="button">Upa-zila</button>
                                        </div>
                                        <asp:DropDownList ID="ddlupzila" data-placeholder="Choose Upazila" OnSelectedIndexChanged="ddlupzila_SelectedIndexChanged" runat="server" CssClass="custom-select chzn-select" AutoPostBack="true">
                                        </asp:DropDownList>
                                        <div class="input-group-prepend" id="AddUpz" runat="server">
                                             <asp:LinkButton ID="lnkAdUpz" runat="server" OnClick="lnkAdUpz_Click" class="btn btn-danger">+ Add</asp:LinkButton>
                                            
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="form-group" id="frm_Post" runat="server" visible="false">
                                <div class="col-md-8 p-0">
                                    <div class="input-group input-group-alt profession-slect srDiv">
                                        <div class="input-group-prepend">
                                            <button class="btn btn-secondary" type="button">Post Office</button>
                                        </div>
                                        <asp:DropDownList ID="ddlpost" data-placeholder="Choose Employee" OnSelectedIndexChanged="ddlpost_SelectedIndexChanged" runat="server" CssClass="custom-select chzn-select" AutoPostBack="true">
                                        </asp:DropDownList>
                                        <div class="input-group-prepend" id="AddPost" runat="server">
                                             <asp:LinkButton ID="lnkAdPost" runat="server" OnClick="lnkAdPost_Click" class="btn btn-danger">+ Add</asp:LinkButton>
                                            
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <asp:panel ID="pnlFrom" runat="server" Visible="false">
                            <div class="form-group">
                                <div class="col-md-8 p-0 mt-2 pading5px">
                                    <div class="input-group input-group-alt profession-slect">
                                        <div class="input-group-prepend">
                                            <button class="btn btn-secondary" type="button">Name(Eng)</button>
                                        </div>
                                        <asp:TextBox ID="TxtVillageEng" placeholder="Enter name(Eng)" runat="server" CssClass="form-control"></asp:TextBox>

                                    </div>
                                </div>

                            </div>
                            <div class="form-group"  >
                                <div class="col-md-8 p-0 mt-2 pading5px">
                                    <div class="input-group input-group-alt profession-slect ">
                                        <div class="input-group-prepend">
                                            <button class="btn btn-secondary" type="button">Name(Ban)</button>
                                        </div>
                                        <asp:TextBox ID="TxtVillBan" runat="server" placeholder="Enter name(Ban)" CssClass="form-control"></asp:TextBox>

                                    </div>
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="col-md-8 p-0 mt-2 pading5px">
                                    <div class="input-group input-group-alt profession-slect ">
                                        <div class="input-group-prepend">
                                            <button class="btn btn-secondary" type="button">Post Code</button>
                                        </div>
                                        <asp:TextBox ID="txtCode" runat="server" placeholder="Enter post code" CssClass="form-control"></asp:TextBox>
                                        <div class="input-group-prepend">
                                            <asp:LinkButton ID="lnkSave" runat="server" OnClick="lnkSave_Click"  CssClass="btn btn-primary okBtn">Save</asp:LinkButton>

                                        </div>
                                    </div>
                                </div>
                            </div>

                            </asp:panel>
                        

                            </div>

                        </div>
                    <div class="col-md-6">
                   
                          <asp:GridView ID="GvVillInf0" runat="server" AllowPaging="True" CssClass="table-striped table-hover table-bordered grvContentarea"
                            AutoGenerateColumns="False" Font-Size="12px" 
                            ageSize="50" 
                            ShowFooter="True">
                            <PagerSettings NextPageText="Next" PreviousPageText="Previous" />
                            <FooterStyle Font-Bold="True" />

                            <Columns>
                                <asp:TemplateField HeaderText="Sl">
                                    <ItemTemplate>
                                        <asp:Label ID="lblserialnoid" runat="server" Style="text-align: right"
                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Font-Bold="True" Font-Size="13px" />
                                    <ItemStyle Font-Size="12px" />
                                </asp:TemplateField>


                              <%--  <asp:TemplateField HeaderText="District">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgrcode" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "dist"))%>'
                                            Width="80px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Font-Size="12px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="District BN">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgrcode" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "distBN"))%>'
                                            Width="80px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Font-Size="12px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Upozila">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgrcode" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "upziName"))%>'
                                            Width="80px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Font-Size="12px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Upozila BN">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgrcode" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "upziNameBN"))%>'
                                            Width="80px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Font-Size="12px" />
                                </asp:TemplateField>--%>

                                

                                <asp:TemplateField HeaderText="Post Office">
                                    <ItemTemplate>
                                        <asp:Label ID="lbgrcod3" runat="server" Font-Size="12px"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "postname"))%>'
                                            ></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Font-Bold="True" Font-Size="13px" />
                                    <ItemStyle Font-Size="12px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Post Office BN">
                                    <ItemTemplate>
                                        <asp:Label ID="lbgrcod3" runat="server" Font-Size="12px"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "postnamebn"))%>'
                                           ></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Font-Bold="True" Font-Size="13px" />
                                    <ItemStyle Font-Size="12px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Postal Code">
                                    <ItemTemplate>
                                        <asp:Label ID="lbgrcod" runat="server" Font-Size="12px"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "postcod"))%>'
                                            ></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Font-Bold="True" Font-Size="13px" />
                                    <ItemStyle Font-Size="12px" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Village Name">
                                    <ItemTemplate>
                                        <asp:Label ID="lblvillname" runat="server" Font-Size="12px"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "name"))%>'
                                            ></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Font-Bold="True" Font-Size="13px" />
                                    <ItemStyle Font-Size="12px" />
                                </asp:TemplateField>

                                  <asp:TemplateField HeaderText="Village Name BN">
                                    <ItemTemplate>
                                        <asp:Label ID="lblvillname" runat="server" Font-Size="12px"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "bn_name"))%>'
                                            ></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Font-Bold="True" Font-Size="13px" />
                                    <ItemStyle Font-Size="12px" />
                                </asp:TemplateField>
                            <%--    <asp:CommandField ShowDeleteButton="True" ControlStyle-CssClass="btn btn-xs btn-danger" DeleteText="Delete" />--%>
                            </Columns>

                            <PagerStyle CssClass="gvPagination" />
                            <HeaderStyle CssClass="grvHeader" />
                            <FooterStyle CssClass="grvFooter" />
                            <AlternatingRowStyle BackColor="" />
                        </asp:GridView>
                      </div>   
                    </div>
                         
                    </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

