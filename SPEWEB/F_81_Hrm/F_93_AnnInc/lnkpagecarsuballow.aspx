<%@ Page Title="" Language="C#" MasterPageFile="~/SPEMaster.Master" AutoEventWireup="true" CodeBehind="lnkpagecarsuballow.aspx.cs" Inherits="SPEWEB.F_81_Hrm.F_93_AnnInc.lnkpagecarsuballow" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
<ContentTemplate>
    
    

    <div class="container moduleItemWrpper">
        <div class="contentPart">
            <div class="row">
a
                <fieldset class="scheduler-border fieldset_A">
                    <div class="form-horizontal">
                        <div class="form-group">
                            <div class="col-md-3 col-sm-3 col-lg-3 pading5px">
                                <asp:Label ID="Label3" runat="server" CssClass="lblTxt lblName">Employee Type</asp:Label>
                                <asp:DropDownList ID="ddlWstation" runat="server" Width="200" OnSelectedIndexChanged="ddlWstation_SelectedIndexChanged" CssClass="chzn-select pull-left" AutoPostBack="true" TabIndex="2"></asp:DropDownList>
                            </div>
                            <div class="col-md-3 col-sm-3 col-lg-3 pading5px">
                                <asp:Label ID="Label5" runat="server" CssClass="smLbl_to">Division</asp:Label>
                                <asp:DropDownList ID="ddlDivision" runat="server" Width="225" OnSelectedIndexChanged="ddlDivision_SelectedIndexChanged" CssClass="chzn-select pull-left" AutoPostBack="true" TabIndex="2"></asp:DropDownList>
                            </div>
                            <div class="col-md-3 col-sm-3 col-lg-3 pading5px">
                                <asp:Label ID="Label7" runat="server" CssClass="smLbl_to">Department</asp:Label>
                                <asp:DropDownList ID="ddlDept" runat="server" Width="200" OnSelectedIndexChanged="ddlDept_SelectedIndexChanged" CssClass="chzn-select pull-left" AutoPostBack="true" TabIndex="2"></asp:DropDownList>
                            </div>
                            <div class="col-md-3 col-sm-3 col-lg-3 pading5px">
                                <asp:Label ID="Label8" runat="server" CssClass="smLbl_to">Section</asp:Label>

                                <asp:DropDownList ID="ddlSection" runat="server" Width="200" CssClass="chzn-select pull-left" AutoPostBack="true" TabIndex="2"></asp:DropDownList>

                            </div>

                            <div class="col-md-3 col-sm-3 col-lg-3 pading5px">
                                <asp:LinkButton ID="lnkbtnShow" runat="server" CssClass="btn btn-primary okBtn" OnClick="lnkbtnShow_OnClick"
                                                TabIndex="3">Ok</asp:LinkButton>

                            </div>

                        </div>
                    </div>
                </fieldset>
            </div>
            <div class="row">
                <asp:GridView ID="gvcarsubsetup" runat="server" AllowPaging="True" PageSize="50"
                    AutoGenerateColumns="False" Height="200px" CssClass="table-striped table-hover table-bordered grvContentarea"
                    ShowFooter="True"
                    Width="831px">
                    <PagerSettings Position="Top" />
                    <RowStyle />
                    <Columns>
                        <asp:TemplateField HeaderText="Sl">
                            <ItemTemplate>
                                <asp:Label ID="lblgvSlNo" runat="server" Font-Bold="True" Height="16px"
                                    Style="text-align: right"
                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Section Name">
                            <ItemTemplate>
                                <asp:Label ID="lgempid" runat="server" Font-Bold="True"
                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "empid")) %>'
                                    Width="120px"></asp:Label>

                                <asp:Label ID="lblsecname" runat="server" Font-Bold="True"
                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "secname")) %>'
                                    Width="120px"></asp:Label>

                            </ItemTemplate>
                            <FooterTemplate>
                            </FooterTemplate>
                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Id Card">
                            <ItemTemplate>
                                <asp:Label ID="lgvSection" runat="server"
                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "idcard")) %>'
                                    Width="150px"></asp:Label>
                            </ItemTemplate>
                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Name &amp; Designation">

                            <ItemTemplate>
                                <asp:Label ID="lgvndesig" runat="server"
                                    Text='<%#"<b>"+Convert.ToString(DataBinder.Eval(Container.DataItem, "empname"))+"</b>"+"<br>"+Convert.ToString(DataBinder.Eval(Container.DataItem, "desig")) %>'
                                    Width="160px"></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="left" />
                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Gross Amount">
                            <ItemTemplate>
                                 
                                <asp:Label ID="lblincgross" runat="server" BackColor="Transparent"
                                             BorderColor="#660033" BorderStyle="Solid" BorderWidth="0px" Font-Size="11px"
                                             Style="text-align: right"
                                             Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "incgross")).ToString("#,##0;(#,##0); ") %>'
                                             Width="55px"></asp:Label>
                                
                                
                            </ItemTemplate>

                            <FooterStyle HorizontalAlign="Right" />
                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Car Allow">
                            <ItemTemplate>
                                 
                                <asp:TextBox ID="txtCaramt" runat="server" BackColor="Transparent" AutoPostBack="True"
                                             BorderColor="#660033" BorderStyle="Solid" BorderWidth="0px" Font-Size="11px"
                                             Style="text-align: right"
                                             Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "caramt")).ToString("#,##0;(#,##0); ") %>'
                                             Width="55px" OnTextChanged="txtCaramt_OnTextChanged"></asp:TextBox>
                                
                                
                            </ItemTemplate>

                            <FooterStyle HorizontalAlign="Right" />
                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                        </asp:TemplateField>
                        
                        
                        
                        <asp:TemplateField HeaderText="Sub Allow">
                            <ItemTemplate>
                                <asp:TextBox ID="txtsubamt" runat="server" BackColor="Transparent"
                                    BorderColor="#660033" BorderStyle="Solid" BorderWidth="0px" Font-Size="11px"
                                    Style="text-align: right"
                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "subamt")).ToString("#,##0;(#,##0); ") %>'
                                    Width="55px"></asp:TextBox>
                            </ItemTemplate>
                            <FooterStyle HorizontalAlign="Right" />
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
    </div>
    </ContentTemplate>
</asp:UpdatePanel>
</asp:Content>


