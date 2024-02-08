<%@ Page Title="" Language="C#" MasterPageFile="~/SPE.Master" AutoEventWireup="true" CodeBehind="HREmpShiftSetup.aspx.cs" Inherits="SPEWEB.F_81_Hrm.F_92_Mgt.HREmpShiftSetup" %>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript" language="javascript">
        function FnDanger() {
            $.toaster('Sorry No Data Found of this Section', '<span class="glyphicon glyphicon-info-sign"></span> Information', 'danger');

        }
        $(document).ready(function () {

            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);

        });

        function pageLoaded() {



        }
        function isNumberKey(txt, evt) {
            var charCode = (evt.which) ? evt.which : evt.keyCode;
            if (charCode == 46) {
                //Check if the text already contains the . character
                if (txt.value.indexOf('.') === -1) {
                    return true;
                } else {
                    return false;
                }
            } else {
                if (charCode > 31 &&
                    (charCode < 48 || charCode > 57))
                    return false;
            }
            return true;
        }
    </script>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="card card-fluid">
                <div class="card-body" style="min-height: 400px;">
                    <div class="row">
                        <div class="col-md-3 col-lg-3 col-sm-3">
                            <div class="form-group">
                                <div class="input-group input-group-sm input-group-alt">
                                    <div class="input-group-prepend">
                                        <span class="input-group-text ">Shift Name </span>
                                    </div>
                                    <asp:TextBox ID="txtShiftName" CssClass="form-control " runat="server" TabIndex="1" placeholder="Enter Your Shift Name"></asp:TextBox>
                                    <asp:Label ID="lblshiftPLNid" runat="server" CssClass="form-control" Visible="false"></asp:Label>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-2 col-lg-2 col-sm-2">
                            <div class="input-group input-group-sm  input-group-alt">
                                <div class="input-group-prepend">
                                    <label class="input-group-text " type="button">Shift Start Time </label>
                                </div>
                                <asp:DropDownList ID="ddlOffintime" AutoPostBack="true" runat="server" CssClass="form-control pl-0 pr-0" TabIndex="16"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-2 col-lg-2 col-sm-2">
                            <div class="input-group input-group-sm  input-group-alt">
                                <div class="input-group-prepend">
                                    <label class="input-group-text" type="button">Mac. St. Time </label>
                                </div>
                                <asp:TextBox ID="TxtMachineStime" runat="server" CssClass="form-control" TextMode="Time"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-2 col-lg-2 col-sm-2">
                            <div class="input-group input-group-sm  input-group-alt">
                                <div class="input-group-prepend">
                                    <label class="input-group-text" type="button">Mac. En. Time </label>
                                </div>
                                <asp:TextBox ID="txtMacEndTime" runat="server" CssClass="form-control" TextMode="Time"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-1 col-lg-1 col-sm-1">
                            <div class="input-group input-group-sm  input-group-alt">
                                <div class="input-group-prepend">
                                    <label class="input-group-text" type="button">Late Margin </label>
                                </div>
                                <asp:TextBox ID="txtLateMargin" TabIndex="3" runat="server" onkeypress="return isNumberKey(this,event);" CssClass="form-control">0</asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-2 col-lg-2 col-sm-2">
                            <div class="input-group input-group-sm input-group-alt">
                                <div class="input-group-prepend">
                                    <label class="input-group-text" type="button">Absent Time </label>
                                </div>
                                <asp:TextBox ID="txtabsTime" runat="server" CssClass="form-control" TextMode="Time"></asp:TextBox>

                            </div>
                        </div>
                        <div class="col-md-2 col-lg-2 col-sm-2">
                            <div class="input-group input-group-sm input-group-alt">
                                <div class="input-group-prepend">
                                    <label class="input-group-text" type="button">Lu/Di In </label>
                                </div>
                                <asp:DropDownList ID="ddlLanintime" AutoPostBack="true" runat="server" CssClass="form-control pr-0 pl-0" TabIndex="16"></asp:DropDownList>
                            </div>
                        </div>

                        <div class="col-md-2 col-lg-2 col-sm-2">
                            <div class="input-group input-group-sm input-group-alt">
                                <div class="input-group-prepend">
                                    <span class="input-group-text small">Lu/Di Out </span>
                                </div>
                                <asp:DropDownList ID="ddlLanouttime" AutoPostBack="true" runat="server" CssClass="form-control pl-0 pr-0" TabIndex="16"></asp:DropDownList>
                            </div>
                        </div>

                        <div class="col-md-2 col-lg-2 col-sm-3">
                            <div class="input-group input-group-sm input-group-alt">
                                <div class="input-group-prepend">
                                    <label class="input-group-text" type="button">Shift End Time</label>
                                </div>
                                <asp:DropDownList ID="ddlOffouttime" AutoPostBack="true" runat="server" CssClass="form-control pl-0 pr-0" TabIndex="16"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-1 col-lg-1 col-sm-1">
                            <div class="input-group input-group-sm  input-group-alt">
                                <div class="input-group-prepend">
                                    <label class="input-group-text" type="button">O.T. Grace</label>
                                </div>
                                <asp:TextBox ID="txtouttimegrace" TabIndex="3" runat="server" onkeypress="return isNumberKey(this,event);" CssClass="form-control">0</asp:TextBox>
                            </div>
                        </div>

                        <div class="col-md-1 col-lg-1 col-sm-1">
                            <asp:LinkButton ID="lnkBtnAdd" runat="server" OnClick="lnkBtnAdd_Click" CssClass="btn btn-sm btn-primary">Add</asp:LinkButton>
                        </div>
                    </div>
                    <div class="row mt-2 mb-2">
                        <asp:GridView ID="grvshift" runat="server" AutoGenerateColumns="False" CssClass="table-striped table-bordered table-hover">
                            <RowStyle />
                            <Columns>
                                <asp:TemplateField HeaderText="SL #">
                                    <ItemTemplate>
                                        <asp:Label ID="serialnoid0" runat="server" Style="text-align: right"
                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                    </ItemTemplate>

                                </asp:TemplateField>


                                <asp:TemplateField HeaderText="ID #">
                                    <ItemTemplate>
                                        <asp:Label ID="lblshiftid" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "shiftid")) %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>


                                <asp:TemplateField HeaderText="Shift <br> Name">
                                    <ItemTemplate>
                                        <asp:Label ID="lblshiftName" runat="server" Style="text-align: center"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "shifname")) %>'
                                            Font-Size="11PX" Width="160px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Shift <br> Start Time">
                                    <ItemTemplate>
                                        <asp:Label ID="lblshiftintime" runat="server" Style="text-align: center"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sintime")) %>'
                                            Font-Size="11PX" Width="100px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Machine <br> Start Time">
                                    <ItemTemplate>
                                        <asp:Label ID="lblMSTime" runat="server" Style="text-align: center"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "macstarttime")) %>'
                                            Font-Size="11PX" Width="100px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                 <asp:TemplateField HeaderText="Machine <br> End Time">
                                    <ItemTemplate>
                                        <asp:Label ID="lblMETime" runat="server" Style="text-align: center"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "macendtime")) %>'
                                            Font-Size="11px" Width="100px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Late <br> Margin">
                                    <ItemTemplate>
                                        <asp:Label ID="lbltxtLMTime" runat="server" Style="text-align: center"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "latemarg")) %>'
                                            Font-Size="11PX" Width="80px"></asp:Label>
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

                                <asp:TemplateField HeaderText="Shift <br> End Time">
                                    <ItemTemplate>
                                        <asp:Label ID="lblshiftouttime" runat="server" Style="text-align: center"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "souttime")) %>'
                                            Font-Size="11PX" Width="70px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Out Time <br> Grace">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvotimegrace" runat="server" Style="text-align: center"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "outtgrace")) %>'
                                            Font-Size="11PX" Width="80px"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:LinkButton ToolTip="Apply Shift" ID="lnkApply" OnClientClick="return confirm('Are You Sure to Apply this Shift?');" OnClick="lnkApply_Click" CssClass="btn btn-xs btn-warning pull-left" runat="server"> 
                                       Apply Shift
                                        </asp:LinkButton>

                                        <asp:LinkButton ToolTip="Delete Shift" ID="Lbtnremov" OnClientClick="return confirm('Are You Sure to Delete this Shift?');" OnClick="Lbtnremov_Click" CssClass="btn btn-xs btn-danger pull-left" runat="server"> 
                                       <span class=" fa fa-trash"></span>
                                        </asp:LinkButton>

                                        <asp:LinkButton ToolTip="Edit Shift" ID="lnkEdit" CssClass="btn btn-xs btn-info" OnClick="lnkEdit_Click" runat="server"><span class="fa fa-edit"></span></asp:LinkButton>
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

