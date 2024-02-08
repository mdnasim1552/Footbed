<%@ Page Title="" Language="C#" MasterPageFile="~/SPE.Master" AutoEventWireup="true" CodeBehind="EmpBankSalaryEOT.aspx.cs" Inherits="SPEWEB.F_81_Hrm.F_89_Pay.EmpBankSalaryEOT" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript">
        $(document).ready(function () {
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);


        });

        function Search_Gridview(strKey, cellNr, gvName) {
            alert(strKey);
            var tblData;

            var strData = strKey.value.toLowerCase().split(" ");
            switch (gvName) {
                case "gvBankPayment":
                    tblData = document.getElementById("<%=gvBankPayment.ClientID %>");
                    break;
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


        }

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
            <div class="card card-fluid">
                <div class="card-body">
                    <div class="row">
                        <div class="col-md-2 col-sm-2 col-lg-2">
                            <div class="form-group">
                                <asp:Label ID="lblWstation" runat="server" CssClass="label">Employee Type</asp:Label>
                                <asp:DropDownList ID="ddlWstation" runat="server" CssClass="chzn-select form-control form-control-sm" OnSelectedIndexChanged="ddlWstation_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-2 col-sm-2 col-lg-2">
                            <div class="form-group">
                                <asp:Label ID="lblDivison" runat="server" CssClass="label">Division</asp:Label>
                                <asp:DropDownList ID="ddlDivision" runat="server" CssClass="chzn-select form-control form-control-sm" OnSelectedIndexChanged="ddlDivision_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-2 col-sm-2 col-lg-2">
                            <div class="form-group">
                                <asp:Label ID="lblDept" runat="server" CssClass="label">Department</asp:Label>
                                <asp:DropDownList ID="ddlDept" runat="server" CssClass="chzn-select form-control form-control-sm" OnSelectedIndexChanged="ddlDept_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-2 col-sm-2 col-lg-2">
                            <div class="form-group">
                                <asp:Label ID="lblSection" runat="server" CssClass="label">Section</asp:Label>
                                <asp:DropDownList ID="ddlSection" runat="server" CssClass="chzn-select form-control form-control-sm"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-1 col-sm-1 col-lg-1 ">
                            <div class="form-group">
                                <asp:Label ID="lblJobLocation" runat="server" CssClass="label">Job Location</asp:Label>
                                <asp:DropDownList ID="ddlJobLocation" runat="server" CssClass="chzn-select form-control form-control-sm"></asp:DropDownList>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-2 col-sm-2 col-lg-2">
                            <div class="form-group">
                                <asp:Label ID="lblMonth" runat="server" CssClass="label">Month</asp:Label>
                                <asp:DropDownList ID="ddlMonth" runat="server" CssClass=" form-control chzn-select " TabIndex="3"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-sm-2 col-md-2 col-lg-2">
                            <div class="form-group">
                                <asp:Label ID="lblEmpName" runat="server" CssClass="label">Bank Name                                   
                                <asp:LinkButton ID="ibtnFindBanK" runat="server" CssClass="label" OnClick="ibtnFindBanK_Click" ToolTip="Bank Name"><i class="fas fa-search"></i></asp:LinkButton>
                                </asp:Label>
                                <asp:DropDownList ID="ddlBankName" runat="server" CssClass="chzn-select form-control form-control-sm"></asp:DropDownList>
                            </div>
                        </div>
                         <div class="col-md-2 col-sm-2 col-lg-2">
                            <asp:Label ID="lblBankStat" runat="server" CssClass="label">Report Type</asp:Label>
                            <asp:DropDownList ID="ddlBankSt" runat="server" CssClass="chzn-select form-control form-control-sm">
                                <asp:ListItem Value="0">Bank Statement</asp:ListItem>
                                <asp:ListItem Value="1">Forwarding Letter</asp:ListItem>
                                <asp:ListItem Value="2">Account Transfer</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                        <div class="col-md-1 col-sm-1 col-lg-1">
                            <div class="form-group">
                                <asp:Label ID="lblottype" runat="server" CssClass="label">OT Type</asp:Label>
                                <asp:DropDownList ID="ddlOtType" runat="server" CssClass="chzn-select form-control form-control-sm">
                                    <asp:ListItem Value="001">OT 1 Hour</asp:ListItem>
                                    <asp:ListItem Value="002">OT 2 Hour</asp:ListItem>
                                    <asp:ListItem Value="003">Off Day</asp:ListItem>
                                    <asp:ListItem Value="007">Compliance Hour</asp:ListItem>
                                    <asp:ListItem Value="009">Holiday Allowance</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-1 col-sm-1 col-lg-1" id="divEmpStatus" runat="server">
                            <div class="form-group">
                               <asp:Label ID="lblEmpStatus" runat="server" CssClass="label">Emp. Status</asp:Label>
                                <asp:DropDownList ID="ddlEmpStatus" runat="server" CssClass="chzn-select form-control form-control-sm">
                                    <asp:ListItem Value="0" Selected="True">Active</asp:ListItem>
                                    <asp:ListItem Value="1">Resign</asp:ListItem>
                                    <asp:ListItem Value="2">Hold</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>             
                        <div class="col-md-half col-sm-half col-lg-half" id="divchkBank" runat="server">
                            <div class="form-group" style="margin-top: 25px;">
                                <asp:CheckBox ID="chkSecondary" runat="server" Text="Sec. Bank" CssClass="checkbox" />
                            </div>
                        </div>
                        
                        <div class="col-md-half col-sm-half col-lg-half ml-2" id="divchkBonus" runat="server">
                            <div class="form-group" style="margin-top: 25px;">
                                <asp:CheckBox ID="chkBonus" runat="server" Text="F. Bonus" CssClass="checkbox" />
                            </div>
                        </div>
                        <div class="col-md-half col-sm-half col-lg-half ml-2">
                            <div class="form-group" style="margin-top: 20px;">
                                <asp:LinkButton ID="lnkbtnShow" runat="server" CssClass="btn btn-primary btn-sm" OnClick="lnkbtnShow_Click">Ok</asp:LinkButton>
                            </div>
                        </div>
                         <div class="col-md-half col-sm-half col-lg-half ml-2" id="divChkEmp" runat="server" visible="false">
                            <div class="form-group" style="margin-top: 25px;">
                                <asp:CheckBox ID="chkAddEmp" runat="server" Text="Add Emp." AutoPostBack="true" OnCheckedChanged="chkAddEmp_CheckedChanged"></asp:CheckBox>
                            </div>
                        </div>
                        <div class="col-md-1 col-sm-1 col-lg-1 ml-2">
                            <div class="form-group">
                                <asp:Label ID="lblPage" runat="server" CssClass="label">Page Size</asp:Label>
                                <asp:DropDownList ID="ddlpagesize" runat="server" CssClass="chzn-select form-control form-control-sm" AutoPostBack="True"
                                    OnSelectedIndexChanged="ddlpagesize_SelectedIndexChanged">
                                    <asp:ListItem>10</asp:ListItem>
                                    <asp:ListItem>15</asp:ListItem>
                                    <asp:ListItem>20</asp:ListItem>
                                    <asp:ListItem>30</asp:ListItem>
                                    <asp:ListItem>50</asp:ListItem>
                                    <asp:ListItem>100</asp:ListItem>
                                    <asp:ListItem>150</asp:ListItem>
                                    <asp:ListItem>200</asp:ListItem>
                                    <asp:ListItem>300</asp:ListItem>
                                    <asp:ListItem>600</asp:ListItem>
                                    <asp:ListItem>900</asp:ListItem>
                                    <asp:ListItem>1100</asp:ListItem>
                                    <asp:ListItem>1200</asp:ListItem>
                                    <asp:ListItem>1500</asp:ListItem>
                                    <asp:ListItem>2000</asp:ListItem>
                                    <asp:ListItem>3000</asp:ListItem>
                                    <asp:ListItem>5000</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-1 col-sm-1 col-lg-1" id="divchkBankLock" runat="server" visible="False">
                            <div class="form-group" style="margin-top: 20px;">
                                <asp:CheckBox ID="lblBankLock" runat="server" CssClass="checkbox" />
                            </div>
                        </div>
                    </div>
                    <div class="row" id="divAddEmp" runat="server" visible="false">
                        <div class="col-md-2 col-sm-2 col-lg-2">
                            <div class="form-group">
                                <asp:Label ID="lblEmployee" runat="server" CssClass="label">Employee</asp:Label>
                                <asp:DropDownList ID="ddlEmployee" runat="server" CssClass="chzn-select form-control form-control-sm"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-1 col-sm-1 col-lg-1">
                            <div class="form-group" style="margin-top: 20px;">
                                <asp:LinkButton ID="lbtnAddEmployee" runat="server" CssClass="btn btn-success btn-sm" OnClick="lbtnAddEmployee_Click" ToolTip="Add Employee">Add Emp.</asp:LinkButton>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="card card-fluid" style="min-height: 450px;">
                <div class="card-body">
                    <div class="row">
                        <asp:GridView ID="gvBankPayment" runat="server" AllowPaging="True" CssClass="table-striped table-hover table-bordered grvContentarea"
                            AutoGenerateColumns="False" OnPageIndexChanging="gvBankPayment_PageIndexChanging"
                            ShowFooter="True">
                            <PagerSettings Position="Bottom" Mode="NumericFirstLast" />
                            <RowStyle />
                            <Columns>
                                <asp:TemplateField HeaderText="SL #">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvSlNo" runat="server" Font-Bold="True"
                                            Style="text-align: right"
                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="">

                                    <HeaderTemplate>
                                        <asp:TextBox ID="txtSearchArt" BorderStyle="None" runat="server" Width="60px" placeholder="Card #" onkeyup="Search_Gridview(this, 1, 'gvBankPayment')"></asp:TextBox><br />
                                    </HeaderTemplate>


                                    <FooterTemplate>
                                        <asp:CheckBox ID="chkBankLock" runat="server" CssClass=" btn btn-primary btn-sm checkbox" Text="Lock" Width="90px" />
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lgIdCard" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "idcard")) %>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" BackColor="White" />


                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="empid" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lgempid" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "empid")) %>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>

                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Employee Name">
                                    <FooterTemplate>
                                        <asp:LinkButton ID="lbtSalUpdate" runat="server" Visible="False" CssClass="btn btn-success btn-sm" OnClick="lbtSalUpdate_Click">Update</asp:LinkButton>
                                    </FooterTemplate>
                                    <HeaderTemplate>
                                        <table style="width: 200px;">
                                            <tr>
                                                <td class="style58">
                                                    <label runat="server" backcolor="#000066"
                                                        bordercolor="White" borderstyle="Solid" borderwidth="1px"
                                                        forecolor="White" style="text-align: center" width="120px">
                                                        Employee Name</label>
                                                </td>
                                                <td class="style60">&nbsp;</td>
                                                <td>
                                                    <asp:HyperLink ID="hlbtnCBdataExel" runat="server" ToolTip="Export To Excel" Width="30px"
                                                        CssClass="btn btn-sm btn-success"><i  class="fa fa-file-excel" aria-hidden="true"></i></asp:HyperLink>

                                                </td>
                                            </tr>
                                        </table>
                                    </HeaderTemplate>

                                    <ItemTemplate>
                                        <asp:Label ID="lgvEmpName" runat="server"
                                            Text='<%#"<b>" +Convert.ToString(DataBinder.Eval(Container.DataItem, "empname"))  %>'
                                            Width="300px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="left" />
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Bank AC No">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvBACNo" runat="server" BackColor="Transparent"
                                            BorderColor="#660033" BorderStyle="Solid" BorderWidth="0px" Font-Size="11px"
                                            Style="text-align: left"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "acno")) %>'
                                            Width="120px">
                                                        
                                        </asp:Label>

                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" />

                                    <FooterStyle HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Designation">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvdesig" runat="server" BackColor="Transparent"
                                            BorderColor="#660033" BorderStyle="Solid" BorderWidth="0px" Font-Size="11px"
                                            Style="text-align: left"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "desig")) %>'
                                            Width="120px">
                                        </asp:Label>
                                    </ItemTemplate>

                                    <ItemStyle HorizontalAlign="Left" />

                                    <FooterStyle HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Department">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvdetname" runat="server" BackColor="Transparent"
                                            BorderColor="#660033" BorderStyle="Solid" BorderWidth="0px" Font-Size="11px"
                                            Style="text-align: left"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "detname")) %>'
                                            Width="100px">
                                        </asp:Label>
                                    </ItemTemplate>

                                    <ItemStyle HorizontalAlign="Left" />

                                    <FooterStyle HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Amount">
                                    <FooterTemplate>
                                        <asp:Label ID="lgvFBamt" runat="server" ForeColor="#000" Font-Size="12px"></asp:Label>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvAmt" runat="server" BackColor="Transparent" Font-Size="11px"
                                            BorderColor="#660033" BorderStyle="Solid" BorderWidth="0px"
                                            Style="text-align: right"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt")).ToString("#,##0;(#,##0); ") %>'
                                            Width="80px"></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Right" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>

                            </Columns>
                            <FooterStyle CssClass="" />
                            <EditRowStyle />
                            <AlternatingRowStyle />
                            <PagerStyle CssClass="gvPagination" />
                            <HeaderStyle CssClass="gvHeader" />
                        </asp:GridView>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>


