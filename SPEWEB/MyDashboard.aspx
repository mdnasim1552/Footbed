<%@ Page Title="" Language="C#" MasterPageFile="~/SPEMaster.Master" AutoEventWireup="true" CodeBehind="MyDashboard.aspx.cs" Inherits="SPEWEB.MyDashboard" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <link rel='stylesheet' href='assets\css\style.css' type='text/css' media='all' />
    <link rel='stylesheet' href='assets\css\colors.css' type='text/css' media='all' />
    <link rel='stylesheet' href='assets\css\comments.css' type='text/css' media='all' />
    <link rel='stylesheet' id='responsiveslides-css' href='assets\css\responsiveslides.css' type='text/css' media='all' />
    <link rel='stylesheet' id='reponsive-css' href='assets\css\reponsive.css' type='text/css' media='all' />
    <link rel='stylesheet' id='animate-custom-css' href='assets\css\animate-custom.css' type='text/css' media='all' />

    <style>
        @font-face {
            font-family: 'Agency FB';
            src: url('fonts/AgencyFB-Bold.eot');
            src: url('fonts/AgencyFB-Bold.eot?#iefix') format('embedded-opentype'), url('fonts/AgencyFB-Bold.woff') format('woff'), url('fonts/AgencyFB-Bold.ttf') format('truetype');
            font-weight: bold;
            font-style: normal;
        }

        #DDPrintOpt {
            display: none;
        }

        /*body {
            background: #3495f3 url("~/assets/images/bg-3.jpg") repeat scroll 0 0 / cover !important;
        }*/
        html body .container {
            background: rgba(0, 0, 0, 0) url("Image/bg.PNG") repeat scroll 0 0 !important;
        }

        .sidepnaelMenu {
            height: 214px !important;
        }
        .fa {
	color: #fff;
	display: inline-block;
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
	text-rendering: auto;
	transform: translate(0px, 0px);
	float: left;
	margin-left: 15px;
}
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="myBody">

        <asp:Label ID="lblprintstk" runat="server" Visible="false"></asp:Label>

        <div id="spaces-main" class="pt-perspective">
            <section class="page-section home-page">
                <div class="container">
                    <div class="row metro-panel">
                        <div class="large-12 columns">
                            <div class="row menu-row boxShadow1">
                                <div class="large-12 columns">
                                    <div class="asit_ComLogo">
                                        <h1>
                                            <asp:Image ID="Image1" CssClass="Companylogo" runat="server" ImageUrl="~/Image/LOGO1.PNG" />
                                        </h1>
                                    </div>
                                    <div class="asit_ComInfo">
                                        <div class="AppscompName pading5px">
                                            <asp:Label ID="LblGrpCompany" CssClass="lblCompName" runat="server" Text="Label"></asp:Label>
                                            <asp:Label ID="lbladd" CssClass="LblGrpCompanyAdress" runat="server" Text="Label"> </asp:Label>
                                        </div>
                                    </div>

                                    <div class="col-md-3 pull-right">

                                        <div class="asit_userinfoPanel">

                                            <div class="userMenu pull-right">

                                                <ul>

                                                    <li class="dropdown">
                                                        <a data-toggle="dropdown" data-target="#" href="#">
                                                            <asp:Image ID="UserImg" CssClass=" img-rounded" ImageUrl="~/Image/LOGO1.JPG" runat="server" AlternateText="Log Out" Style="width: 60px; height: 50px;" />
                                                            <%--<span style="text-align: center; margin: 0; padding: 0; display: block; color: #C4302B;">Sign in By</span>--%></a>

                                                        <ul class="dropdown-menu userInformation" aria-labelledby="dropdownMenu">
                                                            <li>
                                                                <div class="well">
                                                                    <%--<asp:Image ID="UserImg2" ImageUrl="~/Image/LOGO1.JPG" runat="server" AlternateText="Image Not Found" Style="width: 70px; height: 75px; float: left; margin-right: 15px;" />--%>
                                                                    <p>
                                                                        <asp:Label ID="lblmoduleid" Style="display: none;" runat="server"></asp:Label>
                                                                    </p>

                                                                    <p>
                                                                        <asp:Label ID="lblLoginInfo" runat="server">User ID : ADMIN</asp:Label>
                                                                    </p>
                                                                    <p>Computer: nahid_pc, Chief Technology Officer</p>

                                                                    <div class="DivUserLogButton1">
                                                                        <a href="#" class="lbtnLogPanel" style="display: none;">Messages</a>&nbsp;&nbsp;&nbsp;
                                                   
                                                        <a href="#" class="lbtnLogPanel" style="display: none;">Daily Logs</a>&nbsp;&nbsp;&nbsp;&nbsp;
                                                   
                                                        <a href='<%=this.ResolveUrl("~/Login")%>' class="lbtnLogPanel" id="lbtnLogout">Sign Out</a>
                                                                    </div>

                                                                    <div class=" clearfix"></div>
                                                                </div>
                                                            </li>

                                                        </ul>
                                                        <div class=" clearfix"></div>


                                                    </li>


                                                </ul>
                                                <div class="clearfix"></div>
                                            </div>

                                        </div>
                                    </div>


                                </div>



                            </div>
                        </div>


                        <asp:Panel ID="PanTrading" Visible="false" runat="server">

                            <style>
                                html body {
                                    background-color: #E0E0E0 !important;
                                }

                                /*@font-face {
                                    font-family: 'Agency FB';
                                    src: url('~/fonts/AgencyFB-Bold.eot?#iefix') format('embedded-opentype'), 
                                        url('~/fonts/AgencyFB-Bold.woff') format('woff'), 
                                        url('~/fonts/AgencyFB-Bold.ttf') format('truetype'), 
                                        url('~/fonts/AgencyFB-Bold.svg#AgencyFB-Bold') format('svg');
                                    font-weight: normal;
                                    font-style: normal;
                                }*/

                                .box-title,
                                .featured-box-title,
                                .featured-title {
                                    position: absolute;
                                    bottom: 5px;
                                    left: 15px;
                                    font-size: 16px;
                                    font-weight: 300;
                                    text-shadow: 1px 0 1px rgba(0, 0, 0, 0.2);
                                    font-family: 'Agency FB' !important;
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
                                .boxShadow1 {
                                    /*box-shadow: 0 0 20px #888888;*/
                                    -moz-box-shadow: 0 5px 5px rgba(182, 182, 182, 0.75);
                                    -webkit-box-shadow: 0 5px 5px rgba(101, 125, 142, 0.75);
                                    box-shadow: 0 5px 5px rgba(101, 125, 142, 0.75);
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
                                .fadeInRightBig h3 {
	/* background: #232526; */
	/* background: -webkit-linear-gradient(to left, #232526 , #414345); */
	/* background: linear-gradient(to left, #232526 , #414345); */
	/* color: #fff; */
	/* font-family: 'Agency FB'; */
	/* font-size: 24px; */
	/* font-weight: bold; */
	/* line-height: 40px; */
	/* margin: 5px 0 0; */
	/* text-align: center; */
	/* text-decoration: underline; */
	font-family: 'Agency FB';
	background: #046971;
	/* border: 1px solid #699f44; */
	/* box-shadow: 0 0 4px 2px #bec9b6 inset; */
	color: #fff;
	font-family: "Agency FB";
	font-size: 22px;
	line-height: 40px;
	margin: 5px -3px 0;
	text-align: center;
	text-decoration: underline;
	font-weight: normal;
}
                                /*.fadeInRightBig h3 {
                                    background: #046971;
                                    border-top-left-radius: 5px;
                                    border-top-right-radius: 5px;
                                    color: #fff;
                                    font-family: 'Times New Roman';
                                    font-size: 22px;
                                    /* font-weight: bold; 
                                    line-height: 50px;
                                    padding: 0 0;
                                    text-decoration: none;
                                    text-align: center;
                                    margin: 0 -3PX;
                                }*/

                                .fa-4x {
                                    font-size: 20px !important;
                                    padding-bottom: 5px;
                                }

                                .metro-panel .space > div {
                                    margin: 3px -3px;
                                    min-height: 70px;
                                    overflow: hidden;
                                    position: relative;
                                    text-align: center !important;
                                }

                                .sidepnaelMenu {
                                    background: none !important;
                                    height: 228px;
                                    color: #000 !important;
                                }

                                .metro-panel .dropSideMenu a {
                                    color: #000 !important;
                                }

                                .sidepnaelMenu .fa {
                                    color: #000 !important;
                                }

                                .dropSideMenu li a {
                                    background: #fff none repeat scroll 0 0;
                                    color: #000;
                                    display: block;
                                    font-size: 13px;
                                    height: 68px !important;
                                    padding: 1px 20px !important;
                                    text-align: center;
                                    vertical-align: middle;
                                    width: 90px;
                                }

                                .dropSideMenu {
                                    margin-left: -182px !important;
                                    top: 0;
                                    right: 0;
                                    color: #000 !important;
                                    height: 228px;
                                    width: 406px !important;
                                }

                                .color-71 {
                                    background: #00AF50;
                                }

                                .color-72 {
                                    background: #2F74B5;
                                }

                                .col103 {
                                    color: #046971 !important;
                                }

                                .color-73 {
                                    background: #92D14F;
                                }
                              .color-74 {
	background: #5A9BD5;
}
                                .color-788 {
                                    background: #046971;
                                }
                                .color-75 {
	background: #538234;
}
                                .color-78 {
	background: #263A4A;
}
                                .color-77 {
	background: #808080;
}
.color-79 {
	background: #01B0F1;
}
  .color-80 {
	background: #BF8F00;
}
  .color-81 {
	background: #833D0C;
}
  .color-82 {
	background: #8397B0;
}
  .color-84 {
	background: #70AD46;
}
  .color-75 {
	background: #538234;
}
  .color-14 {
	background: #43b643;
}
  .metro-panel .space .boxdsh {
	left: 45% !important;
	top: 71px;
	padding-bottom: 10px;
}

  .metro-panel .boxdsh1 {
	margin-left: 35%;
}
  .color-14 {
	background: #089bad none repeat scroll 0 0;
	color: #fff;
}
                            </style>

                            <div class="row boxShadow" style="padding: 0 6px;">

                                <div id="before-tiles" class="large-12 columns">
                                </div>
                                <div class="boxInn">
                                    <div class="four large-4 columns">
                                        <h3>Modules</h3>
                                        <div class="row">
                                             <div class='twelve large-12 columns space featured blog-box medium'>
                                                <div class='color-71'>
                                                    <asp:LinkButton ID="lnkbtnStepOpra" runat="server" OnClick="lnkbtnStepOpra_Click">
                                                        <span class='box-title'>Steps Of Operation</span>
                                                        <br/>
                                                        <i class='fa-cubes fa fa-4x'></i>
                                                    </asp:LinkButton>
                                                </div>
                                            </div>

                                              <div class='six small-6 columns contact-box space'>
                                                <div class='color-72'>
                                                    <asp:LinkButton ID="lnkbtnAbp" runat="server" OnClick="lnkbtnAbp_Click">
                                                
                                                    <span class='box-title'>Business Planning</span>
                                                    <br/>
                                                    <i class='fa-sign-in fa fa-4x'></i>
                                                    </asp:LinkButton>
                                                </div>
                                            </div>

                                           

                                              <div class='six small-6 columns contact-box space'>
                                                <div class='color-80'>
                                                    <asp:LinkButton ID="linkbtnMerch" runat="server" OnClick="linkbtnMerch_Click">
                                                        <span class='box-title'>Merchandising </span>
                                                        <br>
                                                        <i class='fa-shopping-cart fa fa-4x'></i>
                                                    </asp:LinkButton>


                                                </div>
                                            </div>

                                              <div class='six small-3 columns contact-box space'>
                                                <div class='color-82'>

                                                    <asp:LinkButton ID="lnkbtnCostBud" runat="server" OnClick="lnkbtnCostBud_Click">
                                                    <span class='box-title'>Cost & Budget</span>
                                                    <br>
                                                    <i class='fa-ship fa fa-4x'></i>
                                                    </asp:LinkButton>
                                                </div>
                                            </div>

                                               <div class='six small-6 columns contact-box space'>
                                                <div class='color-78'>
                                                    <asp:LinkButton ID="lnkbtnProShiPlan" runat="server" OnClick="lnkbtnProShiPlan_Click">
                                                    <span class='box-title'>Master  Plan</span>
                                                    <br>
                                                    <i class='fa-suitcase fa fa-4x'></i>
                                                    </asp:LinkButton>
                                                </div>
                                            </div>

                                              <div class='six small-3 columns contact-box space'>
                                                <div class='color-788''>
                                                    <asp:LinkButton ID="lnkbtnCommer" runat="server" OnClick="lnkbtnCommer_Click">
                                                    <span class='box-title'>Commercial</span>
                                                    <br>
                                                    <i class='fa-shopping-cart fa fa-4x'></i>
                                                    </asp:LinkButton>
                                                </div>
                                            </div>

                                              <div class='six small-3 columns contact-box space'>
                                                <div class='color-81'>
                                                    <asp:LinkButton ID="LinkButton1" runat="server" OnClick="lnkbtnProcur_Click">
                                                    <span class='box-title'>Procurement</span>
                                                    <br>
                                                    <i class='fa-shopping-cart fa fa-4x'></i>
                                                    </asp:LinkButton>
                                                </div>
                                            </div>

                                           <div class='six small-3 columns contact-box space'>
                                                <div class='color-77'>
                                                    <asp:LinkButton ID="lnkbtnRowMatInventory" runat="server" OnClick="lnkbtnRowMatInventory_Click">
                                                    <span class='box-title'>RM Inventory</span>
                                                    <br>
                                                    <i class='fa-users fa fa-4x'></i>
                                                    </asp:LinkButton>
                                                </div>
                                            </div>

                                              <div class='six small-6 columns contact-box space'>
                                                <div class='color-79'>
                                                    <asp:LinkButton ID="linkbtnprodMin" runat="server" OnClick="linkbtnprodMin_Click">
                                                    <span class='box-title'>Production Plan</span>
                                                    <br>
                                                    <i class='fa-shopping-cart fa fa-4x'></i>
                                                    </asp:LinkButton>
                                                </div>
                                            </div>

                                              
                                              <div class='twelve small-3 columns twitter-feed-box space'>
                                                <div class='color-788'>
                                                    <asp:LinkButton ID="lnkbtnGInventory" runat="server" OnClick="lnkbtnGInventory_Click">
                                                    <span class='box-title'>FG Inventory</span>
                                                    <br>
                                                    <i class='fa-money fa fa-4x'></i>
                                                    </asp:LinkButton>
                                                </div>
                                            </div>

                                               <div class='six small-3 columns contact-box space'>
                                                <div class='color-72'>
                                                    <asp:LinkButton ID="LinkButton2" runat="server" OnClick="lnkbtnExp_Click">
                                                
                                                    <span class='box-title'>Export </span>
                                                    <br/>
                                                    <i class='fa-sign-in fa fa-4x'></i>
                                                    </asp:LinkButton>
                                                </div>
                                            </div>

                                            
                                            
                                         
                                              <div class='six small-3 columns contact-box space'>
                                                <div class='color-74'>
                                                    <asp:LinkButton ID="lnkbtnFixedAss" runat="server" OnClick="lnkbtnFixedAss_Click">
                                                        <span class='box-title'>Fixed Assets</span>
                                                        <br>
                                                        <i class='fa-shopping-cart fa fa-4x'></i>
                                                    </asp:LinkButton>
                                                </div>
                                            </div>

                                           <div class='six small-3 columns contact-box space'>
                                                <div class='color-75'>
                                                    <asp:LinkButton ID="lnkbtnAccounts" runat="server" OnClick="lnkbtnAccounts_Click">
                                                   <span class='box-title'>Accounts</span>
                                                    <br>
                                                    <i class='fa-shopping-cart fa fa-4x'></i>
                                                    </asp:LinkButton>
                                                </div>
                                            </div>


                                              <div class='six small-6 columns contact-box space'>
                                                <div class='color-72'>
                                                    <asp:LinkButton ID="LinkButton5" runat="server" OnClick="lnkbtnMM_Click">
                                                
                                                    <span class='box-title'>Management </span>
                                                    <br/>
                                                    <i class='fa-sign-in fa fa-4x'></i>
                                                    </asp:LinkButton>
                                                </div>
                                            </div>

                                              <div class='six small-3 columns contact-box space'>
                                                <div class='color-75'>
                                                    <asp:LinkButton ID="lnkbtnProc" runat="server" OnClick="lnkbtnHR_Click">
                                                    <span class='box-title'>HR</span> <br/>
                                                   <i class='fa-tags fa fa-4x'></i>
                                                    </asp:LinkButton>
                                                </div>
                                            </div>

                                              <div class='six small-3 columns contact-box space'>
                                                <div class='color-77'>
                                                    <asp:LinkButton ID="LinkButton6" runat="server" OnClick="lnkbtnKPI_Click">
                                                    <span class='box-title'>KPI</span> <br/>
                                                   <i class='fa-tags fa fa-4x'></i>
                                                    </asp:LinkButton>
                                                </div>
                                            </div>

                                            

                                          <%--  <div class='six small-6 columns contact-box space'>
                                                <div class='color-77'>
                                                    <asp:LinkButton ID="lnkbtnProc" runat="server" OnClick="lnkbtnProc_Click">
                                                    <span class='box-title'>Quotation</span> <br/>
                                                   <i class='fa-tags fa fa-4x'></i>
                                                    </asp:LinkButton>
                                                </div>
                                            </div>
                                          --%>
                                           

                                        </div>
                                        <br />
                                    </div>

                                    <div class="four large-4 columns">
                                        <h3>DashBoard</h3>

                                        <div class="row">
                                            <div class='six small-12 columns contact-box space'>
                                                <div class='color-14'>
                                                    <a href="<%=this.ResolveUrl("~/DeafultMenu?Type=8010")%>">
                                                        <span class='box-title'>General Flowchart</span>
                                                        <br>
                                                        <i class='fa-shopping-cart fa fa-4x'></i>
                                                    </a>
                                                </div>
                                            </div>
                                            <div class='six small-6 columns contact-box space'>
                                                <div class='color-84'>
                                                    <a href="<%=this.ResolveUrl("~/F_11_Pro/PurInformation")%>">
                                                        <span class='box-title'>HRM Flowchart</span>
                                                        <br>
                                                        <i class='fa-credit-card fa fa-4x'></i>
                                                    </a>
                                                </div>
                                            </div>
                                            

                                            <div class='six small-6 columns contact-box space'>
                                                <div class='color-72'>
                                                    <asp:LinkButton ID="LinkButton3" runat="server" OnClick="lnkbtnMM_Click">
                                                
                                                    <span class='box-title'>Settings </span>
                                                    <br/>
                                                    <i class='fa-usd-in fa fa-4x'></i>
                                                    </asp:LinkButton>


                                                   
                                                </div>
                                            </div>

                                            
                                            <div class='twelve small-12 columns twitter-feed-box space' style="padding-top: 9px;">
                                                <div class="sidepnaelMenu">

                                                    <div class="btn-group" id="sidepnaelMenu">
                                                        <a class="btn dropdown-toggle" data-toggle="dropdown" href="#" title="Open Menu">
                                                            <i class="fa fa-bars glypSideMenu" style="margin-left: 0 !important;"></i>

                                                        </a>
                                                        <ul class="dropdown-menu dropSideMenu">

                                                            <li><a href="Image/ASITProfile.pdf" target="_blank"><i class="fa fa-user-secret dropFa"></i>
                                                                <br />
                                                                Profile</a></li>
                                                            <li>
                                                                <a href="<%=this.ResolveUrl("~/Technology")%>" target="_blank"><i class="fa fa-users dropFa"></i>
                                                                    <br />
                                                                    Tools</a>

                                                            </li>
                                                            <li><a href="Image/MicroVsOrac.pdf" target="_blank"><i class="fa fa-user-secret dropFa"></i>
                                                                <br />
                                                                Microsoft
                        <br />
                                                                Vs Oracle</a></li>
                                                            <li><a href="<%=this.ResolveUrl("~/Clients_List1")%>" target="_blank"><i class="fa fa-sitemap dropFa"></i>
                                                                <br />
                                                                Client
                        <br />
                                                                List</a></li>
                                                            <li><a href="WorkOrder" target="_blank"><i class="fa fa-file dropFa"></i>
                                                                <br />
                                                                Work Order</a></li>
                                                            <li><a href="#"><i class="fa fa-info dropFa"></i>
                                                                <br />
                                                                Notification</a></li>
                                                            <li><a href="#"><i class="fa fa-user dropFa"></i>
                                                                <br />
                                                                Online User</a></li>
                                                            <li><a href="#supportPart" onclick="load_modal()"><i class="fa fa-phone dropFa"></i>
                                                                <br />
                                                                Support</a></li>
                                                            <li><a href="<%=this.ResolveUrl("~/F_21_GAcc/SupCustTaxVat")%>"><i class="fa fa-question dropFa"></i>
                                                                <br />
                                                                Help</a></li>
                                                        </ul>
                                                        <!-- Small modal -->


                                                    </div>


                                                    <div id="supportPart" class="modal fade bs-example-modal-sm" tabindex="-1" role="dialog" aria-labelledby="myLargeModalLabel" aria-hidden="true">
                                                        <div class="modal-dialog modal-sm">
                                                            <div class="modal-content well">
                                                                <div class="panel-heading">
                                                                    <h2>Online Support</h2>
                                                                </div>
                                                                <h3>Software Help:- Md Mahbubur Rahman (Raihan): 01917792844</h3>
                                                                <h3>Technical Help:- Mostak 0177545613</h3>
                                                                <div class="modal-footer">
                                                                    <button type="button" class="btn btn-danger" data-dismiss="modal">Close</button>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class='six small-6 columns contact-box space'>
                                                <div class='color-79'>
                                                    <a href="<%=this.ResolveUrl("~/F_35_GrAcc/RptIndvRealGraph")%>">
                                                        <span class='box-title'>Overall</span>
                                                        <br>
                                                        <i class='fa-tachometer fa fa-4x'></i>
                                                    </a>
                                                </div>
                                            </div>
                                            <div class='twelve small-6 columns twitter-feed-box space'>
                                                <div class='color-75'>
                                                   
                                                    <a href="<%=this.ResolveUrl("~/StepofOperation")%>">
                                                        <span class='box-title'>Group Information</span>
                                                        <br>
                                                        <i class='fa-tachometer fa fa-4x'></i>
                                                    </a>
                                                </div>
                                            </div>
                                            <%--<div class='six small-6 columns contact-box space'>
                                            <div class='color-1'>
                                                <a href='#'>
                                                    <span class='box-title'>Production</span>
                                                    <br>
                                                    <i class='fa-database fa fa-4x'></i>
                                                </a>
                                            </div>
                                        </div>--%>
                                        </div>
                                    </div>
                                    <div class="four large-4 columns">
                                        <h3>
                                             <a href="<%=this.ResolveUrl("~/F_99_Allinterface/HRDashboard")%>">
                                            Work Interface
                                                 </a>
                                        </h3>
                                        <div class="row">

                                            <div class='six small-3 columns contact-box space'>
                                                <div class='color-80'>

                                                    <a href="<%=this.ResolveUrl("~/F_01_Mer/RptMerChanInterface?Type=Merchan")%>">
                                                        <span class='box-title'>Merchandising</span>
                                                        <br />
                                                        <i class='fa-envelope-square fa fa-4x'></i>
                                                    </a>
                                                </div>
                                            </div>
                                             <div class='six small-3 columns contact-box space'>
                                                <div class='color-78'>

                                                    <a href="<%=this.ResolveUrl("~/F_01_Mer/RptMerChanInterface?Type=PD")%>">
                                                        <span class='box-title'>PD</span>
                                                        <br />
                                                        <i class='fa-envelope-square fa fa-4x'></i>
                                                    </a>
                                                </div>
                                            </div>
                                            <div class='six small-3 columns contact-box space'>
                                                <div class='color-80'>

                                                    <a href="<%=this.ResolveUrl("~/F_11_RawInv/RptWareHouseInterface")%>">
                                                        <span class='box-title'>Warehouse</span>
                                                        <br />
                                                        <i class='fa-envelope-square fa fa-4x'></i>
                                                    </a>
                                                </div>
                                            </div>
                                            <div class='six small-3 columns contact-box space'>
                                                <div class='color-72'>

                                                    <a href="<%=this.ResolveUrl("~/F_19_EXP/RptExportInterface")%>">
                                                        <span class='box-title'>Export</span>
                                                        <br />
                                                        <i class='fa-envelope-square fa fa-4x'></i>
                                                    </a>
                                                </div>
                                            </div>


                                            <div class='six small-4 columns contact-box space'>
                                                <div class='color-80'>

                                                    <a href="<%=this.ResolveUrl("~/F_10_Procur/RptPurInterfaceLocal")%>">
                                                        <span class='box-title'>Local Procurement</span>
                                                        <br />
                                                        <i class='fa-envelope-square fa fa-4x'></i>
                                                    </a>
                                                </div>
                                            </div>
                                            <div class='six small-5 columns contact-box space'>
                                                <div class='color-72'>

                                                    <a href="<%=this.ResolveUrl("~/F_09_Commer/RptLCInterface")%>">
                                                        <span class='box-title'>Import (Foreign/Local) </span>
                                                        <br />
                                                        <i class='fa-envelope-square fa fa-4x'></i>

                                                    </a>



                                                </div>
                                            </div>
                                            <div class='six small-3 columns contact-box space'>
                                                <div class='color-71'>
                                                    <a href="<%=this.ResolveUrl("~/F_34_Mgt/RptAdminInterface")%>">
                                                        <span class='box-title'>Management</span>
                                                        <br />
                                                        <i class='fa-envelope-square fa fa-4x'></i>
                                                    </a>
                                                </div>
                                            </div>

                                            
                                            <div class="twelve small-3 columns twitter-feed-box space">
                                                <div class="color-78">
                                                    <a href="<%=this.ResolveUrl("~/F_15_Pro/ProductionInterface?Type=FG")%>">
                                                        <span class="box-title">Production</span>
                                                        <br />
                                                        <i class="fa-envelope-square fa fa-4x"></i>

                                                    </a>
                                                </div>
                                            </div>
                                                <div class="twelve small-3 columns twitter-feed-box space">
                                                <div class="color-71">
                                                    <a href="<%=this.ResolveUrl("~/F_15_Pro/ProductionInterfaceSemi?Type=SemiFG")%>">
                                                        <span class="box-title">Production Semi</span>
                                                       
                                                        <i class="fa-envelope-square fa fa-4x"></i>

                                                    </a>
                                                </div>
                                            </div>

                                            <div class="six small-3 columns twitter-feed-box space">
                                                <div class="color-788">
                                                    <a href="<%=this.ResolveUrl("~/F_15_DPayReg/GenBillInterface")%>">
                                                        <span class="box-title">General Bill</span>
                                                        <br />
                                                        <i class="fa-envelope-square fa fa-4x"></i>

                                                    </a>
                                                </div>
                                            </div>
                                              <div class="six small-3 columns twitter-feed-box space">
                                                <div class="color-78">
                                                    <a href="<%=this.ResolveUrl("~/F_21_GAcc/AllVoucherTopSheet")%>">
                                                        <span class="box-title">Voucher 360 <sup>0</span>
                                                        <br />
                                                        <i class="fa-envelope-square fa fa-4x"></i>

                                                    </a>
                                                </div>
                                            </div>

                                        </div>

                                        <div class="row">
                                            <div class="twelve small-4 columns twitter-feed-box space">
                                                <div class="color-80">
                                                    <a href="<%=this.ResolveUrl("~/F_15_DPayReg/BillRegInterface")%>">
                                                        <span class="box-title">Bill Register</span>
                                                        <br />
                                                        <i class="fa-database fa fa-4x"></i>
                                                    </a>

                                                </div>
                                            </div>

                                            
                                            <div class='six small-4 columns contact-box space'>
                                                <div class='color-71'>
                                                    <a href="<%=this.ResolveUrl("~/F_23_MAcc/AccountInterface")%>">
                                                        <span class='box-title'>Accounts</span>
                                                        <br />
                                                        <i class='fa-envelope-square fa fa-4x'></i>
                                                    </a>
                                                </div>
                                            </div>


                                          <div class="twelve small-4 columns twitter-feed-box space">
                                                <div class="color-74">

                                                      <a href="<%=this.ResolveUrl("~/F_81_Hrm/F_92_Mgt/InterfaceHR")%>">
                                                        <span class='box-title'>HRM</span>
                                                        <br />
                                                        <i class='fa-envelope-square fa fa-4x'></i>
                                                    </a>
                                                   

                                                </div>
                                            </div>
                                             <%-- <div class="twelve small-4 columns twitter-feed-box space">
                                                <div class="color-79">
                                                    <a href="<%=this.ResolveUrl("~/F_14_Pro/PurInformation")%>">
                                                        <span class="box-title">Purchase</span>
                                                        <br />
                                                        <i class="fa-database fa fa-4x"></i>
                                                    </a>
                                                </div>
                                            </div>--%>
                                        </div>






                                        <div class="row">
                                        

                                            <div class='twelve small-12 columns twitter-feed-box space' style="height: 146px;">
                                                <div class='color-14' style="padding-bottom:50px;">
                                                   <%--  <a href="<%=this.ResolveUrl("~/AllGraph")%>">
                                                        <span class="box-title">DashBoard</span>
                                                      <br/> 
                                                        <i class="fa-folder-open fa fa-4x boxdsh1"></i>
                                                          <br/>  <br/>  <br/> 
                                                    </a>--%>
                                                   <%-- <asp:LinkButton ID="lnkGeneral" runat="server" OnClick="lnkbtnGeneral_Click">--%>
                                                    <a href="<%=this.ResolveUrl("~/AllGraph")%>">
                                                         <br/>  <br/>  <br/> 
                                                        <span class='box-title boxtex boxdsh'>DashBoard</span>
                                                         <br/> 
                                                        <i class='fa-folder-open fa fa-4x boxdsh1'></i> 

                                                          <br/>   
                                                         </a>
                                                    <%--</asp:LinkButton>--%>
                                                </div>
                                            </div>
                                            <%--<div class="twelve small-12 columns twitter-feed-box space">
                                                <div class="color-80">
                                                    <a href="<%=this.ResolveUrl("~/F_46_GrMgtInter/RptGrpDailyReportJq")%>">
                                                        <span class="box-title">Group Operation</span>
                                                        <br />
                                                        <i class="fa-pie-chart fa fa-4x"></i>
                                                    </a>
                                                </div>
                                            </div>--%>
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

