<%@ Page Title="" Language="C#" MasterPageFile="~/SPE.Master" AutoEventWireup="true" CodeBehind="DocUpload.aspx.cs" Inherits="SPEWEB.F_33_Doc.DocUpload" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
   
      <script type="text/javascript" language="javascript">          
          $(document).ready(function () {

            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);
                    });
        function pageLoaded() {
          
            $('.chzn-select').chosen({ search_contains: true });
        }

      </script>
    <style>
        .chzn-single {
    border-radius: 3px !important;
    height: 29px !important;
    
}
    </style>
       <asp:UpdatePanel ID="UpdatePanel1" runat="server">
           <ContentTemplate>
    <div class="card card-fluid">
                <div class="card-body">
                    <div class="row">
                         <div class="col-md-2">
                             <asp:Label ID="LblDocNo" runat="server"  Text="Doc/Auto Ref. No:" CssClass="control-label"></asp:Label>
                             <asp:Label ID="lblCurMSRNo1" runat="server" Text="Doc No-0001" CssClass="form-control form-control-sm"></asp:Label>
                             </div>
                         <div class="col-md-1">
                              <asp:Label ID="LblDate" runat="server" CssClass="control-label" Text="Date:"></asp:Label>
                              <asp:TextBox ID="txtDate" runat="server" CssClass="form-control form-control-sm"
                                             ToolTip="(dd-MMM-yyyy)" 
                                           ></asp:TextBox>
                                        <cc1:CalendarExtender ID="txtDate_CalendarExtender" runat="server" 
                                            Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txtDate">
                                        </cc1:CalendarExtender>
                             </div>
                         <div class="col-md-1">
                            <div class="btn-group" style="margin-top:20px;" role="group" aria-label="Button group with nested dropdown">
                                <button type="button" class="btn btn-danger btn-sm">Operations</button>
                                <div class="btn-group" role="group">
                                    <button id="btnGroupDrop4" type="button" class="btn btn-danger btn-sm dropdown-toggle" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false"></button>
                                    <div class="dropdown-menu" aria-labelledby="btnGroupDrop4" style="">
                                        <div class="dropdown-arrow"></div>
                                        <asp:HyperLink ID="HyperLink1" runat="server" Target="_blank" CssClass="dropdown-item">Document Interface</asp:HyperLink>
                                        <asp:HyperLink ID="HyperLink2" runat="server" Target="_blank" NavigateUrl="~/F_33_Doc/DocCodeBook?Type=Entry" CssClass="dropdown-item">Document Type Setup</asp:HyperLink>
                                        <asp:HyperLink ID="HyperLink3" runat="server" Target="_blank" NavigateUrl="#" CssClass="dropdown-item">User Permission</asp:HyperLink>
                                        <asp:HyperLink ID="HyperLink4" runat="server" Target="_blank" NavigateUrl="#" CssClass="dropdown-item">Supplier Information</asp:HyperLink>

                                    </div>
                                </div>
                            </div>
                        </div>
                         <div class="col-md-2">
                              <asp:Label ID="Label3" runat="server" CssClass="control-label" Text="Prev Document:" Visible="false"></asp:Label>
                             <asp:DropDownList ID="DdlPrevDoc" runat="server" CssClass="form-control form-control-sm" Visible="false"></asp:DropDownList>

                             </div>
                        </div>
                    <br />
                    </div>
        </div>
                <div class="row">
               <div class="col-md-6">
 <div class="card card-fluid" style="min-height: 300px;">
                <div class="card-body row">
              <div class="col-md-6">
                   <div class="form-group">
                             <asp:Label ID="LblDocTitle" runat="server"  Text="Document Title/Ref No/ P.O No: <abbr class='text-danger' title='Required'>*</abbr>" CssClass="control-label"></asp:Label>
                             <asp:TextBox ID="TxtRefno" placeholder="XX-XXXXXX" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
                             </div>
                   <div class="form-group">
                       <asp:Label ID="LblOrderDat" runat="server" CssClass="control-label" Text="Order Date: <abbr class='text-danger' title='Required'>*</abbr>"></asp:Label>
                              <asp:TextBox ID="TxtOrdDate" runat="server" CssClass="form-control form-control-sm"
                                             ToolTip="(dd-MMM-yyyy)" 
                                           ></asp:TextBox>
                                        <cc1:CalendarExtender ID="CalendarExtender1" runat="server" 
                                            Enabled="True" Format="dd-MMM-yyyy" TargetControlID="TxtOrdDate">
                                        </cc1:CalendarExtender>
                                       </div>
                   <div class="form-group">
                              <asp:Label ID="LBlValue" runat="server" CssClass="control-label" Text="Value/PO Value: <abbr class='text-danger' title='Required'>*</abbr>"></asp:Label>

                       <div class="input-group input-group-sm input-group-alt">
                          <asp:TextBox ID="Txtvalue" placeholder="0.0000" type="number" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
                         <div class="input-group-append ">
                              <span class="input-group-text" id="CurSymbol" runat="server">Not Set</span>
                            </div>
                          </div>

                             </div>
                  <div class="form-group ">
                              <asp:Label ID="Label2" runat="server" CssClass="control-label" Text="Document Type:"></asp:Label>                          
                     
                              <asp:DropDownList ID="DDlDocType" runat="server" CssClass="form-control form-control-sm"></asp:DropDownList>
                                                             

                             </div>
                  </div>
                   
                  <div class="col-md-6">
                   <div class="form-group">
                              <asp:Label ID="LblSupp" runat="server"  Text="Supplier Name: <abbr class='text-danger' title='Required'>*</abbr>" CssClass="control-label"></asp:Label>
                             <asp:DropDownList ID="DdlSupplier"  AutoPostBack="true" OnSelectedIndexChanged="DdlSupplier_SelectedIndexChanged" runat="server" CssClass="form-control form-control-sm chzn-select"></asp:DropDownList>
                       </div>
                   <div class="form-group">
                              <asp:Label ID="LBlDelDat" runat="server" CssClass="control-label" Text="Delivery Date: <abbr class='text-danger' title='Required'>*</abbr>"></asp:Label>
                              <asp:TextBox ID="TxtDelDate" runat="server" CssClass="form-control form-control-sm"
                                             ToolTip="(dd-MMM-yyyy)" 
                                           ></asp:TextBox>
                                        <cc1:CalendarExtender ID="CalendarExtender2" runat="server" 
                                            Enabled="True" Format="dd-MMM-yyyy" TargetControlID="TxtDelDate">
                                        </cc1:CalendarExtender>
                             </div>
                      <div class="form-group">
                              <asp:Label ID="Label1" runat="server" CssClass="control-label">Remarks: <span class="badge badge-secondary">
                            <em>Optional</em>
                          </span></asp:Label>

                          <asp:TextBox ID="TxtRemarks" runat="server" CssClass="form-control form-control-sm" TextMode="MultiLine" 
                                    ></asp:TextBox>
                      </div>
                      <div class="form-group">
                          <br>
