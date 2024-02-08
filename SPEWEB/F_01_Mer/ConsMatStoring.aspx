<%@ Page Title="" Language="C#" MasterPageFile="~/SPE.Master" AutoEventWireup="true" CodeBehind="ConsMatStoring.aspx.cs" Inherits="SPEWEB.F_01_Mer.ConsMatStoring" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript" language="javascript">
        //$('body').delegate('#checkbox3', 'change', function () {
        //    // From the other examples
        //    if (!this.checked) {
        //        var sure = confirm("Are you sure?");
        //        this.checked = !sure;
        //        $('#checkbox3').val(sure.toString());
        //    }
        //});

        $(document).on('click', '#checkbox3', function () {
            if ($(this).is(":checked")) {
                //alert("ssf");

                $('#buyersegment').show();

                $('#submitsegment').show();


            }
            else {

                $('#buyersegment').hide();
                $('#submitsegment').hide();
            }
        });

        function CLoseMOdal() {
            $('#myModal').modal('hide');
        }
        $(document).ready(function () {

            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);

        });

        function pageLoaded() {
            $('.chzn-select').chosen({ search_contains: true });
        }
    </script>

    <style>
        .chzn-single {
            border-radius: 3px !important;
            height: 29px !important;
        }
    </style>
    <style>
        .funkyradio div {
            clear: both;
            overflow: hidden;
        }

        .funkyradio label {
            width: 100%;
            border-radius: 3px;
            border: 1px solid #D1D3D4;
            font-weight: normal;
        }

        .funkyradio input[type="radio"]:empty,
        .funkyradio input[type="checkbox"]:empty {
            display: none;
        }

            .funkyradio input[type="radio"]:empty ~ label,
            .funkyradio input[type="checkbox"]:empty ~ label {
                position: relative;
                line-height: 2em;
                text-indent: 3.25em;
                margin-top: 20px;
                cursor: pointer;
                -webkit-user-select: none;
                -moz-user-select: none;
                -ms-user-select: none;
                user-select: none;
            }

                .funkyradio input[type="radio"]:empty ~ label:before,
                .funkyradio input[type="checkbox"]:empty ~ label:before {
                    position: absolute;
                    display: block;
                    top: 0;
                    bottom: 0;
                    left: 0;
                    content: '';
                    width: 2.5em;
                    background: #D1D3D4;
                    border-radius: 3px 0 0 3px;
                }

        .funkyradio input[type="radio"]:hover:not(:checked) ~ label,
        .funkyradio input[type="checkbox"]:hover:not(:checked) ~ label {
            color: #888;
        }

            .funkyradio input[type="radio"]:hover:not(:checked) ~ label:before,
            .funkyradio input[type="checkbox"]:hover:not(:checked) ~ label:before {
                content: '\2714';
                text-indent: .9em;
                color: #C2C2C2;
            }

        .funkyradio input[type="radio"]:checked ~ label,
        .funkyradio input[type="checkbox"]:checked ~ label {
            color: #777;
        }

            .funkyradio input[type="radio"]:checked ~ label:before,
            .funkyradio input[type="checkbox"]:checked ~ label:before {
                content: '\2714';
                text-indent: .9em;
                color: #333;
                background-color: #ccc;
            }

        .funkyradio input[type="radio"]:focus ~ label:before,
        .funkyradio input[type="checkbox"]:focus ~ label:before {
            box-shadow: 0 0 0 3px #999;
        }

        .funkyradio-success input[type="radio"]:checked ~ label:before,
        .funkyradio-success input[type="checkbox"]:checked ~ label:before {
            color: #fff;
            background-color: #5cb85c;
        }
    </style>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="card card-fluid">
                <div class="card-body">
                    <div class="row">

                        <div class="col-md-2 ">
                            <div class="form-group">
                                <asp:Label ID="Label1" runat="server" CssClass="label">Date</asp:Label>
                                <asp:TextBox ID="txtDatefrom" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
                                <cc1:CalendarExtender ID="datefrom" runat="server" Format="dd-MMM-yyyy" TargetControlID="txtDatefrom"></cc1:CalendarExtender>
                            </div>
                        </div>
                        <div class="col-md-3 ">
                            <div class="form-group">
                                <asp:Label ID="LblBuyer" runat="server" CssClass="label">Client Name
                                </asp:Label>

                                <asp:DropDownList ID="ddlbuyer" CssClass="form-control form-control-sm chzn-select" runat="server"></asp:DropDownList>
                                <asp:Label ID="LblMatGroup" runat="server" CssClass="label" Visible="false">Select Material
                                </asp:Label>

                                <asp:DropDownList ID="DdlMatGrp" Visible="false" CssClass="form-control form-control-sm chzn-select" runat="server" AutoPostBack="true" OnSelectedIndexChanged="DdlMatGrp_SelectedIndexChanged"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-1 ">
                            <div class="form-group" style="margin-top: 20px">
                                <asp:LinkButton ID="lbtnOk" runat="server" Text="Ok" OnClick="lbtnOk_Click" CssClass="btn btn-primary btn-sm" TabIndex="4"></asp:LinkButton>
                            </div>
                        </div>
                        <div class="col-md-2">
                            <div class="form-group">
                                <div class="funkyradio" id="ImportChckbox" runat="server" visible="false">
                                    <div class="funkyradio-success">
                                        <input type="checkbox" name="checkbox" id="checkbox3" />
                                        <label for="checkbox3">Click For Import &#128522; </label>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-3 ">
                            <div class="form-group " id="buyersegment" style="display: none;">
                                <asp:Label ID="lblFrombuyer" runat="server" CssClass="label">From Buyer Name
                                </asp:Label>

                                <asp:DropDownList ID="DdlFromBuyer" CssClass="form-control form-control-sm " runat="server"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-1 ">
                            <div class="form-group " id="submitsegment" style="display: none; margin-top: 20px">
                                <asp:LinkButton ID="LbtnImportSubmit" runat="server" OnClick="LbtnImportSubmit_Click" Text="Submit" CssClass="btn btn-success btn-sm" TabIndex="4"></asp:LinkButton>
                            </div>
                        </div>
                    </div>
                    <div class="clearfix"></div>

                    <asp:Panel ID="analysispanel" runat="server" Visible="false">
                        <div class="row">
                            <div class="col-md-3">
                                <div class="form-group">
                                    <asp:Label ID="lblProcess" runat="server" CssClass="label" Text="DEPARTMENT"></asp:Label>

                                    <asp:DropDownList ID="ddlProcess" runat="server" CssClass="form-control form-control-sm chzn-select" AutoPostBack="true" OnSelectedIndexChanged="ddlProcess_SelectedIndexChanged">
                                    </asp:DropDownList>
                                    <asp:Label ID="LblGrpSpcf" runat="server" CssClass="label" Text="Specifications"></asp:Label>

                                    <asp:DropDownList ID="DdlSpcgrp" runat="server" CssClass="form-control form-control-sm chzn-select" AutoPostBack="true" OnSelectedIndexChanged="ddlProcess_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-md-2">
                                <div class="form-group">
                                    <asp:LinkButton ID="LbtnComponent" CssClass="label" runat="server" OnClick="LbtnComponent_Click" Text="COMPONENT NAME"></asp:LinkButton>

                                    <asp:DropDownList ID="ddlComponent" runat="server" CssClass="form-control form-control-sm chzn-select" Width=""></asp:DropDownList>

                                </div>
                            </div>
                            <div class="col-md-6" id="PanelRes" runat="server">
                                <div class="form-group">
                                    <asp:LinkButton ID="lblProcess0" runat="server" CssClass="col-md-2 smLbl_to" OnClick="imgbtnResourceCost_Click" Text="MATERIALS NAME"></asp:LinkButton>
                                    <asp:DropDownList ID="ddlResourcesCost" runat="server" CssClass="form-control chzn-select">
                                    </asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-md-6" id="Panelresspe" runat="server">
                                <div class="row">
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <asp:LinkButton ID="LinkButton1" runat="server" CssClass="label" Text="MATERIALS NAME"></asp:LinkButton>
                                            <asp:DropDownList ID="DdlMatecode" runat="server" AutoPostBack="true" OnSelectedIndexChanged="DdlMatecode_SelectedIndexChanged" CssClass="form-control chzn-select">
                                            </asp:DropDownList>
                                        </div>
                                    </div>

                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <asp:Label ID="LblSpecfications" runat="server" CssClass="label" Text="Specifications"></asp:Label>
                                            <asp:DropDownList ID="DdlSpcfcod" runat="server" CssClass="form-control chzn-select">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <div class="col-md-1">
                                <div class="form-group" style="margin-top: 20px">
                                    <asp:LinkButton ID="lnkAddResouctCost" runat="server" Text="Add Table" OnClick="lnkAddResouctCost_Click" CssClass="btn btn-primary btn-sm " TabIndex="1">Add</asp:LinkButton>
                                </div>
                            </div>
                        </div>

                    </asp:Panel>
                </div>
            </div>
            <div class="card card-fluid">
                <div class="card-body" style="min-height: 400px">
                    <div class="row">

                        <asp:GridView ID="gvCost" runat="server" AutoGenerateColumns="False" ShowFooter="True" CssClass="table-striped table-hover table-bordered grvContentarea"
                            Width="479px" OnRowDeleting="gvCost_RowDeleting">
                            <Columns>
                                <asp:TemplateField HeaderStyle-Font-Bold="true" HeaderStyle-Font-Size="12px" HeaderStyle-ForeColor="#333333"
                                    HeaderText="SL" ItemStyle-Font-Size="12px">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvsl0" runat="server" Text='<%# Convert.ToInt32(Container.DataItemIndex+1).ToString("00")+"." %>'
                                            Width="30px" Style="text-align: left"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Font-Bold="True" Font-Size="12px" ForeColor="#333333" />
                                    <ItemStyle Font-Size="12px" />
                                </asp:TemplateField>

                                <asp:CommandField ShowDeleteButton="True" ControlStyle-CssClass="text-red" DeleteText="<span class='fa fa-trash'></span>" />

                                <asp:TemplateField HeaderStyle-Font-Bold="true" HeaderStyle-Font-Size="10px" HeaderStyle-ForeColor="#333333"
                                    HeaderText="DEPARTMENT NAME" ItemStyle-Font-Size="10px" ItemStyle-HorizontalAlign="Left">
                                    <HeaderTemplate>
                                        <table>

                                            <tr>
                                                <th class="">DEPARTMENT NAME                                                              
                                                </th>
                                                <th class="pull-right">
                                                    <asp:HyperLink ID="hlbtnRdataExel" runat="server" BackColor="#000066" ToolTip="Export Excel"
                                                        BorderColor="White" BorderStyle="Solid" BorderWidth="1px" Font-Bold="True"
                                                        ForeColor="White" Style="text-align: center; margin-left: 10px;" Width="20px"><span class="fa fa-file-excel"></span></asp:HyperLink>
                                                </th>
                                            </tr>
                                        </table>
                                    </HeaderTemplate>
                                    <ItemTemplate>

                                        <asp:Label ID="lblgvdept" runat="server" BorderStyle="None" Style="text-align: left" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "prodesc")) %>'
                                            Width="150px"></asp:Label>

                                    </ItemTemplate>
                                    <HeaderStyle Font-Bold="True" Font-Size="10px" ForeColor="#333333" />
                                    <ItemStyle Font-Size="10px" />

                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-Font-Bold="true" HeaderStyle-Font-Size="10px" HeaderStyle-ForeColor="#333333"
                                    HeaderText="COMPONENT NAME" ItemStyle-Font-Size="10px">

                                    <ItemTemplate>
                                        <asp:Label ID="lblgvcompcode" runat="server" Visible="false" BorderStyle="None" Style="text-align: left" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "compcode")) %>'
                                            Width="0px"></asp:Label>
                                        <asp:Label ID="lblgvCompnent" runat="server" BorderStyle="None" Style="text-align: left" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "compname")) %>'
                                            Width="150px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Font-Bold="True" Font-Size="10px" ForeColor="#333333" />
                                    <ItemStyle Font-Size="10px" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderStyle-Font-Bold="true" HeaderStyle-Font-Size="10px" HeaderStyle-ForeColor="#333333"
                                    HeaderText="MATERIALS CODE NO" ItemStyle-Font-Size="10px" ItemStyle-HorizontalAlign="Left">

                                    <ItemTemplate>

                                        <asp:Label ID="lblgvDesc" runat="server" BorderStyle="None" Style="text-align: left" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "resdesc")) %>'
                                            Width="150px"></asp:Label>

                                    </ItemTemplate>
                                    <HeaderStyle Font-Bold="True" Font-Size="10px" ForeColor="#333333" />
                                    <ItemStyle Font-Size="10px" />
                                    <FooterStyle HorizontalAlign="left" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-Font-Bold="true" HeaderStyle-Font-Size="10px" HeaderStyle-ForeColor="#333333"
                                    HeaderText="Part No" Visible="false" ItemStyle-Font-Size="10px">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvcodeCost" runat="server" Style="text-align: left" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rescode")) %>'
                                            Width="60px"></asp:Label>

                                    </ItemTemplate>

                                    <HeaderStyle Font-Bold="True" Font-Size="10px" ForeColor="#333333" />
                                    <ItemStyle Font-Size="8px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-Font-Bold="true" HeaderStyle-Font-Size="10px" HeaderStyle-ForeColor="#333333"
                                    HeaderText="MATERIAL NAME" ItemStyle-Font-Size="10px">

                                    <ItemTemplate>
                                        <asp:Label ID="lblgvspcfcode" runat="server" Visible="false" BorderStyle="None" Style="text-align: left" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "spcfcode")) %>'
                                            Width="0px"></asp:Label>
                                        <asp:Label ID="lblgvspcfdesc" runat="server" BorderStyle="None" Style="text-align: left" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "spcfdesc")) %>'
                                            Width="250px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lbltoalf" runat="server">Total</asp:Label>
                                    </FooterTemplate>
                                    <HeaderStyle Font-Bold="True" Font-Size="10px" ForeColor="#333333" />
                                    <ItemStyle Font-Size="10px" />
                                </asp:TemplateField>




                                <asp:TemplateField HeaderStyle-Font-Bold="true" HeaderStyle-Font-Size="10px" HeaderStyle-ForeColor="#333333" ItemStyle-HorizontalAlign="Right"
                                    HeaderText="CONS/ PAIR">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtgvconqty" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                            BorderWidth="1px" CssClass="GridItmTextBoxRight"
                                            Font-Size="10px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "conqty")).ToString("#,##0.000000;(#,##0.000000); ") %>'
                                            Width="50px"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="rfvqty" runat="server" ControlToValidate="txtgvconqty" EnableClientScript="false" Display="Dynamic" ErrorMessage="QTY invalid" ForeColor="Red" SetFocusOnError="true" />

                                    </ItemTemplate>

                                    <HeaderStyle Font-Bold="True" Font-Size="10px" ForeColor="#333333" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-Font-Bold="true" HeaderStyle-Font-Size="10px" HeaderStyle-ForeColor="#333333"
                                    HeaderText="UNIT" ItemStyle-Font-Size="10px">

                                    <ItemTemplate>
                                        <asp:Label ID="txtgvunit0" runat="server" BorderStyle="None" Style="text-align: left" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "resunit")) %>'
                                            Width="30px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Font-Bold="True" Font-Size="10px" ForeColor="#333333" />
                                    <ItemStyle Font-Size="10px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-Font-Bold="true" HeaderStyle-Font-Size="10px" HeaderStyle-ForeColor="#333333" ItemStyle-HorizontalAlign="Right"
                                    HeaderText="PROPOESD <br> WASTAGE %">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtgvwestpc" runat="server" BorderColor="#99CCFF" BorderStyle="Solid"
                                            BorderWidth="1px" CssClass="GridItmTextBoxRight"
                                            Font-Size="10px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "westpc")).ToString("#,##0.000;(#,##0.000); ") %>'
                                            Width="40px"></asp:TextBox>
                                    </ItemTemplate>

                                    <HeaderStyle Font-Bold="True" Font-Size="10px" ForeColor="#333333" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-Font-Bold="true" HeaderStyle-Font-Size="10px" HeaderStyle-ForeColor="#333333" ItemStyle-HorizontalAlign="Right"
                                    HeaderText="SUB TOTAL">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtgvqtyCost" runat="server" BorderStyle="None" CssClass="GridItmTextBoxRight"
                                            Font-Size="10px" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty")).ToString("#,##0.000000;(#,##0.000000); ") %>'
                                            Width="50px"></asp:TextBox>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lblgvttlqty" runat="server" Font-Bold="True" Font-Size="10px" ForeColor="#CC0066"
                                            Style="text-align: right" Width="60px"></asp:Label>
                                    </FooterTemplate>
                                    <HeaderStyle Font-Bold="True" Font-Size="10px" ForeColor="#333333" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-Font-Bold="true" HeaderStyle-Font-Size="12px" HeaderStyle-ForeColor="#333333"
                                    ItemStyle-Font-Size="10px" ItemStyle-HorizontalAlign="Right" HeaderText="PRICE/ UNIT">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtgvqrateCost" runat="server" BorderColor="#99CCFF" BorderWidth="1px" BorderStyle="Solid" Font-Size="12px" AutoPostBack="true" OnTextChanged="txtgvqrateCost_TextChanged"
                                            Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "rate")).ToString("#,##0.000000;(#,##0.000000); ") %>'
                                            Width="50px"></asp:TextBox>
                                    </ItemTemplate>

                                    <HeaderStyle Font-Bold="True" Font-Size="10px" ForeColor="#333333" />

                                    <ItemStyle Font-Size="10px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="TOTAL <br> IN USD">
                                    <ItemTemplate>

                                        <asp:TextBox ID="txtgvamtCost" runat="server" BorderStyle="None" Font-Size="10px"
                                            Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt")).ToString("#,##0.000;(#,##0.000); ") %>'
                                            Width="50px"></asp:TextBox>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lblgvfamtCost" runat="server" Font-Bold="True" Font-Size="10px" ForeColor="#CC0066"
                                            Style="text-align: right" Width="60px"></asp:Label>
                                    </FooterTemplate>
                                    <HeaderStyle Font-Bold="True" Font-Size="10px" ForeColor="#333333" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="BDT Amount" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgBdtamt" runat="server" Font-Size="12px"
                                            ItemStyle-Font-Size="12px" Style="text-align: right"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "convrate")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="80px"></asp:Label>

                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lblgvBdamtCost" runat="server" Font-Bold="True" Font-Size="10px" ForeColor="#CC0066"
                                            Style="text-align: right" Width="60px"></asp:Label>
                                    </FooterTemplate>
                                    <HeaderStyle Font-Bold="True" Font-Size="10px" ForeColor="#333333" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="%" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvpercnt" runat="server" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "percnt")).ToString("#,##0.00;(#,##0.00); ")+(Convert.ToDouble((DataBinder.Eval(Container.DataItem, "percnt")))>0?"%":"")%>'
                                            Width="40px" Style="text-align: right" ItemStyle-Font-Size="10px" Font-Size="10px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lblgvfper" runat="server" Font-Bold="True" Font-Size="10px" ForeColor="#CC0066"
                                            Width="40px" Style="text-align: right"></asp:Label>
                                    </FooterTemplate>
                                    <HeaderStyle Font-Bold="True" Font-Size="10px" ForeColor="#333333" />
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

        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>
