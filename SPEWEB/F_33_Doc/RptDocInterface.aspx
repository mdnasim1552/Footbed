<%@ Page Title="" Language="C#" MasterPageFile="~/SPE.Master" AutoEventWireup="true" CodeBehind="RptDocInterface.aspx.cs" Inherits="SPEWEB.F_33_Doc.RptDocInterface" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style type="text/css">
    


        ul.tbMenuWrp {
            margin: 0;
            padding: 0;
            border: 0;
            background: none !important;
        }

            ul.tbMenuWrp li {
                width: 135px;
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
            background: #472AC6 !important;
            color: #fff;
        }


        .tbMenuWrp table tr td {
            /*height: 50px;*/
            padding: 0 0;
            float: left;
            list-style: none;
            margin: 0 3px;
            color: #fff;
            text-align: center;
            /*border: 2px solid #D1D735;*/
            /*-webkit-border-radius: 30px;
            -moz-border-radius: 30px;
            border-radius: 30px;*/
            cursor: pointer;
            background: #fff;
            position: relative;
        }






        table.grvContentarea tr td span.glyphicon {
            margin: 0 4px;
        }

        .tbMenuWrp table tr td label {
            color: #000;
            cursor: pointer;
            font-weight: bold;
            height: 35px;
            margin: 1px 0;
            /*padding: 2px;*/
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
            background: #fff;
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
            background: #667DE8;
            color: #000000;
        }

        .lblactive label tr td {
            background: #667DE8 !important;
            color: #000 !important;
        }

        .blink_me {
            animation: blinker 5s linear infinite;
        }

        @keyframes blinker {
            50% {
                opacity: 0;
            }
        }

        .grvContentarea tr td:last-child {
            /*width: 120px;*/
        }


        .fan:nth-child(1) {
            background-color: #e6b0e1;
            color: #fff;
            height: 100%;
            line-height: 32px;
        }


        .fan {
            border-radius: 0;
            px display: inline-block;
            float: left;
            font-size: 18px;
            padding: 8px;
        }

            .fan:nth-child(1) {
                background-color: #817E24;
                border-bottom: 2px solid red;
                /* border-top: 2px solid red; */
                /* border-left: 3px solid #4800ff; */
                color: #fff;
                height: 35px;
                line-height: 14px;
            }

            .fan:nth-child(2) {
            }

            .fan:nth-child(3) {
            }

            .fan:nth-child(4) {
            }

            .fan:nth-child(5) {
            }

            .fan:nth-child(6) {
            }

            .fan:nth-child(7) {
            }
        /* for interface*/

        .circle-tile {
            margin-bottom: 15px;
            text-align: center;
            width: 117px;
            font-size: 11px;
        }

        .circle-tile-heading {
            border: 3px solid rgba(255, 255, 255, 0.3);
            border-radius: 100%;
            color: #FFFFFF;
            font-size: 15px;
            height: 39px;
            margin: -2px auto -22px;
            padding: 8px 4px;
            position: relative;
            text-align: center;
            transition: all 0.3s ease-in-out 0s;
            width: 42px;
        }

            .circle-tile-heading .fa {
                line-height: 80px;
            }

        .circle-tile-content {
            padding-top: 18px;
            border-radius: 0px 15px;
        }

        .circle-tile-number {
            font-size: 26px;
            font-weight: 700;
            line-height: 1;
            padding: 5px 0 15px;
        }

        .circle-tile-description {
            text-transform: uppercase;
        }

        .circle-tile-footer {
            background-color: rgba(0, 0, 0, 0.1);
            color: rgba(255, 255, 255, 0.5);
            display: block;
            padding: 5px;
            transition: all 0.3s ease-in-out 0s;
        }

            .circle-tile-footer:hover {
                background-color: rgba(0, 0, 0, 0.2);
                color: rgba(255, 255, 255, 0.5);
                text-decoration: none;
            }

        .circle-tile-heading.dark-blue:hover {
            background-color: #2E4154;
        }

        .circle-tile-heading.green:hover {
            background-color: #138F77;
        }

        .circle-tile-heading.orange:hover {
            background-color: #DA8C10;
        }

        .circle-tile-heading.blue:hover {
            background-color: #2473A6;
        }

        .circle-tile-heading.red:hover {
            background-color: #CF4435;
        }

        .circle-tile-heading.purple:hover {
            background-color: #7F3D9B;
        }

        .tile-img {
            text-shadow: 2px 2px 3px rgba(0, 0, 0, 0.9);
        }

        .dark-blue {
            background-color: #00A2C8;
        }

        .green {
            background-color: #16A085;
        }

        .blue {
            background-color: #2980B9;
        }

        .orange {
            background-color: #016C96;
        }

        .red {
            background-color: #007CAA;
        }

        .purple {
            background-color: #026999;
        }

        .dark-gray {
            background-color: #7F8C8D;
        }

        .gray {
            background-color: #95A5A6;
        }

        .light-gray {
            background-color: #BDC3C7;
        }

        .yellow {
            background-color: #F1C40F;
        }

        .text-dark-blue {
            color: #34495E;
        }

        .text-green {
            color: #16A085;
        }

        .text-blue {
            color: #2980B9;
        }

        .text-orange {
            color: #F39C12;
        }

        .text-red {
            color: #E74C3C;
        }

        .text-purple {
            color: #8E44AD;
        }

        .text-faded {
            color: rgba(255, 255, 255, 0.7);
        }
    </style>

    <script type="text/javascript">
        $(document).ready(function () {
            //For navigating using left and right arrow of the keyboard
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);
        });



        function Search_Gridview(strKey, cellNr, gvName) {
            //alert(cellNr);
            var tblData;


            var strData = strKey.value.toLowerCase().split(" ");
            switch (gvName) {
                case "gvDocStatus":
                    tblData = document.getElementById("<%=gvDocStatus.ClientID %>");
                    break;
                case "gvdocprocess":
                    tblData = document.getElementById("<%=gvdocprocess.ClientID %>");
                    break;
                case "gvFinalApprvl":
                    tblData = document.getElementById("<%=gvFinalApprvl.ClientID %>");
                    break;
                case "gvCompleted":
                    tblData = document.getElementById("<%=gvCompleted.ClientID %>");
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

            //   $('.chzn-select').chosen({ search_contains: true });
        };

        function openModal() {

            $('#exampleModalDrawerRight').modal('toggle');

        }

    </script>


<%--    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>--%>
      <div class="page">
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
                        <div class="col-md-1">
                            <div class="form-group">
                                <label class="control-label" for="FromDate">From</label>
                                <asp:TextBox ID="txtFDate" runat="server" CssClass="form-control form-control-sm" AutoPostBack="true"></asp:TextBox>
                                <cc1:CalendarExtender ID="CalendarExtender_txtfrmdate" runat="server" Enabled="True"
                                    Format="dd-MMM-yyyy" TargetControlID="txtFDate"></cc1:CalendarExtender>
                            </div>
                        </div>
                        <div class="col-md-1">
                            <div class="form-group">
                                <label class="control-label" for="ToDate">To</label>
                                <asp:TextBox ID="txtdate" runat="server" CssClass="form-control form-control-sm" AutoPostBack="true"></asp:TextBox>
                                <cc1:CalendarExtender ID="CalendarExtender_txtdate" runat="server" Enabled="True"
                                    Format="dd-MMM-yyyy" TargetControlID="txtdate"></cc1:CalendarExtender>
                            </div>

                        </div>

                       


                        <div class="col-md-1">
                            <div class="form-group">
                                <asp:LinkButton ID="lnkbtnok" runat="server" CssClass="margin-top30px btn-sm btn btn-primary" OnClick="lbtnOk_Click">Ok</asp:LinkButton>
                            </div>
                        </div>

                     
                        <div class="col-md-1">
                            <div class="margin-top30px btn-group" role="group" aria-label="Button group with nested dropdown">
                                <button type="button" class="btn btn-sm btn-danger">Operations</button>
                                <div class="btn-group" role="group">
                                    <button id="btnGroupDrop4" type="button" class="btn btn-sm btn-danger dropdown-toggle" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false"></button>
                                    <div class="dropdown-menu" aria-labelledby="btnGroupDrop4" style="">
                                        <div class="dropdown-arrow"></div>
                                        <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/F_33_Doc/DocUpload?Type=Entry&genno=" Target="_blank" CssClass="dropdown-item">New Doc Entry</asp:HyperLink>
                                        <asp:HyperLink ID="HyperLink2" runat="server" NavigateUrl="~/F_33_Doc/DocCodeBook?Type=Entry" Target="_blank" CssClass="dropdown-item">Doc Code Book</asp:HyperLink>
                                        <%--<asp:HyperLink ID="HyperLink2" runat="server" Target="_blank" NavigateUrl="~/F_15_Pro/PurInformation.aspx" CssClass="dropdown-item">Dashboard</asp:HyperLink>--%>
                                    </div>
                                </div>
                            </div>
                        </div>



                    </div>
                </div>
            </div>


            <div class="card card-fluid" style="min-height:350px;">
                <div class="card-body">

                    <%--    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>--%>

                    <%-- <asp:Timer ID="Timer1" runat="server" OnTick="Timer1_Tick" Interval="50000"></asp:Timer>
                            <triggers> <asp:AsyncPostBackTrigger ControlID="Timer1" EventName="Tick" /></triggers>--%>

                    <div class="row">
                        <asp:Panel ID="PnlInt" runat="server" Visible="false">
                            <div id="slSt" class=" col-md-12">
                                <div class="panel with-nav-tabs panel-primary">
                                    <fieldset class="tabMenu">
                                        <div class="form-horizontal">
                                            <div class="form-group">
                                                <div class="tbMenuWrp nav nav-tabs">
                                                    <asp:RadioButtonList ID="RadioButtonList1" runat="server" AutoPostBack="true" OnSelectedIndexChanged="RadioButtonList1_SelectedIndexChanged" RepeatDirection="Horizontal">
                                                        <asp:ListItem Value="0"></asp:ListItem>
                                                        <asp:ListItem Value="1"></asp:ListItem>
                                                        <asp:ListItem Value="2"></asp:ListItem>
                                                        <asp:ListItem Value="3"></asp:ListItem>
                                                    </asp:RadioButtonList>

                                                </div>
                                            </div>
                                        </div>
                                        <div class="clearfix"></div>
                                    </fieldset>
                                    <div>
                                        <asp:Panel ID="pnlallInqList" runat="server" Visible="false">
                                            <div class="row">
                                                <div class="table-responsive col-lg-12" style="min-height: 450px;">
                                                    <asp:GridView ID="gvDocStatus" runat="server" AutoGenerateColumns="False" CssClass=" table-striped table-hover table-bordered grvContentarea"
                                                        ShowFooter="True" OnRowDataBound="gvDocStatus_RowDataBound">
                                                        <RowStyle />
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="Sl">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvSlNo0" runat="server" Font-Bold="True"
                                                                        Style="text-align: right"
                                                                        Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="15px"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>
                                                              <asp:TemplateField HeaderText="Doc No">
                                                                <HeaderTemplate>
                                                                    <asp:TextBox ID="txtSearchDocNo" BackColor="Transparent" BorderStyle="None" runat="server" Width="110px" placeholder="Doc No" onkeyup="Search_Gridview(this,1, 'gvDocStatus')"></asp:TextBox><br />
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="txtgvDocno" runat="server"  OnClick="txtgvDocno_Click"                                                                       
                                                                        Text='<%# "DOC-NO-"+Convert.ToString(DataBinder.Eval(Container.DataItem, "docno")) %>'
                                                                        Width="110px"></asp:LinkButton>
                                                                </ItemTemplate>

                                                                <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Ref No">
                                                                <HeaderTemplate>
                                                                    <asp:TextBox ID="txtSearchRefno" BackColor="Transparent" BorderStyle="None" runat="server" Width="110px" placeholder="Ref/P.O No" onkeyup="Search_Gridview(this,1, 'gvDocStatus')"></asp:TextBox><br />
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="txtgvRefno" runat="server" AutoCompleteType="Disabled"
                                                                        BackColor="Transparent" BorderStyle="None" Font-Size="11px"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "refno")) %>'
                                                                        Width="110px"></asp:Label>
                                                                </ItemTemplate>

                                                                <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Suppplier Name">
                                                                <HeaderTemplate>
                                                                    <asp:TextBox ID="txtSearchSupplier" BackColor="Transparent" BorderStyle="None" runat="server" Width="60px" placeholder="Supplier" onkeyup="Search_Gridview(this,2, 'gvDocStatus')"></asp:TextBox><br />
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblSupplier" runat="server" AutoCompleteType="Disabled"
                                                                        BackColor="Transparent" BorderStyle="None" Font-Size="11px"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "ssirdesc")) %>'
                                                                        Width="200px"></asp:Label>
                                                                </ItemTemplate>

                                                                <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Order Date">
                                                                <HeaderTemplate>
                                                                    <asp:TextBox ID="txtSearchOrderDate" BackColor="Transparent" BorderStyle="None" runat="server" Width="80px" placeholder="Order Date" onkeyup="Search_Gridview(this,3, 'gvDocStatus')"></asp:TextBox><br />
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="LblOrdDate" runat="server" AutoCompleteType="Disabled"
                                                                        BackColor="Transparent" BorderStyle="None" Font-Size="11px"
                                                                        Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "ordrdat")).ToString("dd-MMM-yyyy") %>'
                                                                        Width="80px"></asp:Label>
                                                                </ItemTemplate>

                                                                <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>

                                                           
                                                            <asp:TemplateField HeaderText="Delivery Date">
                                                                <HeaderTemplate>
                                                                    <asp:TextBox ID="txtSearchDelDat" BackColor="Transparent" BorderStyle="None" runat="server" Width="60px" placeholder="Delivery Date" onkeyup="Search_Gridview(this,6, 'gvDocStatus')"></asp:TextBox><br />
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvartno" runat="server" AutoCompleteType="Disabled"
                                                                        BackColor="Transparent" BorderStyle="None" Font-Size="11px"
                                                                        Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "deldat")).ToString("dd-MMM-yyyy") %>'
                                                                        Width="80px"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>
                                                           

                                                            <asp:TemplateField HeaderText="DOc Id" Visible="false">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvDocId" runat="server" Visible="false" Height="16px" Font-Size="11px"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "docno")) %>'
                                                                        Width="80px"></asp:Label>
                                                                    <asp:Label ID="lblSSIRcode" runat="server" Visible="false" Height="16px" Font-Size="11px"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "ssircode")) %>'
                                                                        Width="80px"></asp:Label>      
                                                                         <asp:Label ID="lblfstep" runat="server" Visible="false" Height="16px" Font-Size="11px"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "curstep")) %>'
                                                                        Width="80px"></asp:Label>  
                                                             
                                                                         <asp:Label ID="lbltstep" runat="server" Visible="false" Height="16px" Font-Size="11px"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "tstep")) %>'
                                                                        Width="80px"></asp:Label> 
                                                                    
                                                                         <asp:Label ID="blFrwtime" runat="server" Visible="false" Height="16px" Font-Size="11px"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "forwtime")) %>'
                                                                        Width="80px"></asp:Label>  
                                                                        <asp:Label ID="LBlTimeFlag" runat="server" Visible="false" Height="16px" Font-Size="11px"
                                                                        Text='<%# Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "timeflag")) %>'
                                                                        Width="80px"></asp:Label>  
                                                                    

                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Currency" >
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvCurrency" runat="server" CssClass="text-center"  Height="16px" Font-Size="11px"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "currency")) %>'
                                                                        Width="50px"></asp:Label>
                                                                                                                                   

                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Order value">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="txtgvordval" runat="server" BackColor="Transparent"
                                                                        BorderStyle="None" Height="18px" Style="text-align: right" 
                                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ordval")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                                        Width="70px"></asp:Label>
                                                                </ItemTemplate>

                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                                <ItemStyle HorizontalAlign="Right" />
                                                                <FooterStyle HorizontalAlign="Right" />
                                                            </asp:TemplateField>
                                                             <asp:TemplateField HeaderText="Remarks">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvNotes" runat="server" CssClass="text-purple"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "remarks")) %>'
                                                                        Width="80px"></asp:Label>
                                                                                                                                   

                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>
                                                              <asp:TemplateField HeaderText="Posted On">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvPostedOn" runat="server" CssClass="font-italic small text-right text-danger"  Height="16px" Font-Size="11px"
                                                                        Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "posteddat")).ToString("dd-MMM-yyyy hh:mm tt") %>'
                                                                        Width="120px"></asp:Label>
                                                                                                                                   

                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>
                                                              <asp:TemplateField HeaderText="Posted By" >
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvUser" runat="server" CssClass="text-center"  Height="16px" Font-Size="11px"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "postedusr")) %>'
                                                                        Width="100px"></asp:Label>
                                                                                                                                   

                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Stack On" >
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvCurStep" runat="server" CssClass="text-center"  Height="16px" Font-Size="11px"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "curusrname")) %>'
                                                                        Width="100px"></asp:Label>
                                                                                                                                   

                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Action">
                                                                <ItemTemplate>
                                                                    <div class="dropdown">
                                                                        <button class="btn btn-primary btn-xs dropdown-toggle" type="button" data-toggle="dropdown">
                                                                            Action
                                                                             
                                                                        </button>
                                                                        <ul class="dropdown-menu dropdown-menu-right">
                                                                            <li>
                                                                                <asp:HyperLink ID="lnkEdit" runat="server" Target="_blank" ForeColor="Blue" Font-Underline="false"> &nbsp; <span class="fa fa-edit"></span> Edit </asp:HyperLink>
                                                                            </li>
                                                                          <li>
                                                                                <asp:LinkButton ID="LbtnForward" OnClick="LbtnForward_Click" runat="server" ForeColor="Blue" Font-Underline="false"> &nbsp; <span class="fa fa-check"></span> Forward</asp:LinkButton></li>
                                                                             <li>
                                                                                <asp:HyperLink ID="HypActivity" NavigateUrl='<%# "~/F_33_Doc/ShowAllDoc?Type=Rpt&genno="+SPELIB.Common.Base64Encode(Convert.ToString(DataBinder.Eval(Container.DataItem, "docno"))) %>' runat="server" Target="_blank" ForeColor="Blue" Font-Underline="false"> &nbsp; <span class="fa fa-list"></span> Activity Log</asp:HyperLink>

                                                                             </li>

                                                                             <%-- <li>
                                                                                <asp:HyperLink ID="HyCommPreCostPrint" runat="server" Target="_blank" ForeColor="Blue" Font-Underline="false"><span class="fa fa-print"></span>CBD Common</asp:HyperLink></li>
                                  --%>                                          <%--<li><asp:HyperLink ID="HyOrderPrint" runat="server" Target="_blank" ForeColor="Blue" Font-Underline="false"><span class="fa fa-print"></span>Order</asp:HyperLink></li>--%>

                                                                            <%--<li><asp:HyperLink ID="HyFOrderPrint" runat="server" Target="_blank" ForeColor="Blue" Font-Underline="false"><span class="fa fa-print"></span>BOM Foreign</asp:HyperLink></li>--%>
                                                                            <%--<li><asp:HyperLink ID="HyLOrderPrint" runat="server" Target="_blank" ForeColor="Blue" Font-Underline="false"><span class="fa fa-print"></span>BOM Local</asp:HyperLink></li>--%>
                                                                        </ul>
                                                                    </div>


                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                        </Columns>


                                                        <PagerStyle CssClass="gvPagination" />
                                                        <HeaderStyle CssClass="grvHeader" />
                                                        <FooterStyle CssClass="grvFooter" />
                                                        <RowStyle CssClass="grvRows" />

                                                    </asp:GridView>
                                                </div>
                                            </div>
                                        </asp:Panel>

                                        <asp:Panel ID="pnlApprovalProcess" Visible="false" runat="server">

                                            <div class="row">

                                                <div class="table-responsive col-lg-12" style="min-height: 450px;">
                                                <asp:GridView ID="gvdocprocess" runat="server" AutoGenerateColumns="False" CssClass=" table-striped table-hover table-bordered grvContentarea"
                                                        ShowFooter="True" OnRowDataBound="gvdocprocess_RowDataBound">
                                                        <RowStyle />
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="Sl">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvSlNo0" runat="server" Font-Bold="True"
                                                                        Style="text-align: right"
                                                                        Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="15px"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>
                                                              <asp:TemplateField HeaderText="Doc No">
                                                                <HeaderTemplate>
                                                                    <asp:TextBox ID="txtSearchDocNo" BackColor="Transparent" BorderStyle="None" runat="server" Width="110px" placeholder="Doc No" onkeyup="Search_Gridview(this,1, 'gvDocStatus')"></asp:TextBox><br />
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="txtgvProcessDocno" runat="server"  OnClick="txtgvProcessDocno_Click"                                                                       
                                                                        Text='<%# "DOC-NO-"+Convert.ToString(DataBinder.Eval(Container.DataItem, "docno")) %>'
                                                                        Width="110px"></asp:LinkButton>
                                                                </ItemTemplate>

                                                                <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Ref No">
                                                                <HeaderTemplate>
                                                                    <asp:TextBox ID="txtSearchRefno" BackColor="Transparent" BorderStyle="None" runat="server" Width="110px" placeholder="Ref/P.O No" onkeyup="Search_Gridview(this,1, 'gvDocStatus')"></asp:TextBox><br />
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="txtgvRefno" runat="server" AutoCompleteType="Disabled"
                                                                        BackColor="Transparent" BorderStyle="None" Font-Size="11px"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "refno")) %>'
                                                                        Width="110px"></asp:Label>
                                                                </ItemTemplate>

                                                                <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Suppplier Name">
                                                                <HeaderTemplate>
                                                                    <asp:TextBox ID="txtSearchSupplier" BackColor="Transparent" BorderStyle="None" runat="server" Width="60px" placeholder="Supplier" onkeyup="Search_Gridview(this,2, 'gvDocStatus')"></asp:TextBox><br />
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblSupplier" runat="server" AutoCompleteType="Disabled"
                                                                        BackColor="Transparent" BorderStyle="None" Font-Size="11px"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "ssirdesc")) %>'
                                                                        Width="200px"></asp:Label>
                                                                </ItemTemplate>

                                                                <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Order Date">
                                                                <HeaderTemplate>
                                                                    <asp:TextBox ID="txtSearchOrderDate" BackColor="Transparent" BorderStyle="None" runat="server" Width="80px" placeholder="Order Date" onkeyup="Search_Gridview(this,3, 'gvDocStatus')"></asp:TextBox><br />
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="LblOrdDate" runat="server" AutoCompleteType="Disabled"
                                                                        BackColor="Transparent" BorderStyle="None" Font-Size="11px"
                                                                        Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "ordrdat")).ToString("dd-MMM-yyyy") %>'
                                                                        Width="80px"></asp:Label>
                                                                </ItemTemplate>

                                                                <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>

                                                           
                                                            <asp:TemplateField HeaderText="Delivery Date">
                                                                <HeaderTemplate>
                                                                    <asp:TextBox ID="txtSearchDelDat" BackColor="Transparent" BorderStyle="None" runat="server" Width="60px" placeholder="Delivery Date" onkeyup="Search_Gridview(this,6, 'gvDocStatus')"></asp:TextBox><br />
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvartno" runat="server" AutoCompleteType="Disabled"
                                                                        BackColor="Transparent" BorderStyle="None" Font-Size="11px"
                                                                        Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "deldat")).ToString("dd-MMM-yyyy") %>'
                                                                        Width="80px"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>
                                                           

                                                            <asp:TemplateField HeaderText="DOc Id" Visible="false">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvDocId" runat="server" Visible="false" Height="16px" Font-Size="11px"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "docno")) %>'
                                                                        Width="80px"></asp:Label>
                                                                    <asp:Label ID="lblSSIRcode" runat="server" Visible="false" Height="16px" Font-Size="11px"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "ssircode")) %>'
                                                                        Width="80px"></asp:Label>      
                                                                         <asp:Label ID="lblfstep" runat="server" Visible="false" Height="16px" Font-Size="11px"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "curstep")) %>'
                                                                        Width="80px"></asp:Label>  
                                                                           <asp:Label ID="lblPrevStep" runat="server" Visible="false" Height="16px" Font-Size="11px"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "prevstep")) %>'
                                                                        Width="80px"></asp:Label> 
                                                                         <asp:Label ID="lbltstep" runat="server" Visible="false" Height="16px" Font-Size="11px"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "tstep")) %>'
                                                                        Width="80px"></asp:Label>  
                                                                       <asp:Label ID="blFrwtime" runat="server" Visible="false" Height="16px" Font-Size="11px"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "forwtime")) %>'
                                                                        Width="80px"></asp:Label>  
                                                                        <asp:Label ID="LBlTimeFlag" runat="server" Visible="false" Height="16px" Font-Size="11px"
                                                                        Text='<%# Convert.ToBoolean(DataBinder.Eval(Container.DataItem, "timeflag")) %>'
                                                                        Width="80px"></asp:Label>  

                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Currency" >
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvCurrency" runat="server" CssClass="text-center"  Height="16px" Font-Size="11px"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "currency")) %>'
                                                                        Width="50px"></asp:Label>
                                                                                                                                   

                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Order value">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="txtgvordval" runat="server" BackColor="Transparent"
                                                                        BorderStyle="None" Height="18px" Style="text-align: right" Font-Size="11px"
                                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ordval")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                                        Width="70px"></asp:Label>
                                                                </ItemTemplate>

                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                                <ItemStyle HorizontalAlign="Right" />
                                                                <FooterStyle HorizontalAlign="Right" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Remarks">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvNotes" runat="server" CssClass="text-purple"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "remarks")) %>'
                                                                        Width="80px"></asp:Label>
                                                                                                                                   

                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>
                                                              <asp:TemplateField HeaderText="Forward On">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvPostedOn" ToolTip='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "lstcomments")) %>' runat="server" CssClass="font-italic small text-right text-danger"  Height="16px" Font-Size="11px"
                                                                        Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "lstrowdat")).ToString("dd-MMM-yyyy hh:mm tt") %>'
                                                                        Width="120px"></asp:Label>
                                                                                                                                   

                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>
                                                           <asp:TemplateField HeaderText="Forwarded By" >
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvUser" runat="server"  ToolTip='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "lstcomments")) %>' CssClass="text-center"  Height="16px" Font-Size="11px"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "lstusrname")) %>'
                                                                        Width="100px"></asp:Label>
                                                                                                                                   

                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField>
                                                                <ItemTemplate>
                                                                    
                                                                    </ItemTemplate>
                                                            </asp:TemplateField>
                                                             <asp:TemplateField HeaderText="Notes">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvCommetns" runat="server" CssClass="text-purple"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "lstcomments")) %>'
                                                                        Width="100px"></asp:Label>
                                                                                                                                   

                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Comments" >
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="TxtgvComments"  runat="server" Width="150px" CssClass="form-control form-control-sm"></asp:TextBox>
                                                                                                                                   

                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>
                                                       
                                                            <asp:TemplateField HeaderText="Action">
                                                                <ItemTemplate>
                                                                    <div class="dropdown">
                                                                        <button class="btn btn-primary btn-xs dropdown-toggle" type="button" data-toggle="dropdown">
                                                                            Action
                                                                             
                                                                        </button>
                                                                        <ul class="dropdown-menu dropdown-menu-right">                                                                          
                                                                          <li>
                                                                                <asp:LinkButton ID="LbtnForwardProcess" OnClick="LbtnForwardProcess_Click" runat="server" ForeColor="Blue" Font-Underline="false"> &nbsp; <span class="fa fa-check"></span> Forward</asp:LinkButton>

                                                                          </li>
                                                                              <li>
                                                                                <asp:LinkButton ID="LbtnBackward" runat="server" OnClick="LbtnBackward_Click" ForeColor="Blue" Font-Underline="false">&nbsp; <span class="fa fa-backward"></span> Backward</asp:LinkButton>

                                                                               </li>
                                                                            <li>
                                                                                <asp:HyperLink ID="HypActivity" NavigateUrl='<%# "~/F_33_Doc/ShowAllDoc?Type=Rpt&genno="+ SPELIB.Common.Base64Encode(Convert.ToString(DataBinder.Eval(Container.DataItem, "docno"))) %>' runat="server" Target="_blank" ForeColor="Blue" Font-Underline="false"> &nbsp; <span class="fa fa-list"></span> Activity Log</asp:HyperLink>

                                                                             </li>
                                                                             
                                                                        
                                                                          <%--  <li>
                                                                                <asp:HyperLink ID="HyPreCostPrint" runat="server" Target="_blank" ForeColor="Blue" Font-Underline="false"><span class="fa fa-print"></span>CBD Direct</asp:HyperLink></li>
                                                                            <li>
                                                                                <asp:HyperLink ID="HyCommPreCostPrint" runat="server" Target="_blank" ForeColor="Blue" Font-Underline="false"><span class="fa fa-print"></span>CBD Common</asp:HyperLink></li>
                                  --%>                                          <%--<li><asp:HyperLink ID="HyOrderPrint" runat="server" Target="_blank" ForeColor="Blue" Font-Underline="false"><span class="fa fa-print"></span>Order</asp:HyperLink></li>--%>

                                                                            <%--<li><asp:HyperLink ID="HyFOrderPrint" runat="server" Target="_blank" ForeColor="Blue" Font-Underline="false"><span class="fa fa-print"></span>BOM Foreign</asp:HyperLink></li>--%>
                                                                            <%--<li><asp:HyperLink ID="HyLOrderPrint" runat="server" Target="_blank" ForeColor="Blue" Font-Underline="false"><span class="fa fa-print"></span>BOM Local</asp:HyperLink></li>--%>
                                                                        </ul>
                                                                    </div>


                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                        </Columns>


                                                        <PagerStyle CssClass="gvPagination" />
                                                        <HeaderStyle CssClass="grvHeader" />
                                                        <FooterStyle CssClass="grvFooter" />
                                                        <RowStyle CssClass="grvRows" />

                                                    </asp:GridView>
                                                </div>
                                            </div>
                                                                 
                                        </asp:Panel>
                                        <asp:Panel ID="PanlFinalApproval" Visible="false" runat="server">
                                                                                     
                                            <div class="row">
                                                <div class="table-responsive col-lg-12" style="min-height:350px;">
                                                    <asp:GridView ID="gvFinalApprvl" runat="server" AutoGenerateColumns="False" CssClass=" table-striped table-hover table-bordered grvContentarea"
                                                        ShowFooter="True">
                                                        <RowStyle />
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="Sl">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvSlNo0" runat="server" Font-Bold="True"
                                                                        Style="text-align: right"
                                                                        Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="15px"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>
                                                              <asp:TemplateField HeaderText="Doc No">
                                                                <HeaderTemplate>
                                                                    <asp:TextBox ID="txtSearchDocNo" BackColor="Transparent" BorderStyle="None" runat="server" Width="110px" placeholder="Doc No" onkeyup="Search_Gridview(this,1, 'gvDocStatus')"></asp:TextBox><br />
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="txtgvFinalAppDocno" runat="server"  OnClick="txtgvFinalAppDocno_Click"                                                                       
                                                                        Text='<%# "DOC-NO-"+Convert.ToString(DataBinder.Eval(Container.DataItem, "docno")) %>'
                                                                        Width="110px"></asp:LinkButton>
                                                                </ItemTemplate>

                                                                <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Ref No">
                                                                <HeaderTemplate>
                                                                    <asp:TextBox ID="txtSearchRefno" BackColor="Transparent" BorderStyle="None" runat="server" Width="110px" placeholder="Ref/P.O No" onkeyup="Search_Gridview(this,1, 'gvDocStatus')"></asp:TextBox><br />
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="txtgvRefno" runat="server" AutoCompleteType="Disabled"
                                                                        BackColor="Transparent" BorderStyle="None" Font-Size="11px"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "refno")) %>'
                                                                        Width="110px"></asp:Label>
                                                                </ItemTemplate>

                                                                <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Suppplier Name">
                                                                <HeaderTemplate>
                                                                    <asp:TextBox ID="txtSearchSupplier" BackColor="Transparent" BorderStyle="None" runat="server" Width="60px" placeholder="Supplier" onkeyup="Search_Gridview(this,2, 'gvDocStatus')"></asp:TextBox><br />
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblSupplier" runat="server" AutoCompleteType="Disabled"
                                                                        BackColor="Transparent" BorderStyle="None" Font-Size="11px"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "ssirdesc")) %>'
                                                                        Width="200px"></asp:Label>
                                                                </ItemTemplate>

                                                                <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Order Date">
                                                                <HeaderTemplate>
                                                                    <asp:TextBox ID="txtSearchOrderDate" BackColor="Transparent" BorderStyle="None" runat="server" Width="80px" placeholder="Order Date" onkeyup="Search_Gridview(this,3, 'gvDocStatus')"></asp:TextBox><br />
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="LblOrdDate" runat="server" AutoCompleteType="Disabled"
                                                                        BackColor="Transparent" BorderStyle="None" Font-Size="11px"
                                                                        Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "ordrdat")).ToString("dd-MMM-yyyy") %>'
                                                                        Width="80px"></asp:Label>
                                                                </ItemTemplate>

                                                                <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>

                                                           
                                                            <asp:TemplateField HeaderText="Delivery Date">
                                                                <HeaderTemplate>
                                                                    <asp:TextBox ID="txtSearchDelDat" BackColor="Transparent" BorderStyle="None" runat="server" Width="60px" placeholder="Delivery Date" onkeyup="Search_Gridview(this,6, 'gvDocStatus')"></asp:TextBox><br />
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvartno" runat="server" AutoCompleteType="Disabled"
                                                                        BackColor="Transparent" BorderStyle="None" Font-Size="11px"
                                                                        Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "deldat")).ToString("dd-MMM-yyyy") %>'
                                                                        Width="80px"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>
                                                           

                                                            <asp:TemplateField HeaderText="DOc Id" Visible="false">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvDocId" runat="server" Visible="false" Height="16px" Font-Size="11px"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "docno")) %>'
                                                                        Width="80px"></asp:Label>
                                                                    <asp:Label ID="lblSSIRcode" runat="server" Visible="false" Height="16px" Font-Size="11px"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "ssircode")) %>'
                                                                        Width="80px"></asp:Label>      
                                                                         <asp:Label ID="lblfstep" runat="server" Visible="false" Height="16px" Font-Size="11px"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "curstep")) %>'
                                                                        Width="80px"></asp:Label>  
                                                                     <asp:Label ID="lblPrevStep" runat="server" Visible="false" Height="16px" Font-Size="11px"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "prevstep")) %>'
                                                                        Width="80px"></asp:Label>  
                                                                         <asp:Label ID="lbltstep" runat="server" Visible="false" Height="16px" Font-Size="11px"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "tstep")) %>'
                                                                        Width="80px"></asp:Label>  

                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Currency" >
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvCurrency" runat="server" CssClass="text-center"  Height="16px" Font-Size="11px"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "currency")) %>'
                                                                        Width="50px"></asp:Label>
                                                                                                                                   

                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Order value">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="txtgvordval" runat="server" BackColor="Transparent"
                                                                        BorderStyle="None" Height="18px" Style="text-align: right" Font-Size="11px"
                                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ordval")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                                        Width="70px"></asp:Label>
                                                                </ItemTemplate>

                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                                <ItemStyle HorizontalAlign="Right" />
                                                                <FooterStyle HorizontalAlign="Right" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Remarks">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvNotes" runat="server" CssClass="text-purple"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "remarks")) %>'
                                                                        Width="80px"></asp:Label>
                                                                                                                                   

                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>
                                                              <asp:TemplateField HeaderText="Forward On">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvPostedOn" runat="server" ToolTip='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "lstcomments")) %>' CssClass="font-italic small text-right text-danger"  Height="16px" Font-Size="11px"
                                                                        Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "lstrowdat")).ToString("dd-MMM-yyyy hh:mm tt") %>'
                                                                        Width="120px"></asp:Label>
                                                                                                                                   

                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>
                                                           <asp:TemplateField HeaderText="Forwarded By" >
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvUser" ToolTip='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "lstcomments")) %>' runat="server" CssClass="text-center"  Height="16px" Font-Size="11px"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "lstusrname")) %>'
                                                                        Width="100px"></asp:Label>
                                                                                                                                   

                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Notes">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvCommetns" runat="server" CssClass="text-purple"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "lstcomments")) %>'
                                                                        Width="100px"></asp:Label>
                                                                                                                                   

                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Comments" >
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="TxtgvComments"  CssClass="form-control form-control-sm"  runat="server" Width="150px"></asp:TextBox>
                                                                                                                                   

                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>
                                                       
                                                            <asp:TemplateField HeaderText="Action">
                                                                <ItemTemplate>
                                                                    <div class="dropdown">
                                                                        <button class="btn btn-primary btn-xs dropdown-toggle" type="button" data-toggle="dropdown">
                                                                            Action
                                                                             
                                                                        </button>
                                                                        <ul class="dropdown-menu dropdown-menu-right">                                                                          
                                                                          <li>
                                                                                <asp:LinkButton ID="LbtnFinalProcess" OnClick="LbtnFinalProcess_Click" runat="server" ForeColor="Blue" Font-Underline="false"> &nbsp; <span class="fa fa-check"></span> Approve</asp:LinkButton>

                                                                          </li>
                                                                             <li>
                                                                                <asp:LinkButton ID="LbtnFinalAppBackward" runat="server" OnClick="LbtnFinalAppBackward_Click" ForeColor="Blue" Font-Underline="false">&nbsp; <span class="fa fa-backward"></span> Backward</asp:LinkButton>

                                                                               </li>
                                                                            <li>
                                                                                <asp:HyperLink ID="HypActivity" NavigateUrl='<%# "~/F_33_Doc/ShowAllDoc?Type=Rpt&genno="+SPELIB.Common.Base64Encode(Convert.ToString(DataBinder.Eval(Container.DataItem, "docno"))) %>' runat="server" Target="_blank" ForeColor="Blue" Font-Underline="false"> &nbsp; <span class="fa fa-list"></span> Activity Log</asp:HyperLink>

                                                                             </li>
                                                                           <%--
                                                                                <asp:HyperLink ID="HyCommPreCostPrint" runat="server" Target="_blank" ForeColor="Blue" Font-Underline="false"><span class="fa fa-print"></span>CBD Common</asp:HyperLink></li>
                                                                         <li><asp:HyperLink ID="HyOrderPrint" runat="server" Target="_blank" ForeColor="Blue" Font-Underline="false"><span class="fa fa-print"></span>Order</asp:HyperLink></li>--%>

                                                                            <%--<li><asp:HyperLink ID="HyFOrderPrint" runat="server" Target="_blank" ForeColor="Blue" Font-Underline="false"><span class="fa fa-print"></span>BOM Foreign</asp:HyperLink></li>--%>
                                                                            <%--<li><asp:HyperLink ID="HyLOrderPrint" runat="server" Target="_blank" ForeColor="Blue" Font-Underline="false"><span class="fa fa-print"></span>BOM Local</asp:HyperLink></li>--%>
                                                                        </ul>
                                                                    </div>


                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                        </Columns>


                                                        <PagerStyle CssClass="gvPagination" />
                                                        <HeaderStyle CssClass="grvHeader" />
                                                        <FooterStyle CssClass="grvFooter" />
                                                        <RowStyle CssClass="grvRows" />

                                                    </asp:GridView>

                                                </div>
                                            </div>

                                        </asp:Panel>
                                        <asp:Panel ID="PanlOrdAcRej" Visible="false" runat="server">

                                            <div class="row">
                                                <div class="table-responsive col-lg-12" style="min-height:350px">
                                              <asp:GridView ID="gvCompleted" runat="server" AutoGenerateColumns="False" CssClass=" table-striped table-hover table-bordered grvContentarea"
                                                        ShowFooter="True">
                                                        <RowStyle />
                                                        <Columns>
                                                            <asp:TemplateField HeaderText="Sl">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvSlNo0" runat="server" Font-Bold="True"
                                                                        Style="text-align: right"
                                                                        Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="15px"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>
                                                              <asp:TemplateField HeaderText="Doc No">
                                                                <HeaderTemplate>
                                                                    <asp:TextBox ID="txtSearchDocNo" BackColor="Transparent" BorderStyle="None" runat="server" Width="110px" placeholder="Doc No" onkeyup="Search_Gridview(this,1, 'gvDocStatus')"></asp:TextBox><br />
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="txtgvCompletedDocno" runat="server"  OnClick="txtgvCompletedDocno_Click"                                                                       
                                                                        Text='<%# "DOC-NO-"+Convert.ToString(DataBinder.Eval(Container.DataItem, "docno")) %>'
                                                                        Width="110px"></asp:LinkButton>
                                                                </ItemTemplate>

                                                                <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Ref No">
                                                                <HeaderTemplate>
                                                                    <asp:TextBox ID="txtSearchRefno" BackColor="Transparent" BorderStyle="None" runat="server" Width="110px" placeholder="Ref/P.O No" onkeyup="Search_Gridview(this,1, 'gvDocStatus')"></asp:TextBox><br />
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="txtgvRefno" runat="server" AutoCompleteType="Disabled"
                                                                        BackColor="Transparent" BorderStyle="None" Font-Size="11px"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "refno")) %>'
                                                                        Width="110px"></asp:Label>
                                                                </ItemTemplate>

                                                                <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Suppplier Name">
                                                                <HeaderTemplate>
                                                                    <asp:TextBox ID="txtSearchSupplier" BackColor="Transparent" BorderStyle="None" runat="server" Width="60px" placeholder="Supplier" onkeyup="Search_Gridview(this,2, 'gvDocStatus')"></asp:TextBox><br />
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblSupplier" runat="server" AutoCompleteType="Disabled"
                                                                        BackColor="Transparent" BorderStyle="None" Font-Size="11px"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "ssirdesc")) %>'
                                                                        Width="200px"></asp:Label>
                                                                </ItemTemplate>

                                                                <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>

                                                            <asp:TemplateField HeaderText="Order Date">
                                                                <HeaderTemplate>
                                                                    <asp:TextBox ID="txtSearchOrderDate" BackColor="Transparent" BorderStyle="None" runat="server" Width="80px" placeholder="Order Date" onkeyup="Search_Gridview(this,3, 'gvDocStatus')"></asp:TextBox><br />
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="LblOrdDate" runat="server" AutoCompleteType="Disabled"
                                                                        BackColor="Transparent" BorderStyle="None" Font-Size="11px"
                                                                        Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "ordrdat")).ToString("dd-MMM-yyyy") %>'
                                                                        Width="80px"></asp:Label>
                                                                </ItemTemplate>

                                                                <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>

                                                           
                                                            <asp:TemplateField HeaderText="Delivery Date">
                                                                <HeaderTemplate>
                                                                    <asp:TextBox ID="txtSearchDelDat" BackColor="Transparent" BorderStyle="None" runat="server" Width="60px" placeholder="Delivery Date" onkeyup="Search_Gridview(this,6, 'gvDocStatus')"></asp:TextBox><br />
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvartno" runat="server" AutoCompleteType="Disabled"
                                                                        BackColor="Transparent" BorderStyle="None" Font-Size="11px"
                                                                        Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "deldat")).ToString("dd-MMM-yyyy") %>'
                                                                        Width="80px"></asp:Label>
                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>
                                                           

                                                            <asp:TemplateField HeaderText="DOc Id" Visible="false">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvDocId" runat="server" Visible="false" Height="16px" Font-Size="11px"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "docno")) %>'
                                                                        Width="80px"></asp:Label>
                                                                    <asp:Label ID="lblSSIRcode" runat="server" Visible="false" Height="16px" Font-Size="11px"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "ssircode")) %>'
                                                                        Width="80px"></asp:Label>      
                                                                         <asp:Label ID="lblfstep" runat="server" Visible="false" Height="16px" Font-Size="11px"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "curstep")) %>'
                                                                        Width="80px"></asp:Label>  
                                                                         <asp:Label ID="lbltstep" runat="server" Visible="false" Height="16px" Font-Size="11px"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "tstep")) %>'
                                                                        Width="80px"></asp:Label>  

                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Currency" >
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvCurrency" runat="server" CssClass="text-center"  Height="16px" Font-Size="11px"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "currency")) %>'
                                                                        Width="50px"></asp:Label>
                                                                                                                                   

                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Order value">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="txtgvordval" runat="server" BackColor="Transparent"
                                                                        BorderStyle="None" Height="18px" Style="text-align: right" Font-Size="11px"
                                                                        Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ordval")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                                        Width="70px"></asp:Label>
                                                                </ItemTemplate>

                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                                <ItemStyle HorizontalAlign="Right" />
                                                                <FooterStyle HorizontalAlign="Right" />
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Remarks">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvNotes" runat="server" CssClass="text-purple"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "remarks")) %>'
                                                                        Width="80px"></asp:Label>
                                                                                                                                   

                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>
                                                              <asp:TemplateField HeaderText="Approve On">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvPostedOn" runat="server" ToolTip='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "lstcomments")) %>' CssClass="font-italic small text-right text-danger"  Height="16px" Font-Size="11px"
                                                                        Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "lstrowdat")).ToString("dd-MMM-yyyy hh:mm tt") %>'
                                                                        Width="120px"></asp:Label>
                                                                                                                                   

                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>
                                                           <asp:TemplateField HeaderText="Approve By" >
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblgvUser" ToolTip='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "lstcomments")) %>' runat="server" CssClass="text-center"  Height="16px" Font-Size="11px"
                                                                        Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "lstusrname")) %>'
                                                                        Width="100px"></asp:Label>
                                                                                                                                   

                                                                </ItemTemplate>
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>
                                                           <asp:TemplateField HeaderText="Action">
                                                                <ItemTemplate>
                                                                    <div class="dropdown">
                                                                        <button class="btn btn-primary btn-xs dropdown-toggle" type="button" data-toggle="dropdown">
                                                                            Action
                                                                             
                                                                        </button>
                                                                        <ul class="dropdown-menu dropdown-menu-right">                                                                          
                                                                         
                                                                            <li>
                                                                                <asp:HyperLink ID="HypActivity" NavigateUrl='<%# "~/F_33_Doc/ShowAllDoc?Type=Rpt&genno="+SPELIB.Common.Base64Encode(Convert.ToString(DataBinder.Eval(Container.DataItem, "docno"))) %>' runat="server" Target="_blank" ForeColor="Blue" Font-Underline="false"> &nbsp; <span class="fa fa-list"></span> Activity Log</asp:HyperLink>

                                                                             </li>
                                                                            <li>
                                                                                <asp:HyperLink ID="HypLPrint" NavigateUrl='<%# "~/F_33_Doc/Print?Type=AppPrint&docno="+Convert.ToString(DataBinder.Eval(Container.DataItem, "docno")) %>' runat="server" Target="_blank" ForeColor="Blue" Font-Underline="false"> &nbsp; <span class="fa fa-print"></span> Details Information</asp:HyperLink>

                                                                             </li>
                                                                             </ul>
                                                                    </div>


                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                       
                                                     

                                                        </Columns>


                                                        <PagerStyle CssClass="gvPagination" />
                                                        <HeaderStyle CssClass="grvHeader" />
                                                        <FooterStyle CssClass="grvFooter" />
                                                        <RowStyle CssClass="grvRows" />

                                                    </asp:GridView>

                                                </div>
                                            </div>

                                        </asp:Panel>

                                                                      
                                   
                                    </div>
                                </div>

                            </div>
                        </asp:Panel>


                        <asp:Panel ID="pnlReprots" runat="server">

                           
                        </asp:Panel>


                      

                    </div>



                </div>
            </div>

          <div class="modal modal-drawer fade has-shown" id="exampleModalDrawerRight" tabindex="-1" role="dialog" aria-labelledby="exampleModalDrawerRightLabel" style="display: none;" aria-hidden="true">
            <!-- .modal-dialog -->
            <div class="modal-dialog modal-lg modal-drawer-right" role="document">
                <!-- .modal-content -->
                <div class="modal-content">
                    <!-- .modal-header -->
                    <div class="modal-header modal-body-scrolled">
                        <h5 id="exampleModalDrawerRightLabel" class="modal-title">Document Inforation</h5>
                    </div>
                    <!-- /.modal-header -->
                    <!-- .modal-body -->
                    <div class="modal-body">
                     <div class="card card-fluid">
                   <!-- .card-header -->
                    <header class="card-header">
                      <!-- .nav-tabs -->
                      <ul class="nav nav-tabs card-header-tabs" id="FileTabs" runat="server">
                      
                       
                      </ul>
                      <!-- /.nav-tabs -->
                    </header>
                    <!-- /.card-header -->
                    <!-- .card-body -->
                    <div class="card-body" runat="server">
                      <!-- .tab-content -->
                     <div id="myTabContent" runat="server" class="tab-content">
                      
                      </div>
                      <!-- /.tab-content -->
                    </div>
                    <!-- /.card-body -->
                  </div>
                    </div>
                    <!-- /.modal-body -->
                    <!-- .modal-footer -->
                    <div class="modal-footer modal-body-scrolled">
                        <button type="button" class="btn btn-light" data-dismiss="modal">Close</button>
                    </div>
                    <!-- /.modal-footer -->
                </div>
                <!-- /.modal-content -->
            </div>
            <!-- /.modal-dialog -->
        </div>
          </div>

     <%--   </ContentTemplate>
    </asp:UpdatePanel>--%>


    <%-- <Triggers>

<asp:AsyncPostBackTrigger ControlID="btn_refresh" EventName="Click"></asp:AsyncPostBackTrigger>

</Triggers>--%>

 

</asp:Content>