<div class="btn btn-secondary btn-sm fileinput-button">
                          <i class="fa fa-plus fa-fw"></i>
                          <span>Add files...</span>
                          <!-- The file input field used as target for the file upload widget -->
                         
                            <asp:FileUpload ID="imgFileUpload" runat="server" Height="26px" 
                                    onchange="submitform();" />
    </div>
                           <asp:LinkButton ID="lbtnUpdate" runat="server"  CssClass="btn btn-sm btn-primary fa-pull-right"
                                   onclick="lbtnUpdate_Click" 
                                    ><span class="fa fa-save"></span> Save</asp:LinkButton>
                      </div>
                      
                  </div>

<div class="col-md-12">
        <asp:GridView ID="gvFileGrid" runat="server" AutoGenerateColumns="False" CssClass=" table-striped table-hover table-bordered grvContentarea"
                               >
                                <RowStyle />
                                <Columns>
                                    <asp:TemplateField HeaderText="Sl">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvSlNo0" runat="server" Font-Bold="True" Height="16px"
                                                Style="text-align: right"
                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Code">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvItmCode" runat="server" Height="16px"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gcod")) %>'
                                                Width="49px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Description">
                                        <ItemTemplate>
                                          <asp:Label ID="lgcResDesc1" runat="server" 
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gdesc")) %>' 
                                                        Width="220px"></asp:Label>
                                          
                                        </ItemTemplate>
                                        <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                      <asp:TemplateField HeaderText="Type">
                                        <ItemTemplate>
                                          <asp:Label ID="lgcType" runat="server" CssClass="text-blue font-italic text-center"
                                                        Text='<%# System.IO.Path.GetExtension(Convert.ToString(DataBinder.Eval(Container.DataItem, "fileurl"))) %>' 
                                                        Width="40px"></asp:Label>
                                          
                                        </ItemTemplate>
                                        <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Upload Date">
                                        
                                        <ItemTemplate>
                                            <asp:Label ID="txtgvVal" runat="server" CssClass="text-danger font-italic"
                                                Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "rowdat")).ToString("dd-MMM-yyyy hh:mm tt") %>'
                                                Width="120px" ></asp:Label>

                                            
                                        </ItemTemplate>


                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        <ItemStyle HorizontalAlign="left" />
                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderText="User">
                                        <ItemTemplate>
                                          <asp:Label ID="lgcResDesc1" runat="server" CssClass="text-center" 
                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "usrname")) %>' 
                                                        Width="60px"></asp:Label>
                                          
                                        </ItemTemplate>
                                        <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Action">
                                        <ItemTemplate>
                                      <asp:LinkButton ID="LbtnDownload" runat="server" CssClass="btn btn-xs btn-info"><span class="fa fa-download "></span></asp:LinkButton>
                                      <asp:LinkButton ID="LbtnDelete" runat="server" OnClientClick="return confirm('Do you want to remove?')" CssClass="btn btn-xs btn-danger"><span class="fa fa-trash"></span></asp:LinkButton>
                                          
                                        </ItemTemplate>
                                      
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                </Columns>
                             
                                <EditRowStyle />
                                <AlternatingRowStyle />
                                <PagerStyle CssClass="gvPagination" />
                                <HeaderStyle CssClass="grvHeader" />
                            </asp:GridView>
    <br />
