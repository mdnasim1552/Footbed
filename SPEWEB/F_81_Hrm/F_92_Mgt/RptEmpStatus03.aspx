<%@ Page Title="" Language="C#" MasterPageFile="~/SPEMaster.Master" AutoEventWireup="true" CodeBehind="RptEmpStatus03.aspx.cs" Inherits="SPEWEB.F_81_Hrm.F_92_Mgt.RptEmpStatus03" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="DropCheck" Namespace="xMilk" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">



    <script language="javascript" type="text/javascript">
        $(document).ready(function () {
            //For navigating using left and right arrow of the keyboard
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);
         <%--  $('#<%=this.txtSrcCompany.ClientID %>').focus();--%>

        });
        function Search_Gridview(strKey, cellNr, gvName) {
            //alert(cellNr);
            var tblData;


            var strData = strKey.value.toLowerCase().split(" ");
            switch (gvName) {
                case "gvEmpList":
                    tblData = document.getElementById("<%=gvEmpList.ClientID %>");
                    break;
              <%--  case "gvConSheet":
                    tblData = document.getElementById("<%=gvConSheet.ClientID %>");
                    break;
                case "gvPreCost":
                    tblData = document.getElementById("<%=gvPreCost.ClientID %>");
                    break;
                case "gvOrdAcRej":
                    tblData = document.getElementById("<%=gvOrdAcRej.ClientID %>");
                    break;
                case "gvOrdDetails":
                    tblData = document.getElementById("<%=gvOrdDetails.ClientID %>");
                    break;
                case "gvOrdDetailsApp":
                    tblData = document.getElementById("<%=gvOrdDetailsApp.ClientID %>");
                    break;
                case "gvBOMGen":
                    tblData = document.getElementById("<%=gvBOMGen.ClientID %>");
                    break;
                case "gvBOMApp":
                    tblData = document.getElementById("<%=gvBOMApp.ClientID %>");
                    break;
                case "gvProCom":
                    tblData = document.getElementById("<%=gvProCom.ClientID %>");
                    break;--%>
            }


            var rowData;
            for (var i = 1; i < tblData.rows.length; i++) {
                rowData = tblData.rows[i].cells[cellNr].innerHTML;
                var styleDisplay = 'none';
                for (var j = 0; j < strData.length; j++) {
                    if (rowData.toLowerCase().indexOf(strData[j]) >= 0)
                        styleDisplay = '';
                    else {
                        styleDisplay = 'none';
                        break;
                    }
                }
                tblData.rows[i].style.display = styleDisplay;
            }
        }


        function pageLoaded() {

            $("input, select").bind("keydown", function (event) {
                var k1 = new KeyPress();
                k1.textBoxHandler(event);
            });

            $('.chzn-select').chosen({ search_contains: true });

        };
    </script>





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
            <div class="container moduleItemWrpper">
                <div class="contentPart">
                    <div class="row">
                        <fieldset class="scheduler-border fieldset_A">
                            <div class="form-horizontal">
                                <asp:Panel ID="Panel4" runat="server">
                                    <div class="form-group">
                                        <div class="col-md-3 col-sm-3 col-lg-3 pading5px">
                                            <asp:Label ID="Label2" runat="server" CssClass="lblTxt lblName">Employee Type</asp:Label>
                                            <asp:DropDownList ID="ddlWstation" runat="server" Width="200" OnSelectedIndexChanged="ddlWstation_SelectedIndexChanged" CssClass="form-control chzn-select pull-left" AutoPostBack="true" TabIndex="2"></asp:DropDownList>
                                        </div>
                                        <div class="col-md-3 col-sm-3 col-lg-3 pading5px">
                                            <asp:Label ID="Label3" runat="server" CssClass="smLbl_to">Division</asp:Label>
                                            <asp:DropDownList ID="ddlDivision" runat="server" Width="225" OnSelectedIndexChanged="ddlDivision_SelectedIndexChanged" CssClass="form-control chzn-select pull-left" AutoPostBack="true" TabIndex="2"></asp:DropDownList>
                                        </div>
                                        <div class="col-md-3 col-sm-3 col-lg-3 pading5px">
                                            <asp:Label ID="Label7" runat="server" CssClass="smLbl_to">Department</asp:Label>
                                            <asp:DropDownList ID="ddlDept" runat="server" Width="200" OnSelectedIndexChanged="ddlDept_SelectedIndexChanged" CssClass="form-control chzn-select pull-left" AutoPostBack="true" TabIndex="2"></asp:DropDownList>
                                        </div>
                                        <div class="col-md-3 col-sm-3 col-lg-3 pading5px">
                                            <asp:Label ID="Label8" runat="server" CssClass="smLbl_to">Section</asp:Label>

                                            <asp:DropDownList ID="ddlSection" runat="server" Width="200" CssClass="form-control chzn-select pull-left" OnSelectedIndexChanged="ddlSection_SelectedIndexChanged" AutoPostBack="true" TabIndex="2"></asp:DropDownList>
                                        </div>
                                    </div>

                                    <div class="form-group">
                                       <div class="col-md-3 col-sm-3 col-lg-3 pading5px">
                                            <asp:Label ID="Label15" runat="server" CssClass="lblTxt lblName">Grade:</asp:Label>
                                            <asp:TextBox ID="txtSrcgrade" runat="server" CssClass="inputTxt inputName inpPixedWidth" Visible="false"></asp:TextBox>
                                          <asp:DropDownList ID="ddlgrade" OnSelectedIndexChanged="ddlgrade_SelectedIndexChanged" AutoPostBack="True" runat="server" Width="200" CssClass="chzn-select form-control  inputTxt" TabIndex="6">
                                            </asp:DropDownList>
                                        </div>
                                  
                                        <div class="col-md-1 pading5px ">
                                            <asp:LinkButton ID="lnkbtnShow" runat="server" CssClass="btn btn-primary okBtn pull-left" OnClick="lnkbtnShow_Click">Ok</asp:LinkButton>
                                        </div>
                                        <div class="col-md-3 pading5px">
                                            <asp:Label ID="lblPage" runat="server" CssClass="lblTxt lblName" Text="Page Size:"></asp:Label>

                                            <asp:DropDownList ID="ddlpagesize" runat="server" AutoPostBack="True" CssClass="ddlPage" OnSelectedIndexChanged="ddlpagesize_SelectedIndexChanged" TabIndex="18">
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
                                        <div class="col-md-2">
                                            <asp:Label ID="lblmsg" runat="server" CssClass="btn btn-danger primaryBtn"></asp:Label>
                                        </div>

                                    </div>

                                     
                                </asp:Panel>
                            </div>
                        </fieldset>
                        <div class="table table-responsive">
                            <asp:GridView ID="gvEmpList" runat="server" AllowPaging="True" CssClass=" table-striped table-hover table-bordered grvContentarea"
                                AutoGenerateColumns="False" OnPageIndexChanging="gvEmpList_PageIndexChanging"
                                ShowFooter="True" Width="420px">
                                <Columns>
                                    <asp:TemplateField HeaderText="Sl.No.">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvSlNo1" runat="server" Font-Bold="True" Height="16px"
                                                Style="text-align: right"
                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Card #">
                                        <HeaderTemplate>
                                                                            <asp:TextBox ID="txtSearchArt"   BorderStyle="None"   runat="server" Width="60px" placeholder="Card #" onkeyup="Search_Gridview(this,2, 'gvEmpList')"></asp:TextBox><br />
                                                                        </HeaderTemplate>




                                        <ItemTemplate>
                                            <asp:Label ID="lblgvcardnoemp" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "idcardno")) %>'
                                                Width="80px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                                                           <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" BackColor="White"  />

                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Company &amp; Employee Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvcompanyandemp" runat="server" Text='<%# "<B>"+ Convert.ToString(DataBinder.Eval(Container.DataItem, "companyname")) + "</B>"+
                                                                         (DataBinder.Eval(Container.DataItem, "gradedesc").ToString().Trim().Length>0 ? 
                                                                         (Convert.ToString(DataBinder.Eval(Container.DataItem, "companyname")).Trim().Length>0 ?  "<br>" : "")+                                                             
                                                                         "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"+ 
                                                                         "<B>"+Convert.ToString(DataBinder.Eval(Container.DataItem, "gradedesc")).Trim()+"</B>": "")  + 
                                                                         (DataBinder.Eval(Container.DataItem, "empname").ToString().Trim().Length>0 ? 
                                                                          (Convert.ToString(DataBinder.Eval(Container.DataItem, "gradedesc")).Trim().Length>0 ?   "<br>" :"") + 
                                                                         "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"+ 
                                                                         Convert.ToString(DataBinder.Eval(Container.DataItem, "empname")).Trim(): "")
                                                                    %>'
                                                Width="300px">
                                                    
                                                    
                                                    
                                            </asp:Label>
                                        </ItemTemplate>
                                        <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                        <HeaderStyle HorizontalAlign="left" VerticalAlign="Top" />
                                    </asp:TemplateField>

                                    
                                    <asp:TemplateField HeaderText="Department">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvDeptname" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "section")) %>'
                                                Width="150px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Grade">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvGrdDesc" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gradedesc")) %>'
                                                Width="80px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Designation">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvdesignationemp" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "desig")) %>'
                                                Width="120px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>


                                    
                                    <asp:TemplateField HeaderText="Joining Date">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvjoindateemp" runat="server" BackColor="Transparent"
                                                BorderStyle="None" Font-Size="11px"
                                                Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "joindate")).ToString("dd-MMM-yyyy") %>'
                                                Width="80px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        <ItemStyle HorizontalAlign="left" VerticalAlign="Top" />
                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderText="Service Period">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvserperiod" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "serperiod")) %>'
                                                Width="150px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lblgvtotal" runat="server" Font-Bold="true">Total</asp:Label>
                                        </FooterTemplate>
                                        <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>



                                    <asp:TemplateField HeaderText="Gross. Salary">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvsalary" runat="server" Style="text-align: right"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "gssal")).ToString("#,##0;(#,##0); ") %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lgvFoallows" runat="server" Style="text-align: right" Font-Bold="true"></asp:Label>
                                        </FooterTemplate>
                                        <FooterStyle HorizontalAlign="Right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        <ItemStyle HorizontalAlign="right" />
                                    </asp:TemplateField>

                                </Columns>
                                <FooterStyle CssClass="grvFooter" />
                                <EditRowStyle />
                                <AlternatingRowStyle />
                                <PagerStyle CssClass="gvPagination" />
                                <HeaderStyle CssClass="grvHeader" />
                                 <EmptyDataTemplate>
                        <div style="color:red; text-align:center !important; font-style:italic; font-size:15px; ">No records to display.</div>
                    </EmptyDataTemplate>


                            </asp:GridView>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

