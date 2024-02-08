<%@ Page Title="" Language="C#" MasterPageFile="~/SPE.Master" AutoEventWireup="true" CodeBehind="LinkSpecificCodeBook.aspx.cs" Inherits="SPEWEB.F_21_GAcc.LinkSpecificCodeBook" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript">
        function openModal() {
            $('#myModal').modal('toggle');
        }
        function CLoseModal() {
            $('#myModal').modal('hide');
            $('#SpecificationModal').modal('hide');
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

            <div class="card card-fluid">
                <div class="card-body">
                    <div class="row">
                        <div class="col-md-3">
                            <div class="form-group">
                                <asp:Label ID="Label11" runat="server" CssClass="label">Material Name</asp:Label>
                                <asp:Label ID="lblMaterialName" runat="server" CssClass="form-control form-control-sm"></asp:Label>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div style="margin-top: 25px">
                                <asp:Label ID="lblmsg" CssClass="text-info" runat="server" Visible="false"></asp:Label>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <div class="card card-fluid">
                <div class="card-body" style="min-height: 300px">
                    <asp:GridView ID="grvacc" runat="server" CssClass="table-striped table-hover table-bordered grvContentarea" AutoGenerateColumns="False" OnRowCancelingEdit="grvacc_RowCancelingEdit"
                        OnRowEditing="grvacc_RowEditing" OnRowUpdating="grvacc_RowUpdating" PageSize="15" OnRowDataBound="grvacc_RowDataBound" Width="500px">
                        <PagerSettings NextPageText="Next" PreviousPageText="Previous" />

                        <Columns>

                            <%--SL--%>
                            <asp:TemplateField HeaderText="SL.">
                                <ItemTemplate>
                                    <asp:Label ID="serialnoid" runat="server" CssClass="text-center" Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>'
                                        Width="30px"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <%--Edit--%>
                            <asp:CommandField DeleteText="" HeaderText="Edit" InsertText="" NewText="" SelectText="" ShowEditButton="True"></asp:CommandField>

                            <asp:TemplateField HeaderText=" " Visible="false">
                                <EditItemTemplate>
                                    <asp:Label ID="lbgrcode" runat="server" Width="40px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "spcfcod2"))+"-" %>'></asp:Label>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblspccod2" runat="server" Width="40px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "spcfcod2"))+"-" %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <%--Specf. Code--%>
                            <asp:TemplateField HeaderText="Spcf. Code">
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtgrcode" runat="server" MaxLength="6"
                                        Font-Size="12px" Height="16px" Width="70px"
                                        Style="border-top-style: none; border-right-style: none; border-left-style: none; border-bottom-style: none; font-size: 12px; border-left-color: midnightblue; border-bottom-color: midnightblue; border-top-color: midnightblue; border-right-color: midnightblue;"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "spcfcod4")) %>'></asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lbgrcod" runat="server" Width="70px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "spcfcod4")) %>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Center" />
                                <ItemStyle Font-Size="12px" HorizontalAlign="Center" />
                            </asp:TemplateField>

                            <%--Code--%>
                            <asp:TemplateField HeaderText="Code">
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtgvimpa" runat="server"
                                        MaxLength="100"
                                        Font-Size="12px" Height="16px" Width="170px"
                                        Style="border-top-style: none; border-right-style: none; border-left-style: none; border-bottom-style: none; font-size: 12px; border-left-color: midnightblue; border-bottom-color: midnightblue; border-top-color: midnightblue; border-right-color: midnightblue;"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sirtdes")) %>'></asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblgrimpa" runat="server" Width="170px" Font-Size="12px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sirtdes")) %>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle Font-Bold="True" Font-Size="13px" />
                                <ItemStyle Font-Size="12px" />
                            </asp:TemplateField>

                            <%--Specification--%>
                            <asp:TemplateField HeaderText="Thickness">
                                <HeaderTemplate>
                                    <asp:TextBox ID="txtSrchThickns" BackColor="Transparent" BorderStyle="None" runat="server" Width="220px" placeholder="Thickness" onkeyup="Search_Gridview(this, 5, 'grvacc')"></asp:TextBox><br />
                                </HeaderTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtgvDesc" runat="server" MaxLength="4000"
                                        Font-Size="12px" Height="16px" Width="220px"
                                        Style="border-top-style: none; border-right-style: none; border-left-style: none; border-bottom-style: none; font-size: 12px; border-left-color: midnightblue; border-bottom-color: midnightblue; border-top-color: midnightblue; border-right-color: midnightblue;"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "SpcfDesc")) %>'></asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:HyperLink ID="hlnkgvdesc" runat="server" Style="border-style: none;"
                                        Font-Underline="false" Target="_blank" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "spcfdesc"))  %>'
                                        Width="220px">        
                                    </asp:HyperLink>
                                </ItemTemplate>
                                <HeaderStyle Font-Bold="True" Font-Size="12px" />
                                <ItemStyle Font-Size="12px" />
                            </asp:TemplateField>

                            <%--Lxw--%>
                            <asp:TemplateField HeaderText="Lxw">
                                <HeaderTemplate>
                                    <asp:TextBox ID="txtSrchLxw" BackColor="Transparent" BorderStyle="None" runat="server" Width="150px" placeholder="Lxw" onkeyup="Search_Gridview(this, 6, 'grvacc')"></asp:TextBox><br />
                                </HeaderTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtgvDesc1" runat="server" MaxLength="250"
                                        Font-Size="12px" Height="16px" Width="150px"
                                        Style="border-top-style: none; border-right-style: none; border-left-style: none; border-bottom-style: none; font-size: 12px; border-left-color: midnightblue; border-bottom-color: midnightblue; border-top-color: midnightblue; border-right-color: midnightblue;"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "desc1")) %>'></asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblgvdesc1" runat="server" Style="border-style: none;"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "desc1"))  %>'
                                        Width="150px"> 
                                    </asp:Label>
                                </ItemTemplate>
                                <HeaderStyle Font-Bold="True" Font-Size="13px" HorizontalAlign="Center" />
                                <ItemStyle Font-Size="12px" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Color">
                                <HeaderTemplate>
                                    <asp:TextBox ID="txtSrchClr" BackColor="Transparent" BorderStyle="None" runat="server" Width="110px" placeholder="Color" onkeyup="Search_Gridview(this, 7, 'grvacc')"></asp:TextBox><br />
                                </HeaderTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtgvDesc2" runat="server" MaxLength="250"
                                        Font-Size="12px" Height="16px" Width="110px"
                                        Style="border-top-style: none; border-right-style: none; border-left-style: none; border-bottom-style: none; font-size: 12px; border-left-color: midnightblue; border-bottom-color: midnightblue; border-top-color: midnightblue; border-right-color: midnightblue;"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "desc2")) %>'></asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblgvdesc2" runat="server" Style="border-style: none;"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "desc2"))  %>'
                                        Width="110px">   
                                    </asp:Label>
                                </ItemTemplate>
                                <HeaderStyle Font-Bold="True" Font-Size="13px" HorizontalAlign="Center" />
                                <ItemStyle Font-Size="12px" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Brand">
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtgvDesc3" runat="server" MaxLength="250"
                                        Font-Size="12px" Height="16px" Width="150px"
                                        Style="border-top-style: none; border-right-style: none; border-left-style: none; border-bottom-style: none; font-size: 12px; border-left-color: midnightblue; border-bottom-color: midnightblue; border-top-color: midnightblue; border-right-color: midnightblue;"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "desc3")) %>'></asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblgvdesc3" runat="server" Style="border-style: none;"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "desc3"))  %>'
                                        Width="150px">  
                                    </asp:Label>
                                </ItemTemplate>
                                <HeaderStyle Font-Bold="True" Font-Size="13px" HorizontalAlign="Center" />
                                <ItemStyle Font-Size="12px" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Other">
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtgvDesc4" runat="server" MaxLength="250"
                                        Font-Size="12px" Height="16px" Width="110px"
                                        Style="border-top-style: none; border-right-style: none; border-left-style: none; border-bottom-style: none; font-size: 12px; border-left-color: midnightblue; border-bottom-color: midnightblue; border-top-color: midnightblue; border-right-color: midnightblue;"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "desc4")) %>'></asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblgvdesc4" runat="server" Style="border-style: none;"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "desc4"))  %>'
                                        Width="110px">
                                    </asp:Label>
                                </ItemTemplate>
                                <HeaderStyle Font-Bold="True" Font-Size="13px" />
                                <ItemStyle Font-Size="12px" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Std Rate">
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtgvsirval" runat="server" Font-Size="10px" MaxLength="100"
                                        Style="border-style: none; text-align: right !important"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem,  "stdrate")).ToString("#,##0.0000;(#,##0.0000); ") %>'
                                        Width="70px"></asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblsirval" runat="server" Font-Size="10px"
                                        Style="font-size: 10px; text-align: right;"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem,  "stdrate")).ToString("#,##0.0000;(#,##0.0000); ") %>'
                                        Width="70px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="left" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Image" Visible="true">
                                <ItemTemplate>
                                    <asp:Image ID="lblImageUrl" Width="60" Height="40" runat="server" class="img-responsive"
                                        ImageUrl='<%# (Eval("photo").ToString()=="") ? "~/images/no_img_preview.png" : Eval("photo") %>'></asp:Image>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Upload" Visible="true">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lbtnupload" CssClass="text-primary" OnClick="lbtnupload_Click" runat="server"><span class="fa  fa-upload"></span></asp:LinkButton>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>

                        </Columns>

                        <PagerStyle CssClass="gvPagination" />
                        <HeaderStyle CssClass="grvHeader" />
                        <FooterStyle CssClass="grvFooter" />
                    </asp:GridView>
                </div>
            </div>

            <%--Image Upload Modal--%>

            <div class="modal fade" id="myModal" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
                <div class="modal-dialog modal-dialog-scrollable" role="document">
                    <div class="modal-content">

                        <div class="modal-header">
                            <h6 id="myModalLabel" class="modal-title"><span class="fa fa-image"></span>Upload Image</h6>
                        </div>

                        <div class="modal-body px-0">
                            <div class="card-body">
                                <asp:Label ID="LblSirSpcfcod" runat="server"></asp:Label>
                                <div id="dropzone" class="fileinput-dropzone">
                                    <span>Drop your file or click to upload.</span>
                                    <asp:FileUpload ID="fileuploaddropzone" runat="server" onchange="submitform();" />
                                </div>

                                <div id="progress" class="progress progress-xs rounded-0 fade">
                                    <div class="progress-bar progress-bar-striped progress-bar-animated bg-success" role="progressbar" aria-valuemin="0" aria-valuemax="100"></div>
                                </div>
                            </div>
                        </div>

                        <div class="modal-footer">
                            <%--<asp:LinkButton ID="lblbtnSave" runat="server" OnClick="lblbtnSave_Click" CssClass="btn btn-sm btn-success" OnClientClick="CLoseModal();">Save</asp:LinkButton>--%>
                            <button type="button" class="btn btn-sm btn-secondary" data-dismiss="modal">Close</button>
                        </div>

                    </div>
                </div>
            </div>


        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