</div>
                    </div>
     </div>
               </div>
                <div class="col-md-6">
                    <section class="card card-fluid" style="min-height: 500px;">
                    <!-- .card-header -->
                    <header class="card-header">
                      <!-- .nav-tabs -->
                      <ul class="nav nav-tabs card-header-tabs" id="FileTabs" runat="server">
                       <%-- <li class="nav-item">
                          <a class="nav-link active show" data-toggle="tab" href="#T0101001">1</a>
                        </li>
                        <li class="nav-item">
                          <a class="nav-link" data-toggle="tab" href="#T0101002">2</a>
                        </li>--%>
                       <%-- <li class="nav-item dropdown">
                          <a class="nav-link" data-toggle="dropdown" href="#" role="button">Dropdown
                            <span class="caret"></span>
                          </a>
                          <div class="dropdown-arrow"></div>
                          <div class="dropdown-menu">
                            <a class="dropdown-item" href="#">Action</a>
                            <a class="dropdown-item" href="#">Another action</a>
                            <a class="dropdown-item" href="#">Something else here</a>
                            <div class="dropdown-divider"></div>
                            <a class="dropdown-item" href="#">Separated link</a>
                          </div>
                        </li>--%>
                      </ul>
                      <!-- /.nav-tabs -->
                    </header>
                    <!-- /.card-header -->
                    <!-- .card-body -->
                    <div class="card-body" runat="server">
                      <!-- .tab-content -->
                     <div id="myTabContent" runat="server" class="tab-content">
                       <%-- <div class="tab-pane fade active show" id="T0101001">
                          <p> Raw denim you probably haven't heard of them jean shorts Austin. Nesciunt tofu stumptown aliqua, retro synth master cleanse. Mustache cliche tempor, williamsburg carles vegan helvetica. Reprehenderit butcher retro keffiyeh dreamcatcher
                            synth. Cosby sweater eu banh mi, qui irure terry richardson ex squid. Aliquip placeat salvia cillum iphone. Seitan aliquip quis cardigan american apparel, butcher voluptate nisi qui. </p>
                        </div>
                        <div class="tab-pane fade " id="T0101002">
                          <p> Food truck fixie locavore, accusamus mcsweeney's marfa nulla single-origin coffee squid. Exercitation +1 labore velit, blog sartorial PBR leggings next level wes anderson artisan four loko farm-to-table craft beer twee. Qui photo
                            booth letterpress, commodo enim craft beer mlkshk aliquip jean shorts ullamco ad vinyl cillum PBR. Homo nostrud organic, assumenda labore aesthetic magna delectus mollit. </p>
                        </div>--%>
                      </div>
                      <!-- /.tab-content -->
                    </div>
                    <!-- /.card-body -->
                  </section>

               </div>
                </div>

   
     </ContentTemplate>
     </asp:UpdatePanel>
</asp:Content>



