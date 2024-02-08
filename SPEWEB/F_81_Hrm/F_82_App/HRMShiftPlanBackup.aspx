<%@ Page Title="" Language="C#" MasterPageFile="~/SPE.Master" AutoEventWireup="true" CodeBehind="HRMShiftPlanBackup.aspx.cs" Inherits="SPEWEB.F_81_Hrm.F_82_App.HRMShiftPlanBackup" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

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
                        <div class="col-md-3 col-lg-3 col-sm-3">
                            <div class="input-group input-group-alt">
                                <div class="input-group-prepend">
                                    <label class="btn btn-secondary" type="button">Shift Planed Date </label>
                                </div>
                                <asp:TextBox ID="txtfromdate" runat="server" AutoCompleteType="Disabled" CssClass="   form-control"></asp:TextBox>
                                <cc1:CalendarExtender ID="txtfromdate_CalendarExtender" runat="server"
                                    Enabled="True" Format="dd-MMM-yyyy" TargetControlID="txtfromdate"></cc1:CalendarExtender>
                            </div>
                        </div>
                        <div class="col-md-3 col-lg-3 col-sm-3">
                            <div class="input-group input-group-alt">
                                <div class="input-group-prepend">
                                    <label class="btn btn-secondary" type="button">Job Location</label>
                                </div>
                                <asp:DropDownList ID="ddlJobLocation" runat="server" CssClass="chzn-select form-control"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-3  col-lg-3 col-sm-3">
                            <asp:LinkButton ID="lnkbtnShowData" runat="server" OnClick="lnkbtnShowData_Click" CssClass="btn btn-md btn-primary">Show</asp:LinkButton>
                        </div>
                    </div>
                </div>
            </div>
            <div class="card card-fluid">
                <div class="card-body" style="min-height: 450px;">
                    <div class="row">
                        <div class="col-md-3 col-lg-3 col-sm-3">
                            <div class="input-group input-group-alt">
                                <div class="input-group-prepend">
                                    <label class="btn btn-secondary" type="button">Off Day Type </label>
                                </div>
                                <asp:DropDownList ID="ddloffdaytype" runat="server" CssClass="form-control pl-0 pr-0" TabIndex="16">
                                    <asp:ListItem Value="0">N/A</asp:ListItem>
                                    <asp:ListItem Value="1">Applicable</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                         <div class="col-md-3 col-lg-3 col-sm-3">
                            <div class="input-group input-group-alt">
                                <div class="input-group-prepend">
                                    <label class="btn btn-secondary" type="button">Shift Name </label>
                                </div>
                                <asp:TextBox ID="txtShiftName" CssClass="form-control disabled" ReadOnly="true" runat="server" TabIndex="1" placeholder="Enter Your Shift Name"></asp:TextBox>
                                <asp:Label ID="lblshiftPLNid" runat="server" CssClass="form-control" Visible="false"></asp:Label>
                            </div>
                        </div>
                    </div>
                    <div class="row">                       
                        <div class="col-md-3 col-lg-3 col-sm-3">
                            <div class="input-group input-group-alt">
                                <div class="input-group-prepend">
                                    <label class="btn btn-secondary" type="button">Machine Start Time </label>
                                </div>
                                <asp:TextBox ID="TxtMachineStime" runat="server" CssClass="form-control" TextMode="Time"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-3 col-lg-3 col-sm-3">
                            <div class="input-group input-group-alt">
                                <div class="input-group-prepend">
                                    <label class="btn btn-secondary" type="button">Shift Start Time </label>
                                </div>
                                <asp:TextBox ID="txtoffintime" runat="server" CssClass="form-control" TextMode="Time"></asp:TextBox>
                                <%--<asp:DropDownList ID="ddlOffintime" AutoPostBack="true" runat="server" CssClass="form-control pl-0 pr-0" TabIndex="16"></asp:DropDownList>--%>
                            </div>
                        </div>

                        <div class="col-md-3 col-lg-3 col-sm-3">
                            <div class="input-group input-group-alt">
                                <div class="input-group-prepend">
                                    <label class="btn btn-secondary" type="button">Late Margin </label>
                                </div>
                                <asp:TextBox ID="txtLateMargin" TabIndex="3" runat="server" onkeypress="return isNumberKey(this,event);" CssClass="form-control">0</asp:TextBox>
                            </div>
                        </div>
                         <div class="col-md-2 col-lg-2 col-sm-2 ">
                            <div class="input-group input-group-alt">
                                <div class="input-group-prepend">
                                    <label class="btn btn-secondary" type="button">Out Grace Time</label>
                                </div>
                                <asp:TextBox ID="txtOutGraceTime" runat="server" onkeypress="return isNumberKey(this,event);" CssClass="form-control">0</asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-3 col-lg-3 col-sm-3">
                            <div class="input-group input-group-alt">
                                <div class="input-group-prepend">
                                    <label class="btn btn-secondary" type="button">Absent Time </label>
                                </div>
                                <asp:TextBox ID="txtabsTime" runat="server" CssClass="form-control" TextMode="Time"></asp:TextBox>

                            </div>
                        </div>
                        <div class="col-md-3 col-lg-3 col-sm-3">
                            <div class="input-group input-group-alt">
                                <div class="input-group-prepend">
                                    <label class="btn btn-secondary" type="button">Lunch/Dinner InTime </label>
                                </div>
                                <asp:TextBox ID="txtLanintime" runat="server" CssClass="form-control" TextMode="Time"></asp:TextBox>
                                <%--<asp:DropDownList ID="ddlLanintime" AutoPostBack="true" runat="server" CssClass="form-control pr-0 pl-0" TabIndex="16"></asp:DropDownList>--%>
                            </div>
                        </div>

                        <div class="col-md-2 col-lg-3 col-sm-3">
                            <div class="input-group input-group-alt">
                                <div class="input-group-prepend">
                                    <label class="btn btn-secondary" type="button">Lunch/Dinner OutTime </label>
                                </div>
                                <asp:TextBox ID="txtLanouttime" runat="server" CssClass="form-control" TextMode="Time"></asp:TextBox>
                                <%--<asp:DropDownList ID="ddlLanouttime" AutoPostBack="true" runat="server" CssClass="form-control pl-0 pr-0" TabIndex="16"></asp:DropDownList>--%>
                            </div>
                        </div>

                        <div class="col-md-2 col-lg-2 col-sm-3">
                            <div class="input-group input-group-alt">
                                <div class="input-group-prepend">
                                    <label class="btn btn-secondary" type="button">Shift OutTime</label>
                                </div>
                                <asp:TextBox ID="txtOffouttime" runat="server" CssClass="form-control" TextMode="Time"></asp:TextBox>
                                <%--<asp:DropDownList ID="ddlOffouttime" AutoPostBack="true" runat="server" CssClass="form-control pl-0 pr-0" TabIndex="16"></asp:DropDownList>--%>
                            </div>
                        </div>
                        <div class="col-md-1 col-lg-1 col-sm-1">
                            <asp:LinkButton ID="lbtnUpdateshiftback" runat="server" OnClick="lbtnUpdateshiftback_Click" CssClass="btn btn-md btn-primary">Update</asp:LinkButton>
                        </div>
                    </div>
                    <div class="row">
                        <asp:GridView ID="grvshift" runat="server" AutoGenerateColumns="False" CssClass="table-striped table-hover table-bordered grvContentarea"
                            OnRowDataBound="grvshift_RowDataBound">
                            <RowStyle />
                            <Columns>
                                <asp:TemplateField HeaderText="Sl#">
                                    <ItemTemplate>
                                        <asp:Label ID="serialnoid0" runat="server" Style="text-align: right"
                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Shift <br> Name">
                                    <ItemTemplate>
                                        <asp:Label ID="lblshiftid" runat="server" Visible="false" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "shiftid")) %>'></asp:Label>

                                        <asp:Label ID="lblshiftName" runat="server" Style="text-align: center"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "shifname")) %>'
                                            Font-Size="11PX" Width="100px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Shift <br> Start" HeaderStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="lblshiftintime" runat="server" Style="text-align: center"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sintime")) %>'
                                            Font-Size="11PX" Width="100px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Machine <br> Start">
                                    <ItemTemplate>
                                        <asp:Label ID="lblMSTime" runat="server" Style="text-align: center"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "macstarttime")) %>'
                                            Font-Size="11PX" Width="100px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Late Margin">
                                    <ItemTemplate>
                                        <asp:Label ID="lbltxtLMTime" runat="server" Style="text-align: center"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "latemarg")) %>'
                                            Font-Size="11PX" Width="100px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Shift <br> End">
                                    <ItemTemplate>
                                        <asp:Label ID="lblshiftouttime" runat="server" Style="text-align: center"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "souttime")) %>'
                                            Font-Size="11PX" Width="100px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>


                                <asp:TemplateField HeaderText="Absent <br> Time">
                                    <ItemTemplate>
                                        <asp:Label ID="lbltxtAbsTime" runat="server" Style="text-align: center"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "abstime")) %>'
                                            Font-Size="11PX" Width="100px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Lunch <br> Break">
                                    <ItemTemplate>
                                        <asp:Label ID="lbllunchinttime" runat="server" Style="text-align: center"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "lintime")) %>'
                                            Font-Size="11PX" Width="100px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Lunch <br> Out Time">
                                    <ItemTemplate>
                                        <asp:Label ID="lbllunchouttime" runat="server" Style="text-align: center"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "louttime")) %>'
                                            Font-Size="11PX" Width="100px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Out Grace <br>Time">
                                    <ItemTemplate>
                                        <asp:Label ID="lblouttgrace" runat="server" Style="text-align: center"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "outtgrace")).ToString("#,##0;(#,##0);") %>'
                                            Font-Size="11px" Width="80px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Off Day Type">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvoffdaytype" runat="server" Style="text-align: center"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "ofdaytype")) %>'
                                            Font-Size="11PX" Width="80px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Location">
                                    <ItemTemplate>
                                        <asp:Label ID="lbllocationid" runat="server" Visible="false" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "locationid")) %>'></asp:Label>
                                        <asp:Label ID="lblgvlocation" runat="server" Style="text-align: center"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "locationdesc")) %>'
                                            Font-Size="11PX" Width="80px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:LinkButton ToolTip="Select Shift" ID="lnkEdit" CssClass="btn btn-xs btn-info" OnClick="lnkEdit_Click" runat="server"><span class="fa fa-edit"></span></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <PagerStyle CssClass="gvPagination" />
                            <HeaderStyle CssClass="gvHeader" />
                            <EditRowStyle />
                            <AlternatingRowStyle />
                        </asp:GridView>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
