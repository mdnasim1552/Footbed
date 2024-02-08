<%@ Page Title="" Language="C#" MasterPageFile="~/SPEMaster.Master" AutoEventWireup="true" CodeBehind="GeneralAccounts.aspx.cs" Inherits="SPEWEB.F_81_Hrm.F_90_PF.GeneralAccounts" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>
<script runat="server">

   
</script>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
   <%--<script language="JavaScript" src="JS/JScript.js" type="text/javascript"></script>--%>
    <link href="CSS/Style.css" rel="stylesheet" type="text/css" />
    <link href="../../CSS/PageInformation.css" rel="stylesheet" type="text/css" />
    <link href="../../CSS/Style.css" rel="stylesheet" type="text/css" />
   
   
  
    <script type="text/javascript" src="JS/JScript.js" ></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">


                    
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
          <ContentTemplate>
            <div class="container moduleItemWrpper">
                <div class="contentPart">
                    <div class="row">
                        <fieldset class="scheduler-border fieldset_A">
                            <div class="form-horizontal">
                                <div class="form-group">
                                     <div class="col-md-6 pading5px asitCol6">
                                          <asp:LinkButton ID="ibtnvounu" runat="server" CssClass="btn btn-primary srearchBtn" OnClick="ibtnvounu_Click" Visible="false"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>

                                            <asp:Label ID="lblEntryDate" runat="server" CssClass="lblTxt lblName" Text="Voucher Date"></asp:Label>

                                            <asp:TextBox ID="txtEntryDate" runat="server" CssClass=" inputtextbox"></asp:TextBox>
                                             <cc1:CalendarExtender ID="txtEntryDate_CalendarExtender" runat="server"  Enabled="True" Format="dd-MMM-yyyy ddd" TargetControlID="txtEntryDate">  </cc1:CalendarExtender>

                                          <asp:LinkButton ID="lnkPrivVou" runat="server" CssClass="btn btn-primary  primaryBtn" OnClick="lnkPrivVou_Click">Prev.Voucher</asp:LinkButton>

                                          <asp:DropDownList ID="ddlPrivousVou" runat="server" Width="200px" CssClass="ddlPage"> </asp:DropDownList>

                                            <asp:LinkButton ID="lnkOk" runat="server" CssClass="btn btn-primary  primaryBtn" OnClick="lnkOk_Click">Ok</asp:LinkButton>
                                        </div>
                                      </div>
                                <div class="form-group">
                                     <div class="col-md-6 pading5px asitCol6">
                                         
                                            <asp:Label ID="lbllstVouno" runat="server" CssClass="lblTxt lblName" Text="Last Voucher No."></asp:Label>

                                            <asp:TextBox ID="txtLastVou" runat="server" ReadOnly="true" CssClass=" inputtextbox"></asp:TextBox>

                                            <asp:Label ID="lblcontrolAccHead" runat="server" CssClass="lblTxt lblName" Text="Control Accounts"></asp:Label>

                                          <asp:DropDownList ID="ddlConAccHead" runat="server"  onselectedindexchanged="ddlConAccHead_SelectedIndexChanged" Width="220px" CssClass="ddlPage"> </asp:DropDownList>

                                        </div>
                                     </div>
                                <div class="form-group">
                                     <div class="col-md-6 pading5px asitCol6">
                                         
                                            <asp:Label ID="lblcurVounum" runat="server" CssClass="lblTxt lblName" Text="CR Voucher No."></asp:Label>

                                            <asp:TextBox ID="txtcurrentvou" runat="server"  CssClass=" inputtextbox"></asp:TextBox>

                                            <asp:TextBox ID="txtCurrntlast6" runat="server"  CssClass=" inputtextbox"  ToolTip="You Can Change Voucher Number." Enabled="False"></asp:TextBox>

                                            <asp:Label ID="lblmsg" runat="server" CssClass=" btn btn-danger primaryBtn"></asp:Label>

                                        </div>
                                     </div>

                                <asp:Panel ID="Panel2" runat="server"  Visible="false">

                                     <div class="form-group">
                                     <div class="col-md-10 pading5px asitCol10">

                                         <asp:TextBox ID="txtserceacc" runat="server" CssClass=" inputtextbox"></asp:TextBox>

                                          <asp:LinkButton ID="lnkAcccode" runat="server" CssClass="btn btn-primary primaryBtn" OnClick="lnkAcccode_Click">Head of Account</asp:LinkButton>

                                          <asp:DropDownList ID="ddlacccode" runat="server" AutoPostBack="True"  CssClass=" ddlPage" onselectedindexchanged="ddlacccode_SelectedIndexChanged"   Width="320px"> </asp:DropDownList>

                                            <asp:Label ID="lblDramt" runat="server" CssClass=" smLbl_to" Text="Dr. Amount" ></asp:Label>

                                         <asp:TextBox ID="txtDrAmt" runat="server" CssClass=" inputtextbox"></asp:TextBox>

                                        </div>
                                      </div>

                                     <div class="form-group">
                                     <div class="col-md-6 pading5px asitCol6">

                                         <asp:TextBox ID="txtserchReCode" runat="server" CssClass=" inputtextbox" Visible="false"></asp:TextBox>

                                          <asp:LinkButton ID="lnkRescode" runat="server" CssClass="btn btn-primary srearchBtn" OnClick="lnkRescode_Click" Visible="false">Sub of Account</asp:LinkButton>

                                          <asp:DropDownList ID="ddlresuorcecode" runat="server" AutoPostBack="True"  CssClass=" ddlPage" onselectedindexchanged="ddlresuorcecode_SelectedIndexChanged"   Visible="false"  Width="440px"> </asp:DropDownList>

                                            <asp:Label ID="lblCramt" runat="server" CssClass="lblTxt lblName" Text="Cr. Amount"></asp:Label>

                                         <asp:TextBox ID="txtCrAmt" runat="server" CssClass=" inputtextbox"></asp:TextBox>

                                        </div>
                                      </div>

                                     <div class="form-group">
                                     <div class="col-md-6 pading5px asitCol6">

                                         <asp:TextBox ID="txtSearchSpeci" runat="server" CssClass=" inputtextbox" Visible="false"></asp:TextBox>

                                          <asp:LinkButton ID="lnkSpecification" runat="server" CssClass="btn btn-primary srearchBtn" OnClick="lnkSpecification_Click" Visible="false">Specification</asp:LinkButton>

                                          <asp:DropDownList ID="ddlSpclinf" runat="server" AutoPostBack="True"  CssClass=" ddlPage" onselectedindexchanged="ddlSpclinf_SelectedIndexChanged"   Visible="false" Width="220px"> </asp:DropDownList>

                                            <asp:Label ID="lblrate" runat="server" CssClass="lblTxt lblName" Text="Rate" Visible="False"></asp:Label>

                                         <asp:TextBox ID="txtrate" runat="server" CssClass=" inputtextbox" ReadOnly="true" Visible="false"></asp:TextBox>

                                          <asp:Label ID="lblqty" runat="server" CssClass="lblTxt lblName"  Text="Quantity" Visible="False"></asp:Label>

                                         <asp:TextBox ID="txtqty" runat="server" CssClass=" inputtextbox"  Visible="false"></asp:TextBox>

                                        </div>
                                      </div>

                                     <div class="form-group">
                                     <div class="col-md-6 pading5px asitCol6">

                                            <asp:Label ID="lblremarks" runat="server" CssClass="lblTxt lblName" Text="Remarks"></asp:Label>

                                           <asp:TextBox ID="txtremarks" runat="server" CssClass=" inputtextbox" ></asp:TextBox>

                                           <asp:LinkButton ID="lnkOk0" runat="server" CssClass="btn btn-primary primaryBtn" OnClick="lnkOk0_Click">Add Table</asp:LinkButton>
                                         
                                        </div>
                                      </div>

                                </asp:Panel>
                            </div>
                        </fieldset>
                    </div>
                     <div class="table table-responsive">
                            <asp:GridView ID="dgv1" runat="server" AutoGenerateColumns="False"  CssClass=" table-striped table-hover table-bordered grvContentarea"
                                         
                                            Height="16px" ShowFooter="True" Width="674px">
                                           
                                            <Columns>
                                                <asp:TemplateField HeaderText="Sl.No.">
                                                    <ItemTemplate>
                                                        <asp:Label ID="serialnoid" runat="server" CssClass="GridLebel" 
                                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle Font-Bold="True" Font-Size="16px" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="A/c Code" Visible="False">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblAccCod" runat="server" CssClass="GridLebel" 
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actcode")) %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Sub Code" Visible="False">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblResCod" runat="server" CssClass="GridLebel" 
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "subcode")) %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Spcl Code" Visible="False">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSpclCod" runat="server" CssClass="GridLebel" 
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "spclcode")) %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Head of Accounts">
                                                   <FooterTemplate>
                                                        <asp:LinkButton ID="lnkTotal" runat="server" Font-Bold="True" 
                                                            onclick="lnkTotal_Click"  CssClass="btn btn-primary primarygrdBtn">Total</asp:LinkButton>
                                                    </FooterTemplate>
                                                    <ItemTemplate>
                                                     <asp:Label ID="lblAccdesc1" runat="server" CssClass="GridLebelL" 
                                                                        Font-Size="11px" 
                                                                        Text='<%# "<B>"+ Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc")) + "</B>"+
                                                                         (DataBinder.Eval(Container.DataItem, "subdesc").ToString().Trim().Length>0 ? 
                                                                         (Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc")).Trim().Length>0 ? "<br>" : "") + 
                                                                         "&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp" + 
                                                                         Convert.ToString(DataBinder.Eval(Container.DataItem, "subdesc")).Trim(): "")  + 
                                                                         (DataBinder.Eval(Container.DataItem, "spcldesc").ToString().Trim().Length>0 ? 
                                                                         " [" + Convert.ToString(DataBinder.Eval(Container.DataItem, "spcldesc")).Trim() + "]": "") %>' 
                                                                        Width="400px" Font-Names="Verdana"></asp:Label>
                                                                                                                             
                                                        <asp:Label ID="lblAccdesc" runat="server" CssClass="GridLebelL" 
                                                            Font-Size="11px"  Visible="False"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc")) %>' 
                                                            Width="50px" Font-Names="Verdana"></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterStyle HorizontalAlign="Right" />
                                                    <HeaderStyle HorizontalAlign="Left" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Details Description" Visible="False">
                                                 <ItemTemplate>
                                                   <asp:Label ID="lblResdesc" runat="server" CssClass="GridLebelL" 
                                                                        Font-Size="10px" 
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "subdesc")) %>' 
                                                                        Width="300px"></asp:Label>
                                                 </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Left" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Specification" Visible="False">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSpcldesc" runat="server" CssClass="GridLebel" 
                                                            Font-Size="12px" 
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "spcldesc")) %>' 
                                                            Width="80px"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                
                                                <asp:TemplateField HeaderText="Quantity">
                                                    <FooterTemplate>
                                                        <asp:TextBox ID="txtTgvQty" runat="server" BackColor="Transparent" 
                                                            BorderColor="Transparent" BorderStyle="None" BorderWidth="1px" 
                                                            CssClass="GridTextbox" Visible="False" Width="60px" Font-Size="12px" 
                                                            ReadOnly="True"></asp:TextBox>
                                                    </FooterTemplate>
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtgvQty" runat="server" BackColor="Transparent" 
                                                            BorderColor="Transparent" BorderStyle="None" 
                                                            CssClass="GridTextbox" 
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "trnqty")).ToString("#,##0.00;(#,##0.00); ") %>' 
                                                            Width="60px" Font-Size="12px" ForeColor="Black"></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Rate">
                                                    <FooterTemplate>
                                                        <asp:TextBox ID="txtTgvRate" runat="server" BackColor="Transparent" 
                                                            BorderColor="Transparent" BorderStyle="None" BorderWidth="1px" 
                                                            CssClass="GridTextbox" 
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "trnqty")).ToString("#,##0.00;(#,##0.00); ") %>' 
                                                            Visible="False" Width="80px" Font-Size="12px" ReadOnly="True"></asp:TextBox>
                                                    </FooterTemplate>
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtgvRate" runat="server" BackColor="Transparent" 
                                                            BorderColor="Transparent" BorderStyle="None" BorderWidth="1px" 
                                                            CssClass="GridTextbox" 
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "trnrate")).ToString("#,##0.00;(#,##0.00); ") %>' 
                                                            Width="80px" Font-Size="12px" ReadOnly="True" ForeColor="Black"></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Dr.Amount">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtgvDrAmt" runat="server" BackColor="Transparent" 
                                                            BorderColor="Transparent" BorderStyle="None" BorderWidth="1px" 
                                                           
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "trndram")).ToString("#,##0.00;(#,##0.00); ") %>' 
                                                            Width="90px" Font-Size="12px" ForeColor="Black" style="text-align:right;"></asp:TextBox>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:TextBox ID="txtTgvDrAmt" runat="server" BackColor="Transparent" 
                                                            BorderColor="Transparent" BorderStyle="None" BorderWidth="1px" 
                                                             Font-Bold="True" Font-Size="12px" ReadOnly="True" 
                                                            Width="90px" ForeColor="#000"></asp:TextBox>
                                                    </FooterTemplate>
                                                     <ItemStyle HorizontalAlign="Right" />
                                                    <FooterStyle HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Cr.Amount">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtgvCrAmt" runat="server" BackColor="Transparent" 
                                                            BorderColor="Transparent" BorderStyle="None" BorderWidth="1px" 
                                                           
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "trncram")).ToString("#,##0.00;(#,##0.00); ") %>' 
                                                            Width="90px" Font-Size="12px" ForeColor="Black" style="text-align:right;">></asp:TextBox>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:TextBox ID="txtTgvCrAmt" runat="server" BackColor="Transparent" 
                                                            BorderColor="Transparent" BorderStyle="None" BorderWidth="1px" 
                                                            Font-Bold="True" Font-Size="12px" ReadOnly="True" 
                                                            Width="90px" ForeColor="#000"></asp:TextBox>
                                                    </FooterTemplate>
                                                    
                                                    <ItemStyle HorizontalAlign="Right" />
                                                    <FooterStyle HorizontalAlign="Right" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Remarks">                                          
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtgvRemarks" runat="server" BackColor="Transparent" 
                                                            BorderColor="Transparent" BorderStyle="None" BorderWidth="1px" 
                                                            CssClass="GridTextbox" Font-Size="12px" 
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "trnrmrk")) %>' 
                                                            Width="80px" ForeColor="Black"></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                            <FooterStyle CssClass="grvFooter"/>
                                            <EditRowStyle />
                                            <AlternatingRowStyle />
                                            <PagerStyle CssClass="gvPagination" />
                                            <HeaderStyle CssClass="grvHeader" />


                                        </asp:GridView>

                       <fieldset class="scheduler-border fieldset_A">
                          <div class="form-horizontal">
                              <asp:Panel ID="Panel4" runat="server" Visible="False">
                               <div class="form-group">
                                     <div class="col-md-6 pading5px asitCol6">

                                          <asp:Label ID="lblRefNum" runat="server" CssClass="lblTxt lblName" Text="Ref./Cheq No/Slip No." ></asp:Label>

                                         <asp:TextBox ID="txtRefNum" runat="server" AutoCompleteType="Disabled"   CssClass=" inputtextbox"></asp:TextBox>

                                          <asp:Label ID="lblSrInfo" runat="server" CssClass="lblTxt lblName"  Text="Other ref. (if any)"  ></asp:Label>

                                          <asp:TextBox ID="txtSrinfo" runat="server" AutoCompleteType="Disabled"  CssClass=" inputtextbox" Width="200px"></asp:TextBox>

                                          <asp:LinkButton ID="lnkFinalUpdate" runat="server" CssClass="btn btn-danger primaryBtn" OnClick="lnkFinalUpdate_Click" >Final Update</asp:LinkButton>

                                        </div>
                                      </div>

                               <div class="form-group">
                                     <div class="col-md-10 pading5px asitCol10">

                                          <asp:Label ID="lblNaration" runat="server" CssClass="lblTxt lblName" Text="Narration"  ></asp:Label>

                                        <asp:TextBox ID="txtNarration" runat="server" AutoCompleteType="Disabled"   TextMode="MultiLine" Width="370px"  CssClass="inputtextbox" ></asp:TextBox>

                                        </div>
                                      </div>

                          </asp:Panel>
                        </div>
                    </fieldset>

                     </div>
                </div>
            </div>


                           
                                <%--<tr>
                                    <td align="left" valign="top" colspan="3" style="text-align: center">
                                        <asp:ImageButton ID="ibtnvounu" runat="server" Height="16px" 
                                            ImageUrl="~/Image/movie_26.gif" onclick="ibtnvounu_Click" Width="145px" 
                                            Visible="False" />
                                    </td>
                                    <td align="left" style="width: 93px" valign="top" Width="280px">
                                        &nbsp;</td>
                                    <td align="right" valign="top">
                                        <asp:Label ID="lblEntryDate" runat="server" CssClass="label2" ForeColor="#000" 
                                            Text="Voucher Date" Width="100px" Font-Bold="True" Font-Size="12px"></asp:Label>
                                    </td>
                                    <td align="left" valign="top">
                                        <asp:TextBox ID="txtEntryDate" runat="server" CssClass="ddl" 
                                             Width="97px"></asp:TextBox>
                                        <cc1:CalendarExtender ID="txtEntryDate_CalendarExtender" runat="server" 
                                            Enabled="True" Format="dd-MMM-yyyy ddd" TargetControlID="txtEntryDate">
                                        </cc1:CalendarExtender>
                                        <br />
                                    </td>
                                    <td align="left" class="style8" valign="top">
                                        <asp:LinkButton ID="lnkPrivVou" runat="server" CssClass="button" 
                                            Font-Bold="True" Font-Size="11px" Height="16px" onclick="lnkPrivVou_Click" 
                                            Width="90px">Prev.Voucher</asp:LinkButton>
                                    </td>
                                    <td class="style8" align="left" valign="top">
                                        <asp:DropDownList ID="ddlPrivousVou" runat="server" BackColor="Aqua" 
                                            CssClass="ddl" Height="19px" Width="200px">
                                        </asp:DropDownList>
                                    </td>
                                    <td align="left" class="style191" valign="top">
                                        <asp:LinkButton ID="lnkOk" runat="server" CssClass="button" Font-Bold="True" 
                                            onclick="lnkOk_Click"   Width="90px" Font-Size="12px">Ok</asp:LinkButton>
                                    </td>
                                    <td>
                                    </td>
                                </tr>--%>
                                <%--<tr valign="top">
                                    <td align="left" class="style196" valign="top">
                                        <asp:Label ID="lbllstVouno" runat="server" CssClass="label2" 
                                            Text="Last Voucher No." Width="120px"></asp:Label>
                                    </td>
                                    <td align="left" colspan="2" valign="top">
                                        <asp:TextBox ID="txtLastVou" runat="server" BackColor="Aqua" CssClass="ddl" 
                                             ReadOnly="True" Width="90px"></asp:TextBox>
                                    </td>
                                    <td align="left" style="width: 93px" valign="top" Width="280px">
                                        &nbsp;</td>
                                    <td align="right" valign="top">
                                        <asp:Label ID="lblcontrolAccHead" runat="server" CssClass="label2" 
                                            Text="Control Accounts" Width="100px" ForeColor="#000" Font-Bold="True" 
                                            Font-Size="12px"></asp:Label>
                                    </td>
                                    <td align="left" valign="top" colspan="3">
                                        <asp:DropDownList ID="ddlConAccHead" runat="server" 
                                            CssClass="ddl" 
                                            onselectedindexchanged="ddlConAccHead_SelectedIndexChanged" Width="400px">
                                        </asp:DropDownList>
                                    </td>
                                    <td class="style191" align="left" valign="top">
                                        </td>
                                    <td>
                                    </td>
                                </tr>--%>
                                <%--<tr>
                                    <td align="left" class="style196" valign="top">
                                        <asp:Label ID="lblcurVounum" runat="server" CssClass="label2" 
                                            Text="Current Voucher No." Width="120px"></asp:Label>
                                    </td>
                                    <td align="left" style="width: 0" valign="top" Width="280px">
                                        <asp:TextBox ID="txtcurrentvou" runat="server" 
                                            CssClass="ddl" ReadOnly="True" 
                                            Width="40px"></asp:TextBox>
                                    </td>
                                    <td align="left" valign="top">
                                        <asp:TextBox ID="txtCurrntlast6" runat="server" 
                                            CssClass="ddl" 
                                            ToolTip="You Can Change Voucher Number." Width="40px" Enabled="False"></asp:TextBox>
                                    </td>
                                    <td align="left" style="width: 93px" valign="top" Width="280px">
                                        &nbsp;</td>
                                    <td class="style195" align="right" valign="top" colspan="4">
                                        <asp:Label ID="lblmsg" runat="server" CssClass="label3" Width="500px" 
                                            Font-Names="Verdana" Font-Size="12px" Height="16px"></asp:Label>
                                    </td>
                                    <td class="style191">
                                        </td>
                                    <td>
                                        </td>
                                </tr>--%>
                    
                                                <%--<tr>
                                                    <td align="left" class="style24" valign="top">
                                                        <asp:TextBox ID="txtserceacc" runat="server" CssClass="ddl" Width="70px"></asp:TextBox>
                                                    </td>
                                                    <td align="left" class="style25" valign="top">
                                                        <asp:LinkButton ID="lnkAcccode" runat="server" CssClass="button" 
                                                            onclick="lnkAcccode_Click" Width="120px" Font-Size="12px">Head of Account</asp:LinkButton>
                                                    </td>
                                                    <td align="left" class="style119" valign="top" colspan="3">
                                                        <asp:DropDownList ID="ddlacccode" runat="server" AutoPostBack="True" 
                                                            CssClass="ddl" onselectedindexchanged="ddlacccode_SelectedIndexChanged" 
                                                            Width="440px">
                                                        </asp:DropDownList>
                                                        <cc1:ListSearchExtender ID="ddlacccode_ListSearchExtender" runat="server" 
                                                            Enabled="True" QueryPattern="Contains" TargetControlID="ddlacccode">
                                                        </cc1:ListSearchExtender>
                                                    </td>
                                                    <td align="left" class="style122" valign="top">
                                                        <asp:Label ID="lblDramt" runat="server" CssClass="label2" Text="Dr. Amount" 
                                                            Width="71px" ForeColor="#000"></asp:Label>
                                                    </td>
                                                    <td align="left" class="style129" valign="top">
                                                        <asp:TextBox ID="txtDrAmt"  runat="server" CssClass="ddl"></asp:TextBox>
                                                    </td>
                                                    <td>
                                                    </td>
                                                </tr>--%>
                                                <%--<tr>
                                                    <td class="style24">
                                                        <asp:TextBox ID="txtserchReCode" runat="server" CssClass="ddl" Visible="False" 
                                                            Width="70px"></asp:TextBox>
                                                    </td>
                                                    <td class="style25">
                                                        <asp:LinkButton ID="lnkRescode" runat="server" CssClass="button" 
                                                            onclick="lnkRescode_Click" Visible="False" Width="120px" Font-Size="12px">Sub of Account</asp:LinkButton>
                                                    </td>
                                                    <td class="style119" colspan="3">
                                                        <asp:DropDownList ID="ddlresuorcecode" runat="server" AutoPostBack="True" 
                                                            CssClass="ddl" onselectedindexchanged="ddlresuorcecode_SelectedIndexChanged" 
                                                            Visible="False" Width="440px">
                                                        </asp:DropDownList>
                                                        <cc1:ListSearchExtender ID="ddlresuorcecode_ListSearchExtender" runat="server" 
                                                            Enabled="True" QueryPattern="Contains" TargetControlID="ddlresuorcecode">
                                                        </cc1:ListSearchExtender>
                                                    </td>
                                                    <td class="style122">
                                                        <asp:Label ID="lblCramt" runat="server" CssClass="label2" ForeColor="#000" 
                                                            Text="Cr. Amount" Width="71px"></asp:Label>
                                                    </td>
                                                    <td class="style129">
                                                        <asp:TextBox ID="txtCrAmt" runat="server" CssClass="ddl"></asp:TextBox>
                                                    </td>
                                                    <td>
                                                        &nbsp;</td>
                                                </tr>--%>
                                                <%--<tr>
                                                    <td class="style24">
                                                        <asp:TextBox ID="txtSearchSpeci" runat="server" CssClass="ddl" Visible="False" 
                                                            Width="70px"></asp:TextBox>
                                                    </td>
                                                    <td class="style25">
                                                        <asp:LinkButton ID="lnkSpecification" runat="server" CssClass="button" 
                                                            onclick="lnkSpecification_Click" Visible="False" Width="120px" 
                                                            Font-Size="12px">Specification</asp:LinkButton>
                                                    </td>
                                                    <td class="style119">
                                                        <asp:DropDownList ID="ddlSpclinf" runat="server" AutoPostBack="True" 
                                                            CssClass="ddl" onselectedindexchanged="ddlSpclinf_SelectedIndexChanged" 
                                                            Visible="False" Width="220px">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td class="style119">
                                                        <asp:Label ID="lblrate" runat="server" CssClass="label2" ForeColor="#000" 
                                                            Text="Rate" Visible="False" Width="71px"></asp:Label>
                                                    </td>
                                                    <td class="style119">
                                                        <asp:TextBox ID="txtrate" runat="server" CssClass="ddl" Visible="False" 
                                                            ReadOnly="True"></asp:TextBox>
                                                    </td>
                                                    <td class="style122">
                                                        <asp:Label ID="lblqty" runat="server" CssClass="label2" ForeColor="#000" 
                                                            Text="Quantity" Visible="False" Width="71px"></asp:Label>
                                                    </td>
                                                    <td class="style129">
                                                        <asp:TextBox ID="txtqty" runat="server" CssClass="ddl" 
                                                            Visible="False"></asp:TextBox>
                                                    </td>
                                                    <td>
                                                    </td>
                                                </tr>--%>
                                                <%--<tr>
                                                    <td class="style24">
                                                        &nbsp;</td>
                                                    <td class="style25">
                                                        <asp:Label ID="lblremarks" runat="server" CssClass="label2" ForeColor="#000" 
                                                            Text="Remarks" Width="81px" Height="17px"></asp:Label>
                                                    </td>
                                                    <td class="style133">
                                                        <asp:TextBox ID="txtremarks" runat="server" CssClass="ddl" Width="220px"></asp:TextBox>
                                                    </td>
                                                    <td class="style133">
                                                        &nbsp;</td>
                                                    <td class="style133">
                                                        &nbsp;</td>
                                                    <td class="style134">
                                                        &nbsp;</td>
                                                    <td class="style135">
                                                        <asp:LinkButton ID="lnkOk0" runat="server" CssClass="button" Font-Bold="True" 
                                                            onclick="lnkOk0_Click" Width="78px" Font-Size="12px">Add Table</asp:LinkButton>
                                                    </td>
                                                    <td class="style136">
                                                    </td>
                                                </tr>--%>
                            
                                                <%--<tr>
                                                    <td class="style158">
                                                        <asp:Label ID="lblRefNum" runat="server" CssClass="label2" 
                                                            Text="Ref./Cheq No/Slip No." Width="120px"></asp:Label>
                                                    </td>
                                                    <td class="style161">
                                                        <asp:TextBox ID="txtRefNum" runat="server" AutoCompleteType="Disabled" 
                                                            CssClass="ddl" Width="166px"></asp:TextBox>
                                                    </td>
                                                    <td class="style198">
                                                        &nbsp;</td>
                                                    <td class="style198">
                                                        <asp:Label ID="lblSrInfo" runat="server" CssClass="label2" 
                                                            Text="Other ref. (if any)" Width="100px"></asp:Label>
                                                    </td>
                                                    <td class="style117">
                                                        <asp:TextBox ID="txtSrinfo" runat="server" AutoCompleteType="Disabled" 
                                                            CssClass="ddl" Width="240px"></asp:TextBox>
                                                    </td>
                                                    <td class="style197">
                                                        &nbsp;</td>
                                                    <td class="style199">
                                                        <asp:LinkButton ID="lnkFinalUpdate" runat="server" CssClass="button" 
                                                            Font-Bold="True" Font-Size="12px" onclick="lnkFinalUpdate_Click" Width="90px">Final Update</asp:LinkButton>
                                                    </td>
                                                </tr>--%>
                                                <%--<tr>
                                                    <td  valign="top" style="text-align: right">
                                                        <asp:Label ID="lblNaration" runat="server" CssClass="label2" Text="Narration" 
                                                            Width="100px"></asp:Label>
                                                    </td>
                                                    <td class="style159" colspan="4">
                                                        <asp:TextBox ID="txtNarration" runat="server" AutoCompleteType="Disabled"   TextMode="MultiLine" Width="600px"></asp:TextBox>
                                                    </td>
                                                    <td class="style199">
                                                        &nbsp;</td>
                                                    <td>
                                                        &nbsp;</td>
                                                </tr>--%>
                                               
             </ContentTemplate>
       </asp:UpdatePanel>
</asp:Content>

