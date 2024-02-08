<%@ Page Title="" Language="C#" MasterPageFile="~/SPEMaster.Master" AutoEventWireup="true" CodeBehind="RptGroupTaskold.aspx.cs" Inherits="SPEWEB.F_34_Mgt.RptGroupTaskold" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <style>
        .rbtcss {
            border: 1px;
            border-color: #808080;
            padding: 5px;
            margin: 10px;
        }
    </style>

        <script language="javascript" type="text/javascript">
         $(document).ready(function () {
             //For navigating using left and right arrow of the keyboard
             Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);
         });
         function pageLoaded() {
             $('.chzn-select').chosen({ search_contains: true });

         };
    </script>

    <div class="container moduleItemWrpper">
        <div class="contentPart">
            <div class="row">
                <fieldset class="scheduler-border fieldset_A">
                    <div class="form-horizontal">
                        <asp:Panel ID="Panel1" runat="server">
                            <div class="form-group">
                                <div class="col-md-7 pading5px">
                                    <%--OnSelectedIndexChanged="rdbtnProduct_SelectedIndexChanged" AutoPostBack="true" --%>

                                    <asp:RadioButtonList ID="rbtgtc" runat="server" BackColor="#DBEBD4" CssClass="rbtnList1 chkBoxControl margin5px"
                                        RepeatColumns="6" RepeatDirection="Horizontal" Width="600px"
                                        OnSelectedIndexChanged="rbtgtc_SelectedIndexChanged" AutoPostBack="true">
                                        <asp:ListItem Value="0" Selected="True">Assign</asp:ListItem>
                                        <asp:ListItem Value="1">User</asp:ListItem>
                                        <asp:ListItem Value="2">Project </asp:ListItem>
                                        <asp:ListItem Value="3">Task </asp:ListItem>
                                        <asp:ListItem Value="4">Target Date</asp:ListItem>
                                        <asp:ListItem Value="5"> Actual Complete</asp:ListItem>
                                        <asp:ListItem Value="6">Inactive </asp:ListItem>
                                    </asp:RadioButtonList>
                                </div>

<%--                                 <asp:RadioButtonList ID="rbtlBonSheet" runat="server" BackColor="#DBEBD4" CssClass="rbtnList1 chkBoxControl margin5px"
                                                        RepeatColumns="6" RepeatDirection="Horizontal" Width="531px">
                                                        <asp:ListItem>NDE</asp:ListItem>
                                                        <asp:ListItem>Foster</asp:ListItem>
                                                        <asp:ListItem>Sanmar</asp:ListItem>
                                                        <asp:ListItem>Multiplan</asp:ListItem>
                                                        <asp:ListItem>Rupayan</asp:ListItem>
                                                        <asp:ListItem>Paradise</asp:ListItem>

                                                    </asp:RadioButtonList>--%>

                            </div>
                            <div class="form-group">
                              
                                <div class="col-md-1">
                                        <%--<asp:Label ID="lblgtcwise" runat="server" CssClass="lblTxt lblName">Type</asp:Label>--%>
                                        <h6 id="lblgtcwise" class="test1" runat="server"> Type Wise </h6>
                                    </div>
                               
                                <div class="col-md-3">
                                    <%--<select id="ddlShowAlldata" runat="server" style="width: 300px; height: 25px;"></select>--%>

                                     <asp:DropDownList ID="ddlShowAlldata" runat="server"  CssClass="chzn-select" Style="width: 300px; height: 25px;"></asp:DropDownList>

                                </div>

                                <div class="col-md-1">
                                      <asp:LinkButton ID="lbtOk" runat="server" Text="Ok" OnClick="lbtOk_Click" CssClass="btn btn-primary okBtn"></asp:LinkButton>
                                </div>

                            </div>

                        </asp:Panel>
                    </div>
                </fieldset>

                <div class="table table-responsive">
                    <asp:GridView ID="gvGrouptask" runat="server" AutoGenerateColumns="False" CssClass=" table-striped table-hover table-bordered grvContentarea"
                        ShowFooter="True">
                        <RowStyle />
                        <Columns>
                            <asp:TemplateField HeaderText="Sl.No.">
                                <ItemTemplate>
                                    <asp:Label ID="lblgvSlNo0" runat="server" Font-Bold="True" Height="16px"
                                        Style="text-align: right"
                                        Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="40px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>

                             <asp:TemplateField HeaderText="" Visible="false">
                                <ItemTemplate>
                                        <asp:Label ID="Lbkassign" runat="server" Height="16px" Font-Size="11px"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "asinuser")) %>'
                                        Width="80px"></asp:Label>

                                    <asp:Label ID="txtgvall" runat="server" Height="16px" Font-Size="11px"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "userid")) %>'
                                        Width="80px"></asp:Label>
                                 
                                      <asp:Label ID="Label1" runat="server" Height="16px" Font-Size="11px"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actcode")) %>'
                                        Width="80px"></asp:Label>

                                      <asp:Label ID="Label2" runat="server" Height="16px" Font-Size="11px"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "taskcod")) %>'
                                        Width="80px"></asp:Label>

                                      <asp:Label ID="Label3" runat="server" Height="16px" Font-Size="11px"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "cstatus")) %>'
                                        Width="80px"></asp:Label>

                                      <asp:Label ID="Label4" runat="server" Height="16px" Font-Size="11px"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "probledat")) %>'
                                        Width="80px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Task Assign">
                                <ItemTemplate>
                                    <asp:Label ID="txtgvUsername" runat="server" Height="16px" Font-Size="11px"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "asinnamne")) %>'
                                        Width="80px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="User Name">
                                <ItemTemplate>
                                    <asp:Label ID="txtgvUsername" runat="server" Height="16px" Font-Size="11px"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "postedname")) %>'
                                        Width="80px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Project Name">
                                <ItemTemplate>
                                    <asp:Label ID="txtgvProject" runat="server" Height="16px" Font-Size="11px"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc")) %>'
                                        Width="150px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Task">
                                <ItemTemplate>
                                    <asp:Label ID="txtgvProject" runat="server" Height="16px" Font-Size="11px"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "taskname")) %>'
                                        Width="80px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Message">
                                <ItemTemplate>
                                    <asp:Label ID="txtgvMessage" runat="server" Height="16px" Font-Size="11px"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "fmessage")) %>'
                                        Width="250px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Target Complete">
                                <ItemTemplate>
                                    <asp:Label ID="txtgvTargetcom" runat="server"
                                        BackColor="Transparent" BorderStyle="None" Font-Size="11px"
                                        Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "probledat")).ToString("dd-MMM-yyyy") %>'
                                        Width="80px"></asp:Label>
                                </ItemTemplate>
                                <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Status">
                                <ItemTemplate>
                                    <asp:Label ID="txtgvStatus" runat="server" Height="16px" Font-Size="11px"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "cstatus")) %>'
                                        Width="250px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Actual Complete">
                                <ItemTemplate>
                                    <asp:Label ID="txtgvActualcomp" runat="server" Height="16px" Font-Size="11px"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "clstatus")) %>'
                                        Width="100px"></asp:Label>
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
        </div>
    </div>



</asp:Content>

