<%@ Page Title="" Language="C#" MasterPageFile="~/SPE.Master" AutoEventWireup="true" CodeBehind="DocCodeBook.aspx.cs" Inherits="SPEWEB.F_33_Doc.DocCodeBook" %>

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
                                    <asp:Label ID="Label1" runat="server" CssClass="control-label" Text="Code Label:"></asp:Label>

                                        <asp:DropDownList ID="ddlOthersBookSegment" CssClass="form-control form-control-sm" runat="server">
                                            <asp:ListItem Value="2">Sub Code-1</asp:ListItem>
                                            <asp:ListItem Selected="True" Value="7">Details Code</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:Label ID="lbalterofddl0" runat="server" Visible="False" CssClass="form-control form-control-sm"></asp:Label>
                           </div>
                              </div>
                                    <div class="col-md-1">
                                        <asp:LinkButton ID="lnkok"  runat="server" Text="Ok" OnClick="lnkok_Click" CssClass="btn btn-sm btn-primary " style="margin-top:20px;"></asp:LinkButton>
                                    </div>
                               
                                </div>

                    </div>
                    </div>
                           </div>
             <div class="card card-fluid" style="min-height: 300px;">
                <div class="card-body row">
                    <asp:GridView ID="grvacc" runat="server" AllowPaging="True" CssClass="table-striped table-hover table-bordered grvContentarea"
                        AutoGenerateColumns="False"  Font-Size="12px"
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
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gcod4")) %>'
                                        Width="50px"></asp:TextBox>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lbgrcod3" runat="server" Font-Size="12px"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gcod4")) %>'
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
                              <asp:TemplateField HeaderText="SL Number">
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtgvSlnum" runat="server" Font-Size="12px" MaxLength="100"
                                        Style="border-top-style: none; border-right-style: none; border-left-style: none; border-bottom-style: none; font-size: 12px; border-left-color: midnightblue; border-bottom-color: midnightblue; border-top-color: midnightblue; border-right-color: midnightblue;"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "slnum")) %>'
                                        Width="30px"></asp:TextBox>
                                </EditItemTemplate>

                                <ItemTemplate>
                                    <asp:Label ID="lblSLnum" runat="server" Font-Size="12px"
                                        Style="font-size: 12px"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "slnum")) %>'
                                        Width="30px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle Font-Bold="True" Font-Size="13px" HorizontalAlign="Left" />
                                <ItemStyle Font-Size="12px" HorizontalAlign="Left" />
                            </asp:TemplateField>
                              <asp:TemplateField HeaderText="Select User">
                                <EditItemTemplate>
                                    <asp:DropDownList ID="DdlUser" runat="server"></asp:DropDownList>
                                </EditItemTemplate>

                                <ItemTemplate>
                                    <asp:Label ID="lblUserName" runat="server" Font-Size="12px"
                                        Style="font-size: 12px"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "usrname")) %>'
                                        Width="150px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle Font-Bold="True" Font-Size="13px" HorizontalAlign="Left" />
                                <ItemStyle Font-Size="12px" HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Time Limit">
                                <EditItemTemplate>
                                    <asp:DropDownList ID="ddlIncTime" runat="server">
                                        <asp:ListItem Value="08:00">08:00</asp:ListItem>
                                        <asp:ListItem Value="08:30">08:30</asp:ListItem>
                                        <asp:ListItem Value="09:00">09:00</asp:ListItem>
                                        <asp:ListItem Value="09:30">09:30</asp:ListItem>
                                        <asp:ListItem Value="10:00">10:00</asp:ListItem>
                                        <asp:ListItem Value="10:30">10:30</asp:ListItem>
                                        <asp:ListItem Value="11:00">11:00</asp:ListItem>
                                        <asp:ListItem Value="11:30">11:30</asp:ListItem>                                      
                                        <asp:ListItem Value="12:00">12:00</asp:ListItem>
                                        <asp:ListItem Value="12:30">12:30</asp:ListItem>
                                        <asp:ListItem Value="01:00">01:00</asp:ListItem>
                                        <asp:ListItem Value="01:30">01:30</asp:ListItem>
                                        <asp:ListItem Value="02:00">02:00</asp:ListItem>
                                        <asp:ListItem Value="02:30">02:30</asp:ListItem>


                                    </asp:DropDownList>
                                      <asp:DropDownList ID="TimeDaylight" runat="server">
                                        <asp:ListItem Value="AM">AM</asp:ListItem>
                                        <asp:ListItem Value="PM">PM</asp:ListItem>
                                    </asp:DropDownList>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblIncomeTime" runat="server" Font-Size="12px"
                                        Style="font-size: 12px"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "incmtime")) %>'
                                        Width="150px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle Font-Bold="True" Font-Size="13px" HorizontalAlign="Left" />
                                <ItemStyle Font-Size="12px" HorizontalAlign="Left" />
                            </asp:TemplateField>
                             <asp:TemplateField HeaderText="Status">
                                <EditItemTemplate>
                                    <asp:DropDownList ID="ddlStatus" runat="server">
                                        <asp:ListItem Value="1">True</asp:ListItem>
                                        <asp:ListItem Value="0">False</asp:ListItem>
                                    </asp:DropDownList>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblStatus" runat="server" Font-Size="12px"
                                        Style="font-size: 12px"
                                        Text='<%# (Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "timeflag"))==true)?"True":"False" %>'
                                        Width="40px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle Font-Bold="True" Font-Size="13px" HorizontalAlign="Left" />
                                <ItemStyle Font-Size="12px" HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Color">
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtgvColor" runat="server" type="color" Font-Size="12px" MaxLength="100"
                                        Style="border-top-style: none; border-right-style: none; border-left-style: none; border-bottom-style: none; font-size: 12px; border-left-color: midnightblue; border-bottom-color: midnightblue; border-top-color: midnightblue; border-right-color: midnightblue;"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "color")) %>'
                                        Width="50px"></asp:TextBox>
                                </EditItemTemplate>

                                <ItemTemplate>
                                    <asp:Label ID="lblColor" runat="server" Font-Size="12px"
                                        Style="font-size: 12px"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "color")) %>'
                                        Width="50px"></asp:Label>
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
                           
                          



                        </Columns>

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
