<%@ Page Title="" Language="C#" MasterPageFile="~/SPE.Master" AutoEventWireup="true" CodeBehind="LcReceive.aspx.cs" Inherits="SPEWEB.F_09_Commer.LcReceive" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script language="javascript" type="text/javascript">
        $(document).ready(function () {
            //For navigating using left and right arrow of the keyboard
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);


        });

        function Search_Gridview(strKey, cellNr, gvName) {
            //alert(cellNr);
            var tblData;

            var strData = strKey.value.toLowerCase().split(" ");

            switch (gvName) {
                case "dgvReceive":
                    tblData = document.getElementById("<%=dgvReceive.ClientID %>");
                    break;
                case "gvRecItem":
                    tblData = document.getElementById("<%=gvRecItem.ClientID %>");
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
            $('.chzn-select').chosen({ search_contains: true });
        };

    </script>
    <style>
        .chzn-single {
            border-radius: 3px !important;
            height: 29px !important;
        }
    </style>

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

                        <div class="col-md-3">
                            <div class="form-group">
                                <asp:Label ID="lblLcno" runat="server" CssClass="label">L/C Number</asp:Label>

                                <asp:DropDownList ID="ddlLcCode" runat="server" CssClass="form-control form-control-sm chzn-select" TabIndex="6" AutoPostBack="true" OnSelectedIndexChanged="ddlLcCode_SelectedIndexChanged">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-1">
                            <div class="form-group" style="margin-top: 20px">
                                <asp:LinkButton ID="LbtnOk" runat="server" CssClass="btn btn-primary btn-sm" OnClick="LbtnOk_Click">Ok</asp:LinkButton>
                            </div>
                        </div>

                        <div class="col-md-2">
                            <div class="form-group">
                                <asp:Label ID="lblreceivedat" runat="server" CssClass="label">Receive Date:</asp:Label>
                                <asp:TextBox ID="txtreceivedate" runat="server" CssClass=" form-control form-control-sm" TabIndex="14"></asp:TextBox>
                                <cc1:CalendarExtender ID="CalExr2" runat="server" Format="dd-MMM-yyyy ddd" TargetControlID="txtreceivedate" />
                            </div>
                        </div>
                        <div class="col-md-2">
                            <div class="form-group">
                                <asp:Label ID="lblgrr" runat="server" CssClass="label">LRC No.</asp:Label>
                                <asp:TextBox ID="txtgrrno" runat="server" CssClass=" form-control form-control-sm" ReadOnly="True" TabIndex="14"></asp:TextBox>

                            </div>
                        </div>
                        <div class="col-md-1">
                            <div class="form-group">
                                <asp:Label ID="lblResList1" runat="server" CssClass="label" Text="Chalan No:"></asp:Label>
                                <asp:TextBox ID="txtChalanNo" runat="server" CssClass=" form-control form-control-sm" TabIndex="1"></asp:TextBox>

                            </div>
                        </div>
                        <div class="col-md-2">
                            <div class="form-group">
                                <asp:Label ID="Label1" runat="server" CssClass="label" Text="Challan Date"></asp:Label>

                                <asp:TextBox ID="txtChlDate" runat="server" CssClass="form-control form-control-sm" TabIndex="5" ToolTip="(dd-MMM-yyyy)"></asp:TextBox>
                                <cc1:CalendarExtender ID="CalendarExtender1" runat="server"
                                    Format="dd-MMM-yyyy" TargetControlID="txtChlDate"></cc1:CalendarExtender>
                            </div>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-md-3">

                            <div class="form-group">
                                <asp:Label ID="lblstorid" runat="server" CssClass="label">Store Id:</asp:Label>

                                <asp:DropDownList ID="ddlStorid" runat="server" CssClass="form-control form-control-sm chzn-select" TabIndex="16">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-1">
                            <div class="form-group" style="margin-top: 20px">
                                <asp:LinkButton ID="lnkReceive" runat="server" CssClass="btn btn-primary  btn-sm" OnClick="lnkReceive_Click">Receive</asp:LinkButton>
                            </div>
                        </div>
                        <div class="form-group col-md-2">
                            <asp:LinkButton ID="LbtnReqItemShow" OnClick="LbtnReqItemShow_Click" runat="server" Style="margin-top: 20px;" CssClass="btn btn-sm btn-warning" Text="Item Expand"></asp:LinkButton>
                        </div>
                        <div class="col-md-5">
                            <div class="form-group" style="display: none">

                                <asp:Label ID="lblPreGrn" runat="server" CssClass="lblTxt lblName">Pre GRN:</asp:Label>
                                <asp:TextBox ID="txtsrGrn" runat="server" CssClass=" inputtextbox" TabIndex="14"></asp:TextBox>

                                <asp:LinkButton ID="imgbtnPreGrn" runat="server" CssClass="btn btn-primary srearchBtn" OnClick="imgbtnPreGrn_Click" TabIndex="15"><span class="glyphicon glyphicon-search asitGlyp"> </span></asp:LinkButton>



                                <asp:DropDownList ID="ddlPreGrn" runat="server" AutoPostBack="True" CssClass="form-control inputTxt chzn-select" TabIndex="16">
                                </asp:DropDownList>
                            </div>
                        </div>



                    </div>
                </div>
            </div>
            <div class="card card-fluid">
                <div class="card-body" style="min-height: 580px;">
                    <div class="row mb-2">
                        <asp:GridView ID="gvRecItem" runat="server" Visible="false"
                            AutoGenerateColumns="False"
                            ShowFooter="True" CssClass="table-striped table-hover table-bordered grvContentarea">
                            <PagerSettings Position="Top" />
                            <RowStyle />
                            <Columns>
                                <asp:TemplateField HeaderText="SL">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvSl" runat="server" Font-Bold="True" Height="16px"
                                            Style="text-align: right"
                                            Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="">
                                    <HeaderTemplate>
                                        <asp:TextBox ID="txtSearchDesc" BackColor="Transparent" BorderStyle="None" runat="server" CssClass="text-center" placeholder="Description" onkeyup="Search_Gridview(this, 1, 'gvRecItem')"></asp:TextBox><br />
                                    </HeaderTemplate>
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

                                <asp:TemplateField HeaderText=" ">
                                    <HeaderTemplate>
                                        <asp:TextBox ID="txtSearchSpcf" BackColor="Transparent" BorderStyle="None" runat="server" CssClass="text-center" placeholder="Specification" onkeyup="Search_Gridview(this, 2, 'gvRecItem')"></asp:TextBox><br />
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvSSpecification" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "spcfdesc")) %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="left" Width="120px" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="">
                                    <HeaderTemplate>
                                        <asp:TextBox ID="txtSearchSz" BackColor="Transparent" BorderStyle="None" runat="server" CssClass="text-center" placeholder="Size" onkeyup="Search_Gridview(this, 3, 'gvRecItem')"></asp:TextBox><br />
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvSSize" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "size")) %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="left" Width="80px" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="">
                                    <HeaderTemplate>
                                        <asp:TextBox ID="txtSearchClr" BackColor="Transparent" BorderStyle="None" runat="server" CssClass="text-center" placeholder="Color" onkeyup="Search_Gridview(this, 4, 'gvRecItem')"></asp:TextBox><br />
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvSColor" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "color")) %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" Width="60px" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Unit ">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvUnit" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "unit")) %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Ord. Qty">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvSOrdqty" runat="server" Style="text-align: right"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ordrqty")).ToString("#,##0.0000;(#,##0.0000); ") %>'
                                            Width="90px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    <ItemStyle HorizontalAlign="right" />
                                    <FooterTemplate>
                                        <asp:Label runat="server" ID="gvLblTtl1" Font-Bold="True" Style="text-align: right" Font-Size="11px"></asp:Label>
                                    </FooterTemplate>
                                    <FooterStyle HorizontalAlign="Right" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Rec.Bal.Qty">
                                    <ItemTemplate>
                                        <asp:Label ID="lgvSRecBalqty" runat="server" Style="text-align: right"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "remainordr")).ToString("#,##0.0000;(#,##0.0000); ") %>'
                                            Width="90px"></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    <ItemStyle HorizontalAlign="right" />
                                    <FooterTemplate>
                                        <asp:Label runat="server" ID="gvLblTtl2" Font-Bold="True" Style="text-align: right" Font-Size="11px"></asp:Label>
                                    </FooterTemplate>
                                    <FooterStyle HorizontalAlign="Right" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Rec Qty">
                                    <ItemTemplate>
                                        <asp:TextBox ID="lgvISRecqty" runat="server" Style="text-align: right"
                                            Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "rcvqty")).ToString("#,##0.0000;(#,##0.0000); ") %>'
                                            Width="70px"></asp:TextBox>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lgvRSumRecqty" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#CC0066" Width="70px"
                                            Style="text-align: right"></asp:Label>
                                    </FooterTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    <ItemStyle HorizontalAlign="right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Bal. Qty">
                                    <ItemTemplate>
                                        <asp:TextBox ID="lgvISBalqty" runat="server" Style="text-align: right"
                                            Text='<%# DataBinder.Eval(Container.DataItem, "balqty") %>'
                                            Width="70px"></asp:TextBox>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:LinkButton ID="lgvISBalqtyCalculate" OnClick="lgvISBalqtyCalculate" runat="server" CssClass="btn btn-xs btn-success">Bal.Adjust <span class="fa fa-repeat"></span></asp:LinkButton>
                                    </FooterTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    <ItemStyle HorizontalAlign="right" />
                                </asp:TemplateField>

                            </Columns>
                            <FooterStyle CssClass="grvFooter" />
                            <EditRowStyle />
                            <AlternatingRowStyle />
                            <PagerStyle CssClass="gvPagination" />
                            <HeaderStyle CssClass="grvHeader" />
                        </asp:GridView>
                    </div>
                    <div class="row">

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

                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkCol" runat="server" />
                                    </ItemTemplate>
                                    <HeaderTemplate>
                                        <asp:CheckBox ID="chkhead" AutoPostBack="true" OnCheckedChanged="chkheadl_CheckedChanged" runat="server" />
                                    </HeaderTemplate>

                                    <HeaderStyle Font-Bold="True" Font-Size="12px" />
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

                                <asp:TemplateField HeaderText="Resource Description" HeaderStyle-Font-Size="12px" HeaderStyle-ForeColor="#333333" ItemStyle-HorizontalAlign="Left">
                                    <HeaderTemplate>
                                        <asp:TextBox ID="txtSearchRscDesc" BackColor="Transparent" BorderStyle="None" runat="server" Width="90%" CssClass="text-center"
                                            placeholder="Resource Description" onkeyup="Search_Gridview(this, 3, 'dgvReceive')"></asp:TextBox><br />
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvResdesc1" runat="server" Font-Size="10px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "resdesc")) %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Font-Bold="True" Font-Size="12px" ForeColor="#333333" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Specification" HeaderStyle-Font-Size="12px" HeaderStyle-ForeColor="#333333" ItemStyle-HorizontalAlign="Left">

                                    <HeaderTemplate>
                                        <asp:TextBox ID="txtSrchSpcf" BackColor="Transparent" BorderStyle="None" runat="server" Width="90%" CssClass="text-center"
                                            placeholder="Specification" onkeyup="Search_Gridview(this, 4, 'dgvReceive')"></asp:TextBox>
                                    </HeaderTemplate>

                                    <ItemTemplate>
                                        <asp:Label ID="lblgvSpcdesc" runat="server" Font-Size="10px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "spcfdesc")) %>'></asp:Label>
                                    </ItemTemplate>

                                    <HeaderStyle Font-Bold="True" Font-Size="12px" ForeColor="#333333" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Size" HeaderStyle-Font-Size="12px" HeaderStyle-ForeColor="#333333" ItemStyle-HorizontalAlign="Left">

                                    <HeaderTemplate>
                                        <asp:TextBox BackColor="Transparent" BorderStyle="None" runat="server" Width="90%" CssClass="text-center"
                                            placeholder="Size" onkeyup="Search_Gridview(this, 5, 'dgvReceive')"></asp:TextBox>
                                    </HeaderTemplate>

                                    <ItemTemplate>
                                        <asp:Label ID="lblgvSizeDesc" runat="server" Font-Size="10px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "size")) %>'></asp:Label>
                                    </ItemTemplate>

                                    <HeaderStyle Font-Bold="True" Font-Size="12px" ForeColor="#333333" />
                                    <ItemStyle HorizontalAlign="Left" />

                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Color" HeaderStyle-Font-Size="12px" HeaderStyle-ForeColor="#333333" ItemStyle-HorizontalAlign="Left">

                                    <HeaderTemplate>
                                        <asp:TextBox BackColor="Transparent" BorderStyle="None" runat="server" Width="90%" CssClass="text-center"
                                            placeholder="Color" onkeyup="Search_Gridview(this, 6, 'dgvReceive')"></asp:TextBox>
                                    </HeaderTemplate>

                                    <ItemTemplate>
                                        <asp:Label ID="lblgvColorDesc" runat="server" Font-Size="10px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "color")) %>'></asp:Label>
                                    </ItemTemplate>

                                    <HeaderStyle Font-Bold="True" Font-Size="12px" ForeColor="#333333" />
                                    <ItemStyle HorizontalAlign="Left" />
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="BOM" HeaderStyle-Font-Size="12px"
                                    HeaderStyle-ForeColor="#333333" ItemStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblBOmId" runat="server" Font-Size="10px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "bomid")) %>'></asp:Label>
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
                                  <asp:TemplateField HeaderStyle-Font-Bold="true" HeaderStyle-Font-Size="12px" HeaderStyle-ForeColor="#333333"
                                    HeaderText="PO">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgrvpono" runat="server" Font-Size="10px" Width="90px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pono")) %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Font-Bold="True" Font-Size="12px" ForeColor="#333333" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Order Qty">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvordqty" runat="server" Font-Size="12px" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "ordrqty")).ToString("#,##0.00;(#,##0.00); ") %>'
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
                                <asp:TemplateField HeaderText="Free Qty">
                                    <ItemTemplate>
                                        <asp:Label ID="txtgrvFreeqty1" runat="server" AutoCompleteType="Disabled" BackColor="Transparent"
                                            BorderStyle="None" Font-Size="11px" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "freeqty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                            Width="90px"></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lblgrvFFreeqty1" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#CC0066" Width="90px"
                                            Style="text-align: right"></asp:Label>
                                    </FooterTemplate>
                                    <FooterStyle Font-Bold="true" Font-Size="12px" ForeColor="#333333" />
                                    <HeaderStyle Font-Bold="True" Font-Size="12px" ForeColor="#333333" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Rec. Upto Last" HeaderStyle-Font-Size="12px" HeaderStyle-ForeColor="#333333">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvreuptlast" runat="server" Font-Size="12px" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "rcvuptolast")).ToString("#,##0.00;(#,##0.00); ") %>'
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

                                <asp:TemplateField HeaderText="Remaining Order" HeaderStyle-Font-Size="12px" HeaderStyle-ForeColor="#333333">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgvrmainord" runat="server" Font-Size="12px" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "remainordr")).ToString("#,##0.00;(#,##0.00); ") %>'
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

                                <asp:TemplateField HeaderText="Arrival Qty" HeaderStyle-Font-Size="12px" HeaderStyle-ForeColor="#333333">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtgvrcvQty" runat="server" Font-Size="11px" BackColor="White" BorderStyle="Solid"
                                            Style="text-align: right" Width="70px" Font-Bold="False" BorderColor="#00CCFF"
                                            BorderWidth="1px" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "rcvqty")).ToString("#,##0.00;(#,##0.00); ") %>'></asp:TextBox>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lblgvFrcvQty" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#CC0066" Width="70px"
                                            Style="text-align: right"></asp:Label>
                                    </FooterTemplate>
                                    <FooterStyle Font-Bold="true" Font-Size="12px" ForeColor="#333333" />
                                    <HeaderStyle Font-Size="12px" ForeColor="#333333" />
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
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Lot No." HeaderStyle-Font-Size="12px" HeaderStyle-ForeColor="#333333">
                                    <ItemTemplate>
                                        <asp:Label ID="lblgrvlotno" runat="server" Font-Size="11px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "lotno")) %>'></asp:Label>
                                    </ItemTemplate>
                                    <HeaderStyle Font-Size="12px" ForeColor="#333333" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Expire Date" HeaderStyle-Font-Size="12px" HeaderStyle-ForeColor="#333333">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtexpeirdate" runat="server" BackColor="Transparent" BorderStyle="None"
                                            Width="80px" Font-Size="12px" Text='<%# Convert.ToDateTime(DataBinder.Eval(Container.DataItem, "expdate")).ToString("dd-MMM-yyyy ") %>'></asp:TextBox>
                                        <cc1:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd-MMM-yyyy"
                                            TargetControlID="txtexpeirdate"></cc1:CalendarExtender>
                                    </ItemTemplate>
                                    <HeaderStyle Font-Size="12px" ForeColor="#333333" />
                                </asp:TemplateField>
                                <asp:TemplateField Visible="false" HeaderText="Shipment Qty</br> (Invoice from</br> Shipper)" HeaderStyle-Font-Size="12px" HeaderStyle-ForeColor="#333333">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtgvShipQty" runat="server" Font-Size="11px" BackColor="White" BorderStyle="Solid"
                                            Style="text-align: right" Width="70px" Font-Bold="False" BorderColor="#00CCFF"
                                            BorderWidth="1px" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "shipqty")).ToString("#,##0.00;(#,##0.00); ") %>'></asp:TextBox>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lblgvFShipQty" runat="server" Font-Bold="True" Font-Size="12px" ForeColor="#CC0066" Width="70px"
                                            Style="text-align: right"></asp:Label>
                                    </FooterTemplate>
                                    <FooterStyle Font-Bold="true" Font-Size="12px" ForeColor="#333333" />
                                    <HeaderStyle Font-Size="12px" ForeColor="#333333" />
                                </asp:TemplateField>
                            </Columns>


                            <FooterStyle BackColor="#F5F5F5" />
                            <EditRowStyle />
                            <AlternatingRowStyle />
                            <PagerStyle CssClass="gvPagination" />
                            <HeaderStyle CssClass="grvHeader" />
                        </asp:GridView>

                        <div class="clearfix"></div>


                        <fieldset class="scheduler-border fieldset_B">
                            <div class="form-horizontal">
                                <asp:Panel ID="pnlexcelheading" runat="server" Visible="False" TabIndex="22">
                                    <div class="form-group">
                                        <div class="col-md-3 pading5px asitCol3">
                                            <asp:Label ID="lblheding" runat="server" CssClass=" dataLblview" Text="Product Details"></asp:Label>

                                        </div>

                                    </div>

                                </asp:Panel>
                            </div>
                        </fieldset>


                        <asp:Repeater ID="rpprodetails" runat="server">

                            <HeaderTemplate>
                                <table id="tblrpprodetails" class=" table-striped table-hover table-bordered grvContentarea">
                                    <tr>
                                        <th>SL</th>
                                        <th>Product_Id</th>
                                        <th>Pack_No</th>
                                        <th>M_IMEI</th>
                                        <th>S_IMEI</th>
                                        <th>Serial_No</th>
                                        <th>Color</th>
                                    </tr>
                            </HeaderTemplate>
                            <ItemTemplate>

                                <tr>
                                    <td>
                                        <asp:Label ID="lblrpSlNo" runat="server" Font-Bold="True" Height="16px"
                                            Style="text-align: right"
                                            Text='<%# Convert.ToString(Container.ItemIndex+1)+"." %>' Width="30px"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="lrpproid" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "Product_Id")) %>'
                                            Width="100px"></asp:Label>
                                    </td>

                                    <td>
                                        <asp:Label ID="lblrppackno" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "Pack_No")) %>'
                                            Width="100px"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblrpmimei" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "M_IMEI")) %>'
                                            Width="100px"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblrpsimei" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "S_IMEI")) %>'
                                            Width="100px"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblrpselno" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "Serial_No")) %>'
                                            Width="110px"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblrpColor" runat="server"
                                            Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "Color")) %>'
                                            Width="100px"></asp:Label>
                                    </td>

                                </tr>

                            </ItemTemplate>

                            <FooterTemplate>

                                <tr>
                                    <th></th>
                                    <th></th>
                                    <th></th>
                                    <th></th>
                                    <th></th>
                                    <th></th>

                                    <th></th>
                                </tr>


                                </table>
                            </FooterTemplate>





                        </asp:Repeater>
                        <div>
                            <asp:Label ID="lblPrintMsg" runat="server" CssClass="FormLevel"></asp:Label>

                        </div>

                    </div>
                </div>

            </div>

        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>


