<%@ Page Title="" Language="C#" MasterPageFile="~/SPE.Master" AutoEventWireup="true" CodeBehind="AccCurCodeBook.aspx.cs" Inherits="SPEWEB.F_34_Mgt.AccCurCodeBook" %>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="card card-fluid">
                <div class="card-body">
                    <div class="row">
                        <asp:GridView ID="grvacc" runat="server" AutoGenerateColumns="False"
                            OnRowCancelingEdit="grvacc_RowCancelingEdit"
                            OnRowEditing="grvacc_RowEditing" OnRowUpdating="grvacc_RowUpdating" PageSize="15" CssClass=" table-striped table-hover table-bordered grvContentarea">
                            <PagerSettings NextPageText="Next" PreviousPageText="Previous" />
                            <FooterStyle />
                            <Columns>


                                <asp:TemplateField HeaderText="Sl">
                                    <ItemTemplate>
                                        <asp:Label ID="serialnoid" runat="server" Style="text-align: right" Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>'
                                            Width="30px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle  Font-Size="16px" />
                                </asp:TemplateField>
                                <asp:CommandField ShowEditButton="True" CancelText="<span class='fa fa-times'></span>" EditText="<span class='fa fa-pen'></span>" UpdateText="<span class='fa fa-save'></span>" />

                                <%--  <asp:CommandField DeleteText="" HeaderText="Edit" InsertText="" NewText="" SelectText=""
                                    ShowEditButton="True">
                                    <HeaderStyle Font-Bold="True" Font-Size="16px" />
                                    <ItemStyle Font-Bold="True" Font-Size="12px" ForeColor="#0000C0" />
                                </asp:CommandField>--%>

                                <asp:TemplateField HeaderText="Code">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtgrcode" runat="server" Height="16px" MaxLength="6" Style="border-top-style: none; border-right-style: none; border-left-style: none; border-bottom-style: none; font-size: 12px; border-left-color: midnightblue; border-bottom-color: midnightblue; border-top-color: midnightblue; border-right-color: midnightblue;"
                                            Width="60px"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "code")) %>'></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lbgrcod" runat="server" Width="60px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "code")) %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Font-Bold="True" Font-Size="16px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Currency">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtgvDesc" runat="server" MaxLength="100" Style="border-top-style: none; border-right-style: none; border-left-style: none; border-bottom-style: none; font-size: 12px; border-left-color: midnightblue; border-bottom-color: midnightblue; border-top-color: midnightblue; border-right-color: midnightblue;"
                                            Width="50px"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "codedesc")) %>'></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvdesc" runat="server" BorderColor="#99CCFF" BorderStyle="none"
                                            Font-Size="11px" Style="text-align: left; background-color: Transparent; color: Black;"
                                            Font-Underline="false" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "codedesc"))  %>'
                                            Width="120px">                                             
                                            
                                        </asp:Label>



                                    </ItemTemplate>
                                    <HeaderStyle Font-Bold="True" Font-Size="16px" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Description">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtgvCurDesc" runat="server" MaxLength="100" Style="border-top-style: none; border-right-style: none; border-left-style: none; border-bottom-style: none; font-size: 12px; border-left-color: midnightblue; border-bottom-color: midnightblue; border-top-color: midnightblue; border-right-color: midnightblue;"
                                            Width="250px"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "curdesc")) %>'></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvCurdesc" runat="server" BorderColor="#99CCFF" BorderStyle="none"
                                            Font-Size="11px" Style="text-align: left; background-color: Transparent; color: Black;"
                                            Font-Underline="false" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "curdesc"))  %>'
                                            Width="120px">                                             
                                            
                                        </asp:Label>



                                    </ItemTemplate>
                                    <HeaderStyle Font-Bold="True" Font-Size="16px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Symbol">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtgvSym" runat="server" MaxLength="100" Style="border-top-style: none; border-right-style: none; border-left-style: none; border-bottom-style: none; font-size: 12px; border-left-color: midnightblue; border-bottom-color: midnightblue; border-top-color: midnightblue; border-right-color: midnightblue;"
                                            Width="50px"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "cursymbol")) %>'></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvSym" runat="server" BorderColor="#99CCFF" BorderStyle="none"
                                            Font-Size="11px" Style="text-align: left; background-color: Transparent; color: Black;"
                                            Font-Underline="false" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "cursymbol"))  %>'
                                            Width="120px">                                             
                                            
                                        </asp:Label>



                                    </ItemTemplate>
                                    <HeaderStyle Font-Bold="True" Font-Size="16px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Word">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtgvWord" runat="server" MaxLength="100" Style="border-top-style: none; border-right-style: none; border-left-style: none; border-bottom-style: none; font-size: 12px; border-left-color: midnightblue; border-bottom-color: midnightblue; border-top-color: midnightblue; border-right-color: midnightblue;"
                                            Width="150px"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "curword")) %>'></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvWord" runat="server" BorderColor="#99CCFF" BorderStyle="none"
                                            Font-Size="11px" Style="text-align: left; background-color: Transparent; color: Black;"
                                            Font-Underline="false" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "curword"))  %>'
                                            Width="120px">                                             
                                            
                                        </asp:Label>



                                    </ItemTemplate>
                                    <HeaderStyle Font-Bold="True" Font-Size="16px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Non-active">
                                    <EditItemTemplate>

                                        <asp:CheckBox ID="CheckBox1" runat="server" />
                                        <asp:TextBox ID="txtgvnoneActive" runat="server" MaxLength="100" Style="border-top-style: none; border-right-style: none; border-left-style: none; border-bottom-style: none; font-size: 12px; border-left-color: midnightblue; border-bottom-color: midnightblue; border-top-color: midnightblue; border-right-color: midnightblue;"
                                            Width="50px"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "curstatus")) %>'></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvNonactive" runat="server" BorderColor="#99CCFF" BorderStyle="none"
                                            Font-Size="11px" Style="text-align: left; background-color: Transparent; color: Black;"
                                            Font-Underline="false" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "curstatus"))  %>'
                                            Width="120px">                                             
                                            
                                        </asp:Label>



                                    </ItemTemplate>
                                    <HeaderStyle Font-Bold="True" Font-Size="16px" />
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
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
