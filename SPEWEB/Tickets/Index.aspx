<%@ Page Title="" Language="C#" MasterPageFile="~/SPE.Master" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="SPEWEB.Tickets.Index" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style>
        .page-title{
            font-size: 1.30rem;
        }
        table {
            border: 0;
            margin-top: 10px;
            margin-bottom: 10px;
        }

        .table td, .table th {
            border: 0;
            padding: 5px 12px;
        }

        #loader {
            position: absolute;
            top: 20%;
            left: 40%;
            z-index: 9999;
        }

        .select2-container .select2-selection--single .select2-selection__rendered {
            text-align: left;
        }
    </style>
    <script>

        //function openModal() {
        //    $('#CreatTicketModal').modal('toggle');
        //}

        function RedirTicketCreate() {

            window.open('CreateTicket.aspx', '_blank');

        }

    </script>


    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
           
                    <div class="card" style="height:500px">
                        <div class="card-body">
                          
                                    <div class="form-horizontal">
                                        <div class="form-group row">
                                            <asp:Label for="lblPageSize" runat="server" class="col-1 col-sm-1 col-form-label">Page Size</asp:Label>
                                            <div class="col-1 col-sm-1">
                                                <asp:DropDownList ID="ddlPageSize" runat="server" CssClass="form-control">
                                                    <asp:ListItem Value="50">50</asp:ListItem>
                                                    <asp:ListItem Value="100">100</asp:ListItem>
                                                    <asp:ListItem Value="150">150</asp:ListItem>
                                                    <asp:ListItem Value="200">200</asp:ListItem>
                                                    <asp:ListItem Value="300">300</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                            <div class="col-10">
                                                <asp:LinkButton ForeColor="Green" ID="lnkbtnCreateTicket" runat="server" CssClass="btn btn-sm btn-success float-right mr-2" ToolTip="View Company" BackColor="Transparent" OnClick="lnkbtnCreateTicket_Click">
                                      <i class="fa fa-plus"></i>&nbsp;Create Ticket
                                                </asp:LinkButton>
                                            </div>
                                        </div>
                                    </div>
                      
                                <div class="col-xl-12" >
                                   
                                        <ul class="nav nav-pills navtab-bg nav-justified">
                                            <li class="nav-item border">
                                                <a href="#home1" data-toggle="tab" aria-expanded="false" id="" class="nav-link active">All Tickets <asp:Label Text="(0)" runat="server"  ID="lblaltckcunt"/></a>
                                            </li>


                                            <li class="nav-item border">
                                                <a href="#QC" data-toggle="tab" aria-expanded="false" class="nav-link">QC <asp:Label Text="(0)" runat="server"  ID="lblqctckcunt"/></a>
                                            </li>

                                            <li class="nav-item border">
                                                <a href="#Complete" data-toggle="tab" aria-expanded="false" class="nav-link">Complete <asp:Label Text="(0)" runat="server"  ID="lbldntckcunt"/></a>
                                            </li>

                                        </ul>

                                        <div class="tab-content">
                                            <div class="tab-pane active" id="home1">
                                                <div class="table-responsive">
                                                    <asp:GridView ID="grvacc" runat="server" AllowPaging="True"
                                                        CssClass="table table-centered table-striped dt-responsive dataTable no-footer"
                                                        AutoGenerateColumns="False" Font-Size="12px" OnPageIndexChanging="grvacc_PageIndexChanging"
                                                        PageSize="50" OnRowDataBound="grvacc_RowDataBound"
                                                        ShowFooter="True">
                                                        <PagerSettings NextPageText="Next" PreviousPageText="Previous" />
                                                        <FooterStyle Font-Bold="True"/>

                                                        <Columns>
                                                            <asp:TemplateField HeaderText="Sl" HeaderStyle-Width="30px">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblserialnoid" runat="server" Style="text-align: center"
                                                                        Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle Font-Bold="True" Font-Size="13px" />
                                                                <ItemStyle Font-Size="12px" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Create Date">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblcreatetask" runat="server" Font-Size="12px"
                                                                        Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "createdate")).ToString("dd-MMM-yyyy")=="01-Jan-1900"? ""
                                                    :Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "createdate")).ToString("dd-MMM-yyyy") %>' Width="70px" ></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle Font-Bold="True" Font-Size="13px"  />
                                                                <ItemStyle Font-Size="12px" />

                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Ticket Desc" HeaderStyle-VerticalAlign="Middle" HeaderStyle-HorizontalAlign="Center">
                                                                <ItemTemplate>
                                                                    <a href="TicketDetails.aspx?TicketId=<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sdid")) %>">
                                                                        <asp:Label ID="lbltaskdesc" runat="server" Font-Size="12px"
                                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sddesc")) %>'
                                                                           ></asp:Label></a>
                                                                </ItemTemplate>
                                                                <HeaderStyle Font-Bold="True" Font-Size="13px" />
                                                                <ItemStyle Font-Size="12px" Width="100%" />
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Ticket Type" HeaderStyle-HorizontalAlign="Center">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblTicketType" runat="server" Font-Size="12px"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sddetdesc")) %>'
                                                                        Width="70px"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle Font-Bold="True" Font-Size="13px" />
                                                                <ItemStyle Font-Size="12px" />
                                                            </asp:TemplateField>

                                                            <%--<asp:TemplateField HeaderText="Proposed User" HeaderStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblProposed" runat="server" Font-Size="12px"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "prouname")) %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle Font-Bold="True" Font-Size="13px" />
                                                    <ItemStyle Font-Size="12px" />
                                                </asp:TemplateField>--%>

                                                            <asp:TemplateField HeaderText="Fixed By" HeaderStyle-HorizontalAlign="Center">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblAssign" runat="server" Font-Size="12px"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "updateuser")) %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle Font-Bold="True" Font-Size="13px" />
                                                                <ItemStyle Font-Size="12px" />
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Action">
                                                                <ItemTemplate>
                                                                    <div class="btn-group dropdown">
                                                                        <a href="javascript: void(0);" class="table-action-btn dropdown-toggle arrow-none btn btn-pink btn-sm" data-toggle="dropdown" aria-expanded="false"><i class="mdi mdi-dots-horizontal"></i></a>
                                                                        <div class="dropdown-menu dropdown-menu-right">
                                                                            <asp:LinkButton ID="editTicket" runat="server" Visible="false" CssClass="dropdown-item" ToolTip="Edit Ticket" BackColor="Transparent" OnClick="editTicket_Click">
                                                                    <i class="mdi mdi-pencil mr-2 text-muted font-18 vertical-middle"></i>Edit</asp:LinkButton>

                                                                            <a class="dropdown-item" href="TicketDetails.aspx?TicketId=<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "sdid")) %>"><i class="mdi mdi-eye mr-2 text-muted font-18 vertical-middle"></i>View Ticket</a>

                                                                            <asp:LinkButton ID="lnkDeleteTicket" runat="server" Visible="false" CssClass="dropdown-item" ToolTip="Delete Ticket" BackColor="Transparent" OnClick="lnkDeleteTicket_Click">
                                                                    <i class="mdi mdi-delete mr-2 text-muted font-18 vertical-middle"></i>Delete</asp:LinkButton>

                                                                        </div>
                                                                    </div>
                                                                </ItemTemplate>
                                                                <HeaderStyle Font-Bold="True" Font-Size="13px" />
                                                                <ItemStyle Font-Size="12px" Height="2px"/>
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Created by">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblCreated" runat="server" Font-Size="12px"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "createuser")) %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle Font-Bold="True" Font-Size="13px" />
                                                                <ItemStyle Font-Size="12px" />
                                                            </asp:TemplateField>


                                                        </Columns>
                                                        <AlternatingRowStyle BackColor="" />

                                                    </asp:GridView>
                                                </div>
                                            </div>

                                            <div class="tab-pane" id="QC">
                                                <div class="table-responsive">
                                                    <asp:GridView ID="grvQC" runat="server" AllowPaging="True"
                                                        CssClass="table table-centered table-striped dt-responsive dataTable no-footer"
                                                        AutoGenerateColumns="False" Font-Size="12px" OnPageIndexChanging="grvacc_PageIndexChanging"
                                                        PageSize="50" OnRowDataBound="grvacc_RowDataBound"
                                                        ShowFooter="True">
                                                        <PagerSettings NextPageText="Next" PreviousPageText="Previous" />
                                                        <FooterStyle Font-Bold="True" />

                                                        <Columns>
                                                            <asp:TemplateField HeaderText="Sl" HeaderStyle-Width="30px">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblserialnoid" runat="server" Style="text-align: center"
                                                                        Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle Font-Bold="True" Font-Size="13px" />
                                                                <ItemStyle Font-Size="12px" />
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Create Date">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblcreatetask" runat="server" Font-Size="12px"
                                                                        Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "createtask")).ToString("dd-MMM-yyyy")=="01-Jan-1900"? ""
                                                    :Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "createtask")).ToString("dd-MMM-yyyy") %>' Width="70px"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle Font-Bold="True" Font-Size="13px" />
                                                                 <ItemStyle Font-Size="12px"  />
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Ticket Desc" HeaderStyle-VerticalAlign="Middle" HeaderStyle-HorizontalAlign="Center">
                                                                <ItemTemplate>
                                                                    <a href="TicketDetails.aspx?TicketId=<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "id")) %>">
                                                                        <asp:Label ID="lbltaskdesc" runat="server" Font-Size="12px"
                                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "taskdesc")) %>'
                                                                            Width="100%"></asp:Label></a>
                                                                </ItemTemplate>
                                                                <HeaderStyle Font-Bold="True" Font-Size="13px" />
                                                                <ItemStyle Font-Size="12px" />
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Ticket Type" HeaderStyle-HorizontalAlign="Center">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblTicketType" runat="server" Font-Size="12px"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "tsktypedes")) %>'
                                                                        Width="70px"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle Font-Bold="True" Font-Size="13px" />
                                                                <ItemStyle Font-Size="12px" />
                                                            </asp:TemplateField>

                                                            <%--  <asp:TemplateField HeaderText="Proposed User" HeaderStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblProposed" runat="server" Font-Size="12px"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "prouname")) %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle Font-Bold="True" Font-Size="13px" />
                                                    <ItemStyle Font-Size="12px" />
                                                </asp:TemplateField>--%>

                                                            <asp:TemplateField HeaderText="Fixed By" HeaderStyle-HorizontalAlign="Center">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblAssign" runat="server" Font-Size="12px"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "assignuname")) %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle Font-Bold="True" Font-Size="13px" />
                                                                <ItemStyle Font-Size="12px" />
                                                            </asp:TemplateField>
                                                              <asp:TemplateField HeaderText="Estimated Time">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbltaskprogress" runat="server" Font-Size="12px" CssClass='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "tprogclass")) %>'
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "estimationtime")) %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle Font-Bold="True" Font-Size="13px" />
                                                                <ItemStyle Font-Size="12px" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Ticket Progress">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbltaskprogress" runat="server" Font-Size="12px" CssClass='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "tprogclass")) %>'
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "tskprogdesc")) %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle Font-Bold="True" Font-Size="13px" />
                                                                <ItemStyle Font-Size="12px" />
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Priority">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbltaskpriority" runat="server" Font-Size="12px" CssClass='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "tprirotyclass")) %>'
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "tskprodesc")) %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle Font-Bold="True" Font-Size="13px" />
                                                                <ItemStyle Font-Size="12px" />
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Action">

                                                                <ItemTemplate>
                                                                    <div class="btn-group dropdown">
                                                                        <a href="javascript: void(0);" class="table-action-btn dropdown-toggle arrow-none btn btn-pink btn-sm" data-toggle="dropdown" aria-expanded="false"><i class="mdi mdi-dots-horizontal"></i></a>
                                                                        <div class="dropdown-menu dropdown-menu-right">
                                                                            <asp:LinkButton ID="TickQcDoneEng" runat="server" Visible="true" CssClass="dropdown-item" ToolTip="QC Done" BackColor="Transparent" OnClick="TickQcDoneEng_Click">
                                                                <i class="mdi mdi-book-play mr-2 text-muted font-18 vertical-middle"></i>QC Done</asp:LinkButton>
                                                                            <asp:LinkButton ID="lnkForwardAll" runat="server" CssClass="dropdown-item" ToolTip="Start Ticket" BackColor="Transparent" OnClick="lnkForwardAll_Click">
                                                                   <i class="mdi mdi-backup-restore mr-2 text-muted font-18 vertical-middle"></i>QC Cancel</asp:LinkButton>
                                                                            <a class="dropdown-item" href="TicketDetails.aspx?TicketId=<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "id")) %>"><i class="mdi mdi-eye mr-2 text-muted font-18 vertical-middle"></i>View Ticket</a>

                                                                        </div>
                                                                    </div>

                                                                </ItemTemplate>
                                                                <HeaderStyle Font-Bold="True" Font-Size="13px" />
                                                                <ItemStyle Font-Size="12px" />
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Created by">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblCreated" runat="server" Font-Size="12px"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "createUsername")) %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle Font-Bold="True" Font-Size="13px" />
                                                                <ItemStyle Font-Size="12px" />
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Remarks">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblRemarks" runat="server" Font-Size="12px"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "remarks")) %>'
                                                                        Width="100px"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle Font-Bold="True" Font-Size="13px" />
                                                                <ItemStyle Font-Size="12px" />
                                                            </asp:TemplateField>

                                                        </Columns>
                                                        <AlternatingRowStyle BackColor="" />

                                                    </asp:GridView>
                                                </div>
                                            </div>
                                            <div class="tab-pane" id="Complete">
                                                <div class="table-responsive">
                                                    <asp:GridView ID="grvComplete" runat="server" AllowPaging="True"
                                                        CssClass="table table-centered table-striped dt-responsive dataTable no-footer"
                                                        AutoGenerateColumns="False" Font-Size="12px" OnPageIndexChanging="grvacc_PageIndexChanging"
                                                        PageSize="50" OnRowDataBound="grvacc_RowDataBound"
                                                        ShowFooter="True">
                                                        <PagerSettings NextPageText="Next" PreviousPageText="Previous" />
                                                        <FooterStyle Font-Bold="True" />

                                                        <Columns>
                                                            <asp:TemplateField HeaderText="Sl" HeaderStyle-Width="30px">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblserialnoid" runat="server" Style="text-align: center"
                                                                        Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle Font-Bold="True" Font-Size="13px" />
                                                                <ItemStyle Font-Size="12px" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Create Date">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblcreatetask" runat="server" Font-Size="12px"
                                                                        Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "createtask")).ToString("dd-MMM-yyyy")=="01-Jan-1900"? ""
                                                    :Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "createtask")).ToString("dd-MMM-yyyy") %>' Width="70px"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle Font-Bold="True" Font-Size="13px" />
                                                                 <ItemStyle Font-Size="12px" />
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Ticket Desc" HeaderStyle-VerticalAlign="Middle" HeaderStyle-HorizontalAlign="Center">
                                                                <ItemTemplate>
                                                                    <a href="TicketDetails.aspx?TicketId=<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "id")) %>">
                                                                        <asp:Label ID="lbltaskdesc" runat="server" Font-Size="12px"
                                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "taskdesc")) %>'
                                                                            Width="300px"></asp:Label></a>
                                                                </ItemTemplate>
                                                                <HeaderStyle Font-Bold="True" Font-Size="13px" />
                                                                <ItemStyle Font-Size="12px" />
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Ticket Type" HeaderStyle-HorizontalAlign="Center">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblTicketType" runat="server" Font-Size="12px"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "tsktypedes")) %>'
                                                                        Width="100px"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle Font-Bold="True" Font-Size="13px" />
                                                                <ItemStyle Font-Size="12px" />
                                                            </asp:TemplateField>

                                                            <%--<asp:TemplateField HeaderText="Proposed User" HeaderStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblProposed" runat="server" Font-Size="12px"
                                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "prouname")) %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle Font-Bold="True" Font-Size="13px" />
                                                    <ItemStyle Font-Size="12px" />
                                                </asp:TemplateField>--%>

                                                            <asp:TemplateField HeaderText="Fixed By" HeaderStyle-HorizontalAlign="Center">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblAssign" runat="server" Font-Size="12px"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "assignuname")) %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle Font-Bold="True" Font-Size="13px" />
                                                                <ItemStyle Font-Size="12px" />
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Ticket Progress">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbltaskprogress" runat="server" Font-Size="12px" CssClass='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "tprogclass")) %>'
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "tskprogdesc")) %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle Font-Bold="True" Font-Size="13px" />
                                                                <ItemStyle Font-Size="12px" />
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Priority">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lbltaskpriority" runat="server" Font-Size="12px" CssClass='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "tprirotyclass")) %>'
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "tskprodesc")) %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle Font-Bold="True" Font-Size="13px" />
                                                                <ItemStyle Font-Size="12px" />
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Created by">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblCreated" runat="server" Font-Size="12px"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "createUsername")) %>'></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle Font-Bold="True" Font-Size="13px" />
                                                                <ItemStyle Font-Size="12px" />
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Remarks">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblRemarks" runat="server" Font-Size="12px"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "remarks")) %>'
                                                                        Width="100px"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle Font-Bold="True" Font-Size="13px" />
                                                                <ItemStyle Font-Size="12px" />
                                                            </asp:TemplateField>

                                                        </Columns>
                                                        <AlternatingRowStyle BackColor="" />

                                                    </asp:GridView>
                                                </div>
                                            </div>

                                        </div>
                                 
                                </div>
                          
                        </div>
                    </div>
               

        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

