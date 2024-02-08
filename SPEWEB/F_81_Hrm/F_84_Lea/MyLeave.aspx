<%@ Page Title="" Language="C#" MasterPageFile="~/SPE.Master" AutoEventWireup="true" CodeBehind="MyLeave.aspx.cs" Inherits="SPEWEB.F_81_Hrm.F_84_Lea.MyLeave" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:Label ID="lblempid" Style="display: none;" runat="server" CssClass="lblTxt lblName"></asp:Label>

    <div class="card card-fluid">
        <div class="card-body">

            <div class="row">

                <div class="col-md-2 col-sm-2 col-lg-2 ">
                    <div class="form-group">
                        <asp:LinkButton ID="imgbtnlAppEmpSeaarch" runat="server" OnClick="imgbtnlAppEmpSeaarch_Click" CssClass="label">Employee Name</asp:LinkButton>
                        <asp:DropDownList ID="ddlEmpName" runat="server" OnSelectedIndexChanged="ddlEmpName_SelectedIndexChanged" CssClass="chzn-select form-control form-control-sm" AutoPostBack="true" TabIndex="2"></asp:DropDownList>
                    </div>
                </div>

                <div class="col-md-1 col-sm-1 col-lg-1 ">
                    <div class="form-group">
                        <asp:Label ID="Label14" runat="server" CssClass="label">Joining Date:</asp:Label>
                        <asp:Label ID="lblJoiningDate" runat="server" CssClass=" smLbl_to"></asp:Label>
                    </div>

                </div>
                <div class="col-md-2 col-sm-2 col-lg-2 ">
                    <div class="form-group">
                        <asp:Label ID="Label9" runat="server" CssClass="label">Apply Date</asp:Label>
                        <asp:TextBox ID="txtaplydate" runat="server" CssClass="form-control form-control-sm "></asp:TextBox>
                        <cc1:CalendarExtender ID="txtaplydate_CalendarExtender1" runat="server" Format="dd-MMM-yyyy" TargetControlID="txtaplydate"></cc1:CalendarExtender>
                    </div>
                </div>
                <div class="col-md-3 col-sm-3 col-lg-3 ">
                    <div class="form-group" style="margin-top: 20px;">
                        <asp:CheckBox ID="chkPreLeave" runat="server" AutoPostBack="True" OnCheckedChanged="chkPreLeave_CheckedChanged" TabIndex="13" Text="Previous Leave" CssClass="checkbox" />

                        <asp:LinkButton ID="lnkbtnRef" runat="server" CssClass="btn btn-primary btn-sm pull-left" OnClick="lnkbtnRef_Click">Refresh</asp:LinkButton>
                    </div>


                </div>
                <div class="col-md-4 col-sm-4 col-lg-4 ">

                    <asp:Panel ID="PnlPreLeave" runat="server" Visible="False">
                        <div class="row">
                            <div class="col-md-6 col-sm-6 col-lg-6 ">
                                <div class="form-group">
                                    <asp:Label ID="Label13" runat="server" CssClass="label">Pre. Leave</asp:Label>
                                    <asp:DropDownList ID="ddlPreLeave" runat="server" CssClass="chzn-select form-control form-control-sm" TabIndex="2"></asp:DropDownList>
                                </div>
                            </div>

                            <div class="col-md-1 col-sm-1 col-lg-1 ">
                                <div class="form-group" style="margin-top: 20px;">
                                    <asp:LinkButton ID="lnkbtnPreLeave" runat="server" CssClass="btn btn-primary btn-sm pull-left" OnClick="lnkbtnPreLeave_Click">Show</asp:LinkButton>
                                </div>
                            </div>




                        </div>

                    </asp:Panel>
                </div>



                


            </div>
        </div>
    </div>

    <div class="row">
        <div class="col-md-5 col-sm-5 col-lg-5 ">
            <section class="card" style="min-height: 350px;">
               
                  <div class="log-divider" id="lblleaveApp" runat="server" visible="false"> <span>
                    <i class="fa fa-fw fa-dollar-sign"></i>Leave Application</span> </div>

                <asp:GridView ID="gvLeaveApp" runat="server" AutoGenerateColumns="False" ShowFooter="True" CssClass="table-striped table-hover table-bordered grvContentarea">
                    <RowStyle />
                    <Columns>
                        <asp:TemplateField HeaderText="Sl">
                            <ItemTemplate>
                                <asp:Label ID="lblgvSlNo0" runat="server" Font-Bold="True" Height="16px" Style="text-align: right"
                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Desription">
                            <ItemTemplate>
                                <asp:Label ID="lblgvDescription" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gdesc")) %>'
                                    Width="150px"></asp:Label>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:LinkButton ID="lnkbtnUpdateLeave" runat="server" CssClass="btn  btn-danger primaryBtn" OnClick="lnkbtnUpdateLeave_Click">Update </asp:LinkButton>
                            </FooterTemplate>
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="left" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Applied For">
                            <ItemTemplate>
                                <asp:TextBox ID="txtgvlapplied" runat="server" BorderStyle="None" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "lapplied")).ToString("#,##0;(#,##0); ") %>'
                                    Width="80px" BackColor="Transparent" Font-Size="12px"
                                    Style="text-align: right"></asp:TextBox>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:LinkButton ID="lbtnDelete" runat="server" Font-Bold="True" CssClass="btn btn-primary primaryBtn" OnClick="lbtnDelete_Click">Delete</asp:LinkButton>
                            </FooterTemplate>
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="right" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Leave Std. Date">
                            <ItemTemplate>
                                <asp:TextBox ID="txtgvenjoydt1" runat="server" BorderStyle="None" Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "lenjoydt1")).ToString("dd-MMM-yyyy") %>'
                                    Width="100px" BackColor="Transparent" Font-Size="12px"></asp:TextBox>
                                <cc1:CalendarExtender ID="txtgvenjoydt1_CalendarExtender" runat="server" Enabled="True"
                                    Format="dd-MMM-yyyy" TargetControlID="txtgvenjoydt1"></cc1:CalendarExtender>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="left" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Leave End Date">
                            <ItemTemplate>
                                <asp:TextBox ID="txtgvenjoydt2" runat="server" BorderStyle="None" Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "lenjoydt2")).ToString("dd-MMM-yyyy ") %>'
                                    Width="100px" Visible='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "lenjoydt2")).ToString("dd-MMM-yyyy")!="01-Jan-1900" %>'
                                    BackColor="Transparent" Font-Size="12px"></asp:TextBox>
                                <cc1:CalendarExtender ID="txtgvenjoydt2_CalendarExtender" runat="server" Enabled="True"
                                    Format="dd-MMM-yyyy" TargetControlID="txtgvenjoydt2"></cc1:CalendarExtender>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="left" />
                        </asp:TemplateField>
                    </Columns>
                    <FooterStyle CssClass="grvFooter" />
                    <EditRowStyle />
                    <AlternatingRowStyle />
                    <PagerStyle CssClass="gvPagination" />
                    <HeaderStyle CssClass="grvHeader" />
                </asp:GridView>

                <asp:Panel ID="PnlRmrks" runat="server" Visible="False">
                 
                <div class="row" >
                    <div class="col-md-6 col-sm-6 col-lg-6 ">
                        <div class="form-group">
                            <asp:Label ID="Label1" runat="server" CssClass="label">Reason(s) :</asp:Label>
                            <asp:TextBox ID="txtLeavLreasons" runat="server" CssClass="form-control form-control-sm " TextMode="MultiLine"></asp:TextBox>

                        </div>
                    </div>
                    <div class="col-md-6 col-sm-6 col-lg-6 ">
                        <div class="form-group">
                            <asp:Label ID="Label15" runat="server" CssClass="label">Address of enjoing time:</asp:Label>
                            <asp:TextBox ID="txtaddofenjoytime" runat="server" CssClass="form-control form-control-sm " TextMode="MultiLine"></asp:TextBox>

                        </div>
                    </div>


                </div>

                <div class="row">

                    <div class="col-md-6 col-sm-6 col-lg-6 ">
                        <div class="form-group">
                            <asp:Label ID="Label16" runat="server" CssClass="label">Remarks :</asp:Label>
                            <asp:TextBox ID="txtLeavRemarks" runat="server" CssClass="form-control form-control-sm " TextMode="MultiLine"></asp:TextBox>

                        </div>
                    </div>
                    <div class="col-md-6 col-sm-6 col-lg-6 ">
                        <div class="form-group">
                            <asp:Label ID="Label17" runat="server" CssClass="label">While on Leave, Duties will Performed by :</asp:Label>
                            <asp:TextBox ID="txtdutiesnameandDesig" runat="server" CssClass="form-control form-control-sm " TextMode="MultiLine"></asp:TextBox>

                        </div>
                    </div>
                </div>


            </asp:Panel>
            </section>

        </div>

        <div class="col-md-7 col-sm-7 col-lg-7 ">
            <section class="card" style="height: 350px;">
                 <div class="log-divider" id="lblleaveStatus" runat="server" visible="false"> <span>
                    <i class="fa fa-fw fa-dollar-sign"></i>Leave Status</span> </div>

                <asp:GridView ID="gvLeaveStatus" runat="server" AutoGenerateColumns="False" ShowFooter="True" CssClass="table-striped table-hover table-bordered grvContentarea">
                    <RowStyle />
                    <Columns>
                        <asp:TemplateField HeaderText="Sl">
                            <ItemTemplate>
                                <asp:Label ID="lblgvSlNo1" runat="server" Font-Bold="True" Height="16px" Style="text-align: right"
                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="10px"></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Desription">
                            <ItemTemplate>
                                <asp:Label ID="lblgvDescription0" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gdesc")) %>'
                                    Width="130px"></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="left" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Opening </br> Bal.">
                            <ItemTemplate>
                                <asp:Label ID="lblgvlentitled0" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "entitle")).ToString("#,##0;(#,##0); ") %>'
                                    Width="50px"></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="right" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Entitlement">
                            <ItemTemplate>
                                <asp:Label ID="lblgvlentitled1" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "permonth")).ToString("#,##0;(#,##0); ") %>'
                                    Width="60px"></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="right" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Leave </br>This Year">
                            <ItemTemplate>
                                <asp:Label ID="lblgvlentitled1" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ltaken")).ToString("#,##0;(#,##0); ") %>'
                                    Width="50px"></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="right" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Present </br>Bal.">
                            <ItemTemplate>
                                <asp:Label ID="lblgvlentitled1" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "pbal")).ToString("#,##0;(#,##0); ") %>'
                                    Width="50px"></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="right" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Requested">
                            <ItemTemplate>
                                <asp:Label ID="lblgvballeave1" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "applyday")).ToString("#,##0;(#,##0); ") %>'
                                    Width="60px"></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="right" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Approved">
                            <ItemTemplate>
                                <asp:Label ID="lblgvballeave1" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "appday")).ToString("#,##0;(#,##0); ") %>'
                                    Width="50px"></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="right" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Closing </br> Bal.">
                            <ItemTemplate>
                                <asp:Label ID="lblgvballeave1" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "balleave")).ToString("#,##0;(#,##0); ") %>'
                                    Width="50px"></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="right" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Last Leave </br>Std. Date">
                            <ItemTemplate>
                                <asp:Label ID="lblgvenjoydt10" runat="server" Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "lenjoydt1")).ToString("dd-MMM-yyyy") %>'
                                    Width="80px" Visible='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "lenjoydt1")).ToString("dd-MMM-yyyy")!="01-Jan-1900" %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="left" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Last Leave</br> End Date">
                            <ItemTemplate>
                                <asp:Label ID="lblgvleavedt20" runat="server" Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "lenjoydt2")).ToString("dd-MMM-yyyy ") %>'
                                    Width="80px" Visible='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "lenjoydt2")).ToString("dd-MMM-yyyy")!="01-Jan-1900" %>'></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="left" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Last Leave</br> Day's">
                            <ItemTemplate>
                                <asp:Label ID="lblgvenjoyday0" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "lenjoyday")).ToString("#,##0;(#,##0); ") %>'
                                    Width="50px"></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="right" />
                        </asp:TemplateField>
                    </Columns>
                    <FooterStyle CssClass="grvFooter" />
                    <EditRowStyle />
                    <AlternatingRowStyle />
                    <PagerStyle CssClass="gvPagination" />
                    <HeaderStyle CssClass="grvHeader" />
                </asp:GridView>

                <div class="log-divider" id="lblleaveInformation" runat="server" visible="false"> <span>
                    <i class="fa fa-fw fa-dollar-sign"></i>  Previous Leave Information</span> </div>

                <asp:GridView ID="gvleaveInfo" runat="server" AutoGenerateColumns="False" CssClass="table-striped table-hover table-bordered grvContentarea"
                    ShowFooter="True" OnRowDataBound="gvleaveInfo_RowDataBound"
                    OnRowDeleting="gvleaveInfo_RowDeleting">
                    <RowStyle />
                    <Columns>
                        <asp:TemplateField HeaderText="Sl">
                            <ItemTemplate>
                                <asp:Label ID="lgvSlNo3" runat="server" Font-Bold="True" Height="16px"
                                    Style="text-align: right"
                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="10px"></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="trnleaveid" Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="lgvltrnleaveid" runat="server"
                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "ltrnid")) %>'
                                    Width="120px"></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="left" />
                        </asp:TemplateField>

                        <asp:CommandField ShowDeleteButton="True" ControlStyle-ForeColor="Red" DeleteText="<span class='fa fa-trash'></span>" />

                        <asp:TemplateField HeaderText="Desription">
                            <ItemTemplate>
                                <asp:Label ID="lgvledescription" runat="server"
                                    Text='<%# "<B>"+ Convert.ToString(DataBinder.Eval(Container.DataItem, "gdesc")) + "</B>"+
                                                                         (DataBinder.Eval(Container.DataItem, "grpdesc").ToString().Trim().Length>0 ? 
                                                                         (Convert.ToString(DataBinder.Eval(Container.DataItem, "gdesc")).Trim().Length>0 ?  "<br>" : "")+                                                             
                                                                         "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"+ 
                                                                         Convert.ToString(DataBinder.Eval(Container.DataItem, "grpdesc")).Trim(): "") 
                                                                         
                                                                    %>'
                                    Width="120px">
                                                                
                                                                
                                                                
                                </asp:Label>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="left" />
                        </asp:TemplateField>


                        <asp:TemplateField HeaderText="Apply Date">
                            <ItemTemplate>
                                <asp:Label ID="lgvapplydate" runat="server"
                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "aplydat")) %>'
                                    Width="80px"></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="left" />
                        </asp:TemplateField>


                        <asp:TemplateField HeaderText="From Date">
                            <ItemTemplate>
                                <asp:Label ID="lgvlstartdate" runat="server"
                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "lstrtdat")) %>'
                                    Width="80px"></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="left" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="End Date">
                            <ItemTemplate>
                                <asp:Label ID="lgvlenddate" runat="server"
                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "lenddat"))%>'
                                    Width="80px"></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="left" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Leave </br> Days">
                            <ItemTemplate>
                                <asp:Label ID="lgvleavedays" runat="server"
                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "enjoyday")).ToString("#,##0;(#,##0); ") %>'
                                    Width="40px"></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="right" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Reason">
                            <ItemTemplate>
                                <asp:Label ID="lgvreason" runat="server"
                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "lreason")) %>'
                                    Width="140px"></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="left" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Remarks">
                            <ItemTemplate>
                                <asp:Label ID="lgvremarks" runat="server"
                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "lrmarks")) %>'
                                    Width="140px"></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" />
                            <ItemStyle HorizontalAlign="left" />
                        </asp:TemplateField>




                    </Columns>
                    <FooterStyle CssClass="grvFooter" />
                    <EditRowStyle />
                    <AlternatingRowStyle />
                    <PagerStyle CssClass="gvPagination" />
                    <HeaderStyle CssClass="grvHeader" />
                </asp:GridView>
            </section>
        </div>

    </div>
   
    <div class="form-group">
                    <div class="col-md-3 pading5px asitCol3">
                        <asp:RadioButtonList ID="rblstapptype" runat="server" CssClass="rbtnList1 chkBoxControl" RepeatColumns="6" RepeatDirection="Horizontal"
                            Width="220px" TabIndex="16" Visible="False">
                            <asp:ListItem>Type 1</asp:ListItem>
                            <asp:ListItem>Type 2</asp:ListItem>
                            <asp:ListItem>Type 3</asp:ListItem>
                        </asp:RadioButtonList>

                        <asp:Label ID="lblComPany" runat="server" CssClass="inputTxt" Visible="False"></asp:Label>
                        <asp:Label ID="lblSection" runat="server" CssClass="inputTxt" Visible="False"></asp:Label>
                        <asp:Label ID="lblDesignation" runat="server" CssClass=" smLbl_to" Visible="False"></asp:Label>
                        <div class="col-md-1 col-sm-1 col-lg-1 ">
                            <div class="form-group">
                                <asp:Label ID="lbltrnleaveid" runat="server" Visible="False"></asp:Label>

                            </div>
                        </div>
                    </div>
                </div>

</asp:Content>

