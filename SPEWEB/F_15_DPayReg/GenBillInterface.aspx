<%@ Page Title="" Language="C#" MasterPageFile="~/SPE.Master" AutoEventWireup="true" CodeBehind="GenBillInterface.aspx.cs" Inherits="SPEWEB.F_15_DPayReg.GenBillInterface" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <style type="text/css">
        .modal-dialog {
            margin: 44px auto;
            width: 100%;
        }

        .InBox {
            color: red !important;
        }

        .modal-title {
            font-weight: bold;
            color: #000;
        }

        .modal-title span {
            color: red;
        }

        .wrntLbl {
            float: right;
            width: 60%;
            background: #DFF0D8;
            border: 1px solid #DFF0D8;
        }

        .contentPart .ServProdInfo .form-group {
            overflow: hidden;
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
            height: 65px;
            width: 120px;
            padding: 0 0;
            float: left;
            list-style: none;
            margin: 0 1px;
            color: #fff;
            text-align: center;
            border: 2px solid #D1D735;
            /*-webkit-border-radius: 30px;
            -moz-border-radius: 30px;
            border-radius: 30px;*/
            cursor: pointer;
            background: #fff;
            position: relative;
        }

            .tbMenuWrp table tr td:nth-child(1) {
                background: #3BA8E0;
            }



            .tbMenuWrp table tr td:nth-child(2) {
                background: #5EB75B;
                display: none;
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
                background: #4BCF9E;
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
            color: #000;
            cursor: pointer;
            font-weight: bold;
            height: 100%;
            margin: 1px 0;
            padding: 2px;
            width: 100%;
        }

            .tbMenuWrp table tr td label.active > a, .tbMenuWrp table tr td label.active > .tbMenuWrp table tr td label:focus, .tbMenuWrp table tr td label.active > a:hover {
                /*background: #12A5A6;*/
                /*color: #fff;*/
            }


        .tbMenuWrp table tr td input[type="checkbox"], input[type="radio"] {
            display: none;
        }

        .tabMenu a {
            display: block;
            line-height: 15px;
            font-size: 14px;
            color: #000;
            text-align: center;
            background-color: #00ff21;
        }

        .tbMenuWrp table tr td label span.lbldata {
            border: 2px solid #fff;
            border-radius: 50%;
            color: #fff;
            display: inline-block;
            float: left;
            font-size: 17px;
            font-weight: bold;
            padding: 2px;
            position: absolute;
            right: 4px;
            top: 7px;
        }

        .rptPurInt span.lbldata2 {
            background: #e5dcdd none repeat scroll 0 0;
            border: 1px solid #3ba8e0;
            display: block;
            font-size: 12px;
            line-height: 22px;
            margin: 14px 0 0;
            padding: 0;
            text-align: center;
        }

        .tbMenuWrp table tr td label .lblactive {
            /*background: #12A5A6;
            color: #fff;*/
        }

        .grvContentarea tr td:last-child {
            /*width: 120px;*/
        }



        .fan {
            border: 2px solid #f3b728;
            border-radius: 50%;
            display: inline-block;
            float: left;
            font-size: 18px;
            margin-top: 4px;
            padding: 2px;
        }

            .fan:nth-child(1) {
                background: #FF9C40 !important;
                color: #fff;
                border: 2px solid #E4DDDD;
            }

            .fan:nth-child(2) {
                color: #E49015;
                background-color: #5EB75A !important;
            }

            .fan:nth-child(3) {
                color: #fff;
                background: #085407 !important;
            }

            .fan:nth-child(4) {
                color: #fff;
                background: #DA3F40 !important;
                border: 2px solid #E4DDDD;
            }

            .fan:nth-child(5) {
                color: #fff;
                background: #009BFF !important;
                border: 2px solid #E4DDDD;
            }

            .fan:nth-child(6) {
                color: #E4DDDD;
                background: #539250 !important;
                border: 2px solid #E4DDDD;
            }

            .fan:nth-child(1) {
                color: #E4DDDD;
                background: #E79956 !important;
                border: 2px solid #E4DDDD;
            }

            .fan:nth-child(1) {
                color: #fff;
                background: #459A42 !important;
                border: 2px solid #E4DDDD;
            }





        .modalPopup {
            top: 25px !important;
            min-height: 400px;
            overflow: scroll;
        }
    </style>





    <script type="text/javascript">
        $(document).ready(function () {
            //For navigating using left and right arrow of the keyboard
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);

            function loadModal() {
                $('#exampleModal3').modal('show');
            }

        });

        function Search_Gridview(strKey, cellNr, gvName) {
            //alert(cellNr);
            var tblData;


            var strData = strKey.value.toLowerCase().split(" ");
            switch (gvName) {
                case "gvReqInfo":
                    tblData = document.getElementById("<%=gvReqInfo.ClientID %>");
                    break;
                case "gvPenApproval":
                    tblData = document.getElementById("<%=gvPenApproval.ClientID %>");
                    break;
                case "gvFinlApproval":
                    tblData = document.getElementById("<%=gvFinlApproval.ClientID %>");
                    break;
                case "gvPayOrder":
                    tblData = document.getElementById("<%=gvPayOrder.ClientID %>");
                    break;
                case "gvReqInfo1":
                    tblData = document.getElementById("<%=gvReqInfo1.ClientID %>");
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



        };

    </script>


    <%--<asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>--%>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>

           <%-- <asp:Timer ID="Timer1" runat="server" OnTick="Timer1_Tick" Interval="5000">
            </asp:Timer>--%>

            <%--<triggers>
 
                   <asp:AsyncPostBackTrigger ControlID="Timer1" EventName="Tick" />
 
               </triggers>
            --%>




            <div class="card card-fluid">
                <div class="card-body">


                    <div class="row">
                        <div class="col-md-2">
                            <div class="form-group">
                                <label class="control-label" for="FromDate">Date</label>
                                <asp:TextBox ID="txtdate" runat="server" CssClass="form-control" AutoPostBack="true"></asp:TextBox>
                                <cc1:CalendarExtender ID="CalendarExtender_txtfrmdate" runat="server" Enabled="True"
                                    Format="dd-MMM-yyyy" TargetControlID="txtdate"></cc1:CalendarExtender>

                            </div>
                        </div>
                        <div class="col-md-1">
                            <div class="form-group">
                                <asp:LinkButton ID="lnkbtnok" runat="server" CssClass="margin-top30px btn btn-primary" OnClick="lbtnOk_Click">Ok</asp:LinkButton>
                            </div>
                        </div>

                        <div class="col-md-3">
                            <asp:LinkButton ID="LbtnSetting" runat="server" CssClass="margin-top30px btn btn-success" OnClick="LbtnSetting_Click">Setting</asp:LinkButton>
                            <asp:LinkButton ID="LbtnInt" runat="server" CssClass="margin-top30px btn btn-secondary " OnClick="LbtnInt_Click">Interface</asp:LinkButton>
                            <asp:LinkButton ID="LbtnRep" runat="server" CssClass="margin-top30px btn btn-warning" OnClick="LbtnRep_Click">ALL Reports</asp:LinkButton>

                        </div>
                        <div class="col-md-1">
                            <div class="margin-top30px btn-group" role="group" aria-label="Button group with nested dropdown">
                                <button type="button" class="btn btn-danger">Operations</button>
                                <div class="btn-group" role="group">
                                    <button id="btnGroupDrop4" type="button" class="btn btn-danger dropdown-toggle" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false"></button>
                                    <div class="dropdown-menu" aria-labelledby="btnGroupDrop4" style="">
                                        <div class="dropdown-arrow"></div>
                                        <asp:HyperLink ID="HyperLink1" runat="server" Target="_blank" CssClass="dropdown-item">Create Requsition</asp:HyperLink>


                                    </div>
                                </div>
                            </div>
                        </div>

                         

                    </div>

                </div>
            </div>

            <div class="card card-fluid">
                <div class="card-body">

                    <div class="row">
                        <asp:Panel ID="PnlInt" runat="server" Visible="false">
                            <asp:Panel ID="pnlInterf" runat="server">

                                <div class="panel with-nav-tabs panel-primary">
                                    <fieldset class="tabMenu">
                                        <div class="form-horizontal">
                                            <div class="form-group">
                                                <div class="tbMenuWrp nav nav-tabs rptPurInt">
                                                    <asp:RadioButtonList ID="RadioButtonList1" runat="server" AutoPostBack="true" OnSelectedIndexChanged="RadioButtonList1_SelectedIndexChanged" RepeatDirection="Horizontal">
                                                        <asp:ListItem Value="0"></asp:ListItem>
                                                        <asp:ListItem Value="1" style="display: none;"></asp:ListItem>
                                                        <asp:ListItem Value="2"></asp:ListItem>
                                                        <%-- <asp:ListItem Value="3"></asp:ListItem>--%>
                                                        <asp:ListItem Value="4"></asp:ListItem>
                                                        <asp:ListItem Value="5"></asp:ListItem>

                                                    </asp:RadioButtonList>
                                                </div>
                                            </div>
                                        </div>
                                    </fieldset>
                                    <div>
                                    </div>
                                </div>



                            </asp:Panel>

                            <asp:Panel ID="pnlReqInfo" runat="server" Visible="false">

                                <asp:GridView ID="gvReqInfo" runat="server" AutoGenerateColumns="False" CssClass="table-striped table-hover table-bordered grvContentarea"
                                    ShowFooter="True" OnRowDataBound="gvReqInfo_RowDataBound">
                                    <RowStyle />
                                    <Columns>


                                        <asp:TemplateField HeaderText="Sl">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvDSlNo" runat="server" Style="text-align: right"
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="reqno#" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvreqno" runat="server" Style="text-align: right"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "reqno"))%>' Width="15px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Project Code" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblpcode" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactcode")) %>'
                                                    Width="160px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="Project Name" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvpatcdesc" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc")) %>'
                                                    Width="170px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Requistion <br>Date">
                                            <ItemTemplate>
                                                <asp:Label ID="lnkgvreqrat1" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "reqdat1")) %>'
                                                    Width="90px"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Requistion <br> No">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvreqno1" runat="server" Style="text-align: left"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "reqno1"))%>' Width="80px"></asp:Label>

                                            </ItemTemplate>

                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Bill Date">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvBillDate" runat="server" Style="text-align: left"
                                                    Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "billdate")).ToString("dd-MMM-yyyy") %>' Width="80px"></asp:Label>

                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" />
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Bill No">
                                            <HeaderTemplate>
                                                <asp:TextBox ID="txtSrcBillNo" BackColor="Transparent" Width="105px" BorderStyle="None" runat="server" placeholder="Bill No" onkeyup="Search_Gridview(this,4, 'gvReqInfo')"></asp:TextBox><br />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvBillNo" runat="server" Style="text-align: left"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "billno"))%>' Width="105px"></asp:Label>

                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" />
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="MRF No.">
                                            <ItemTemplate>

                                                <asp:Label ID="hypMrfno" runat="server" BorderStyle="none"
                                                    Font-Size="11px" Style="background-color: Transparent;" Font-Underline="false"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mrfno"))%>' Width="60px">
                                      
                                                </asp:Label>


                                            </ItemTemplate>
                                            <ItemStyle Width="60px" />
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>

                                         <asp:TemplateField HeaderText="Supplier/Pay to">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvReqInfoesupdesc" runat="server" Font-Size="12px" Style="font-size: 12px; color: black;" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "supdesc")) %>'
                                                    Width="150px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle Font-Size="12px" HorizontalAlign="Left" />
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="pactcode" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvpactcode" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactcode")) %>'
                                                    Width="120px"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Resource</br> Count">
                                            <ItemTemplate>
                                                <asp:Label ID="lblrescount" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rescount"))%>' Width="40px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="Requistion <br>Amount">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvreqamt" runat="server" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "reqamt")).ToString("#,##0;(#,##0);") %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>

                                            <FooterStyle HorizontalAlign="Right" Width="60px" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Approved Amount">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvApamt" runat="server" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "appamt")).ToString("#,##0;(#,##0);") %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>

                                            <FooterStyle HorizontalAlign="Right" Width="60px" />
                                        </asp:TemplateField>



                                        <asp:TemplateField HeaderText="Balance Amount" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvPaidamt" runat="server" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "balamt")).ToString("#,##0;(#,##0);") %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>

                                            <FooterStyle HorizontalAlign="Right" Width="60px" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Current Status">
                                            <ItemTemplate>
                                                <asp:Label ID="lblcurrentSt" runat="server" Font-Size="12px" Style="font-size: 12px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "cstatus")) %>'
                                                    Width="120px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle Font-Size="12px" HorizontalAlign="Center" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="CS Attached file" Visible="false">
                                            <ItemTemplate>

                                                <asp:HyperLink ID="hlkQutation" runat="server" Text="Attachded Qutation" Target="_blank"></asp:HyperLink>

                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle Font-Size="12px" HorizontalAlign="Left" />
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="">
                                            <ItemTemplate>

                                                <asp:HyperLink ID="HyInprPrint11" CssClass="btn btn-xs btn-default" runat="server" Target="_blank" ForeColor="Blue" Font-Underline="false"><span class="fa fa-print"></span>
                                                </asp:HyperLink>



                                                <asp:LinkButton ID="btnDelOrder11" CssClass="btn btn-xs btn-default" runat="server"><span style="color:red" class="fa fa-recycle"></span> </asp:LinkButton>
                                            </ItemTemplate>
                                            <ItemStyle Width="100px" />
                                            <HeaderStyle HorizontalAlign="Center" Width="100px" VerticalAlign="Top" />
                                        </asp:TemplateField>


                                    </Columns>
                                    <PagerStyle CssClass="gvPagination" />
                                    <HeaderStyle CssClass="grvHeader" />
                                    <FooterStyle CssClass="grvFooter" />
                                    <RowStyle CssClass="grvRows" />
                                </asp:GridView>
                            </asp:Panel>

                            <asp:Panel ID="pnlPendapp" runat="server" Visible="false">

                                <asp:GridView ID="gvPenApproval" runat="server" AutoGenerateColumns="False" CssClass="table-striped table-hover table-bordered grvContentarea"
                                    ShowFooter="True" OnRowDataBound="gvPenApproval_RowDataBound">
                                    <RowStyle />
                                    <Columns>


                                        <asp:TemplateField HeaderText="Sl">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvDSlNopapr" runat="server" Style="text-align: right"
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="reqno#" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvreqnopapr" runat="server" Style="text-align: right"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "reqno"))%>' Width="15px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Project Name" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvpatcdescpapr" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc")) %>'
                                                    Width="170px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Requistion <br>Date">
                                            <ItemTemplate>
                                                <asp:Label ID="lnkgvreqrat1papr" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "reqdat1")) %>'
                                                    Width="90px"></asp:Label>


                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Requistion <br> No">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvreqno1papr" runat="server" Style="text-align: left"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "reqno1"))%>' Width="80px"></asp:Label>

                                            </ItemTemplate>

                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="Bill Date">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvBillDate2" runat="server" Style="text-align: left"
                                                    Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "billdate")).ToString("dd-MMM-yyyy") %>' Width="80px"></asp:Label>

                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" />
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Bill No">
                                            <HeaderTemplate>
                                                <asp:TextBox ID="txtSrcBillNo" BackColor="Transparent" Width="100px" CssClass="text-center" BorderStyle="None" runat="server" placeholder="Bill No" onkeyup="Search_Gridview(this,4, 'gvPenApproval')"></asp:TextBox><br />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvBillNo2" runat="server" Style="text-align: left"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "billno"))%>' Width="90px"></asp:Label>

                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" />
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="MRF No.">
                                            <ItemTemplate>

                                                <asp:HyperLink ID="hlnkgvgvmrfnopapr" runat="server" BorderStyle="none"
                                                    Font-Size="11px" Style="background-color: Transparent; color: blue;" Font-Underline="false"
                                                    Target="_blank" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mrfno"))%>' Width="60px">
                                      
                                                </asp:HyperLink>


                                            </ItemTemplate>
                                            <ItemStyle Width="60px" />
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Supplier/Pay to">
                                            <HeaderTemplate>
                                                <asp:TextBox ID="txtSrcSupp" BackColor="Transparent" Width="150px" CssClass="text-center" BorderStyle="None" runat="server" placeholder="Supplier/Pay to" onkeyup="Search_Gridview(this,6, 'gvPenApproval')"></asp:TextBox><br />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblesupdesc" runat="server" Font-Size="12px" Style="font-size: 12px; color: black;" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "supdesc")) %>'
                                                    Width="150px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label runat="server" ID="gvpALblTtl" Font-Bold="True" Style="text-align: right" Text="Total"></asp:Label>
                                            </FooterTemplate>
                                            <FooterStyle HorizontalAlign="Right" />
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle Font-Size="12px" HorizontalAlign="Left" />
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="pactcode" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvpactcodepapr" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactcode")) %>'
                                                    Width="120px"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="Resource</br> Count">
                                            <ItemTemplate>
                                                <asp:Label ID="lblrescountap" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rescount"))%>' Width="40px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Requistion <br>Amount">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvreqamtpapr" runat="server" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "reqamt")).ToString("#,##0;(#,##0);") %>'
                                                    Width="65px"></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:Label runat="server" ID="gvLblTtlReq" Font-Bold="True" Style="text-align:right" ></asp:Label>
                                            </FooterTemplate>
                                            <FooterStyle HorizontalAlign="Right" Width="65px" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Approved Amount">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvApamtpapr" runat="server" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "appamt")).ToString("#,##0;(#,##0);") %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>

                                            <FooterStyle HorizontalAlign="Right" Width="60px" />
                                        </asp:TemplateField>



                                        <asp:TemplateField HeaderText="Balance Amount" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvPaidamtpapr" runat="server" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "balamt")).ToString("#,##0;(#,##0);") %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>

                                            <FooterStyle HorizontalAlign="Right" Width="60px" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Current Status">
                                            <ItemTemplate>
                                                <asp:Label ID="lblcurrentStpapr" runat="server" Font-Size="12px" Style="font-size: 12px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "cstatus")) %>'
                                                    Width="160px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle Font-Size="12px" HorizontalAlign="Center" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="CS Attached file">
                                            <ItemTemplate>

                                                <asp:HyperLink ID="hlkQutation" runat="server" Text="Attachded Qutation" Style="width: 110px;" Target="_blank"></asp:HyperLink>

                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle Font-Size="12px" HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        
                                       
                                        <asp:TemplateField HeaderText="Requisition Entry">
                                            <ItemTemplate>
                                                <asp:Label ID="lbleuser" runat="server" Font-Size="12px" Style="font-size: 12px; color: black;" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rusername")) %>'
                                                    Width="100px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle Font-Size="12px" HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="">
                                            <ItemTemplate>

                                                <asp:HyperLink ID="HyInprPrintfapproved" CssClass="btn btn-xs btn-default" runat="server" Target="_blank" ForeColor="Blue" Font-Underline="false"><span class="fa fa-print"></span>
                                                </asp:HyperLink>


                                                <asp:HyperLink ID="lnkbtnEntry" CssClass="btn btn-xs btn-default" runat="server" Target="_blank" ForeColor="Black" Font-Underline="false"><span class="fa fa-check"></span>
                                                </asp:HyperLink>


                                                <asp:HyperLink ID="lnkbtnEditIN" CssClass="btn btn-xs btn-default" runat="server" Target="_blank" ForeColor="Black" Font-Underline="false">
                                                <span class="fa fa-edit"></span>
                                                </asp:HyperLink>

                                                <asp:LinkButton ID="btnDelOrderfapproved" CssClass="btn btn-xs btn-default" runat="server" OnClick="btnDelOrderfapproved_Click"><span style="color:red" class="fa fa-recycle"></span> </asp:LinkButton>



                                            </ItemTemplate>
                                            <ItemStyle Width="150px" />
                                            <HeaderStyle HorizontalAlign="Center" Width="150px" VerticalAlign="Top" />
                                        </asp:TemplateField>



                                    </Columns>
                                    <PagerStyle CssClass="gvPagination" />
                                    <HeaderStyle CssClass="grvHeader" />
                                    <FooterStyle CssClass="grvFooter" />
                                    <RowStyle CssClass="grvRows" />
                                </asp:GridView>

                            </asp:Panel>

                            <asp:Panel ID="pnlFinalApp" runat="server" Visible="false">

                                <asp:GridView ID="gvFinlApproval" runat="server" AutoGenerateColumns="False" CssClass="table-striped table-hover table-bordered grvContentarea"
                                    ShowFooter="True" OnRowDataBound="gvFinlApproval_RowDataBound">
                                    <RowStyle />
                                    <Columns>


                                        <asp:TemplateField HeaderText="Sl">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvDSlNoFnApp" runat="server" Style="text-align: right"
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="reqno#" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvreqnoFnApp" runat="server" Style="text-align: right"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "reqno"))%>' Width="15px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Project Name" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvpatcdescFnApp" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc")) %>'
                                                    Width="170px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Requistion <br>Date">
                                            <ItemTemplate>
                                                <asp:Label ID="lnkgvreqrat1FnApp" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "reqdat1")) %>'
                                                    Width="90px"></asp:Label>


                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Requistion <br> No">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvreqno1FnApp" runat="server" Style="text-align: left"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "reqno1"))%>' Width="80px"></asp:Label>

                                            </ItemTemplate>

                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="MRF No.">
                                            <ItemTemplate>

                                                <asp:HyperLink ID="hlnkgvgvmrfnoFnApp" runat="server" BorderStyle="none"
                                                    Font-Size="11px" Style="background-color: Transparent; color: blue;" Font-Underline="false"
                                                    Target="_blank" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mrfno"))%>' Width="60px">
                                      
                                                </asp:HyperLink>


                                            </ItemTemplate>
                                            <ItemStyle Width="60px" />
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>




                                        <asp:TemplateField HeaderText="pactcode" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvpactcodeFnApp" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactcode")) %>'
                                                    Width="120px"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="Resource</br> Count">
                                            <ItemTemplate>
                                                <asp:Label ID="lblrescountap" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rescount"))%>' Width="40px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Requistion <br>Amount">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvreqamtFnApp" runat="server" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "reqamt")).ToString("#,##0;(#,##0);") %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>

                                            <FooterStyle HorizontalAlign="Right" Width="60px" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Approved Amount">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvApamtFnApp" runat="server" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "appamt")).ToString("#,##0;(#,##0);") %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>

                                            <FooterStyle HorizontalAlign="Right" Width="60px" />
                                        </asp:TemplateField>



                                        <asp:TemplateField HeaderText="Balance Amount" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvPaidamtFnApp" runat="server" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "balamt")).ToString("#,##0;(#,##0);") %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>

                                            <FooterStyle HorizontalAlign="Right" Width="60px" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Current Status">
                                            <ItemTemplate>
                                                <asp:Label ID="lblcurrentStFnApp" runat="server" Font-Size="12px" Style="font-size: 12px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "cstatus")) %>'
                                                    Width="120px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle Font-Size="12px" HorizontalAlign="Center" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="CS Attached file">
                                            <ItemTemplate>

                                                <asp:HyperLink ID="hlkQutationfapp" runat="server" Text="Attachded Qutation" Target="_blank"></asp:HyperLink>

                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle Font-Size="12px" HorizontalAlign="Left" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="">
                                            <ItemTemplate>

                                                <asp:HyperLink ID="HyInprPrint" CssClass="btn btn-xs btn-default" runat="server" Visible="false" Target="_blank" ForeColor="Blue" Font-Underline="false"><span class="fa fa-print"></span>
                                                </asp:HyperLink>

                                                <asp:HyperLink ID="lnkbtnEntry" CssClass="btn btn-xs btn-default" runat="server" Target="_blank" ForeColor="Black" Font-Underline="false"><span class="fa fa-check"></span>
                                                </asp:HyperLink>

                                                <asp:HyperLink ID="lnkbtnEditIN" CssClass="btn btn-xs btn-default" runat="server" Target="_blank" ForeColor="Black" Font-Underline="false">
                                               <span class="fa fa-edit"></span>
                                                </asp:HyperLink>


                                                <asp:LinkButton ID="btnDelOrder" CssClass="btn btn-xs btn-default" runat="server" OnClick="btnDelOrder_Click"><span style="color:red" class="fa fa-recycle"></span> </asp:LinkButton>
                                            </ItemTemplate>
                                            <ItemStyle Width="150px" />
                                            <HeaderStyle HorizontalAlign="Center" Width="150px" VerticalAlign="Top" />
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="Requisition Entry">
                                            <ItemTemplate>
                                                <asp:Label ID="lbleuserfapp" runat="server" Font-Size="12px" Style="font-size: 12px; color: black;" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rusername")) %>'
                                                    Width="100px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle Font-Size="12px" HorizontalAlign="Left" />
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="First Approval">
                                            <ItemTemplate>
                                                <asp:Label ID="lblapeuserfapp" runat="server" Font-Size="12px" Style="font-size: 12px; color: black;" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "aprvname")) %>'
                                                    Width="100px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle Font-Size="12px" HorizontalAlign="Left" />
                                        </asp:TemplateField>

                                    </Columns>
                                    <PagerStyle CssClass="gvPagination" />
                                    <HeaderStyle CssClass="grvHeader" />
                                    <FooterStyle CssClass="grvFooter" />
                                    <RowStyle CssClass="grvRows" />
                                </asp:GridView>

                            </asp:Panel>

                            <asp:Panel ID="pnlpayOrder" runat="server" Visible="false">

                                <asp:GridView ID="gvPayOrder" runat="server" AutoGenerateColumns="False" CssClass="table-striped table-hover table-bordered grvContentarea"
                                    ShowFooter="True" OnRowDataBound="gvPayOrder_RowDataBound">
                                    <RowStyle />
                                    <Columns>


                                        <asp:TemplateField HeaderText="Sl">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvDSlNoPor" runat="server" Style="text-align: right"
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="reqno#" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvreqnopPor" runat="server" Style="text-align: right"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "reqno"))%>' Width="15px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Project Name" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvpatcdescpPor" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc")) %>'
                                                    Width="170px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Requistion <br> Date">
                                            <ItemTemplate>
                                                <asp:Label ID="lnkgvreqrat1pPor" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "reqdat1")) %>'
                                                    Width="85px"></asp:Label>


                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Requistion <br> No">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvreqno1pPor" runat="server" Style="text-align: left"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "reqno1"))%>' Width="85px"></asp:Label>

                                            </ItemTemplate>

                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Bill Date">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvBillDate3" runat="server" Style="text-align: left"
                                                    Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "billdate")).ToString("dd-MMM-yyyy") %>' Width="80px"></asp:Label>

                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" />
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Bill No">
                                            <HeaderTemplate>
                                                <asp:TextBox ID="txtSrcBillNo" BackColor="Transparent" Width="120px" BorderStyle="None" runat="server" placeholder="Bill No" onkeyup="Search_Gridview(this, 4, 'gvPayOrder')"></asp:TextBox><br />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvBillNo3" runat="server" Style="text-align: left"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "billno"))%>' Width="90px"></asp:Label>

                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" />
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="MRF No.">
                                            <ItemTemplate>

                                                <asp:HyperLink ID="hlnkgvgvmrfnopPor" runat="server" BorderStyle="none" Style="background-color: Transparent; color: blue;" Font-Underline="false"
                                                    Target="_blank" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mrfno"))%>' Width="80px">
                                      
                                                </asp:HyperLink>


                                            </ItemTemplate>
                                            <ItemStyle Width="80px" />
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                            
                                        <asp:TemplateField HeaderText="Supplier/Pay to">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvPayesupdesc" runat="server" Font-Size="12px" Style="font-size: 12px; color: black;" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "supdesc")) %>'
                                                    Width="150px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle Font-Size="12px" HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="pactcode" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvpactcodepPor" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactcode")) %>'
                                                    Width="120px"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Resource</br> Count">
                                            <ItemTemplate>
                                                <asp:Label ID="lblrescountpayord" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rescount"))%>' Width="40px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="Requistion <br> Amount">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvreqamtpPor" runat="server" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "reqamt")).ToString("#,##0;(#,##0);") %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>

                                            <FooterStyle HorizontalAlign="Right" Width="60px" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Approved Amount">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvApamtpPor" runat="server" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "appamt")).ToString("#,##0;(#,##0);") %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>

                                            <FooterStyle HorizontalAlign="Right" Width="60px" />
                                        </asp:TemplateField>



                                        <asp:TemplateField HeaderText="Balance Amount" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvPaidamtpPor" runat="server" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "balamt")).ToString("#,##0;(#,##0);") %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>

                                            <FooterStyle HorizontalAlign="Right" Width="60px" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Current Status">
                                            <ItemTemplate>
                                                <asp:Label ID="lblcurrentStpPor" runat="server" Font-Size="12px" Style="font-size: 12px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "cstatus")) %>'
                                                    Width="100px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle Font-Size="12px" HorizontalAlign="Center" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="">
                                            <ItemTemplate>
                                                <asp:HyperLink ID="HyInprPrint" CssClass="btn btn-xs btn-default" runat="server" Target="_blank" ForeColor="Blue" Font-Underline="false"><span class="fa fa-print"></span>
                                                </asp:HyperLink>
                                                <asp:LinkButton ID="btnDelOrder" CssClass="btn btn-xs btn-default" runat="server"><span style="color:red" class="fa fa-recycle"></span> </asp:LinkButton>
                                            </ItemTemplate>
                                            <ItemStyle Width="80px" />
                                            <HeaderStyle HorizontalAlign="Center" Width="100px" VerticalAlign="Top" />
                                        </asp:TemplateField>
                                    </Columns>
                                    <PagerStyle CssClass="gvPagination" />
                                    <HeaderStyle CssClass="grvHeader" />
                                    <FooterStyle CssClass="grvFooter" />
                                    <RowStyle CssClass="grvRows" />
                                </asp:GridView>

                            </asp:Panel>

                            <asp:Panel ID="PnlreqInfo1" runat="server" Visible="false">

                                <asp:GridView ID="gvReqInfo1" runat="server" AutoGenerateColumns="False" CssClass="table-striped table-hover table-bordered grvContentarea"
                                    ShowFooter="True">
                                    <RowStyle />
                                    <Columns>


                                        <asp:TemplateField HeaderText="SL">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvDSlNoreq" runat="server" Style="text-align: right"
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="reqno#" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvreqnoreq" runat="server" Style="text-align: right"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "reqno"))%>' Width="15px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="Requistion<br>Date">
                                            <ItemTemplate>
                                                <asp:Label ID="lnkgvreqrat1req" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "reqdat1")) %>'
                                                    Width="70px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Requistion<br>No">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvreqno1req" runat="server" Style="text-align: left"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "reqno1"))%>' Width="85px"></asp:Label>

                                            </ItemTemplate>

                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="MRF No.">
                                            <ItemTemplate>

                                                <asp:Label ID="hypMrfnoreq" runat="server" BorderStyle="none"
                                                    Font-Size="11px" Style="background-color: Transparent;" Font-Underline="false"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "mrfno"))%>' Width="60px">
                                      
                                                </asp:Label>


                                            </ItemTemplate>
                                            <ItemStyle Width="60px" />
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                              
                                        <asp:TemplateField HeaderText="Supplier/Pay to">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvReqesupdesc" runat="server" Font-Size="12px" Style="font-size: 12px; color: black;" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "supdesc")) %>'
                                                    Width="150px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                            <ItemStyle Font-Size="12px" HorizontalAlign="Left" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Resource</br> Count">
                                            <ItemTemplate>
                                                <asp:Label ID="lblrescountreq" runat="server"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rescount"))%>' Width="40px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="Requistion <br>Amount">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvreqamtreq" runat="server" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "reqamt")).ToString("#,##0;(#,##0);") %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>

                                            <FooterStyle HorizontalAlign="Right" Width="60px" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Approved Amount">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvApamtreq" runat="server" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "appamt")).ToString("#,##0;(#,##0);") %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>

                                            <FooterStyle HorizontalAlign="Right" Width="60px" />
                                        </asp:TemplateField>



                                        <asp:TemplateField HeaderText="Balance Amount" Visible="false">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvPaidamtreq" runat="server" Style="text-align: right"
                                                    Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "balamt")).ToString("#,##0;(#,##0);") %>'
                                                    Width="60px"></asp:Label>
                                            </ItemTemplate>

                                            <FooterStyle HorizontalAlign="Right" Width="60px" />
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="First Approval">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvfapprv" runat="server" Style="text-align: left"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "aprvname")) %>'
                                                    Width="120px"></asp:Label>
                                            </ItemTemplate>

                                            <FooterStyle HorizontalAlign="right" Width="60px" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Final Approval">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvfinal" runat="server" Style="text-align: left"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "faprvname")) %>'
                                                    Width="120px"></asp:Label>
                                            </ItemTemplate>

                                            <FooterStyle HorizontalAlign="Right" Width="60px" />
                                        </asp:TemplateField>

                                    </Columns>
                                    <PagerStyle CssClass="gvPagination" />
                                    <HeaderStyle CssClass="grvHeader" />
                                    <FooterStyle CssClass="grvFooter" />
                                    <RowStyle CssClass="grvRows" />
                                </asp:GridView>
                            </asp:Panel>

                        </asp:Panel>

                        <asp:Panel ID="PnlSet" runat="server" Visible="false">


                            <ul class="list-unstyled">

                                <li>
                                    <a href="<%=this.ResolveUrl("~/F_15_Pro/MatAvailabilityFG?Type=FG&actcode=&sircode=&genno=")%> " target="_blank">04. Materials Availability- FG</a>

                                </li>



                            </ul>




                        </asp:Panel>
                        <asp:Panel ID="PnlRep" runat="server" Visible="false">

                            <ul class="list-unstyled">

                                <li>

                                    <a href="<%=this.ResolveUrl("~/F_07_Inv/RptCentralStore?InputType=General")%> " target="_blank">01. Inventory Report-General</a>
                                </li>

                            </ul>





                        </asp:Panel>

                    </div>





                </div>





            </div>



            <%--    <script src="http://ajax.googleapis.com/ajax/libs/jquery/1.10.2/jquery.min.js"></script>
                <script src="http://cdnjs.cloudflare.com/ajax/libs/waypoints/2.0.3/waypoints.min.js"></script>--%>

            <script src="../Scripts/jquery.counterup.min.js"></script>
            <script>
                jQuery(document).ready(function ($) {
                    $('.counter').counterUp({
                        delay: 10,
                        time: 1000
                    });
                });
            </script>
        </ContentTemplate>



    </asp:UpdatePanel>
    <asp:Label ID="lblprintstkl" runat="server"></asp:Label>

</asp:Content>


