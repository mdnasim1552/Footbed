<%@ Page Title="" Language="C#" MasterPageFile="~/SPEMaster.Master" AutoEventWireup="true" CodeBehind="LinkFinncialReports.aspx.cs" Inherits="SPEWEB.F_35_GrAcc.LinkFinncialReports" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="CSS/Style.css" rel="stylesheet" type="text/css" />
    <link href="../CSS/Style.css" rel="stylesheet" type="text/css" />
 
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

   

    
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="container moduleItemWrpper">
                 <div class="contentPart">
                        <div class="row">
                        <fieldset class="scheduler-border fieldset_A">
                            <div class="form-horizontal">
                              <asp:Panel ID="Panel4" runat="server">

                                       <div class="form-group">
                                              <div class="col-md-5 asitCol5 pading5px">
                                                 <asp:Label ID="lblDatefrom" runat="server" CssClass="lblTxt lblName"  Text="Date From:"></asp:Label>

                                                 <asp:Label ID="lblfrmdate" runat="server" CssClass=" inputtextbox"></asp:Label>

                                                  <asp:Label ID="lbldateto" runat="server" CssClass=" smLbl_to"  Text="To:"></asp:Label>

                                                 <asp:Label ID="lbltodate" runat="server" CssClass=" inputtextbox"></asp:Label>

                                                 <asp:Label ID="lblrptlbl" runat="server" CssClass=" smLbl_to"  Text="Report Level:"></asp:Label>

                                              
                                                 <asp:DropDownList ID="DDListLevels" runat="server"  CssClass="ddlPage" OnSelectedIndexChanged="DDListLevels_SelectedIndexChanged">
                                                                            <asp:ListItem>level-1</asp:ListItem>
                                                                            <asp:ListItem>level-2</asp:ListItem>
                                                                            <asp:ListItem>level-3</asp:ListItem>
                                                                            <asp:ListItem Selected="True">level-4</asp:ListItem>
                                                                        </asp:DropDownList>

                                                   <asp:LinkButton ID="lbtnOk" CssClass="btn btn-primary primaryBtn" runat="server" OnClick="lbtnOk_Click" TabIndex="2">Ok </span></asp:LinkButton>

                                                </div>
                                            <div class="col-md-1">
                                                <asp:CheckBox ID="ChkTopHead" runat="server" Checked="True"  CssClass=" btn btn-primary checkBox" OnCheckedChanged="ChkTopHead_CheckedChanged"  Text="Top Head"/>
                                            </div>
                                           </div>

                                       <div class="form-group">
                                              <div class="col-md-10 asitCol10 pading5px">
                                                 <asp:Label ID="lblOpeningDate" runat="server" CssClass="lblTxt lblName"  Text="Opening Date:"  Visible="False"></asp:Label>

                                                   <asp:TextBox ID="txtOpeningDate" runat="server"  CssClass="inputtextbox"  Visible="False"></asp:TextBox>
                                                    <cc1:CalendarExtender ID="txtOpeningDate_CalendarExtender" runat="server" 
                                                        Format="dd-MMM-yyyy " TargetControlID="txtOpeningDate">
                                                    </cc1:CalendarExtender>

                                                </div>
                                           </div>

                                  </asp:Panel>
                                </div>
                            </fieldset>
                        </div>
                     <div class="table table-responsive">
                         <asp:MultiView ID="MultiView1" runat="server">
                              <asp:View ID="ISView" runat="server">
                                   <div class="form-group">
                                              <div class="col-md-10 asitCol10 pading5px">
                                               <asp:Label ID="Label10" runat="server"  Text="Income Statement" CssClass="lblName lblTxt"    Visible="False"></asp:Label>

                                                </div>
                                           </div>

                                   <asp:GridView ID="dgvIS" runat="server" AutoGenerateColumns="False"  CssClass=" table-striped table-hover table-bordered grvContentarea"

                                   
                                    OnRowDataBound="dgvIS_RowDataBound" Width="647px">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Code">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvcode6" runat="server" CssClass="GridLebel" 
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actcode4")) %>' 
                                                    Width="100px"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField FooterStyle-Font-Bold="true" FooterStyle-Font-Size="12px" 
                                            HeaderText="Description Of accounts">
                                            <HeaderTemplate>
                                                <table style="width:47%;">
                                                    <tr>
                                                        <td class="style58">
                                                            <asp:Label ID="Label4" runat="server" Font-Bold="True" 
                                                                Text="Description Of Accounts" Width="180px"></asp:Label>
                                                        </td>
                                                        <td class="style60">
                                                            &nbsp;</td>
                                                        <td>
                                                            <asp:HyperLink ID="hlbtnDetails" runat="server"  CssClass="btn btn-primary primaryBtn" Target="_blank" Width="90px">Next</asp:HyperLink>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:HyperLink ID="HLgvISDesc" runat="server" __designer:wfdid="w38" 
                                                    CssClass="GridLebelL" Font-Underline="False" Target="_blank" 
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc4")) %>' 
                                                    Width="300px"></asp:HyperLink>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" Font-Size="12px" />
                                            <HeaderStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField FooterStyle-Font-Bold="true" FooterStyle-Font-Size="12px" 
                                            FooterStyle-HorizontalAlign="Right" HeaderText="Current Period" 
                                            ItemStyle-HorizontalAlign="Right">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvcuamt" runat="server" CssClass="GridLebel" 
                                                    Font-Size="10px" 
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "curam")).ToString("#,##0;(#,##0); ") %>' 
                                                    Width="100px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Right" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField FooterStyle-Font-Bold="true" FooterStyle-Font-Size="12px" 
                                            FooterStyle-HorizontalAlign="Right" HeaderText="Previous Period" 
                                            ItemStyle-HorizontalAlign="Right">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvopnamt" runat="server" CssClass="GridLebel" 
                                                    Font-Size="10px" 
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "opnam")).ToString("#,##0;(#,##0); ") %>' 
                                                    Width="100px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Right" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Change" ItemStyle-HorizontalAlign="Right">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvclobal" runat="server" CssClass="GridLebel" 
                                                    Font-Size="10px" 
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "changeam")).ToString("#,##0;(#,##0); ") %>' 
                                                    Width="100px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Current %" ItemStyle-HorizontalAlign="Right">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvCPar" runat="server" CssClass="GridLebel" Font-Size="10px" 
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "percentcu")).ToString("#,##0.00;(#,##0.00); ") %>' 
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Total %" ItemStyle-HorizontalAlign="Right">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvPar" runat="server" CssClass="GridLebel" Font-Size="10px" 
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "cpercent")).ToString("#,##0.00;(#,##0.00); ") %>' 
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                    </Columns>
                                   <FooterStyle CssClass="grvFooter" />
                                <EditRowStyle />
                                <AlternatingRowStyle />
                                <PagerStyle CssClass="gvPagination" />
                                <HeaderStyle CssClass="grvHeader" />

                                </asp:GridView>

                              </asp:View>

                             <asp:View ID="BSView" runat="server">
                                   <div class="form-group">
                                              <div class="col-md-10 asitCol10 pading5px">
                                               <asp:Label ID="Label11" runat="server" Text="Balance Sheet"  CssClass="lblName lblTxt"    Visible="False"></asp:Label>

                                                </div>
                                           </div>

                                   <asp:GridView ID="dgvBS" runat="server" AutoGenerateColumns="False" CssClass=" table-striped table-hover table-bordered grvContentarea"
                                   
                                    OnRowDataBound="dgvBS_RowDataBound" Width="640px">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Code">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvcode" runat="server" CssClass="GridLebel" 
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actcode4")) %>' 
                                                    Width="100px"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Description Of accounts">
                                            <HeaderTemplate>
                                                <table style="width:47%;">
                                                    <tr>
                                                        <td class="style58">
                                                            <asp:Label ID="Label3122" runat="server" Font-Bold="True" 
                                                                Text="Description Of Accounts" Width="180px"></asp:Label>
                                                        </td>
                                                        <td class="style60">
                                                            &nbsp;</td>
                                                        <td>
                                                            <asp:HyperLink ID="hlbtnDetailsbs" runat="server"   CssClass="btn btn-primary primaryBtn" Target="_blank" Width="90px">Next</asp:HyperLink>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:HyperLink ID="HLgvBSDesc" runat="server" __designer:wfdid="w38" 
                                                    CssClass="GridLebelL" Font-Underline="False" Target="_blank" 
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc4")) %>' 
                                                    Width="300px"></asp:HyperLink>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Total" ItemStyle-HorizontalAlign="Right">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvclobal" runat="server" CssClass="GridLebel" 
                                                    Font-Size="10px" 
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "closam")).ToString("#,##0.00;(#,##0.00); ") %>' 
                                                    Width="100px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Opening" ItemStyle-HorizontalAlign="Right">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvopnamt" runat="server" CssClass="GridLebel" 
                                                    Font-Size="10px" 
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "opnam")).ToString("#,##0.00;(#,##0.00); ") %>' 
                                                    Width="100px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField FooterStyle-Font-Bold="true" FooterStyle-Font-Size="12px" 
                                            FooterStyle-HorizontalAlign="Right" HeaderText="Changes During the Period" 
                                            ItemStyle-HorizontalAlign="Right">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvcuamt" runat="server" CssClass="GridLebel" 
                                                    Font-Size="10px" 
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "curam")).ToString("#,##0.00;(#,##0.00); ") %>' 
                                                    Width="100px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Right" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                    </Columns>
                                  <FooterStyle CssClass="grvFooter"/>
                                        <EditRowStyle />
                                        <AlternatingRowStyle />
                                        <PagerStyle CssClass="gvPagination" />
                                        <HeaderStyle CssClass="grvHeader" />

                                </asp:GridView>
                             </asp:View>


                               <asp:View ID="PSView" runat="server">
                                   <asp:Panel ID="Panel1" runat="server">
                                    <div class="form-horizontal">

                                <div class="form-group">
                                    <div class="col-md-5 pading5px asitCol5">
                                        <asp:Label ID="Label13" runat="server" CssClass="lblTxt lblName" Text="Project Name:"></asp:Label>

                                        <asp:TextBox ID="txtSearch" runat="server"  CssClass="inputtextbox"></asp:TextBox>
                                      
                                         <asp:LinkButton ID="ImgbtnFindProj" CssClass="btn btn-primary srearchBtn" runat="server" OnClick="ImgbtnFindProj_Click" TabIndex="2"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>

                                        <asp:DropDownList ID="ddlAccProject" runat="server" AutoPostBack="True"  CssClass="ddlPage" OnSelectedIndexChanged="ddlAccProject_SelectedIndexChanged" Width="400px">
                                                </asp:DropDownList>
                                    </div>
                                </div>

                                  <div class="form-group">
                                    <div class="col-md-8 pading5px asitCol8">
                                        <asp:Label ID="Label14" runat="server" CssClass="lblTxt lblName" Text="Resource Name:"></asp:Label>

                                        <asp:TextBox ID="txtSrcRes" runat="server"  CssClass="inputtextbox"></asp:TextBox>
                                      
                                         <asp:LinkButton ID="ImgbtnFindRes" CssClass="btn btn-primary srearchBtn" runat="server" OnClick="ImgbtnFindRes_Click" TabIndex="2"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>

                                        <asp:DropDownList ID="ddlResHead" runat="server" AutoPostBack="True"  CssClass="ddlPage"  Width="400px">
                                                </asp:DropDownList>
                                    </div>
                                </div>

                                <div class="form-group">
                                    <div class="col-md-8 pading5px asitCol8">
                                       <asp:Label ID="lblRptGroup" runat="server"  Font-Underline="False"  CssClass="lblName lblTxt" Text="Group :" ></asp:Label>

                                        <asp:DropDownList ID="ddlRptGroup" runat="server" CssClass="ddlPage">
                                                    <asp:ListItem>Main</asp:ListItem>
                                                    <asp:ListItem>Sub-1</asp:ListItem>
                                                    <asp:ListItem>Sub-2</asp:ListItem>
                                                    <asp:ListItem>Sub-3</asp:ListItem>
                                                    <asp:ListItem Selected="True">Details</asp:ListItem>
                                                </asp:DropDownList>
                                    </div>
                                </div>

                                   </div>
                                   </asp:Panel>

                                 <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" BackColor="#DDFFEE" CssClass=" table-striped table-hover table-bordered grvContentarea"

                                    ShowFooter="True" Width="1010px">
                                    <Columns>
                                        <asp:TemplateField HeaderStyle-Font-Size="12px" HeaderText="Code" Visible="False">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvcode1" runat="server" CssClass="GridLebel" Text='<%# DataBinder.Eval(Container.DataItem, "actcode").ToString() %>'></asp:Label></ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField FooterText="Total. &lt;br&gt; Net." FooterStyle-Font-Bold="true" FooterStyle-Font-Size="12px" FooterStyle-HorizontalAlign="Right"
                                            HeaderStyle-Font-Size="14px" HeaderText="         Resource  Description">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvdescryption" runat="server" CssClass="GridLebelL" Text='<%# DataBinder.Eval(Container.DataItem, "subdesc4").ToString() %>'
                                                    Width="320px"></asp:Label></ItemTemplate>
                                            <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-Font-Size="12px" HeaderText="Unit">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvUnit" runat="server" CssClass="GridLebelL" Text='<%# DataBinder.Eval(Container.DataItem, "sirunit").ToString() %>'
                                                    Width="40px"></asp:Label></ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField FooterStyle-HorizontalAlign="Right" HeaderStyle-Font-Size="12px"
                                            HeaderText="Op.Qty" ItemStyle-HorizontalAlign="Right">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvopqty" runat="server" CssClass="GridLebel" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "opnqty")).ToString("#,##0.00;(#,##0.00); ") %>'></asp:Label></ItemTemplate>
                                            <FooterStyle HorizontalAlign="Right" />
                                            <HeaderStyle Font-Size="12px" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                       
                                        <asp:TemplateField FooterStyle-HorizontalAlign="Right" HeaderStyle-Font-Size="12px"
                                            HeaderText="Op.Amt" ItemStyle-HorizontalAlign="Right">
                                            <FooterTemplate>
                                                 <table>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="lblfopamt" runat="server" CssClass="GridLebel"></asp:Label>
                                                        </td>
                                                        
                                                    </tr>
                                                    <tr>
                                                       
                                                        <td>
                                                            <asp:Label ID="Label3121" runat="server" CssClass="GridLebel">-</asp:Label>
                                                        </td>
                                                    </tr>
                                                
                                                </table>
                                               
                                              </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvOpnamt1" runat="server" CssClass="GridLebel" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "opnam")).ToString("#,##0;(#,##0); ") %>'></asp:Label></ItemTemplate>
                                            <FooterStyle HorizontalAlign="Right" />
                                            <HeaderStyle Font-Size="12px" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField FooterStyle-HorizontalAlign="Right" HeaderStyle-Font-Size="12px"
                                            HeaderText="Cu.Qty" ItemStyle-HorizontalAlign="Right">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvCuq" runat="server" CssClass="GridLebel" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "trnqty")).ToString("#,##0.00;(#,##0.00); ") %>'></asp:Label></ItemTemplate>
                                            <FooterStyle HorizontalAlign="Right" />
                                            <HeaderStyle Font-Size="12px" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                       
                                        <asp:TemplateField HeaderStyle-Font-Size="12px" HeaderText="Cu.Amt" ItemStyle-HorizontalAlign="Right">
                                            <FooterTemplate>
                                                <table>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="lblfcuamt" runat="server" CssClass="GridLebel"></asp:Label>
                                                        </td>
                                                        
                                                    </tr>
                                                    <tr>
                                                       
                                                        <td>
                                                            <asp:Label ID="Label1345" runat="server" CssClass="GridLebel">-</asp:Label>
                                                        </td>
                                                    </tr>
                                                
                                                </table>
                                             </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvCuam" runat="server" CssClass="GridLebel" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "trnam")).ToString("#,##0;(#,##0); ") %>'></asp:Label></ItemTemplate>
                                            <HeaderStyle Font-Size="12px" />
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle HorizontalAlign="right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-Font-Size="12px" HeaderText="Cl.Qty" ItemStyle-HorizontalAlign="Right">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvClq" runat="server" CssClass="GridLebel" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "closqty")).ToString("#,##0.00;(#,##0.00); ") %>'></asp:Label></ItemTemplate>
                                            <HeaderStyle Font-Size="12px" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-Font-Size="12px" HeaderText="Cl.Dr Amt" ItemStyle-HorizontalAlign="Right">
                                            <FooterTemplate>
                                                <table>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="lblfclDrAmt" runat="server" CssClass="GridLebel"></asp:Label>
                                                        </td>
                                                        
                                                    </tr>
                                                    <tr>
                                                       
                                                        <td>
                                                            <asp:Label ID="Label112" runat="server" CssClass="GridLebel">-</asp:Label>
                                                        </td>
                                                    </tr>
                                                
                                                </table>
                                                
                                                
                                                
                                                </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvClrDrAmt" runat="server" CssClass="GridLebel" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "closdramt")).ToString("#,##0;(#,##0); ") %>'></asp:Label></ItemTemplate>
                                            <HeaderStyle Font-Size="12px" />
                                            <ItemStyle HorizontalAlign="Right" />
                                             <FooterStyle HorizontalAlign="right" />
                                            
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-Font-Size="12px" HeaderText="Cl. Cr Amt" ItemStyle-HorizontalAlign="Right">
                                            <FooterTemplate>
                                                
                                                <table>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="lblfclCramt" runat="server" CssClass="GridLebel"></asp:Label>
                                                        </td>
                                                        
                                                    </tr>
                                                    <tr>
                                                       
                                                        <td>
                                                            <asp:Label ID="lblfclBalAmt" runat="server" CssClass="GridLebel"></asp:Label>
                                                        </td>
                                                    </tr>
                                                
                                                </table>

                                                </FooterTemplate>


                                            <ItemTemplate>
                                                <asp:Label ID="lblgvClCram" runat="server" CssClass="GridLebel" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "closcramt")).ToString("#,##0;(#,##0); ") %>'></asp:Label></ItemTemplate>
                                            <HeaderStyle Font-Size="12px" />
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle HorizontalAlign="right" />
                                        </asp:TemplateField>
                                    </Columns>
                                   <FooterStyle CssClass="grvFooter"/>
                                    <EditRowStyle />
                                    <AlternatingRowStyle />
                                    <PagerStyle CssClass="gvPagination" />
                                    <HeaderStyle CssClass="grvHeader" />

                                </asp:GridView>
                                           
                                 <asp:GridView ID="dgvPS" runat="server" AutoGenerateColumns="False" BackColor="#DDFFEE" CssClass=" table-striped table-hover table-bordered grvContentarea"

                                    ShowFooter="True" Width="1010px">
                                    <Columns>
                                        <asp:TemplateField HeaderStyle-Font-Size="12px" HeaderText="Code" Visible="False">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvcode1" runat="server" CssClass="GridLebel" Text='<%# DataBinder.Eval(Container.DataItem, "actcode").ToString() %>'></asp:Label></ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField FooterText="Total. &lt;br&gt; Net." FooterStyle-Font-Bold="true" FooterStyle-Font-Size="12px" FooterStyle-HorizontalAlign="Right"
                                            HeaderStyle-Font-Size="14px" HeaderText="         Resource  Description">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvdescryption" runat="server" CssClass="GridLebelL" Text='<%# DataBinder.Eval(Container.DataItem, "subdesc4").ToString() %>'
                                                    Width="320px"></asp:Label></ItemTemplate>
                                            <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-Font-Size="12px" HeaderText="Unit">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvUnit" runat="server" CssClass="GridLebelL" Text='<%# DataBinder.Eval(Container.DataItem, "sirunit").ToString() %>'
                                                    Width="40px"></asp:Label></ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField FooterStyle-HorizontalAlign="Right" HeaderStyle-Font-Size="12px"
                                            HeaderText="Op.Qty" ItemStyle-HorizontalAlign="Right">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvopqty" runat="server" CssClass="GridLebel" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "opnqty")).ToString("#,##0.00;(#,##0.00); ") %>'></asp:Label></ItemTemplate>
                                            <FooterStyle HorizontalAlign="Right" />
                                            <HeaderStyle Font-Size="12px" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                       
                                        <asp:TemplateField FooterStyle-HorizontalAlign="Right" HeaderStyle-Font-Size="12px"
                                            HeaderText="Op.Amt" ItemStyle-HorizontalAlign="Right">
                                            <FooterTemplate>
                                                 <table>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="lblfopamt" runat="server" CssClass="GridLebel"></asp:Label>
                                                        </td>
                                                        
                                                    </tr>
                                                    <tr>
                                                       
                                                        <td>
                                                            <asp:Label ID="Label3121" runat="server" CssClass="GridLebel">-</asp:Label>
                                                        </td>
                                                    </tr>
                                                
                                                </table>
                                               
                                              </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvOpnamt1" runat="server" CssClass="GridLebel" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "opnam")).ToString("#,##0;(#,##0); ") %>'></asp:Label></ItemTemplate>
                                            <FooterStyle HorizontalAlign="Right" />
                                            <HeaderStyle Font-Size="12px" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField FooterStyle-HorizontalAlign="Right" HeaderStyle-Font-Size="12px"
                                            HeaderText="Cu.Qty" ItemStyle-HorizontalAlign="Right">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvCuq" runat="server" CssClass="GridLebel" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "trnqty")).ToString("#,##0.00;(#,##0.00); ") %>'></asp:Label></ItemTemplate>
                                            <FooterStyle HorizontalAlign="Right" />
                                            <HeaderStyle Font-Size="12px" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                       
                                        <asp:TemplateField HeaderStyle-Font-Size="12px" HeaderText="Cu.Amt" ItemStyle-HorizontalAlign="Right">
                                            <FooterTemplate>
                                                <table>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="lblfcuamt" runat="server" CssClass="GridLebel"></asp:Label>
                                                        </td>
                                                        
                                                    </tr>
                                                    <tr>
                                                       
                                                        <td>
                                                            <asp:Label ID="Label1345" runat="server" CssClass="GridLebel">-</asp:Label>
                                                        </td>
                                                    </tr>
                                                
                                                </table>
                                             </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvCuam" runat="server" CssClass="GridLebel" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "trnam")).ToString("#,##0;(#,##0); ") %>'></asp:Label></ItemTemplate>
                                            <HeaderStyle Font-Size="12px" />
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle HorizontalAlign="right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-Font-Size="12px" HeaderText="Cl.Qty" ItemStyle-HorizontalAlign="Right">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvClq" runat="server" CssClass="GridLebel" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "closqty")).ToString("#,##0.00;(#,##0.00); ") %>'></asp:Label></ItemTemplate>
                                            <HeaderStyle Font-Size="12px" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-Font-Size="12px" HeaderText="Cl.Dr Amt" ItemStyle-HorizontalAlign="Right">
                                            <FooterTemplate>
                                                <table>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="lblfclDrAmt" runat="server" CssClass="GridLebel"></asp:Label>
                                                        </td>
                                                        
                                                    </tr>
                                                    <tr>
                                                       
                                                        <td>
                                                            <asp:Label ID="Label112" runat="server" CssClass="GridLebel">-</asp:Label>
                                                        </td>
                                                    </tr>
                                                
                                                </table>
                                                
                                                
                                                
                                                </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvClrDrAmt" runat="server" CssClass="GridLebel" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "closdramt")).ToString("#,##0;(#,##0); ") %>'></asp:Label></ItemTemplate>
                                            <HeaderStyle Font-Size="12px" />
                                            <ItemStyle HorizontalAlign="Right" />
                                             <FooterStyle HorizontalAlign="right" />
                                            
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-Font-Size="12px" HeaderText="Cl. Cr Amt" ItemStyle-HorizontalAlign="Right">
                                            <FooterTemplate>
                                                
                                                <table>
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="lblfclCramt" runat="server" CssClass="GridLebel"></asp:Label>
                                                        </td>
                                                        
                                                    </tr>
                                                    <tr>
                                                       
                                                        <td>
                                                            <asp:Label ID="lblfclBalAmt" runat="server" CssClass="GridLebel"></asp:Label>
                                                        </td>
                                                    </tr>
                                                
                                                </table>

                                                </FooterTemplate>


                                            <ItemTemplate>
                                                <asp:Label ID="lblgvClCram" runat="server" CssClass="GridLebel" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "closcramt")).ToString("#,##0;(#,##0); ") %>'></asp:Label></ItemTemplate>
                                            <HeaderStyle Font-Size="12px" />
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle HorizontalAlign="right" />
                                        </asp:TemplateField>
                                    </Columns>
                                   <FooterStyle CssClass="grvFooter"/>
                                    <EditRowStyle />
                                    <AlternatingRowStyle />
                                    <PagerStyle CssClass="gvPagination" />
                                    <HeaderStyle CssClass="grvHeader" />

                                </asp:GridView>
                               </asp:View>

                               <asp:View ID="BEView" runat="server">
                                    <asp:Panel ID="Panel2" runat="server">
                                         <div class="form-group">
                                       <div class="col-md-5 pading5px asitCol5">
                                        <asp:Label ID="Label15" runat="server" CssClass="lblTxt lblName" Text="Project Name:"></asp:Label>

                                        <asp:TextBox ID="txtSearchp" runat="server"  CssClass="inputtextbox"></asp:TextBox>
                                      
                                         <asp:LinkButton ID="ImgbtnFindProjI" CssClass="btn btn-primary srearchBtn" runat="server" OnClick="ImgbtnFindProjI_Click" TabIndex="2"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>

                                        <asp:DropDownList ID="ddlHAccProject" runat="server" AutoPostBack="True"  CssClass="ddlPage" OnSelectedIndexChanged="ddlAccProject_SelectedIndexChanged" Width="400px"> </asp:DropDownList>

                                         <asp:DropDownList ID="ddlRptGroupbve" runat="server"  CssClass="ddlPage">
                                                    <asp:ListItem>Main</asp:ListItem>
                                                    <asp:ListItem>Sub-1</asp:ListItem>
                                                    <asp:ListItem>Sub-2</asp:ListItem>
                                                    <asp:ListItem>Sub-3</asp:ListItem>
                                                    <asp:ListItem Selected="True">Details</asp:ListItem>
                                                </asp:DropDownList>
                                    </div>
                                </div>

                                    </asp:Panel>

                                   <asp:GridView ID="dgvBE" runat="server" AutoGenerateColumns="False" BackColor="#DDFFEE" CssClass=" table-striped table-hover table-bordered grvContentarea"

                                    ShowFooter="True" Width="850px">
                                    <Columns>
                                        <asp:TemplateField HeaderStyle-Font-Size="12px" HeaderText="Code" Visible="False">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvcode2" runat="server" CssClass="GridLebel" Text='<%# DataBinder.Eval(Container.DataItem, "actcode4").ToString() %>'></asp:Label></ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField FooterStyle-Font-Bold="true" FooterStyle-Font-Size="12px" FooterStyle-HorizontalAlign="Right"
                                            HeaderStyle-Font-Size="12px" HeaderText="Description of Account">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvdescryption0" runat="server" CssClass="GridLebelL" Text='<%# DataBinder.Eval(Container.DataItem, "subdesc1").ToString() %>'
                                                    Width="300px"></asp:Label></ItemTemplate>
                                            <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-Font-Size="12px" HeaderText="Unit">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvUnitbe" runat="server" CssClass="GridLebelL" Text='<%# DataBinder.Eval(Container.DataItem, "sirunit").ToString() %>'
                                                    Width="40px"></asp:Label></ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-Font-Size="12px" HeaderText="Budget Qty" ItemStyle-HorizontalAlign="Right">
                                            <ItemTemplate>
                                                <asp:Label ID="lbgdqty" runat="server" CssClass="GridLebel" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "bgdqty")).ToString("#,##0.00;(#,##0.00); ") %>'></asp:Label></ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lblftbgdqty" runat="server" CssClass="GridLebel"></asp:Label></FooterTemplate>
                                            <HeaderStyle Font-Size="12px" />
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Bgd. Rate" HeaderStyle-Font-Size="12px">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvBgRate" runat="server" CssClass="GridLebel" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "bgdrat")).ToString("#,##0;(#,##0); ") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderStyle-Font-Size="12px" HeaderText="Budget Amt" ItemStyle-HorizontalAlign="Right">
                                            <FooterTemplate>
                                                <asp:Label ID="lblfbgdam" runat="server" CssClass="GridLebel"></asp:Label></FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvbgdam" runat="server" CssClass="GridLebel" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "bgdam")).ToString("#,##0;(#,##0); ") %>'></asp:Label></ItemTemplate>
                                            <HeaderStyle Font-Size="12px" />
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Actual Qty" HeaderStyle-Font-Size="12px">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvToqty" runat="server" CssClass="GridLebel" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "closqty")).ToString("#,##0.00;(#,##0.00); ") %>'></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lblftToQty" runat="server" CssClass="GridLebel"></asp:Label>
                                            </FooterTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Actual Rate" HeaderStyle-Font-Size="12px">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvRate" runat="server" CssClass="GridLebel" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "closrate")).ToString("#,##0;(#,##0); ") %>'></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderStyle-Font-Size="12px" HeaderText="Actual Amt" ItemStyle-HorizontalAlign="Right">
                                            <FooterTemplate>
                                                <asp:Label ID="lblftoamt" runat="server" CssClass="GridLebel"></asp:Label></FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvClam0" runat="server" CssClass="GridLebel" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "closam")).ToString("#,##0;(#,##0); ") %>'></asp:Label></ItemTemplate>
                                            <HeaderStyle Font-Size="12px" />
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-Font-Size="12px" HeaderText="Avail. Qty" ItemStyle-HorizontalAlign="Right">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvavqty" runat="server" CssClass="GridLebel" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "tavqty")).ToString("#,##0.00;(#,##0.00); ") %>'></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lblftavqty" runat="server" CssClass="GridLebel"></asp:Label></FooterTemplate>
                                            <HeaderStyle Font-Size="12px" />
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderStyle-Font-Size="12px" HeaderText="Avail.Rate" ItemStyle-HorizontalAlign="Right">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvavrat" runat="server" CssClass="GridLebel" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "tavrat")).ToString("#,##0;(#,##0); ") %>'></asp:Label>
                                            </ItemTemplate>
                                           
                                            <HeaderStyle Font-Size="12px" />
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="Available.Amt" HeaderStyle-Font-Size="12px">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvAamt" runat="server" CssClass="GridLebel" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "tavamt")).ToString("#,##0;(#,##0); ") %>'></asp:Label></ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lblftAvAmt" runat="server" CssClass="GridLebel"></asp:Label>
                                            </FooterTemplate>
                                            <HeaderStyle HorizontalAlign="Center" Width="12px" />
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="%">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvPer" runat="server" CssClass="GridLebel" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "taper")).ToString("#,##0.00;(#,##0.00); ") %>'></asp:Label></ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                    </Columns>
                               <FooterStyle CssClass="grvFooter"/>
                                    <EditRowStyle />
                                    <AlternatingRowStyle />
                                    <PagerStyle CssClass="gvPagination" />
                                    <HeaderStyle CssClass="grvHeader" />


                                </asp:GridView>
                               </asp:View>

                               <asp:View ID="SpcCode" runat="server">
                                    <asp:Panel ID="Panel3" runat="server">
                                      <div class="form-group">
                                       <div class="col-md-5 pading5px asitCol5">
                                        <asp:Label ID="Label20" runat="server" CssClass="lblTxt lblName" Text="Project Name:"></asp:Label>

                                        <asp:TextBox ID="txtSearchpSpc" runat="server"  CssClass="inputtextbox"></asp:TextBox>
                                      
                                         <asp:LinkButton ID="ImgbtnFindProjSpc" CssClass="btn btn-primary srearchBtn" runat="server" OnClick="ImgbtnFindProjSpc_Click" TabIndex="2"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>

                                        <asp:DropDownList ID="ddlProject" runat="server" AutoPostBack="True"  CssClass="ddlPage" Width="400px"> </asp:DropDownList>

                                       
                                    </div>
                                </div>
                                    </asp:Panel>

                                    <asp:GridView ID="dgvSPC" runat="server" AutoGenerateColumns="False" BackColor="#FFECEC" CssClass=" table-striped table-hover table-bordered grvContentarea"

                                    BorderColor="#66CCFF" BorderStyle="Solid" BorderWidth="3px" ShowFooter="True"
                                    Width="753px">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Code">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvcode5" runat="server" CssClass="GridLebelL" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rescode")) %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Description Of Resourec">
                                            <FooterTemplate>
                                                <asp:Label ID="lblgvfTotal" runat="server" Font-Bold="True" Font-Size="12px" 
                                                    Text="Total"></asp:Label>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvResDesc" runat="server" CssClass="GridLebelL" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "resdesc")) %>'
                                                    Width="250px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Specifition">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvspcdesc" runat="server" CssClass="GridLebelL" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "spcldesc")) %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Qty" ItemStyle-HorizontalAlign="Right">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvqty" runat="server" CssClass="GridLebel" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "trnqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Rate" ItemStyle-HorizontalAlign="Right">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvRate" runat="server" CssClass="GridLebel" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "trnrate")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Amount" ItemStyle-HorizontalAlign="Right">
                                            <FooterTemplate>
                                                <asp:Label ID="lblgvfamount" runat="server" CssClass="GridLebel" Font-Bold="True"
                                                    Font-Size="11px" Width="100px"></asp:Label>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvamount" runat="server" CssClass="GridLebel" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "trnam")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="100px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                    </Columns>
                                   <FooterStyle CssClass="grvFooter"/>
                                <EditRowStyle />
                                <AlternatingRowStyle />
                                <PagerStyle CssClass="gvPagination" />
                                <HeaderStyle CssClass="grvHeader" />

                                </asp:GridView>

                               </asp:View>

                              <asp:View ID="InComeInd" runat="server">
                                    <div class="form-group">
                                       <div class="col-md-5 pading5px asitCol5">
                                        <asp:Label ID="Label21" runat="server" CssClass="lblTxt lblName" Text="Project Name:"></asp:Label>

                                        <asp:TextBox ID="txtSearchpIndp" runat="server"  CssClass="inputtextbox"></asp:TextBox>
                                      
                                         <asp:LinkButton ID="ImgbtnFindProjind" CssClass="btn btn-primary srearchBtn" runat="server" OnClick="ImgbtnFindProjind_Click" TabIndex="2"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>

                                        <asp:DropDownList ID="ddlProjectInd" runat="server" AutoPostBack="True"  CssClass="ddlPage" Width="400px"> </asp:DropDownList>

                                       <asp:RadioButtonList ID="rbtnList1" runat="server"  CssClass="radio" RepeatColumns="6" RepeatDirection="Horizontal" Width="190px">
                                    <asp:ListItem>Details</asp:ListItem>
                                    <asp:ListItem>Summery</asp:ListItem>
                                </asp:RadioButtonList>
                                    </div>
                                </div>

                                   <asp:GridView ID="gvIncome" runat="server" AutoGenerateColumns="False" OnRowDataBound="gvIncome_RowDataBound" CssClass=" table-striped table-hover table-bordered grvContentarea"

                                    ShowFooter="True" Width="758px">
                                   
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sl.No.">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvSlNo" runat="server" Font-Bold="True" Height="16px" Style="text-align: right"
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="50px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText=" Description">
                                            <ItemTemplate>
                                                <asp:Label ID="lgcActDesc" runat="server" Text='<%# "<B>"+ Convert.ToString(DataBinder.Eval(Container.DataItem, "grpdesc")) + "</B>"+
                                                                         (DataBinder.Eval(Container.DataItem, "resdesc").ToString().Trim().Length>0 ? 
                                                                         (Convert.ToString(DataBinder.Eval(Container.DataItem, "grpdesc")).Trim().Length>0 ?  "<br>" : "")+                                                             
                                                                         "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"+ 
                                                                         Convert.ToString(DataBinder.Eval(Container.DataItem, "resdesc")).Trim(): "") 
                                                                         
                                                                    %>' Width="250px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Unit ">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvUnit" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "resunit")) %>'
                                                    Width="40px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Qty">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvqty" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "trnqty")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="75px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Rate">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvRate" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "trnrate")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="75px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Amount">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvAmt" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "trnam")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="75px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFTAmt" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#000"
                                                    Style="text-align: right" Width="75px"></asp:Label>
                                            </FooterTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                    </Columns>
                                   <FooterStyle CssClass="grvFooter"/>
                                    <EditRowStyle />
                                    <AlternatingRowStyle />
                                    <PagerStyle CssClass="gvPagination" />
                                    <HeaderStyle CssClass="grvHeader" />

                                </asp:GridView>

                                    <asp:GridView ID="gvInfast" runat="server" AutoGenerateColumns="False" ShowFooter="True" CssClass=" table-striped table-hover table-bordered grvContentarea"

                                    Width="758px">
                                 
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sl.No.">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvSlNo0" runat="server" Font-Bold="True" Height="16px" Style="text-align: right"
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="50px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Description of Item">
                                            <ItemTemplate>
                                                <asp:Label ID="lgcResDesc" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "resdesc")) %>'
                                                    Width="170px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Unit ">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvUnit" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "resunit")) %>'
                                                    Width="40px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Qty">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvqty" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "trnqty")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="75px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Rate">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvRate" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "trnrate")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="75px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Amount">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvAmt" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "trnam")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="75px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lgvFTfAmt" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#000"
                                                    Style="text-align: right" Width="75px"></asp:Label>
                                            </FooterTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                            <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                    </Columns>
                                   <FooterStyle CssClass="grvFooter"/>
                                    <EditRowStyle />
                                    <AlternatingRowStyle />
                                    <PagerStyle CssClass="gvPagination" />
                                    <HeaderStyle CssClass="grvHeader" />

                                </asp:GridView>

                              </asp:View>

                             <asp:View ID="SPBalSheet" runat="server">

                                  <div class="form-group">
                                       <div class="col-md-5 pading5px asitCol5">
                                        <asp:Label ID="Label22" runat="server" CssClass="lblTxt lblName"  Text="Balance Sheet" Visible="False"></asp:Label>

                                    </div>
                                </div>

                                  <asp:GridView ID="dgvSpBS" runat="server" AutoGenerateColumns="False" BackColor="#FFECEC" CssClass=" table-striped table-hover table-bordered grvContentarea"

                                    BorderColor="#66CCFF" BorderStyle="Solid" BorderWidth="3px" OnRowDataBound="dgvSpBS_RowDataBound"
                                    Width="640px">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Description Of accounts">
                                            <ItemTemplate>
                                                <asp:Label ID="lblDescription" runat="server" CssClass="GridLebelL" Text='<%# "<B>"+ Convert.ToString(DataBinder.Eval(Container.DataItem, "grpdesc")) + "</B>"+
                                                                         (DataBinder.Eval(Container.DataItem, "gendesc").ToString().Trim().Length>0 ? 
                                                                         (Convert.ToString(DataBinder.Eval(Container.DataItem, "grpdesc")).Trim().Length>0 ? "<br>" : "") + 
                                                                         "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + 
                                                                         Convert.ToString(DataBinder.Eval(Container.DataItem, "gendesc")).Trim(): "") 
                                                                          %>'>

                                                            Width="250px" CssClass="GridLebelL"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Total" ItemStyle-HorizontalAlign="Right">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvclosamt" runat="server" CssClass="GridLebel" Font-Size="10px"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "closam")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="100px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Opening" ItemStyle-HorizontalAlign="Right">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvopnamt" runat="server" CssClass="GridLebel" Font-Size="10px"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "opnam")).ToString("#,##0;(#,##0); ") %>'
                                                    Width="100px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                    </Columns>
                                    <FooterStyle CssClass="grvFooter"/>
                                    <EditRowStyle />
                                    <AlternatingRowStyle />
                                    <PagerStyle CssClass="gvPagination" />
                                    <HeaderStyle CssClass="grvHeader" />

                                </asp:GridView>

                             </asp:View>



                         </asp:MultiView>
                     </div>
                     </div>
                </div>

                <%--<tr>
                    <td class="style76">
                        &nbsp;
                    </td>
                    <td class="style61">
                        <asp:Label ID="lblDatefrom" runat="server" Font-Bold="True" Font-Size="14px" Height="16px"
                            Style="text-align: right; color: #FFFFFF;" Text="Date From:" Width="120px"></asp:Label>
                    </td>
                    <td class="style62">
                        <asp:Label ID="lblfrmdate" runat="server" BackColor="#000066" 
                            BorderColor="Yellow" BorderStyle="Solid" BorderWidth="1px" Font-Bold="True" 
                            Font-Size="12px" ForeColor="Yellow" Width="80px"></asp:Label>
                    </td>
                    <td class="style63">
                        <asp:Label ID="lbldateto" runat="server" Font-Bold="True" Font-Size="14px" Height="16px"
                            Style="text-align: right; color: #FFFFFF;" Text="To" Width="20px"></asp:Label>
                    </td>
                    <td class="style75">
                        <asp:Label ID="lbltodate" runat="server" BackColor="#000066" 
                            BorderColor="Yellow" BorderStyle="Solid" BorderWidth="1px" Font-Bold="True" 
                            Font-Size="12px" ForeColor="Yellow" Width="80px"></asp:Label>
                    </td>
                    <td class="style63">
                        &nbsp;
                    </td>
                    <td class="style66">
                        <asp:Label ID="lblrptlbl" runat="server" Font-Bold="True" Font-Size="14px" Height="16px"
                            Style="text-align: right; color: #FFFFFF;" Text="Report Level:" Width="100px"></asp:Label>
                    </td>
                    <td class="style67">
                        <asp:DropDownList ID="DDListLevels" runat="server" Font-Bold="True" Font-Size="14px"
                            Width="90px" OnSelectedIndexChanged="DDListLevels_SelectedIndexChanged">
                            <asp:ListItem>level-1</asp:ListItem>
                            <asp:ListItem>level-2</asp:ListItem>
                            <asp:ListItem>level-3</asp:ListItem>
                            <asp:ListItem Selected="True">level-4</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td class="style68">
                        <asp:LinkButton ID="lbtnOk" runat="server" Font-Bold="True" Font-Size="16px" Height="23px"
                            OnClick="lbtnOk_Click" Style="text-align: center; background-color: #99FFCC"
                            Width="52px">Ok</asp:LinkButton>
                    </td>
                    <td>
                        <asp:CheckBox ID="ChkTopHead" runat="server" Checked="True" Font-Bold="True" Style="font-size: 14px;
                            color: #FFFFFF;" Text="Print top heads" Width="120px" OnCheckedChanged="ChkTopHead_CheckedChanged" />
                    </td>
                </tr>--%>
                <%--<tr>
                    <td class="style76">
                        &nbsp;</td>
                    <td class="style61">
                        <asp:Label ID="lblOpeningDate" runat="server" Font-Bold="True" Font-Size="14px" 
                            Height="16px" Style="text-align: right; color: #FFFFFF;" Text="Opening Date:" 
                            Visible="False" Width="120px"></asp:Label>
                    </td>
                    <td class="style62">
                        <asp:TextBox ID="txtOpeningDate" runat="server" BorderStyle="None" 
                            Visible="False" Width="80px"></asp:TextBox>
                        <cc1:CalendarExtender ID="txtOpeningDate_CalendarExtender" runat="server" 
                            Format="dd-MMM-yyyy " TargetControlID="txtOpeningDate">
                        </cc1:CalendarExtender>
                    </td>
                    <td class="style63">
                        &nbsp;</td>
                    <td class="style75">
                        &nbsp;</td>
                    <td class="style63">
                        &nbsp;</td>
                    <td class="style66">
                        &nbsp;</td>
                    <td class="style67">
                        &nbsp;</td>
                    <td class="style68">
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                </tr>--%>
                <%--<tr>
                            <td>
                                <asp:Label ID="Label10" runat="server" Font-Bold="True" Font-Size="14px" Height="16px"
                                    Style="text-align: right; color: #FFFFFF;" Text="Income Statement" Width="120px"
                                    Visible="False"></asp:Label>
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>--%>
                <%--<td>
                                <asp:Label ID="Label11" runat="server" Font-Bold="True" Font-Size="14px" Height="16px"
                                    Style="text-align: right; color: #FFFFFF;" Text="Balance Sheet" Width="120px"
                                    Visible="False"></asp:Label>
                            </td>--%>
                <%--<tr>
                                            <td>
                                                &nbsp;
                                            </td>
                                            <td class="style88">
                                                <asp:Label ID="Label13" runat="server" Font-Bold="True" Font-Size="14px" Height="16px"
                                                    Style="text-align: right; color: #FFFFFF;" Text="Project Name:" Width="120px"></asp:Label>
                                            </td>
                                            <td class="style89">
                                                <asp:TextBox ID="txtSearch" runat="server" Style="border-style: solid; border-width: 1px"
                                                    Width="80px"></asp:TextBox>
                                            </td>
                                            <td class="style90">
                                                <asp:ImageButton ID="ImgbtnFindProj" runat="server" Height="19px" ImageUrl="~/Image/find_images.jpg"
                                                    OnClick="ImgbtnFindProj_Click" Width="21px" />
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlAccProject" runat="server" AutoPostBack="True" Font-Size="12px"
                                                    OnSelectedIndexChanged="ddlAccProject_SelectedIndexChanged" Width="400px">
                                                </asp:DropDownList>
                                            </td>
                                      
                                        <tr>
                                            <td>
                                                &nbsp;
                                            </td>
                                            <td class="style88">
                                                <asp:Label ID="Label14" runat="server" Font-Bold="True" Font-Size="14px" Height="16px"
                                                    Style="text-align: right; color: #FFFFFF;" Text="Resource Name:" Width="120px"></asp:Label>
                                            </td>
                                            <td class="style89">
                                                <asp:TextBox ID="txtSrcRes" runat="server" Style="border-style: solid; border-width: 1px"
                                                    Width="80px"></asp:TextBox>
                                            </td>
                                            <td class="style90">
                                                <asp:ImageButton ID="ImgbtnFindRes" runat="server" Height="19px" ImageUrl="~/Image/find_images.jpg"
                                                    OnClick="ImgbtnFindRes_Click" Width="21px" />
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlResHead" runat="server" Font-Size="12px" Width="400px">
                                                </asp:DropDownList>
                                            </td>
                                          
                                        </tr>
                                        <tr>
                                            <td>
                                                &nbsp;
                                            </td>
                                            <td class="style88">
                                                <asp:Label ID="lblRptGroup" runat="server" CssClass="style27" Font-Size="12px" Font-Underline="False"
                                                    Style="font-weight: 700; text-align: right" Text="Group :" Width="120px"></asp:Label>
                                            </td>
                                            <td class="style89">
                                                <asp:DropDownList ID="ddlRptGroup" runat="server" Font-Bold="True" Font-Size="12px"
                                                    Height="21px" Style="text-transform: capitalize" Width="80px">
                                                    <asp:ListItem>Main</asp:ListItem>
                                                    <asp:ListItem>Sub-1</asp:ListItem>
                                                    <asp:ListItem>Sub-2</asp:ListItem>
                                                    <asp:ListItem>Sub-3</asp:ListItem>
                                                    <asp:ListItem Selected="True">Details</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                         
                                        </tr>
                                    --%>
                <%--<tr>
                                            <td>
                                                &nbsp;
                                            </td>
                                            <td class="style91">
                                                <asp:Label ID="Label15" runat="server" Font-Bold="True" Font-Size="14px" Height="16px"
                                                    Style="text-align: right; color: #FFFFFF;" Text="Project Name:" Width="120px"></asp:Label>
                                            </td>
                                            <td class="style92">
                                                <asp:TextBox ID="txtSearchp" runat="server" Style="border-style: solid; border-width: 1px"
                                                    Width="80px"></asp:TextBox>
                                            </td>
                                            <td class="style93">
                                                <asp:ImageButton ID="ImgbtnFindProjI" runat="server" Height="19px" ImageUrl="~/Image/find_images.jpg"
                                                    OnClick="ImgbtnFindProjI_Click" Width="21px" />
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlHAccProject" runat="server" AutoPostBack="True" Font-Size="12px"
                                                    OnSelectedIndexChanged="ddlAccProject_SelectedIndexChanged" Width="400px">
                                                </asp:DropDownList>
                                                <cc1:ListSearchExtender ID="lseP" runat="server" QueryPattern="Contains" TargetControlID="ddlHAccProject">
                                                </cc1:ListSearchExtender>
                                            </td>
                                          
                                            <td class="style91">
                                                <asp:Label ID="lblRptGroup0" runat="server" CssClass="style27" Font-Size="12px" Font-Underline="False"
                                                    Style="font-weight: 700; text-align: right" Text="Group :" Width="120px"></asp:Label>
                                            </td>
                                            <td class="style92">
                                                <asp:DropDownList ID="ddlRptGroupbve" runat="server" Font-Bold="True" Font-Size="12px"
                                                    Height="21px" Style="text-transform: capitalize" Width="80px">
                                                    <asp:ListItem>Main</asp:ListItem>
                                                    <asp:ListItem>Sub-1</asp:ListItem>
                                                    <asp:ListItem>Sub-2</asp:ListItem>
                                                    <asp:ListItem>Sub-3</asp:ListItem>
                                                    <asp:ListItem Selected="True">Details</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                           
                                </asp:Panel>
                            --%>
                <%--<tr>
                                            <td>
                                                &nbsp;
                                            </td>
                                            <td class="style94">
                                                <asp:Label ID="Label20" runat="server" Font-Bold="True" Font-Size="14px" Height="16px"
                                                    Style="text-align: right; color: #FFFFFF;" Text="Project Name:" Width="120px"></asp:Label>
                                            </td>
                                            <td class="style95">
                                                <asp:TextBox ID="txtSearchpSpc" runat="server" Style="border-style: solid; border-width: 1px"
                                                    Width="100px"></asp:TextBox>
                                            </td>
                                            <td class="style96">
                                                <asp:ImageButton ID="ImgbtnFindProjSpc" runat="server" Height="19px" ImageUrl="~/Image/find_images.jpg"
                                                    OnClick="ImgbtnFindProjSpc_Click" Width="21px" />
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlProject" runat="server" AutoPostBack="True" Font-Size="12px"
                                                    Width="400px">
                                                </asp:DropDownList>
                                                <cc1:ListSearchExtender ID="ddlProject_ListSearchExtender" runat="server" QueryPattern="Contains"
                                                    TargetControlID="ddlProject">
                                                </cc1:ListSearchExtender>
                                            </td>
                                        
                                </asp:Panel>
                            --%>
                               
                            <%--<td class="style87">
                                <asp:Label ID="Label21" runat="server" Font-Bold="True" Font-Size="14px" Height="16px"
                                    Style="text-align: right; color: #FFFFFF;" Text="Project Name:" Width="120px"></asp:Label>
                            </td>--%>
                            <%--<td class="style98">
                                <asp:TextBox ID="txtSearchpIndp" runat="server" Style="border-style: solid; border-width: 1px"
                                    Width="80px"></asp:TextBox>
                            </td>--%>
                            <%--<td class="style63">
                                <asp:ImageButton ID="ImgbtnFindProjind" runat="server" Height="19px" ImageUrl="~/Image/find_images.jpg"
                                    OnClick="ImgbtnFindProjind_Click" Width="21px" />
                            </td>--%>
                            <%--<td>
                                <asp:DropDownList ID="ddlProjectInd" runat="server" AutoPostBack="True" Font-Size="12px"
                                    Width="400px">
                                </asp:DropDownList>
                                <cc1:ListSearchExtender ID="ddlProjectInd_ListSearchExtender" runat="server" QueryPattern="Contains"
                                    TargetControlID="ddlProjectInd">
                                </cc1:ListSearchExtender>
                            </td>--%>
                            <%--<td>
                                <asp:RadioButtonList ID="rbtnList1" runat="server" BackColor="#BBBB99" BorderColor="#FFCC00"
                                    BorderStyle="Solid" BorderWidth="1px" Font-Bold="True" Font-Size="14px" Height="14px"
                                    RepeatColumns="6" RepeatDirection="Horizontal" Width="190px">
                                    <asp:ListItem>Details</asp:ListItem>
                                    <asp:ListItem>Summery</asp:ListItem>
                                </asp:RadioButtonList>
                            </td>--%>
                            <%--<td>
                                <asp:Label ID="Label22" runat="server" Font-Bold="True" Font-Size="14px" Height="16px"
                                    Style="text-align: right; color: #FFFFFF;" Text="Balance Sheet" Visible="False"
                                    Width="120px"></asp:Label>
                            </td>--%>

        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

