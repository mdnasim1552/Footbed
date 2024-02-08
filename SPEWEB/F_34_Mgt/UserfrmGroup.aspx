<%@ Page Title="" Language="C#" MasterPageFile="~/SPE.Master" AutoEventWireup="true" CodeBehind="UserfrmGroup.aspx.cs" Inherits="SPEWEB.F_34_Mgt.UserfrmGroup" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript">

        $(document).ready(function () {
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);

        });
        function pageLoaded() {


            $("input, select").bind("keydown", function (event) {
                var k1 = new KeyPress();
                k1.textBoxHandler(event);

            });


            

            var gv = $('#<%=this.gvPermission.ClientID %>');
            gv.Scrollable();

           
            var gvUseForm = $('#<%=this.gvUseForm.ClientID %>');
            gvUseForm.Scrollable({
                ScrollHeight:450,});
            //gvUseForm.
            $('.chzn-select').chosen({ search_contains: true });
        }
    </script>


    
    

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>

         <div class="card card-fluid">
                <div class="card-body">
                    <div class="row">                     
                            
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <asp:Label CssClass="label" ID="LblUer" runat="server">Search User</asp:Label>
                                             <div class="input-group input-group-sm input-group-alt">
                            <asp:TextBox ID="txtSrcName" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
                               <div class="input-group-append">    
                                     <asp:LinkButton ID="ibtnFindName" CssClass="input-group-text" runat="server" OnClick="ibtnFindName_Click" TabIndex="9"><span class="fa fa-search"> </span></asp:LinkButton>
                                

                              
                            </div>
                             </div>
                                      </div>
                                        </div>

                                    <div class="col-md-1">
                                            <div class="form-group" style="margin-top:20px;">
                                         <asp:LinkButton ID="lbtnNewUser" OnClick="lbtnNewUser_Click" CssClass="btn btn-xs btn-danger" runat="server"><span class="glyphicon glyphicon-user "> </span> New User</asp:LinkButton>

                                          
                                        </div>
                                             </div>
                                        

                                   
                                    <div class="col-md-3 ">
                                        <div class="form-group">
                                        <asp:Label ID="lblId" CssClass="label" runat="server" Visible="False" Text="User Name"></asp:Label>
                                        <asp:Label ID="txtuserid" CssClass="form-control form-control-sm" runat="server" Visible="False" Text="User Name"></asp:Label>
                                        </div>
                                    </div>

                                    <div class="col-md-3 pading5px asitCol3 pull-right">
                                        <div class="msgHandSt">
                                            <asp:Label ID="lblMsg" CssClass="btn-danger primaryBtn btn disabled" runat="server" Visible="false"></asp:Label>
                                        </div>


                                    </div>
                                </div>
                            </div>
                      
                    </div>
             <div class="card card-fluid">
                <div class="card-body" style="min-height:500px;">
                    <div class="row">
                        <div class="col-md-9">                
                              <asp:GridView ID="gvUseForm" runat="server" AutoGenerateColumns="False"
                            ShowFooter="True" Width="918px"  AllowPaging="True" CssClass="table-striped table-hover table-bordered grvContentarea"
                            OnPageIndexChanging="gvUseForm_PageIndexChanging"               
                            PageSize="100">
                            <RowStyle />
                            <Columns>
                                <asp:TemplateField HeaderText="Sl">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvSlNo" runat="server" Font-Bold="True"
                                            Style="text-align: right"
                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                  <asp:TemplateField HeaderText="">
                                      <ItemTemplate>
                                          <asp:LinkButton ID="EditBtn" OnClick="EditBtn_Click" runat="server" Text="<span class='fa fa-edit'></span>"></asp:LinkButton>
                                      </ItemTemplate>
                                      </asp:TemplateField>
                              
                                <asp:TemplateField HeaderText="User Id">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lbtnUserId" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "grpusrid")) %>'
                                            Width="50px" OnClick="lbtnUserId_Click"></asp:LinkButton>
                                    </ItemTemplate>

                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtgvuserid" runat="server" BackColor="Transparent"
                                            BorderStyle="None" MaxLength="7" Width="50px"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "grpusrid")) %>'></asp:TextBox>
                                    </EditItemTemplate>

                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="User Name">
                                    <ItemTemplate>
                                        <asp:LinkButton OnClick="lgvusrShorName_Click" ID="lgvusrShorName" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "usrsname")) %>'
                                            Width="120px"></asp:LinkButton>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtusrShorName" runat="server" BackColor="Transparent"
                                            BorderStyle="None" Width="120px"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "usrsname")) %>'></asp:TextBox>
                                    </EditItemTemplate>

                                    <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Full Name">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvusrFullName" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "usrname")) %>'
                                            Width="120px"></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtusrFullName" runat="server" BackColor="Transparent"
                                            BorderStyle="None" Width="120px"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "usrname")) %>'></asp:TextBox>
                                    </EditItemTemplate>
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Designation">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvDesig" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "usrdesig")) %>'
                                            Width="150px"></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtgvDesig" runat="server" BackColor="Transparent"
                                            BorderStyle="None" Width="120px"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "usrdesig")) %>'></asp:TextBox>
                                    </EditItemTemplate>


                                    <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Pass Word" Visible="false">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtgvpass" runat="server" BackColor="Transparent"
                                            BorderStyle="None" Width="140px"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "usrpass")) %>' TextMode="Password"></asp:TextBox>
                                    </EditItemTemplate>
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                          <%--    <asp:TemplateField HeaderText="Active">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkActive" runat="server"
                                            Checked='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "usractive"))=="True" %>' />
                                    </ItemTemplate>
                                   <EditItemTemplate>
                                                    
                                                </EditItemTemplate>
                               <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>--%>

                                <asp:TemplateField HeaderText="Remarks" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvrmrk" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "usrrmrk")) %>'
                                            Width="120px"></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtgvgvrmrk" runat="server" BackColor="Transparent"
                                            BorderStyle="None" Width="120px"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "usrrmrk")) %>'></asp:TextBox>
                                    </EditItemTemplate>
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Emp ID" >
                                    <EditItemTemplate>
                                        <asp:Panel ID="Panel21" runat="server">
                                            <table style="width: 100%;">
                                                <tr>
                                                    <td>
                                                        <asp:TextBox ID="txtSrCentrid" runat="server" CssClass="  inputtextbox" Width="30px"></asp:TextBox>
                                                    </td>
                                                    <td>

                                                        <div class="colMdbtn">
                                                            <asp:LinkButton ID="ibtnSrchCentr" runat="server" CssClass="btn btn-primary srearchBtn" OnClick="ibtnSrchCentr_Click" TabIndex="14"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>

                                                        </div>

                                                    </td>
                                                    <td>
                                                        <div class="col-md-4 pading5px">
                                                            <asp:DropDownList ID="ddlempid" runat="server" CssClass="form-control inputTxt chzn-select" Width="150px">
                                                            </asp:DropDownList>
                                                        </div>

                                                    </td>
                                                </tr>
                                            </table>
                                        </asp:Panel>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvCentrName" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "empname")) %>'
                                            Width="150px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                
                                 <asp:TemplateField HeaderText="Emp" Visible="false">
                                    

                                    <ItemTemplate>
                                        <asp:Label ID="lblgvempid" runat="server" Font-Size="12px" Style="font-size: 12px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "empid")) %>'
                                            Width="120px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Font-Bold="True"  HorizontalAlign="Left" />
                                    <ItemStyle Font-Size="12px" HorizontalAlign="Left" />
                                </asp:TemplateField>
                            </Columns>
                            <FooterStyle CssClass="grvFooter" />
                            <PagerSettings Position="Top" />
                            <PagerStyle CssClass="gvPagination" />
                            <HeaderStyle CssClass="grvHeader" />
                            <EditRowStyle />
                            <AlternatingRowStyle />
                        </asp:GridView></div>
                        <div class="col-md-3" id="userDetPan" runat="server" visible="false">
                           
                                      <section class="card" >
                                            <header class="card-header">User Information</header>
                                            <!-- .card-body -->
                                            <div class="card-body">
                                                <div class="row">
                                                   <div class="col-md-12 text-center" style="padding-top:0px !important" >
                                                       <a href="#" class="user-avatar user-avatar-lg ">                          
                                                            <asp:Image ID="UsrImg" runat="server"  />
                        </a>
                                                       <h3 class="card-title text-truncate">
                          <a href="#"> <asp:Label ID="lbluserheading" runat="server"></asp:Label></a>
                        </h3>
                         <h6 class="card-subtitle text-muted mb-3">  @   <asp:Label ID="lblGrpUsrId" runat="server"></asp:Label></h6>
                                                     
                                   <asp:GridView ID="indUsrinf" runat="server" AutoGenerateColumns="False" Style="margin-top:0px;"
                           CssClass=" table-striped table-hover table-bordered grvContentarea">
                                   <Columns>
                                <asp:TemplateField HeaderText="SL">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvSlNo" runat="server" Font-Bold="True" Height="16px"
                                            Style="text-align: right"
                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>                               
                                <asp:TemplateField HeaderText="Company">
                                    <ItemTemplate>
                                        <asp:Label ID="lbtnComname" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "comname")) %>'
                                            Width="180px"></asp:Label>
                                    </ItemTemplate>

                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                           <asp:TemplateField HeaderText="User Id">
                                    <ItemTemplate>
                                        <asp:Label ID="lbtncomusrid" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "usrid")) %>'
                                            Width="50px"></asp:Label>
                                    </ItemTemplate>

                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                       </Columns>

                            </asp:GridView>

                                                       <br />
                                    </div>
                                                </div>

                                            </div>
                                            <!-- /.card-body -->
                                        </section>
                            
                            
                        </div>
      
                    </div>

                   
                    <asp:MultiView ID="MultiView1" runat="server">
                        <asp:View ID="View1" runat="server">
                            <div class="row">                        
                                       
                                            <div class="col-md-1 ">
                                                 <div class="form-group">
                                                <asp:Label ID="lblPage" runat="server" CssClass="label" Text="Page Size" Visible="false"></asp:Label>

                                                <asp:DropDownList ID="ddlpagesize" runat="server" AutoPostBack="True" CssClass=" form-control form-control-sm"
                                                     OnSelectedIndexChanged="ddlpagesize_SelectedIndexChanged" Visible="False"
                                                  >
                                                    <asp:ListItem Value="10">10</asp:ListItem>
                                                    <asp:ListItem Value="15">15</asp:ListItem>
                                                    <asp:ListItem Value="20">20</asp:ListItem>
                                                    <asp:ListItem Value="30">30</asp:ListItem>
                                                    <asp:ListItem Value="50">50</asp:ListItem>
                                                    <asp:ListItem Value="100">100</asp:ListItem>
                                                    <asp:ListItem Value="150">150</asp:ListItem>
                                                    <asp:ListItem Value="200">200</asp:ListItem>
                                                    <asp:ListItem Value="300">300</asp:ListItem>
                                                    <asp:ListItem Value="400">400</asp:ListItem>
                                                    <asp:ListItem Value="600">600</asp:ListItem>
                                                    <asp:ListItem Value="900">900</asp:ListItem>
                                                </asp:DropDownList>

                                            </div>
                                                </div>
                                            <div class="col-md-1">
                                                 <div class="form-group" style="margin-top:20px">
                                                <asp:CheckBox ID="chkShowall" runat="server" AutoPostBack="True"                                                    
                                                    OnCheckedChanged="chkShowall_CheckedChanged" Text="Show All" CssClass="checkbox" />
                                            </div>
                                                 </div>
                                            <div class="col-md-3">
                                                 <div class="form-group">
                                                     <asp:Label ID="LblModule" runat="server">Select Module</asp:Label>
                                                <asp:DropDownList ID="ddlModuleName" runat="server" AutoPostBack="True"
                                                    OnSelectedIndexChanged="ddlModuleName_SelectedIndexChanged" CssClass="form-control form-control-sm">
                                                                                                  </asp:DropDownList>
                                            </div>
                                                 </div>
                                            <div class="col-md-2">
                                                 <div class="form-group" style="margin-top:20px;">
                                                <asp:LinkButton ID="lnkbtnBack" runat="server" CssClass="btn  btn-primary btn-sm"
                                                    OnClick="lnkbtnBack_Click">Back</asp:LinkButton>
                                            </div>
                                                 </div>
                                          
                                      
                                <asp:GridView ID="gvPermission" runat="server" AllowPaging="True"
                                    AutoGenerateColumns="False" CssClass="table-striped table-hover table-bordered grvContentarea"
                                    OnPageIndexChanging="gvPermission_PageIndexChanging"
                                    OnRowDeleting="gvPermission_RowDeleting" ShowFooter="True">
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
                                        <asp:TemplateField HeaderText="Form Name" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvufrmname" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "frmname")) %>'
                                                    Width="120px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Form id" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvufrmid" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "frmid")) %>'
                                                    Width="120px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Query Type" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvQrytype" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "qrytype")) %>'
                                                    Width="120px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Description">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvDescription" runat="server" Text='<%# "<B>"+ Convert.ToString(DataBinder.Eval(Container.DataItem, "modulename")) + "</B>"+
                                                                         (DataBinder.Eval(Container.DataItem, "dscrption").ToString().Trim().Length>0 ? 
                                                                         (Convert.ToString(DataBinder.Eval(Container.DataItem, "modulename")).Trim().Length>0 ?  "<br>" : "")+                                                             
                                                                         "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"+ 
                                                                         Convert.ToString(DataBinder.Eval(Container.DataItem, "dscrption")).Trim(): "") 
                                                                         
                                                                    %>'
                                                    Width="280px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:LinkButton ID="lbtnUpPer" runat="server" Font-Bold="True" CssClass="btn btn-danger btn-sm" OnClick="lbtnUpPer_Click">Update</asp:LinkButton>
                                            </FooterTemplate>
                                            <HeaderTemplate>
                                                <table style="width: 100%;">
                                                    <tr>
                                                        <td class="style22">Description</td>
                                                        <td>&nbsp;</td>
                                                        <td>&nbsp;</td>
                                                        <td>&nbsp;</td>
                                                        <td>&nbsp;</td>
                                                        <td>&nbsp;</td>
                                                        <td class="style23">&nbsp;</td>
                                                        <td>
                                                            <asp:CheckBox ID="chkAllfrm" runat="server" AutoPostBack="True"
                                                                OnCheckedChanged="chkAllfrm_CheckedChanged" Text="ALL " />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </HeaderTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Permission">
                                            <HeaderTemplate>
                                                <table style="width: 90px;">
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label3" runat="server" Text="Permission"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:CheckBox ID="chkallView" runat="server" AutoPostBack="True"
                                                                OnCheckedChanged="chkallView_CheckedChanged" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkPermit" runat="server"
                                                    Checked='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "chkper"))=="True" %>'
                                                    Width="50px" />
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="left" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Entry or Edit">
                                            <HeaderTemplate>
                                                <table style="width: 90px;">
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label4" runat="server" Text="Entry or Edit"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:CheckBox ID="chkallEntry" runat="server" AutoPostBack="True"
                                                                OnCheckedChanged="chkallEntry_CheckedChanged" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkEntry" runat="server"
                                                    Checked='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "entry"))=="True" %>'
                                                    Width="50px" />
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="left" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Delete">
                                                <HeaderTemplate>
                                                    <table style="width: 90px;">
                                                       
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="Labsel5" runat="server" Text="Delete"></asp:Label>
                                                                <asp:CheckBox ID="chkAllDel" runat="server" AutoPostBack="True" OnCheckedChanged="chkAllDel_CheckedChanged"
                                                                     />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </HeaderTemplate>
                                                
                                                <ItemTemplate>
                                                   <asp:CheckBox ID="deleteCk" runat="server" AutoPostBack="True" 
                                                                    Checked='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "deleteCk"))=="True" %>'
                                                                     />
                                                </ItemTemplate>
                                                <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                <HeaderStyle HorizontalAlign="left" VerticalAlign="Top" />
                                            </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Print">
                                            <HeaderTemplate>
                                                <table style="width: 90px;">
                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label5" runat="server" Text="Print"></asp:Label>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:CheckBox ID="chkallPrint" runat="server" AutoPostBack="True"
                                                                OnCheckedChanged="chkallPrint_CheckedChanged" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkPrint" runat="server"
                                                    Checked='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "printable"))=="True" %>'
                                                    Width="50px" />
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="left" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Row All">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkall" runat="server" AutoPostBack="True"
                                                    OnCheckedChanged="chkall_CheckedChanged" />
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                    </Columns>
                                    <FooterStyle CssClass="grvFooter" />
                                    <PagerSettings Position="Top" />
                                    <PagerStyle CssClass="gvPagination" />
                                    <HeaderStyle CssClass="grvHeader" />
                                    <EditRowStyle />
                                    <AlternatingRowStyle />
                                </asp:GridView>
                                <asp:Label ID="lblusrid" runat="server" Visible="False"></asp:Label>
                            </div>
                        </asp:View>
                        <asp:View ID="NewUser" runat="server">
                            <div class="row">
                            <div class="col-md-5"> 
                                <div class="row">
                                <div class="col-md-6">
                                     <div class="form-group">
                                    <%--  <asp:Label ID="Label1" runat="server" Text="User"></asp:Label>
                                        <asp:TextBox ID="txtSrcName" runat="server" CssClass="inputTxt lblTxt inpPixedWidth"></asp:TextBox> --%>
                                    <asp:Label ID="LblUsr" runat="server" CssClass="label" Text="User"></asp:Label>
                                    <asp:TextBox ID="txtUsr" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
                                </div>
                                     </div>
                                <div class="col-md-6">
                                     <div class="form-group">
                                    <asp:Label ID="lblfullName" runat="server" CssClass="label" Text="Full Name"></asp:Label>
                                    <asp:Label ID="LblUpFlag" runat="server" Visible="false"></asp:Label>
                                    <asp:TextBox ID="TxtFullName" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
                                </div>
                                    </div>

                                 <div class="col-md-6">
                                     <div class="form-group">
                                    <asp:Label ID="lblDesg" runat="server" CssClass="label" Text="Designation"></asp:Label>
                                    <asp:TextBox ID="TxtDesg" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
                                </div>
                                      </div>
                                  <div class="col-md-6">
                                     <div class="form-group">
                                    <asp:Label ID="lblPass" runat="server" CssClass="label" Text="Password"></asp:Label>
                                    <asp:TextBox ID="TxtPass" runat="server" TextMode="Password" CssClass="form-control form-control-sm"></asp:TextBox>
                                     <asp:Label ID="Grpusr" runat="server" Visible="false"></asp:Label>
                                </div>
                                </div>
                             <div class="col-md-6">
                                     <div class="form-group">
                                    <asp:Label ID="LblRemark" runat="server" CssClass="label" Text="Remarks"></asp:Label>
                                    <asp:TextBox ID="TxtRemark" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
                                </div>
                                  </div>
                                <div class="col-md-6">
                                     <div class="form-group">                           
                                    <asp:Label ID="Label1" runat="server" CssClass="label" Text="User Role"></asp:Label>
                                    <asp:DropDownList ID="ddlUserRole" runat="server" CssClass="chzn-select form-control form-control-sm" >                                        
                                    </asp:DropDownList>
                                </div>
                                    </div>
                                  <div class="col-md-6">
                                     <div class="form-group">    
                           
                                    <asp:Label ID="lblHrEmp" runat="server" CssClass="label" Text="HR Employee"></asp:Label>
                                    <asp:DropDownList ID="ddlHrEmp" runat="server" CssClass="chzn-select form-control form-control-sm" >                                        
                                    </asp:DropDownList>
                                </div>
                                      </div>
                                 <div class="col-md-6">
                                     <div class="form-group">
                                      <asp:Label ID="lblComp" runat="server" CssClass="label" Text="Company"></asp:Label>
                                    <asp:DropDownList ID="ddlComp" runat="server" CssClass="chzn-select  form-control form-control-sm" >
                                        </asp:DropDownList>
                                </div>
                                    
                            </div>
                                 <div class="col-md-6">
                                    <asp:CheckBox ID="actChkbox" runat="server" Checked="true" />Active?
                                </div>                            
                                  <div class="col-md-6 form-inline ">
                                      
                                    <asp:Button ID="btnClose" runat="server" OnClick="btnClose_Click" CssClass="btn btn-xs btn-warning " Text="Close" />
                                     &nbsp;
                                         <asp:Button ID="lbtnUpdate" CssClass="btn btn-xs btn-danger" OnClick="lbtnUpdate_Click" runat="server" Text="Update" />

                                    &nbsp;
                                    <asp:Button ID="btnAdd" runat="server" CssClass=" btn btn-xs btn-primary" OnClick="btnAdd_Click" Text="Add" />

                                      </div>
                                 </div>
                                <br />
                           </div>
                       
                         
                            <div class="col-md-5">
                            <asp:GridView ID="gvcompany" runat="server" AutoGenerateColumns="False"    OnRowDeleting="gvcompany_RowDeleting"
                            ShowFooter="True" CssClass="table-striped table-hover table-bordered grvContentarea">
                            <RowStyle />
                            <Columns>
                                <asp:TemplateField HeaderText="Sl">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvDSlNo" runat="server" Font-Bold="True" Style="text-align: right"
                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                  <asp:CommandField ShowDeleteButton="True" ControlStyle-CssClass="text-red" DeleteText="<span class='fa fa-trash'></span>" />
                          



                                <asp:TemplateField HeaderText="Company Name">
                                    <ItemTemplate>
                                        <asp:Label ID="lblpcomp" runat="server" Font-Bold="True" Style="text-align: left"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "comname")) %>' Width="180px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle HorizontalAlign="Right" Font-Bold="true" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>

                            <%--    <asp:TemplateField HeaderText="User Id">
                                    <ItemTemplate>
                                        <asp:Label ID="lblusr" runat="server" Font-Bold="True" Style="text-align: left"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "usrid")) %>' Width="160px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle HorizontalAlign="Right" Font-Bold="true" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>--%>

                                <asp:TemplateField HeaderText="User">
                                    <ItemTemplate>
                                        <asp:Label ID="lblusrShort" runat="server" Font-Bold="True" Style="text-align: left"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "usrsname")) %>' Width="80px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle HorizontalAlign="Right" Font-Bold="true" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Full Name">
                                    <ItemTemplate>
                                        <asp:Label ID="lblUsrFullName" runat="server" Font-Bold="True" Style="text-align: left"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "usrname")) %>' Width="150px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle HorizontalAlign="Right" Font-Bold="true" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Designation">
                                    <ItemTemplate>
                                        <asp:Label ID="lblDesg" runat="server" Font-Bold="True" Style="text-align: left"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "usrdesig")) %>' Width="100px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle HorizontalAlign="Right" Font-Bold="true" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                 <asp:TemplateField HeaderText="Active">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkActiveSt" runat="server"
                                            Checked='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "usractive"))=="True" %>' />
                                    </ItemTemplate>
                                   <EditItemTemplate>
                                                    
                                                </EditItemTemplate>
                               <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                            </Columns>
                            <EditRowStyle />
                            <AlternatingRowStyle />
                            <PagerStyle CssClass="gvPagination" />
                            <HeaderStyle CssClass="grvHeader" />
                            <FooterStyle CssClass="grvFooter" />
                        </asp:GridView>
                                </div>
                                </div>
                        </asp:View>
                    </asp:MultiView>
                   
                </div>
            </div>

                    </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>


