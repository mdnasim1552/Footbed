<%@ Page Title="" Language="C#" MasterPageFile="~/SPE.Master" AutoEventWireup="true" CodeBehind="ConsumptionSheet.aspx.cs" Inherits="SPEWEB.F_01_Mer.ConsumptionSheet" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
      <style type="text/css">
        .chzn-single {
            border-radius: 3px !important;
            height: 29px !important;
        }
    </style>
    <script type="text/javascript" language="javascript">
        function openModal() {
            $('#SpecificationModal').modal('show');

        }
        function CLoseMOdal() {
            $('#myModal').modal('hide');
            $('#SpecificationModal').modal('hide');            
        }
       
        $(document).ready(function () {
          
          

            //$(".DeleteBtn").click(function () {
            //    alert("Handler for .click() called.");
            //});

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
             <div class="nahidProgressbar">
                <asp:UpdateProgress ID="UpdateProgress2" runat="server" AssociatedUpdatePanelID="UpdatePanel1" DisplayAfter="30">
                    <ProgressTemplate>
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
                     <div class="col-md-2">
                             <div class="form-group">                                
                                        <asp:Label ID="Label1" runat="server" CssClass="label">Date</asp:Label>
                                        <asp:TextBox ID="txtDatefrom" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
                                        <cc1:CalendarExtender ID="datefrom" runat="server" Format="dd-MMM-yyyy" TargetControlID="txtDatefrom"></cc1:CalendarExtender>
                                        <asp:Label ID="Label3" Visible="false" runat="server" CssClass="smLbl_to">Inquiry No</asp:Label>
                                    </div>
                           </div>
                                    <div class="col-md-2 hidden">
                                        <asp:DropDownList ID="ddlinqno" AutoPostBack="true" OnSelectedIndexChanged="ddlinqno_SelectedIndexChanged" CssClass="form-control inputTxt chzn-select" runat="server"></asp:DropDownList>
                                    </div>
                                    <div class="col-md-2 hidden">
                                        <asp:DropDownList ID="ddlStyle" AutoPostBack="true" OnSelectedIndexChanged="ddlStyle_SelectedIndexChanged" CssClass="form-control inputTxt chzn-select" runat="server"></asp:DropDownList>
                                    </div>
                        <div class="col-md-3 ">
                            <div class="form-group" >
                                <asp:LinkButton ID="lbtnOk" runat="server" Text="Ok" OnClick="lbtnOk_Click" CssClass="btn btn-primary btn-sm" TabIndex="4"></asp:LinkButton>
                                <asp:CheckBox ID="ChckVIew" CssClass="chkBoxControl" Text="View All Data" runat="server" AutoPostBack="true" OnCheckedChanged="ChckVIew_CheckedChanged" />
                                <asp:CheckBox ID="ChckCopy" CssClass="chkBoxControl" Text="Copy From" runat="server" AutoPostBack="true" OnCheckedChanged="ChckCopy_CheckedChanged" />

                            </div>
                        </div>
                                  <div class="col-md-2">
                                      <div class="form-group">
                                        <asp:Label ID="LblSelArticle" runat="server" Visible="false" CssClass="label">Select Article</asp:Label>

                                          <asp:DropDownList ID="ddlPreList" Visible="false" runat="server" OnSelectedIndexChanged="ddlPreList_SelectedIndexChanged" CssClass="form-control chzn-select inputTxt" AutoPostBack="true">
                                        </asp:DropDownList>
                                      </div>
                                      </div>                                 
                                    <div class="col-md-1">
                                        <div class="form-group" style="margin-top:20px">
                                               <asp:LinkButton ID="LbtnCopy" OnClientClick="return confirm('Really Do You Want to Copy?')" Visible="false" runat="server" Text="Copy" OnClick="LbtnCopy_Click" CssClass="btn btn-xs btn-success" TabIndex="4"></asp:LinkButton>
                           
                                    </div>
                                </div>
                    </div>
                    </div>
     </div>

              <div class="row" style="min-height:300px;">

                     <div class="col-lg-12">

                         <div id="accordion" class="card-expansion">
                           <!-- .card -->
                        <section class="card card-expansion-item">
                          <header class="card-header border-0" id="headingTwo">
                            <a class="btn btn-reset collapsed" data-toggle="collapse" data-target="#collapseTwo" aria-expanded="false" aria-controls="collapseTwo">
                              <span class="collapse-indicator mr-2">
                                <i class="fa fa-fw fa-caret-right"></i>
                              </span>
                              <span>Basic Information</span>
                            </a>
                          </header>
                          <div id="collapseTwo" class="collapse" aria-labelledby="headingTwo" data-parent="#accordion">
                            <div class="card-body pt-0">                                 
                                    <div class="row">
                                    <div class="col-md-10">
                                    <div class="row">
                                        <div class="col-md-2">
                                        <div class="form-group">
                                        <asp:Label ID="Label11" runat="server" CssClass="label">CLIENT NAME</asp:Label>
                                              <asp:TextBox ID="txtbuyer" runat="server" CssClass="form-control form-control-sm" Enabled="false"></asp:TextBox>
                                              <asp:TextBox ID="txtbuyerid" runat="server" CssClass="form-control form-control-sm" Visible="false"></asp:TextBox>

                                            </div>
                                             </div>
                                        <div class="col-md-2">
                                        <div class="form-group">
                                         <asp:Label ID="Label13" runat="server" CssClass="label">MERCHANDISER</asp:Label>
                                            <asp:TextBox ID="txtmerchand"  runat="server" Enabled="false" CssClass="form-control form-control-sm"></asp:TextBox>

                                            </div>
                                            </div>
                                        <div class="col-md-2">
                                         <div class="form-group">
                                          <asp:Label ID="Label16" runat="server" CssClass="label">BRAND NAME</asp:Label>                                 
                                            <asp:TextBox ID="txtbrand"  runat="server" CssClass="form-control form-control-sm" Enabled="false"></asp:TextBox>
                                        </div>
                                             </div>
                                        <div class="col-md-2">
                                         <div class="form-group">
                                           <asp:Label ID="Label12" runat="server" CssClass="label">TECHNICIAN</asp:Label>
                                            <asp:TextBox ID="txtsampleno"  runat="server" CssClass="form-control form-control-sm"></asp:TextBox>

                                             </div>
                                              </div>
                                        <div class="col-md-2">
                                         <div class="form-group">
                                        <asp:Label ID="lblarticle" runat="server" CssClass="label">ARTICLE NO.</asp:Label>
                                            <asp:TextBox ID="txtArtno" runat="server" CssClass="form-control form-control-sm" Enabled="false"></asp:TextBox>

                                             </div>
                                             </div>
                                        <div class="col-md-2">
                                         <div class="form-group">
                                        <asp:Label ID="lblcategory" runat="server" CssClass="label">STYLE</asp:Label>
                                            <asp:TextBox ID="txtCategory" runat="server" CssClass="form-control form-control-sm" Enabled="false"></asp:TextBox>
                                             </div>
                                              </div>
                                        <div class="col-md-2">
                                         <div class="form-group">
                                         <asp:Label ID="Label7" runat="server" CssClass="label">SIZE RANGE</asp:Label>
                                            <asp:TextBox ID="txtsizernge" runat="server" CssClass="form-control form-control-sm" Enabled="false"></asp:TextBox>

                                             </div>
                                             </div>
                                        <div class="col-md-2">
                                         <div class="form-group">
                                        <asp:Label ID="Label4" runat="server" CssClass="label">COLOR</asp:Label>
                                            <asp:TextBox ID="txtcolor" runat="server" CssClass="form-control form-control-sm" Enabled="false"></asp:TextBox>

                                             </div>
                                            </div>
                                        <div class="col-md-2">
                                         <div class="form-group">
                                          <asp:Label ID="Label8" runat="server" CssClass="label">SAMPLE SIZE</asp:Label>
                                            <asp:TextBox ID="txtsamplesize" runat="server" CssClass="form-control form-control-sm" Enabled="false"></asp:TextBox>

                                             </div>
                                            </div>
                                        <div class="col-md-2">
                                         <div class="form-group">
                                        <asp:Label ID="Label14" runat="server" CssClass="label">SEASON</asp:Label>
                                            <asp:TextBox ID="txtseason" runat="server" CssClass="form-control form-control-sm" Enabled="false"></asp:TextBox>

                                            </div>
                                             </div>
                                         <div class="col-md-2">
                                         <div class="form-group">
                                         <asp:Label ID="Label9" runat="server" CssClass="label">COST SIZE</asp:Label>
                                            <asp:TextBox ID="txtconsize" runat="server" CssClass="form-control form-control-sm" Enabled="false"></asp:TextBox>

                                             </div>
                                             </div>
                                        <div class="col-md-2">
                                         <div class="form-group">
                                         <asp:Label ID="Label15" runat="server" CssClass="label ">LAST/ MOLD</asp:Label>
                                            <asp:TextBox ID="txtlastrefno"  runat="server" CssClass="form-control form-control-sm "></asp:TextBox>

                                             </div>
                                            </div>
                                        <div class="col-md-2">
                                             <div class="form-group">
                                              <asp:Label ID="Label17" runat="server" CssClass="label">CONSTRUCTION</asp:Label>
                                       
                                            <asp:TextBox ID="txtconstruction" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
                                        </div>
                                        </div>
                                          <div class="col-md-2">
                                              <div class="form-group">
                                          <asp:Label ID="Label24" runat="server" CssClass="col-md-2 smLbl_to hidden">Ref Merchand</asp:Label>
                                      
                                            <asp:TextBox ID="RefMarName" BorderStyle="solid" BorderWidth="1px" BorderColor="#6acc39" runat="server"  CssClass="hidden form-control"></asp:TextBox>
                                        </div>
                                              </div>
                                        
                                    </div>
                                        </div>
                                    <div class="col-md-2">
                                        <div class="row">
                                          <section class="card card-figure">
                      <!-- .card-figure -->
                      <figure class="figure">
                        <!-- .figure-img -->
                        <div class="figure-img">
                            <asp:Image ID="Uploadedimg" runat="server" CssClass="img-fluid" />
                           
                          <%--<img class="img-fluid" src="assets/images/dummy/img-5.jpg" alt="Card image cap">--%>
                          <div class="figure-action">   
                                       	<a data-toggle="modal" class="btn btn-xs btn-success" href="#myModal"> <span class="fa fa-image"></span>Click for  Replace Image</a>
                              
                             
                          </div>
                        </div>
                        <!-- /.figure-img -->
                        <!-- .figure-caption -->
                        <figcaption class="figure-caption">
                          <h6 class="figure-title">
                         <span class="fa fa-image"></span> 
                                <asp:HyperLink ID="hypLnkbtn" runat="server" Target="_blank">
                                    Sample Image View
                                        
                                    </asp:HyperLink>
                             
                          </h6>
                          <p class="text-muted mb-0">Note: You can change this image </p>
                                    <asp:LinkButton ID="LbtnSampleImport" OnClick="LbtnSampleImport_Click" OnClientClick="return confirm('Do you want to import from sampling?')" CssClass="btn btn-xs btn-link" runat="server"><span class="fa fa-arrow-circle-down"></span>From Sampling</asp:LinkButton>

                        </figcaption>
                        <!-- /.figure-caption -->
                      </figure>
                      <!-- /.card-figure -->
                    </section>
                                        </div>
                                        </div>
                                        </div>
                                </div>
                              </div>
                            </section>

                             <section class="card card-expansion-item expanded">
                          
                           <header class="card-header border-0 " id="headingOne">
                            <a class="btn btn-reset" data-toggle="collapse" data-target="#collapseOne" aria-expanded="true" aria-controls="collapseOne">
                              <span class="collapse-indicator mr-2">
                                <i class="fa fa-fw fa-caret-right"></i>
                              </span>
                              <span>Input data  
                                           </span>
                            </a>
                            
                          </header>
                          <div id="collapseOne" class="collapse show" aria-labelledby="headingOne" data-parent="#accordion">
                            <div class="card-body pt-0">
                                <div class="row" id="panelCosting" runat="server">
                                     <div class="col-md-1">
                                        <div class="form-group">
                                        <asp:Label ID="Label22" runat="server" CssClass="label">CURRENCY </asp:Label>
                                            <asp:DropDownList ID="ddlCurList" CssClass="form-control form-control-sm" AutoPostBack="true" OnSelectedIndexChanged="ddlCurrency_SelectedIndexChanged" runat="server"></asp:DropDownList>
                                        </div>
                                    </div>
                                     <div class="col-md-1">
                                    <div class="form-group">
                                        <asp:Label ID="lblExchangerate" runat="server" CssClass="label">EXCH. RATE </asp:Label>
                                       
                                            <asp:TextBox ID="txtExchngerate"  runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
                                        </div>
                                    </div>
                                     <div class="col-md-1">
                                    <div class="form-group">
                                        <asp:Label ID="Label19" runat="server" CssClass="label">TARGET PRICE</asp:Label>
                                      
                                            <asp:TextBox ID="txttarprice"  runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
                                        </div>
                                    </div>
                                     <div class="col-md-1">
                                    <div class="form-group">
                                        <asp:Label ID="Label20" runat="server" CssClass="label">OFFER PRICE</asp:Label>
                                       
                                            <asp:TextBox ID="txtoffprice"  runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
                                        </div>
                                    </div>
                                     <div class="col-md-1">
                                    <div class="form-group">
                                        <asp:Label ID="Label21" runat="server" CssClass="label">CONF. PRICE</asp:Label>
                                        
                                            <asp:TextBox ID="txtconfrmprice"  runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
                                        </div>
                                    </div>
                                     <div class="col-md-1">
                                        <div class="form-group">
                                        <asp:Label ID="Label23" runat="server" CssClass="label">EST. PRICE</asp:Label>
                                       <div class="input-group input-group-sm input-group-alt">
                                            <asp:TextBox ID="txtEstimated" Enabled="false"   runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
                                           <div class="input-group-append">                              
                                          <asp:Label ID="Label25" runat="server" CssClass="input-group-text text-success">USD</asp:Label>
                                                               
                            </div>
                                       </div>
                                        </div>
                                    </div>  
                                    <div class="col-md-1">
                                        <div class="form-group">
                                             <asp:Label ID="Label5" runat="server" CssClass="label">ORDER QTY</asp:Label>
                                      
                                            <asp:TextBox ID="txtordqty"  runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
                                       
                                            </div>
                                        </div>
                                </div>
                              <div class="row" id="ProcessPanel" runat="server">
                                  <div class="col-md-2">
                                    <div class="form-group">                                    
                                            <asp:Label ID="lblProcess" runat="server" CssClass="label" Text="DEPARTMENT"></asp:Label>
                                      <div class="input-group input-group-sm input-group-alt">
                                            <asp:DropDownList ID="ddlProcess" runat="server" CssClass="form-control form-control-sm chzn-select" AutoPostBack="true" OnSelectedIndexChanged="ddlProcess_SelectedIndexChanged">
                                            </asp:DropDownList>
                                             <div class="input-group-append">  
                                        <asp:LinkButton ID="LbtnImport" OnClick="LbtnImport_Click" OnClientClick="return confirm('Do You want to import?')" runat="server" Visible="false" CssClass="input-group-text text-success"><span class="fa fa-link"></span></asp:LinkButton>

                                                 </div>
                                        </div>
                                        </div>
                                      </div>
                                  <div class="col-md-2">
                                       <div class="form-group">   
                                   <asp:LinkButton ID="LbtnComponent" CssClass="label" runat="server" OnClick="LbtnComponent_Click" Text="COMPONENT NAME"></asp:LinkButton>
                                                                                  
                                            <asp:DropDownList ID="ddlComponent" runat="server"  CssClass="form-control form-control-sm chzn-select"></asp:DropDownList>

                                       </div> 
                                      </div> 
                                   <div class="col-md-4">
                                       <div class="form-group">
                                    <asp:LinkButton ID="lblProcess0" runat="server" CssClass="label" OnClick="imgbtnResourceCost_Click" Text="MATERIALS NAME"></asp:LinkButton>
                                           <asp:DropDownList ID="ddlResourcesCost"  runat="server" CssClass="form-control form-control-sm chzn-select">
                                            </asp:DropDownList>
                                       </div>
                                           </div>
                                   <div class="col-md-1">
                                           <div class="form-group" style="margin-top:20px;">
                                            <asp:LinkButton ID="lnkAddResouctCost" runat="server" Text="Add Table" OnClick="lnkAddResouctCost_Click" CssClass="btn btn-primary btn-sm " TabIndex="1">Add</asp:LinkButton>
                                            <asp:HyperLink NavigateUrl='~/F_21_GAcc/AccResourceCode?Type=Matcode' Target="_blank" ID="lnkComponent" runat="server" Text="Add Table" CssClass="btn btn-sm btn-success primaryBtn" TabIndex="1">&nbsp; <span class="fa fa-plus"></span> &nbsp;</asp:HyperLink>
                                        </div>
                                        </div>

                                       
                                        
                                    </div>
                                                                   
                                    <div class="col-md-4">
                                        <div class="form-group">                                          
                                        <div class="col-md-2 pading5px hidden">
                                            <asp:DropDownList ID="ddlcolor" runat="server" CssClass="form-control inputTxt chzn-select">
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-md-3 hidden">
                                            <asp:Label ID="lblsize" runat="server" CssClass="smLbl_to" Text="Con. Size"></asp:Label>
                                            <asp:DropDownList ID="ddlconsize" runat="server" Width="120px" CssClass=" inputTxt chzn-select">
                                            </asp:DropDownList>
                                        </div>
                                        </div>
                                        <div class="form-group hidden">                                            
                                            <asp:Label ID="lblSpcf" runat="server" CssClass="col-md-3 smLbl_to" Text="Description"></asp:Label>
                                       
                                        <div class="col-md-5 pading5px">
                                            <asp:DropDownList ID="ddlSpcfcode" runat="server" CssClass="form-control inputTxt chzn-select">
                                            </asp:DropDownList>
                                        </div>
                                     
                                        </div>
                                         
                                 </div>
                                
                          

                                <div class="row">
                            <div class="col-md-12">
                                <asp:MultiView ID="MultiView1" runat="server">
                                    <asp:View ID="Consumption" runat="server">
                                        <asp:GridView ID="gvCost" runat="server" AutoGenerateColumns="False" ShowFooter="True" CssClass="table-striped table-hover table-bordered grvContentarea"
                                            Width="479px" OnRowDeleting="gvCost_RowDeleting" OnRowDataBound="gvCost_RowDataBound">
                                            <Columns>
                                                <asp:TemplateField HeaderStyle-Font-Bold="true" HeaderStyle-Font-Size="12px" HeaderStyle-ForeColor="#333333"
                                                    HeaderText="SL" ItemStyle-Font-Size="12px">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvsl0" runat="server" Text='<%# Convert.ToInt32(Container.DataItemIndex+1).ToString("0")+"." %>'
                                                            Width="20px" Style="text-align: left"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle Font-Bold="True" Font-Size="12px" ForeColor="#333333" />
                                                    <ItemStyle Font-Size="12px" />
                                                </asp:TemplateField>
                                              <%--  OnClientClick="return confirm('Are you sure you want to delete?');"--%>
                                                <asp:CommandField ShowDeleteButton="True" ControlStyle-CssClass="text-red" ItemStyle-CssClass="DeleteBtn"    DeleteText="<span class='fa  fa-trash'></span>" />
                                               
                                               

                                                <asp:TemplateField HeaderStyle-Font-Bold="true" HeaderStyle-Font-Size="10px" HeaderStyle-ForeColor="#333333"
                                                    HeaderText="DEPARTMENT NAME" ItemStyle-Font-Size="10px" ItemStyle-HorizontalAlign="Left">
                                                     <HeaderTemplate>
                                                    <table>

                                                        <tr>
                                                             <th class="">DEPARTMENT NAME                                                              
                                                            </th>
                                                            <th class="pull-right">
                                                                <asp:HyperLink ID="hlbtnRdataExel" runat="server" BackColor="#000066" ToolTip="Export Excel"
                                                                    BorderColor="White" BorderStyle="Solid" BorderWidth="1px" Font-Bold="True"
                                                                    ForeColor="White" Style="text-align: center; margin-left: 10px;" Width="20px"><span class="fa fa-file-excel"></span></asp:HyperLink>
                                                            </th>
                                                        </tr>
                                                    </table>
                                                </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:DropDownList ID="DdlDepartmnet" Width="160px" runat="server" CssClass="form-control form-control-sm"></asp:DropDownList>
                                                        <asp:Label ID="lblgvdept" runat="server" Visible="false" BorderStyle="None" Style="text-align: left" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "prodesc")) %>'
                                                            Width="150px"></asp:Label>
                                                          <asp:Label ID="lblgvDeptcode" runat="server" Visible="false" BorderStyle="None" Style="text-align: left" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "procode")) %>'
                                                            Width="150px"></asp:Label>

                                                    </ItemTemplate>
                                                    <HeaderStyle Font-Bold="True" Font-Size="10px" ForeColor="#333333" />
                                                    <ItemStyle Font-Size="10px" />

                                                </asp:TemplateField>
                                                   
                                                <asp:TemplateField HeaderStyle-Font-Bold="true" HeaderStyle-Font-Size="10px" HeaderStyle-ForeColor="#333333"
                                                    HeaderText="COMPONENT NAME" ItemStyle-Font-Size="10px">

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
                                                    HeaderText="MATERIAL NAME" ItemStyle-Font-Size="10px">

                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvspcfcode" runat="server" Visible="false" BorderStyle="None" Style="text-align: left" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "spcfcode")) %>'
                                                            Width="0px"></asp:Label>
                                                        <asp:LinkButton ID="lblgvspcfdesc" ToolTip="Click For Change Specifications" OnClick="lblgvspcfdesc_Click"  runat="server" BorderStyle="None" Style="text-align: left" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "spcfdesc")) %>'
                                                            Width="250px"></asp:LinkButton>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label ID="lbltoalf" runat="server">Direct Material Cost</asp:Label>
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
                                                            <asp:RequiredFieldValidator ID="rfvqty" runat="server" ControlToValidate="txtgvconqty" EnableClientScript="false" Display="Dynamic" ErrorMessage="QTY invalid" ForeColor="Red" SetFocusOnError="true" />
                                               
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
                                                    ItemStyle-Font-Size="10px" ItemStyle-HorizontalAlign="Right" HeaderText="PRICE/ UNIT">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtgvqrateCost" runat="server" BorderStyle="None" Font-Size="12px" AutoPostBack="true" OnTextChanged="txtgvqrateCost_TextChanged"
                                                            Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "rate")).ToString("#,##0.000000;(#,##0.000000); ") %>'
                                                            Width="50px"></asp:TextBox>
                                                               </ItemTemplate>

                                                    <HeaderStyle Font-Bold="True" Font-Size="10px" ForeColor="#333333" />
                                                   
                                                    <ItemStyle Font-Size="10px" />
                                                </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="C&F %">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtgvqcfrate" runat="server" BorderStyle="None" Font-Size="12px" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "cfrate")).ToString("#,##0.000000;(#,##0.000000); ") %>'
                                                            Width="50px"></asp:TextBox>
                                                               </ItemTemplate>

                                                    <HeaderStyle Font-Bold="True" Font-Size="10px" ForeColor="#333333" />
                                                   
                                                    <ItemStyle Font-Size="10px" />
                                                </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="F.C&F Rate">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvqfcfrate" runat="server" BorderStyle="None" Font-Size="12px" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "fcfrate")).ToString("#,##0.000000;(#,##0.000000); ") %>'
                                                            Width="50px"></asp:Label>
                                                               </ItemTemplate>

                                                    <HeaderStyle Font-Bold="True" Font-Size="10px" ForeColor="#333333" />
                                                   
                                                    <ItemStyle Font-Size="10px" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="TOTAL <br> IN USD">
                                                    <ItemTemplate>

                                                        <asp:TextBox ID="txtgvamtCost" runat="server" BorderStyle="None" Font-Size="10px"
                                                            Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt")).ToString("#,##0.000000;(#,##0.000000); ") %>'
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
                                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "convrate")).ToString("#,##0.0000;(#,##0.0000); ") %>'
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
                                        <br />
                                     
                                    </asp:View>
                                    <asp:View ID="RptCon" runat="server">
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
                                                             <th class="">DEPARTMENT NAME                                                              
                                                            </th>
                                                            <th class="pull-right">
                                                                <asp:HyperLink ID="hlbtnRdataExel2" runat="server" BackColor="#000066" ToolTip="Export Excel"
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
                                                    HeaderText="COMPONENT NAME" ItemStyle-Font-Size="10px">

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
                                                    HeaderText="MATERIALS NAME" ItemStyle-Font-Size="10px">

                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvspcfcode" runat="server" Visible="false" BorderStyle="None" Style="text-align: left" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "spcfcode")) %>'
                                                            Width="0px"></asp:Label>
                                                        <asp:Label ID="lblgvspcfdesc" runat="server" BorderStyle="None" Style="text-align: left" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "spcfdesc")) %>'
                                                            Width="250px"></asp:Label>
                                                    </ItemTemplate>
                                                  
                                                    <HeaderStyle Font-Bold="True" Font-Size="10px" ForeColor="#333333" />
                                                    <ItemStyle Font-Size="10px" />
                                                </asp:TemplateField>




                                                <asp:TemplateField HeaderStyle-Font-Bold="true" HeaderStyle-Font-Size="10px" HeaderStyle-ForeColor="#333333" ItemStyle-HorizontalAlign="Right"
                                                    HeaderText="CON/ PAIR">
                                                    <ItemTemplate>
                                                        <asp:Label ID="txtgvconqty" runat="server"
                                                            CssClass="GridItmTextBoxRight"
                                                            Font-Size="10px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "conqty")).ToString("#,##0.000000;(#,##0.000000); ") %>'
                                                            Width="50px"></asp:Label>
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
                                                    HeaderText="PROPOESD <br>WASTAGE %">
                                                    <ItemTemplate>
                                                        <asp:Label ID="txtgvwestpc" runat="server"
                                                            CssClass="GridItmTextBoxRight"
                                                            Font-Size="10px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "westpc")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                            Width="40px"></asp:Label>
                                                    </ItemTemplate>

                                                    <HeaderStyle Font-Bold="True" Font-Size="10px" ForeColor="#333333" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderStyle-Font-Bold="true" HeaderStyle-Font-Size="10px" HeaderStyle-ForeColor="#333333" ItemStyle-HorizontalAlign="Right"
                                                    HeaderText="SUB TOTAL">
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
                                                    ItemStyle-Font-Size="10px" ItemStyle-HorizontalAlign="Right" HeaderText="PRICE/ UNIT">
                                                    <ItemTemplate>
                                                        <asp:Label ID="txtgvqrateCost" runat="server" BorderStyle="None" Font-Size="12px"
                                                            Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "rate")).ToString("#,##0.000000;(#,##0.000000); ") %>'
                                                            Width="50px" ></asp:Label>
                                                       </ItemTemplate>

                                                    <HeaderStyle Font-Bold="True" Font-Size="10px" ForeColor="#333333" />
                                                    <ItemStyle Font-Size="10px" />
                                                </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="C&F %">
                                                    <ItemTemplate>
                                                        <asp:Label ID="txtgvqcfrate" runat="server" BorderStyle="None" Font-Size="12px"
                                                            Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "cfrate")).ToString("#,##0.000000;(#,##0.000000); ") %>'
                                                            Width="50px" ></asp:Label>
                                                       </ItemTemplate>

                                                    <HeaderStyle Font-Bold="True" Font-Size="10px" ForeColor="#333333" />
                                                    <ItemStyle Font-Size="10px" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="F.C&F Rate">
                                                    <ItemTemplate>
                                                        <asp:Label ID="txtgvqFcfrate" runat="server" BorderStyle="None" Font-Size="12px"
                                                            Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "fcfrate")).ToString("#,##0.000000;(#,##0.000000); ") %>'
                                                            Width="50px" ></asp:Label>
                                                       </ItemTemplate>

                                                    <HeaderStyle Font-Bold="True" Font-Size="10px" ForeColor="#333333" />
                                                    <ItemStyle Font-Size="10px" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="TOTAL <br> IN USD">
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
                                    </asp:View>
                                    <asp:View ID="smpsiz" runat="server">                                       
                                        
                                    </asp:View>
                                </asp:MultiView>
                                <br />
                               
                                <div class="col-md-6">
                                    <asp:Panel ID="DirectCost" runat="server" Visible="True">
                                    <asp:GridView ID="gvdircost" runat="server" AutoGenerateColumns="False" ShowFooter="True" CssClass="table-striped table-hover table-bordered grvContentarea"
                                        Width="449px" OnRowDataBound="gvdircost_RowDataBound">
                                        <Columns>
                                            <asp:TemplateField HeaderStyle-Font-Bold="true" HeaderStyle-Font-Size="12px" HeaderStyle-ForeColor="#333333"
                                                HeaderText="" ItemStyle-Font-Size="12px">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvsl0" runat="server" Text='<%# Convert.ToInt32(Container.DataItemIndex+1).ToString("00")+"." %>'
                                                        Width="10px" Style="text-align: left"></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle Font-Bold="True" Font-Size="12px" ForeColor="#333333" />
                                                <ItemStyle Font-Size="12px" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderStyle-Font-Bold="true" HeaderStyle-Font-Size="10px" HeaderStyle-ForeColor="#333333"
                                                HeaderText="" ItemStyle-Font-Size="10px" ItemStyle-HorizontalAlign="Left">

                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvcodeCost" runat="server" Visible="false" Style="text-align: left" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rescode")) %>'
                                                        Width="60px"></asp:Label>

                                                    <asp:HyperLink ID="lblgvDesc" runat="server" Target="_blank" ForeColor="Black"  BorderStyle="None" Style="text-align: left" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "resdesc")) %>'
                                                        Width="400px"></asp:HyperLink>

                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lbltoalf" runat="server">ARTICAL FACTORY COST</asp:Label>
                                                </FooterTemplate>
                                                <HeaderStyle Font-Bold="True" Font-Size="10px" ForeColor="#333333" />
                                                <ItemStyle Font-Size="10px" />
                                                <FooterStyle HorizontalAlign="right" />
                                            </asp:TemplateField>
                                              <asp:TemplateField HeaderText="Percent(%)">
                                                <ItemTemplate>

                                                    <asp:TextBox ID="txtpercnt" runat="server" BorderStyle="Solid" Font-Size="10px" BorderWidth="1px" BorderColor="#42f459"
                                                        Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "percnt")).ToString("#,##0.00;(#,##0.00); ") %>'   Width="60px"></asp:TextBox>
                                                </ItemTemplate>
                                             <FooterTemplate>
                                                        <asp:Label ID="lblgvfamtPrevCost" runat="server" Font-Bold="True" Font-Size="10px" ForeColor="#CC0066"
                                                            Style="text-align: right" Width="60px"></asp:Label>
                                                    </FooterTemplate>
                                                <HeaderStyle Font-Bold="True" Font-Size="10px" ForeColor="#333333" />
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Amount">
                                                <ItemTemplate>

                                                    <asp:TextBox ID="txtgvamtCost" runat="server" BorderStyle="Solid" Font-Size="10px" BorderWidth="1px" BorderColor="#42f459"
                                                        Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt")).ToString("#,##0.000;(#,##0.000); ") %>'
                                                        Width="60px"></asp:TextBox>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lblgvfamtCost" runat="server" Font-Bold="True" Font-Size="10px" ForeColor="#CC0066"
                                                        Style="text-align: right" Width="60px"></asp:Label>
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
                                </asp:Panel>
                                </div>

                                 <div class="col-md-6">
                                    <asp:Panel ID="pnlSmpleSizes" runat="server" Visible="false" CssClass="form-inline">
                                      <div class="row form-inline" style="margin-left:20px">
                                           <div class="col-md-12">
                                                 <asp:Label ID="Label26" runat="server" CssClass="smLbl_to">Notes</asp:Label>
                                        <div class="pading5px">
                                            <asp:TextBox ID="txtNotes" Rows="2" Columns="140" TextMode="MultiLine" BorderStyle="solid" BorderWidth="1px" BorderColor="#6acc39" runat="server" CssClass="form-control"></asp:TextBox>
                                        </div>

                                            </div>
                                            <div class="col-md-12">
                                            <asp:Label ID="Label10" runat="server" CssClass="smLbl_to" Text="Sample Size"></asp:Label>

                                                <asp:GridView ID="grvSmpleSizes" runat="server" AutoGenerateColumns="False" ShowFooter="True" CssClass="table-striped table-hover table-bordered grvContentarea"
                                                    Width="479px">
                                                    <Columns>
                                                        <asp:TemplateField HeaderStyle-Font-Bold="true" HeaderStyle-Font-Size="12px" HeaderStyle-ForeColor="#333333"
                                                            ItemStyle-Font-Size="12px">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblgvsl0" runat="server" Text='<%# Convert.ToInt32(Container.DataItemIndex+1).ToString("00")+"." %>'
                                                                    Width="100%" Style="text-align: left"></asp:Label>
                                                            </ItemTemplate>

                                                            <ItemStyle Font-Size="12px" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderStyle-Font-Bold="true" HeaderStyle-Font-Size="10px" HeaderStyle-ForeColor="#333333"
                                                            HeaderText="Type Desc" ItemStyle-Font-Size="10px" ItemStyle-HorizontalAlign="Left">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="lblgvtypedesc" runat="server" BorderStyle="None" Style="text-align: left" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "typedesc")) %>'
                                                                    Width="100%"></asp:TextBox>
                                                            </ItemTemplate>
                                                            <HeaderStyle Font-Bold="True" Font-Size="10px" ForeColor="#333333" />
                                                            <ItemStyle Font-Size="12px" />
                                                            <FooterStyle HorizontalAlign="left" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderStyle-Font-Bold="true" HeaderStyle-Font-Size="10px" HeaderStyle-ForeColor="#333333"
                                                            HeaderText="" ItemStyle-Font-Size="10px">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="lblgvs1" runat="server" BorderStyle="None" Style="text-align: left" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "s1")) %>'
                                                                    Width="100%"></asp:TextBox>
                                                            </ItemTemplate>
                                                            <HeaderStyle Font-Bold="True" Font-Size="10px" ForeColor="#333333" />
                                                            <ItemStyle Font-Size="12px" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderStyle-Font-Bold="true" HeaderStyle-Font-Size="10px" HeaderStyle-ForeColor="#333333"
                                                            HeaderText="" ItemStyle-Font-Size="10px" ItemStyle-HorizontalAlign="Left">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="lblgvs2" runat="server" BorderStyle="None" Style="text-align: left" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "s2")) %>'
                                                                    Width="100%"></asp:TextBox>
                                                            </ItemTemplate>
                                                            <HeaderStyle Font-Bold="True" Font-Size="10px" ForeColor="#333333" />
                                                            <ItemStyle Font-Size="12px" />
                                                            <FooterStyle HorizontalAlign="left" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderStyle-Font-Bold="true" HeaderStyle-Font-Size="10px" HeaderStyle-ForeColor="#333333"
                                                            HeaderText="" ItemStyle-Font-Size="10px">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="lblgvs3" runat="server" BorderStyle="None" Style="text-align: left" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "s3")) %>'
                                                                    Width="100%"></asp:TextBox>

                                                            </ItemTemplate>

                                                            <HeaderStyle Font-Bold="True" Font-Size="10px" ForeColor="#333333" />
                                                            <ItemStyle Font-Size="12px" />
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderStyle-Font-Bold="true" HeaderStyle-Font-Size="10px" HeaderStyle-ForeColor="#333333"
                                                            HeaderText="" ItemStyle-Font-Size="10px">

                                                            <ItemTemplate>
                                                                <asp:TextBox ID="lblgvs4" runat="server" BorderStyle="None" Style="text-align: left" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "s4")) %>'
                                                                    Width="100%"></asp:TextBox>

                                                            </ItemTemplate>

                                                            <HeaderStyle Font-Bold="True" Font-Size="10px" ForeColor="#333333" />
                                                            <ItemStyle Font-Size="12px" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderStyle-Font-Bold="true" HeaderStyle-Font-Size="10px" HeaderStyle-ForeColor="#333333"
                                                            HeaderText="" ItemStyle-Font-Size="10px">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="lblgvs5" runat="server" BorderStyle="None" Style="text-align: left" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "s5")) %>'
                                                                    Width="100%"></asp:TextBox>

                                                            </ItemTemplate>

                                                            <HeaderStyle Font-Bold="True" Font-Size="10px" ForeColor="#333333" />
                                                            <ItemStyle Font-Size="12px" />
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderStyle-Font-Bold="true" HeaderStyle-Font-Size="10px" HeaderStyle-ForeColor="#333333"
                                                            HeaderText="" ItemStyle-Font-Size="10px">

                                                            <ItemTemplate>
                                                                <asp:TextBox ID="lblgvs6" runat="server" BorderStyle="None" Style="text-align: left" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "s6")) %>'
                                                                    Width="100%"></asp:TextBox>

                                                            </ItemTemplate>

                                                            <HeaderStyle Font-Bold="True" Font-Size="10px" ForeColor="#333333" />
                                                            <ItemStyle Font-Size="12px" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderStyle-Font-Bold="true" HeaderStyle-Font-Size="10px" HeaderStyle-ForeColor="#333333"
                                                            HeaderText="" ItemStyle-Font-Size="10px">

                                                            <ItemTemplate>
                                                                <asp:TextBox ID="lblgvs7" runat="server" BorderStyle="None" Style="text-align: left" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "s7")) %>'
                                                                    Width="100%"></asp:TextBox>

                                                            </ItemTemplate>

                                                            <HeaderStyle Font-Bold="True" Font-Size="10px" ForeColor="#333333" />
                                                            <ItemStyle Font-Size="12px" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderStyle-Font-Bold="true" HeaderStyle-Font-Size="10px" HeaderStyle-ForeColor="#333333"
                                                            HeaderText="" ItemStyle-Font-Size="10px">

                                                            <ItemTemplate>
                                                                <asp:TextBox ID="lblgvs8" runat="server" BorderStyle="None" Style="text-align: left" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "s8")) %>'
                                                                    Width="100%"></asp:TextBox>

                                                            </ItemTemplate>

                                                            <HeaderStyle Font-Bold="True" Font-Size="10px" ForeColor="#333333" />
                                                            <ItemStyle Font-Size="12px" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderStyle-Font-Bold="true" HeaderStyle-Font-Size="10px" HeaderStyle-ForeColor="#333333"
                                                            HeaderText="" ItemStyle-Font-Size="10px">

                                                            <ItemTemplate>
                                                                <asp:TextBox ID="lblgvs9" runat="server" BorderStyle="None" Style="text-align: left" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "s9")) %>'
                                                                    Width="100%"></asp:TextBox>

                                                            </ItemTemplate>

                                                            <HeaderStyle Font-Bold="True" Font-Size="10px" ForeColor="#333333" />
                                                            <ItemStyle Font-Size="12px" />
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderStyle-Font-Bold="true" HeaderStyle-Font-Size="10px" HeaderStyle-ForeColor="#333333"
                                                            HeaderText="" ItemStyle-Font-Size="10px">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="lblgvs10" runat="server" BorderStyle="None" Style="text-align: left" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "s10")) %>'
                                                                    Width="100%"></asp:TextBox>

                                                            </ItemTemplate>
                                                            <HeaderStyle Font-Bold="True" Font-Size="10px" ForeColor="#333333" />
                                                            <ItemStyle Font-Size="12px" />
                                                        </asp:TemplateField>
                                                         <asp:TemplateField HeaderStyle-Font-Bold="true" HeaderStyle-Font-Size="10px" HeaderStyle-ForeColor="#333333"
                                                            HeaderText="" ItemStyle-Font-Size="10px">
                                                            <ItemTemplate>
                                                              <asp:LinkButton ID="lnkAddSmpSiz" runat="server" Text="Add Table" OnClick="lnkAddSmpSiz_Click" CssClass="btn btn-primary primaryBtn " TabIndex="1">Add</asp:LinkButton>
                                                            </ItemTemplate>
                                                            <HeaderStyle Font-Bold="True" Font-Size="10px" ForeColor="#333333" />
                                                            <ItemStyle Font-Size="12px" />
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
                                    
                                </asp:Panel>
                                </div>
                                <br />
                                <br />
                            </div>

                        </div>
                                </div>
                             
                              </div>
                                 
                                 </section>
                       </div>
                                         
                                 
                                
                                    <div class="form-group">                                      
                                        <asp:Label ID="Label18" runat="server" CssClass="col-md-2 smLbl_to hidden">Est Production Date</asp:Label>
                                        <div class="col-md-4  pading5px">
                                            <asp:TextBox ID="txtestproduction" BorderStyle="solid" BorderWidth="1px" BorderColor="#6acc39" runat="server" CssClass="hidden form-control"></asp:TextBox>
                                            <cc1:CalendarExtender ID="txtestproduction_calender" runat="server" Format="dd-MMM-yyyy" TargetControlID="txtestproduction"></cc1:CalendarExtender>

                                        </div>
                                    </div>

                                    <div class="form-group hidden">
                                        <div class="col-md-4 pading5px">
                                            <asp:Label ID="Label6" runat="server" CssClass="smLbl_to">Cons Qty</asp:Label>
                                            <asp:TextBox ID="txtconqty" runat="server" CssClass="inputTxt" Width="132px"></asp:TextBox>
                                            <asp:Label ID="Label2" runat="server" CssClass="smLbl_to">Style Unit</asp:Label>
                                            <asp:TextBox ID="txtsirunit" runat="server" CssClass="inputTxt" Width="128px"></asp:TextBox>

                                        </div>
                                    </div>
                                    
                                </div>
        
                       

                        
               <div id="myModal" class="modal animated slideInLeft" role="dialog">
                <div class="modal-dialog modal-sm">
                    <div class="modal-content ">
                        <div class="modal-header">
                            
                            <h4 class="modal-title">  <span class="fa fa-table"></span>Replace Sample Images </h4>
                        </div>
                        <div class="modal-body">                                                       
                             <asp:Label ID="lblStylecode" runat="server" Visible="false"></asp:Label>
                                <div class="form-group">
                                                 <cc1:AsyncFileUpload OnClientUploadError="uploadError" 
                                                                    OnClientUploadComplete="uploadComplete" runat="server"
                                                                    ID="AsyncFileUpload1" UploaderStyle="Modern"
                                                                    CompleteBackColor="White"
                                                                    UploadingBackColor="#CCFFFF" ThrobberID="imgLoader"
                                                                    OnUploadedComplete="FileUploadComplete" />                                 
                                </div>   
                        </div>
                           

                        
                        <div class="modal-footer ">
                              <asp:LinkButton ID="lblbtnSave" runat="server" CssClass="btn btn-sm btn-success" OnClientClick="CLoseMOdal();" ><span class="glyphicon glyphicon-save"></span> Update </asp:LinkButton>
                              <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                        </div>
                   </div>
                    </div>
             </div>
               <div id="SpecificationModal" class="modal animated slideInLeft" role="dialog">
                <div class="modal-dialog">
                    <div class="modal-content ">
                        <div class="modal-header">
                            
                            <h4 class="modal-title">  <span class="fa fa-table"></span>Change Specifications </h4>
                        </div>
                        <div class="modal-body form-horizontal">                                                       
                             <asp:Label ID="lblHelper" runat="server" Visible="false"></asp:Label>
                                <div class="form-group">
                                             <label class="col-md-4">Specifications</label>            
                                    <div class="col-md-8">
                                        <asp:DropDownList ID="ddlSpecification" CssClass="form-control chzn-select" runat="server">                                             
                                        </asp:DropDownList>
                                    </div>
                                </div>   
                        </div>
                           

                        
                        <div class="modal-footer ">
                              <asp:LinkButton ID="LinkButton1" runat="server" OnClick="LinkButton1_Click" CssClass="btn btn-sm btn-success" OnClientClick="CLoseMOdal();" ><span class="glyphicon glyphicon-save"></span> Update </asp:LinkButton>
                              <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                        </div>
                   </div>
                    </div>
             </div>
                
                    </div>
            

        </ContentTemplate>
    </asp:UpdatePanel>
      <script type="text/javascript">
        function uploadComplete(sender) {
            $('#myModal').modal('hide');
            $get("<%=((Label)this.Master.FindControl("lblmsg")).ClientID%>").style.color = "green";
            $get("<%=((Label)this.Master.FindControl("lblmsg")).ClientID%>").innerHTML = "File Uploaded Successfully";
        }

        function uploadError(sender) {
            $('#myModal').modal('hide');
            $get("<%=((Label)this.Master.FindControl("lblmsg")).ClientID%>").style.color = "red";
            $get("<%=((Label)this.Master.FindControl("lblmsg")).ClientID%>").innerHTML = "File upload failed.";
        }


      </script> 
</asp:Content>
