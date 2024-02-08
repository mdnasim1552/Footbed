<%@ Page Title="" Language="C#" MasterPageFile="~/SPE.Master" AutoEventWireup="true" CodeBehind="DepartmentAlocation.aspx.cs" Inherits="SPEWEB.F_04_Sampling.DepartmentAlocation" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
      <style type="text/css">
        .chzn-single {
            border-radius: 3px !important;
            height: 29px !important;
        }
        .right-corder-container {
      position: fixed;
    right: 10px;
    top: 308px;
}


.right-corder-container .right-corder-container-button {
    height: 45px;
    width: 45px;
    border: none;
    background-color: #6FB583;
    /*border-radius: 62px;*/
    transition: all 300ms;
    box-shadow: 2px 2px 5px rgb(25 73 15);
    cursor: pointer;
}

.right-corder-container .right-corder-container-button span {
    font-size: 24px;
    color: white;
    position: absolute;
    left: 14px;
    top: 10px;
    line-height: 28px;
}
    </style>
        <script type="text/javascript" language="javascript">
        window.onload = function () {
            var strCook = document.cookie;
            if (strCook.indexOf("!~") != 0) {
                var intS = strCook.indexOf("!~");
                var intE = strCook.indexOf("~!");
                var strPos = strCook.substring(intS + 2, intE);
                document.getElementById("grdWithScroll").scrollTop = strPos;
                //console.log("Position"+strPos);
            }
        }
        function SetDivPosition() {
            var intY = document.getElementById("grdWithScroll").scrollTop;
            console.log(intY);
            document.cookie = "yPos=!~" + intY + "~!";
        }

        function SetPosition() {
            var strCook = document.cookie;
            if (strCook.indexOf("!~") != 0) {
                var intS = strCook.indexOf("!~");
                var intE = strCook.indexOf("~!");
                var strPos = strCook.substring(intS + 2, intE);
                document.getElementById("grdWithScroll").scrollTop = strPos;
                console.log("Position" + strPos);
            }
        }
    </script>
    <script type="text/javascript" language="javascript">
        function openModal() {
            $('#SpecificationModal').modal('show');

        }
        function CLoseMOdal() {
            $('#myModal').modal('hide');
            $('#SpecificationModal').modal('hide');            
        }
       
        $(document).ready(function () {
          
            $('#chkCol').change(function () {
                SetPosition();
            });

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
                   
                                    <div class="col-md-2 hidden">
                                        <asp:DropDownList ID="ddlinqno" AutoPostBack="true" OnSelectedIndexChanged="ddlinqno_SelectedIndexChanged" CssClass="form-control inputTxt chzn-select" runat="server"></asp:DropDownList>
                                    </div>
                                     <div class="col-md-2 hidden">
                                        <asp:DropDownList ID="ddlStyle" AutoPostBack="true" OnSelectedIndexChanged="ddlStyle_SelectedIndexChanged" CssClass="form-control inputTxt chzn-select" runat="server"></asp:DropDownList>
                                    </div>
                        <div class="col-md-2 hidden">
                                              <asp:LinkButton ID="lbtnOk" runat="server" Text="Ok" OnClick="lbtnOk_Click" CssClass="btn btn-primary btn-sm" TabIndex="4"></asp:LinkButton>                    
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
                          <div id="collapseOne" class="collapse show" aria-labelledby="headingOne" data-parent="#accordion" >
                            
                            <div class="card-body pt-1" >                                                                     
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
                                            Width="479px" OnRowDataBound="gvCost_RowDataBound" >
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
                                                <asp:CommandField ShowDeleteButton="True" Visible="false" ControlStyle-CssClass="text-red" ItemStyle-CssClass="DeleteBtn"    DeleteText="<span class='fa  fa-trash'></span>" />
                                               
                                                <asp:TemplateField>
                                                                    <ItemTemplate>
                                                                        <asp:CheckBox ID="chkCol" runat="server" ClientIDMode="Static" />
                                                                    </ItemTemplate>                                                                  
                                                                    <HeaderStyle Font-Bold="True" Font-Size="12px" />
                                                                </asp:TemplateField>

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
                                                        <asp:LinkButton ID="lblgvspcfdesc" ToolTip="Click For Change Specifications"  runat="server" BorderStyle="None" Style="text-align: left" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "spcfdesc")) %>'
                                                            Width="250px"></asp:LinkButton>
                                                    </ItemTemplate>
                                                  
                                                    <HeaderStyle Font-Bold="True" Font-Size="10px" ForeColor="#333333" />
                                                    <ItemStyle Font-Size="10px" />
                                                </asp:TemplateField>




                                                <asp:TemplateField HeaderStyle-Font-Bold="true" HeaderStyle-Font-Size="10px" HeaderStyle-ForeColor="#333333" ItemStyle-HorizontalAlign="Right"
                                                    HeaderText="CONS/ PAIR">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtgvconqty" Enabled="false" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                                            BorderWidth="1px" CssClass="GridItmTextBoxRight"
                                                            Font-Size="10px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "conqty")).ToString("#,##0.000000;(#,##0.000000); ") %>'
                                                            Width="50px"></asp:TextBox>
                                                        
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
                                                        <asp:TextBox ID="txtgvwestpc" Enabled="false" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                                            BorderWidth="1px" CssClass="GridItmTextBoxRight"
                                                            Font-Size="10px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "westpc")).ToString("#,##0.000;(#,##0.000); ") %>'
                                                            Width="40px"></asp:TextBox>
                                                    </ItemTemplate>

                                                    <HeaderStyle Font-Bold="True" Font-Size="10px" ForeColor="#333333" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderStyle-Font-Bold="true" HeaderStyle-Font-Size="10px" HeaderStyle-ForeColor="#333333" ItemStyle-HorizontalAlign="Right"
                                                    HeaderText="SUB TOTAL">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtgvqtyCost" Enabled="false" runat="server" BorderStyle="None" CssClass="GridItmTextBoxRight"
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
                                                        <asp:TextBox ID="txtgvqrateCost" Enabled="false" runat="server" BorderStyle="None" Font-Size="12px" 
                                                            Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "rate")).ToString("#,##0.000000;(#,##0.000000); ") %>'
                                                            Width="50px"></asp:TextBox>
                                                               </ItemTemplate>

                                                    <HeaderStyle Font-Bold="True" Font-Size="10px" ForeColor="#333333" />
                                                   
                                                    <ItemStyle Font-Size="10px" />
                                                </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="C&F %">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtgvqcfrate" Enabled="false" runat="server" BorderStyle="None" Font-Size="12px" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "cfrate")).ToString("#,##0.000000;(#,##0.000000); ") %>'
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

                                                        <asp:TextBox ID="txtgvamtCost" Enabled="false" runat="server" BorderStyle="None" Font-Size="10px"
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
                                  
                                </asp:MultiView> 
                            
                 
                                <br />
                                <br />
                            </div>

                        </div>
                                </div>
                             
                              </div>
                                 
                                 </section>
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
                 
                    </div>
            
            <div class="right-corder-container">
                <div class="row">
                <asp:DropDownList ID="ddlProcess" runat="server" CssClass="form-control form-control-sm chzn-select" >
                                            </asp:DropDownList>
                                            <asp:LinkButton ID="lnkAddResouctCost" runat="server" Text="Set" CssClass="btn btn-primary btn-sm "  OnClick="lnkAddResouctCost_Click" TabIndex="1">Set</asp:LinkButton>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    
</asp:Content>
