<%@ Page Title="" Language="C#" MasterPageFile="~/SPEMaster.Master" AutoEventWireup="true" CodeBehind="HrWinMenu.aspx.cs" Inherits="SPEWEB.HrWinMenu" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
     <link rel='stylesheet' href='assets\css\style.css' type='text/css' media='all' />
    <link rel='stylesheet' href='assets\css\colors.css' type='text/css' media='all' />
    <link rel='stylesheet' href='assets\css\comments.css' type='text/css' media='all' />
    <link rel='stylesheet' id='responsiveslides-css' href='assets\css\responsiveslides.css' type='text/css' media='all' />
    <link rel='stylesheet' id='reponsive-css' href='assets\css\reponsive.css' type='text/css' media='all' />
    <link rel='stylesheet' id='animate-custom-css' href='assets\css\animate-custom.css' type='text/css' media='all' />

    
    <style>
        .centErp {
            color: #099fd4;
            font-size: 10px !important;
            margin: 0;
        }

        .centErp1 span {
            font-size: 22px;
            margin-left: 5px;
            margin-top: 5px;
            font-weight: normal;
            color: #09ADE0;
        }

        .centErp p {
            font-size: 15px;
            width: 400px;
        }

        .copyR {
            line-height: 20px;
        }

        footer p {
            color: #000;
            margin: 0;
            padding: 5px 0;
        }

        .text-right {
            text-align: right;
        }

        body {
            font-family: Calibri,Arial !important;
            font-size: 11px !important;
            line-height: 18px;
        } 
        html body {
                                    background-color: #E0E0E0;
                                }
                                .headerMain2 {
                                    background:#E0E0E0;
                                }
    </style>


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
     <div class="myBody">
        <div id="spaces-main" class="pt-perspective">
            <section class="page-section home-page">
                <div class="asit_container">
                    <div class="row metro-panel">

                        <asp:Panel ID="PanTrading" runat="server">

                            <style>
                                html body {
                                    background-color: #E0E0E0;
                                }
                                .headerMain2 {
                                    background:#E0E0E0;
                                }
                               .myBody{
                                   top:79px;
                               }

                                @font-face {
                                    font-family: 'Agency FB';
                                    src: url('fonts/AgencyFB-Bold.eot?#iefix') format('embedded-opentype'), url('fonts/AgencyFB-Bold.woff') format('woff'), url('fonts/AgencyFB-Bold.ttf') format('truetype'), url('fonts/AgencyFB-Bold.svg#AgencyFB-Bold') format('svg');
                                    font-weight: normal;
                                    font-style: normal;
                                }

                                .box-title,
                                .featured-box-title,
                                .featured-title {
                                    position: absolute;
                                    bottom: 5px;
                                    left: 15px;
                                    font-size: 16px;
                                    font-weight: normal;
                                    text-shadow: 1px 0 1px rgba(0, 0, 0, 0.2);
                                    font-family: 'Agency FB' !important;
                                    color: #fff;
                                }

                                .page-section.home-page {
                                    /*background: url(assets/images/bg-3.jpg) #3495F3;*/
                                    background-color: #E0E0E0;
                                    color: #001840 !important;
                                    background-size: cover;
                                }


                                /*.colorWhite {
                                    color:#001840;
                                    background-color: #fff !important;
                                }
                                .colorWhite a span {
                                    color: #001840;
                                }

                                .metro-panel .space > div {
                                    min-height: 65px;
                                    text-align: left !important;
                                    position: relative;
                                    overflow: hidden;
                                    margin: 2px 0px;
                                   
                                }

                                .txtdiv {
                                    float: left;
                                    margin-left: 10px;
                                    margin-right:10px;
                                    
                                }

                                form .row .row .column, form .row .row .columns {
                                    padding: 0px 2px !important;
                                }

                                form .row .row {
                                    margin: 0 -0.2em !important;
                                }

                                .txtlineh {
                                    line-height: 1px !important;
                                }
                                .box-title, .featured-box-title, .featured-title{
                                    position:static;

                                }

                               .colorWhite a span{
                                    line-height:50px;
                                }*/
                                .box-title, .featured-box-title, .featured-title {
                                    left: 6px;
                                }

                                .boxShadow1 {
                                    /*box-shadow: 0 0 20px #888888;*/
                                    -moz-box-shadow: 0 5px 5px rgba(182, 182, 182, 0.75);
                                    -webkit-box-shadow: 0 5px 5px rgba(101, 125, 142, 0.75);
                                    box-shadow: 0 5px 5px rgba(101, 125, 142, 0.75);
                                }

                                /*.metro-panel .space > div {
                                    margin: 1px -4px;
                                }*/

                                .fadeInRightBig h3 {
                                    font-family: 'Agency FB';
                                    background: #046971;
                                    /*border: 1px solid #699f44;*/
                                    /*box-shadow: 0 0 4px 2px #bec9b6 inset;*/
                                    color: #fff !important;
                                    font-family: "Agency FB";
                                    font-size: 22px;
                                    line-height: 40px;
                                    margin: 5px -3px 0;
                                    text-align: center;
                                    text-decoration: underline;
                                    font-weight: normal;
                                }
                                   .fadeInRightBig h3 a{
                                       color:#fff;
                                   }
                               
                                .fa-4x {
                                    font-size: 22px !important;
                                }



                                .boxShadow {
                                    -webkit-border-bottom-right-radius: 5px;
                                    -webkit-border-bottom-left-radius: 5px;
                                    -moz-border-radius-bottomright: 5px;
                                    -moz-border-radius-bottomleft: 5px;
                                    border-bottom-right-radius: 5px;
                                    border-bottom-left-radius: 5px;
                                    box-shadow: 1px 0 10px 4px rgba(101, 125, 142, 0.75);
                                    border-bottom: 1px solid rgba(101, 125, 142, 0.75);
                                }

                                .boxInn {
                                    padding-bottom: 5px;
                                }

                                .fa {
                                    display: inline-block;
                                    float: left;
                                    font-family: FontAwesome;
                                    font-feature-settings: normal;
                                    font-kerning: auto;
                                    font-language-override: normal;
                                    font-size: inherit;
                                    font-size-adjust: none;
                                    font-stretch: normal;
                                    font-style: normal;
                                    font-synthesis: weight style;
                                    font-variant: normal;
                                    font-weight: normal;
                                    line-height: 1;
                                    margin-left: 16px;
                                    text-rendering: auto;
                                    color: lavender;
                                }

                                .boxShadow {
                                    /*background-image:url("Image/bg.png") !important;*/
                                }

                                .incolor {
                                    color: #575624 !important;
                                }



                                /****************************************   Custom Style   **************************************/

                                .color-1 {
                                    background: #564825;
                                    /*background: #525252;*/ /* fallback for old browsers */
                                    /*background: -webkit-linear-gradient(to left, #525252 , #3d72b4);*/ /* Chrome 10-25, Safari 5.1-6 */
                                    /*background: linear-gradient(to left, #525252 , #3d72b4);*/ /* W3C, IE 10+/ Edge, Firefox 16+, Chrome 26+, Opera 12+, Safari 7+ */
                                }

                                .color-2 {
                                    background: #6B5B86;
                                }

                                .color-3 {
                                    background: #000000; /* fallback for old browsers */
                                    background: -webkit-linear-gradient(to left, #000000, #434343); /* Chrome 10-25, Safari 5.1-6 */
                                    background: linear-gradient(to left, #000000, #434343); /* W3C, IE 10+/ Edge, Firefox 16+, Chrome 26+, Opera 12+, Safari 7+ */
                                }


                                .color-4 {
                                    /* fallback DIY*/
                                    /* Safari 4-5, Chrome 1-9 */
                                    background: -webkit-gradient(linear, left top, right top, from(#2F2727), color-stop(0.05, #1a82f7), color-stop(0.5, #2F2727), color-stop(0.95, #1a82f7), to(#2F2727));
                                    /* Safari 5.1+, Chrome 10+ */
                                    background: -webkit-linear-gradient(left, #2F2727, #1a82f7 5%, #2F2727, #1a82f7 95%, #2F2727);
                                    /* Firefox 3.6+ */
                                    background: -moz-linear-gradient(left, #2F2727, #1a82f7 5%, #2F2727, #1a82f7 95%, #2F2727);
                                    /* IE 10 */
                                    background: -ms-linear-gradient(left, #2F2727, #1a82f7 5%, #2F2727, #1a82f7 95%, #2F2727);
                                    /* Opera 11.10+ */
                                    background: -o-linear-gradient(left, #2F2727, #1a82f7 5%, #2F2727, #1a82f7 95%, #2F2727);
                                }

                                .color-5 {
                                    background: #141E30; /* fallback for old browsers */
                                    background: -webkit-linear-gradient(to left, #141E30, #243B55); /* Chrome 10-25, Safari 5.1-6 */
                                    background: linear-gradient(to left, #141E30, #243B55); /* W3C, IE 10+/ Edge, Firefox 16+, Chrome 26+, Opera 12+, Safari 7+ */
                                    /*background: #004FF9; 
background: -webkit-linear-gradient(to left, #004FF9 , #FFF94C);
background: linear-gradient(to left, #004FF9 , #FFF94C);*/
                                }

                                .color-6 {
                                    /* fallback DIY*/
                                    /* Safari 4-5, Chrome 1-9 */
                                    background: -webkit-gradient(linear, left top, right top, from(#2F2727), color-stop(0.05, #1a82f7), color-stop(0.5, #2F2727), color-stop(0.95, #1a82f7), to(#2F2727));
                                    /* Safari 5.1+, Chrome 10+ */
                                    background: -webkit-linear-gradient(left, #2F2727, #1a82f7 5%, #2F2727, #1a82f7 95%, #2F2727);
                                    /* Firefox 3.6+ */
                                    background: -moz-linear-gradient(left, #2F2727, #1a82f7 5%, #2F2727, #1a82f7 95%, #2F2727);
                                    /* IE 10 */
                                    background: -ms-linear-gradient(left, #2F2727, #1a82f7 5%, #2F2727, #1a82f7 95%, #2F2727);
                                    /* Opera 11.10+ */
                                    background: -o-linear-gradient(left, #2F2727, #1a82f7 5%, #2F2727, #1a82f7 95%, #2F2727);
                                }

                                .color-7 {
                                    /*background: #6A9113;*/
                                    background: #D6CFBF;
                                }

                                    .color-7 .fa {
                                        /*right:0;*/
                                        margin-top: -15px;
                                        color: #938562 !important;
                                    }

                                    

                                    .color-7 .box-title {
                                        bottom: 33%;
                                        left: 15px;
                                        color: #938562 !important;
                                    }

                                .color-8 .fa {
                                    padding-top: 15px;
                                    margin-left: 4px;
                                    color: #938562;
                                    font-size: 12px !important;
                                }

                                .color-8 .box-title {
                                    bottom: 33%;
                                    left: 24px;
                                    color: #938562 !important;
                                }

                                .color-8 .leftIssue {
                                    left: 50%;
                                }

                                .color-8 .leftIssue1 {
                                    margin-left: 75px;
                                }

                                .color-8 {
                                    /*background-color:#089BA6;*/
                                    /*background-color:#2B6457;*/
                                    background-color: #FCF5E5;
                                }

                                .color-9 {
                                    background-color: #ebebeb;
                                }

                                    .color-9 .box-title {
                                        color: #089bad !important;
                                    }

                                    .color-9 .fa {
                                        color: #089bad !important;
                                    }

                                .color-10 {
                                    background: #43cea2; /* fallback for old browsers */
                                    background: -webkit-linear-gradient(to left, #43cea2, #185a9d); /* Chrome 10-25, Safari 5.1-6 */
                                    background: linear-gradient(to left, #43cea2, #185a9d); /* W3C, IE 10+/ Edge, Firefox 16+, Chrome 26+, Opera 12+, Safari 7+ */
                                }

                                .color-11 {
                                    background-color: #089BA6;
                                }

                                .color-12 {
                                    background: #00223E; /* fallback for old browsers */
                                    background: -webkit-linear-gradient(to left, #00223E); /* Chrome 10-25, Safari 5.1-6 */
                                    background: linear-gradient(to left, #00223E); /* W3C, IE 10+/ Edge, Firefox 16+, Chrome 26+, Opera 12+, Safari 7+ */
                                }

                                .color-13 {
                                    background: #A26B57;
                                    /*background: #780206;*/ /* fallback for old browsers */
                                    /*background: -webkit-linear-gradient(to left, #780206 , #061161);*/ /* Chrome 10-25, Safari 5.1-6 */
                                    /*background: linear-gradient(to left, #780206 , #061161);*/ /* W3C, IE 10+/ Edge, Firefox 16+, Chrome 26+, Opera 12+, Safari 7+ */
                                }

                                .color-14 {
                                    background: #000000; /* fallback for old browsers */
                                    background: -webkit-linear-gradient(to left, #000000, #53346D); /* Chrome 10-25, Safari 5.1-6 */
                                    background: linear-gradient(to left, #000000, #53346D); /* W3C, IE 10+/ Edge, Firefox 16+, Chrome 26+, Opera 12+, Safari 7+ */
                                }

                                .color-15 {
                                    background: #606c88; /* fallback for old browsers */
                                    background: -webkit-linear-gradient(to left, #606c88, #3f4c6b); /* Chrome 10-25, Safari 5.1-6 */
                                    background: linear-gradient(to left, #606c88, #3f4c6b); /* W3C, IE 10+/ Edge, Firefox 16+, Chrome 26+, Opera 12+, Safari 7+ */
                                }

                                /*.color-16 {
                                    background-color: #68a5ad;
                                }*/



                                .color-17 {
                                    background: #16222A; /* fallback for old browsers */
                                    background: -webkit-linear-gradient(to left, #16222A, #3A6073); /* Chrome 10-25, Safari 5.1-6 */
                                    background: linear-gradient(to left, #16222A, #3A6073); /* W3C, IE 10+/ Edge, Firefox 16+, Chrome 26+, Opera 12+, Safari 7+ */
                                }

                                .color-18 {
                                    background: #4E4261;
                                }

                                .color-19 {
                                    background: #2C632E; /* fallback for old browsers */
                                }

                                .color-20 {
                                    background-color: #E0E0E0;
                                }

                                .color-21 {
                                    background-color: #4B281B;
                                }

                                .color-22 {
                                    background-color: #897770;
                                }

                                .color-23 {
                                    background-color: #62875d;
                                }

                                .color-24 {
                                    background-color: #5d8787;
                                }

                                .color-25 {
                                    background-color: #7a94cc;
                                }

                                .color-26 {
                                    background-color: #8D5E6B;
                                }

                                .color-27 {
                                    background-color: #355A3E;
                                }

                                .color-28 {
                                    background-color: #9F8F19;
                                }

                                /*.color-29{
                                    background-color: #089bad;
                                }*/

                                /*.color-30 {
                                    background-color: #68a5ad;
                                }*/

                                .color-91 {
                                    background: #135058;
                                    color: #fff;
                                }

                                .color-92 {
                                    background: #bf8f00;
                                    color: #fff;
                                }

                                .color-94 {
                                    background: #68A5AD;
                                    color: #fff;
                                }

                                .color-95 {
                                    background: #92D14F;
                                    color: #fff;
                                }

                                .color-96 {
                                    background: #089BA6;
                                    color: #fff;
                                }

                                .color-97 {
                                    background: #538234;
                                    color: #fff;
                                }

                                .color-98 {
                                    background: #3C6B95 !important;
                                    color: #fff !important;
                                }

                                .color-99 {
                                    background: #B26711;
                                    color: #fff;
                                }

                                .color-100 {
                                    background: #833D0C;
                                    color: #fff;
                                }

                                .color-101 {
                                    background: #2E8B57;
                                    color: #fff;
                                }

                                .box-title {
                                    color: #7F6930 !important;
                                }

                                .co4 {
                                    color: #7F6930 !important;
                                }





                                .color-103 {
                                    background: #089BAD;
                                    color: #fff !important;
                                }

                                .box-title {
                                    color: #fff !important;
                                }



                                .Cl .box-title {
                                    font-size: 16px;
                                    color: #fff !important;
                                }

                                .Cl2 .box-title {
                                    font-size: 14px;
                                    color: #fff !important;
                                }

                                .colRed {
                                    color: cornsilk;
                                }

                                .Cldash .fa {
                                    font-size: 28px !important;
                                }

                                .Cldash .box-title {
                                    font-size: 18px !important;
                                }

                                form .row .row .column, form .row .row .columns {
                                    padding: 0 5px;
                                }

                                .color-93 {
                                    background: #00AF50 !important;
                                    color: #fff !important;
                                }

                                    .color-93 .box-title {
                                        color: #fff !important;
                                    }

                                    .color-93 .fa {
                                        color: #fff !important;
                                    }

                                .color-103 .box-title {
                                    color: #fff !important;
                                }

                                .color-103 .fa .co6 {
                                    color: #fff !important;
                                }

                                .color-102 {
                                    background: #EDF5EA;
                                    color: #0A3662 !important;
                                }

                                    .color-102 .fa {
                                        color: #0A3662 !important;
                                    }

                                    .color-102 .box-title {
                                        color: #0A3662 !important;
                                    }


                                .color-105 {
                                    /*background: #6A9113;*/
                                    background: #fcf5f6;
                                }

                                    .color-105 .fa {
                                        /*right:0;*/
                                        margin-top: -15px;
                                        color: #938562 !important;
                                    }

                                    .color-105 .box-title {
                                        bottom: 33%;
                                        left: 15px;
                                        color: #938562 !important;
                                    }

                                    .color-115 {
                                    /*background: #6A9113;*/
                                    background: #046971;
                                    color:#fff;
                                }
                                     .color-105 .fnb {
                                        /*right:0;*/
                                        margin-top: -5px;
                                        color: #938562 !important;
                                    }

                                     .metro-panel .space > div {
  margin: 2px -3px;
}

                            </style>



                            <div class="row boxShadow"  style="padding: 0 6px;  background-image: url("Image/bg.PNG") !important; margin: 0;">
                                <div class="boxInn">

                                    <div class="four large-4 columns Cl Cl2">
                                                                               <h3><a href="#">Setting</a></h3>

                                         <div class="row">
                                            
                                                <a href="<%=this.ResolveUrl("~/F_81_Hrm/F_82_App/HRCodeBook.aspx")%>" target="_blank">
                                            <div class='twelve large-12 columns space featured blog-box medium'>
                                                <div class='color-93'>                                                   
                                                        <span class='box-title boxcol'>Information Code</span>
                                                        <br/>
                                                        <i class='fa-cubes fa fa-4x  co6'></i>                                                   
                                                </div>
                                            </div>
                                           </a>

                                           

                                                                                       



                                                <a href="<%=this.ResolveUrl("~/F_21_GAcc/AccSubCodeBook.aspx?InputType=Dept")%>" target="_blank">
                                            
                                            
                                            <div class='six small-6 columns contact-box space'>
                                                <div class='color-115'>
                                                    
                                                
                                                    <span class='box-title incolor'>Department Code</span>
                                                    <br/>
                                                    <i class='fa-plus-square-o fa fa-4x'></i>
                                                    
                                                </div>
                                            </div>
                                            
                                                    </a>
                                                <a href="<%=this.ResolveUrl("~/F_81_Hrm/F_82_App/HRDesigCode.aspx")%>" target="_blank">

                                            
                                            <div class='six small-6 columns contact-box space'>
                                                <div class='color-99'>
                                                    
                                                
                                                    <span class='box-title incolor'>Designations Code</span>
                                                    <br/>
                                                    <i class='fa-plus-square-o fa fa-4x'></i>
                                                    
                                                </div>
                                            </div>
                                          
                                                    </a>
                                           

<%--                                            
                                            <asp:LinkButton ID="lnkbtnExport" runat="server" PostBackUrl="F_81_Hrm/F_82_App/HREmpEntry.aspx?Type=Officetime">
                                            <div class='six small-3 columns contact-box space'>
                                                <div class='color-94'>
                                                  
                                                    <span class='box-title incolor'>Lunch Time</span>
                                                    <br>
                                                    <i class='fa-plus-square-o fa '></i>
                                                   
                                                </div>
                                            </div>
                                            </asp:LinkButton>--%>
                                           
                                             <a href="<%=this.ResolveUrl("~/F_81_Hrm/F_82_App/EmpEntryForm.aspx")%>" target="_blank">
                                                <div class='six small-12 columns contact-box space'>
                                                    <div class='color-98'>

                                                        <span class='box-title'>Create New ID</span>
                                                        <br>
                                                        <i class=' fa-calendar fa fa-4x'></i>

                                                    </div>
                                                </div>
                                            </a>
                                                <a href="<%=this.ResolveUrl("~/F_81_Hrm/F_82_App/HREmpEntry.aspx?Type=Officetime")%>" target="_blank">
                                           
                                            
                                            <div class='six small-6 columns contact-box space'>
                                                <div class='color-99'>
                                                   
                                                
                                                    <span class='box-title incolor'>Office Time</span>
                                                    <br/>
                                                   <i class='fa-plus-square-o fa fa-4x'></i>
                                                   
                                                </div>
                                            </div>
                                           </a>

                                            <div class='six small-6 columns contact-box space'>
                                                <div class='color-101'>
                                                     <a href="<%=this.ResolveUrl("~/F_81_Hrm/F_83_Att/HREmpOffDays.aspx")%>" target="_blank">
                                                    
                                                        <span class='box-title incolor'>Off Days</span>
                                                        <br>
                                                        <i class='fa fa-bar-chart fa-4x'></i>
                                                  </a>


                                                </div>
                                            </div>
                                         

                                            <%--<asp:LinkButton ID="lnkInstSetup" runat="server" PostBackUrl="F_81_Hrm/F_86_All/EmpOvertime.aspx?Type=Lencashment">
                                            <div class='six small-3 columns contact-box space'>
                                                <div class='color-99'>
                                                   
                                                    <span class='box-title incolor'>Encashment</span>
                                                    <br>
                                                    <i class=' fa-th-large fa '></i>
                                                   
                                                </div>
                                            </div>
                                            </asp:LinkButton>--%>


                                            <%--<asp:LinkButton ID="lnkbtnProShiPlan" runat="server" PostBackUrl="~/F_81_Hrm/F_84_Lea/HRLeaveOpening.aspx">
                                            <div class='six small-3 columns contact-box space'>
                                                <div class='color-100'>
                                                    
                                                    <span class='box-title incolor'>Leave Opening</span>
                                                    <br>
                                                    <i class='fa fa-th  '></i>
                                                   
                                                </div>
                                            </div>

                                            </asp:LinkButton>--%>

                                                <a href="<%=this.ResolveUrl("~/F_81_Hrm/F_84_Lea/HREmpLeave.aspx?Type=LeaveRule")%>" target="_blank">

                                            
                                            <div class='six small-6 columns contact-box space'>
                                                <div class='color-96'>
                                                   
                                                    <span class='box-title incolor'>Leave Rule</span>
                                                    <br>
                                                    <i class=' fa-th-large fa fa-4x'></i>
                                                   
                                                </div>
                                            </div>
                                          </a>

                                            <a href="<%=this.ResolveUrl("~/F_81_Hrm/F_85_Lon/EmpLoanInfo.aspx")%>" target="_blank">
                                                <div class='six small-6 columns contact-box space'>
                                                    <div class='color-115'>

                                                        <span class='box-title incolor'>Personal Loan</span>
                                                        <br>
                                                        <i class='fa-th-large fa fa-4x'></i>

                                                    </div>
                                                </div>
                                            </a>

                                      
                                          
                                           
                                             <a href='<%=this.ResolveUrl("~/DeafultMenu.aspx?Type=4101")%>' target="_blank">
                                                <div class="six small-6 columns contact-box space">
                                                    <div class="color-93">

                                                        <span class="box-title incolor">Flow</span>
                                                        <br />
                                                        <i class="fa fa-bar-chart fa-4x"></i>
                                                        <%--<div class="wd-tweets"></div>--%>
                                                    </div>
                                                </div>
                                            </a>
                                            

                                          <%--  <div class='six small-3 columns contact-box space'>
                                                <div class='color-95'>
                                                    <asp:LinkButton ID="lnkbtnMis" runat="server" >
                                                        <span class='box-title incolor'></span>
                                                        <br>
                                                        <i class='fa-renren fa '></i>
                                                    </asp:LinkButton>
                                                </div>
                                            </div>--%>

                                            <div class='six small-6 columns contact-box space'>
                                                <div class='color-94'>
                                                    <asp:LinkButton ID="lnkbtnHRM" OnClick="lnkbtnHRM_Click" runat="server" >
                                                        <span class='box-title incolor'>Menu</span>
                                                        <br>
                                                        <i class='fa-renren fa fa-4x'></i>
                                                    </asp:LinkButton>
                                                </div>
                                            </div>
                                            

                                        </div>
                                        <br />
                                    </div>

                                    <div class="four large-4 columns mid">
                                        <h3><a href="<%=this.ResolveUrl("~/StandardFlow.aspx")%>">Entry (HR)</a></h3>

                                          <div class="row">
                                           
                                                <div class='six small-3 columns contact-box space'>
                                                    <div class='color-7'>

                                                        <span class='box-title'>Recuritment</span>
                                                        <br>
                                                        <i class='fa fa-long-arrow-right '></i>

                                                    </div>
                                                </div>
                                        

                                              <a href="<%=this.ResolveUrl("~/F_81_Hrm/F_81_Rec/ShortListing.aspx?Type=IResult")%>" target="_blank">
                                                <div class='six small-3 columns contact-box space'>
                                                    <div class='color-8'>

                                                        <span class='box-title'>Interview</span>
                                                        <br>
                                                        <i class='fa fa-th-large'></i>

                                                    </div>
                                                </div>
                                            </a>
                                             
                                            
                                               <a href="<%=this.ResolveUrl("~/F_81_Hrm/F_81_Rec/ShortListing.aspx?Type=Fselection")%>" target="_blank">
                                                <div class='six small-3 columns contact-box space'>
                                                    <div class='color-8'>

                                                        <span class='box-title'>Selection</span>
                                                        <br>
                                                        <i class='fa fa-th-large'></i>

                                                    </div>
                                                </div>
                                            </a>
                                               <a href="<%=this.ResolveUrl("~/F_81_Hrm/F_81_Rec/LetterOfAppoinment.aspx?Type=LCreate")%>" target="_blank">

                                             
                                            <div class='six small-3 columns contact-box space'>
                                                <div class='color-8'>
                                                    
                                                
                                                    <span class='box-title incolor'>Appointment</span>
                                                    <br/>
                                                   
                                                     <i class='fa fa-th-large'></i>
                                                </div>
                                            </div>
                                           </a>
                                          
                                                <div class='six small-3 columns contact-box space'>
                                                    <div class='color-7'>

                                                        <span class='box-title'>Appointment</span>
                                                        <br>
                                                        <i class='fa fa-long-arrow-right '></i>

                                                    </div>
                                                </div>
                                        
                                            

                                            <a href="<%=this.ResolveUrl("~/F_81_Hrm/F_82_App/EmpEntry01.aspx?Type=Entry&empid=")%>" target="_blank">
                                                <div class='six small-3 columns contact-box space'>
                                                    <div class='color-8'>

                                                        <span class='box-title'>CV Input</span>
                                                        <br>
                                                          <i class='fa fa-th-large'></i>

                                                    </div>
                                                </div>
                                            </a>
                                             
                                            
                                             <a href="<%=this.ResolveUrl("~/F_81_Hrm/F_82_App/HREmpEntry.aspx?Type=Aggrement")%>" target="_blank">
                                                <div class='six small-6 columns contact-box space'>
                                                    <div class='color-8'>

                                                        <span class='box-title'>Aggrement</span>
                                                        <br>
                                                        <i class='fa fa-th-large'></i>

                                                    </div>
                                                </div>
                                            </a>
 

                                          
                                                <div class='six small-3 columns contact-box space'>
                                                    <div class='color-7'>

                                                        <span class='box-title '>Attendance</span>
                                                        <br>
                                                          <i class='fa fa-long-arrow-right '></i>

                                                    </div>
                                                </div>
                                         
                                    <a href="<%=this.ResolveUrl("~/F_81_Hrm/F_83_Att/HRDailyAttenUpload.aspx")%>" target="_blank">
                                                <div class='six small-3 columns contact-box space'>
                                                    <div class='color-8'>

                                                        <span class='box-title '>Update</span>
                                                        <br>
                                                          <i class='fa fa-th-large '></i>

                                                    </div>
                                                </div>
                                            </a>
                                             
<a href="<%=this.ResolveUrl("~/F_81_Hrm/F_83_Att/EmpMonLateApproval.aspx?Type=MLateAppDay")%>" target="_blank">
                                                <div class='six small-3 columns contact-box space'>
                                                    <div class='color-8'>

                                                        <span class='box-title'>Late Approval</span>
                                                        <br>
                                                          <i class='fa fa-th-large '></i>

                                                    </div>
                                                </div>
                                            </a>

                                             <a href="<%=this.ResolveUrl("~/F_81_Hrm/F_83_Att/HRDailyAttenManually.aspx?Type=Daily")%>" target="_blank">
                                                <div class='six small-3 columns contact-box space'>
                                                    <div class='color-8'>

                                                        <span class='box-title'>Punch Approval</span>
                                                        <br>
                                                          <i class='fa fa-th-large '></i>

                                                    </div>
                                                </div>
                                            </a>

                                     
                                          
                                                <div class='six small-3 columns contact-box space'>
                                                    <div class='color-7'>

                                                        <span class='box-title'>Leave</span>
                                                        <br>
                                                        <i class='fa fa-long-arrow-right '></i>

                                                    </div>
                                                </div>
                                             <a href="<%=this.ResolveUrl("~/F_81_Hrm/F_84_Lea/HREmpLeave.aspx?Type=LeaveApp")%>" target="_blank">
                                           

                                            <div class='twelve small-6 columns twitter-feed-box space'>
                                                <div class='color-8'>
                                                 
                                                    <span class='box-title incolor'>Leave Application</span>
                                                    <br>
                                                    <i class='fa fa-th-large'></i>
                                                 
                                                </div>
                                            </div>
                                            </a>
                                             <a href="<%=this.ResolveUrl("~/F_81_Hrm/F_92_Mgt/InterfaceLeavApp.aspx")%>" target="_blank">
                                                <div class='six small-3 columns contact-box space'>
                                                    <div class='color-8'>

                                                        <span class='box-title'>Process</span>
                                                        <br>
                                                        <i class='fa fa-th-large'></i>

                                                    </div>
                                                </div>
                                            </a>
                                             
                                           
                                                <div class='six small-3 columns contact-box space'>
                                                    <div class='color-7'>

                                                        <span class='box-title'>Transfer</span>
                                                        <br>
                                                        <i class='fa fa-long-arrow-right '></i>

                                                    </div>
                                                </div>
                                           
                                            
                                             <a href="<%=this.ResolveUrl("~/F_81_Hrm/F_92_Mgt/RetiredEmployee.aspx")%>" target="_blank">
                                                <div class='six small-3 columns contact-box space'>
                                                    <div class='color-8'>

                                                        <span class='box-title'>Resignation</span>
                                                        <br>
                                                        <i class='fa fa-th-large'></i>

                                                    </div>
                                                </div>
                                            </a>
                                             <a href="<%=this.ResolveUrl("~/F_81_Hrm/F_92_Mgt/EmpHold.aspx?Type=EmpSalHold")%>" target="_blank">
                                                <div class='six small-3 columns contact-box space'>
                                                    <div class='color-8'>

                                                        <span class='box-title'>Hold</span>
                                                        <br>
                                                        <i class='fa fa-th-large fnb'></i>

                                                    </div>
                                                </div>
                                            </a>
                                             <a href="<%=this.ResolveUrl("~/F_81_Hrm/F_92_Mgt/InterfaceHR.aspx")%>" target="_blank">
                                                <div class='six small-3 columns contact-box space'>
                                                    <div class='color-8'>

                                                        <span class='box-title'>HR DPT</span>
                                                        <br>
                                                        <i class='fa fa-th-large'></i>

                                                    </div>
                                                </div>
                                            </a>



                                          
                                                <div class='six small-3 columns contact-box space'>
                                                    <div class='color-105'>

                                                        <span class='box-title'>PF</span>
                                                        <br>
                                                       <i class='fa fa-long-arrow-right fnb'></i>

                                                    </div>
                                                </div>
                                       
                                             <a href="<%=this.ResolveUrl("~/F_81_Hrm/F_90_PF/AccBankRecon.aspx")%>" target="_blank">
                                                <div class='six small-3 columns contact-box space'>
                                                    <div class='color-105'>

                                                        <span class='box-title'>PF Update</span>
                                                        <br>
                                                       <i class='fa fa-th-large fnb'></i>

                                                    </div>
                                                </div>
                                            </a>
                                          
                                           
                                             <a href="<%=this.ResolveUrl("~/F_81_Hrm/F_86_All/EmpOvertime.aspx?Type=loan")%>" target="_blank">
                                                <div class='six small-3 columns contact-box space'>
                                                    <div class='color-105'>

                                                        <span class='box-title'>PF Loan (R)</span>
                                                        <br>
                                                       <i class='fa fa-th-large fnb'></i>

                                                    </div>
                                                </div>
                                            </a>
                                            
                                             <a href="<%=this.ResolveUrl("~/F_81_Hrm/F_85_Lon/EmpLoanInfo.aspx")%>" target="_blank">
                                                <div class='six small-3 columns contact-box space'>
                                                    <div class='color-105'>

                                                        <span class='box-title'>PF Loan (P)</span>
                                                        <br>
                                                       <i class='fa fa-th-large fnb'></i>

                                                    </div>
                                                </div>
                                            </a>
                                            




                                        </div>
                                    </div>

                                    <div class="four large-4 columns Cl">
                                                                                <h3><a href="<%=this.ResolveUrl("~/RptDefault.aspx?Type=All")%>">Reports</a></h3>

                                          <div class="row">
                                            <a href="<%=this.ResolveUrl("~/F_81_Hrm/F_92_Mgt/AllEmpList.aspx")%>" target="_blank">
                                                <div class='twelve small-3 columns twitter-feed-box space Cldash'>
                                                    <div class='color-92'>

                                                        <span class='box-title'>Members</span>
                                                        <br />
                                                         <i class="fa-tachometer fa  colRed"></i>

                                                    </div>
                                                </div>
                                            </a>

                                            <a href="<%=this.ResolveUrl("~/F_81_Hrm/F_97_MIS/RptMgtInterface.aspx")%>" target="_blank">
                                                <div class='six small-3 columns contact-box space Cldash'>
                                                    <div class='color-92'>

                                                        <span class='box-title'>Summary</span>
                                                        <br>
                                                        <i class="fa-tachometer fa  colRed"></i>

                                                    </div>
                                                </div>
                                            </a>


                                            <a href='<%=this.ResolveUrl("~/F_81_Hrm/F_99_MgtAct/RptgroupAttendance.aspx")%>' target="_blank">
                                                <div class="twelve small-3 columns twitter-feed-box space Cldash">
                                                    <div class="color-92">

                                                        <span class="box-title incolor">Attendance</span>
                                                        <br />
                                                        <i class="fa-tachometer fa  colRed"></i>
                                                        <%--<div class="wd-tweets"></div>--%>
                                                    </div>
                                                </div>
                                            </a>
                                            <a href='<%=this.ResolveUrl("~/F_81_Hrm/F_83_Att/RptWeekPresence.aspx")%>' target="_blank">
                                                <div class="twelve small-3 columns twitter-feed-box space Cldash">
                                                    <div class="color-92">

                                                        <span class="box-title incolor">Attendance(W)</span>
                                                        <br />
                                                         <i class="fa-tachometer fa  colRed"></i>
                                                        <%--<div class="wd-tweets"></div>--%>
                                                    </div>
                                                </div>
                                            </a>
                                           


                                            <a href='<%=this.ResolveUrl("~/F_81_Hrm/F_89_Pay/RpHRtPayroll.aspx?Type=Salary&Entry=Payroll")%>' target="_blank">
                                                <div class="twelve small-3 columns twitter-feed-box space">
                                                    <div class="color-100">

                                                        <span class="box-title incolor">Salary Sheet</span>
                                                        <br />
                                                        <i class="fa-plus-square-o fa fa-4x"></i>
                                                        <%-- <div class="wd-tweets"></div>--%>
                                                    </div>
                                                </div>
                                            </a>
                                             <a href='<%=this.ResolveUrl("~/F_81_Hrm/F_89_Pay/RptSalarySummary.aspx?Type=SalSum")%>' target="_blank">
                                                <div class="twelve small-3 columns twitter-feed-box space">
                                                    <div class="color-101">

                                                        <span class="box-title incolor">Salary Sum.</span>
                                                        <br />
                                                        <i class="fa-plus-square-o fa fa-4x"></i>
                                                        <%-- <div class="wd-tweets"></div>--%>
                                                    </div>
                                                </div>
                                            </a>

                                             
                                            <a href='<%=this.ResolveUrl("~/F_81_Hrm/F_89_Pay/EmpBankSalary.aspx?Type=Entry")%>' target="_blank">
                                                <div class="twelve small-3 columns twitter-feed-box space">
                                                    <div class=" color-100">

                                                        <span class="box-title incolor">Forwarding</span>
                                                        <br />
                                                        <i class="fa-plus-square-o fa  fa-4x"></i>
                                                        <%-- <div class="wd-tweets"></div>--%>
                                                    </div>
                                                </div>
                                            </a><a href='<%=this.ResolveUrl("~/F_81_Hrm/F_89_Pay/RpHRtPayroll.aspx?Type=Payslip")%>' target="_blank">
                                                <div class="twelve small-3 columns twitter-feed-box space">
                                                    <div class="color-115">

                                                        <span class="box-title incolor">Pay Slip</span>
                                                        <br />
                                                        <i class="fa-plus-square-o fa fa-4x"></i>
                                                        <%-- <div class="wd-tweets"></div>--%>
                                                    </div>
                                                </div>
                                            </a>

                                           
                                             <a href='<%=this.ResolveUrl("~/F_81_Hrm/F_83_Att/RptAttendenceSheet.aspx")%>' target="_blank">
                                                <div class="twelve small-6 columns twitter-feed-box space">
                                                    <div class="color-96">

                                                        <span class="box-title incolor">Attendance Information</span>
                                                        <br />
                                                        <i class="fa-plus-square-o fa fa-4x"></i>
                                                        <%--<div class="wd-tweets"></div>--%>
                                                    </div>
                                                </div>
                                            </a>

                                               <a href='<%=this.ResolveUrl("~/F_81_Hrm/F_82_App/RptEmpInformation.aspx?Type=EmpAllInfo")%>' target="_blank">
                                                <div class="twelve small-6 columns twitter-feed-box space">
                                                    <div class="color-98">

                                                        <span class="box-title incolor">Profile</span>
                                                        <br />
                                                        <i class="fa-plus-square-o fa fa-4x "></i>
                                                        <%-- <div class="wd-tweets"></div>--%>
                                                    </div>
                                                </div>
                                            </a>
                                          
                                              <a href='<%=this.ResolveUrl("~/F_81_Hrm/F_84_Lea/RptHREmpLeave.aspx?Type=EmpLeaveSt")%>' target="_blank">
                                                <div class="twelve small-4 columns twitter-feed-box space">
                                                    <div class="color-115">

                                                        <span class="box-title incolor">Leave Status (Ind)</span>
                                                        <br />
                                                        <i class="fa fa-bar-chart fa-4x"></i>
                                                        <%--<div class="wd-tweets"></div>--%>
                                                    </div>
                                                </div>
                                            </a>

                                              <a href='<%=this.ResolveUrl("~/F_81_Hrm/F_84_Lea/RptEmpLeaveStatus02.aspx?Type=EmpLeaveStatus")%>' target="_blank">
                                                <div class="twelve small-4 columns twitter-feed-box space">
                                                    <div class="color-100">

                                                        <span class="box-title incolor">Leave Status (Dept)</span>
                                                        <br />
                                                        <i class="fa fa-bar-chart fa-4x"></i>
                                                        <%--<div class="wd-tweets"></div>--%>
                                                    </div>
                                                </div>
                                            </a> 
                                              <a href='<%=this.ResolveUrl("~/F_81_Hrm/F_84_Lea/RptEmpLeaveStatus02.aspx?Type=MonWiseLeave")%>' target="_blank">
                                                <div class="twelve small-4 columns twitter-feed-box space">
                                                    <div class="color-99">

                                                        <span class="box-title incolor">Leave Month Wise</span>
                                                        <br />
                                                        <i class="fa fa-bar-chart fa-4x"></i>
                                                        <%--<div class="wd-tweets"></div>--%>
                                                    </div>
                                                </div>
                                            </a>
                                            <a href='<%=this.ResolveUrl("~/F_81_Hrm/F_85_Lon/EmpLoanStatus.aspx")%>' target="_blank">
                                                <div class="twelve small-3 columns twitter-feed-box space">
                                                    <div class=" color-96">

                                                        <span class="box-title incolor">Loan</span>
                                                        <br />
                                                        <i class="fa fa-4x fa-bar-chart fa-4x"></i>
                                                        <%--<div class="wd-tweets"></div>--%>
                                                    </div>
                                                </div>
                                            </a>
                                            
                                              <a href='<%=this.ResolveUrl("~/F_81_Hrm/F_92_Mgt/EmpStatus02.aspx?Type=EmpCon")%>' target="_blank">
                                           
                                                <div class="twelve small-3 columns twitter-feed-box space">
                                                    <div class="color-95">

                                                        <span class="box-title incolor">Confirmation</span>
                                                        <br />
                                                         <i class='fa-plus-square-o fa fa-4x '></i>
                                                        <%--<div class="wd-tweets"></div>--%>
                                                    </div>
                                                </div>
                                            </a>

                                          <a href='<%=this.ResolveUrl("~/F_81_Hrm/F_92_Mgt/EmpStatus02.aspx?Type=SepType")%>' target="_blank">
                                           
                                                <div class="twelve small-3 columns twitter-feed-box space">
                                                    <div class="color-115">

                                                        <span class="box-title incolor">Seperation</span>
                                                        <br />
                                                         <i class='fa-plus-square-o fa fa-4x'></i>
                                                        <%--<div class="wd-tweets"></div>--%>
                                                    </div>
                                                </div>
                                            </a>

                                             <a href='<%=this.ResolveUrl("~/F_81_Hrm/F_90_PF/RptAccProFund.aspx")%>' target="_blank">
                                           
                                                <div class="twelve small-3 columns twitter-feed-box space">
                                                    <div class="color-96">

                                                        <span class="box-title incolor">PF Accounts</span>
                                                        <br />
                                                         <i class='fa-plus-square-o fa fa-4x'></i>
                                                        <%--<div class="wd-tweets"></div>--%>
                                                    </div>
                                                </div>
                                            </a>

                                         
                                          


                                           
                                          



                                              <a href='<%=this.ResolveUrl("~/F_81_Hrm/F_97_MIS/RptNewJoingInfo.aspx")%>' target="_blank">
                                                <div class="twelve small-6 columns twitter-feed-box space">
                                                    <div class="color-99">

                                                        <span class="box-title incolor">Joining</span>
                                                        <br />
                                                        <i class="fa-plus-square-o fa fa-4x"></i>
                                                        <%-- <div class="wd-tweets"></div>--%>
                                                    </div>
                                                </div>
                                            </a>

                                          
                                          
                                          
                                            

                                               <a href='<%=this.ResolveUrl("~/F_81_Hrm/F_82_App/RptMyInterface.aspx?empid=")%>' target="_blank">
                                                <div class="twelve small-6 columns twitter-feed-box space">
                                                    <div class="color-93">

                                                        <span class="box-title incolor">My Interface</span>
                                                        <br />
                                                        <i class="fa-renren fa fa-4x"></i>
                                                        <%--<div class="wd-tweets"></div>--%>
                                                    </div>
                                                </div>
                                            </a>
                                             
                                        </div>
                                    </div>
                                </div>
                              
                                <div id="after-tiles" class="large-12 columns">
                                </div>
                            </div>
                        </asp:Panel>
                    </div>
                </div>
                <div class="clearfix"></div>
            </section>

        </div>
    </div>
     



    <script type='text/javascript' src='assets\js\jquery\jquery.js'></script>
    <script type='text/javascript' src='assets\js\jquery\jquery-migrate.min.js'></script>
    <script type='text/javascript' src='assets/js/comment-reply.min.js'></script>
    <script type='text/javascript' src='assetsjs/vendor/custom.modernizr.js'></script>
    <script type='text/javascript' src='assets\js\foundation.min.js'></script>
    <script type='text/javascript' src='assets\js\modernizr.custom.js'></script>
    <script type='text/javascript' src='assets\js\foundation\foundation.section.js'></script>
    <script type='text/javascript' src='assets\js\responsiveslides.js'></script>
    <script type='text/javascript' src='assets\js\scripts.js'></script>
    <!-- jQuery library -->
    <script src="assets\js\jquery.min.js"></script>

    <script type='text/javascript' src='assets\js\wd-ajax-load\js\load-posts.js'></script>
    <script type='text/javascript' src='assets/js/jquery.form.min.js'></script>
</asp:Content>

