<%@ Page Title="" Language="C#" MasterPageFile="~/SPEMaster.Master" AutoEventWireup="true" CodeBehind="LetterInterface.aspx.cs" Inherits="SPEWEB.F_81_Hrm.F_92_Mgt.LetterInterface" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <style type="text/css">
        /*.tbMenuWrp table tr td:nth-child(1) {
                background: #3BA8E0;
            }

            .tbMenuWrp table tr td:nth-child(2) {
                background: #5EB75B;
            }

            .tbMenuWrp table tr td:nth-child(3) {
                background: #EFAD4D;
            }

            .tbMenuWrp table tr td:nth-child(4) {
                background: #D95350;
            }

            .tbMenuWrp table tr td:nth-child(5) {
                background: #76C9B5;
            }

            .tbMenuWrp table tr td:nth-child(6) {
                background: #769BF4;
            }

            .tbMenuWrp table tr td:nth-child(7) {
                background: #00CBF3;
            }

            .tbMenuWrp table tr td:nth-child(8) {
                background: #4BCF9E;
            }


            .tbMenuWrp table tr td:nth-child(9) {
                background: #3BA8E0;
            }

            .tbMenuWrp table tr td:nth-child(10) {
                background: #5EB75B;
            }

            .tbMenuWrp table tr td:nth-child(11) {
                background: #EFAD4D;
            }

            .tbMenuWrp table tr td:nth-child(12) {
                background: #D95350;
            }

            .tbMenuWrp table tr td:nth-child(13) {
                background: #76C9B5;
            }

            .tbMenuWrp table tr td:nth-child(14) {
                background: #769BF4;
            }

            .tbMenuWrp table tr td:nth-child(15) {
                background: #00CBF3;
            }

            .tbMenuWrp table tr td:nth-child(16) {
                background: #4BCF9E;
            }*/

        .InBox {
            color: red !important;
        }

        .OverAll {
            /*animation-name: example;
            animation-duration: 4s;
            animation-iteration-count: 5;*/
            /*font-size: 18px;*/
            color: black;
            font-size: 14px;
            text-align: center !important;
            margin-top: 0px;
        }
        /* Chrome, Safari, Opera */
        @-webkit-keyframes example {
            from {
                background-color: pink;
            }

            to {
                background-color: brown;
            }
        }

        /* Standard syntax */
        @keyframes example {
            from {
                background-color: pink;
            }

            to {
                background-color: brown;
            }
        }

        ul.sidebarMenu {
            margin: 0;
            padding: 0;
            width: 115%;
        }

            ul.sidebarMenu li {
                display: block;
                height: 30px;
                list-style: none;
                border: 1px solid #DFF0D8;
                border-bottom: 0;
            }

                ul.sidebarMenu li:last-child {
                    border-bottom: 1px solid #DFF0D8;
                }

                ul.sidebarMenu li a {
                    text-align: left;
                    display: block;
                    line-height: 30px;
                    font-size: 14px;
                    font-family: Calibri;
                }

                ul.sidebarMenu li h4 {
                    line-height: 50px;
                    text-align: center;
                    display: block;
                }

                ul.sidebarMenu li a:hover {
                    background: #D7E6D1;
                    color: black;
                }

        ul.tbMenuWrp {
            margin: 0;
            padding: 0;
            border: 0;
            background: none !important;
        }

            ul.tbMenuWrp li {
                height: 50px;
                width: 155px;
                padding: 0px 0;
                float: left;
                list-style: none;
                margin: 0 2px;
                color: #fff;
                background: #5F5F5F;
                -webkit-border-radius: 4px;
                -moz-border-radius: 4px;
                border-radius: 4px;
            }

                ul.tbMenuWrp li a {
                    padding: 0 0;
                    height: 50px;
                    background: #5F5F5F;
                    -webkit-border-radius: 4px;
                    -moz-border-radius: 4px;
                    border-radius: 4px;
                    display: block;
                    color: #fff;
                    padding: 0px 0 0 0;
                    vertical-align: middle;
                    border: none !important;
                }

                    ul.tbMenuWrp li a:hover {
                        background: #12A5A6;
                    }

                    ul.tbMenuWrp li a:focus {
                        outline: none;
                        outline-offset: 0;
                    }

                    ul.tbMenuWrp li a label {
                        color: #fff;
                        background: none;
                        border: none;
                        text-align: center;
                        font-weight: bold;
                        font-size: 16px;
                        display: block;
                        cursor: pointer;
                        width: 100%;
                    }

        .tbMenuWrp > li.active > a, .tbMenuWrp > li.active > a:focus, .tbMenuWrp > li.active > a:hover {
            background: #199698;
            color: #fff;
        }


        .tbMenuWrp table tr td {
            /*height: 50px;*/
            height: 36px;
            width: 108%;
            padding: 0 0;
            float: left;
            list-style: none;
            margin: 0.5px;
            color: #fff;
            text-align: center;
            border: 1px solid #9752A2;
            /*-webkit-border-radius: 30px;
            -moz-border-radius: 30px;
            border-radius: 30px;*/
            cursor: pointer;
        }

        /*.tbMenuWrp table tr td:nth-child(7) {
                width: 115px;
                padding: 0 3px;
            }

            .tbMenuWrp table tr td:nth-child(8) {
                width: 115px;
                padding: 0 3px;
            }

            .tbMenuWrp table tr td:nth-child(9) {
                width: 115px;
                padding: 0 3px;
            }*/


        table.grvContentarea tr td span.glyphicon {
            margin: 0 4px;
        }

        .tbMenuWrp table tr td label {
            cursor: pointer;
            /*width: 130px;*/
            /*background: whitesmoke;*/
            border-radius: 25px;
            color: #000;
            /*font-weight: bold;*/
            padding: 0 0 0 4px;
            line-height: 30px;
            margin: 1px 2px;
            display: block;
            text-align: left;
        }

            .tbMenuWrp table tr td label.active > a, .tbMenuWrp table tr td label.active > .tbMenuWrp table tr td label:focus, .tbMenuWrp table tr td label.active > a:hover {
                background: #12A5A6;
                color: #fff;
            }


        .tbMenuWrp table tr td input[type="checkbox"], input[type="radio"] {
            display: none;
        }

        .tabMenu a {
            display: block;
            line-height: 30px;
            font-size: 11px;
            color: #000;
            text-align: center;
            float: left;
        }

        .tbMenuWrp table tr td label span.lbldata {
            /*background: #F3B728;*/
            background-color: #337AB7;
            border: 1px solid #F3B728;
            border-radius: 50%;
            /*display: block;*/
            height: 30px;
            font-size: 11px;
            line-height: 18px;
            margin: 0 5px 0 0;
            padding: 4px 1px;
            width: 28px;
            float: left;
            text-align: center;
            color: #fff;
        }

        .tbMenuWrp table tr td label .lblactive {
            background: #12A5A6;
            color: #fff;
        }

        .grvContentarea tr td:last-child {
            width: 120px;
        }

        /* ====================== Gride Under Text ========================*/

        ul li {
            list-style: none;
        }

            ul li a {
                font-size: 11px;
                font-weight: normal;
                line-height: 10px;
                font-family: 'Times New Roman', Times, serif;
                color: #000;
                margin-left: 20px;
            }

        .chaktxt {
            font-size: 13px;
            font-family: 'Times New Roman', Times, serif;
            color: #000;
            font-weight: normal;
        }

        label {
            display: inline-block;
            font-weight: 400;
            margin-bottom: 5px;
            margin-left: 5px;
            max-width: 100%;
        }
    </style>


    <script type="text/javascript">
        $(document).ready(function () {
            //For navigating using left and right arrow of the keyboard
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);
        });
        function pageLoaded() {

            $("input, select").bind("keydown", function (event) {
                var k1 = new KeyPress();
                k1.textBoxHandler(event);
            });
        };

    </script>

    <%-- <asp:ObjectDataSource ID="source_session_online" runat="server" SelectMethod="session_online" TypeName="t_session" />--%>



    <%--<asp:Button ID="Button1" runat="server" Text="Refresh" OnClick="btn_refresh_Click" />--%>



    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>

            <%-- <asp:Timer ID="Timer1" runat="server" OnTick="Timer1_Tick" Interval="20000">
            </asp:Timer>

            <triggers>
 
                   <asp:AsyncPostBackTrigger ControlID="Timer1" EventName="Tick" />
 
               </triggers>--%>


            <div class="container moduleItemWrpper">
                <div class="contentPart">
                    <div class="row">

                        <div class="row">
                            <fieldset class="scheduler-border fieldset_A">
                                <div class="form-group">

                                    <div class="col-md-2">
                                        <asp:Label ID="Label1" runat="server" CssClass=" smLbl_to">Date: </asp:Label>
                                        <asp:TextBox ID="txtdate" runat="server" CssClass="inputTxt inputName inPixedWidth120 " AutoPostBack="true" OnTextChanged="txtdate_TextChanged"></asp:TextBox>
                                        <cc1:CalendarExtender ID="txtdate_CalendarExtender" runat="server" Enabled="True"
                                            Format="dd-MMM-yyyy" TargetControlID="txtdate"></cc1:CalendarExtender>
                                    </div>
                                    <div class="col-md-4 col-md-offset-2">
                                        <h5><strong>HR Interface</strong></h5>

                                    </div>
                                </div>
                                <div class="clearfix"></div>
                            </fieldset>
                        </div>



                        <div class="col-md-2">

                            <fieldset class="tabMenu">
                                <div class="form-horizontal">
                                    <div class="form-group">

                                        <div class="tbMenuWrp nav nav-tabs">
                                            <asp:RadioButtonList ID="RadioButtonList1" runat="server" AutoPostBack="true" OnSelectedIndexChanged="RadioButtonList1_SelectedIndexChanged" RepeatDirection="Horizontal">
                                                <asp:ListItem Value="14"></asp:ListItem>
                                                <asp:ListItem Value="15"></asp:ListItem>
                                               <%-- <asp:ListItem Value="10003"></asp:ListItem>--%>
                                                <asp:ListItem Value="10002"></asp:ListItem>
                                                <asp:ListItem Value="10005"></asp:ListItem>
                                                <asp:ListItem Value="9402"></asp:ListItem>
                                                <asp:ListItem Value="9403"></asp:ListItem>
                                                
                                              <%--  <asp:ListItem Value="10006"></asp:ListItem>
                                                <asp:ListItem Value="10007"></asp:ListItem>
                                                <asp:ListItem Value="10008"></asp:ListItem>
                                                <asp:ListItem Value="8"></asp:ListItem>
                                                <asp:ListItem Value="10009"></asp:ListItem>
                                                <asp:ListItem Value="10010"></asp:ListItem>
                                                <asp:ListItem Value="10011"></asp:ListItem>
                                                <asp:ListItem Value="10012"></asp:ListItem>
                                                <asp:ListItem Value="13"></asp:ListItem>
                                                <asp:ListItem Value="9"></asp:ListItem>--%>
                                            </asp:RadioButtonList>
                                        </div>
                                    </div>
                                </div>
                            </fieldset>


                        </div>
                        <div id="slSt" class=" col-md-10">
                            <div class="panel with-nav-tabs panel-primary">
                                <asp:Panel ID="pnlHistory" runat="server" Visible="true">
                                    <div class="row">
                                        <div class="table-responsive col-lg-12" style="min-height: 350px;">

                                            <asp:GridView ID="gvSalesUpdate" runat="server" AutoGenerateColumns="False" CssClass="table-striped table-hover table-bordered grvContentarea"
                                                ShowFooter="True" OnRowDataBound="gvSalesUpdate_RowDataBound">
                                                <RowStyle />
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Sl">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvSlNo0" runat="server" Font-Bold="True"
                                                                Style="text-align: right"
                                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Name">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvcentrid" runat="server"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "empname")) %>'
                                                                Width="160px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Designation">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvcentrdesc" runat="server" BackColor="Transparent"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "Desig")) %>'
                                                                Width="160px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Type" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="txtype" runat="server" BackColor="Transparent"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "TYPE")) %>' Width="2px"></asp:Label>
                                                            <asp:Label ID="txtID" runat="server" BackColor="Transparent" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "EMPID")) %>' Width="2px"></asp:Label>

                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Date">
                                                        <ItemTemplate>
                                                            <asp:Label ID="txtgvDate" runat="server" BackColor="Transparent"
                                                                Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "DATE")).ToString("dd-MMM-yyyy") %>'
                                                                Width="80px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>


                                                    <asp:TemplateField HeaderText="">
                                                        <ItemTemplate>
                                                            <asp:HyperLink ID="lnkbtnPrintIN" runat="server"><span class="glyphicon glyphicon-print"></span></asp:HyperLink>
                                                            <asp:HyperLink ID="lnkbtnApp" runat="server" Target="_blank" ForeColor="Black" Font-Underline="false"><span class="glyphicon glyphicon-ok"></span>
                                                            </asp:HyperLink>
                                                            <asp:LinkButton ID="btnDelOrder" runat="server"><span style="color:red" class="glyphicon glyphicon-floppy-remove"></span> </asp:LinkButton>
                                                        </ItemTemplate>
                                                        <ItemStyle Width="80px" />
                                                        <HeaderStyle HorizontalAlign="Center" Width="80px" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                </Columns>
                                                <FooterStyle CssClass="grvFooter" />
                                                <EditRowStyle />
                                                <AlternatingRowStyle />
                                                <PagerStyle CssClass="gvPagination" />
                                                <HeaderStyle CssClass="grvHeader" />
                                            </asp:GridView>



                                            <asp:GridView ID="gvselect" runat="server" AutoGenerateColumns="False" CssClass="table-striped table-hover table-bordered grvContentarea"
                                                ShowFooter="True" OnRowDataBound="gvselect_RowDataBound">
                                                <RowStyle />
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Sl">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvSlNo02" runat="server" Font-Bold="True"
                                                                Style="text-align: right"
                                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Catagory">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvce2" runat="server"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "Cat")) %>'
                                                                Width="100px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Name">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvcentrid2" runat="server"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "empname")) %>'
                                                                Width="160px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Designation">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvcentrdesc2" runat="server" BackColor="Transparent"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "Desig")) %>'
                                                                Width="160px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Type" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="txtype" runat="server" BackColor="Transparent"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "TYPE")) %>' Width="2px"></asp:Label>
                                                            <asp:Label ID="txtID2" runat="server" BackColor="Transparent" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "EMPID")) %>' Width="2px"></asp:Label>

                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Date">
                                                        <ItemTemplate>
                                                            <asp:Label ID="txtgvDate2" runat="server" BackColor="Transparent"
                                                                Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "DATE")).ToString("dd-MMM-yyyy") %>'
                                                                Width="80px"></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>


                                                    <asp:TemplateField HeaderText="">
                                                        <ItemTemplate>
                                                            <asp:HyperLink ID="lnkbtnPrintIN2" runat="server"><span class="glyphicon glyphicon-print"></span></asp:HyperLink>
                                                            <asp:HyperLink ID="lnkbtnApp2" runat="server" Target="_blank" ForeColor="Black" Font-Underline="false"><span class="glyphicon glyphicon-ok"></span>
                                                            </asp:HyperLink>
                                                            <asp:LinkButton ID="btnDelOrder2" runat="server"><span style="color:red" class="glyphicon glyphicon-floppy-remove"></span> </asp:LinkButton>
                                                        </ItemTemplate>
                                                        <ItemStyle Width="80px" />
                                                        <HeaderStyle HorizontalAlign="Center" Width="80px" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                </Columns>
                                                <FooterStyle CssClass="grvFooter" />
                                                <EditRowStyle />
                                                <AlternatingRowStyle />
                                                <PagerStyle CssClass="gvPagination" />
                                                <HeaderStyle CssClass="grvHeader" />
                                            </asp:GridView>




                                            <asp:GridView ID="gvappinfo" Visible="false" runat="server" OnRowDataBound="gvappinfo_RowDataBound" CssClass="table-striped table-hover table-bordered grvContentarea" AutoGenerateColumns="False"
                                                ShowFooter="True">
                                                <RowStyle />
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Sl.">
                                                        <ItemTemplate>
                                                            <asp:Label ID="serialno" runat="server" Style="text-align: right"
                                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label>
                                                        </ItemTemplate>

                                                    </asp:TemplateField>


                                                    <asp:TemplateField HeaderText="Adv. No">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvadvno" runat="server" Font-Size="11PX"
                                                                Style="text-align: left"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "advno1")) %>'
                                                                Width="60px"></asp:Label>
                                                        </ItemTemplate>

                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="AdvNo" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgva58" runat="server" Font-Size="11PX"
                                                                Style="text-align: left"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "advno")) %>'
                                                                Width="1px"></asp:Label>
                                                        </ItemTemplate>

                                                    </asp:TemplateField>



                                                    <asp:TemplateField HeaderText="Ref. No" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvrefno" runat="server" Font-Size="11PX"
                                                                Style="text-align: left"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "refno")) %>'
                                                                Width="60px"></asp:Label>
                                                        </ItemTemplate>

                                                    </asp:TemplateField>


                                                    <asp:TemplateField HeaderText="Date" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvdate" runat="server" Font-Size="11PX"
                                                                Style="text-align: left"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "advdat1")) %>'
                                                                Width="60px"></asp:Label>
                                                        </ItemTemplate>

                                                    </asp:TemplateField>


                                                    <asp:TemplateField HeaderText="Company Name">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvcompany" runat="server" Font-Size="11PX"
                                                                Style="text-align: left"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "company")) %>'
                                                                Width="120px"></asp:Label>
                                                        </ItemTemplate>

                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Department">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvdeptname" runat="server" Font-Size="11PX"
                                                                Style="text-align: left"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "deptdesc")) %>'
                                                                Width="120px"></asp:Label>
                                                        </ItemTemplate>

                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Post">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvpost" runat="server" Font-Size="11PX"
                                                                Style="text-align: left"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "postdesc")) %>'
                                                                Width="120px"></asp:Label>
                                                        </ItemTemplate>

                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Qualification">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvqualifica" runat="server" Font-Size="11PX"
                                                                Style="text-align: left"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "qfication")) %>'
                                                                Width="150px"></asp:Label>
                                                        </ItemTemplate>

                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Requirement" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvq" runat="server" Font-Size="11PX"
                                                                Style="text-align: left"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "requir")) %>'
                                                                Width="100px"></asp:Label>
                                                        </ItemTemplate>

                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Job Source" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvj" runat="server" Font-Size="11PX"
                                                                Style="text-align: left"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "jobsource")) %>'
                                                                Width="100px"></asp:Label>
                                                        </ItemTemplate>

                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="NO. of Position">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvjob" runat="server" Font-Size="11PX"
                                                                Style="text-align: right"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "requir")) %>'
                                                                Width="50px"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                 <%--   <asp:TemplateField HeaderText="">
                                                        <ItemTemplate>
                                                            <asp:HyperLink ID="lnkbtnp" runat="server" Style="float: left;"><span class="glyphicon glyphicon-print"></span></asp:HyperLink>

                                                            <asp:HyperLink ID="lnkok" runat="server" Target="_blank" ForeColor="Black" Font-Underline="false" Style="float: left;"><span class="glyphicon glyphicon-ok"></span></asp:HyperLink>

                                                            <asp:LinkButton ID="btnde" runat="server" Style="float: left;"><span style="color:red" class="glyphicon glyphicon-floppy-remove"></span> </asp:LinkButton>
                                                        </ItemTemplate>
                                                    
                                                        <ItemStyle Width="80px" />
                                                        <HeaderStyle HorizontalAlign="Center" Width="80px" VerticalAlign="Top" />
                                                    </asp:TemplateField>--%>

                                                     <asp:TemplateField HeaderText="">
                                                        <ItemTemplate>
                                                            <asp:HyperLink ID="lnkbtnp" runat="server"><span class="glyphicon glyphicon-print"></span></asp:HyperLink>
                                                            <asp:HyperLink ID="lnkok" runat="server" Target="_blank" ForeColor="Black" Font-Underline="false"><span class="glyphicon glyphicon-ok"></span>
                                                            </asp:HyperLink>
                                                            <asp:LinkButton ID="btnde" runat="server"><span style="color:red" class="glyphicon glyphicon-floppy-remove"></span> </asp:LinkButton>
                                                        </ItemTemplate>
                                                        <ItemStyle Width="80px" />
                                                        <HeaderStyle HorizontalAlign="Center" Width="80px" VerticalAlign="Top" />
                                                    </asp:TemplateField>

                                                </Columns>
                                                <PagerStyle CssClass="gvPagination" />
                                                <HeaderStyle CssClass="grvHeader" />
                                                <FooterStyle CssClass="grvFooter" />
                                                <EditRowStyle />
                                                <AlternatingRowStyle />
                                            </asp:GridView>


                                            <asp:GridView ID="gvfselect" Visible="false" runat="server" OnRowDataBound="gvfselect_RowDataBound" CssClass="table-striped table-hover table-bordered grvContentarea" AutoGenerateColumns="False"
                                                ShowFooter="True" Width="678px">
                                                <RowStyle />
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Sl.">
                                                        <ItemTemplate>
                                                            <asp:Label ID="serialno" runat="server" Style="text-align: right"
                                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label>
                                                        </ItemTemplate>

                                                    </asp:TemplateField>


                                                    <asp:TemplateField HeaderText="Adv. No">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvadvno" runat="server" Font-Size="11PX"
                                                                Style="text-align: left"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "advno1")) %>'
                                                                Width="60px"></asp:Label>
                                                        </ItemTemplate>

                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="AdvNo" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgva58" runat="server" Font-Size="11PX"
                                                                Style="text-align: left"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "advno")) %>'
                                                                Width="1px"></asp:Label>
                                                        </ItemTemplate>

                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Department" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvdeptname" runat="server" Font-Size="11PX"
                                                                Style="text-align: left"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "deptdesc")) %>'
                                                                Width="120px"></asp:Label>
                                                        </ItemTemplate>

                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Post">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblgvpost" runat="server" Font-Size="11PX"
                                                                Style="text-align: left"
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "postdesc")) %>'
                                                                Width="120px"></asp:Label>
                                                        </ItemTemplate>

                                                    </asp:TemplateField>


                                                    <asp:TemplateField HeaderText="">
                                                        <ItemTemplate>
                                                            <asp:HyperLink ID="lnkbtnp" runat="server"><span class="glyphicon glyphicon-print"></span></asp:HyperLink>
                                                            <asp:HyperLink ID="lnkok" runat="server" Target="_blank" ForeColor="Black" Font-Underline="false"><span class="glyphicon glyphicon-ok"></span></asp:HyperLink>
                                                            <asp:LinkButton ID="btnde" runat="server"><span style="color:red" class="glyphicon glyphicon-floppy-remove"></span> </asp:LinkButton>
                                                        </ItemTemplate>
                                                        <ItemStyle Width="100px" />
                                                        <HeaderStyle HorizontalAlign="Center" Width="100px" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                </Columns>
                                                <PagerStyle CssClass="gvPagination" />
                                                <HeaderStyle CssClass="grvHeader" />
                                                <FooterStyle CssClass="grvFooter" />
                                                <EditRowStyle />
                                                <AlternatingRowStyle />
                                            </asp:GridView>









                                        </div>
                                    </div>
                                </asp:Panel>
                            </div>
                            <div class="row">


                                <div class="col-sm-4 col-md-4 col-lg-4">
                                    <asp:CheckBox ID="chkreq" CssClass="chaktxt" AutoPostBack="true" OnCheckedChanged="chkreq_CheckedChanged" runat="server" Text="A. Recruitment" />
                                    <asp:Panel ID="Panel1" runat="server" Visible="false">
                                        <ul>
                                            <li><a href="../F_81_Rec/JobAdvertisement.aspx?Type=Entry&genno=" target="_blank">01. Manpower Requisition</a></li>
                                            <li><a href="../F_81_Rec/ShortListing.aspx?Type=SList" target="_blank">02. Sort Listing</a></li>
                                            <li><a href="../F_81_Rec/ShortListing.aspx?Type=IResult" target="_blank">03. Interview Result Input</a></li>
                                            <li><a href="../F_81_Rec/ShortListing.aspx?Type=Fselection" target="_blank">04. Final Selection Input</a></li>
                                            <li><a href="../../LetterDefault.aspx?Type=10003&Entry=Offer Letter For General" target="_blank">05. Offer Letter Create</a></li>
                                            <li><a href="../F_81_Rec/LetterOfAppoinment.aspx?Type=LCreate" target="_blank">06. Appiontment Letter Create</a></li>
                                        </ul>
                                    </asp:Panel>
                                </div>


                                <div class="col-sm-4 col-md-4 col-lg-4">
                                    <asp:CheckBox ID="chkapp" CssClass="chaktxt" runat="server" AutoPostBack="true" OnCheckedChanged="chkapp_CheckedChanged" Text="B. Appointment" />
                                    <asp:Panel ID="Panel2" runat="server" Visible="false">
                                        <ul>
                                            <li><a href="../F_82_App/EmpEntry01.aspx?Type=Entry&empid=">01. Personal Information</a></li>
                                            <li><a href="../F_81_Hrm/F_82_App/HREmpEntry.aspx?Type=Aggrement">02. Employee Agreement</a></li>
                                            <li><a href="../F_82_App/ImgUpload.aspx?Type=Entry&empid=">03. Employee Image Upload</a></li>
                                        </ul>
                                    </asp:Panel>
                                </div>

                                <div class="col-sm-4 col-md-4 col-lg-4">
                                    <asp:CheckBox ID="chkatt" CssClass="chaktxt" runat="server" AutoPostBack="true" OnCheckedChanged="chkatt_CheckedChanged" Text="C. Attendance System" />
                                    <asp:Panel ID="Panel3" runat="server" Visible="false">
                                        <ul>
                                            <li><a href="../F_83_Att/HREmpOffDays.aspx">01. Employee Off Days</a></li>
                                            <li><a href="../F_81_Hrm/F_83_Att/HREmpAbsCt.aspx">02. Absent Count</a></li>
                                            <li><a href="../F_83_Att/HRDailyAttenManually.aspx">03. Daily Attendance - Manually</a></li>
                                            <li><a href="../F_81_Hrm/F_83_Att/HRDailyAttenUpload.aspx">04. Daily Attendance - Upload</a></li>
                                            <li><a href="../F_83_Att/HREmpMonthlyAtten.aspx">05. Monthly Attendance - Manually</a></li>
                                        </ul>
                                    </asp:Panel>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>

            </div>
            
        </ContentTemplate>

    </asp:UpdatePanel>
    <asp:Label ID="lblprintstkl" runat="server"></asp:Label>

</asp:Content>


