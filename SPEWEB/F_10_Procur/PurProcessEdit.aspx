<%@ Page Title="" Language="C#" MasterPageFile="~/SPE.Master" AutoEventWireup="true" CodeBehind="PurProcessEdit.aspx.cs" Inherits="SPEWEB.F_10_Procur.PurProcessEdit" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript">
        window.onload = function () {
            var strCook = document.cookie;
            if (strCook.indexOf("!~") != 0) {
                var intS = strCook.indexOf("!~");
                var intE = strCook.indexOf("~!");
                var strPos = strCook.substring(intS + 2, intE);
                document.getElementById("grdWithScroll").scrollTop = strPos;
                //console.log("Position"+strPos);
            }
        }
        function SetDivPosition() {
            var intY = document.getElementById("grdWithScroll").scrollTop;
            console.log(intY);
            document.cookie = "yPos=!~" + intY + "~!";
        }

        function SetPosition() {
            var strCook = document.cookie;
            if (strCook.indexOf("!~") != 0) {
                var intS = strCook.indexOf("!~");
                var intE = strCook.indexOf("~!");
                var strPos = strCook.substring(intS + 2, intE);
                document.getElementById("grdWithScroll").scrollTop = strPos;
                console.log("Position" + strPos);
            }
        }
    </script>
    <script type="text/javascript">

        $(document).ready(function () {
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);

        });
        function pageLoaded() {

         <%--   var gv1 = $('#<%=this.gvpurorder.ClientID %>');
            gv1.Scrollable();--%>
            $("input, select").bind("keydown", function (event) {
                var k1 = new KeyPress();
                k1.textBoxHandler(event);


            });

            $('.chzn-select').chosen({ search_contains: true });

        };

    </script>

    <style type="text/css">
        .checkbox-update input{
            margin-right: 5px;
        }
        .chzn-single {
            border-radius: 3px !important;
            height: 29px !important;
        }

        .chzn-drop {
            width: 100% !important;
        }

        .chzn-search input {
            width: 100% !important;
        }
    </style>


    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>

                    <div class="card card-fluid">
                        <div class="card-body">
                            <div class="row">
                                <div class="form-group col-md-2">
                                    <asp:Label ID="lCurReqdate" runat="server" CssClass="col-form-label text-dark font-size-14" Text="Date:"></asp:Label>
                                    <asp:TextBox ID="txtCurReqDate" runat="server" AutoCompleteType="Disabled" CssClass="form-control form-control-sm" ToolTip="(dd.mm.yyyy)"></asp:TextBox>
                                    <cc1:CalendarExtender ID="txtCurReqDate_CalendarExtender" runat="server"
                                        Enabled="True" Format="dd.MM.yyyy" TargetControlID="txtCurReqDate"></cc1:CalendarExtender>
                                </div>

                                <div class="form-group col-md-5">
                                    <asp:Label ID="Label12" runat="server" CssClass="col-form-label text-dark font-size-14" Text="Requisition"></asp:Label>
                                    <div class="pl-2 row">
                                        <asp:DropDownList ID="ddlReqNo01" runat="server" CssClass=" form-control form-control-sm chzn-select" >
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="form-group col-md-2">
                              <asp:LinkButton ID="lbtnOk" runat="server" CssClass="btn btn-sm btn-primary mt-4" OnClick="lbtnOk_Click">Ok</asp:LinkButton>

                                </div>
                                <div class="form-group col-md-2">
                                    <asp:CheckBox ID="UpdateAllRow" CssClass="form-check form-check-input mt-4 checkbox-update" runat="server" Text="Update All Row"/>  
                                </div>

                                <div class="form-group col-md-2">
                                    <asp:Label ID="lblmsg1" runat="server" Visible="false" CssClass="btn btn-danger primaryBtn"></asp:Label>
                                    <asp:Label ID="lblvalvounum" runat="server" CssClass="lblTxt lblName" Visible="false"></asp:Label>
                                </div>
                            </div>
                        </div>
                    </div>
                    

                    

                    <div class="card card-fluid">
                        <div class="card-body py-5" style="min-height:350px">
                            <asp:GridView ID="gvpurorder" runat="server" AutoGenerateColumns="False"
                            CssClass="table-striped table-hover table-bordered grvContentarea" OnRowCancelingEdit="gvpurorder_RowCancelingEdit" OnRowEditing="gvpurorder_RowEditing" OnRowUpdating="gvpurorder_RowUpdating" OnRowDataBound="gvpurorder_RowDataBound">

                                <PagerSettings Position="Top" />
                                <RowStyle />
                                <Columns>
                                    <asp:TemplateField HeaderText="Sl">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvSlNo3" runat="server" Font-Bold="True" Height="16px" Style="text-align: right"
                                                Text='<%# Convert.ToString(Container.DataItemIndex+1)+"." %>' Width="20px"></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Res Code" Visible="False">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvResCod" runat="server" Height="16px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rsircode")) %>'
                                                Width="60px"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Supl Code" Visible="False">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvResCod1" runat="server" Height="16px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "ssircode")) %>'
                                                Width="60px"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Spcf Code" Visible="False">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvSpcfCod" runat="server" Height="16px" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "spcfcod")) %>'
                                                Width="60px"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderText="No">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvGenNo" runat="server" Font-Size="12px" Text='<%# "<B>"+ Convert.ToString(DataBinder.Eval(Container.DataItem, "grpdesc")) + "</B>"+
                                                                                    (DataBinder.Eval(Container.DataItem, "genno").ToString().Trim().Length>0 ? 
                                                                                    (Convert.ToString(DataBinder.Eval(Container.DataItem, "grpdesc")).Trim().Length>0 ?  "<br>" : "")+                                                             
                                                                                    "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;"+ 
                                                                                    Convert.ToString(DataBinder.Eval(Container.DataItem, "genno")).Trim(): "") 
                                                                         
                                                                            %>'
                                                Width="110px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="left" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>

                                    <%--  <asp:TemplateField HeaderText="">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="btnDelMat" OnClick="btnDelMat_Click" OnClientClick="javascript:return FunConfirm();" runat="server"><span style="color:red" class="glyphicon glyphicon-floppy-remove"></span> </asp:LinkButton>


                                        </ItemTemplate>
                             
                           
                                    </asp:TemplateField>--%>




                                    <asp:TemplateField HeaderText="Date">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvAppDat0" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "gdate")) %>'
                                                Width="70px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="left" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Ref. No">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvgrefno" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "refno")) %>'
                                                Width="120px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="left" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />

                                    </asp:TemplateField>

                                    <asp:CommandField CancelText="Can" ShowEditButton="True" />
                                        <asp:TemplateField HeaderText="Code ">
                                        <ItemTemplate>
                                            <asp:Label ID="lblgvRsircode" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rsircode")) %>'
                                                Width="85px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="left" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Material Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvMaterials3" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rsirdesc")) %>'
                                                Width="250px"></asp:Label>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:DropDownList ID="ddlmaterialname" CssClass="chzn-select" runat="server" Width="270px" OnSelectedIndexChanged="ddlmaterialname_SelectedIndexChanged" AutoPostBack="true">
                                            </asp:DropDownList>
                                        </EditItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Unit ">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvUnit2" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "rsirunit")) %>'
                                                Width="50px"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="left" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Specification">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvSpecification" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "spcfdesc")) %>'
                                                Width="130px"></asp:Label>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:DropDownList ID="ddlspecification" CssClass="chzn-select" runat="server" Width="150px">
                                            </asp:DropDownList>
                                        </EditItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderText="Supplier Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvSupplier01" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "ssirdesc")) %>'
                                                Width="100px"></asp:Label>
                                        </ItemTemplate>

                                        <EditItemTemplate>
                                            <asp:DropDownList ID="ddlSupname" CssClass="chzn-select" runat="server" Width="150px">
                                            </asp:DropDownList>
                                        </EditItemTemplate>

                                        <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Qty">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvreqty01" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty")).ToString("#,##0.00;(#,##0.00); ") %>'
                                                Width="60px" BorderStyle="Solid" BorderColor="#5aad6e" BorderWidth="1"></asp:TextBox>
                                        </ItemTemplate>
                                        <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Supplier Rate" Visible="False">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvsuprate" runat="server" Style="text-align: right; border-style: none;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "srate")).ToString("#,##0.000000;(#,##0.000000); ") %>'
                                                Width="60px"></asp:TextBox>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>


                                    <asp:TemplateField HeaderText="Actual Rate">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtgvAppRate01" BorderStyle="Solid" BorderColor="#5aad6e" BorderWidth="1" runat="server" Style="text-align: right;" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "rate")).ToString("#,##0.000000;(#,##0.000000); ") %>'
                                                Width="60px"></asp:TextBox>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Amount">
                                        <ItemTemplate>
                                            <asp:Label ID="lgvamount" runat="server" Style="text-align: right" Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt")).ToString("#,##0.0000;(#,##0.0000); ") %>'
                                                Width="100px"></asp:Label>
                                        </ItemTemplate>
                                        <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>

                                    <%--<asp:TemplateField HeaderText="User Name">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lgvusername" runat="server" Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "usrname")) %>'
                                                                        Width="120px"></asp:Label></ItemTemplate>
                                                                <FooterStyle Font-Bold="True" HorizontalAlign="right" />
                                                                <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                            </asp:TemplateField>--%>
                                </Columns>

                                <EditRowStyle />
                                <AlternatingRowStyle />
                                <PagerStyle CssClass="gvPagination" />
                                <HeaderStyle CssClass="grvHeader" />
                            </asp:GridView>
                        </div>
                    </div>
                    
                

        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>

