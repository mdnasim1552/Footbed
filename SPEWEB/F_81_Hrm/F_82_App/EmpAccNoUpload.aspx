<%@ Page Title="" Language="C#" MasterPageFile="~/SPEMaster.Master" AutoEventWireup="true" CodeBehind="EmpAccNoUpload.aspx.cs" Inherits="SPEWEB.F_81_Hrm.F_82_App.EmpAccNoUpload" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="container moduleItemWrpper">
        <div class="contentPart">
            <div class="row">
                <fieldset class="scheduler-border fieldset_A">
                    <div class="form-horizontal">
                        <div class="form-group">                                                   
                            <div class="col-md-4">
                                        <div class="formrow">
                                            <asp:Panel ID="pnlxcel" runat="server">
                                                <asp:Label ID="lblExel" runat="server" CssClass="formlbl" Text="Upload "></asp:Label>
                                                <div class="uploadFile">
                                                    <asp:FileUpload ID="fileuploadExcel" runat="server" onchange="submitform();" />
                                                </div>
                                                <asp:LinkButton ID="btnexcuplosd" runat="server" Style="margin-left: 100px;" CssClass=" btn btn-xs btn-success" Text="UPDATE" OnClick="btnexcuplosd_Click" ></asp:LinkButton>
                                            </asp:Panel>
                                        </div>
                                    </div>
                            <div class="col-md-6">
                                <label class="text-primary">Note: Please Ensure Excel File Column Name is ( ID, NAME, BANK, ACC_NO) and Sheet Name Must have Sheet1</label>
                            </div>
                        </div>
                        <div class="form-group hidden">

                            <div class="col-md-3 col-sm-3 col-lg-3 pading5px">
                                <asp:Label ID="Label5" runat="server" CssClass="lblTxt lblName">Employee Type</asp:Label>
                                <asp:DropDownList ID="ddlWstation" runat="server" Width="205" OnSelectedIndexChanged="ddlWstation_SelectedIndexChanged" CssClass="chzn-select pull-left" AutoPostBack="true" TabIndex="2"></asp:DropDownList>
                            </div>
                            <div class="col-md-3 col-sm-3 col-lg-3 pading5px">
                                <asp:Label ID="Label6" runat="server" CssClass="smLbl_to">Division</asp:Label>
                                <asp:DropDownList ID="ddlDivision" runat="server" Width="225" OnSelectedIndexChanged="ddlDivision_SelectedIndexChanged" CssClass="chzn-select pull-left" AutoPostBack="true" TabIndex="2"></asp:DropDownList>
                            </div>
                            <div class="col-md-3 col-sm-3 col-lg-3 pading5px">
                                <asp:Label ID="Label7" runat="server" CssClass="smLbl_to">Department</asp:Label>
                                <asp:DropDownList ID="ddlDept" runat="server" Width="200" OnSelectedIndexChanged="ddlDept_SelectedIndexChanged" CssClass="chzn-select pull-left" AutoPostBack="true" TabIndex="2"></asp:DropDownList>
                            </div>
                            <div class="col-md-3 col-sm-3 col-lg-3 pading5px">
                                <asp:Label ID="Label8" runat="server" CssClass="smLbl_to">Section</asp:Label>
                                <asp:DropDownList ID="ddlSection" runat="server" Width="250" CssClass="chzn-select pull-left  inputTxt"></asp:DropDownList>

                            </div>



                        </div>

                        <div class="form-group hidden">
                            <div class="col-md-3 pading5px asitCol3">
                                <asp:Label ID="lblPage" runat="server" CssClass="lblTxt lblName">Page Size</asp:Label>
                                <asp:DropDownList ID="ddlpagesize" runat="server" AutoPostBack="True" CssClass="ddlPage">
                                    <asp:ListItem>10</asp:ListItem>
                                    <asp:ListItem>15</asp:ListItem>
                                    <asp:ListItem>20</asp:ListItem>
                                    <asp:ListItem>30</asp:ListItem>
                                    <asp:ListItem>50</asp:ListItem>
                                    <asp:ListItem>100</asp:ListItem>
                                    <asp:ListItem>150</asp:ListItem>
                                    <asp:ListItem>200</asp:ListItem>
                                    <asp:ListItem>300</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <div class="col-md-4 pading5px">
                                <div class="pull-left">
                                    <asp:LinkButton ID="lbtnOk" runat="server" CssClass="btn btn-primary okBtn pull-left" OnClick="lbtnOk_Click">ok</asp:LinkButton>
                                </div>
                            </div>


                        </div>
                    </div>
                </fieldset>


                <div class="table-responsive">
                    <asp:GridView ID="Attendencelog" AutoGenerateColumns="false" runat="server" Width="800px" CssClass="table table-bordered table-hover grvContentarea">
                        <Columns>
                            <asp:TemplateField HeaderText="Sl">
                                <ItemTemplate>
                                    <asp:Label ID="lblgvSlNo1" runat="server" Font-Bold="True" Height="16px" Style="text-align: right"
                                        Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="ID Card No">
                                <HeaderTemplate>
                                    <table style="border: none;">
                                        <tr>
                                            <td style="border: none;">
                                                <asp:TextBox ID="txtSearchIdcard" BackColor="Transparent" BorderStyle="None" runat="server" Width="60px" placeholder="ID Card No" onkeyup="Search_Gridview(this,1, 'Attendencelog')"></asp:TextBox><br />

                                            </td>
                                            <td>
                                                <asp:HyperLink ID="hlbtntbCdataExel" CssClass="btn btn-xs btn-success" runat="server"><span class="fa fa-file-excel-o"></span></asp:HyperLink>
                                            </td>
                                        </tr>
                                    </table>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblgvidcardNo" runat="server" Font-Bold="True" Height="16px" Style="text-align: right"
                                        Text='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "idcard")) %>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Employee">
                                <HeaderTemplate>
                                    <asp:TextBox ID="txtSearchEmp" BackColor="Transparent" BorderStyle="None" runat="server" Width="120px" placeholder="Employee" onkeyup="Search_Gridview(this,2, 'Attendencelog')"></asp:TextBox><br />
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblgvEmpName" runat="server" Font-Bold="True" Height="16px" Style="text-align: right"
                                        Text='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "empname")) %>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Designation">
                                <ItemTemplate>
                                    <asp:Label ID="lblgvEmpName" Width="180px" runat="server" Font-Bold="True" Height="16px" Style="text-align: left;"
                                        Text='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "desg")) %>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Depaertment">
                                <ItemTemplate>
                                    <asp:Label ID="lblgvDept" runat="server" Font-Bold="True" Height="16px" Style="text-align: right"
                                        Text='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "depname")) %>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Machine">
                                <HeaderTemplate>
                                    <asp:TextBox ID="txtSearchMachine" BackColor="Transparent" BorderStyle="None" runat="server" Width="90px" placeholder="Machine" onkeyup="Search_Gridview(this,5, 'Attendencelog')"></asp:TextBox><br />
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblgvMachine" runat="server" Font-Bold="True" Height="16px" Style="text-align: right"
                                        Text='<%#  Convert.ToString(DataBinder.Eval(Container.DataItem, "machine")) %>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Out Time">
                                <ItemTemplate>
                                    <asp:Label ID="dayId01" Width="80px" runat="server" Font-Bold="True" Height="16px" Style="text-align: right"
                                        Text='<%#  Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "s1")).ToString("hh:mm tt") %>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="In Time">
                                <ItemTemplate>
                                    <asp:Label ID="dayId02" runat="server" Width="60px" Font-Bold="True" Height="16px" Style="text-align: right"
                                        Text='<%# (Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "s2")).ToString("dd-MMM-yyyy")=="01-Jan-1900")?"": Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "s2")).ToString("hh:mm tt ") %>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Out Time">
                                <ItemTemplate>
                                    <asp:Label ID="dayId03" runat="server" Width="60px" Font-Bold="True" Height="16px" Style="text-align: right"
                                        Text='<%#  (Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "s3")).ToString("dd-MMM-yyyy")=="01-Jan-1900")?"":Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "s3")).ToString("hh:mm tt ") %>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="In Time">
                                <ItemTemplate>
                                    <asp:Label ID="dayId04" runat="server" Width="60px" Font-Bold="True" Height="16px" Style="text-align: right"
                                        Text='<%#  (Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "s4")).ToString("dd-MMM-yyyy")=="01-Jan-1900")?"":Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "s4")).ToString("hh:mm tt ") %>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Out Time">
                                <ItemTemplate>
                                    <asp:Label ID="dayId05" runat="server" Width="60px" Font-Bold="True" Height="16px" Style="text-align: right"
                                        Text='<%#  (Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "s5")).ToString("dd-MMM-yyyy")=="01-Jan-1900")?"":Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "s5")).ToString("hh:mm tt ") %>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="In Time">
                                <ItemTemplate>
                                    <asp:Label ID="dayId06" runat="server" Width="60px" Font-Bold="True" Height="16px" Style="text-align: right"
                                        Text='<%#  (Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "s6")).ToString("dd-MMM-yyyy")=="01-Jan-1900")?"":Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "s6")).ToString("hh:mm tt ") %>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                            </asp:TemplateField>
                        </Columns>
                        <HeaderStyle CssClass="grvHeader" />
                    </asp:GridView>
                </div>
            </div>
        </div>

    </div>
</asp:Content>

