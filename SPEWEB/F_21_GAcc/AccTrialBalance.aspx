<%@ Page Title="" Language="C#" MasterPageFile="~/SPE.Master" AutoEventWireup="true" CodeBehind="AccTrialBalance.aspx.cs" Inherits="SPEWEB.F_21_GAcc.AccTrialBalance" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript">
        $(document).ready(function () {
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);


        });
        function pageLoaded() {

            var gv1 = $('#<%=this.dgv1.ClientID %>');
            gv1.Scrollable();


            $("input, select").bind("keydown", function (event) {
                var k1 = new KeyPress();
                k1.textBoxHandler(event);

            });
        }

    </script>

    <style>
        .switch {
            position: relative;
            display: inline-block;
            width: 40px;
            height: 20px;
        }

            .switch input {
                opacity: 0;
                width: 0;
                height: 0;
            }

        .slider {
            position: absolute;
            cursor: pointer;
            top: 0;
            left: 0;
            right: 0;
            bottom: 0;
            background-color: #ccc;
            -webkit-transition: .4s;
            transition: .4s;
        }

            .slider:before {
                position: absolute;
                content: "";
                height: 18px;
                width: 18px;
                left: 1px;
                bottom: 1px;
                background-color: white;
                -webkit-transition: .4s;
                transition: .4s;
            }

        input:checked + .slider {
            background-color: #2196F3;
        }

        input:focus + .slider {
            box-shadow: 0 0 1px #2196F3;
        }

        input:checked + .slider:before {
            -webkit-transform: translateX(18px);
            -ms-transform: translateX(18px);
            transform: translateX(18px);
        }

        /* Rounded sliders */
        .slider.round {
            border-radius: 20px;
        }

            .slider.round:before {
                border-radius: 50%;
            }
    </style>





    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="nahidProgressbar">
                <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1" DisplayAfter="30">
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


            <asp:MultiView ID="MultiView1" runat="server">
                <asp:View ID="TrialBalance" runat="server">
                    <div class="card card-fluid">
                        <div class="card-body" style="min-height: 350px;">
                            <div class="row">
                                <div class="col-md-2 col-sm-2 col-lg-2 ">
                                    <div class="form-group">
                                        <asp:Label ID="lblDaterange" runat="server" CssClass="label">From</asp:Label>
                                        <asp:TextBox ID="txtDatefrom" runat="server" CssClass="form-control form-control-sm small"></asp:TextBox>
                                        <cc1:CalendarExtender ID="txtDatefrom_CalendarExtender2" runat="server" Format="dd-MMM-yyyy" TargetControlID="txtDatefrom"></cc1:CalendarExtender>
                                    </div>
                                </div>
                                <div class="col-md-2 col-sm-2 col-lg-2 ">
                                    <div class="form-group">
                                        <asp:Label ID="Label7" runat="server" CssClass="label">To</asp:Label>
                                        <asp:TextBox ID="txtDateto" runat="server" CssClass="form-control form-control-sm small"></asp:TextBox>
                                        <cc1:CalendarExtender ID="txtDateto_CalendarExtender9" runat="server" Format="dd-MMM-yyyy" TargetControlID="txtDateto"></cc1:CalendarExtender>
                                    </div>
                                </div>
                                <div class="col-md-2 col-sm-2 col-lg-2 ">
                                    <div class="form-group">
                                        <asp:Label ID="lblchelist" runat="server" CssClass="label">Report Level</asp:Label>

                                        <asp:DropDownList ID="ddlReportLevel" runat="server" CssClass="chzn-select form-control form-control-sm" TabIndex="6">
                                            <asp:ListItem Value="1">Level-1</asp:ListItem>
                                            <asp:ListItem Value="2">Level-2</asp:ListItem>
                                            <asp:ListItem Value="3">Level-3</asp:ListItem>
                                            <asp:ListItem Selected="True" Value="4">Level-4</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-1 col-sm-1 col-lg-1 ">
                                    <div class="form-group" style="margin-top: 20px;">
                                        <asp:LinkButton ID="lnkok" runat="server" CssClass="btn btn-primary btn-sm pull-left" OnClick="lnkok_Click">Ok</asp:LinkButton>
                                    </div>
                                </div>
                            </div>

                            <div class="row">
                                <asp:GridView ID="dgv1" runat="server" AutoGenerateColumns="False" CssClass="table-striped table-hover table-bordered grvContentarea"
                                    OnRowDataBound="dgv1_RowDataBound" ShowFooter="True" Width="500px"
                                    PageSize="20">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sl">
                                            <ItemTemplate>
                                                <asp:Label ID="lblserialnoid" runat="server" Style="text-align: right"
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="33px"></asp:Label>
                                            </ItemTemplate>

                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Code">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvcode" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actcode4")) %>' Width="95px"></asp:Label>
                                            </ItemTemplate>

                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Decription" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvAcDesc" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc4")) %>' Width="95px"></asp:Label>
                                            </ItemTemplate>

                                        </asp:TemplateField>
                                        <asp:TemplateField FooterText="Total"
                                            HeaderText="Description of Accounts">
                                            <HeaderTemplate>
                                                <table style="width: 300px;">
                                                    <tr>
                                                        <td class="style58">
                                                            <asp:Label ID="Label4" runat="server" Font-Bold="True"
                                                                Text="Description Of Accounts" Width="180px"></asp:Label>
                                                        </td>
                                                        <td class="style60">&nbsp;</td>
                                                        <td>
                                                            <asp:HyperLink ID="hlbtntbCdataExel" runat="server" BackColor="#000066"
                                                                BorderColor="White" BorderStyle="Solid" BorderWidth="1px" Font-Bold="True"
                                                                ForeColor="White" Style="text-align: center" Width="90px">Export Exel</asp:HyperLink>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:HyperLink ID="HLgvDesc" runat="server" _
                                                    Font-Size="12px" Font-Underline="False" Target="_blank"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc4")) %>'
                                                    Width="300px"></asp:HyperLink>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle HorizontalAlign="left" />
                                            <FooterStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Opening <br/> Dr. Amt">
                                            <FooterTemplate>
                                                <asp:Label ID="lblfopndramt" runat="server" Font-Bold="True"></asp:Label>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvopndramt" runat="server"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "opndram")).ToString("#,##0.00;(#,##0.00); ") %>' Width="110px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle HorizontalAlign="Right" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>


                                        <asp:TemplateField
                                            HeaderText="Opening <br/> Cr. Amt">
                                            <FooterTemplate>
                                                <asp:Label ID="lblfopncramt" runat="server"></asp:Label>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvopncramt" runat="server"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "opncram")).ToString("#,##0.00;(#,##0.00); ") %>' Width="110px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="true" HorizontalAlign="Right" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>




                                        <asp:TemplateField HeaderText="Dr. Amount">
                                            <FooterTemplate>
                                                <asp:Label ID="lblfDramt" runat="server"></asp:Label>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvDramt" runat="server"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "dram")).ToString("#,##0.00;(#,##0.00); ") %>' Width="110px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="true" HorizontalAlign="Right" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Cr. Amount">
                                            <FooterTemplate>
                                                <asp:Label ID="lblfCramt" runat="server"></asp:Label>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvCramt" runat="server"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "cram")).ToString("#,##0.00;(#,##0.00); ") %>' Width="110px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="true" HorizontalAlign="Right" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Closing  <br/> Dr.  Amt">
                                            <FooterTemplate>
                                                <asp:Label ID="lblfclodramt" runat="server"></asp:Label>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvclodramt" runat="server"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "closdram")).ToString("#,##0.00;(#,##0.00); ") %>' Width="110px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="true" HorizontalAlign="Right" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="Closing    <br/> Cr. Amt">
                                            <FooterTemplate>
                                                <asp:Label ID="lblfclocramt" runat="server"></asp:Label>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvclocramt" runat="server"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "closcram")).ToString("#,##0.00;(#,##0.00); ") %>' Width="110px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="true" HorizontalAlign="Right" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Net Amt">
                                            <FooterTemplate>
                                                <asp:Label ID="lblfnetamt" runat="server"></asp:Label>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvnetamt" runat="server"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "closam")).ToString("#,##0.00;(#,##0.00); ") %>' Width="110px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="true" HorizontalAlign="Right" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>



                                        <asp:TemplateField HeaderText="">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvdrcr" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "drcr")) %>' Width="20px"></asp:Label>
                                            </ItemTemplate>

                                        </asp:TemplateField>


                                    </Columns>
                                    <FooterStyle CssClass="grvFooter" />
                                    <EditRowStyle />
                                    <AlternatingRowStyle />
                                    <PagerStyle CssClass="gvPagination" />
                                    <HeaderStyle CssClass="grvHeader" />
                                </asp:GridView>

                            </div>
                        </div>
                    </div>





                </asp:View>

                <asp:View ID="DetailsTrial" runat="server">

                    <div class="card card-fluid">
                        <div class="card-body" style="min-height: 350px;">
                            <div class="row">
                                <div class="col-md-2 col-sm-2 col-lg-2 ">
                                    <div class="form-group">
                                        <asp:Label ID="Label20" runat="server" CssClass="label">From</asp:Label>
                                        <asp:TextBox ID="txtDatefromd" runat="server" CssClass="form-control form-control-sm small"></asp:TextBox>
                                        <cc1:CalendarExtender ID="txtDatefromd_CalendarExtender9" runat="server" Format="dd-MMM-yyyy" TargetControlID="txtDatefromd"></cc1:CalendarExtender>
                                    </div>
                                </div>
                                <div class="col-md-2 col-sm-2 col-lg-2 ">
                                    <div class="form-group">
                                        <asp:Label ID="Label21" runat="server" CssClass="label">To</asp:Label>
                                        <asp:TextBox ID="txtDatetod" runat="server" CssClass="form-control form-control-sm small"></asp:TextBox>
                                        <cc1:CalendarExtender ID="txtDatetod_CalendarExtender10" runat="server" Format="dd-MMM-yyyy" TargetControlID="txtDatetod"></cc1:CalendarExtender>
                                    </div>
                                </div>
                                <div class="col-md-2 col-sm-2 col-lg-2 ">
                                    <div class="form-group">
                                        <asp:Label ID="lblAccLevel" runat="server" CssClass="label">Level(Accounts)</asp:Label>

                                        <asp:DropDownList ID="ddlacclevel" runat="server" CssClass="chzn-select form-control form-control-sm" TabIndex="6">
                                            <asp:ListItem Value="2">Level-1</asp:ListItem>
                                            <asp:ListItem Value="4">Level-2</asp:ListItem>
                                            <asp:ListItem Value="8" Selected="True">Level-3</asp:ListItem>
                                            <asp:ListItem Value="12">Details</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-2 col-sm-2 col-lg-2 ">
                                    <div class="form-group">
                                        <asp:Label ID="Label22" runat="server" CssClass="label">Level(Resource)</asp:Label>

                                        <asp:DropDownList ID="ddlReportLevelDetails" runat="server" CssClass="chzn-select form-control form-control-sm" TabIndex="6">
                                            <asp:ListItem Value="2">Main</asp:ListItem>
                                            <asp:ListItem Value="4">Sub-1</asp:ListItem>
                                            <asp:ListItem Value="7">Sub-2</asp:ListItem>
                                            <asp:ListItem Value="9">Sub-3</asp:ListItem>
                                            <asp:ListItem Value="12" Selected="True">Details</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-1 col-sm-1 col-lg-1 ">
                                    <div class="form-group" style="margin-top: 20px;">
                                        <asp:LinkButton ID="lnkDetailsok" runat="server" CssClass="btn btn-primary btn-sm pull-left" OnClick="lnkDetailsok_Click">Ok</asp:LinkButton>
                                    </div>
                                </div>
                            </div>

                            <div class="row">
                                <asp:GridView ID="gvDetails" runat="server" AutoGenerateColumns="False"
                                    CssClass=" table-striped table-hover table-bordered grvContentarea gvTopHeader"
                                    OnRowDataBound="gvDetails_RowDataBound" ShowFooter="True"
                                    PageSize="20">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sl">
                                            <ItemTemplate>
                                                <asp:Label ID="lblserialnoid0" runat="server" Style="text-align: right"
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="40px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle Font-Bold="True" Font-Size="16px" />
                                            <ItemStyle Font-Size="12px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Code">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvcoded" runat="server" CssClass="GridLebelL"
                                                    Text='<%# "<B>"+ Convert.ToString(DataBinder.Eval(Container.DataItem, "actcode4")) + "</B>"+
                                                                         (DataBinder.Eval(Container.DataItem, "rescode4").ToString().Trim().Length>0? 
                                                                   (Convert.ToString(DataBinder.Eval(Container.DataItem, "actcode4")).Trim().Length>0 ? "<br>" : "") + 
                                                                         "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + 
                                                                         Convert.ToString(DataBinder.Eval(Container.DataItem, "rescode4")).Trim(): "") 
                                                                          %>'
                                                    Width="120px">
                                                                          
                                                </asp:Label>

                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField FooterStyle-Font-Bold="true" FooterStyle-Font-Size="12px"
                                            FooterStyle-HorizontalAlign="Right"
                                            HeaderText="Description">
                                            <HeaderTemplate>
                                                <table style="width: 100%;">
                                                    <tr>
                                                        <td class="style58">
                                                            <asp:Label ID="Label4" runat="server" Font-Bold="True" Text="Description "></asp:Label>
                                                        </td>
                                                        <td class="style59">&nbsp;</td>
                                                        <td>
                                                            <asp:HyperLink ID="hlbtnCdataExel" runat="server" BackColor="#000066"
                                                                BorderColor="White" BorderStyle="Solid" BorderWidth="1px" Font-Bold="True"
                                                                ForeColor="White">Export Exel</asp:HyperLink>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </HeaderTemplate>
                                            <ItemTemplate>

                                                <asp:HyperLink ID="HLgvDesc" runat="server" CssClass="GridLebelL" ForeColor="Black" Font-Underline="False" Target="_blank"
                                                    Text='<%# "<B>"+ Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc4")) + "</B>"+
                                                                         (DataBinder.Eval(Container.DataItem, "resdesc").ToString().Trim().Length>0 ? 
                                                                         (Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc4")).Trim().Length>0 ? "<br>" : "") + 
                                                                         "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;" + 
                                                                         Convert.ToString(DataBinder.Eval(Container.DataItem, "resdesc")).Trim(): "") 
                                                                          %>'
                                                    Width="400px">
                                                             
                                                             
                                                </asp:HyperLink>

                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label ID="lblfopDes" runat="server" CssClass="GridLebel"
                                                    Font-Bold="True"></asp:Label>
                                            </FooterTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle HorizontalAlign="left" />
                                            <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField FooterStyle-Font-Bold="true" FooterStyle-Font-Size="12px"
                                            FooterStyle-HorizontalAlign="Right" HeaderText="Opening Amt"
                                            ItemStyle-HorizontalAlign="Right">
                                            <FooterTemplate>
                                                <asp:Label ID="lblfopnamtd" runat="server"
                                                    Font-Bold="True"></asp:Label>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvopnamtd" runat="server" CssClass="GridLebel"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "opnam")).ToString("#,##0.00;(#,##0.00); ") %>' Width="90px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Right" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField FooterStyle-Font-Bold="true" FooterStyle-Font-Size="12px"
                                            FooterStyle-HorizontalAlign="Right" HeaderText="Dr. Amount"
                                            ItemStyle-HorizontalAlign="Right">
                                            <FooterTemplate>
                                                <asp:Label ID="lblfDramtd" runat="server" CssClass="GridLebel" Font-Bold="True"></asp:Label>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvDramtd" runat="server" CssClass="GridLebel"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "dram")).ToString("#,##0.00;(#,##0.00); ") %>' Width="90px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Right" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField FooterStyle-Font-Bold="true" FooterStyle-Font-Size="12px"
                                            FooterStyle-HorizontalAlign="Right" HeaderText="Cr. Amount"
                                            ItemStyle-HorizontalAlign="Right">
                                            <FooterTemplate>
                                                <asp:Label ID="lblfCramtd" runat="server" CssClass="GridLebel" Font-Bold="True"></asp:Label>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvCramtd" runat="server" CssClass="GridLebel"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "cram")).ToString("#,##0.00;(#,##0.00); ") %>' Width="90px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Right" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField FooterStyle-Font-Bold="true" FooterStyle-Font-Size="12px"
                                            FooterStyle-HorizontalAlign="Right" HeaderText="Closing Amount"
                                            ItemStyle-HorizontalAlign="Right">
                                            <FooterTemplate>
                                                <asp:Label ID="lblfcloamtd" runat="server" CssClass="GridLebel"
                                                    Font-Bold="True"></asp:Label>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvclobald" runat="server" CssClass="GridLebel"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "closam")).ToString("#,##0.00;(#,##0.00); ") %>' Width="90px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Right" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                    </Columns>
                                    <FooterStyle CssClass="grvFooter" />
                                    <EditRowStyle />
                                    <AlternatingRowStyle />
                                    <PagerStyle CssClass="gvPagination" />
                                    <HeaderStyle CssClass="grvHeader" />
                                </asp:GridView>
                            </div>

                        </div>
                    </div>






                </asp:View>
                <asp:View ID="ViewBankPosition" runat="server">
                    <div class="card card-fluid">
                        <div class="card-body" style="min-height: 350px;">
                            <div class="row">
                                <div class="col-md-2 col-sm-2 col-lg-2 ">
                                    <div class="form-group">
                                        <asp:Label ID="Label1" runat="server" CssClass="label">From</asp:Label>
                                        <asp:TextBox ID="txtDatefrombank" runat="server" CssClass="form-control form-control-sm small"></asp:TextBox>
                                        <cc1:CalendarExtender ID="txtDatefrombank_CalendarExtender1" runat="server" Format="dd-MMM-yyyy" TargetControlID="txtDatefrombank"></cc1:CalendarExtender>
                                    </div>
                                </div>
                                <div class="col-md-2 col-sm-2 col-lg-2 ">
                                    <div class="form-group">
                                        <asp:Label ID="Label8" runat="server" CssClass="label">To</asp:Label>
                                        <asp:TextBox ID="txtDatetobank" runat="server" CssClass="form-control form-control-sm small"></asp:TextBox>
                                        <cc1:CalendarExtender ID="txtDatetobank_CalendarExtender2" runat="server" Format="dd-MMM-yyyy" TargetControlID="txtDatetobank"></cc1:CalendarExtender>
                                    </div>
                                </div>
                                <div class="col-md-2 col-sm-2 col-lg-2 ">
                                    <div class="form-group">
                                        <asp:Label ID="Label10" runat="server" CssClass="label">Report Level</asp:Label>

                                        <asp:DropDownList ID="ddlReportLevelBank" runat="server" CssClass="chzn-select form-control form-control-sm" TabIndex="6">
                                            <asp:ListItem Value="2">Level-1</asp:ListItem>
                                            <asp:ListItem Value="4">Level-2</asp:ListItem>
                                            <asp:ListItem Value="8">Level-3</asp:ListItem>
                                            <asp:ListItem Selected="True" Value="12">Details</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-1 col-sm-1 col-lg-1 ">
                                    <div class="form-group" style="margin-top: 20px;">
                                        <asp:LinkButton ID="lnkbtnBankPosition" runat="server" CssClass="btn btn-primary btn-sm pull-left" OnClick="lnkbtnBankPosition_Click">Ok</asp:LinkButton>
                                    </div>
                                </div>
                            </div>

                            <div class="row">
                                <asp:GridView ID="gvBankPosition" runat="server" AutoGenerateColumns="False" CssClass=" table-striped table-hover table-bordered grvContentarea gvTopHeader" ShowFooter="True" OnRowDataBound="gvBankPosition_RowDataBound">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sl">
                                            <ItemTemplate>
                                                <asp:Label ID="lblserialnoid1" runat="server" Style="text-align: right"
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle Font-Bold="True" Font-Size="16px" />
                                            <ItemStyle Font-Size="12px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Code">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvcodebank" runat="server" CssClass="GridLebel"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actcode")) %>' Width="90px"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField FooterStyle-Font-Bold="true" FooterStyle-Font-Size="12px"
                                            FooterStyle-HorizontalAlign="Right"
                                            HeaderText="Description of Accounts">
                                            <HeaderTemplate>
                                                <table style="width: 100%;">
                                                    <tr>
                                                        <td class="style58" style="width: auto">
                                                            <asp:Label ID="Label4" runat="server" Font-Bold="True" Text="Description of Accounts"></asp:Label>
                                                        </td>

                                                        <td>
                                                            <asp:HyperLink ID="hlbtnbnkpdataExel" runat="server" BackColor="#000066"
                                                                BorderColor="White" BorderStyle="Solid" BorderWidth="1px" Font-Bold="True"
                                                                ForeColor="White">Export Exel</asp:HyperLink>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:HyperLink ID="HLgvDescbank" runat="server" __designer:wfdid="w38"
                                                    CssClass="GridLebelL" Font-Size="12px" Font-Underline="False" Target="_blank"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc")) %>'
                                                    Width="400px"></asp:HyperLink>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle HorizontalAlign="left" />
                                            <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Right" />
                                        </asp:TemplateField>

                                        <asp:TemplateField FooterStyle-Font-Bold="true" FooterStyle-Font-Size="12px"
                                            FooterStyle-HorizontalAlign="Right" HeaderText="Opening Balance"
                                            ItemStyle-HorizontalAlign="Right">

                                            <ItemTemplate>
                                                <asp:Label ID="lblgvopnbal" runat="server" CssClass="GridLebel"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "opndram")).ToString("#,##0.00;(#,##0.00); ") %>' Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Right" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>

                                        <asp:TemplateField FooterStyle-Font-Bold="true" FooterStyle-Font-Size="12px"
                                            FooterStyle-HorizontalAlign="Right" HeaderText="Opening Liabilities"
                                            ItemStyle-HorizontalAlign="Right">

                                            <ItemTemplate>
                                                <asp:Label ID="lblgvopnliabilities" runat="server" CssClass="GridLebel"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "opncram")).ToString("#,##0.00;(#,##0.00); ") %>' Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Right" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField FooterStyle-Font-Bold="true" FooterStyle-Font-Size="12px"
                                            FooterStyle-HorizontalAlign="Right" HeaderText="Deposit"
                                            ItemStyle-HorizontalAlign="Right">

                                            <ItemTemplate>
                                                <asp:Label ID="lblgvDramtbank" runat="server" CssClass="GridLebel"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "dram")).ToString("#,##0.00;(#,##0.00); ") %>' Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Right" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField FooterStyle-Font-Bold="true" FooterStyle-Font-Size="12px"
                                            FooterStyle-HorizontalAlign="Right" HeaderText="Withdrawn"
                                            ItemStyle-HorizontalAlign="Right">

                                            <ItemTemplate>
                                                <asp:Label ID="lblgvCramtbank" runat="server" CssClass="GridLebel"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "cram")).ToString("#,##0.00;(#,##0.00); ") %>' Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Right" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>


                                        <asp:TemplateField FooterStyle-Font-Bold="true" FooterStyle-Font-Size="12px"
                                            FooterStyle-HorizontalAlign="Right" HeaderText="Closing Balance"
                                            ItemStyle-HorizontalAlign="Right">

                                            <ItemTemplate>
                                                <asp:Label ID="lblgvclobalbank" runat="server" CssClass="GridLebel"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "closdram")).ToString("#,##0.00;(#,##0.00); ") %>' Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Right" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>

                                        <asp:TemplateField FooterStyle-Font-Bold="true" FooterStyle-Font-Size="12px"
                                            FooterStyle-HorizontalAlign="Right" HeaderText="Closing Liabilities"
                                            ItemStyle-HorizontalAlign="Right">

                                            <ItemTemplate>
                                                <asp:Label ID="lblgvcloliabilities" runat="server" CssClass="GridLebel"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "closcram")).ToString("#,##0.00;(#,##0.00); ") %>' Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Right" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>



                                        <asp:TemplateField FooterStyle-Font-Bold="true" FooterStyle-Font-Size="12px"
                                            FooterStyle-HorizontalAlign="Right" HeaderText="Bank Limit"
                                            ItemStyle-HorizontalAlign="Right">

                                            <ItemTemplate>
                                                <asp:Label ID="lblgvbankLim" runat="server" CssClass="GridLebel"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "bankamt")).ToString("#,##0.00;(#,##0.00); ") %>' Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Right" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField FooterStyle-Font-Bold="true" FooterStyle-Font-Size="12px"
                                            FooterStyle-HorizontalAlign="Right" HeaderText="Bank Available Balance"
                                            ItemStyle-HorizontalAlign="Right">

                                            <ItemTemplate>
                                                <asp:Label ID="lblgvbankBal" runat="server" CssClass="GridLebel"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "bankbal")).ToString("#,##0.00;(#,##0.00); ") %>' Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Right" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>

                                    </Columns>
                                    <FooterStyle CssClass="grvFooter" />
                                    <EditRowStyle />
                                    <AlternatingRowStyle />
                                    <PagerStyle CssClass="gvPagination" />
                                    <HeaderStyle CssClass="grvHeader" />
                                </asp:GridView>


                            </div>

                        </div>
                    </div>




                </asp:View>
                <asp:View ID="ViewConsolidated" runat="server">
                    <div class="card card-fluid">
                        <div class="card-body" style="min-height: 350px;">
                            <div class="row">
                                <div class="col-md-2 col-sm-2 col-lg-2 ">
                                    <div class="form-group">
                                        <asp:Label ID="Label3" runat="server" CssClass="label">As On Date</asp:Label>
                                        <asp:TextBox ID="txtAsDate" runat="server" CssClass="form-control form-control-sm small"></asp:TextBox>
                                        <cc1:CalendarExtender ID="txtAsDate_CalendarExtender1" runat="server" Format="dd-MMM-yyyy" TargetControlID="txtAsDate"></cc1:CalendarExtender>
                                    </div>
                                </div>

                                <div class="col-md-2 col-sm-2 col-lg-2 ">
                                    <div class="form-group">
                                        <asp:Label ID="Label12" runat="server" CssClass="label">Report Level</asp:Label>

                                        <asp:DropDownList ID="ddlReportLevelcon" runat="server" CssClass="chzn-select form-control form-control-sm" TabIndex="6">
                                            <asp:ListItem Value="2">Level-1</asp:ListItem>
                                            <asp:ListItem Value="4">Level-2</asp:ListItem>
                                            <asp:ListItem Value="8">Level-3</asp:ListItem>
                                            <asp:ListItem Selected="True" Value="12">Level-4</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-1 col-sm-1 col-lg-1 ">
                                    <div class="form-group" style="margin-top: 20px;">
                                        <asp:LinkButton ID="lnkTrialBalCon" runat="server" CssClass="btn btn-primary btn-sm pull-left" OnClick="lnkTrialBalCon_Click">Ok</asp:LinkButton>
                                    </div>
                                </div>
                            </div>


                            <div class="row">


                                <asp:GridView ID="gvtbcon" runat="server" AutoGenerateColumns="False"
                                    CssClass=" table-striped table-hover table-bordered grvContentarea gvTopHeader"
                                    OnRowDataBound="gvtbcon_RowDataBound" PageSize="20" ShowFooter="True">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sl">
                                            <ItemTemplate>
                                                <asp:Label ID="lblserialnoid2" runat="server" Style="text-align: right"
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle Font-Bold="True" Font-Size="16px" />
                                            <ItemStyle Font-Size="12px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Code">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvcodecon" runat="server" CssClass="GridLebel"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actcode4")) %>'
                                                    Width="95px"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField FooterStyle-Font-Bold="true" FooterStyle-Font-Size="12px"
                                            FooterStyle-HorizontalAlign="Right" FooterText="Total"
                                            HeaderText="Description of Accounts">
                                            <HeaderTemplate>
                                                <table style="width: 47%;">
                                                    <tr>
                                                        <td class="style58">
                                                            <asp:Label ID="Label5" runat="server" Font-Bold="True"
                                                                Text="Description Of Accounts" Width="180px"></asp:Label>
                                                        </td>
                                                        <td class="style60">&nbsp;</td>
                                                        <td>
                                                            <asp:HyperLink ID="hlbtntbCdataExelcon" runat="server" BackColor="#000066"
                                                                BorderColor="White" BorderStyle="Solid" BorderWidth="1px" Font-Bold="True"
                                                                ForeColor="White" Style="text-align: center" Width="90px">Export Exel</asp:HyperLink>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:HyperLink ID="HLgvDesccon" runat="server" __designer:wfdid="w38"
                                                    CssClass="GridLebelL" Font-Size="12px" Font-Underline="False" Target="_blank"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc4")) %>'
                                                    Width="300px"></asp:HyperLink>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle HorizontalAlign="left" />
                                            <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Right" />
                                        </asp:TemplateField>


                                        <asp:TemplateField FooterStyle-Font-Bold="true" FooterStyle-Font-Size="12px"
                                            FooterStyle-HorizontalAlign="Right" HeaderText="Closing <br /> Dr. Amount"
                                            ItemStyle-HorizontalAlign="Right">
                                            <FooterTemplate>
                                                <asp:Label ID="lblfClosDramtcon" runat="server" CssClass="GridLebel" Font-Bold="True"></asp:Label>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblgClosDramtcon" runat="server" CssClass="GridLebel"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "closdram")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="90px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Right" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField FooterStyle-Font-Bold="true" FooterStyle-Font-Size="12px"
                                            FooterStyle-HorizontalAlign="Right" HeaderText="Closing <br /> Cr. Amount"
                                            ItemStyle-HorizontalAlign="Right">
                                            <FooterTemplate>
                                                <asp:Label ID="lblfClosCramtcon" runat="server" CssClass="GridLebel" Font-Bold="True"></asp:Label>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvClosCramtcon" runat="server" CssClass="GridLebel"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "closcram")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="90px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Right" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField FooterStyle-Font-Bold="true" FooterStyle-Font-Size="12px"
                                            FooterStyle-HorizontalAlign="Right" HeaderText="Net  &lt;br/&gt; Dr.  Amt"
                                            ItemStyle-HorizontalAlign="Right">
                                            <FooterTemplate>
                                                <asp:Label ID="lblfnetdramtcon" runat="server" CssClass="GridLebel"
                                                    Font-Bold="True"></asp:Label>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvnetdramtcon" runat="server" CssClass="GridLebel"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "netdram")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="90px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Right" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField FooterStyle-Font-Bold="true" FooterStyle-Font-Size="12px"
                                            FooterStyle-HorizontalAlign="Right" HeaderText="Net  &lt;br/&gt; Cr. Amt"
                                            ItemStyle-HorizontalAlign="Right">
                                            <FooterTemplate>
                                                <asp:Label ID="lblfnetcramtcon" runat="server" CssClass="GridLebel"
                                                    Font-Bold="True"></asp:Label>
                                            </FooterTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvnetcramtcon" runat="server" CssClass="GridLebel"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "netcram")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="90px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Right" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>


                                    </Columns>
                                    <FooterStyle CssClass="grvFooter" />
                                    <EditRowStyle />
                                    <AlternatingRowStyle />
                                    <PagerStyle CssClass="gvPagination" />
                                    <HeaderStyle CssClass="grvHeader" />
                                </asp:GridView>

                            </div>
                        </div>
                    </div>
                </asp:View>
                <asp:View ID="ViewBankPos02" runat="server">
                    <div class="card card-fluid">
                        <div class="card-body" style="min-height: 350px;">
                            <div class="row">
                                <div class="col-md-2 col-sm-2 col-lg-2 ">
                                    <div class="form-group">
                                        <asp:Label ID="Label11" runat="server" CssClass="label">As On Date</asp:Label>
                                        <asp:TextBox ID="txtAsDateb" runat="server" CssClass="form-control form-control-sm small"></asp:TextBox>
                                        <cc1:CalendarExtender ID="txtAsDateb_CalendarExtender1" runat="server" Format="dd-MMM-yyyy" TargetControlID="txtAsDateb"></cc1:CalendarExtender>
                                    </div>
                                </div>

                                <div class="col-md-2 col-sm-2 col-lg-2 ">
                                    <div class="form-group">
                                        <asp:Label ID="Label15" runat="server" CssClass="label">Report Level</asp:Label>

                                        <asp:DropDownList ID="ddlReportLevelbk02" runat="server" CssClass="chzn-select form-control form-control-sm" TabIndex="6">
                                            <asp:ListItem Value="2">Level-1</asp:ListItem>
                                            <asp:ListItem Value="4">Level-2</asp:ListItem>
                                            <asp:ListItem Value="8">Level-3</asp:ListItem>
                                            <asp:ListItem Selected="True" Value="12">Level-4</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-1 col-sm-1 col-lg-1 ">
                                    <div class="form-group" style="margin-top: 20px;">
                                        <asp:LinkButton ID="lnkBankPosition02" runat="server" CssClass="btn btn-primary btn-sm pull-left" OnClick="lnkBankPosition02_Click">Ok</asp:LinkButton>
                                    </div>
                                </div>
                            </div>
                            <div class="row">

                                <asp:GridView ID="gvBankPosition02" runat="server" AutoGenerateColumns="False"
                                    CssClass=" table-striped table-hover table-bordered grvContentarea gvTopHeader"
                                    ShowFooter="True">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sl">
                                            <ItemTemplate>
                                                <asp:Label ID="lblserialnoid3" runat="server" Style="text-align: right"
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle Font-Bold="True" Font-Size="16px" />
                                            <ItemStyle Font-Size="12px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Code">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvcodebank02" runat="server" CssClass="GridLebel"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actcode")) %>'
                                                    Width="90px"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField FooterStyle-Font-Bold="true" FooterStyle-Font-Size="12px"
                                            FooterStyle-HorizontalAlign="Right" HeaderText="Description of Accounts">
                                            <HeaderTemplate>
                                                <table style="width: 100%;">
                                                    <tr>
                                                        <td class="style58" style="width: auto">
                                                            <asp:Label ID="Label6" runat="server" Font-Bold="True"
                                                                Text="Description of Accounts"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:HyperLink ID="hlbtnbnkpdataExel02" runat="server" BackColor="#000066"
                                                                BorderColor="White" BorderStyle="Solid" BorderWidth="1px" Font-Bold="True"
                                                                ForeColor="White">Export Exel</asp:HyperLink>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:HyperLink ID="HLgvDescbank02" runat="server" __designer:wfdid="w38"
                                                    CssClass="GridLebelL" Font-Size="12px" Font-Underline="False" Target="_blank"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc")) %>'
                                                    Width="400px"></asp:HyperLink>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle HorizontalAlign="left" />
                                            <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Right" />
                                        </asp:TemplateField>

                                        <asp:TemplateField FooterStyle-Font-Bold="true" FooterStyle-Font-Size="12px"
                                            FooterStyle-HorizontalAlign="Right" HeaderText="Closing Balance"
                                            ItemStyle-HorizontalAlign="Right">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvclobalbank02" runat="server" CssClass="GridLebel"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "closdram")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Right" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField FooterStyle-Font-Bold="true" FooterStyle-Font-Size="12px"
                                            FooterStyle-HorizontalAlign="Right" HeaderText="Closing Liabilities"
                                            ItemStyle-HorizontalAlign="Right">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvcloliabilities02" runat="server" CssClass="GridLebel"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "closcram")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Right" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField FooterStyle-Font-Bold="true" FooterStyle-Font-Size="12px"
                                            FooterStyle-HorizontalAlign="Right" HeaderText="Issue Amt"
                                            ItemStyle-HorizontalAlign="Right">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvissueamt" runat="server" CssClass="GridLebel"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "isuamt")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Right" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>

                                        <asp:TemplateField FooterStyle-Font-Bold="true" FooterStyle-Font-Size="12px"
                                            FooterStyle-HorizontalAlign="Right" HeaderText="Collection Amt"
                                            ItemStyle-HorizontalAlign="Right">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvcollamt" runat="server" CssClass="GridLebel"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "collamt")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Right" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>



                                        <asp:TemplateField FooterStyle-Font-Bold="true" FooterStyle-Font-Size="12px"
                                            FooterStyle-HorizontalAlign="Right" HeaderText="Net Balance"
                                            ItemStyle-HorizontalAlign="Right">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvnetbal" runat="server" CssClass="GridLebel"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "bankbal")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Right" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>

                                        <asp:TemplateField FooterStyle-Font-Bold="true" FooterStyle-Font-Size="12px"
                                            FooterStyle-HorizontalAlign="Right" HeaderText="Bank Liabilities"
                                            ItemStyle-HorizontalAlign="Right">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvbankliabilities" runat="server" CssClass="GridLebel"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "banklia")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="80px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Right" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                    </Columns>
                                    <FooterStyle CssClass="grvFooter" />
                                    <EditRowStyle />
                                    <AlternatingRowStyle />
                                    <PagerStyle CssClass="gvPagination" />
                                    <HeaderStyle CssClass="grvHeader" />
                                </asp:GridView>
                            </div>
                        </div>
                    </div>

                </asp:View>
                <asp:View ID="ViewBalConfirmation" runat="server">
                    <div class="card card-fluid">
                        <div class="card-body" style="min-height: 350px;">
                            <div class="row">
                                <div class="col-md-2 col-sm-2 col-lg-2 ">
                                    <div class="form-group">
                                        <asp:Label ID="Label13" runat="server" CssClass="label">From</asp:Label>
                                        <asp:TextBox ID="txtDatefrombankcb" runat="server" CssClass="form-control form-control-sm small"></asp:TextBox>
                                        <cc1:CalendarExtender ID="txtDatefrombankcb_CalendarExtender1" runat="server" Format="dd-MMM-yyyy" TargetControlID="txtDatefrombankcb"></cc1:CalendarExtender>
                                    </div>
                                </div>
                                <div class="col-md-2 col-sm-2 col-lg-2 ">
                                    <div class="form-group">
                                        <asp:Label ID="Label14" runat="server" CssClass="label">To</asp:Label>
                                        <asp:TextBox ID="txtDatetobankcb" runat="server" CssClass="form-control form-control-sm small"></asp:TextBox>
                                        <cc1:CalendarExtender ID="txtDatetobankcb_CalendarExtender2" runat="server" Format="dd-MMM-yyyy" TargetControlID="txtDatetobankcb"></cc1:CalendarExtender>
                                    </div>
                                </div>
                                <div class="col-md-2 col-sm-2 col-lg-2 ">
                                    <div class="form-group">
                                        <asp:Label ID="Label16" runat="server" CssClass="label">Report Level</asp:Label>

                                        <asp:DropDownList ID="ddlReportLevelBankcb" runat="server" CssClass="chzn-select form-control form-control-sm" TabIndex="6">
                                            <asp:ListItem Value="2">Level-1</asp:ListItem>
                                            <asp:ListItem Value="4">Level-2</asp:ListItem>
                                            <asp:ListItem Value="8">Level-3</asp:ListItem>
                                            <asp:ListItem Selected="True" Value="12">Details</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-1 col-sm-1 col-lg-1 ">
                                    <div class="form-group" style="margin-top: 20px;">
                                        <asp:LinkButton ID="lnkbtnCashBankBal" runat="server" CssClass="btn btn-primary btn-sm pull-left" OnClick="lnkbtnCashBankBal_Click">Ok</asp:LinkButton>
                                    </div>
                                </div>
                            </div>
                            <div class="row">

                                <asp:GridView ID="gvCABankBal" runat="server" AutoGenerateColumns="False"
                                    CssClass=" table-striped table-hover table-bordered grvContentarea gvTopHeader"
                                    ShowFooter="True" OnRowDataBound="gvCABankBal_RowDataBound">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Sl">
                                            <ItemTemplate>
                                                <asp:Label ID="lblserialnoid5" runat="server" Style="text-align: right"
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle Font-Bold="True" Font-Size="16px" />
                                            <ItemStyle Font-Size="12px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Code">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvcodebank4" runat="server" CssClass="GridLebel"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "actcode")) %>'
                                                    Width="90px"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField FooterStyle-Font-Bold="true" FooterStyle-Font-Size="12px"
                                            FooterStyle-HorizontalAlign="Right" HeaderText="Description of Accounts">
                                            <HeaderTemplate>
                                                <table style="width: 100%;">
                                                    <tr>
                                                        <td class="style58" style="width: auto">
                                                            <asp:Label ID="Label9" runat="server" Font-Bold="True"
                                                                Text="Description of Accounts"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:HyperLink ID="hlbtnbnkpdataExelcb" runat="server" BackColor="#000066"
                                                                BorderColor="White" BorderStyle="Solid" BorderWidth="1px" Font-Bold="True"
                                                                ForeColor="White">Export Exel</asp:HyperLink>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:HyperLink ID="HLgvDescbankcb" runat="server"
                                                    CssClass="GridLebelL" Font-Size="12px" Font-Underline="False" Target="_blank"
                                                    Text='<%# "<B>"+ Convert.ToString(DataBinder.Eval(Container.DataItem, "grpdesc")) + "</B>"+
                                                                         (DataBinder.Eval(Container.DataItem, "actdesc").ToString().Trim().Length>0 ? 
                                                                         (Convert.ToString(DataBinder.Eval(Container.DataItem, "grpdesc")).Trim().Length>0 ?  "<br>" : "")+                                                             
                                                                         "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"+ 
                                                                         Convert.ToString(DataBinder.Eval(Container.DataItem, "actdesc")).Trim(): "") 
                                                                         
                                                                    %>'
                                                    Width="250px">
                                                                      
                                                                      
                                                </asp:HyperLink>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle HorizontalAlign="left" />
                                            <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Right" />
                                        </asp:TemplateField>

                                        <asp:TemplateField FooterStyle-Font-Bold="true" FooterStyle-Font-Size="12px"
                                            FooterStyle-HorizontalAlign="Right" HeaderText="Change"
                                            ItemStyle-HorizontalAlign="Right">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvnetbalcb" runat="server" CssClass="GridLebel"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "netbal")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="90px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Right" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>


                                        <asp:TemplateField FooterStyle-Font-Bold="true" FooterStyle-Font-Size="12px"
                                            FooterStyle-HorizontalAlign="Right" HeaderText="Opening "
                                            ItemStyle-HorizontalAlign="Right">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvopnamcb" runat="server" CssClass="GridLebel"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "opnam")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="90px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Right" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>

                                        <asp:TemplateField FooterStyle-Font-Bold="true" FooterStyle-Font-Size="12px"
                                            FooterStyle-HorizontalAlign="Right" HeaderText="Closing"
                                            ItemStyle-HorizontalAlign="Right">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvclosamcb" runat="server" CssClass="GridLebel"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "closam")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="90px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Right" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>








                                    </Columns>
                                    <FooterStyle CssClass="grvFooter" />
                                    <EditRowStyle />
                                    <AlternatingRowStyle />
                                    <PagerStyle CssClass="gvPagination" />
                                    <HeaderStyle CssClass="grvHeader" />
                                </asp:GridView>
                            </div>
                        </div>
                    </div>

                </asp:View>

            </asp:MultiView>


            <asp:MultiView ID="MultiViewold" runat="server">
            </asp:MultiView>

        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>

