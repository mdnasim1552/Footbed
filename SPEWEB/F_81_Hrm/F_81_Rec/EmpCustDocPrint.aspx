<%@ Page Title="" Language="C#" MasterPageFile="~/SPEMaster.Master"  ValidateRequest="false" AutoEventWireup="true" CodeBehind="EmpCustDocPrint.aspx.cs" Inherits="SPEWEB.F_81_Hrm.F_81_Rec.EmpCustDocPrint" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    
    <script src="../../tinymce/jquery.tinymce.min.js"></script>
    <script src="../../tinymce/tinymce.min.js"></script>
    <script type="text/javascript" language="javascript">
        
        $(document).ready(function () {
           
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);
            
        });
        function pageLoaded() {
           
            tinymce.init({
                selector: 'textarea',
                toolbar: 'lineheight',
                lineheight_formats: '1 1.1 1.2 1.3 1.4 1.5 2',
                height: 400,
               
                plugins: [
                    "advlist autolink link image lists charmap print preview hr anchor pagebreak spellchecker table",
                    "searchreplace wordcount visualblocks visualchars code fullscreen insertdatetime media nonbreaking",
                    "save table contextmenu directionality template paste textcolor"
                ],
                fontsize_formats: "8pt 9pt 10pt 11pt 12pt 14pt 18pt 20pt 24pt 26pt 36pt",
                font_formats:
                    "Andale Mono=andale mono,times; SutonnyMJ=sutonnyMJ,solaimanLipi=solaimanLipi,Cambria=Cambria, Arial=arial,helvetica,sans-serif; Arial Black=arial black,palatino; Comic Sans MS=comic sans ms,sans-serif; Courier New=courier new,courier; Georgia=georgia,palatino; Helvetica=helvetica; Impact=impact,chicago; Symbol=symbol; Tahoma=tahoma,arial,helvetica,sans-serif; Terminal=terminal,monaco; Times New Roman=times new roman,times; Trebuchet MS=trebuchet ms,geneva; Verdana=verdana,geneva; Webdings=webdings; Wingdings=wingdings,zapf dingbats",

                toolbar: " tableprops tablerowprops tablecellprops | tableinsertcolbefore tableinsertcolafter tabledeletecol insertfile undo redo | fontsizeselect | styleselect| fontselect  | bold italic| alignleft aligncenter |  bullist numlist outdent indent | link image | print preview media fullpage | forecolor backcolor",
               
                imagetools_cors_hosts: ['www.tinymce.com', 'codepen.io'],
                forced_root_block: false,
                convert_urls: false,
                relative_urls: false,
                remove_script_host: false,
                document_base_url: false,
                content_css: [
                    '//fast.fonts.net/cssapi/e6dc9b99-64fe-4292-ad98-6974f93cd2a2.css',
                    '//www.tinymce.com/css/codepen.min.css'
                    
                ]

            });
           

        }

      
    </script>

  
   <div class="container moduleItemWrpper">
                <div class="contentPart">
    
    <div class="col-md-12 col-lg-12">
       
       
                   <div class="card card-fluid">
                <div class="card-body" style="height: 150px">
                    <div class="row">
                        <div class="col-md-2 col-xs-2 col-sm-2 ">
                            <asp:Label ID="Label4" runat="server" CssClass="lblTxt lblName">Employee Type</asp:Label>
                            <div class="form-inline">
                                <asp:TextBox ID="TextBox2" runat="server" CssClass="form-control-sm form-control W100Pixel" Visible="false"></asp:TextBox>
                                <asp:DropDownList ID="ddlWstation" runat="server" Width="100%"  OnSelectedIndexChanged="ddlWstation_SelectedIndexChanged" CssClass="form-control form-control-sm" AutoPostBack="true" TabIndex="2"></asp:DropDownList>

                            </div>
                        </div>
                        <div class="col-md-2 col-xs-2 col-sm-2 ">
                           <asp:Label ID="Label5" runat="server" CssClass="smLbl_to">Division</asp:Label>
                            <div class="form-inline">
                                
                                <asp:TextBox ID="TextBox1" runat="server" CssClass="form-control-sm form-control W100Pixel" Visible="false"></asp:TextBox>
                                <asp:DropDownList ID="ddlDivision" runat="server" Width="100%" OnSelectedIndexChanged="ddlDivision_SelectedIndexChanged" CssClass="form-control form-control-sm" AutoPostBack="true" TabIndex="2"></asp:DropDownList>

                            </div>
                        </div>
                        <div class="col-md-2 col-xs-2 col-sm-2">
                            <asp:Label ID="lblDept" runat="server" CssClass=" smLbl_to">Department</asp:Label>
                            <div class="form-inline">
                                <asp:TextBox ID="txtSrcPro" runat="server" CssClass="form-control-sm form-control W100Pixel" Visible="false"></asp:TextBox>
                                <asp:DropDownList ID="ddlDept" OnSelectedIndexChanged="ddlDept_SelectedIndexChanged" AutoPostBack="True" runat="server" Width="100%" CssClass="form-control form-control-sm ">
                                </asp:DropDownList>

                            </div>
                        </div>
                        <div class="col-md-2 col-xs-2 col-sm-2 ">
                            <asp:Label ID="Label3" runat="server" CssClass="smLbl_to">Section</asp:Label>
                            <div class="form-inline">
                                <asp:TextBox ID="txtSrcSec" runat="server" CssClass="form-control-sm form-control W100Pixel" Visible="false"></asp:TextBox>

                                <asp:DropDownList ID="ddlSection" runat="server" Width="100%" CssClass="form-control form-control-sm " AutoPostBack="true" OnSelectedIndexChanged="ddlSection_SelectedIndexChanged">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-2 col-xs-2 col-sm-2">
                            <asp:Label ID="lblEmp" runat="server" CssClass="smLbl_to">Employee List</asp:Label>
                            <div class="form-inline">
                                <asp:TextBox ID="txtEmpSrc" Style="display: none" runat="server" CssClass="form-control-sm form-control inputTxt inputName W100Pixel" Visible="false"></asp:TextBox>
                                <asp:DropDownList ID="ddlPEmpName" Width="100%" runat="server" AutoPostBack="false" CssClass="form-control form-control-sm " Visible="true">
                                </asp:DropDownList>
                            </div>
                        </div>
                        
                    </div>
                     <div class="row form-inline">
                    <div class="col-md-2 col-xs-2 col-sm-2">
                        <asp:Label ID="Label2" runat="server" CssClass="smLbl_to"> Letter Type </asp:Label>
                        <div class="form-inline">
                            <asp:DropDownList ID="ddlLttrType" runat="server" AutoPostBack="false" CssClass="form-control form-control-sm" Width="100%">
                                <asp:ListItem Value="01001">Appoitment Letter</asp:ListItem>
                                <asp:ListItem Value="02001">Confirmation Letter</asp:ListItem>
                                <asp:ListItem Value="04001">Joining Letter</asp:ListItem>
                                 <asp:ListItem Value="06001">Appoitment Top Sheet Letter</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-md-2 col-xs-2 col-sm-2">
                           
                            <div class="form-inline" style="margin-top:20px">
                                <asp:LinkButton ID="lbtnOk" runat="server"  CssClass="btn btn-sm btn-primary"  OnClick="lbtnOk_Click">Ok</asp:LinkButton>
                            </div>
                        </div>
                      </div>
                </div>
            </div>

            <div class="card card-fluid">
                <div class="card-body" style="height: 570px">


                    <div class="row" style="align-content: center">

                        <asp:TextBox ID="txtml" runat="server" TextMode="MultiLine" ></asp:TextBox>
                      

                    </div>
                    <div class="row" style="align-items: center">

                        <asp:Button ID="Button2" runat="server" CssClass="btn btn-success" OnClick="btnsave_Click" Text="Save" Visible="true" ValidationGroup="postValid" OnClientClick="tinyMCE.triggerSave(false,true);" />
                    </div>



                </div>
            </div>
   </div>
 </div>
                        </div>
</asp:Content>

