<%@ Page Title="" Language="C#" MasterPageFile="~/SPE.Master" AutoEventWireup="true" CodeBehind="HRDailyAttenManually.aspx.cs" Inherits="SPEWEB.F_81_Hrm.F_83_Att.HRDailyAttenManually" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">


    <script type="text/javascript" language="javascript">
        $(document).ready(function () {
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);
        });
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
                                <asp:Label ID="Label5" runat="server" CssClass="control-label">Employee Type</asp:Label>
                                <asp:DropDownList ID="ddlWstation" runat="server" OnSelectedIndexChanged="ddlWstation_SelectedIndexChanged" CssClass="chzn-select form-control form-control-sm" AutoPostBack="true" TabIndex="2"></asp:DropDownList>
                            </div>
                        </div>

                        <div class="col-md-2 col-sm-2 col-lg-2">
                            <div class="form-group">
                                <asp:Label ID="Label6" runat="server" CssClass="control-label">Division</asp:Label>
                                <asp:DropDownList ID="ddlDivision" runat="server" OnSelectedIndexChanged="ddlDivision_SelectedIndexChanged" CssClass="chzn-select form-control form-control-sm" AutoPostBack="true" TabIndex="2"></asp:DropDownList>
                            </div>
                        </div>

                        <div class="col-md-2 col-sm-2 col-lg-2">
                            <div class="form-group">
                                <asp:Label ID="Label7" runat="server" CssClass="control-label">Department</asp:Label>
                                <asp:DropDownList ID="ddlDept" runat="server" OnSelectedIndexChanged="ddlDept_SelectedIndexChanged" CssClass="chzn-select form-contro form-control-sm" AutoPostBack="true" TabIndex="2"></asp:DropDownList>
                            </div>
                        </div>

                        <div class="col-md-2 col-sm-2 col-lg-2">
                            <div class="form-group">
                                <asp:Label ID="Label8" runat="server" CssClass="control-label">Section</asp:Label>
                                <asp:DropDownList ID="ddlSection" runat="server" CssClass="chzn-select form-control form-control-sm"></asp:DropDownList>
                            </div>
                        </div>

                        <div class="col-md-2 col-sm-2 col-lg-2">
                            <div class="form-group">
                                <asp:Label ID="lblline" runat="server" CssClass="control-label">Line</asp:Label>
                                <asp:DropDownList ID="ddlempline" runat="server" CssClass="chzn-select form-control form-control-sm"></asp:DropDownList>
                            </div>
                        </div>

                        <div class="col-md-2 col-sm-2 col-lg-2">
                            <div class="form-group">
                                <asp:Label ID="lbljoblocation" runat="server" CssClass="label">Job Location</asp:Label>
                                <asp:DropDownList ID="ddlJob" runat="server" CssClass="chzn-select form-control form-control-sm" TabIndex="2"></asp:DropDownList>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-2 col-sm-2 col-lg-2">
                            <div class="form-group">
                                <asp:Label ID="Label10" runat="server" CssClass="control-label">Date</asp:Label>
                                <asp:TextBox ID="txtdate" runat="server" CssClass="form-control form-control-sm" Style="height: 26px;"></asp:TextBox>
                                <cc1:CalendarExtender ID="csefdate" runat="server" Format="dd-MMM-yyyy" TargetControlID="txtdate"></cc1:CalendarExtender>
                            </div>
                        </div>
                        <div class="col-md-1 col-sm-1 col-lg-1" id="divToDate" runat="server" visible="false">
                            <div class="form-group">
                                <asp:Label ID="lbldateto" runat="server" CssClass="control-label">To</asp:Label>
                                <asp:TextBox ID="txtdateto" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
                                <cc1:CalendarExtender ID="txtdateto_CalendarExtender" runat="server" Format="dd-MMM-yyyy" TargetControlID="txtdateto"></cc1:CalendarExtender>
                            </div>
                        </div>
                        <div class="col-md-1 col-sm-1 col-lg-1">
                            <div class="form-group">
                                <asp:Label ID="lblSearEmp" runat="server">Card                                  
                                <asp:LinkButton ID="imgbtnSearchEmployee" runat="server" OnClick="imgbtnSearchEmployee_Click" Font-Underline="false" ToolTip="Search By Card"><i class="fa fa-search"></i></asp:LinkButton>
                                </asp:Label>
                                <asp:TextBox ID="txtSrcEmployee" runat="server" CssClass="form-control form-control-sm" placeholder="Card: 10001"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-2 col-sm-2 col-lg-2" id="divFinalUpdate" runat="server" visible="false" style="margin-top: 20px;">
                            <div class="form-group">
                                <asp:LinkButton ID="lFinalUpdatedwise" runat="server" CssClass="btn btn-danger btn-sm" OnClick="lFinalUpdatedwise_Click">Final Update</asp:LinkButton>
                            </div>
                        </div>

                        <div class="col-md-1 col-sm-1 col-lg-1">
                            <div class="form-group">
                                <asp:Label ID="lblPage" runat="server" CssClass="control-label">Page Size</asp:Label>
                                <asp:DropDownList ID="ddlpagesize" runat="server" AutoPostBack="True" CssClass="chzn-select form-control form-control-sm" OnSelectedIndexChanged="ddlpagesize_SelectedIndexChanged">
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
                                    <asp:ListItem>1200</asp:ListItem>
                                    <asp:ListItem>1500</asp:ListItem>
                                    <asp:ListItem>2000</asp:ListItem>
                                    <asp:ListItem>3000</asp:ListItem>
                                    <asp:ListItem>5000</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-2 col-sm-2 col-lg-2" style="margin-top: 20px;">
                            <div class="form-group">
                                <asp:RadioButtonList ID="rbtnAtten" runat="server" AutoPostBack="True"
                                    BackColor="#DFF0D8" BorderColor="#000" CssClass="rbtnList1 margin5px"
                                    Font-Bold="True" Font-Size="11px" ForeColor="Black"
                                    RepeatDirection="Horizontal">
                                    <asp:ListItem Value="29001">Manual</asp:ListItem>
                                    <asp:ListItem Value="28001">Machine</asp:ListItem>
                                    <asp:ListItem Selected="True" Value="00000">Both</asp:ListItem>
                                </asp:RadioButtonList>
                            </div>
                        </div>
                        <div class="col-md-1 col-sm-1 col-lg-1" style="margin-top: 20px;">
                            <div class="form-group">
                                <asp:LinkButton ID="lbtnOk" runat="server" CssClass="btn btn-primary btn-sm" OnClick="lbtnOk_Click">ok</asp:LinkButton>
                            </div>
                        </div>
                        <div class="col-md-2 col-sm-2 col-lg-2" style="margin-top: 20px;">
                            <asp:Panel ID="pnlxcel" runat="server" Visible="false">
                                <asp:Label ID="lblmsg" CssClass="btn-danger primaryBtn btn disabled" runat="server" Visible="false"></asp:Label>
                                <asp:Label ID="lblExel" runat="server" CssClass="formlbl" Visible="false" Text="Upload "></asp:Label>
                                <div class="uploadFile">
                                    <input id="File1" runat="server" name="File1" type="file" class="pull-left" />
                                    <%-- <asp:FileUpload ID="fileuploadExcel" runat="server" Visible="false"  />--%>
                                </div>
                            </asp:Panel>
                        </div>
                        <div class="col-md-1 col-sm-1 col-lg-1" style="margin-top: 20px;">
                            <asp:Panel ID="pnlxcel2" runat="server" Visible="false">
                                <asp:LinkButton ID="btnexcuplosd" runat="server" CssClass=" btn btn-xs btn-success" Text="Upload" OnClick="btnexcuplosd_Click"></asp:LinkButton>
                            </asp:Panel>
                        </div>

                    </div>
                </div>
            </div>
            <div class="card card-fluid">
                <div class="card-body" style="min-height: 450px;">
                    <div class="row table-responsive" runat="server">
                        <asp:GridView ID="gvDailyAttn" runat="server" AutoGenerateColumns="False" ShowFooter="True"
                            AllowPaging="True" OnPageIndexChanging="gvDailyAttn_PageIndexChanging"
                            CssClass="table-striped table-hover table-bordered grvContentarea">
                            <RowStyle />
                            <Columns>
                                <asp:TemplateField HeaderText="Sl">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvSlNo" runat="server" Font-Bold="True" Height="16px" Style="text-align: right"
                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="27px"></asp:Label>
                                    </ItemTemplate>
                                   <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />

                                </asp:TemplateField>
                                 <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:LinkButton runat="server" ID="lnkbtnDeleteAttn" OnClick="lnkbtnDeleteAttn_Click" Width="20px" ToolTip="Delete Attn.">
                                                <i class="fas fa-trash" style="color:red;"></i></asp:LinkButton>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                <asp:TemplateField HeaderText="Section Name">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvsection" runat="server" Font-Size="10px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "section")) %>'
                                            Width="82px"></asp:Label>
                                    </ItemTemplate>

                                    <HeaderTemplate>
                                        <table style="border: none;">
                                            <tr>
                                                <td style="border: none;">
                                                    <asp:Label ID="Label4" runat="server" Font-Bold="True"
                                                        Text="Section Name" Width="120"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:HyperLink ID="hlbtntbCdataExel" CssClass="btn btn-xs btn-success" Style="height: 30px;" runat="server" ToolTip="Export To Excel"><i class="fas fa-file-excel"></i></asp:HyperLink>
                                                </td>
                                            </tr>
                                        </table>
                                    </HeaderTemplate>
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                   <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Emp. ID" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvEmpId" runat="server" Font-Size="9px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "empid")) %>'
                                            Width="60px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                   <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Card #">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvEmpIDCard" runat="server" Height="16px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "idcardno")) %>'
                                            Width="48px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                   <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Employee Name ">
                                    <FooterTemplate>
                                        <asp:LinkButton ID="lFinalUpdate" runat="server" CssClass="btn btn-success btn-sm" OnClick="lFinalUpdate_Click" ToolTip="Update Daily Attendance">Final Update</asp:LinkButton>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvEmpName" runat="server" Height="16px" Text='<%# "<b>"+Convert.ToString(DataBinder.Eval(Container.DataItem, "empname"))+"</b>" %>'
                                            Width="150px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                   <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText=" Designation">
                                    <FooterTemplate>
                                        <asp:LinkButton ID="lnkbtnTotal" runat="server" CssClass="btn btn-primary btn-sm" OnClick="lnkbtnTotal_Click">Total</asp:LinkButton>
                                    </FooterTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvDesig" runat="server" Height="16px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "desig")) %>'
                                            Width="120px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                   <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Shift Name">
                                    <ItemTemplate>
                                        <asp:Label ID="lblshiftname" runat="server" Height="16px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "shiftname")) %>'
                                            Width="75px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterStyle Font-Bold="True" HorizontalAlign="Left" />
                                   <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Off. Intime">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvoffIntime" runat="server" Height="16px" Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "offintime")).ToString("hh:mm tt") %>'
                                            Width="55px"></asp:Label>
                                    </ItemTemplate>
                                   <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Off. Outtime">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvoffouttime" runat="server" Height="16px" Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "offouttime")).ToString("hh:mm tt") %>'
                                            Width="60px"></asp:Label>
                                    </ItemTemplate>
                                   <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Ac. Intime">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtgvIntime" runat="server" BackColor="Transparent" BorderStyle="solid" BorderWidth="1px" BorderColor="green"
                                            Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "intime")).ToString("hh:mm tt") %>'
                                            Width="60px" Font-Size="11px" Visible='<%# (Convert.ToString(DataBinder.Eval(Container.DataItem, "leave")).Trim())!="L" &&(Convert.ToString(DataBinder.Eval(Container.DataItem, "absnt")).Trim())!="A" %>'></asp:TextBox>
                                    </ItemTemplate>
                                   <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Ac. Outtime">
                                    <ItemTemplate>
                                        <table>
                                            <tr>
                                                <td>
                                                    <asp:TextBox ID="txtOutDategv" runat="server" BackColor="Transparent" BorderStyle="solid" BorderWidth="1px" BorderColor="green"
                                                        Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "outtime")).ToString("dd-MMM-yyyy") %>'
                                                        Width="65px" Font-Size="11px" Visible='<%# (Convert.ToString(DataBinder.Eval(Container.DataItem, "leave")).Trim())!="L" &&(Convert.ToString(DataBinder.Eval(Container.DataItem, "absnt")).Trim())!="A" %>'></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtgvOuttime" runat="server" BackColor="Transparent" BorderStyle="solid" BorderWidth="1px" BorderColor="green"
                                                        Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "outtime")).ToString("hh:mm tt") %>'
                                                        Width="55px" Font-Size="11px" Visible='<%# (Convert.ToString(DataBinder.Eval(Container.DataItem, "leave")).Trim())!="L" &&(Convert.ToString(DataBinder.Eval(Container.DataItem, "absnt")).Trim())!="A" %>'></asp:TextBox>
                                                </td>
                                            </tr>
                                        </table>


                                        <cc1:CalendarExtender ID="csesfdate" runat="server" Format="dd-MMM-yyyy" TargetControlID="txtOutDategv"></cc1:CalendarExtender>


                                    </ItemTemplate>
                                   <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />


                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Ln Intime" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvlnintime" runat="server" Height="16px" Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "lnchintime")).ToString("hh:mm tt") %>'
                                            Width="55px" Font-Size="11px"></asp:Label>
                                    </ItemTemplate>
                                   <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Ln Outtime" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvlnouttime" runat="server" Height="16px" Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "lnchouttime")).ToString("hh:mm tt") %>'
                                            Width="55px" Font-Size="11px"></asp:Label>
                                    </ItemTemplate>
                                   <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Leave">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvLeave" runat="server" BackColor="Transparent" BorderStyle="None"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "leave")) %>'
                                            Width="30px" Font-Size="11px"></asp:Label>
                                    </ItemTemplate>
                                   <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Absent">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtgvAbsent" runat="server" BackColor="Transparent" BorderStyle="solid" BorderWidth="1px" BorderColor="green"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "absnt")) %>'
                                            Width="35px" Font-Size="11px"></asp:TextBox>
                                    </ItemTemplate>
                                   <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Deduction">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtgvDed" runat="server" BackColor="Transparent" BorderStyle="solid" BorderWidth="1px" BorderColor="green"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "dedout")).ToString("#,##0.00;(#,##.00); ") %>'
                                            Width="35px" Font-Size="11px"></asp:TextBox>
                                    </ItemTemplate>
                                   <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Add. Hour">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtgvAddHour" runat="server" BackColor="Transparent" BorderStyle="solid" BorderWidth="1px" BorderColor="green"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "addhour")).ToString("#,##0.00;(#,##.00); ") %>'
                                            Width="40px" Font-Size="11px"></asp:TextBox>
                                    </ItemTemplate>
                                   <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Remarks">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtgvremarks" runat="server" BackColor="Transparent" BorderStyle="solid" BorderWidth="1px" BorderColor="green"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "remarks")) %>'
                                            Width="100px" Font-Size="11px"></asp:TextBox>
                                    </ItemTemplate>
                                   <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                </asp:TemplateField>
                            </Columns>
                            <FooterStyle CssClass="" />
                            <EditRowStyle />
                            <AlternatingRowStyle />
                            <PagerSettings Mode="NumericFirstLast" />
                            <PagerStyle CssClass="gvPagination" />
                            <HeaderStyle CssClass="gvHeader" />
                        </asp:GridView>
                        <%-- <cc1:ListSearchExtender ID="ListSearchExtender1" runat="server" QueryPattern="Contains"
                            TargetControlID="ddlProjectName">
                        </cc1:ListSearchExtender>--%>
                    </div>
                </div>
            </div>

        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
