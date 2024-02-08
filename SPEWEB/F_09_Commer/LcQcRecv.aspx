<%@ Page Title="" Language="C#" MasterPageFile="~/SPE.Master" AutoEventWireup="true" CodeBehind="LcQcRecv.aspx.cs" Inherits="SPEWEB.F_09_Commer.LcQcRecv" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style>
        #qcmodal .modal-dialog {
            max-width: 60% !important;
        }

        .chzn-single {
            border-radius: 3px !important;
            height: 29px !important;
        }

        .switch {
            position: relative;
            display: inline-block;
            width: 56px;
            height: 30px;
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
                height: 22px;
                width: 22px;
                left: 4px;
                bottom: 4px;
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
            -webkit-transform: translateX(26px);
            -ms-transform: translateX(26px);
            transform: translateX(26px);
        }

        /* Rounded sliders */
        .slider.round {
            border-radius: 34px;
        }

            .slider.round:before {
                border-radius: 50%;
            }
    </style>
    <script language="javascript" type="text/javascript">
        function OpenQCModal() {

            $('#qcmodal').modal('show');
        }
        function CLoseMOdal() {
            $('#qcmodal').modal('hide');

        }
        $(document).ready(function () {
            //For navigating using left and right arrow of the keyboard
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);
        });
        function pageLoaded() {
            $('.chzn-select').chosen({ search_contains: true });
        };

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
                        <div class="col-md-2">
                            <div class="form-group">
                                <asp:Label ID="lblLcno" runat="server" CssClass="label">SelectL/C </asp:Label>

                                <asp:DropDownList ID="ddlLcCode" runat="server" OnSelectedIndexChanged="ddlLcCode_SelectedIndexChanged" CssClass="form-control form-control-sm chzn-select" TabIndex="6" AutoPostBack="true">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-2">
                            <div class="form-group">
                                <asp:Label ID="Label1" runat="server" CssClass="label">Rec Number</asp:Label>

                                <asp:DropDownList ID="ddlrcvno" runat="server" CssClass="form-control form-control-sm chzn-select" TabIndex="6" AutoPostBack="true">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-1">
                            <div class="form-group" style="margin-top: 20px;">
                                <asp:LinkButton ID="LbtnOk" runat="server" CssClass="btn btn-primary btn-sm" OnClick="LbtnOk_Click">Ok</asp:LinkButton>
                                        <asp:LinkButton ID="LbtnReqItemShow" OnClick="LbtnReqItemShow_Click" runat="server"  CssClass="btn btn-sm btn-warning" Text="Expand"></asp:LinkButton>

                            </div>
                        </div>
                        <div class="col-md-1">
                            <div class="form-group">
                                <asp:Label ID="lblreceivedat" runat="server" CssClass="label"> QC Date:</asp:Label>
                                <asp:TextBox ID="txtreceivedate" runat="server" CssClass="form-control form-control-sm small" TabIndex="14"></asp:TextBox>
                                <cc1:CalendarExtender ID="CalExr2" runat="server" Format="dd-MMM-yyyy ddd" TargetControlID="txtreceivedate" />
                                <asp:TextBox ID="TxtQcDate" Visible="false" runat="server" CssClass=" form-control form-control-sm small" TabIndex="14"></asp:TextBox>
                                <cc1:CalendarExtender ID="CalQcDate" runat="server" Format="dd-MMM-yyyy ddd" TargetControlID="TxtQcDate" />
                            </div>
                        </div>

                        <div class="col-md-2">
                            <div class="form-group">
                                <asp:Label ID="lblgrr" runat="server" CssClass="label">GRN No.</asp:Label>
                                <asp:TextBox ID="txtgrrno" runat="server" CssClass="form-control form-control-sm small" ReadOnly="True" TabIndex="14"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-1">
                            <div class="form-group">
                                <asp:Label ID="lblchnlno" runat="server" CssClass="label">Challan No.</asp:Label>
                                <asp:TextBox ID="txtchnlno" runat="server" CssClass=" form-control form-control-sm" ReadOnly="True" TabIndex="15"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-1 pr-0">
                            <div class="form-group">
                                <asp:Label ID="lblchnldate" runat="server" CssClass="label">Challan Date</asp:Label>
                                <asp:TextBox ID="txtchnldate" runat="server" CssClass=" form-control form-control-sm small" TabIndex="16"></asp:TextBox>
                                <cc1:CalendarExtender ID="ChnlDate" runat="server" Format="dd-MMM-yyyy ddd" TargetControlID="txtchnldate" />

                            </div>
                            <%--<div class="col-md-2">
                                        <asp:Label ID="lmsg" runat="server" CssClass="btn btn-danger primaryBtn"></asp:Label>
                                    </div>--%>
                            <%--<div class="col-md-3 pull-right">
                                        <div class="msgHandSt">--%>


                            <%--    <asp:UpdateProgress ID="UpdateProgress1" runat="server" DisplayAfter="50">
                                            <ProgressTemplate>
                                                <asp:Label ID="Labelpro" runat="server" CssClass="lblProgressBar"
                                                    Text="Please Wait.........."></asp:Label>

                                            </ProgressTemplate>
                                        </asp:UpdateProgress>--%>
                            <%--</div>--%>
                        </div>

                        <div class="col-md-2">
                            <div class="form-group">
                                <asp:Label ID="Label26" runat="server" CssClass="smLbl_to">Print Type</asp:Label>
                                <asp:DropDownList ID="ddlReportLevel" runat="server" CssClass="chzn-select form-control form-control-sm">
                                    <asp:ListItem Value="1">Leather/Non-Leather</asp:ListItem>
                                    <asp:ListItem Value="2">Label & Hang Tag</asp:ListItem>
                                    <asp:ListItem Value="3">Outsole</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>

                        <div class="col-md-2">
                            <div class="form-group" style="display: none;">
                                <asp:LinkButton ID="imgbtnPreGrn" runat="server" CssClass="label" OnClick="imgbtnPreGrn_Click" TabIndex="15">Pre.<span class="fa fa-search"> </span></asp:LinkButton>
                                <asp:DropDownList ID="ddlPreGrn" runat="server" AutoPostBack="True" CssClass="form-control form-control chzn-select" TabIndex="16">
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                </div>
            </div>


            <div class="card card-fluid">
                <div class="card-body" style="min-height: 450px">
                    <div class="row mb-2">
                        <asp:GridView ID="gvRecItem" runat="server"  Visible="false"
                                AutoGenerateColumns="False"  OnRowDataBound="gvRecItem_RowDataBound"
                                ShowFooter="True" CssClass="table-striped table-hover table-bordered grvContentarea">
                                <PagerSettings Position="Top" />
                                <RowStyle />
                                <Columns>
                                    <asp:TemplateField HeaderText="Sl">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvSl" runat="server" Font-Bold="True" Height="16px"
                                                Style="text-align: right"
                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField> 
                                    <asp:TemplateField HeaderText="Description">
                                        <ItemTemplate>
                                            <asp:Label ID="ItemDesc" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "resdesc")) %>'
                                                Width="200px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:LinkButton ID="LbtnRecItemCalculate" OnClick="LbtnRecItemCalculate_Click" runat="server" CssClass="btn btn-xs btn-success">Adjust <span class="fa fa-repeat"></span></asp:LinkButton>
                                        </FooterTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField> 
                                    <asp:TemplateField HeaderText="Specification ">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvSSpecification" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "spcfdesc")) %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="left" Width="120px" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderText="Size ">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvSSize" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "size")) %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="left" Width="80px" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderText="Color ">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvSColor" runat="server"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "color")) %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="left" Width="60px" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="UOM">
                                        <ItemTemplate>
                                            <asp:TextBox ID="lblgvQcUnit" runat="server" Width="60px" CssClass="form-control"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "unit")) %>'></asp:TextBox>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="left" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                       <asp:TemplateField HeaderText="Checking Method">
                                            <ItemTemplate>
                                                <asp:DropDownList ID="ddlQccheckmethod" runat="server" CssClass="form-control" Width="200px">
                                                    <asp:ListItem>AQL</asp:ListItem>
                                                    <asp:ListItem>4 Point</asp:ListItem>
                                                    <asp:ListItem>Percentage</asp:ListItem>
                                                </asp:DropDownList>
                                            </ItemTemplate>
                                          <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        <ItemStyle HorizontalAlign="right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Recv Qty">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvSOrdqty" runat="server" Style="text-align: right"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "rcvqty")).ToString("#,##0.0000;(#,##0.0000); ") %>'
                                                Width="80px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        <ItemStyle HorizontalAlign="right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Qc Qty">
                                        <ItemTemplate>
                                            <asp:TextBox ID="lgvSRecBalqty" runat="server" Style="text-align: right" BorderStyle="None" CssClass="bg-twitter"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qcqty")).ToString("#,##0.0000;(#,##0.0000); ") %>'
                                                Width="80px"></asp:TextBox>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        <ItemStyle HorizontalAlign="right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Pass Qty">
                                        <ItemTemplate>
                                            <asp:TextBox ID="lgvISRecqty" runat="server" Style="text-align: right" BorderStyle="None" CssClass="bg-twitter"
                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "passqty")).ToString("#,##0.0000;(#,##0.0000); ") %>'
                                                Width="70px"></asp:TextBox>
                                        </ItemTemplate>
                                          <FooterTemplate>
                                        <asp:Label ID="lgvRSumRecqty" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#CC0066" Width="70px"
                                            Style="text-align: right"></asp:Label>
                                    </FooterTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        <ItemStyle HorizontalAlign="right" />
                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderText="Checked Details/Problematic">
                                        <ItemTemplate>
                                            <asp:TextBox ID="lblgvchckdetails" runat="server" TextMode="MultiLine" BorderStyle="None"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "chckdetails")) %>'></asp:TextBox>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="left" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField> 
                                     <asp:TemplateField HeaderText="Findings/Rejection">
                                        <ItemTemplate>
                                            <asp:TextBox ID="lblgvFindings" runat="server" TextMode="MultiLine" BorderStyle="None"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "finding")) %>'></asp:TextBox>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="left" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                      <asp:TemplateField HeaderText="Result" ControlStyle-Width="100px">
                                            <ItemTemplate>
                                                    <asp:DropDownList runat="server" ID="ddlQcStatus" CssClass="form-control">
                                                        <asp:ListItem Value="0" Text="Fail"></asp:ListItem>
                                                        <asp:ListItem Value="1" Text="Pass" Selected="True"></asp:ListItem>
                                                        <asp:ListItem Value="2" Text="Not Confirmed"></asp:ListItem>
                                                    </asp:DropDownList>
                                                <%--<label class="switch">
                                                    <asp:CheckBox ID="ChckStatus" runat="server" />
                                                    <span class="slider round"></span>
                                                </label>--%>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Remarks">
                                        <ItemTemplate>
                                            <asp:TextBox ID="lblgvQcRemarks" runat="server" TextMode="MultiLine" BorderStyle="None"
                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "remarks")) %>'></asp:TextBox>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="left" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                </Columns>
                                <FooterStyle CssClass="grvFooter" />
                                <EditRowStyle />
                                <AlternatingRowStyle />
                                <PagerStyle CssClass="gvPagination" />
                                <HeaderStyle CssClass="grvHeader" />
                            </asp:GridView>
                    </div>


                    <div class="table-responsive">

                        <asp:GridView ID="dgvReceive" runat="server" AllowPaging="false"
                            AutoGenerateColumns="False" ShowFooter="true"
                            CssClass="table-striped table-hover table-bordered grvContentarea" OnRowDataBound="dgvReceive_RowDataBound">
                            <PagerSettings NextPageText="Next" PreviousPageText="Previous" Position="Top"
                                Mode="NumericFirstLast" />

                            <Columns>
                                <asp:TemplateField HeaderText="SL" ItemStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvSlNo1" runat="server" Font-Bold="False" Font-Size="12px" Style="text-align: center"
                                            Text='<%# Convert.ToInt32(Container.DataItemIndex+1).ToString("00")+"." %>' Width="40px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Font-Bold="True" Font-Size="12px" ForeColor="#333333" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-Font-Size="12px" HeaderStyle-ForeColor="#333333" HeaderText="Res.Code"
                                    ItemStyle-HorizontalAlign="Left" Visible="true">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvResCode1" runat="server" Font-Size="11px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rescod")) %>'></asp:Label>

                                    </ItemTemplate>

                                    <HeaderStyle Font-Bold="True" Font-Size="12px" ForeColor="#333333" />
                                    <HeaderStyle Font-Size="12px" ForeColor="#333333" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Receive Date" HeaderStyle-Font-Size="12px" HeaderStyle-ForeColor="#333333">
                                    <ItemTemplate>
                                        <asp:Label ID="txtRcvdate" runat="server" BackColor="Transparent" BorderStyle="None"
                                            Width="80px" Font-Size="12px" Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "rcvdate")).ToString("dd-MMM-yyyy ") %>'></asp:Label>

                                    </ItemTemplate>
                                    <HeaderStyle Font-Size="12px" ForeColor="#333333" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Resource Description" HeaderStyle-Font-Size="12px"
                                    HeaderStyle-ForeColor="#333333" ItemStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvResdesc1" runat="server" Font-Size="10px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "resdesc")) %>'></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lbnTotal" Font-Bold="true" runat="server">Total</asp:Label>

                                    </FooterTemplate>
                                    <HeaderStyle Font-Bold="True" Font-Size="12px" ForeColor="#333333" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:TemplateField>
                                
                                <asp:TemplateField HeaderText="Color" HeaderStyle-Font-Size="12px" HeaderStyle-ForeColor="#333333" ItemStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvColor" runat="server" Font-Size="10px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "color")) %>'></asp:Label>
                                    </ItemTemplate>

                                    <HeaderStyle Font-Bold="True" Font-Size="12px" ForeColor="#333333" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:TemplateField>
                                
                                <asp:TemplateField HeaderText="Specification" HeaderStyle-Font-Size="12px"
                                    HeaderStyle-ForeColor="#333333" ItemStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvSpcdesc" runat="server" Font-Size="10px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "spcfdesc")) %>'></asp:Label>
                                    </ItemTemplate>

                                    <HeaderStyle Font-Bold="True" Font-Size="12px" ForeColor="#333333" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:TemplateField>
                                
                                <asp:TemplateField HeaderText="spcfcode" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvSpcode" runat="server" Font-Size="10px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "spcfcod")) %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-Font-Bold="true" HeaderStyle-Font-Size="12px" HeaderStyle-ForeColor="#333333"
                                    HeaderText="Unit">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgrvUnit1" runat="server" Font-Size="10px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "unit")) %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Font-Bold="True" Font-Size="12px" ForeColor="#333333" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Rec Qty">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvordqty" runat="server" Font-Size="12px" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "rcvqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lblgvFordqty" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#CC0066" Width="90px"
                                            Style="text-align: right"></asp:Label>
                                    </FooterTemplate>
                                    <FooterStyle Font-Bold="true" Font-Size="12px" ForeColor="#333333" />
                                    <HeaderStyle Font-Bold="True" Font-Size="12px" ForeColor="#333333" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="QC Upto Last" HeaderStyle-Font-Size="12px" HeaderStyle-ForeColor="#333333">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvreuptlast" runat="server" Font-Size="12px" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "preqcqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lblgvFreuptlast" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#CC0066" Width="90px"
                                            Style="text-align: right"></asp:Label>
                                    </FooterTemplate>
                                    <FooterStyle Font-Bold="true" Font-Size="12px" ForeColor="#333333" />
                                    <HeaderStyle Font-Size="12px" ForeColor="#333333" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Remaining QC" HeaderStyle-Font-Size="12px" HeaderStyle-ForeColor="#333333">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvrmainord" runat="server" Font-Size="12px" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "remqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="70px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lblgvFrmainord" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#CC0066" Width="90px"
                                            Style="text-align: right"></asp:Label>
                                    </FooterTemplate>
                                    <FooterStyle Font-Bold="true" Font-Size="12px" ForeColor="#333333" />
                                    <HeaderStyle Font-Size="12px" ForeColor="#333333" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="QC Qty" HeaderStyle-Font-Size="12px" HeaderStyle-ForeColor="#333333">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtgvrcvQty" runat="server" Font-Size="11px" BackColor="White" BorderStyle="Solid"
                                            Style="text-align: right" Width="70px" Font-Bold="False" BorderColor="#00CCFF"
                                            BorderWidth="1px" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qcqty")).ToString("#,##0.00;(#,##0.00); ") %>'></asp:TextBox>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lblgvFrcvQty" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#CC0066" Width="70px"
                                            Style="text-align: right"></asp:Label>
                                    </FooterTemplate>
                                    <FooterStyle Font-Bold="true" Font-Size="12px" ForeColor="#333333" />
                                    <HeaderStyle Font-Size="12px" ForeColor="#333333" />
                                </asp:TemplateField>
                                 <asp:TemplateField HeaderText="Lot No." HeaderStyle-Font-Size="12px" HeaderStyle-ForeColor="#333333">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgrvlotno" runat="server" Font-Size="11px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "lotno")) %>'></asp:Label>
                                    </ItemTemplate>
                                       <FooterTemplate>
                                            <asp:LinkButton ID="LbtnToClear" OnClick="LbtnToClear_Click" runat="server" CssClass=" btn btn-sm btn-warning text-white">Clear <span class="fa fa-recycle"></span></asp:LinkButton>
                                        </FooterTemplate>
                                    <HeaderStyle Font-Size="12px" ForeColor="#333333" />
                                </asp:TemplateField>
                              
                                
                                <asp:TemplateField HeaderText="Exp Date" HeaderStyle-Font-Size="12px" HeaderStyle-ForeColor="#333333">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtexpeirdate" runat="server" BackColor="Transparent" BorderStyle="None"
                                            Width="80px" Font-Size="12px" Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "expdate")).ToString("dd-MMM-yyyy ") %>'></asp:TextBox>
                                        <cc1:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd-MMM-yyyy"
                                            TargetControlID="txtexpeirdate"></cc1:CalendarExtender>
                                    </ItemTemplate>
                                    <HeaderStyle Font-Size="12px" ForeColor="#333333" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Remarks" HeaderStyle-Font-Size="12px" HeaderStyle-ForeColor="#333333" Visible="false">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtremarks" runat="server" BackColor="White" BorderStyle="Solid" BorderColor="#00CCFF"
                                            Width="150px" Font-Size="12px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "remarks")) %>'></asp:TextBox>

                                    </ItemTemplate>
                                    <HeaderStyle Font-Size="12px" ForeColor="#333333" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="BOM NO">
                                    <ItemTemplate>

                                        <asp:Label ID="lblGvBOMid" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "bomid")) %>'></asp:Label>


                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Rack No" Visible="false">
                                    <ItemTemplate>

                                        <asp:TextBox ID="txtgvRack" runat="server" Font-Size="11px" BackColor="White" BorderStyle="Solid"
                                            Style="text-align: left" Width="70px" Font-Bold="False" BorderColor="#00CCFF"
                                            BorderWidth="1px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rackno")) %>'></asp:TextBox>


                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Location" Visible="false">
                                    <ItemTemplate>
                                        <div class="form-group">
                                            <div class="col-md-6 pading5px">

                                                <asp:DropDownList ID="ddlval" runat="server" CssClass=" ddlPage62 inputTxt chzn-select" Width="150" TabIndex="2">
                                                </asp:DropDownList>



                                            </div>
                                        </div>
                                    </ItemTemplate>
                                    <%--<ItemTemplate>

                                                     <asp:TextBox ID="txtgvLoc" runat="server" Font-Size="11px" BackColor="White" BorderStyle="Solid"
                                                        Style="text-align: left" Width="70px" Font-Bold="False" BorderColor="#00CCFF"
                                                        BorderWidth="1px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "location")) %>'></asp:TextBox>

                                                   
                                                </ItemTemplate>--%>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Qc Update">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="LbtnQcUpdate" OnClick="LbtnQcUpdate_Click" CssClass="text-blue" runat="server"><span class="fa fa-edit"></span></asp:LinkButton>
                                    </ItemTemplate>
                                    <ItemStyle Width="100px" />
                                </asp:TemplateField>
                                  <asp:TemplateField HeaderText="PO No.">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgrvpono" runat="server" Font-Size="11px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "syspon")) %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Font-Size="12px" ForeColor="#333333" />
                                </asp:TemplateField>
                                 <asp:TemplateField HeaderText="PO No.">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgrvspono" runat="server" Font-Size="11px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pono")) %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Font-Size="12px" ForeColor="#333333" />
                                </asp:TemplateField>
                            </Columns>
                            <FooterStyle BackColor="#F5F5F5" />
                            <EditRowStyle />
                            <AlternatingRowStyle />
                            <PagerStyle CssClass="gvPagination" />
                            <HeaderStyle CssClass="grvHeader" />
                        </asp:GridView>

                    </div>
                </div>
            </div>

            <div id="qcmodal" class="modal fade bd-example-modal-lg" role="dialog">
                <div class="modal-dialog modal-lg">
                    <div class="modal-content">

                        <div class="modal-header">
                            <h4 class="modal-title"><span class="fa fa-table mr-2"></span>QC Details Information Update </h4>
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close">

                                <%--<i class="fa fa-close"></i>--%>
                                <span class="h2 text-red" aria-hidden="true">&times;</span>
                            </button>
                        </div>

                        <div class="modal-body">
                            <div class="table-responsive">

                                <asp:GridView ID="gvqcDeails" runat="server" AutoGenerateColumns="False" OnRowDataBound="gvqcDeails_RowDataBound"
                                    ShowFooter="True" CssClass=" table-striped table-hover table-bordered grvContentarea">
                                    <PagerSettings Visible="False" />

                                    <Columns>

                                        <asp:TemplateField HeaderText="SL">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvSlNo" runat="server" Height="16px" Style="text-align: right"
                                                    Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="25px"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Checking Method">
                                            <ItemTemplate>
                                                <asp:DropDownList ID="ddlcheckmethod" runat="server" CssClass="form-control" Width="200px">
                                                    <asp:ListItem>AQL</asp:ListItem>
                                                    <asp:ListItem>4 Point</asp:ListItem>
                                                    <asp:ListItem>Percentage</asp:ListItem>
                                                </asp:DropDownList>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Left" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Received Qty">
                                            <ItemTemplate>
                                                <asp:Label ID="lblgvRecvqty" Style="text-align: right" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "recvqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="75px"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="UOM">
                                            <ItemTemplate>
                                                <asp:TextBox BorderColor="#99CCFF" BorderStyle="Solid"
                                                    BorderWidth="1px" ID="lblgvUom" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "unit")) %>'
                                                    Width="50px"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Checked Qty.">
                                            <ItemTemplate>
                                                <asp:TextBox BorderColor="#99CCFF" BorderStyle="Solid"
                                                    BorderWidth="1px" ID="lblgvCckqty" Style="text-align: right" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qcqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="80px"></asp:TextBox>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Pass Qty">
                                            <ItemTemplate>
                                                <asp:TextBox BorderColor="#99CCFF" BorderStyle="Solid"
                                                    BorderWidth="1px" ID="lblgvpassqty" Style="text-align: right" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "passqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                    Width="80px"></asp:TextBox>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Checked Details/Problematic.">

                                            <ItemTemplate>
                                                <asp:TextBox TextMode="MultiLine" ID="txtgvCheckDetails" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                                    BorderWidth="1px" Font-Size="11px" Style="background-color: Transparent"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "chckdetails")) %>'
                                                    Width="130px"></asp:TextBox>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Findings/Rejection">
                                            <ItemTemplate>
                                                <asp:TextBox TextMode="MultiLine" ID="lblgvFindings" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                                    BorderWidth="1px" Font-Size="11px" Style="background-color: Transparent"
                                                    Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "finding")) %>'
                                                    Width="130px"></asp:TextBox>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="True" Font-Size="12px" HorizontalAlign="Right" />
                                            <ItemStyle HorizontalAlign="Right" />
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Result" ControlStyle-Width="100px">
                                            <ItemTemplate>
                                                    <asp:DropDownList runat="server" ID="ddlPassFail" CssClass="form-control form-control-sm">
                                                        <asp:ListItem Value="0" Text="Fail"></asp:ListItem>
                                                        <asp:ListItem Value="1" Text="Pass" Selected="True"></asp:ListItem>
                                                        <asp:ListItem Value="2" Text="Not Confirmed"></asp:ListItem>
                                                    </asp:DropDownList>
                                                <%--<label class="switch">
                                                    <asp:CheckBox ID="ChckStatus" runat="server" />
                                                    <span class="slider round"></span>
                                                </label>--%>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Remarks">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtgvRemarks" TextMode="MultiLine" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                                    BorderWidth="1px" Font-Size="11px" Style="background-color: Transparent"
                                                    Text='<%# DataBinder.Eval(Container.DataItem, "remarks").ToString() %>' Width="80px"></asp:TextBox>
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
                    <div class="modal-footer ">
                        <asp:LinkButton ID="LbtnUpdateQcDetails" runat="server" OnClick="LbtnUpdateQcDetails_Click" CssClass="btn btn-sm btn-success" OnClientClick="CLoseMOdal();"><span class="glyphicon glyphicon-save"></span> Update </asp:LinkButton>
                        <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                    </div>
                </div>
            </div>
            </div>


        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>


