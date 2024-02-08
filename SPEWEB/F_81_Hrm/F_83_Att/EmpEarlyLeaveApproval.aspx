<%@ Page Title="" Language="C#" MasterPageFile="~/SPEMaster.Master" AutoEventWireup="true" CodeBehind="EmpEarlyLeaveApproval.aspx.cs" Inherits="SPEWEB.F_81_Hrm.F_83_Att.EmpEarlyLeaveApproval" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">



    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="container moduleItemWrpper">
                <div class="contentPart">
                    <div class="row">
                        <fieldset class="scheduler-border fieldset_A">
                            <div class="form-horizontal">
                                <div class="form-group">

                                    <div class="col-md-3 pading5px asitCol3">
                                        <asp:Label ID="Label13" runat="server" CssClass="lblTxt lblName" Text="Emp.  Name"></asp:Label>
                                        <asp:TextBox ID="txtSrcEmpCode" runat="server" CssClass=" inputtextbox" TabIndex="1"></asp:TextBox>

                                        <div class="colMdbtn">
                                            <asp:LinkButton ID="imgbtnEmployee" CssClass="btn btn-primary srearchBtn" runat="server" OnClick="imgbtnEmployee_Click" TabIndex="2"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>
                                        </div>
                                    </div>
                                    <div class="col-md-3 pading5px ">
                                        <asp:DropDownList ID="ddlEmpName" AutoPostBack="True" runat="server" CssClass="form-control inputTxt" TabIndex="3">
                                        </asp:DropDownList>


                                    </div>
                                     <div class="col-md-3 pading5px pull-right">
                                        <div class="msgHandSt">
                                            <asp:Label ID="lmsg11" CssClass="btn btn-danger  disabled" runat="server" Visible="false"></asp:Label>
                                        </div>

                                       
                                    </div>

                                  
                                </div>







                                <div class="form-group">

                                    <div class="col-md-6 pading5px">
                                        <asp:Label ID="Label12" runat="server" CssClass="lblTxt lblName" Text="Company"></asp:Label>
                                        <asp:Label ID="lblCompany" runat="server" CssClass=" inputlblVal" Style="width:388px;"></asp:Label>


                                    </div>



                                    <div class="clearfix"></div>
                                </div>
                                <div class="form-group">

                                    <div class="col-md-6 pading5px">
                                        <asp:Label ID="Label17" runat="server" CssClass="lblTxt lblName" Text="Section"></asp:Label>
                                        <asp:Label ID="lblSection" runat="server" CssClass=" inputlblVal" Style="width:388px;"></asp:Label>


                                    </div>


                                    <div class="clearfix">
                                    </div>

                                </div>
                                <div class="form-group">

                                    <div class="col-md-6 pading5px">
                                        <asp:Label ID="Label15" runat="server" CssClass="lblTxt lblName" Text="Designation"></asp:Label>
                                        <asp:Label ID="lblDesignation" runat="server" CssClass=" inputlblVal" Style="width:388px;"></asp:Label>


                                    </div>
                                   

                                    

                                </div>
                                <div class="form-group">

                                    <div class="col-md-6 pading5px">
                                        <asp:Label ID="Label16" runat="server" CssClass="lblTxt lblName" Text="Month"></asp:Label>

                                        <asp:DropDownList ID="ddlMonth" runat="server" AutoPostBack="True"
                                            CssClass=" ddlPage" TabIndex="3"
                                            OnSelectedIndexChanged="ddlMonth_SelectedIndexChanged" >
                                        </asp:DropDownList>
                                         <asp:LinkButton ID="lnkbtnShow" runat="server" CssClass="btn btn-primary primaryBtn pull-left" onclick="lnkbtnShow_Click" 
                                        >Show</asp:LinkButton>

                                    </div>


                                

                                </div>

                            </div>
                        </fieldset>
                    </div>
                    <div class="row">
                        <asp:GridView ID="gvMonthlyAttn" runat="server" AutoGenerateColumns="False" ShowFooter="True" CssClass=" table-striped table-hover table-bordered grvContentarea"
                            AllowPaging="false" OnPageIndexChanging="gvMonthlyAttn_PageIndexChanging" Width="733px"
                            OnRowDeleting="gvMonthlyAttn_RowDeleting">
                            <RowStyle />
                            <Columns>
                                <asp:TemplateField HeaderText="Sl.No.">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvSlNo" runat="server" Font-Bold="True" Height="16px" Style="text-align: right"
                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Date">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvDate" runat="server" Height="16px" Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "cdate")).ToString("dd-MMM-yyyy") %>'
                                            Width="80px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:LinkButton ID="lFinalUpdate" runat="server" CssClass="btn btn-danger primarygrdBtn" OnClick="lFinalUpdate_Click">Final Update</asp:LinkButton>
                                    </FooterTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Off. Intime">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvoffIntime" runat="server" Height="16px" Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "offintime")).ToString("hh:mm tt") %>'
                                            Width="60px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:LinkButton ID="lFinalTotal" runat="server" Font-Bold="True" Font-Size="12px"
                                            ForeColor="#000" OnClick="lFinalTotal_Click" CssClass="btn btn-primary primarygrdBtn">Total</asp:LinkButton>
                                    </FooterTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Off. Outtime">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvoffouttime" runat="server" Height="16px" Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "offouttime")).ToString("hh:mm tt") %>'
                                            Width="60px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Ac. Intime">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvIntime" runat="server" BackColor="Transparent" BorderStyle="None"
                                            Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "intime")).ToString("hh:mm tt") %>'
                                            Width="60px" Font-Size="11px" 
                                            ></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Ac. Outtime">
                                    <ItemTemplate>
                                        <asp:Label ID="txtgvOuttime" runat="server" BackColor="Transparent" BorderStyle="None"
                                            Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "outtime")).ToString("hh:mm tt") %>'
                                            Width="60px" Font-Size="11px" 
                                            ></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Ln Intime" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvlnintime" runat="server" Height="16px" Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "lnchintime")).ToString("hh:mm tt") %>'
                                            Width="60px" Font-Size="11px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Ln Outtime" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvlnouttime" runat="server" Height="16px" Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "lnchouttime")).ToString("hh:mm tt") %>'
                                            Width="60px" Font-Size="11px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                               
                                <asp:TemplateField HeaderText="Approved">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtgvapproved" runat="server" BackColor="Transparent" BorderStyle="None"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "earleaveapp")) %>'
                                            Width="40px" Font-Size="11px"></asp:TextBox>
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

                
        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>

