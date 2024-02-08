<%@ Page Title="" Language="C#" MasterPageFile="~/SPE.Master" AutoEventWireup="true" CodeBehind="SampleInquiry.aspx.cs" Inherits="SPEWEB.F_01_Mer.SampleInquiry" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
      <script type="text/javascript" language="javascript">
          function openModal() {
              $('#myModal').modal('toggle');
          }
          function CLoseMOdal() {
              $('#myModal').modal('hide');
              $('#Buyer').modal('hide');
              $('#Season').modal('hide');
              $('#BrandModal').modal('hide');
              $('#StyleModal').modal('hide');

          }
          $(document).ready(function () {

            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);
                    });
        function pageLoaded() {
          
            $('.chzn-select').chosen({ search_contains: true });
        }

    </script>
    <style type="text/css">
      
        .UpdateMOdel{
            position: fixed;
  margin: 0;
  width: 100%;
  height: 100%;
  padding: 0;
        }

        .chzn-single {
    border-radius: 3px !important;
    height: 29px !important;
    
}
      .sizecolor  .modal-dialog {
     max-width: 100% !important;
        }
    </style>
   
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="nahidProgressbar">
                <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1" DisplayAfter="30">
                    <ProgressTemplate>

                        <%--  <div class="loader"></div> --%>
                        <div id="loader">
                            <div class="dot"></div>
                            <div class="dot"></div>
                            <div class="dot"></div>
                            <div class="dot"></div>
                            <div class="dot"></div>
                            <div class="dot"></div>
                            <div class="dot"></div>
                            <div class="dot"></div>
                            <div class="lading"></div>
                        </div>
                    </ProgressTemplate>
                </asp:UpdateProgress>
            </div>

   <div class="card card-fluid">
                <div class="card-body">
                    <div class="row">
                      <div class="col-md-1">
                                <div class="form-group">
                                   
                                        <asp:Label ID="Label1" runat="server" CssClass="label">Date</asp:Label>
                                        <asp:TextBox ID="txtDatefrom" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
                                        <cc1:CalendarExtender ID="datefrom" runat="server" Format="dd-MMM-yyyy" TargetControlID="txtDatefrom"></cc1:CalendarExtender>
                                    </div>
                                   </div>   
                                    <div class="col-md-2">
                                <div class="form-group">
                                           <asp:Label ID="lblLcName" runat="server" CssClass="label">INQ No</asp:Label>
                                    <div class="form-inline">
                                         <asp:Label ID="lblCurNo1" Text="INQ00" runat="server" CssClass="form-control form-control-sm"></asp:Label>
                                        <asp:Label ID="lblCurNo2" Text="00000" runat="server" CssClass="form-control form-control-sm"></asp:Label>
                                    </div>
                                         
                                     </div>
                                   </div>
                         <div class="col-md-2">
                                <div class="form-group">
                                          <asp:Label ID="Label3" runat="server" CssClass="label">CLIENT NAME
                                          </asp:Label>
                                   
                                        <asp:DropDownList ID="ddlbuyer" CssClass="form-control form-control-sm chzn-select" runat="server"></asp:DropDownList>
                                    </div>
                              </div>
                                    <div class="col-md-4">
                                     <div class="form-group" style="margin-top:20px;">
                                       <asp:LinkButton ID="lbtnOk" runat="server" Text="Ok" OnClick="lbtnOk_Click" CssClass="btn btn-primary btn-sm" TabIndex="4"></asp:LinkButton>
                                       &nbsp; <a class="btn btn-success btn-xs" href="#Buyer" data-toggle="modal"><i class="glyphicon glyphicon-user"></i> Client</a>
                                       &nbsp; <a class="btn btn-warning btn-xs" href="#Season" data-toggle="modal"><i class="glyphicon glyphicon-plus"></i> Season</a>
                                       &nbsp; <a class="btn btn-danger btn-xs" href="#BrandModal" data-toggle="modal"><i class="glyphicon glyphicon-plus"></i> Brand</a>
                                     &nbsp; <a class="btn btn-primary btn-xs" href="#StyleModal" data-toggle="modal"><i class="glyphicon glyphicon-plus"></i> Style</a>
                                 
                                    </div>
                                        </div>
                                    <div class="col-md-2">
                                         <div class="form-group">
                                      <asp:LinkButton ID="ibtnPreList" runat="server" CssClass="btn-link " OnClick="ibtnPreList_Click">Pre. Inquiry</asp:LinkButton>
                                      <asp:DropDownList ID="ddlPreList" runat="server" CssClass="form-control chzn-select inputTxt" AutoPostBack="true">
                                        </asp:DropDownList>
                                             </div>
                                        
                                    </div>
                          



                                </div>
                                
                           
                    </div>


                  </div>
      
              <div class="card card-fluid">
                <div class="card-body" style="min-height:350px;">
                    <div class="row">
                    <asp:GridView ID="gvSampleInq" runat="server" AutoGenerateColumns="False" CssClass=" table-striped table-hover table-bordered grvContentarea"
                        ShowFooter="True" OnRowDataBound="gvSampleInq_RowDataBound" OnRowDeleting="gvSampleInq_RowDeleting">
                        <RowStyle />
                        <Columns>
                            <asp:TemplateField HeaderText="SL">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lblgvSlNo0" runat="server" Font-Bold="True" OnClick="lblgvItmCodc_Click"
                                        Style="text-align: right" ToolTip="Click for Details Input"
                                        Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:LinkButton>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>
                               <asp:CommandField ShowDeleteButton="True" ControlStyle-ForeColor="DarkRed" DeleteText="<span class='fa fa-trash'></span>" />
                            <asp:TemplateField HeaderText="SEASON">
                                <ItemTemplate>                                                                  
                                    <asp:DropDownList ID="ddlSeason" CssClass="form-control form-control-sm chzn-select" runat="server"></asp:DropDownList>
                                    </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>
                               <asp:TemplateField HeaderText="BRAND NAME">
                                <ItemTemplate>                                                                  
                                    <asp:DropDownList ID="ddlbrand" CssClass="form-control form-control-sm chzn-select" runat="server"></asp:DropDownList>
                                    </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>
                               <asp:TemplateField HeaderText="IMAGE">
                                <ItemTemplate>
                                     <asp:HyperLink ID="hyprrr" runat="server" NavigateUrl='<%# (Eval("images").ToString()=="")?"~/images/no_img_preview.png":Eval("images") %>' Target="_blank">
                                                                    <asp:Image ID="lblImageUrl" Width="60" Height="40" runat="server" ImageUrl='<%# (Eval("images").ToString()=="")?"~/images/no_img_preview.png":Eval("images") %>' class="img-responsive"></asp:Image>
                                                                   </asp:HyperLink>
                                   
                                </ItemTemplate>                               
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>
                             <asp:TemplateField HeaderText="COLOR" >
                                <ItemTemplate>
                                    <asp:TextBox ID="txtgColor" runat="server" AutoCompleteType="Disabled"
                                        BackColor="Transparent" BorderStyle="solid" BorderWidth="1px" BorderColor="#6acc39"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "color")) %>'
                                        Width="80px"></asp:TextBox>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="STYLE">
                                <ItemTemplate>                                                                  
                                    <asp:DropDownList ID="ddlcategory" CssClass="form-control form-control-sm chzn-select" runat="server"></asp:DropDownList>
                                    </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Style Id" Visible="false">
                                <ItemTemplate>
                                   <%-- <asp:LinkButton OnClick="lblgvItmCodc_Click" ID="lblgvItmCodc" runat="server"  Height="16px" Font-Size="11px"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "styleid")) %>'
                                        Width="80px"></asp:LinkButton>  --%>
                                    <asp:Label ID="lblgvItmCodc" runat="server" AutoCompleteType="Disabled"
                                        BackColor="Transparent" BorderStyle="None" Font-Size="11px"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "styleid")) %>'
                                        Width="130px"></asp:Label>

                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>                        
                            <asp:TemplateField HeaderText="STYLE DESCRIPTION" Visible="false">

                                <ItemTemplate>
                                    <asp:TextBox ID="txtgvStyle" runat="server" AutoCompleteType="Disabled"
                                        BackColor="Transparent" BorderStyle="solid" BorderWidth="1px" BorderColor="#6acc39"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "styledesc")) %>'
                                        Width="120px"></asp:TextBox>
                                </ItemTemplate>                               
                              
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>
                             <asp:TemplateField HeaderText="ARTICLE NUM." >
                                <ItemTemplate>
                                    <asp:TextBox ID="txtgArtno" runat="server" AutoCompleteType="Disabled"
                                        BackColor="Transparent" BorderStyle="solid" BorderWidth="1px" BorderColor="#6acc39"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "artno")) %>'
                                        Width="80px"></asp:TextBox>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="ESTIMATED <br> ORDER QTY" >
                                <ItemTemplate>
                                    <asp:TextBox ID="txtgvQtyc" runat="server" BackColor="Transparent"
                                         BorderStyle="solid" BorderWidth="1px" BorderColor="#6acc39" Style="text-align: right" Font-Size="11px"
                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ordqty")).ToString("#,##0;(#,##0); ") %>'
                                        Width="70px"></asp:TextBox>
                                </ItemTemplate>

                                <FooterTemplate>
                                    <asp:Label ID="lblFoterRev" runat="server" BackColor="Transparent"
                                        BorderStyle="None" Height="18px" Style="text-align: right" Font-Size="11px" Width="70px"></asp:Label>

                                </FooterTemplate>

                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                <ItemStyle HorizontalAlign="Right" />
                                <FooterStyle HorizontalAlign="Right" />
                            </asp:TemplateField>
                              <asp:TemplateField HeaderText="">
                                   <HeaderTemplate>
                                      <asp:Label runat="server" ID="txtSearchAutoartcle"  Text='<%# this.GetArticle() %>'></asp:Label>
                                    </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="txtgAutoArtno" runat="server"                                       
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "autoartcle")) %>'
                                        Width="90px"></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>
                             <asp:TemplateField HeaderText="Unit">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtgvUnit" runat="server" 
                                        BackColor="Transparent" BorderStyle="Solid" BorderWidth="1px" BorderColor="#6acc39" Font-Size="11px"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sirunit")) %>'
                                        Width="50px"></asp:TextBox>
                                </ItemTemplate>                               
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>
                             
                           
                               <asp:TemplateField HeaderText="POSSIBLE <br> SIZE RANGE" >
                                <ItemTemplate>
                                    <asp:TextBox ID="txtgSizernge" runat="server" AutoCompleteType="Disabled"
                                         Font-Size="11px" BorderStyle="solid" BorderWidth="1px" BorderColor="#6acc39"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sizernge")) %>'
                                        Width="70px"></asp:TextBox>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="SAMPLE SIZE" >
                                <ItemTemplate>
                                    <asp:TextBox ID="txtgSize" runat="server" AutoCompleteType="Disabled"
                                          Font-Size="11px" BorderStyle="solid" BorderWidth="1px" BorderColor="#6acc39"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "samsize")) %>'
                                        Width="70px"></asp:TextBox>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>
                               <asp:TemplateField HeaderText="CONSUMPTION <br> SIZE" >
                                <ItemTemplate>
                                    <asp:TextBox ID="txtgconSize" runat="server" AutoCompleteType="Disabled"
                                        BackColor="Transparent" BorderStyle="solid" BorderWidth="1px" BorderColor="#6acc39"
                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "consize")) %>'
                                        Width="70px"></asp:TextBox>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>
                              
                            
                          
                            <asp:TemplateField HeaderText="ATTACHMENT">
                                <ItemTemplate>
                                    <a target="_blank" id="LInkbtn" runat="server" href='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "attchmnt")) %>'> View Doc <span class="fa fa-eye"></span></a>
                                  
                                </ItemTemplate>

                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                <ItemStyle HorizontalAlign="Right" />
                              
                            </asp:TemplateField>                          
                               <asp:TemplateField HeaderText="">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lbAddMore" runat="server"   CommandArgument="lbAddMore"
                                                OnClick="AddMore_Click" CssClass="text-primary"><i class="fa fa-plus"></i></asp:LinkButton>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                              </asp:TemplateField>
                               <asp:TemplateField HeaderText="">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="LbtnApprove" OnClick="LbtnApprove_Click" runat="server"  OnClientClick="return  confirm('Do You want to Approve?')" CssClass="text-apple"><i class="fa fa-thumbs-up"></i></asp:LinkButton>
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
            <div class="container-fluid">

         
                        <div id="myModal" class="modal animated slideInLeft sizecolor" role="dialog">
                <div class="modal-dialog ">
                    <div class="modal-content  ">
                        <div class="modal-header">                           
                            <h4 class="modal-title">
                                <span class="fa fa-table"></span> Update Actual Color & Sizes  </h4>
                        </div>
                        <div class="modal-body">
                            <div class="row">
                                 <div class="col-md-3">
                                     <div class="label label-success"><big>Select Color</big></div>
                             <asp:GridView ID="gvColor" runat="server" AutoGenerateColumns="False" Height="90px"
                                                      CssClass=" table-striped table-hover table-bordered grvContentarea" Width="180px"
                                                        Font-Size="11px">
                                                        <FooterStyle BackColor="Purple" Font-Bold="True" Font-Size="11px" ForeColor="White" />
                                                        <Columns>                                                          
                                                              
                                                            <asp:TemplateField HeaderText="Sl.">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvStyleSl0" runat="server" Style="text-transform: capitalize; text-align: left"
                                                                        Text='<%# Convert.ToDouble(Container.DataItemIndex+1).ToString("0")+"." %>' Width="1px"></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Color ID" Visible="False">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvColorID" runat="server" Style="text-transform: capitalize" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "colorid")) %>'
                                                                        Width="51px"></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <ItemTemplate>
                                                                    <asp:CheckBox ID="gvChkColor1" runat="server" Checked='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "colorselect"))=="Y" %>'
                                                                       ForeColor="Blue" Style="font-size: 11px" />
                                                                </ItemTemplate>
                                                               
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Color">
                                                                
                                                                <HeaderStyle HorizontalAlign="Left" />
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtColorDesc" runat="server" BorderStyle="None" Style=" text-align: left;"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "colordesc")).Trim() %>'
                                                                        Width="60px"></asp:TextBox>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                        <FooterStyle CssClass="grvFooter" />
                                                        <EditRowStyle />
                                                        <AlternatingRowStyle />
                                                        <PagerStyle CssClass="gvPagination" />
                                                        <HeaderStyle CssClass="grvHeader" />
                                                    </asp:GridView> 
                            </div>
                            <div class="col-md-2">
                                 <div class="label label-success"><big>Select Size Range</big></div>
                             <asp:GridView ID="gvSize" runat="server" AutoGenerateColumns="False" Height="90px"                                                      
                                                    CssClass=" table-striped table-hover table-bordered grvContentarea" Width="148px"
                                                        Font-Size="11px">
                                                        <FooterStyle BackColor="Purple" Font-Bold="True" Font-Size="11px" ForeColor="White" />
                                                        <Columns>
                                                          
                                                            <asp:TemplateField HeaderText="Sl.">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvStyleSl1" runat="server" Style="text-transform: capitalize; text-align: left"
                                                                        Text='<%# Convert.ToDouble(Container.DataItemIndex+1).ToString("0")+"." %>' Width="1px"></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Size ID" Visible="False">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvSizeID" runat="server" Style="text-transform: capitalize" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sizeid")) %>'
                                                                        Width="51px"></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <ItemTemplate>
                                                                    <asp:CheckBox ID="gvChkSize1" runat="server" Checked='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sizeselect"))=="Y" %>'
                                                                         ForeColor="Blue" Style="font-size: 11px" />
                                                                </ItemTemplate>
                                                               
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Size">
                                                            
                                                                <HeaderStyle HorizontalAlign="Left" />
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtgvSizeDesc" runat="server" Style="border-top-width: 1px; border-left-width: 1px; font-size: 11px; border-bottom-width: 1px; text-align: left; border-right-width: 1px"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sizedesc")).Trim() %>'
                                                                        Width="40px"></asp:TextBox>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                        <FooterStyle CssClass="grvFooter" />
                                                        <EditRowStyle />
                                                        <AlternatingRowStyle />
                                                        <PagerStyle CssClass="gvPagination" />
                                                        <HeaderStyle CssClass="grvHeader" />

                                                    </asp:GridView>
                            </div>
                            <div class="col-md-2">
                                  <div class="label label-success"><big>Select Sample Size</big></div>
                                  <asp:GridView ID="gvsamsize" runat="server" AutoGenerateColumns="False" Height="90px"                                                      
                                                    CssClass=" table-striped table-hover table-bordered grvContentarea" Width="148px"
                                                        Font-Size="11px">
                                                        <FooterStyle BackColor="Purple" Font-Bold="True" Font-Size="11px" ForeColor="White" />
                                                        <Columns>
                                                          
                                                            <asp:TemplateField HeaderText="Sl.">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvsamsizeSl1" runat="server" Style="text-transform: capitalize; text-align: left"
                                                                        Text='<%# Convert.ToDouble(Container.DataItemIndex+1).ToString("0")+"." %>' Width="1px"></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Size ID" Visible="False">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvsamSizeID" runat="server" Style="text-transform: capitalize" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sizeid")) %>'
                                                                        Width="51px"></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <ItemTemplate>
                                                                    <asp:CheckBox ID="gvsamChkSize1" runat="server" Checked='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sizeselect"))=="Y" %>'
                                                                         ForeColor="Blue" Style="font-size: 11px" />
                                                                </ItemTemplate>
                                                               
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Size">
                                                            
                                                                <HeaderStyle HorizontalAlign="Left" />
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtgvsamSizeDesc" runat="server" Style="border-top-width: 1px; border-left-width: 1px; font-size: 11px; border-bottom-width: 1px; text-align: left; border-right-width: 1px"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sizedesc")).Trim() %>'
                                                                        Width="40px"></asp:TextBox>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                        <FooterStyle CssClass="grvFooter" />
                                                        <EditRowStyle />
                                                        <AlternatingRowStyle />
                                                        <PagerStyle CssClass="gvPagination" />
                                                        <HeaderStyle CssClass="grvHeader" />

                                                    </asp:GridView>
                            </div>
                            <div class="col-md-2">
                                  <div class="label label-success"><big>Select Consumption Size</big></div>
                                  <asp:GridView ID="gvconsize" runat="server" AutoGenerateColumns="False" Height="90px"                                                      
                                                    CssClass=" table-striped table-hover table-bordered grvContentarea" Width="148px"
                                                        Font-Size="11px">
                                                        <FooterStyle BackColor="Purple" Font-Bold="True" Font-Size="11px" ForeColor="White" />
                                                        <Columns>
                                                          
                                                            <asp:TemplateField HeaderText="Sl.">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvconsizeSl1" runat="server" Style="text-transform: capitalize; text-align: left"
                                                                        Text='<%# Convert.ToDouble(Container.DataItemIndex+1).ToString("0")+"." %>' Width="1px"></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Size ID" Visible="False">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvconSizeID" runat="server" Style="text-transform: capitalize" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sizeid")) %>'
                                                                        Width="51px"></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <ItemTemplate>
                                                                    <asp:CheckBox ID="gvconChkSize1" runat="server" Checked='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sizeselect"))=="Y" %>'
                                                                         ForeColor="Blue" Style="font-size: 11px" />
                                                                </ItemTemplate>
                                                               
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Size">
                                                            
                                                                <HeaderStyle HorizontalAlign="Left" />
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="txtgvconSizeDesc" runat="server" Style="border-top-width: 1px; border-left-width: 1px; font-size: 11px; border-bottom-width: 1px; text-align: left; border-right-width: 1px"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sizedesc")).Trim() %>'
                                                                        Width="40px"></asp:TextBox>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                        <FooterStyle CssClass="grvFooter" />
                                                        <EditRowStyle />
                                                        <AlternatingRowStyle />
                                                        <PagerStyle CssClass="gvPagination" />
                                                        <HeaderStyle CssClass="grvHeader" />

                                                    </asp:GridView>
                            </div>
                                <div class="col-md-2">
                                    <div class="form-group">
                                        <asp:DropDownList ID="ddluploadtype" runat="server" CssClass="form-control">
                                            <asp:ListItem Value="samimg" Selected="True">Sample Image</asp:ListItem>
                                             <asp:ListItem Value="doc">Document</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <cc1:AsyncFileUpload OnClientUploadError="uploadError"
                                                                    OnClientUploadComplete="uploadComplete" runat="server"
                                                                    ID="AsyncFileUpload1" UploaderStyle="Modern"
                                                                    CompleteBackColor="White"
                                                                    UploadingBackColor="#CCFFFF" ThrobberID="imgLoader"
                                                                    OnUploadedComplete="FileUploadComplete" />
                                  <asp:Image ID="Uploadedimg" runat="server" CssClass="img img-thumbnail img-responsive" />
                                                                                
                                </div>
                            </div>
                           

                        </div>
                        <div class="modal-footer ">
                            <asp:Label ID="lblStylecode" runat="server" Visible="false"></asp:Label>
                              <asp:LinkButton ID="lblbtnSave" runat="server" CssClass="btn btn-sm btn-success" OnClientClick="CLoseMOdal();" OnClick="lblbtnSave_Click"><span class="glyphicon glyphicon-save"></span> Update </asp:LinkButton>
                          
                               <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>


                        </div>
                    </div>
                </div>
            </div>

        <div class="modal fade" id="Buyer" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
        <div class="modal-dialog modal-sm">
            <div class="modal-content">
                <div class="modal-header modal-header-success">                   
                    <h5><i class="glyphicon glyphicon-thumbs-up"></i> Add New Client</h5>
                </div>
                <div class="modal-body form-horizontal">
                    <div class="form-group">
                        <asp:TextBox ID="TxtBuyer" runat="server" placeholder="Buyer Name" CssClass="form-control"></asp:TextBox>
                           </div>
                    <div class="form-group">
                      <asp:TextBox ID="TxtAddress" runat="server" placeholder="Address" Rows="7" TextMode="MultiLine" CssClass="form-control"></asp:TextBox>
           
                    </div>
                </div>
                <div class="modal-footer">
                    <asp:LinkButton ID="SaveBuyer" CssClass="btn btn-success" runat="server" OnClick="SaveBuyer_Click" OnClientClick="CLoseMOdal();">Save Buyer</asp:LinkButton>
                    <button type="button" class="btn btn-default pull-left" data-dismiss="modal">Close</button>
                </div>
            </div><!-- /.modal-content -->
        </div><!-- /.modal-dialog -->
    </div><!-- /.modal -->
            <!-- add new season modal------------------------->
             <div class="modal fade" id="Season" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
        <div class="modal-dialog modal-sm">
            <div class="modal-content">
                <div class="modal-header modal-header-success">
                  
                    <h5><i class="glyphicon glyphicon-thumbs-up"></i> Add New Season</h5>
                </div>
                <div class="modal-body form-horizontal">
                    <div class="form-group">
                        <asp:TextBox ID="txtSeason" runat="server" placeholder="Season Name" CssClass="form-control"></asp:TextBox>
                           </div>
                  
                </div>
                <div class="modal-footer">
                    <asp:LinkButton ID="LbtnSaveSeason" CssClass="btn btn-success" runat="server" OnClick="LbtnSaveSeason_Click" OnClientClick="CLoseMOdal();">Save Season</asp:LinkButton>
                    <button type="button" class="btn btn-default pull-left" data-dismiss="modal">Close</button>
                </div>
            </div><!-- /.modal-content -->
        </div><!-- /.modal-dialog -->
    </div><!-- /.modal -->
            <!-------------------brand name entry modal-------------------------->
       <div class="modal fade" id="BrandModal" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
        <div class="modal-dialog modal-sm">
            <div class="modal-content">
                <div class="modal-header modal-header-success">
                   
                    <h5><i class="glyphicon glyphicon-thumbs-up"></i> Add New Brand Name</h5>
                </div>
                <div class="modal-body form-horizontal">
                    <div class="form-group">
                        <asp:TextBox ID="txtBrandName" runat="server" placeholder="Please Type Brand Name" CssClass="form-control"></asp:TextBox>
                           </div>
                  
                </div>
                <div class="modal-footer">
                    <asp:LinkButton ID="lbtnSaveBrand" CssClass="btn btn-success" runat="server" OnClick="lbtnSaveBrand_Click" OnClientClick="CLoseMOdal();">Save Brand</asp:LinkButton>
                    <button type="button" class="btn btn-default pull-left" data-dismiss="modal">Close</button>
                </div>
            </div><!-- /.modal-content -->
        </div><!-- /.modal-dialog -->
    </div><!-- /.modal -->
                <div class="modal fade" id="StyleModal" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
        <div class="modal-dialog modal-sm">
            <div class="modal-content">
                <div class="modal-header modal-header-success">
                   
                    <h5><i class="glyphicon glyphicon-thumbs-up"></i> Add New Style Name</h5>
                </div>
                <div class="modal-body form-horizontal">
                    <div class="form-group">
                        <asp:TextBox ID="txtStyleName" runat="server" placeholder="Please Type Style Name" CssClass="form-control"></asp:TextBox>
                           </div>
                  
                </div>
                <div class="modal-footer">
                    <asp:LinkButton ID="LbtnSaveStyle" CssClass="btn btn-success" runat="server" OnClick="LbtnSaveStyle_Click" OnClientClick="CLoseMOdal();">Save Style</asp:LinkButton>
                    <button type="button" class="btn btn-default pull-left" data-dismiss="modal">Close</button>
                </div>
            </div><!-- /.modal-content -->
        </div><!-- /.modal-dialog -->
    </div><!-- /.modal -->
              
               </div>
          
        </ContentTemplate>
    </asp:UpdatePanel>  
    <script type="text/javascript">
        function uploadComplete(sender) {
            $get("<%=((Label)this.Master.FindControl("lblmsg")).ClientID%>").style.color = "green";
            $get("<%=((Label)this.Master.FindControl("lblmsg")).ClientID%>").innerHTML = "File Uploaded Successfully";
        }

        function uploadError(sender) {
            $get("<%=((Label)this.Master.FindControl("lblmsg")).ClientID%>").style.color = "red";
            $get("<%=((Label)this.Master.FindControl("lblmsg")).ClientID%>").innerHTML = "File upload failed.";
        }


    </script>   
</asp:Content>

