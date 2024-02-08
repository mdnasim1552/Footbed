<%@ Page Title="" Language="C#" MasterPageFile="~/SPEMaster.Master" AutoEventWireup="true" CodeBehind="LinkGrpLCInfoataglance.aspx.cs" Inherits="SPEWEB.F_35_GrAcc.LinkGrpLCInfoataglance" %>

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
                              <asp:Panel ID="panel11" runat="server">
                                       <div class="form-group">
                                              <div class="col-md-10 asitCol10 pading5px">

                                             <asp:Label ID="Label21" runat="server" CssClass=" lblName lblTxt" Text="Project Name:"></asp:Label>

                                             <asp:TextBox ID="txtSearchpIndp" runat="server"  CssClass="inputtextbox"></asp:TextBox>

                                             <asp:LinkButton ID="ImgbtnFindProjind" CssClass="btn btn-primary srearchBtn" runat="server" OnClick="ImgbtnFindProjind_Click" TabIndex="2"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>

                                               <asp:DropDownList ID="ddlProjectInd" runat="server" CssClass="ddlPage"  AutoPostBack="True" TabIndex="18" Width="372"> </asp:DropDownList>

                                                  <asp:LinkButton ID="lbtnOk" runat="server" CssClass="btn btn-primary primaryBtn" OnClick="lbtnOk_Click"   TabIndex="8">Ok</asp:LinkButton>
                                                </div>
                                            </div>

                                   <div class="form-group">
                                              <div class="col-md-7 asitCol7 pading5px">

                                             <asp:Label ID="Label10" runat="server" CssClass=" lblName lblTxt" Text="As on:"></asp:Label>

                                             <asp:Label ID="lblasondate" runat="server" CssClass=" inputtextbox"></asp:Label>
                                                                                          
                                                </div>
                                            </div>
                                           </asp:Panel>
                                 <div class="form-group">
                                              <div class="col-md-7 asitCol7 pading5px">
                                   <asp:Label ID="lblOrderDetails" runat="server" CssClass="lblName lblTxt" Text="A. Order Details" Visible="False" 
                                Width="300px"></asp:Label>
                                                                                          
                                                </div>
                                            </div>

                                 </div>
                            </fieldset>
                        </div>
                     <div class="table table-responsive">
                             <asp:GridView ID="gvOrDer" runat="server" AutoGenerateColumns="False" CssClass=" table-striped table-hover table-bordered grvContentarea"
                                ShowFooter="True" Width="347px">
                              
                                <Columns>
                                    <asp:TemplateField HeaderText="Sl.No.">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvSlNo" runat="server" Font-Bold="True" 
                                                Style="text-align: right" 
                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="50px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Style Name">
                                        <FooterTemplate>
                                            <asp:Label ID="lgvFtotalord" runat="server" Font-Bold="True" Font-Size="12px" 
                                                ForeColor="#000" Width="70px" Text="Total"></asp:Label>
                                        </FooterTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvressdescord" runat="server"
                                                Style="text-align: left" 
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "resdesc")) %>' 
                                                Width="90px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Unit">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvunitord" runat="server"
                                                Style="text-align: left" 
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "resunit")) %>' 
                                                Width="90px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>


                                    
                                    <asp:TemplateField HeaderText="Order Date">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvorderqtyord" runat="server" Style="text-align: left" 
                                                Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "ordrdate")).ToString("dd-MMM-yyyy") %>' 
                                                Width="80px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="left" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Order Qty">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvorderqtyord" runat="server" Style="text-align: right" 
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ordrqty")).ToString("#,##0;(#,##0); ") %>' 
                                                Width="80px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Order Rate(FC)">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvorderrateord" runat="server" Style="text-align: right" 
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ordrrate")).ToString("#,##0.00;(#,##0.00); ") %>' 
                                                Width="60px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Order Amount">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvorderamtord" runat="server" Style="text-align: right" 
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ordramt")).ToString("#,##0;(#,##0); ") %>' 
                                                Width="80px"></asp:Label>
                                        </ItemTemplate>

                                        <FooterTemplate>
                                            <asp:Label ID="lgvFordramtord" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#000"
                                                Width="70px"></asp:Label>
                                        </FooterTemplate>

                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                   
                                </Columns>
                              <FooterStyle CssClass="grvFooter"/>
                                <EditRowStyle />
                                <AlternatingRowStyle />
                                <PagerStyle CssClass="gvPagination" />
                                <HeaderStyle CssClass="grvHeader" />

                            </asp:GridView>

                             <asp:GridView ID="gvbblcst" runat="server" AutoGenerateColumns="False" CssClass=" table-striped table-hover table-bordered grvContentarea"
                                ShowFooter="True" onrowdatabound="gvbblcst_RowDataBound">
                               
                                <Columns>
                                    <asp:TemplateField HeaderText="Sl.No.">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvSlNo0" runat="server" Font-Bold="True"
                                                Style="text-align: right" 
                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="50px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="BBLC Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvressdescbblc" runat="server"
                                                Style="text-align: left" 
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "resdesc")) %>' 
                                                Width="120px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>

                                     <asp:TemplateField HeaderText="Order Date">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvbblcdate" runat="server"
                                                Style="text-align: left" 
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "ordrdat")) %>' 
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>
                                         <ItemStyle HorizontalAlign="left" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                   
                                    <asp:TemplateField HeaderText="Order Amount">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvorderamtbblc" runat="server" Style="text-align: right" 
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ordramt")).ToString("#,##0;(#,##0); ") %>' 
                                                Width="80px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                           <FooterStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Received Amount">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvrcvamtbblc" runat="server" Style="text-align: right" 
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "rcvamt")).ToString("#,##0;(#,##0); ") %>' 
                                                Width="80px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                           <FooterStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                   
                                </Columns>
                              <FooterStyle CssClass="grvFooter"/>
                                <EditRowStyle />
                                <AlternatingRowStyle />
                                <PagerStyle CssClass="gvPagination" />
                                <HeaderStyle CssClass="grvHeader" />

                            </asp:GridView>

                          <asp:GridView ID="gvproStatus" runat="server" AutoGenerateColumns="False" CssClass=" table-striped table-hover table-bordered grvContentarea"
                                ShowFooter="True" onrowdatabound="gvproStatus_RowDataBound">
                               
                                <Columns>
                                    <asp:TemplateField HeaderText="Sl.No.">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvSlNo4" runat="server" Font-Bold="True" 
                                                Style="text-align: right" 
                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="50px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Description">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvgrpdesc" runat="server" Style="text-align: left" 
                                   
                                                
                                                  Text='<%# "<B>"+ Convert.ToString(DataBinder.Eval(Container.DataItem, "mgrpdesc")) + "</B>"+
                                                (DataBinder.Eval(Container.DataItem, "stepdesc").ToString().Trim().Length>0 ? 
                                                (Convert.ToString(DataBinder.Eval(Container.DataItem, "mgrpdesc")).Trim().Length>0 ? "<br>" : "") + 
                                                "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"+ Convert.ToString(DataBinder.Eval(Container.DataItem, "stepdesc")).Trim(): "") %>' 
                                                
                                                
                                                
                                                Width="120px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lgvFtotalgrpdesc" runat="server" Font-Bold="True" 
                                                Font-Size="12px" ForeColor="#000" Text="Total" Width="70px"></asp:Label>
                                        </FooterTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Qty">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvproqty" runat="server" Style="text-align: right" 
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty")).ToString("#,##0;(#,##0); ") %>' 
                                                Width="80px"></asp:Label>
                                        </ItemTemplate>
                                      
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Percentage">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvpercentage" runat="server" Style="text-align: right" 
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "percnt")).ToString("#,##0.00;(#,##0.00); ") %>' 
                                                Width="80px"></asp:Label>
                                        </ItemTemplate>
                                       
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
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
                              
                                 <div class="form-group">
                                  <div class="col-md-7 asitCol7 pading5px">
                                   <asp:Label ID="lblBBlCStatus" runat="server" CssClass="lblName lblTxt" Text="B. BBLC Status" Visible="False"  Width="300px" ></asp:Label>
                                                                                          
                                         </div>
                                      </div>
                                 </div>
                            </fieldset>

                           <fieldset class="scheduler-border fieldset_A">
                            <div class="form-horizontal">
                              
                                 <div class="form-group">
                                  <div class="col-md-7 asitCol7 pading5px">
                                   <asp:Label ID="lblProStatus" runat="server" CssClass="lblName lblTxt" Text="C.  Production Status"   Visible="False" Width="300px"></asp:Label>
                                                                                          
                                         </div>
                                      </div>
                                 </div>
                            </fieldset>

                          <fieldset class="scheduler-border fieldset_A">
                            <div class="form-horizontal">
                              
                                 <div class="form-group">
                                  <div class="col-md-7 asitCol7 pading5px">
                                   <asp:Label ID="lblOrdproVsShip" runat="server" CssClass="lblName lblTxt" Text="C. Order, Production Vs. Shipment"    Visible="False" Width="300px"></asp:Label>
                                                                                          
                                         </div>
                                      </div>
                                 </div>
                            </fieldset>

                          <asp:GridView ID="gvOrdProVsShip" runat="server" AutoGenerateColumns="False" CssClass=" table-striped table-hover table-bordered grvContentarea"
                                ShowFooter="True">
                                
                                <Columns>
                                    <asp:TemplateField HeaderText="Sl.No.">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvSlNo1" runat="server" Font-Bold="True"
                                                Style="text-align: right" 
                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="50px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Style Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvressdescopvss" runat="server"
                                                Style="text-align: left" 
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "resdesc")) %>' 
                                                Width="90px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Unit">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvunitopvss" runat="server" 
                                                Style="text-align: right" 
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "resunit")) %>' 
                                                Width="90px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Order Qty">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvorderqtyopvss" runat="server" Style="text-align: right" 
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ordrqty")).ToString("#,##0;(#,##0); ") %>' 
                                                Width="80px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>



                                     <asp:TemplateField HeaderText="Production Qty">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvproqtyopvss" runat="server" Style="text-align: right" 
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "proqty")).ToString("#,##0;(#,##0); ") %>' 
                                                Width="80px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                   
                                    <asp:TemplateField HeaderText="Shipment Qty">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvshipqtyopvss" runat="server" Style="text-align: right" 
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "shipqty")).ToString("#,##0;(#,##0); ") %>' 
                                                Width="80px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>


                                     <asp:TemplateField HeaderText="In Process">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvinproqtyopvss" runat="server" Style="text-align: right" 
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "inproqty")).ToString("#,##0;(#,##0); ") %>' 
                                                Width="80px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>


                                     <asp:TemplateField HeaderText="Order Balance">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvordbalopvss" runat="server" Style="text-align: right" 
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ordrbal")).ToString("#,##0;(#,##0); ") %>' 
                                                Width="80px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
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
                              
                                 <div class="form-group">
                                  <div class="col-md-7 asitCol7 pading5px">
                                   <asp:Label ID="lbldocumentation" runat="server" CssClass="lblName lblTxt"  Text="D. Export Documentation"    Visible="False" Width="300px"></asp:Label>
                                                                                          
                                         </div>
                                      </div>
                                 </div>
                            </fieldset>


                          <asp:GridView ID="gvExport" runat="server" AutoGenerateColumns="False"  CssClass=" table-striped table-hover table-bordered grvContentarea"
                                ShowFooter="True" onrowdatabound="gvExport_RowDataBound">
                                
                                <Columns>
                                    <asp:TemplateField HeaderText="Sl.No.">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvSlNo2" runat="server" Font-Bold="True" 
                                                Style="text-align: left" 
                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="50px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Description">
                                       
                                        <ItemTemplate>
                                        <asp:HyperLink ID="hlnkgvressdescexp" runat="server" BorderColor="#99CCFF" BorderStyle="none"
                                            Font-Size="11px" Style="background-color: Transparent; color: Black;" Font-Underline="false"
                                            Target="_blank" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "resdesc")) %>'
                                            Width="100px"></asp:HyperLink>
                                    </ItemTemplate>


                                        
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
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
                              
                                 <div class="form-group">
                                  <div class="col-md-7 asitCol7 pading5px">
                                   <asp:Label ID="lblBBlCduestatus" runat="server" CssClass="lblName lblTxt"  Text="E. BBLC  Payment Status"   Visible="False" Width="300px"></asp:Label>
                                                                                          
                                         </div>
                                      </div>
                                 </div>
                            </fieldset>

                           <asp:GridView ID="gvduebblc" runat="server" AutoGenerateColumns="False" CssClass=" table-striped table-hover table-bordered grvContentarea"
                                ShowFooter="True">
                              
                                <Columns>
                                    <asp:TemplateField HeaderText="Sl.No.">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvSlNo3" runat="server" Font-Bold="True"
                                                Style="text-align: right" 
                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="50px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="BBLC  Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvressdescduebblc" runat="server" 
                                                Style="text-align: left" 
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "resdesc")) %>' 
                                                Width="120px"></asp:Label>
                                        </ItemTemplate>
                                         <FooterTemplate>
                                            <asp:Label ID="lgvFtotalduebblc" runat="server" Font-Bold="True" Font-Size="12px" 
                                                ForeColor="#000" Width="70px" Text="Total"></asp:Label>
                                        </FooterTemplate>


                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>



                                    <asp:TemplateField HeaderText="Settlement Date">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvsettlementdate" runat="server" 
                                                Style="text-align: left" 
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "settldat")) %>' 
                                                Width="80px"></asp:Label>
                                        </ItemTemplate>
                                     


                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>

                                 
                                    <asp:TemplateField HeaderText="Bill Amount">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvbillamtdbblc" runat="server" Style="text-align: right" 
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "billamt")).ToString("#,##0;(#,##0); ") %>' 
                                                Width="80px"></asp:Label>
                                        </ItemTemplate>

                                           <FooterTemplate>
                                            <asp:Label ID="lgvFbillamtdbblc" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#000"
                                                Width="70px"></asp:Label>
                                        </FooterTemplate>



                                        <ItemStyle HorizontalAlign="Right" />
                                           <FooterStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Due Amount">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvdueamtdbblc" runat="server" Style="text-align: right" 
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "dueam")).ToString("#,##0;(#,##0); ") %>' 
                                                Width="80px"></asp:Label>
                                        </ItemTemplate>
                                         <FooterTemplate>
                                            <asp:Label ID="lgvFdueamtdbblc" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#000"
                                                Width="70px"></asp:Label>
                                        </FooterTemplate>



                                        <ItemStyle HorizontalAlign="Right" />
                                           <FooterStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderText="Not Yet Due">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvnydueamtdbblc" runat="server" Style="text-align: right" 
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "nydueamt")).ToString("#,##0;(#,##0); ") %>' 
                                                Width="80px"></asp:Label>
                                        </ItemTemplate>

                                          <FooterTemplate>
                                            <asp:Label ID="lgvFnydueamtdbblc" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#000"
                                                Width="70px"></asp:Label>
                                        </FooterTemplate>

                                        <ItemStyle HorizontalAlign="Right" />
                                           <FooterStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
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
                              
                                 <div class="form-group">
                                  <div class="col-md-7 asitCol7 pading5px">
                                   <asp:Label ID="lblIncomeSt" runat="server" CssClass="lblName lblTxt"  Text="F. IncomeStatement"  Visible="False" Width="300px"></asp:Label>
                                                                                          
                                         </div>
                                      </div>
                                 </div>
                            </fieldset>


                           <asp:GridView ID="gvinstment" runat="server" AutoGenerateColumns="False" CssClass=" table-striped table-hover table-bordered grvContentarea"
                                onrowdatabound="gvinstment_RowDataBound" ShowFooter="True" 
                                style="text-align: left" Width="529px">
                               
                                <Columns>
                                    <asp:TemplateField HeaderText="Sl">
                                        <ItemTemplate>
                                            <asp:Label ID="lblserialnoid" runat="server" style="text-align: right" 
                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Font-Bold="True" Font-Size="16px" />
                                        <ItemStyle Font-Size="12px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Item Description">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvItemDesc" runat="server" 
                                            
                                            
                                           
                                             Text='<%# "<B>"+ Convert.ToString(DataBinder.Eval(Container.DataItem, "mgrpdesc")) + "</B>"+
                                                                         (DataBinder.Eval(Container.DataItem, "resdesc").ToString().Trim().Length>0 ? 
                                                                         (Convert.ToString(DataBinder.Eval(Container.DataItem, "mgrpdesc")).Trim().Length>0 ?  "<br>" : "")+                                                             
                                                                         "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"+ 
                                                                         Convert.ToString(DataBinder.Eval(Container.DataItem, "resdesc")).Trim(): "") 
                                                                         
                                                                    %>'   Width="250px">
                                    
  
                                                                    
                                                                    
                                                                    
                                                                    
                                                                    
                                                                
                                                                    
                                                                    
                                                                    
                                                                   </asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Unit">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvUnit" runat="server" 
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "resunit")) %>' 
                                                Width="50px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Budgeted Amount (FC)">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvBudgetedFC" runat="server" BackColor="Transparent" 
                                                BorderStyle="None"  style="text-align: right" 
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "itmamtfc")).ToString("#,##0.00;(#,##0.00); ") %>' 
                                                Width="80px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="right" />
                                        <FooterStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Budgeted Amount (TK)">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvBudgetedCost" runat="server" BackColor="Transparent" 
                                                BorderStyle="None"  style="text-align: right" 
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "itmamt")).ToString("#,##0.00;(#,##0.00); ") %>' 
                                                Width="80px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="right" />
                                        <FooterStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Actual Amount (TK) ">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvtoamt" runat="server" BackColor="Transparent" 
                                                BorderStyle="None"  style="text-align: right" 
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "toamt")).ToString("#,##0.00;(#,##0.00); ") %>' 
                                                Width="80px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="right" />
                                        <FooterStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Variance (TK)">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvBalAmt" runat="server" BackColor="Transparent" 
                                                BorderStyle="None"  style="text-align: right" 
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "balamt")).ToString("#,##0.00;(#,##0.00); ") %>' 
                                                Width="80px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="right" />
                                        <FooterStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Variance %">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvvariance" runat="server" BackColor="Transparent" 
                                                BorderStyle="None"  style="text-align: right" 
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "variance")).ToString("#,##0.00;(#,##0.00); ")+"%" %>' 
                                                Width="80px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle HorizontalAlign="right" />
                                    </asp:TemplateField>
                                </Columns>
                                <FooterStyle CssClass="grvFooter"/>
                                <EditRowStyle />
                                <AlternatingRowStyle />
                                <PagerStyle CssClass="gvPagination" />
                                <HeaderStyle CssClass="grvHeader" />
                            </asp:GridView>

                           </div>
                     </div>
                </div>



            <%--<tr>
                            <td>
                                &nbsp;
                            </td>
                            <td class="style30">
                                <asp:Label ID="Label21" runat="server" Font-Bold="True" Font-Size="12px" Height="16px"
                                    Style="text-align: left; color: #FFFFFF;" Text="Project Name:" 
                                    Width="80px"></asp:Label>
                            </td>
                            <td class="style29">
                                <asp:TextBox ID="txtSearchpIndp" runat="server" Style="border-style: solid; border-width: 1px"
                                    Width="80px"></asp:TextBox>
                            </td>
                            <td class="style31">
                                <asp:ImageButton ID="ImgbtnFindProjind" runat="server" Height="19px" ImageUrl="~/Image/find_images.jpg"
                                    OnClick="ImgbtnFindProjind_Click" Width="21px" />
                            </td>
                            <td class="style28">
                                <asp:DropDownList ID="ddlProjectInd" runat="server" AutoPostBack="True" Font-Size="12px"
                                    Width="400px">
                                </asp:DropDownList>
                                <cc1:ListSearchExtender ID="ddlProjectInd_ListSearchExtender" runat="server" QueryPattern="Contains"
                                    TargetControlID="ddlProjectInd">
                                </cc1:ListSearchExtender>
                            </td>
                            <td class="style32">
                                &nbsp;&nbsp;
                                <asp:LinkButton ID="lbtnOk" runat="server" Font-Bold="True" Font-Size="16px" 
                                    Height="20px" OnClick="lbtnOk_Click" 
                                    Style="text-align: center;" Width="52px" 
                                    BackColor="#003366" ForeColor="#000" BorderColor="#000" 
                                    BorderStyle="Solid" BorderWidth="1px">Ok</asp:LinkButton>
                            </td>
                            <td>
                                &nbsp;&nbsp;
                            </td>
                            <td>
                                &nbsp;&nbsp;
                            </td>
                            <td>
                                &nbsp;&nbsp;
                            </td>
                            <td>
                                &nbsp;&nbsp;
                            </td>
                          
                        </tr>--%>
                <%--<tr>
                    <td>
                        &nbsp;</td>
                    <td class="style30">
                        <asp:Label ID="Label10" runat="server" Font-Bold="True" Font-Size="12px" 
                            ForeColor="#000" style="text-align: left" Text="As on:" Width="100px"></asp:Label>
                    </td>
                    <td class="style29">
                        <asp:Label ID="lblasondate" runat="server" BackColor="#000066" 
                            BorderColor="Yellow" BorderStyle="Solid" BorderWidth="1px" Font-Bold="True" 
                            Font-Size="12px" ForeColor="Yellow"
                            Width="90px"></asp:Label>
                    </td>
                    <td class="style31">
                        &nbsp;</td>
                    <td class="style28">
                        &nbsp;</td>
                    <td class="style32">
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                </tr>--%>
              
                    <%--<tr>
                        <td>
                            <asp:Label ID="lblOrderDetails" runat="server" BackColor="#000066" 
                                BorderColor="Yellow" BorderStyle="Solid" BorderWidth="1px" Font-Bold="True" 
                                Font-Size="12px" ForeColor="Yellow" Text="A. Order Details" Visible="False" 
                                Width="300px"></asp:Label>
                        </td>
                        <td>
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                    </tr>--%>
                   


                        
                        </td>
                    </tr>
                    <%--<tr>
                        <td>
                            <asp:Label ID="lblBBlCStatus" runat="server" BackColor="#000066" 
                                BorderColor="Yellow" BorderStyle="Solid" BorderWidth="1px" Font-Bold="True" 
                                Font-Size="12px" ForeColor="Yellow" Text="B. BBLC Status" Visible="False" 
                                Width="300px"></asp:Label>
                        </td>
                        <td>
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                    </tr>--%>
                    <tr>
                        <td colspan="12">
                        
                        </td>
                    </tr>
                    <%--<tr>
                        <td>
                            <asp:Label ID="lblProStatus" runat="server" BackColor="#000066" 
                                BorderColor="Yellow" BorderStyle="Solid" BorderWidth="1px" Font-Bold="True" 
                                Font-Size="12px" ForeColor="Yellow" Text="C.  Production Status" 
                                Visible="False" Width="300px"></asp:Label>
                        </td>
                        <td>
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                        <td>
                            &nbsp;</td>
                    </tr>--%>
                    <tr>
                        <td>
                           
                        </td>
                        
                    </tr>
                    <%--<tr>
                        <td>
                            <asp:Label ID="lblOrdproVsShip" runat="server" BackColor="#000066" 
                                BorderColor="Yellow" BorderStyle="Solid" BorderWidth="1px" Font-Bold="True" 
                                Font-Size="12px" ForeColor="Yellow" Text="C. Order, Production Vs. Shipment" 
                                Visible="False" Width="300px"></asp:Label>
                        </td>
                       
                        <td>
                            &nbsp;</td>
                    </tr>--%>
                    <tr>
                        <td colspan="12">
                           
                        </td>
                    </tr>
                    <%--<tr>
                        <td>
                            <asp:Label ID="lbldocumentation" runat="server" BackColor="#000066" 
                                BorderColor="Yellow" BorderStyle="Solid" BorderWidth="1px" Font-Bold="True" 
                                Font-Size="12px" ForeColor="Yellow" Text="D. Export Documentation" Visible="False" 
                                Width="300px"></asp:Label>
                        </td>
                        <t
                    </tr>--%>
                    <tr>
                        <td colspan="12">
                           
                        </td>
                    </tr>
                    <%--<tr>
                        <td>
                            <asp:Label ID="lblBBlCduestatus" runat="server" BackColor="#000066" 
                                BorderColor="Yellow" BorderStyle="Solid" BorderWidth="1px" Font-Bold="True" 
                                Font-Size="12px" ForeColor="Yellow" Text="E. BBLC  Payment Status" Visible="False" 
                                Width="300px"></asp:Label>
                        </td>
                        
                        <td>
                            &nbsp;</td>
                    </tr>--%>
                    <tr>
                        <td colspan="12">
                          
                        </td>
                    </tr>
                    <%--<tr>
                        <td>
                            <asp:Label ID="lblIncomeSt" runat="server" BackColor="#000066" 
                                BorderColor="Yellow" BorderStyle="Solid" BorderWidth="1px" Font-Bold="True" 
                                Font-Size="12px" ForeColor="Yellow" Text="F. IncomeStatement" Visible="False" 
                                Width="300px"></asp:Label>
                        </td>
                        <td>
                   
                    </tr>--%>
                    <tr>
                        <td colspan="12">
                          
                        </td>
                    </tr>
                   
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
