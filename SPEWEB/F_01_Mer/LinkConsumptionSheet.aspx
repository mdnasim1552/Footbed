<%@ Page Title="" Language="C#" MasterPageFile="~/SPEMaster.Master" AutoEventWireup="true" CodeBehind="LinkConsumptionSheet.aspx.cs" Inherits="SPEWEB.F_01_Mer.LinkConsumptionSheet" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript" language="javascript">

     

        $(document).ready(function () {

            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);
        });
        function pageLoaded() {
            //$(function () {
            //    $('[id*=ddlComponent]').multiselect({
            //        includeSelectAllOption: true
            //    })
            //});
            $('.chzn-select').chosen({ search_contains: true });
        }

    </script>
    
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="container moduleItemWrpper">
                <div class="contentPart">
                    <div class="row">
                        <fieldset class="scheduler-border">
                            <div class="form-horizontal">
                                <div class="form-group">
                                    <div class="col-md-3">
                                <asp:CheckBox ID="ChckVIew" CssClass="chkBoxControl" Text="View All Data" runat="server" AutoPostBack="true" OnCheckedChanged="ChckVIew_CheckedChanged" />
                               <asp:CheckBox ID="ChckCopy" CssClass="chkBoxControl" Text="Copy From" runat="server" AutoPostBack="true" OnCheckedChanged="ChckCopy_CheckedChanged" />
 
                                             </div>
                                    
                                  <div class="col-md-2 pading5px">
                                          <asp:DropDownList ID="ddlPreList" Visible="false" runat="server" OnSelectedIndexChanged="ddlPreList_SelectedIndexChanged" CssClass="form-control chzn-select inputTxt" AutoPostBack="true">
                                        </asp:DropDownList>
                                      </div>
                                      <%--<div class="col-md-2">
                                        <asp:DropDownList ID="ddlArticle" Visible="false" runat="server" CssClass="form-control chzn-select inputTxt" >
                                        </asp:DropDownList>
                                </div>--%>
                                    <div class="col-md-1">
                                               <asp:LinkButton ID="LbtnCopy" OnClientClick="return confirm('Really Do You Want to Copy?')" Visible="false" runat="server" Text="Copy" OnClick="LbtnCopy_Click" CssClass="btn btn-xs btn-success" TabIndex="4"></asp:LinkButton>
                           
                                    </div>
                             


                              </div>
                                <div class="" id="ProcessPanel" runat="server">
                                    <div class="form-group">
                                        <div class="col-md-1 pading5px">
                                            <asp:Label ID="lblProcess" runat="server" CssClass="smLbl_to" Text="Department"></asp:Label>
                                        </div>
                                        <div class="col-md-2 pading5px">
                                            <asp:DropDownList ID="ddlProcess" runat="server" CssClass="form-control inputTxt chzn-select" AutoPostBack="true" OnSelectedIndexChanged="ddlProcess_SelectedIndexChanged">
                                            </asp:DropDownList>


                                        </div>
                                        <div class="col-md-1 pading5px">
                                            <asp:Label ID="lblProcess0" runat="server" CssClass="smLbl_to" Text="Materials Name"></asp:Label>

                                        </div>
                                        <div class="col-md-4 pading5px">
                                            <asp:DropDownList ID="ddlResourcesCost" runat="server" CssClass="form-control inputTxt chzn-select">
                                            </asp:DropDownList>
                                        </div>

                                        <div class="col-md-1 pading5px hidden">
                                            <asp:Label ID="lblSpcf" runat="server" CssClass=" smLbl_to" Text="Specification"></asp:Label>
                                        </div>
                                        <div class="col-md-2 pading5px hidden">
                                            <asp:DropDownList ID="ddlSpcfcode" runat="server" CssClass="form-control inputTxt chzn-select">
                                            </asp:DropDownList>
                                        </div>





                                        <div class="col-md-2 pading5px">
                                            <asp:LinkButton ID="lnkAddResouctCost" runat="server" Text="Add Table" OnClick="lnkAddResouctCost_Click" CssClass="btn btn-primary primaryBtn " TabIndex="1">Add</asp:LinkButton>
                                        </div>

                                        <div class="clearfix"></div>
                                    </div>
                                   <asp:GridView ID="gvCost" runat="server" AutoGenerateColumns="False" ShowFooter="True" CssClass="table-striped table-hover table-bordered grvContentarea"
                                            Width="479px" OnRowDeleting="gvCost_RowDeleting" OnRowDataBound="gvCost_RowDataBound">
                                            <Columns>
                                                <asp:TemplateField HeaderStyle-Font-Bold="true" HeaderStyle-Font-Size="12px" HeaderStyle-ForeColor="#333333"
                                                    HeaderText="SL" ItemStyle-Font-Size="12px">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvsl0" runat="server" Text='<%# Convert.ToInt32(Container.DataItemIndex+1).ToString("00")+"." %>'
                                                            Width="10px" Style="text-align: left"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle Font-Bold="True" Font-Size="12px" ForeColor="#333333" />
                                                    <ItemStyle Font-Size="12px" />
                                                </asp:TemplateField>

                                                <asp:CommandField ShowDeleteButton="True" ControlStyle-CssClass="btn btn-xs btn-danger" DeleteText="<span class='glyphicon glyphicon-remove'></span>" />

                                                <asp:TemplateField HeaderStyle-Font-Bold="true" HeaderStyle-Font-Size="10px" HeaderStyle-ForeColor="#333333"
                                                    HeaderText="Department Name" ItemStyle-Font-Size="10px" ItemStyle-HorizontalAlign="Left">
                                                    <HeaderTemplate>
                                                    <table>

                                                        <tr>
                                                             <th class="">DEPARTMENT NAME                                                              
                                                            </th>
                                                            <th class="pull-right">
                                                                <asp:HyperLink ID="hlbtnRdataExel" runat="server" BackColor="#000066" ToolTip="Export Excel"
                                                                    BorderColor="White" BorderStyle="Solid" BorderWidth="1px" Font-Bold="True"
                                                                    ForeColor="White" Style="text-align: center; margin-left: 10px;" Width="20px"><span class="fa fa-file-excel-o"></span></asp:HyperLink>
                                                            </th>
                                                        </tr>
                                                    </table>
                                                </HeaderTemplate>
                                                    <ItemTemplate>

                                                        <asp:Label ID="lblgvdept" runat="server" BorderStyle="None" Style="text-align: left" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "prodesc")) %>'
                                                            Width="150px"></asp:Label>

                                                    </ItemTemplate>
                                                    <HeaderStyle Font-Bold="True" Font-Size="10px" ForeColor="#333333" />
                                                    <ItemStyle Font-Size="10px" />

                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderStyle-Font-Bold="true" HeaderStyle-Font-Size="10px" HeaderStyle-ForeColor="#333333"
                                                    HeaderText="Part Name" ItemStyle-Font-Size="10px" Visible="false">

                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvcompcode" runat="server" Visible="false" BorderStyle="None" Style="text-align: left" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "compcode")) %>'
                                                            Width="0px"></asp:Label>
                                                        <asp:Label ID="lblgvCompnent" runat="server" BorderStyle="None" Style="text-align: left" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "compname")) %>'
                                                            Width="150px"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle Font-Bold="True" Font-Size="10px" ForeColor="#333333" />
                                                    <ItemStyle Font-Size="10px" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderStyle-Font-Bold="true" HeaderStyle-Font-Size="10px" HeaderStyle-ForeColor="#333333"
                                                    HeaderText="MATERIALS CODE NO" ItemStyle-Font-Size="10px" ItemStyle-HorizontalAlign="Left">

                                                    <ItemTemplate>

                                                        <asp:Label ID="lblgvDesc" runat="server" BorderStyle="None" Style="text-align: left" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "resdesc")) %>'
                                                            Width="150px"></asp:Label>

                                                    </ItemTemplate>
                                                    <HeaderStyle Font-Bold="True" Font-Size="10px" ForeColor="#333333" />
                                                    <ItemStyle Font-Size="10px" />
                                                    <FooterStyle HorizontalAlign="left" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderStyle-Font-Bold="true" HeaderStyle-Font-Size="10px" HeaderStyle-ForeColor="#333333"
                                                    HeaderText="Part No" Visible="false" ItemStyle-Font-Size="10px">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvcodeCost" runat="server" Style="text-align: left" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rescode")) %>'
                                                            Width="60px"></asp:Label>
                                                        <asp:Label ID="lblgvcolor" Visible="false" runat="server" Style="text-align: left" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "colorid")) %>'
                                                            Width="60px"></asp:Label>
                                                        <asp:Label ID="lblgvsize" Visible="false" runat="server" Style="text-align: left" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sizeid")) %>'
                                                            Width="60px"></asp:Label>
                                                    </ItemTemplate>

                                                    <HeaderStyle Font-Bold="True" Font-Size="10px" ForeColor="#333333" />
                                                    <ItemStyle Font-Size="8px" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderStyle-Font-Bold="true" HeaderStyle-Font-Size="10px" HeaderStyle-ForeColor="#333333"
                                                    HeaderText="MATERIALS NAME" ItemStyle-Font-Size="10px">

                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvspcfcode" runat="server" Visible="false" BorderStyle="None" Style="text-align: left" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "spcfcode")) %>'
                                                            Width="0px"></asp:Label>
                                                        <asp:Label ID="lblgvspcfdesc" runat="server" BorderStyle="None" Style="text-align: left" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "spcfdesc")) %>'
                                                            Width="250px"></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label ID="lbltoalf" runat="server">Common Material </asp:Label>
                                                    </FooterTemplate>
                                                    <HeaderStyle Font-Bold="True" Font-Size="10px" ForeColor="#333333" />
                                                    <ItemStyle Font-Size="10px" />
                                                </asp:TemplateField>




                                                <asp:TemplateField HeaderStyle-Font-Bold="true" HeaderStyle-Font-Size="10px" HeaderStyle-ForeColor="#333333" ItemStyle-HorizontalAlign="Right"
                                                    HeaderText="CONS/ PAIR">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtgvconqty" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                                            BorderWidth="1px" CssClass="GridItmTextBoxRight"
                                                            Font-Size="10px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "conqty")).ToString("#,##0.000000;(#,##0.000000); ") %>'
                                                            Width="50px"></asp:TextBox>
                                                            <asp:RequiredFieldValidator ValidationGroup="gvSave" ID="rfvqty" runat="server" ControlToValidate="txtgvconqty" EnableClientScript="false" Display="Dynamic" ErrorMessage="QTY invalid" ForeColor="Red" SetFocusOnError="true" />
                                               
                                                    </ItemTemplate>

                                                    <HeaderStyle Font-Bold="True" Font-Size="10px" ForeColor="#333333" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderStyle-Font-Bold="true" HeaderStyle-Font-Size="10px" HeaderStyle-ForeColor="#333333"
                                                    HeaderText="UNIT" ItemStyle-Font-Size="10px">

                                                    <ItemTemplate>
                                                        <asp:Label ID="txtgvunit0" runat="server" BorderStyle="None" Style="text-align: left" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "resunit")) %>'
                                                            Width="30px"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle Font-Bold="True" Font-Size="10px" ForeColor="#333333" />
                                                    <ItemStyle Font-Size="10px" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderStyle-Font-Bold="true" HeaderStyle-Font-Size="10px" HeaderStyle-ForeColor="#333333" ItemStyle-HorizontalAlign="Right"
                                                    HeaderText="PROPOESD <br> WASTAGE %">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtgvwestpc" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                                            BorderWidth="1px" CssClass="GridItmTextBoxRight"
                                                            Font-Size="10px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "westpc")).ToString("#,##0.000;(#,##0.000); ") %>'
                                                            Width="40px"></asp:TextBox>
                                                    </ItemTemplate>

                                                    <HeaderStyle Font-Bold="True" Font-Size="10px" ForeColor="#333333" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderStyle-Font-Bold="true" HeaderStyle-Font-Size="10px" HeaderStyle-ForeColor="#333333" ItemStyle-HorizontalAlign="Right"
                                                    HeaderText="SUB TOTAL">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtgvqtyCost" runat="server" BorderStyle="None" CssClass="GridItmTextBoxRight"
                                                            Font-Size="10px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty")).ToString("#,##0.000000;(#,##0.000000); ") %>'
                                                            Width="50px"></asp:TextBox>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label ID="lblgvttlqty" runat="server" Font-Bold="True" Font-Size="10px" ForeColor="#CC0066"
                                                            Style="text-align: right" Width="60px"></asp:Label>
                                                    </FooterTemplate>
                                                    <HeaderStyle Font-Bold="True" Font-Size="10px" ForeColor="#333333" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderStyle-Font-Bold="true" HeaderStyle-Font-Size="12px" HeaderStyle-ForeColor="#333333"
                                                    ItemStyle-Font-Size="10px" ItemStyle-HorizontalAlign="Right" HeaderText="PRICE/UNIT">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtgvqrateCost" runat="server" BorderStyle="None" Font-Size="12px"
                                                            Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "rate")).ToString("#,##0.000000;(#,##0.000000); ") %>'
                                                            Width="50px"></asp:TextBox>
                                                         
                                                    </ItemTemplate>

                                                    <HeaderStyle Font-Bold="True" Font-Size="10px" ForeColor="#333333" />
                                                    <ItemStyle Font-Size="10px" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="TOTAL <br> IN USD">
                                                    <ItemTemplate>

                                                        <asp:TextBox ID="txtgvamtCost" runat="server" BorderStyle="None" Font-Size="10px"
                                                            Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt")).ToString("#,##0.000;(#,##0.000); ") %>'
                                                            Width="50px"></asp:TextBox>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label ID="lblgvfamtCost" runat="server" Font-Bold="True" Font-Size="10px" ForeColor="#CC0066"
                                                            Style="text-align: right" Width="60px"></asp:Label>
                                                    </FooterTemplate>
                                                    <HeaderStyle Font-Bold="True" Font-Size="10px" ForeColor="#333333" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="BDT Amount" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgBdtamt" runat="server" Font-Size="12px"
                                                            ItemStyle-Font-Size="12px" Style="text-align: right"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "convrate")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                            Width="80px"></asp:Label>

                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label ID="lblgvBdamtCost" runat="server" Font-Bold="True" Font-Size="10px" ForeColor="#CC0066"
                                                            Style="text-align: right" Width="60px"></asp:Label>
                                                    </FooterTemplate>
                                                    <HeaderStyle Font-Bold="True" Font-Size="10px" ForeColor="#333333" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="%" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvpercnt" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "percnt")).ToString("#,##0.00;(#,##0.00); ")+(Convert.ToDouble((DataBinder.Eval(Container.DataItem, "percnt")))>0?"%":"")%>'
                                                            Width="40px" Style="text-align: right" ItemStyle-Font-Size="10px" Font-Size="10px"></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label ID="lblgvfper" runat="server" Font-Bold="True" Font-Size="10px" ForeColor="#CC0066"
                                                            Width="40px" Style="text-align: right"></asp:Label>
                                                    </FooterTemplate>
                                                    <HeaderStyle Font-Bold="True" Font-Size="10px" ForeColor="#333333" />
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
                        </fieldset>

                        <div class="row">
                            <div class="col-md-12" id="pnlReport" runat="server">
                               <%-- <asp:MultiView ID="MultiView1" runat="server">
                                    <asp:View ID="Consumption" runat="server">--%>
                                        
                                   <%-- </asp:View>--%>
                                   <%-- <asp:View ID="RptCon" runat="server">--%>
                                        <asp:GridView ID="gvRtpcon" runat="server" AutoGenerateColumns="False" ShowFooter="True" CssClass="table-striped table-hover table-bordered grvContentarea"
                                            Width="479px">
                                            <Columns>
                                                <asp:TemplateField HeaderStyle-Font-Bold="true" HeaderStyle-Font-Size="12px" HeaderStyle-ForeColor="#333333"
                                                    HeaderText="SL" ItemStyle-Font-Size="12px">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvsl0" runat="server" Text='<%# Convert.ToInt32(Container.DataItemIndex+1).ToString("00")+"." %>'
                                                            Width="10px" Style="text-align: left"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle Font-Bold="True" Font-Size="12px" ForeColor="#333333" />
                                                    <ItemStyle Font-Size="12px" />
                                                </asp:TemplateField>


                                                <asp:TemplateField HeaderStyle-Font-Bold="true" HeaderStyle-Font-Size="10px" HeaderStyle-ForeColor="#333333"
                                                    HeaderText="Department Name" ItemStyle-Font-Size="10px" ItemStyle-HorizontalAlign="Left">
                                                    <HeaderTemplate>
                                                    <table>

                                                        <tr>
                                                             <th class="">Department Name                                                              
                                                            </th>
                                                            <th class="pull-right">
                                                                <asp:HyperLink ID="hlbtnRdataExel" runat="server" BackColor="#000066" ToolTip="Export Excel"
                                                                    BorderColor="White" BorderStyle="Solid" BorderWidth="1px" Font-Bold="True"
                                                                    ForeColor="White" Style="text-align: center; margin-left: 10px;" Width="20px"><span class="fa fa-file-excel-o"></span></asp:HyperLink>
                                                            </th>
                                                        </tr>
                                                    </table>
                                                </HeaderTemplate>
                                                    <ItemTemplate>

                                                        <asp:Label ID="lblgvDeptname" runat="server" BorderStyle="None" Style="text-align: left" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "prodesc")) %>'
                                                            Width="150px"></asp:Label>

                                                    </ItemTemplate>
                                                    <HeaderStyle Font-Bold="True" Font-Size="10px" ForeColor="#333333" />
                                                    <ItemStyle Font-Size="10px" />
                                                    <FooterStyle HorizontalAlign="left" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderStyle-Font-Bold="true" HeaderStyle-Font-Size="10px" HeaderStyle-ForeColor="#333333"
                                                    HeaderText="Part Name" ItemStyle-Font-Size="10px" Visible="false">

                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvcompcode" runat="server" Visible="false" BorderStyle="None" Style="text-align: left" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "compcode")) %>'
                                                            Width="0px"></asp:Label>
                                                        <asp:Label ID="lblgvCompnent" runat="server" BorderStyle="None" Style="text-align: left" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "compname")) %>'
                                                            Width="150px"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle Font-Bold="True" Font-Size="10px" ForeColor="#333333" />
                                                    <ItemStyle Font-Size="10px" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderStyle-Font-Bold="true" HeaderStyle-Font-Size="10px" HeaderStyle-ForeColor="#333333"
                                                    HeaderText="Material Name" ItemStyle-Font-Size="10px" ItemStyle-HorizontalAlign="Left">

                                                    <ItemTemplate>

                                                        <asp:Label ID="lblgvDesc" runat="server" BorderStyle="None" Style="text-align: left" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "resdesc")) %>'
                                                            Width="150px"></asp:Label>

                                                    </ItemTemplate>
                                                    <HeaderStyle Font-Bold="True" Font-Size="10px" ForeColor="#333333" />
                                                    <ItemStyle Font-Size="10px" />
                                                    <FooterStyle HorizontalAlign="left" />
                                                </asp:TemplateField>
                                                <asp:TemplateField Visible="false" HeaderStyle-Font-Bold="true" HeaderStyle-Font-Size="10px" HeaderStyle-ForeColor="#333333"
                                                    HeaderText="Part No" ItemStyle-Font-Size="10px">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvcodeCost" runat="server" Style="text-align: left" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rescode")) %>'
                                                            Width="60px"></asp:Label>
                                                        <asp:Label ID="lblgvcolor" Visible="false" runat="server" Style="text-align: left" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "colorid")) %>'
                                                            Width="60px"></asp:Label>
                                                        <asp:Label ID="lblgvsize" Visible="false" runat="server" Style="text-align: left" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sizeid")) %>'
                                                            Width="60px"></asp:Label>
                                                    </ItemTemplate>

                                                    <HeaderStyle Font-Bold="True" Font-Size="10px" ForeColor="#333333" />
                                                    <ItemStyle Font-Size="8px" />
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderStyle-Font-Bold="true" HeaderStyle-Font-Size="10px" HeaderStyle-ForeColor="#333333"
                                                    HeaderText="Materials Specification" ItemStyle-Font-Size="10px">

                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvspcfcode" runat="server" Visible="false" BorderStyle="None" Style="text-align: left" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "spcfcode")) %>'
                                                            Width="0px"></asp:Label>
                                                        <asp:Label ID="lblgvspcfdesc" runat="server" BorderStyle="None" Style="text-align: left" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "spcfdesc")) %>'
                                                            Width="150px"></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label ID="lbltoalf" runat="server">DIRECT MATERIAL COST</asp:Label>
                                                    </FooterTemplate>
                                                    <HeaderStyle Font-Bold="True" Font-Size="10px" ForeColor="#333333" />
                                                    <ItemStyle Font-Size="10px" />
                                                </asp:TemplateField>




                                                <asp:TemplateField HeaderStyle-Font-Bold="true" HeaderStyle-Font-Size="10px" HeaderStyle-ForeColor="#333333" ItemStyle-HorizontalAlign="Right"
                                                    HeaderText="Con/ Pair">
                                                    <ItemTemplate>
                                                        <asp:Label ID="txtgvconqty" runat="server"
                                                            CssClass="GridItmTextBoxRight"
                                                            Font-Size="10px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "conqty")).ToString("#,##0.000000;(#,##0.000000); ") %>'
                                                            Width="50px"></asp:Label>
                                                    </ItemTemplate>

                                                    <HeaderStyle Font-Bold="True" Font-Size="10px" ForeColor="#333333" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderStyle-Font-Bold="true" HeaderStyle-Font-Size="10px" HeaderStyle-ForeColor="#333333"
                                                    HeaderText="Unit" ItemStyle-Font-Size="10px">

                                                    <ItemTemplate>
                                                        <asp:Label ID="txtgvunit0" runat="server" BorderStyle="None" Style="text-align: left" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "resunit")) %>'
                                                            Width="30px"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle Font-Bold="True" Font-Size="10px" ForeColor="#333333" />
                                                    <ItemStyle Font-Size="10px" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderStyle-Font-Bold="true" HeaderStyle-Font-Size="10px" HeaderStyle-ForeColor="#333333" ItemStyle-HorizontalAlign="Right"
                                                    HeaderText="Propoesd <br>Wastage %">
                                                    <ItemTemplate>
                                                        <asp:Label ID="txtgvwestpc" runat="server"
                                                            CssClass="GridItmTextBoxRight"
                                                            Font-Size="10px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "westpc")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                            Width="40px"></asp:Label>
                                                    </ItemTemplate>

                                                    <HeaderStyle Font-Bold="True" Font-Size="10px" ForeColor="#333333" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderStyle-Font-Bold="true" HeaderStyle-Font-Size="10px" HeaderStyle-ForeColor="#333333" ItemStyle-HorizontalAlign="Right"
                                                    HeaderText="Sub Total">
                                                    <ItemTemplate>
                                                        <asp:Label ID="txtgvqtyCost" runat="server" BorderStyle="None" CssClass="GridItmTextBoxRight"
                                                            Font-Size="10px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty")).ToString("#,##0.000000;(#,##0.000000); ") %>'
                                                            Width="50px"></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label ID="lblgvttlqty" runat="server" Font-Bold="True" Font-Size="10px" ForeColor="#CC0066"
                                                            Style="text-align: right" Width="60px"></asp:Label>
                                                    </FooterTemplate>
                                                    <HeaderStyle Font-Bold="True" Font-Size="10px" ForeColor="#333333" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderStyle-Font-Bold="true" HeaderStyle-Font-Size="12px" HeaderStyle-ForeColor="#333333"
                                                    ItemStyle-Font-Size="10px" ItemStyle-HorizontalAlign="Right" HeaderText="Price/ Unit">
                                                    <ItemTemplate>
                                                        <asp:Label ID="txtgvqrateCost" runat="server" BorderStyle="None" Font-Size="12px"
                                                            Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "rate")).ToString("#,##0.000000;(#,##0.000000); ") %>'
                                                            Width="50px"></asp:Label>
                                                    </ItemTemplate>

                                                    <HeaderStyle Font-Bold="True" Font-Size="10px" ForeColor="#333333" />
                                                    <ItemStyle Font-Size="10px" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Total <br> in USD">
                                                    <ItemTemplate>

                                                        <asp:Label ID="txtgvamtCost" runat="server" BorderStyle="None" Font-Size="10px"
                                                            Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt")).ToString("#,##0.000;(#,##0.000); ") %>'
                                                            Width="50px"></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label ID="lblgvfamtCost" runat="server" Font-Bold="True" Font-Size="10px" ForeColor="#CC0066"
                                                            Style="text-align: right" Width="60px"></asp:Label>
                                                    </FooterTemplate>
                                                    <HeaderStyle Font-Bold="True" Font-Size="10px" ForeColor="#333333" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="BDT Amount" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgBdtamt" runat="server" Font-Size="12px"
                                                            ItemStyle-Font-Size="12px" Style="text-align: right"
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "convrate")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                            Width="80px"></asp:Label>

                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label ID="lblgvBdamtCost" runat="server" Font-Bold="True" Font-Size="10px" ForeColor="#CC0066"
                                                            Style="text-align: right" Width="60px"></asp:Label>
                                                    </FooterTemplate>
                                                    <HeaderStyle Font-Bold="True" Font-Size="10px" ForeColor="#333333" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="%" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvpercnt" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "percnt")).ToString("#,##0.00;(#,##0.00); ")+(Convert.ToDouble((DataBinder.Eval(Container.DataItem, "percnt")))>0?"%":"")%>'
                                                            Width="40px" Style="text-align: right" ItemStyle-Font-Size="10px" Font-Size="10px"></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label ID="lblgvfper" runat="server" Font-Bold="True" Font-Size="10px" ForeColor="#CC0066"
                                                            Width="40px" Style="text-align: right"></asp:Label>
                                                    </FooterTemplate>
                                                    <HeaderStyle Font-Bold="True" Font-Size="10px" ForeColor="#333333" />
                                                </asp:TemplateField>
                                            </Columns>
                                            <FooterStyle CssClass="grvFooter" />
                                            <EditRowStyle />
                                            <AlternatingRowStyle />
                                            <PagerStyle CssClass="gvPagination" />
                                            <HeaderStyle CssClass="grvHeader" />
                                        </asp:GridView>
                                  <%--  </asp:View>--%>
                               <%-- </asp:MultiView>--%>
                                <br />
                                

                            </div>

                        </div>
                    </div>
                </div>
            </div>





            


        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
