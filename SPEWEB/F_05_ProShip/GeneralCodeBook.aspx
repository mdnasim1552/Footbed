<%@ Page Title="" Language="C#" MasterPageFile="~/SPE.Master" AutoEventWireup="true" CodeBehind="GeneralCodeBook.aspx.cs" Inherits="SPEWEB.F_05_ProShip.GeneralCodeBook" %>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="card card-fluid">
                <div class="card-body">
                    <div class="row">
                        <div class="col-md-3">
                            <div class="form-group">

                                <asp:Label ID="LblBookName1" runat="server" CssClass="control-label" Text="Select Code Book:"></asp:Label>


                                <asp:DropDownList ID="ddlOthersBook" runat="server" CssClass="form-control form-control-sm">
                                </asp:DropDownList>

                                <asp:Label ID="lbalterofddl" runat="server" Visible="False" CssClass="form-control form-control-sm"></asp:Label>
                            </div>
                        </div>
                        <div class="col-md-2 ">
                            <div class="form-group">
                                <asp:Label ID="Label1" runat="server" CssClass="control-label" Text="Group:"></asp:Label>

                                <asp:DropDownList ID="ddlOthersBookSegment" CssClass="form-control form-control-sm" runat="server">
                                    <asp:ListItem Value="2">Sub Code-1</asp:ListItem>
                                    <asp:ListItem Selected="True" Value="5">Details Code</asp:ListItem>
                                </asp:DropDownList>
                                <asp:Label ID="lbalterofddl0" runat="server" Visible="False" CssClass="form-control form-control-sm"></asp:Label>
                            </div>
                        </div>
                        <div class="col-md-1">
                            <div class="form-group">
                                <asp:LinkButton ID="lnkok" runat="server" Text="Ok" Style="margin-top: 20px;" OnClick="lnkok_Click" CssClass="btn btn-primary btn-sm"></asp:LinkButton>
                            </div>
                        </div>



                    </div>



                </div>

            </div>

            <div class="card card-fluid">
                <div class="card-body" style="min-height: 350px;">
                    <div class="row">
                        <asp:GridView ID="grvacc" runat="server" AllowPaging="True" CssClass="table-striped table-hover table-bordered grvContentarea"
                            AutoGenerateColumns="False" Font-Size="12px" OnRowDataBound="grvacc_RowDataBound"
                            OnRowCancelingEdit="grvacc_RowCancelingEdit" OnRowEditing="grvacc_RowEditing"
                            OnRowUpdating="grvacc_RowUpdating" PageSize="50" Width="284px"
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

                                <asp:CommandField DeleteText="" HeaderText="Edit" InsertText="" NewText=""
                                    SelectText="" ShowEditButton="True">
                                    <HeaderStyle Font-Bold="True" Font-Size="13px" />
                                    <ItemStyle Font-Bold="True" Font-Size="12px" ForeColor="#0000C0" />
                                </asp:CommandField>

                                <asp:TemplateField HeaderText=" ">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgrcode" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gcod2"))+"-" %>'
                                            Width="30px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Font-Size="12px" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Code">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtgrcode" runat="server" Font-Size="12px" Height="16px"
                                            MaxLength="6"
                                            Style="border-top-style: none; border-right-style: none; border-left-style: none; border-bottom-style: none; font-size: 12px; border-left-color: midnightblue; border-bottom-color: midnightblue; border-top-color: midnightblue; border-right-color: midnightblue;"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gcod3")) %>'
                                            Width="50px"></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lbgrcod3" runat="server" Font-Size="12px"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gcod3")) %>'
                                            Width="50px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Font-Bold="True" Font-Size="13px" />
                                    <ItemStyle Font-Size="12px" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Description of Code">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtgvDesc" runat="server" Font-Size="12px" MaxLength="100"
                                            Style="border-top-style: none; border-right-style: none; border-left-style: none; border-bottom-style: none; font-size: 12px; border-left-color: midnightblue; border-bottom-color: midnightblue; border-top-color: midnightblue; border-right-color: midnightblue;"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gdesc")) %>'
                                            Width="250px"></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lbldesc" runat="server" Font-Size="12px"
                                            Style="font-size: 12px"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gdesc")) %>'
                                            Width="250px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Font-Bold="True" Font-Size="13px" HorizontalAlign="Left" />
                                    <ItemStyle Font-Size="12px" HorizontalAlign="Left" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Hidden Column" Visible="False">
                                    <ItemTemplate>
                                        <asp:Label ID="lbgrcod1" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gcod")) %>'
                                            Visible="False"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Type">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtgvttpe" runat="server" BackColor="White" BorderStyle="None"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gval")) %>'
                                            Width="50px"></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lgvtype" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gval")) %>'
                                            Width="50px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Cutting">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtgvVal1" runat="server" BackColor="White" BorderStyle="None" CssClass="text-right"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "val1")) %>'
                                            Width="50px"></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lgvVal1" runat="server" CssClass="text-right"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "val1")) %>'
                                            Width="50px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Sewing">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtgvVal2" runat="server" BackColor="White" BorderStyle="None" CssClass="text-right"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "val2")) %>'
                                            Width="50px"></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lgvVal2" runat="server" CssClass="text-right"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "val2")) %>'
                                            Width="50px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Lasting">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtgvVal3" runat="server" BackColor="White" BorderStyle="None" CssClass="text-right"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "val3")) %>'
                                            Width="50px"></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lgvVal3" runat="server" CssClass="text-right"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "val3")) %>'
                                            Width="50px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Production Process">
                                     <EditItemTemplate>
                                        <asp:DropDownList ID="ddlPProces" runat="server" CssClass="chzn-select form-control form-control-sm"></asp:DropDownList>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblLinkdesc" runat="server" CssClass="text-left"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "LINKDESC")) %>'
                                            Width="150px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Font-Size="12px"  />
                                    
                                </asp:TemplateField>

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
