<%@ Page Title="" Language="C#" MasterPageFile="~/SPE.Master" AutoEventWireup="true" CodeBehind="UserLoginfrm.aspx.cs" Inherits="SPEWEB.F_34_Mgt.UserLoginfrm" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">



    <script type="text/javascript">

        $(document).ready(function () {
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);

        });
        function openModal() {

            $('#myModal').modal('toggle');

        }
        function CLoseMOdal() {
            $('#myModal').modal('hide');
        }
        function pageLoaded() {


            $("input, select").bind("keydown", function (event) {
                var k1 = new KeyPress();
                k1.textBoxHandler(event);

            });



            var gv = $('#<%=this.gvPermission.ClientID %>');
            gv.Scrollable();

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
                        <div class="col-md-2 col-sm-2 col-lg-2">

                            <div class="form-group">
                                <asp:Label ID="lblConTrolCode" runat="server" CssClass="control-label">Search:</asp:Label>
                                <div class="input-group input-group-alt">
                                    <asp:TextBox runat="server" ID="txtSrcName" CssClass="form-control form-control-sm ">
                                    </asp:TextBox>
                                    <div class="input-group-append">
                                        <asp:LinkButton ID="ibtnFindName" CssClass="input-group-text" runat="server" OnClick="ibtnFindName_Click"><span class="fa fa-search"> </span></asp:LinkButton>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="col-md-4 col-sm-4 col-lg-4">
                            <div class="form-group" style="margin-top: 20px;">
                                <asp:Label ID="lblId" CssClass=" lblName" runat="server" Visible="False" Text="User Name:"></asp:Label>
                                <asp:Label ID="txtuserid" CssClass=" lblName" runat="server" Visible="False" Text="User Name" ForeColor="#00cc66"></asp:Label>
                            </div>
                        </div>
                    </div>
                    <div class="table-responsive mb-2">
                        <asp:GridView ID="gvUseForm" runat="server" AutoGenerateColumns="False"
                            ShowFooter="True" Width="918px" AllowPaging="True" CssClass="table-striped table-hover table-bordered grvContentarea"
                            OnPageIndexChanging="gvUseForm_PageIndexChanging"
                            OnRowCancelingEdit="gvUseForm_RowCancelingEdit"
                            OnRowEditing="gvUseForm_RowEditing" OnRowUpdating="gvUseForm_RowUpdating"
                            PageSize="100">
                            <RowStyle />
                            <Columns>

                                <asp:TemplateField HeaderText="Sl">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvSlNo" runat="server" Font-Bold="True" Height="16px"
                                            Style="text-align: right"
                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                </asp:TemplateField>
                                <asp:CommandField ShowEditButton="True" CancelText="<span class='fa fa-times'></span>" EditText="<span class='fa fa-pen'></span>" UpdateText="<span class='fa fa-save'></span>" />

                                <%--<asp:CommandField CancelText="Can" ShowEditButton="True" UpdateText="Up" />--%>
                                <asp:TemplateField HeaderText="User Id">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lbtnUserId" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "usrid")) %>'
                                            Width="50px" OnClick="lbtnUserId_Click"></asp:LinkButton>
                                    </ItemTemplate>

                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtgvuserid" runat="server" BackColor="Transparent"
                                            BorderStyle="None" MaxLength="7" Width="50px"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "usrid")) %>'></asp:TextBox>
                                    </EditItemTemplate>

                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Short Name">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvusrShorName" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "usrsname")) %>'
                                            Width="80px"></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtusrShorName" runat="server" BackColor="Transparent"
                                            BorderStyle="None" Width="80px"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "usrsname")) %>'></asp:TextBox>
                                    </EditItemTemplate>

                                    <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
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
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Designation">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvDesig" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "usrdesig")) %>'
                                            Width="100px"></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtgvDesig" runat="server" BackColor="Transparent"
                                            BorderStyle="None" Width="100px"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "usrdesig")) %>'></asp:TextBox>
                                    </EditItemTemplate>


                                    <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Pass Word">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtgvpass" runat="server" BackColor="Transparent"
                                            BorderStyle="None" Width="140px"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "usrpass")) %>' TextMode="Password"></asp:TextBox>
                                    </EditItemTemplate>
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Active">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkActive" runat="server"
                                            Checked='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "usractive"))=="True" %>' />
                                    </ItemTemplate>
                                    <%--<EditItemTemplate>
                                                    
                                                </EditItemTemplate>--%>
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                </asp:TemplateField>


                                <asp:TemplateField HeaderText="Email ID">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvWebmailID" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mailid")) %>'
                                            Width="140px"></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="TxtWebmailID" runat="server" BackColor="Transparent"
                                            BorderStyle="None" Width="140px"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mailid")) %>'></asp:TextBox>
                                    </EditItemTemplate>


                                    <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Web Mail PassWord">

                                    <EditItemTemplate>
                                        <asp:TextBox ID="TxtWebmailPWD" runat="server" BackColor="Transparent"
                                            BorderStyle="None" Width="80px"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mailpass")) %>' TextMode="Password"></asp:TextBox>
                                    </EditItemTemplate>


                                    <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                </asp:TemplateField>






                                <asp:TemplateField HeaderText="Emp ID">
                                    <EditItemTemplate>
                                        <asp:Panel ID="Panel21" runat="server">

                                            <asp:TextBox ID="txtSrCentrid" runat="server" CssClass=" hidden  inputtextbox" Visible="false" Width="30px"></asp:TextBox>

                                            <asp:LinkButton ID="ibtnSrchCentr" runat="server" CssClass="btn btn-primary srearchBtn hidden" Visible="false" OnClick="ibtnSrchCentr_Click" TabIndex="14"><span class="fa fa-search"> </span></asp:LinkButton>

                                            <asp:DropDownList ID="ddlempid" runat="server" CssClass="form-control inputTxt chzn-select ">
                                            </asp:DropDownList>

                                        </asp:Panel>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvCentrName" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "empname")) %>'
                                            Width="200px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Width="200" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Emp" Visible="false">


                                    <ItemTemplate>
                                        <asp:Label ID="lblgvempid" runat="server" Font-Size="12px" Style="font-size: 12px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "empid")) %>'
                                            Width="120px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Font-Bold="True" HorizontalAlign="Left" />
                                    <ItemStyle Font-Size="12px" HorizontalAlign="Left" />
                                </asp:TemplateField>


                                <asp:TemplateField HeaderText="User Role">
                                    <ItemTemplate>
                                        <asp:Label ID="lbgUserlId" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "roledesc")) %>'
                                            Width="100px"></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>

                                        <asp:DropDownList ID="ddlUserRole" runat="server" CssClass="form-control inputTxt chzn-select" Width="100px">
                                        </asp:DropDownList>

                                    </EditItemTemplate>


                                    <HeaderStyle Font-Bold="True" HorizontalAlign="Left" />
                                    <ItemStyle Font-Size="12px" HorizontalAlign="Left" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Copy">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lbtnLink" OnClick="lbtnLink_Click" ToolTip="Copy Privilege" runat="server"><span class="fa fa-copy"></span></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Remarks">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvrmrk" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "usrrmrk")) %>'
                                            Width="80px"></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtgvgvrmrk" runat="server" BackColor="Transparent"
                                            BorderStyle="None" Width="80px"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "usrrmrk")) %>'></asp:TextBox>
                                    </EditItemTemplate>
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                </asp:TemplateField>

                            </Columns>
                            <FooterStyle CssClass="" />
                            <PagerStyle CssClass="gvPagination" />
                            <HeaderStyle CssClass="gvHeader" />
                            <EditRowStyle />
                            <AlternatingRowStyle />
                        </asp:GridView>
                    </div>
                </div>
            </div>

            <div class="card card-fluid">
                <div class="card-body" style="min-height: 200px;">
                    <asp:MultiView ID="MultiView1" runat="server">
                        <asp:View ID="View1" runat="server">
                            <div class="row">
                                <div class="col-md-1 col-sm-1 col-lg-1 ">
                                    <div class="form-group">
                                        <asp:Label ID="lblPage" runat="server" CssClass="label" Visible="false">Page size</asp:Label>
                                        <asp:DropDownList ID="ddlpagesize" runat="server" AutoPostBack="True"
                                            CssClass="chzn-select form-control form-control-sm" OnSelectedIndexChanged="ddlpagesize_SelectedIndexChanged" Visible="False">
                                            <asp:ListItem Value="10">10</asp:ListItem>
                                            <asp:ListItem Value="15">15</asp:ListItem>
                                            <asp:ListItem Value="20">20</asp:ListItem>
                                            <asp:ListItem Value="30">30</asp:ListItem>
                                            <asp:ListItem Value="50">50</asp:ListItem>
                                            <asp:ListItem Value="100">100</asp:ListItem>
                                            <asp:ListItem Value="150">150</asp:ListItem>
                                            <asp:ListItem Value="200">200</asp:ListItem>
                                            <asp:ListItem Value="300">300</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-1 col-sm-1 col-lg-1 ">

                                    <div class="form-group" style="margin-top: 20px;">
                                        <asp:CheckBox ID="chkShowall" runat="server" AutoPostBack="True"
                                            OnCheckedChanged="chkShowall_CheckedChanged" Text="Show All" CssClass="checkBox" />
                                    </div>
                                </div>
                                <div class="col-md-2 col-sm-2 col-lg-2 ">
                                    <div class="form-group">
                                        <asp:Label ID="Label1" runat="server" CssClass="label">Module</asp:Label>
                                        <asp:DropDownList ID="ddlModuleName" runat="server" AutoPostBack="True"
                                            OnSelectedIndexChanged="ddlModuleName_SelectedIndexChanged" CssClass="chzn-select form-control form-control-sm">
                                        </asp:DropDownList>

                                    </div>
                                </div>
                                <div class="col-md-1 col-sm-1 col-lg-1 ">

                                    <div class="form-group" style="margin-top: 20px;">
                                        <asp:LinkButton ID="lnkbtnBack" runat="server" CssClass="btn btn-primary btn-sm small"
                                            OnClick="lnkbtnBack_Click">Back</asp:LinkButton>
                                    </div>
                                </div>
                            </div>
                            
                            <div class="row">
                                <asp:GridView ID="gvPermission" runat="server" AllowPaging="True"
                                    AutoGenerateColumns="False" CssClass="table-striped table-hover table-bordered grvContentarea"
                                    OnPageIndexChanging="gvPermission_PageIndexChanging"
                                    OnRowDeleting="gvPermission_RowDeleting" ShowFooter="True">
                                    <RowStyle />
                                    <Columns>
                                        <asp:TemplateField>
                                            <HeaderTemplate>      
                                                <asp:HyperLink ID="hlbtndataExel" runat="server" ToolTip="Export To Excel" Width="40px" ForeColor="White"
                                                        CssClass="btn btn-sm btn-info"><i class="fa fa-file-excel" aria-hidden="true"></i></asp:HyperLink>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvSlNo0" runat="server" Font-Bold="True" Height="16px"
                                                    Style="text-align: right"
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="40px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Form Name" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvufrmname" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "frmname")) %>'
                                                    Width="120px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Form id" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvufrmid" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "frmid")) %>'
                                                    Width="120px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Query Type" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lgvQrytype" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "qrytype")) %>'
                                                    Width="120px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
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
                                                <asp:LinkButton ID="lbtnUpPer" runat="server" Font-Bold="True" CssClass="btn btn-success btn-sm" OnClick="lbtnUpPer_Click" ToolTip="Final Update">Update</asp:LinkButton>
                                            </FooterTemplate>
                                            <HeaderTemplate>
                                                <div class="row">
                                                    <div class="col-md-6 ">
                                                        <div class="form-group">
                                                            <asp:Label ID="lbldesc" runat="server" CssClass="label">Description</asp:Label>
                                                        </div>
                                                    </div>
                                                    <div class="col-md-6 " >
                                                        <div class="form-group">
                                                            <asp:CheckBox ID="chkAllfrm" runat="server" AutoPostBack="True" CssClass="checkbox"
                                                                OnCheckedChanged="chkAllfrm_CheckedChanged" Text="ALL " />
                                                        </div>
                                                    </div>
                                                </div>
                                                
                                            </HeaderTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Permission">
                                            <HeaderTemplate>
                                                 <div class="col-md-12 ">
                                                    <div class="form-group">
                                                        <asp:Label ID="lblper" runat="server" CssClass="label">Permission</asp:Label>
                                                        <asp:CheckBox ID="chkallView" runat="server" AutoPostBack="True" CssClass="checkbox"
                                                            OnCheckedChanged="chkallView_CheckedChanged" />
                                                    </div>
                                                </div>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkPermit" runat="server"
                                                    Checked='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "chkper"))=="True" %>'
                                                    Width="50px" />
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="left" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Entry or Edit </Br> or Cancel">
                                            <HeaderTemplate>
                                                <div class="col-md-12 ">
                                                    <div class="form-group">
                                                        <asp:Label ID="lblperEEC" runat="server" CssClass="label">Entry or Edit </Br> or Cancel</asp:Label>
                                                        <asp:CheckBox ID="chkallEntry" runat="server" AutoPostBack="True" CssClass="checkbox"
                                                            OnCheckedChanged="chkallEntry_CheckedChanged" />
                                                    </div>
                                                </div>


                                                
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
                                                <div class="col-md-12 ">
                                                    <div class="form-group">
                                                        <asp:Label ID="lblperDel" runat="server" CssClass="label">Delete</asp:Label>
                                                        <asp:CheckBox ID="chkAllDel" runat="server" AutoPostBack="True" CssClass="checkbox"
                                                            OnCheckedChanged="chkAllDel_CheckedChanged" />
                                                    </div>
                                                </div>
                                                
                                            </HeaderTemplate>

                                            <ItemTemplate>
                                                <asp:CheckBox ID="deleteCk" runat="server" AutoPostBack="True"
                                                    Checked='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "deleteCk"))=="True" %>' />
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="left" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="View & Print">
                                            <HeaderTemplate>
                                                  <div class="col-md-12 ">
                                                    <div class="form-group">
                                                        <asp:Label ID="lblperVP" runat="server" CssClass="label">View & Print</asp:Label>
                                                        <asp:CheckBox ID="chkallPrint" runat="server" AutoPostBack="True" CssClass="checkbox"
                                                            OnCheckedChanged="chkallPrint_CheckedChanged" />
                                                    </div>
                                                </div>

                                               
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkPrint" runat="server"
                                                    Checked='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "printable"))=="True" %>'
                                                    Width="50px" />
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="left" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Check All">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkall" runat="server" AutoPostBack="True"
                                                    OnCheckedChanged="chkall_CheckedChanged" />
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:TemplateField>
                                    </Columns>
                                    <FooterStyle CssClass="" />
                                    <PagerStyle CssClass="gvPagination" />
                                    <HeaderStyle CssClass="gvHeader" />
                                    <EditRowStyle />
                                    <AlternatingRowStyle />
                                </asp:GridView>
                            </div>
                            <asp:Label ID="lblusrid" runat="server" Visible="False"></asp:Label>

                        </asp:View>
                    </asp:MultiView>

                </div>
            </div>

            </br>   </br>

            <div id="myModal" class="modal animated slideInLeft" role="dialog">
                <div class="modal-dialog">
                    <div class="modal-content  ">
                        <div class="modal-header">
                            <button type="button"  data-dismiss="modal"><span class="fa fa-close"></span></button>
                            <h4 class="modal-title">
                                <span class="fa fa-copy"></span>Copy User Privilege</h4>
                        </div>

                        <div class="modal-body">
                            <div class="row-fluid">
                                <div class="form-group">
                                    <asp:Label ID="Label3" runat="server" CssClass="col-md-4">Select To User: </asp:Label>
                                    <div class="col-md-6">
                                        <asp:DropDownList ID="ddlUser" CssClass="form-control" runat="server"></asp:DropDownList>
                                        <asp:Label ID="fromUserid" runat="server" Visible="false"></asp:Label>
                                    </div>
                                </div>
                                <br />

                            </div>
                        </div>
                        <div class="modal-footer ">

                            <asp:LinkButton ID="lblbtnSave" runat="server" CssClass="btn btn-sm btn-success" OnClientClick="CLoseMOdal();" OnClick="lblbtnSave_Click"><span class="fa fa-save"></span> Update </asp:LinkButton>

                            <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>


                        </div>



                    </div>
                </div>

            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

