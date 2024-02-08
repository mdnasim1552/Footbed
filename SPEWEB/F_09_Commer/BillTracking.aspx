<%@ Page Title="" Language="C#" MasterPageFile="~/SPEMaster.Master" AutoEventWireup="true" CodeBehind="BillTracking.aspx.cs" Inherits="SPEWEB.F_09_Commer.BillTracking" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
   
    <style type="text/css">

        
        .style43
        {
            width: 157px;
        }
        .StyleCheckBoxList
        {
            text-transform: capitalize;
            margin-bottom: 0px;
        }
        
        .style45
        {
            width: 219px;
        }
        
        .txtboxformat
{
	border-style: none;
    border-color: inherit;
    border-width: medium;
    font-size: 12px;
	font-weight: normal;
	margin-right: 0px;
    text-align: left;
    margin-left: 0px;
}
                        
         .AutoExtender
       {
            font-family: Verdana, Helvetica, sans-serif;
            margin:0px 0 0 0px;
            font-size: .8em;
            font-weight: normal;
            border: solid 1px #006699;
            line-height: 20px;
            background-color: White;
            width:370px;
           
        }
        .AutoExtenderList
        {
            border-bottom: dotted 1px #006699;
            cursor: pointer;
            color: Maroon;
        }
        .AutoExtenderHighlight
        {
            color: White;
            background-color: #006699;
            cursor: pointer;
        }
        .style83
        {
            width: 1138px;
        }
        .style109
        {
            width: 83px;
        }
        .style110
        {
            width: 78px;
        }
        .style111
        {
            width: 87px;
        }
        .style112
        {
            width: 65px;
        }
        .style113
        {
            width: 7px;
        }
        .style114
        {
            width: 67px;
        }
        .style115
        {
            width: 101px;
        }
        .style116
        {
            width: 43px;
        }
        .style117
        {
            width: 6px;
        }
        .style120
        {
            width: 40px;
        }
        .style122
        {
            width: 14px;
        }
        .style123
        {
            width: 47px;
        }
        .style124
        {
            width: 116px;
        }
        .style125
        {
            width: 80px;
        }
        </style>

   

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    
     <link href="../CSS/Style.css" rel="stylesheet" type="text/css" />
     <link href="../CSS/PageInformation.css" rel="stylesheet" type="text/css" />
     <link href="../CSS/Style.css" rel="stylesheet" type="text/css" />
     
    <script type="text/javascript"  language="javascript" src="../Scripts/jquery-1.4.1.min.js"></script>
     <script language="javascript" type="text/javascript" src="../Scripts/ScrollableGridPlugin.js"></script>
  
   <script language="javascript" type="text/javascript">

       $(document).ready(function () {
           Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);

       });
       function pageLoaded() {
           var gv = $('#<%=this.gvBillTracking.ClientID %>');
           gv.Scrollable();
       }
   </script>
    


    <table style="width: 100%;">
        <tr>
            <td class="style12" width="500">
                <asp:Label ID="lblHtitle" runat="server" Font-Bold="True" Font-Size="14px" 
                    ForeColor="Yellow" Text="BILL TRACKING LIST" Width="513px"
                   STYLE="border-bottom:1px solid WHITE;border-top:1px solid WHITE;" 
                    Height="16px" ></asp:Label>
            </td>
            <td class="style45">
                                                    <asp:Label ID="lbljavascript" runat="server"></asp:Label>
                    <asp:DropDownList ID="DDPrintOpt" runat="server" Font-Names="Tahoma" 
                        Style="font-size: 11px" Width="130px">
                        <asp:ListItem Selected="True" Value="PDF">Adobe Acrobat (PDF)</asp:ListItem>
                        <asp:ListItem Value="HTML">HTML</asp:ListItem>
                        <asp:ListItem Value="WORD">MS Word</asp:ListItem>
                        <asp:ListItem Value="EXCEL">MS Excel</asp:ListItem>
                    </asp:DropDownList>
            </td>
            <td>
                <asp:LinkButton ID="lbtnPrint" runat="server" Font-Bold="True" Font-Size="12px" 
                    onclick="lbtnPrint_Click" CssClass="button" 
                    ForeColor="White">PRINT</asp:LinkButton>
                                                </td>
        </tr>
        </table>
              
                
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
     
        <ContentTemplate>
           
                            <table style="width:100%;">
                                <tr>
                                    <td>
                                        <asp:Panel ID="Panel8" runat="server">
                                            <table style="width: 100%;">
                                                <tr>
                                                    <td class="style124">
                                                        <asp:Label ID="Label12" runat="server" Font-Bold="True" Font-Size="14px" 
                                                            ForeColor="Yellow" 
                                                            style="border-top:1px solid yellow; border-bottom:1px solid yellow; " 
                                                            Text="Field Information:" Width="120px"></asp:Label>
                                                    </td>
                                                    <td class="style125">
                                                        <asp:CheckBox ID="chkallBillTracking" runat="server" AutoPostBack="True" 
                                                            BackColor="#000066" BorderColor="Yellow" BorderStyle="Solid" BorderWidth="1px" 
                                                            Font-Bold="True" Font-Size="12px" ForeColor="White" 
                                                            oncheckedchanged="chkallBillTracking_CheckedChanged" Text="Check All" 
                                                            Width="80px" />
                                                        test
                                                    </td>
                                                    <td class="style120">
                                                        <asp:Label ID="lblfdate" runat="server" Font-Bold="True" Font-Size="12px" 
                                                            style="color: #FFFFFF; text-align: right;" Text="From:" 
                                                            Width="70px"></asp:Label>
                                                    </td>
                                                    <td class="style122">
                                                        <asp:TextBox ID="txtfromdate" runat="server" BorderStyle="None" 
                                                            Font-Bold="True" Height="16px" Width="87px"></asp:TextBox>
                                                        <cc1:CalendarExtender ID="txtfromdate_CalendarExtender" runat="server" 
                                                            Format="dd-MMM-yyyy" TargetControlID="txtfromdate" TodaysDateFormat="">
                                                        </cc1:CalendarExtender>
                                                    </td>
                                                    <td class="style123">
                                                        <asp:Label ID="tDate" runat="server" Font-Bold="True" Font-Size="12px" 
                                                            style="color: #FFFFFF; text-align: right;" Text="To:" 
                                                            Width="50px"></asp:Label>
                                                    </td>
                                                    <td class="style83">
                                                        <asp:TextBox ID="txttodate" runat="server" BorderStyle="None" Font-Bold="True" 
                                                            Height="16px" Width="87px"></asp:TextBox>
                                                        <cc1:CalendarExtender ID="txttodate_CalendarExtender" runat="server" 
                                                            Format="dd-MMM-yyyy " TargetControlID="txttodate" TodaysDateFormat="">
                                                        </cc1:CalendarExtender>
                                                    </td>
                                                    <td class="style43">
                                                        &nbsp;</td>
                                                    <td>
                                                        &nbsp;</td>
                                                    <td>
                                                        &nbsp;</td>
                                                    <td>
                                                        &nbsp;</td>
                                                    <td>
                                                        &nbsp;</td>
                                                    <td>
                                                        &nbsp;</td>
                                                    <td>
                                                        &nbsp;</td>
                                                    <td>
                                                        &nbsp;</td>
                                                    <td>
                                                        &nbsp;</td>
                                                    <td>
                                                        &nbsp;</td>
                                                </tr>
                                                <tr>
                                                    <td class="style83" colspan="6">
                                                        <asp:CheckBoxList ID="cblBillTracking" runat="server" BorderColor="#FFCC00" 
                                                            BorderStyle="Solid" BorderWidth="1px" CellPadding="2" CellSpacing="0" 
                                                            CssClass="StyleCheckBoxList" Font-Bold="True" Font-Size="12px" 
                                                            ForeColor="White" Height="16px" RepeatColumns="12" Width="1067px" 
                                                            onselectedindexchanged="cblBillTracking_SelectedIndexChanged" 
                                                            AutoPostBack="True">
                                                            <asp:ListItem>aa</asp:ListItem>
                                                            <asp:ListItem>bb</asp:ListItem>
                                                            <asp:ListItem>cc</asp:ListItem>
                                                            <asp:ListItem>dd</asp:ListItem>
                                                            <asp:ListItem>ee</asp:ListItem>
                                                            <asp:ListItem>ff</asp:ListItem>
                                                            <asp:ListItem>gg</asp:ListItem>
                                                            <asp:ListItem>hh</asp:ListItem>
                                                            <asp:ListItem>mm</asp:ListItem>
                                                        </asp:CheckBoxList>
                                                    </td>
                                                    <td class="style43" valign="bottom">
                                                        &nbsp;</td>
                                                    <td>
                                                        &nbsp;</td>
                                                    <td>
                                                        &nbsp;</td>
                                                    <td>
                                                        &nbsp;</td>
                                                    <td>
                                                        &nbsp;</td>
                                                    <td>
                                                        &nbsp;</td>
                                                    <td>
                                                        &nbsp;</td>
                                                    <td>
                                                        &nbsp;</td>
                                                    <td>
                                                        &nbsp;</td>
                                                    <td>
                                                        &nbsp;</td>
                                                </tr>
                                            </table>
                                        </asp:Panel>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Panel ID="Panel4" runat="server" BorderColor="Yellow" BorderStyle="Solid" 
                                            BorderWidth="1px" Width="1074px" >
                                              <table style="width: 87%;">
                                                  <tr>
                                                      <td class="style110">
                                                          <asp:Label ID="lblSearchlist" runat="server" Font-Bold="True" Font-Size="14px" 
                                                              ForeColor="Yellow" 
                                                              style="border-top:1px solid yellow; border-bottom:1px solid yellow; " 
                                                              Text="Search List:" Width="80px"></asp:Label>
                                                      </td>
                                                      <td class="style109">
                                                          <asp:DropDownList ID="ddlFieldList1" runat="server" 
                                                              Font-Bold="True" Font-Size="12px" Width="120px">
                                                          </asp:DropDownList>
                                                      </td>
                                                      <td class="style111">
                                                          <asp:DropDownList ID="ddlSrch1" runat="server" Font-Bold="True" 
                                                              Font-Size="12px" Width="100px">
                                                              <asp:ListItem Value="like">Like</asp:ListItem>
                                                              <asp:ListItem Value="=">Equal</asp:ListItem>
                                                              <asp:ListItem Value="&lt;">Less Then</asp:ListItem>
                                                              <asp:ListItem Value="&gt;">Greater Then</asp:ListItem>
                                                              <asp:ListItem Value="&lt;=">Less Then Equal</asp:ListItem>
                                                              <asp:ListItem Value="&gt;=">Greater Then Equal</asp:ListItem>
                                                          </asp:DropDownList>
                                                      </td>
                                                      <td class="style112">
                                                          <asp:TextBox ID="txtSearch1" runat="server" CssClass="txtboxformat" 
                                                              Width="80px"></asp:TextBox>
                                                      </td>
                                                      <td class="style113">
                                                          <asp:DropDownList ID="ddlOperator1" runat="server" 
                                                              Font-Bold="True" Font-Size="12px" Width="60px">
                                                              <asp:ListItem Value="and">And</asp:ListItem>
                                                              <asp:ListItem Value="or">Or</asp:ListItem>
                                                          </asp:DropDownList>
                                                      </td>
                                                      <td class="style46" rowspan="3">
                                                          <asp:Panel ID="Panel5" runat="server">
                                                              <table style="width:100%;">
                                                                  <tr>
                                                                      <td class="style114">
                                                                          <asp:Label ID="lblOrderList" runat="server" Font-Bold="True" Font-Size="14px" 
                                                                              ForeColor="Yellow" 
                                                                              style="border-top:1px solid yellow; border-bottom:1px solid yellow; " 
                                                                              Text="Order Field:" Width="80px"></asp:Label>
                                                                      </td>
                                                                      <td class="style115">
                                                                          <asp:DropDownList ID="ddlOrder1" runat="server" Font-Bold="True" 
                                                                              Font-Size="12px" Width="150px">
                                                                          </asp:DropDownList>
                                                                      </td>
                                                                      <td class="style116">
                                                                          <asp:DropDownList ID="ddlOrderad1" runat="server" 
                                                                              Font-Bold="True" Font-Size="12px" Width="90px">
                                                                              <asp:ListItem Value="asc">Ascending</asp:ListItem>
                                                                              <asp:ListItem Value="desc">Descendig</asp:ListItem>
                                                                          </asp:DropDownList>
                                                                      </td>
                                                                      <td class="style117">
                                                                          <asp:LinkButton ID="lbtnSearch" runat="server" BackColor="#003366" 
                                                                              BorderColor="White" BorderStyle="Solid" BorderWidth="1px" Font-Bold="True" 
                                                                              Font-Size="12px" Height="16px" onclick="lbtnSearch_Click" 
                                                                              style="text-align: center; color: #FFFFFF; width: 19px;">Ok</asp:LinkButton>
                                                                      </td>
                                                                      <td>
                                                                          <asp:UpdateProgress ID="UpdateProgress2" runat="server">
                                                                              <ProgressTemplate>
                                                                                  <asp:Label ID="Label3" runat="server" BackColor="Blue" BorderColor="White" 
                                                                                      BorderStyle="Solid" BorderWidth="1px" Font-Bold="True" Font-Size="12px" 
                                                                                      ForeColor="Yellow" style="text-align: left" Text="Please wait . . . . . . ." 
                                                                                      Width="120px"></asp:Label>
                                                                              </ProgressTemplate>
                                                                          </asp:UpdateProgress>
                                                                      </td>
                                                                  </tr>
                                                                  <tr>
                                                                      <td class="style114">
                                                                          &nbsp;</td>
                                                                      <td class="style115">
                                                                          <asp:DropDownList ID="ddlOrder2" runat="server" Font-Bold="True" 
                                                                              Font-Size="12px" Width="150px">
                                                                          </asp:DropDownList>
                                                                      </td>
                                                                      <td class="style116">
                                                                          <asp:DropDownList ID="ddlOrderad2" runat="server" 
                                                                              Font-Bold="True" Font-Size="12px" Width="90px">
                                                                              <asp:ListItem Value="asc">Ascending</asp:ListItem>
                                                                              <asp:ListItem Value="desc">Descendig</asp:ListItem>
                                                                          </asp:DropDownList>
                                                                      </td>
                                                                      <td class="style117">
                                                                          &nbsp;</td>
                                                                      <td>
                                                                          &nbsp;</td>
                                                                  </tr>
                                                                  <tr>
                                                                      <td class="style114">
                                                                          &nbsp;</td>
                                                                      <td class="style115">
                                                                          <asp:DropDownList ID="ddlOrder3" runat="server" Font-Bold="True" 
                                                                              Font-Size="12px" Width="150px">
                                                                          </asp:DropDownList>
                                                                      </td>
                                                                      <td class="style116">
                                                                          <asp:DropDownList ID="ddlOrderad3" runat="server" 
                                                                              Font-Bold="True" Font-Size="12px" Width="90px">
                                                                              <asp:ListItem Value="asc">Ascending</asp:ListItem>
                                                                              <asp:ListItem Value="desc">Descendig</asp:ListItem>
                                                                          </asp:DropDownList>
                                                                      </td>
                                                                      <td class="style117">
                                                                          &nbsp;</td>
                                                                      <td>
                                                                          &nbsp;</td>
                                                                  </tr>
                                                              </table>
                                                          </asp:Panel>
                                                      </td>
                                                  </tr>
                                                  <tr>
                                                      <td class="style110">
                                                          &nbsp;</td>
                                                      <td class="style109">
                                                          <asp:DropDownList ID="ddlFieldList2" runat="server" 
                                                              Font-Bold="True" Font-Size="12px" Width="120px">
                                                          </asp:DropDownList>
                                                      </td>
                                                      <td class="style111">
                                                          <asp:DropDownList ID="ddlSrch2" runat="server" Font-Bold="True" 
                                                              Font-Size="12px" Width="100px">
                                                              <asp:ListItem Value="like">Like</asp:ListItem>
                                                              <asp:ListItem Value="=">Equal</asp:ListItem>
                                                              <asp:ListItem Value="&lt;">Less Then</asp:ListItem>
                                                              <asp:ListItem Value="&gt;">Greater Then</asp:ListItem>
                                                              <asp:ListItem Value="&lt;=">Less Then Equal</asp:ListItem>
                                                              <asp:ListItem Value="&gt;=">Greater Then Equal</asp:ListItem>
                                                          </asp:DropDownList>
                                                      </td>
                                                      <td class="style112">
                                                          <asp:TextBox ID="txtSearch2" runat="server" CssClass="txtboxformat" 
                                                              Width="80px"></asp:TextBox>
                                                      </td>
                                                      <td class="style113">
                                                          <asp:DropDownList ID="ddlOperator2" runat="server" 
                                                              Font-Bold="True" Font-Size="12px" Width="60px">
                                                              <asp:ListItem Value="and">And</asp:ListItem>
                                                              <asp:ListItem Value="or">Or</asp:ListItem>
                                                          </asp:DropDownList>
                                                      </td>
                                                  </tr>
                                                  <tr>
                                                      <td class="style110">
                                                          &nbsp;</td>
                                                      <td class="style109">
                                                          <asp:DropDownList ID="ddlFieldList3" runat="server" 
                                                              Font-Bold="True" Font-Size="12px" Width="120px">
                                                          </asp:DropDownList>
                                                      </td>
                                                      <td class="style111">
                                                          <asp:DropDownList ID="ddlSrch3" runat="server" Font-Bold="True" 
                                                              Font-Size="12px" Width="100px">
                                                              <asp:ListItem Value="like">Like</asp:ListItem>
                                                              <asp:ListItem Value="=">Equal</asp:ListItem>
                                                              <asp:ListItem Value="&lt;">Less Then</asp:ListItem>
                                                              <asp:ListItem Value="&gt;">Greater Then</asp:ListItem>
                                                              <asp:ListItem Value="&lt;=">Less Then Equal</asp:ListItem>
                                                              <asp:ListItem Value="&gt;=">Greater Then Equal</asp:ListItem>
                                                          </asp:DropDownList>
                                                      </td>
                                                      <td class="style112">
                                                          <asp:TextBox ID="txtSearch3" runat="server" CssClass="txtboxformat" 
                                                              Width="80px"></asp:TextBox>
                                                      </td>
                                                      <td class="style113">
                                                          &nbsp;</td>
                                                  </tr>
                                                  <tr>
                                                      <td class="style110">
                                                          <asp:Label ID="lblPage" runat="server" Font-Bold="True" Font-Size="12px" 
                                                              style="color: #FFFFFF; text-align: right;" Text="Page Size:" Visible="False" 
                                                              Width="70px"></asp:Label>
                                                      </td>
                                                      <td class="style109">
                                                          <asp:DropDownList ID="ddlpagesize" runat="server" AutoPostBack="True" 
                                                              BackColor="#CCFFCC" Font-Bold="True" Font-Size="12px" ForeColor="#3366FF" 
                                                              onselectedindexchanged="ddlpagesize_SelectedIndexChanged" Visible="False" 
                                                              Width="120px">
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
                                                      </td>
                                                      <td class="style111">
                                                          &nbsp;</td>
                                                      <td class="style112">
                                                          &nbsp;</td>
                                                      <td class="style113">
                                                          &nbsp;</td>
                                                  </tr>
                                              </table>
                                          </asp:Panel>
                                    </td>
                                </tr>
                                 
                                <tr>
                                    <td>
                                        <asp:Panel ID="Panel10" runat="server" Width="1260px">
                                            <asp:GridView ID="gvBillTracking" runat="server" AutoGenerateColumns="False" 
                                                ShowFooter="True" Width="616px" 
                                                onrowdatabound="gvBillTracking_RowDataBound" AllowPaging="True" 
                                                onpageindexchanging="gvBillTracking_PageIndexChanging">
                                                <RowStyle BackColor="#D2FFF7" Font-Size="11px" />
                                                <Columns>
                                                    
                                                    <asp:TemplateField  HeaderText=" Bill No">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lgvBillNo" runat="server" 
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "billno1")) %>' 
                                                                Width="70px"></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Left" />
                                                        
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Bill Date">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lgvMRDate" runat="server" style="text-align: Left" 
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "billdate")) %>' 
                                                                Width="65px"></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Left" />
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField  HeaderText=" Bill Ref.">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lgvBillRef" runat="server" 
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "billref")) %>' 
                                                                Width="65px"></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Left" />
                                                       
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    
                                                    <asp:TemplateField HeaderText="Project Description">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lgvProDesc" runat="server" style="text-align: Left" 
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "pactdesc")) %>' 
                                                                Width="140px"></asp:Label>
                                                        </ItemTemplate>
                                                        
                                                        <ItemStyle HorizontalAlign="Left" />
                                                        
                                                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Material's Description">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lgvMDesc" runat="server" style="text-align: Left" 
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "matdesc")) %>' 
                                                                Width="200px"></asp:Label>
                                                        </ItemTemplate>
                                                    <FooterTemplate>
                                                     <asp:Label ID="lgvmat" runat="server" Text="Grand Total" Font-Bold="True" HorizontalAlign="Left" Font-Size="12px" 
                                                         ForeColor="White" style="text-align: right" Width="200px"></asp:Label>
                                                    </FooterTemplate>
                                                        <ItemStyle HorizontalAlign="Left" />
                                                        <FooterStyle HorizontalAlign="Left" />
                                                        
                                                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                                        
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Specification">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lgvSpc" runat="server" style="text-align: Left" 
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "spcdesc")) %>' 
                                                                Width="75px"></asp:Label>
                                                        </ItemTemplate>
                                                        
                                                        <ItemStyle HorizontalAlign="Left" />
                                                       
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Unit">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lgvUnit" runat="server" style="text-align: Left" 
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "unit")) %>' 
                                                                Width="40px"></asp:Label>
                                                        </ItemTemplate>
                                                        
                                                        <ItemStyle HorizontalAlign="Left" />
                                                        
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Qty">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lgvQty" runat="server" style="text-align: right" 
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "qty")).ToString("#,##0;(#,##0); ") %>' 
                                                                Width="60px"></asp:Label>
                                                        </ItemTemplate>
                                                        
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Rate">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lgvRate" runat="server" style="text-align: right" 
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "rate")).ToString("#,##0;(#,##0); ") %>' 
                                                                Width="50px"></asp:Label>
                                                        </ItemTemplate>
                                                        
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                   <asp:TemplateField HeaderText="Bill Amt">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lgvamt" runat="server" style="text-align: right" 
                                                                Text='<%# Convert.ToDouble(DataBinder.Eval(Container.DataItem, "amt")).ToString("#,##0;(#,##0); ") %>' 
                                                                Width="60px"></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                                    <asp:Label ID="lgvFamt" runat="server" Font-Size="12px" Height="16px" 
                                                                        style="text-align: right" Width="60px" ForeColor="White" Font-Bold="true"></asp:Label>
                                                         </FooterTemplate>
                                                        <ItemStyle HorizontalAlign="Right" />
                                                        
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="BBLC Description">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lgvbblc" runat="server" style="text-align: Left" 
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "bblcdesc")) %>' 
                                                                Width="150px"></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Left" />
                                                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    
                                                    <asp:TemplateField HeaderText="Suplier's Name">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lgvpc9" runat="server" style="text-align: Left" 
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "supdesc")) %>' 
                                                                Width="150px"></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Left" />
                                                        <HeaderStyle HorizontalAlign="Left" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Voucher No">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lgvVNum" runat="server" style="text-align: Left" 
                                                                Text='<%# Convert.ToString(DataBinder.Eval(Container.DataItem, "vounum")) %>' 
                                                                Width="70px"></asp:Label>
                                                        </ItemTemplate>
                                                       
                                                        <ItemStyle HorizontalAlign="Left" />
                                                        
                                                        <HeaderStyle HorizontalAlign="Center" VerticalAlign="Top" />
                                                    </asp:TemplateField>
                                                </Columns>
                                                <FooterStyle BackColor="#333333" />
                                                <PagerStyle ForeColor="White" HorizontalAlign="Left" VerticalAlign="Top" />
                                                <HeaderStyle BackColor="#333300" Font-Bold="True" Font-Size="12px" 
                                                    ForeColor="#FFFFCC" Height="20px" HorizontalAlign="Center" />
                                                <EditRowStyle BackColor="#E2D5CD" VerticalAlign="Top" />
                                                <AlternatingRowStyle BackColor="#CAE4FF" Font-Size="11px" />
                                            </asp:GridView>
                                        </asp:Panel>
                                    </td>
                                </tr>
                            </table>
                      
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

