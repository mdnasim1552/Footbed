<%@ Page Title="" Language="C#" MasterPageFile="~/SPEMaster.Master" AutoEventWireup="true" CodeBehind="DeafultMenu.aspx.cs" Inherits="SPEWEB.DeafultMenu" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <style>
        /****Now the CSS***/
        * {
            margin: 0;
            padding: 0;
        }

        .accImg {
            margin: 0 auto;
        }

        .tree {
            margin: 0 auto;
        }

            .tree ul {
                padding-top: 20px;
                padding-bottom: 20px;
                position: relative;
                transition: all 0.5s;
                -webkit-transition: all 0.5s;
                -moz-transition: all 0.5s;
            }

            .tree li {
                float: left;
                text-align: center;
                list-style-type: none;
                position: relative;
                padding: 20px 5px 0 5px;
                transition: all 0.5s;
                -webkit-transition: all 0.5s;
                -moz-transition: all 0.5s;
            }

                /*We will use ::before and ::after to draw the connectors*/

                .tree li::before, .tree li::after {
                    content: '';
                    position: absolute;
                    top: 0;
                    right: 50%;
                    border-top: 1px solid #ccc;
                    width: 50%;
                    height: 20px;
                }

                .tree li::after {
                    right: auto;
                    left: 50%;
                    border-left: 1px solid #ccc;
                    /*background:url(image/LongRightArrow_L.gif) no-repeat -10px center;*/
                    z-index: 500;
                }

                /*We need to remove left-right connectors from elements without 
any siblings*/
                .tree li:only-child::after, .tree li:only-child::before {
                    display: none;
                }

                /*Remove space from the top of single children*/
                .tree li:only-child {
                    padding-top: 0;
                }

                /*Remove left connector from first child and 
right connector from last child*/
                .tree li:first-child::before, .tree li:last-child::after {
                    border: 0 none;
                }
                /*Adding back the vertical connector to the last nodes*/
                .tree li:last-child::before {
                    border-right: 1px solid #ccc;
                    border-radius: 0 5px 0 0;
                    -webkit-border-radius: 0 5px 0 0;
                    -moz-border-radius: 0 5px 0 0;
                }

                .tree li:first-child::after {
                    border-radius: 5px 0 0 0;
                    -webkit-border-radius: 5px 0 0 0;
                    -moz-border-radius: 5px 0 0 0;
                }

            /*Time to add downward connectors from parents*/
            .tree ul ul::before {
                content: '';
                position: absolute;
                top: 0;
                left: 50%;
                border-left: 1px solid #ccc;
                width: 0;
                height: 20px;
            }

            .tree li a {
                font-family: Cambria;
                -moz-box-shadow: inset 0px 1px 0px 0px #ffffff;
                -webkit-box-shadow: inset 0px 1px 0px 0px #ffffff;
                box-shadow: inset 0px 1px 0px 0px #ffffff;
                background: -webkit-gradient(linear, left top, left bottom, color-stop(0.05, #f9f9f9), color-stop(1, #e9e9e9));
                background: -moz-linear-gradient(top, #f9f9f9 5%, #e9e9e9 100%);
                background: -webkit-linear-gradient(top, #f9f9f9 5%, #e9e9e9 100%);
                background: -o-linear-gradient(top, #f9f9f9 5%, #e9e9e9 100%);
                background: -ms-linear-gradient(top, #f9f9f9 5%, #e9e9e9 100%);
                background: linear-gradient(to bottom, #f9f9f9 5%, #e9e9e9 100%);
                filter: progid:DXImageTransform.Microsoft.gradient(startColorstr='#f9f9f9', endColorstr='#e9e9e9',GradientType=0);
                background-color: #f9f9f9;
                -moz-border-radius: 6px;
                -webkit-border-radius: 6px;
                border-radius: 6px;
                border: 1px solid #dcdcdc;
                display: inline-block;
                cursor: pointer;
                color: #000000;
                font-weight: bold;
                font-size: 18px;
                margin: auto;
                padding: 12px 5px;
                width: 175px;
                text-decoration: none;
                transition: all 0.5s ease;
            }

                /*Time for some hover effects*/
                /*We will apply the hover effect the the lineage of the element also*/
                .tree li a:hover {
                    background: #F1FEDD;
                    background-image: -webkit-linear-gradient(top, #F1FEDD, #DBFDA8);
                    background-image: -moz-linear-gradient(top, #F1FEDD, #DBFDA8);
                    background-image: -ms-linear-gradient(top, #F1FEDD, #DBFDA8);
                    background-image: -o-linear-gradient(top, #F1FEDD, #DBFDA8);
                    background-image: linear-gradient(to bottom, #F1FEDD, #DBFDA8);
                    color: #000;
                }
                    /*Connector styles on hover*/
                    .tree li a:hover + ul li::after,
                    .tree li a:hover + ul li::before,
                    .tree li a:hover + ul::before,
                    .tree li a:hover + ul ul::before {
                        border-color: #94a0b4;
                    }

        .commmonTree {
            /*width: 60%;*/
            margin: 0 auto;
        }

            .commmonTree .titelPanel {
                -moz-box-shadow: inset 0px 1px 0px 0px #ffffff;
                -webkit-box-shadow: inset 0px 1px 0px 0px #ffffff;
                box-shadow: inset 0px 1px 0px 0px #ffffff;
                background: -webkit-gradient(linear, left top, left bottom, color-stop(0.05, #f9f9f9), color-stop(1, #e9e9e9));
                background: -moz-linear-gradient(top, #f9f9f9 5%, #e9e9e9 100%);
                background: -webkit-linear-gradient(top, #f9f9f9 5%, #e9e9e9 100%);
                background: -o-linear-gradient(top, #f9f9f9 5%, #e9e9e9 100%);
                background: -ms-linear-gradient(top, #f9f9f9 5%, #e9e9e9 100%);
                background: linear-gradient(to bottom, #f9f9f9 5%, #e9e9e9 100%);
                filter: progid:DXImageTransform.Microsoft.gradient(startColorstr='#f9f9f9', endColorstr='#e9e9e9',GradientType=0);
                background-color: #f9f9f9;
                -moz-border-radius: 6px;
                -webkit-border-radius: 6px;
                border-radius: 6px;
                border: 1px solid #dcdcdc;
                display: inline-block;
                cursor: pointer;
                color: #000000;
                font-family: arial;
                font-size: 24px;
                font-weight: bold;
                width: 200px;
                padding: 20px 12px;
                text-decoration: none;
                text-shadow: 0px 1px 0px #ffffff;
            }

                .commmonTree .titelPanel:hover {
                    background: -webkit-gradient(linear, left top, left bottom, color-stop(0.05, #e9e9e9), color-stop(1, #f9f9f9));
                    background: -moz-linear-gradient(top, #e9e9e9 5%, #f9f9f9 100%);
                    background: -webkit-linear-gradient(top, #e9e9e9 5%, #f9f9f9 100%);
                    background: -o-linear-gradient(top, #e9e9e9 5%, #f9f9f9 100%);
                    background: -ms-linear-gradient(top, #e9e9e9 5%, #f9f9f9 100%);
                    background: linear-gradient(to bottom, #e9e9e9 5%, #f9f9f9 100%);
                    filter: progid:DXImageTransform.Microsoft.gradient(startColorstr='#e9e9e9', endColorstr='#f9f9f9',GradientType=0);
                    background-color: #e9e9e9;
                }





            .commmonTree .smllMenuBox {
                margin-right: 40px;
                padding: 12px;
                width: 150px !important;
            }

            .commmonTree .smllMenuBox1 {
                margin-left: 40px;
                padding: 12px;
                width: 150px !important;
            }

        .treeCenter {
            width: 100%;
        }

        .treeCenter2 {
            width: 74%;
        }

        .treePnlBudgt {
            width: 58%;
        }

        .treePnlProd {
            width: 58%;
        }

        .treePnlPur {
            width: 85%;
        }

        .menuImg {
            border: 1px solid #85e11e;
            border-radius: 18%;
            height: 15px;
            margin-right: 10px;
            padding: 0;
            width: 15px;
        }

        .flowMenu ul {
            margin-left: 25px;
        }

            .flowMenu ul li {
                list-style: none;
                padding: 5px 0;
            }

                .flowMenu ul li a {
                    padding-bottom: 8px;
                    color: #000;
                    font-size: 14px;
                    font-weight: normal;
                    text-shadow: 1px 0 1px rgba(0, 0, 0, 0.2);
                    font-family: 'Times New Roman';
                }

        .flowMenu h3 {
            background: #046971;
            border-top-left-radius: 5px;
            border-top-right-radius: 5px;
            color: #fff;
            font-family: AR CENA;
            font-size: 18px;
            /*font-weight: bold;*/
            line-height: 40px;
            margin: 5px 0 0;
            padding: 0 0;
            text-decoration: none;
            text-align: center;
        }


        /*Thats all. I hope you enjoyed it.
Thanks :)*/
        ul.dashCir {
            width: 75%;
            margin: 0 auto;
        }

            ul.dashCir li {
                width: 30%;
                float: left;
                margin: 0 3px;
                border: none;
            }

            ul.dashCir span img {
                border: 1px solid #85e11e;
                border-radius: 50%;
                display: block;
                height: 50px;
                margin: 0 auto;
                padding: 4px;
                width: 50px;
            }

        .dashCir li a {
            display: block;
            text-align: center;
            font-family: ar_cenaregular !important;
            font-size: 12px !important;
        }

        .myrbtlist tr {
            margin-bottom: 5px;
            display: block;
        }

        .myrbtlist input[type="checkbox"], input[type="radio"] {
            /*display: block;
            text-align: center;
            height: 50px;
            border: 1px solid #85e11e;
            border-radius: 50%;
            margin: 0 auto !important;*/
            border: 1px solid #85e11e;
        }

       


        /*.myrbt input[type="radio"]:first-child label {
            background: #fff url(Images/MFGLOGO/trmfg.jpg) no-repeat scroll center 8px;
            background-size: 43%;
        }*/




        .myrbtlist label {
            position: absolute;
          
            left: 70px;
            font-weight: normal;
            color: #000;
            font-family: "Times New Roman";
            font-size: 14px;
            font-weight: normal;
            text-shadow: 1px 0 1px rgba(0, 0, 0, 0.2);
        }

        /*.myrbtlist tr:first-child td:first-child label {
            background: #fff url("Images/MFGLOGO/trmfg.jpg") no-repeat scroll center 0px / 19%;
        }

        .myrbtlist tr:nth-child(2) td:first-child label {
            background: #fff url("Images/MFGLOGO/trmfg.jpg") no-repeat scroll center 0px / 19%;
        }*/


      
        /* Radio */

input[type="radio"] {
    /*background-color: #046971;
    background-image: -webkit-linear-gradient(0deg, transparent 20%, hsla(0,0%,100%,.7), transparent 80%),
                      -webkit-linear-gradient(90deg, transparent 20%, hsla(0,0%,100%,.7), transparent 80%);
    border-radius: 10px;
    box-shadow: inset 0 1px 1px hsla(0,0%,100%,.8),
                0 0 0 1px hsla(0,0%,0%,.6),
                0 2px 3px hsla(0,0%,0%,.6),
                0 4px 3px hsla(0,0%,0%,.4),
                0 6px 6px hsla(0,0%,0%,.2),
                0 10px 6px hsla(0,0%,0%,.2);
    cursor: pointer;
    display: inline-block;
   
    margin-right: 15px;
    position: relative;
    width: 15px;
    -webkit-appearance: none;*/
}
input[type="radio"]:after {
    /*background-color: #046971;
    border-radius: 25px;
    box-shadow: inset 0 0 0 1px hsla(0,0%,0%,.4),
                0 1px 1px hsla(0,0%,100%,.8);
    content: '';
    display: block;
    height: 7px;
    left: 4px;
    position: relative;
    top: 4px;
    width: 7px;*/
}
/*input[type="radio"]:checked:after {
    background-color: #046971;
    box-shadow: inset 0 0 0 1px hsla(0,0%,0%,.4),
                inset 0 2px 2px hsla(0,0%,100%,.4),
                0 1px 1px hsla(0,0%,100%,.8),
                0 0 2px 2px hsla(0,70%,70%,.4);
}*/










        .dashCir1 {
            margin: 0 83px !important;
        }
    </style>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="container  moduleItemWrp defaultMenuPart">
        <div class="row">
            <div class="col-md-12">
                <asp:Panel runat="server" ID="pnlBudget" Visible="false">
                    <div class="row ">
                        <div class="col-md-3 mfgacc2 ">
                            <ul class="sideMenu ">
                                <li><a href="#" style="background: #ffffff; border-radius: 0%; box-shadow: none; border: none;"></a></li>
                                <li><a class="nonBg" href="<%=this.ResolveUrl("#")%>" target="_blank">
                                    <img src="Image/int1.png" /><span style="padding: 0px; margin-left: 12px;">Interface</span></a></li>
                                <li><a href="#" style="background: #ffffff; border-radius: 0%; box-shadow: none; border: none;"></a></li>
                                <li><a href="#" style="background: #ffffff; border-radius: 0%; box-shadow: none; border: none;"></a></li>
                                <li><a href="#" style="background: #ffffff; border-radius: 0%; box-shadow: none; border: none;"></a></li>
                                <li><a href="#" style="background: #ffffff; border-radius: 0%; box-shadow: none; border: none;"></a></li>
                                <li>
                                    <a href="<%=this.ResolveUrl("~/F_03_StdCost/StdCostSheet.aspx?InputType=CostAnna")%>" target="_blank">Cost Analysis Sheet

                                    </a></li>


                                <li><a href="<%=this.ResolveUrl("~/GenPage.aspx?Type=03")%>" target="_blank">All Reports
                                </a></li>

                                <li><a href="DeafultMenu.aspx?Type=4110" target="_blank">Settings</a></li>

                            </ul>

                        </div>
                        <div class="col-md-9">


                            <div class="row menu10">

                                <table class="menuTb" width="200" border="0" cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td height="52"></td>
                                        <td></td>
                                        <td>&nbsp;</td>
                                        <td>
                                            <img class="iconImg mIconImg" src="Image/icon28.png" width="43" height="33" alt="x" /></td>
                                        <td class="mainTitelA menubg mMenu">Cost & Budget
                                        </td>
                                        <td>&nbsp;</td>
                                        <td></td>

                                        <td></td>
                                    </tr>
                                    <tr>
                                        <td>&nbsp;</td>
                                        <td></td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>
                                            <img class="arrowDown" src="Image/5.png" width="18" height="31" alt=" c" /></td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td></td>
                                    </tr>
                                    <tr>
                                        <td height="37"></td>
                                        <td></td>
                                        <td></td>
                                        <td>
                                            <img class="iconImg" src="Image/12.png" width="58" height="50" alt="x" /></td>
                                        <td class="mainTitelA mainTitelB"><a href="<%=this.ResolveUrl("~/F_15_Acc/AccSubCodeBook.aspx?InputType=ResCodePrint")%>" target="_blank">Create Revenue & Cost Code
                                        </a></td>
                                        <td></td>
                                        <td></td>
                                        <td></td>
                                    </tr>
                                    <tr>
                                        <td>&nbsp;</td>
                                        <td></td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>
                                            <img class="arrowDown" src="Image/5.png" width="18" height="31" alt=" c" />
                                        </td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td></td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <img class="iconImg" src="Image/icon19.png" width="43" height="33" alt="x" /></td>
                                        <td class="mainTitelA mainTitelB"><a href="<%=this.ResolveUrl("~/F_03_StdCost/StdCostSheet.aspx?InputType=CostAnna")%>" target="_blank">Input  Process Wise Standard Cost 



                                        </a></td>
                                        <td>
                                            <img class="arrowRight" src="Image/7.png" width="35" height="19" alt="x" /></td>
                                        <td>
                                            <img class="iconImg" src="Image/icon29.png" width="43" height="33" alt="x" /></td>

                                        <td class="mainTitelA mainTitelB"><a href="<%=this.ResolveUrl("~/F_03_StdCost/StdCostSheet.aspx?InputType=CostAnna")%>" target="_blank">Create  Analysis / Recipe



                                        </a></td>
                                        <td>
                                            <img class="arrowRight" src="Image/6.png" width="35" height="19" alt="x" /></td>
                                        <td>
                                            <img class="iconImg" src="Image/icon19.png" width="43" height="33" alt="x" /></td>
                                        <td class="mainTitelA mainTitelB"><a href="<%=this.ResolveUrl("~/F_03_StdCost/StdCostSheet.aspx?InputType=CostAnna")%>" target="_blank">Input Standard Revenue 

                                        </a></td>
                                    </tr>

                                </table>
                            </div>





                            <div class="row">

                                <div class="col-md-12 relatedItems ">
                                    <h3>Related Items</h3>


                                    <ul class="nav-pills">
                                        <li><a href="<%=this.ResolveUrl("~/F_15_Acc/AccPayment.aspx?tcode=99&tname=Payment Voucher&Type=Acc")%>" target="_blank"><span class=""></span>Cost List


                                        </a></li>
                                        <%--glyphicon glyphicon-unchecked--%>
                                        <li><a href="<%=this.ResolveUrl("~/F_15_Acc/AccInterComVoucher.aspx")%>" target="_blank"><span class=""></span>Revenue List

                                        </a></li>



                                    </ul>

                                </div>
                            </div>

                            <div class="clearfix"></div>
                        </div>




                    </div>

                    <div class="clearfix"></div>
                </asp:Panel>

                <asp:Panel runat="server" ID="pnlInv" Visible="false">

                    <div class="row ">
                        <div class="col-md-3 mfgacc2 ">
                            <ul class="sideMenu ">
                                <li><a href="#" style="background: #ffffff; border-radius: 0%; box-shadow: none; border: none;"></a></li>
                                <li><a class="nonBg" href="<%=this.ResolveUrl("~/F_11_Pro/PurInformation.aspx")%>" target="_blank">
                                    <img src="Image/int1.png" /><span style="padding: 0px; margin-left: 12px;">Interface</span></a></li>
                                <li><a href="#" style="background: #ffffff; border-radius: 0%; box-shadow: none; border: none;"></a></li>
                                <li><a href="#" style="background: #ffffff; border-radius: 0%; box-shadow: none; border: none;"></a></li>
                                <li><a href="#" style="background: #ffffff; border-radius: 0%; box-shadow: none; border: none;"></a></li>
                                <li><a href="#" style="background: #ffffff; border-radius: 0%; box-shadow: none; border: none;"></a></li>
                                <li><a href="F_15_Acc/RptAccDTransaction.aspx?Type=Accounts&TrMod=DTran" target="_blank">Daily Transaction

                                </a></li>

                                <li><a href="<%=this.ResolveUrl("~/GenPage.aspx?Type=09")%>" target="_blank">Store Ledger</a></li>
                                <li><a href="<%=this.ResolveUrl("~/GenPage.aspx?Type=09")%>" target="_blank">All Reports
                                </a></li>
                                <li><a href="#" style="background: #ffffff; border-radius: 0%; box-shadow: none; border: none;" target="_blank"></a></li>
                                <li><a href="#" style="background: #ffffff; border-radius: 0%; box-shadow: none; border: none;" target="_blank"></a></li>
                                <li><a href="<%=this.ResolveUrl("~/GenPage.aspx?Type=09")%>" target="_blank">Store Items</a></li>
                                <li><a href="F_15_Acc/AccOpening.aspx" target="_blank">Store Opening</a></li>
                                <li><a href="DeafultMenu.aspx?Type=4110" target="_blank">Settings</a></li>

                            </ul>

                        </div>
                        <div class="col-md-9">


                            <div class="row menu10">

                                <table class="menuTb" width="200" border="0" cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td height="52"></td>
                                        <td></td>
                                        <td>&nbsp;</td>
                                        <td>
                                            <img class="iconImg mIconImg" src="Image/icon20.png" width="43" height="33" alt="x" /></td>
                                        <td class="mainTitelA menubg mMenu mainTitelB">Materials Inventory
                                        </td>
                                        <td>&nbsp;</td>
                                        <td></td>

                                        <td></td>
                                    </tr>
                                    <tr>
                                        <td>&nbsp;</td>
                                        <td></td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>
                                            <img class="arrowDown" src="Image/5.png" width="18" height="31" alt=" c" /></td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td></td>
                                    </tr>
                                    <tr>
                                        <td height="37"></td>
                                        <td></td>
                                        <td></td>
                                        <td>
                                            <img class="iconImg" src="Image/12.png" width="58" height="50" alt="x" /></td>
                                        <td class="mainTitelA mainTitelB"><a href="#">Create Requisition From System
                                        </a></td>
                                        <td>
                                            <img class="arrowRight" src="Image/6.png" width="35" height="19" alt="x" /></td>
                                        <td>
                                            <img class="iconImg" src="Image/icon19.png" width="58" height="50" alt="x" /></td>
                                        <td class="mainTitelA mainTitelB">
                                            <a href="<%=this.ResolveUrl("~/F_07_Inv/StockPosition.aspx")%>" target="_blank">Re-Order Level  & EOQ</a> </td>
                                    </tr>
                                    <tr>
                                        <td>&nbsp;</td>
                                        <td></td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>
                                            <img class="arrowDown" src="Image/5.png" width="18" height="31" alt=" c" />
                                        </td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td></td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <img class="iconImg" src="Image/icon21.png" width="43" height="33" alt="x" /></td>
                                        <td class="mainTitelA"><a href="<%=this.ResolveUrl("~/F_07_Inv/PBMatIssueSingle.aspx")%>" target="_blank">Materials Issue 


                                        </a></td>
                                        <td>
                                            <img class="arrowRight" src="Image/6.png" width="35" height="19" alt="x" /></td>
                                        <td>
                                            <img class="iconImg" src="Image/11.png" width="43" height="33" alt="x" /></td>

                                        <td class="mainTitelA "><a href="<%=this.ResolveUrl("~/F_07_Inv/PurMRREntry.aspx?Type=Entry")%>" target="_blank">Materials Receive


                                        </a></td>
                                        <td>
                                            <img class="arrowRight" src="Image/7.png" width="35" height="19" alt="x" /></td>
                                        <td>
                                            <img class="iconImg" src="Image/icon12.png" width="43" height="33" alt="x" /></td>
                                        <td class="mainTitelA "><a href="<%=this.ResolveUrl("~/F_07_Inv/MaterialsTransfer.aspx")%>" target="_blank">Transfer
                                        </a></td>
                                    </tr>
                                    <tr>
                                        <td>&nbsp;</td>
                                        <td></td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>
                                            <img class="arrowDown" src="Image/5.png" width="18" height="31" alt=" c" />
                                        </td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td></td>
                                    </tr>
                                    <tr>
                                        <td></td>
                                        <td></td>
                                        <td></td>
                                        <td>
                                            <img class="iconImg" src="Image/icon14.png" width="43" height="33" alt="x" /></td>

                                        <td class="mainTitelA"><a href="<%=this.ResolveUrl("~/F_07_Inv/EntryLostSoldADes.aspx")%>" target="_blank">Sold & Destroy


                                        </a></td>
                                        <td></td>
                                        <td></td>
                                        <td></td>
                                    </tr>
                                </table>
                            </div>





                            <div class="row">

                                <div class="col-md-12 relatedItems ">
                                    <h3>Related Items</h3>


                                    <ul class="nav-pills">
                                        <li><a href="<%=this.ResolveUrl("~/F_15_Acc/AccPayment.aspx?tcode=99&tname=Payment Voucher&Type=Acc")%>" target="_blank"><span class=""></span>Purchase Return

                                        </a></li>
                                        <%--glyphicon glyphicon-unchecked--%>
                                        <li><a href="<%=this.ResolveUrl("~/F_15_Acc/AccInterComVoucher.aspx")%>" target="_blank"><span class=""></span>Expire Materials
                                        </a></li>
                                        <li><a href="<%=this.ResolveUrl("~/F_15_Acc/AccSales.aspx")%>" target="_blank"><span class=""></span>Materials Sold
                                        </a></li>
                                        <li><a href="<%=this.ResolveUrl("~/F_15_Acc/AccPurchase.aspx")%>" target="_blank"><span class=""></span>Materials History
                                        </a></li>


                                    </ul>

                                </div>
                            </div>

                            <div class="clearfix"></div>
                        </div>




                    </div>

                    <div class="clearfix"></div>
                </asp:Panel>

                <asp:Panel runat="server" ID="pnlImport" Visible="false">

                    <div class="row ">
                        <div class="col-md-3 mfgacc2 ">
                            <ul class="sideMenu ">
                                <li><a href="#" style="background: #ffffff; border-radius: 0%; box-shadow: none; border: none;"></a></li>
                                <li><a class="nonBg" href="<%=this.ResolveUrl("~/F_11_Pro/PurInformation.aspx")%>" target="_blank">
                                    <img src="Image/int1.png" /><span style="padding: 0px; margin-left: 12px;">Interface</span></a></li>
                                <li><a href="#" style="background: #ffffff; border-radius: 0%; box-shadow: none; border: none;"></a></li>
                                <li><a href="#" style="background: #ffffff; border-radius: 0%; box-shadow: none; border: none;"></a></li>
                                <li><a href="#" style="background: #ffffff; border-radius: 0%; box-shadow: none; border: none;"></a></li>
                                <li><a href="#" style="background: #ffffff; border-radius: 0%; box-shadow: none; border: none;"></a></li>
                                <li><a href="F_09_LCM/RptLCPosition.aspx?Type=LCPosition">Status Report
                                </a></li>

                                <li><a href="<%=this.ResolveUrl("~/F_23_SaM/RptComDues.aspx")%>">Statements of Accounts
                                </a></li>
                                <li><a href="<%=this.ResolveUrl("~/GenPage.aspx?Type=09")%>">More Reports</a></li>
                                <li><a href="#" style="background: #ffffff; border-radius: 0%; box-shadow: none; border: none;"></a></li>
                                <li><a href="#" style="background: #ffffff; border-radius: 0%; box-shadow: none; border: none;"></a></li>
                                <li><a href="#" style="background: #ffffff; border-radius: 0%; box-shadow: none; border: none;"></a></li>

                                <li><a href="F_15_Acc/AccOpening.aspx">Customer's Opening
                                </a></li>
                                <li><a href="DeafultMenu.aspx?Type=4110">Settings</a></li>

                            </ul>

                        </div>
                        <div class="col-md-9">


                            <div class="row menu10">

                                <table class="menuTb" width="200" border="0" cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td height="52"></td>
                                        <td></td>
                                        <td>&nbsp;</td>
                                        <td>
                                            <img class="iconImg mIconImg" src="Image/icon50.png" width="43" height="33" alt="x" /></td>
                                        <td class="mainTitelA menubg mMenu">Import</td>
                                        <td>&nbsp;</td>
                                        <td></td>

                                        <td></td>
                                    </tr>
                                    <tr>
                                        <td>&nbsp;</td>
                                        <td></td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>
                                            <img class="arrowDown" src="Image/5.png" width="18" height="31" alt=" c" /></td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td></td>
                                    </tr>
                                    <tr>
                                        <td height="37">
                                            <img class="iconImg" src="Image/icon17.png" width="58" height="50" alt="x" /></td>
                                        <td class="mainTitelA mainTitelB"><a href="<%=this.ResolveUrl("~/F_07_Inv/PurMktSurvey.aspx?Type=MktSurvey")%>" target="_blank">Comparative Statement</a></td>
                                        <td>
                                            <img class="arrowleft" src="Image/6.png" width="37" height="19" alt="x" /></td>
                                        <td>
                                            <img class="iconImg" src="Image/icon14.png" width="58" height="50" alt="x" /></td>
                                        <td class="mainTitelA "><a href="<%=this.ResolveUrl("~/F_07_Inv/StockPosition.aspx")%>" target="_blank">Requisition         Center

                                        </a></td>
                                        <td>
                                            <img class="arrowRight" src="Image/7.png" width="35" height="19" alt="x" /></td>
                                        <td>
                                            <img class="iconImg" src="Image/icon15.png" width="58" height="50" alt="x" /></td>
                                        <td class="mainTitelA"><a href="<%=this.ResolveUrl("~/F_15_Acc/AccCodeBook.aspx?InputType=Accounts")%>" target="_blank">Create L/C Code 
                                        </a></td>
                                    </tr>
                                    <tr>
                                        <td>&nbsp;</td>
                                        <td>
                                            <img class="arrowDown" src="Image/5.png" width="18" height="31" alt=" c" /></td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td></td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td></td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <img class="iconImg" src="Image/icon10.png" width="43" height="33" alt="x" /></td>
                                        <td class="mainTitelA mainTitelB"><a href="<%=this.ResolveUrl("~/F_15_Acc/GeneralAccounts.aspx?tcode=99&tname=Payment Voucher&Mod=Accounts")%>">Requisition Approval

                                        </a></td>
                                        <td>
                                            <img class="arrowRight" src="Image/7.png" width="35" height="19" alt="x" /></td>
                                        <td>
                                            <img class="iconImg" src="Image/icon51.png" width="43" height="33" alt="x" /></td>

                                        <td class="mainTitelA"><a href="<%=this.ResolveUrl("~/F_09_LCM/LCInformation.aspx?tname=order&tid=lc")%>">L/C Opening
                                        </a></td>
                                        <td>
                                            <img class="arrowRight" src="Image/7.png" width="35" height="19" alt="x" /></td>
                                        <td>
                                            <img class="iconImg" src="Image/3.png" width="43" height="33" alt="x" /></td>
                                        <td class="mainTitelA mainTitelB"><a href="<%=this.ResolveUrl("~/F_15_Acc/GeneralAccounts.aspx?tcode=99&tname=Payment Voucher&Mod=Accounts")%>">Payment Against L/C</a></td>
                                    </tr>
                                    <tr>
                                        <td>&nbsp;</td>
                                        <td></td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td></td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>
                                            <img class="arrowDown" src="Image/5.png" width="18" height="31" alt=" c" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <img class="iconImg" src="Image/icon19.png" width="43" height="33" alt="x" /></td>
                                        <td class="mainTitelA "><a href="<%=this.ResolveUrl("~/F_15_Acc/AccPurchaseFor.aspx")%>">Accounts Up-date </a></td>
                                        <td>
                                            <img class="arrowleft" src="Image/6.png" width="35" height="19" alt="x" /></td>
                                        <td>
                                            <img class="iconImg" src="Image/icon52.png" width="43" height="33" alt="x" /></td>

                                        <td class="mainTitelA"><a href="<%=this.ResolveUrl("~/F_09_LCM/LCInformation.aspx?tname=costing&tid=cst")%>">Auto Costing


                                        </a></td>
                                        <td>
                                            <img class="arrowleft" src="Image/6.png" width="35" height="19" alt="x" /></td>
                                        <td>
                                            <img class="iconImg" src="Image/11.png" width="43" height="33" alt="x" /></td>
                                        <td class="mainTitelA"><a href="<%=this.ResolveUrl("~/F_09_LCM/LCInformation.aspx?tname=receive&tid=lc")%>">Receive                                        </a></td>
                                    </tr>
                                </table>
                            </div>





                            <div class="row">

                                <div class="col-md-12 relatedItems ">
                                    <h3>Related Items</h3>


                                    <ul class="nav-pills">
                                        <li><a href="<%=this.ResolveUrl("~/F_15_Acc/AccPayment.aspx?tcode=99&tname=Payment Voucher&Type=Acc")%>"><span class=""></span>Supplier's List

                                        </a></li>
                                        <%--glyphicon glyphicon-unchecked--%>
                                        <li><a href="<%=this.ResolveUrl("~/F_15_Acc/AccInterComVoucher.aspx")%>"><span class=""></span>New Supplier

                                        </a></li>
                                        <li><a href="<%=this.ResolveUrl("~/F_15_Acc/AccSales.aspx")%>"><span class=""></span>New Supplier
                                        </a></li>
                                        <li><a href="<%=this.ResolveUrl("~/F_15_Acc/AccPurchase.aspx")%>"><span class=""></span>Return

                                        </a></li>
                                        <li><a href="<%=this.ResolveUrl("~/F_15_Acc/AccPurchaseFor.aspx")%>"><span class=""></span>Debit Note
                                        </a></li>
                                        <li><a href="<%=this.ResolveUrl("~/F_15_Acc/AccIsuUpdate.aspx")%>"><span class=""></span>Credit Note
                                        </a></li>


                                    </ul>

                                </div>
                            </div>

                            <div class="clearfix"></div>
                        </div>




                    </div>




                    <div class="clearfix"></div>
                </asp:Panel>
                <asp:Panel runat="server" ID="pnlExport" Visible="false">

                    <div class="row ">
                        <div class="col-md-3 mfgacc2 ">
                            <ul class="sideMenu ">
                                <li><a href="#" style="background: #ffffff; border-radius: 0%; box-shadow: none; border: none;"></a></li>
                                <li>
                                    <img src="Image/int1.png" /><span style="padding: 0px; margin-left: 12px;">Interface</span></li>
                                <li><a href="#" style="background: #ffffff; border-radius: 0%; box-shadow: none; border: none;"></a></li>
                                <li><a href="#" style="background: #ffffff; border-radius: 0%; box-shadow: none; border: none;"></a></li>
                                <li><a href="#" style="background: #ffffff; border-radius: 0%; box-shadow: none; border: none;"></a></li>
                                <li><a href="#" style="background: #ffffff; border-radius: 0%; box-shadow: none; border: none;"></a></li>
                                <li><a href="F_15_Acc/RptAccDTransaction.aspx?Type=Accounts&TrMod=DTran">Status Report
                                </a></li>

                                <li><a href="<%=this.ResolveUrl("~/GenPage.aspx?Type=09")%>">Statements of Accounts
                                </a></li>
                                <li><a href="<%=this.ResolveUrl("~/GenPage.aspx?Type=09")%>">More Reports</a></li>
                                <li><a href="#" style="background: #ffffff; border-radius: 0%; box-shadow: none; border: none;"></a></li>
                                <li><a href="#" style="background: #ffffff; border-radius: 0%; box-shadow: none; border: none;"></a></li>
                                <li><a href="#" style="background: #ffffff; border-radius: 0%; box-shadow: none; border: none;"></a></li>

                                <li><a href="F_15_Acc/AccOpening.aspx">Customer's Opening
                                </a></li>
                                <li><a href="DeafultMenu.aspx?Type=4110">Settings</a></li>

                            </ul>

                        </div>
                        <div class="col-md-9">


                            <div class="row menu10">

                                <table class="menuTb" width="200" border="0" cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td height="52"></td>
                                        <td></td>
                                        <td>&nbsp;</td>
                                        <td>
                                            <img class="iconImg mIconImg" src="Image/export.png" width="43" height="33" alt="x" /></td>
                                        <td class="mainTitelA menubg mMenu">Export</td>
                                        <td>&nbsp;</td>
                                        <td></td>

                                        <td></td>
                                    </tr>
                                    <tr>
                                        <td>&nbsp;</td>
                                        <td></td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>
                                            <img class="arrowDown" src="Image/5.png" width="18" height="31" alt=" c" /></td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td></td>
                                    </tr>
                                    <tr>
                                        <td height="37">
                                            <img class="iconImg" src="Image/icon16.png" width="58" height="50" alt="x" /></td>
                                        <td class="mainTitelA"><a href="#">Proforma Invoice</a></td>
                                        <td>
                                            <img class="arrowleft" src="Image/6.png" width="37" height="19" alt="x" /></td>
                                        <td>
                                            <img class="iconImg" src="Image/icon8.png" width="58" height="50" alt="x" /></td>
                                        <td class="mainTitelA mainTitelB "><a href="#">L/C Information Input                                         </a></td>
                                        <td>
                                            <img class="arrowRight" src="Image/7.png" width="35" height="19" alt="x" /></td>
                                        <td>
                                            <img class="iconImg" src="Image/icon15.png" width="58" height="50" alt="x" /></td>
                                        <td class="mainTitelA mainTitelB"><a href="#">Commercial Proposal

                                        </a></td>
                                    </tr>
                                    <tr>
                                        <td>&nbsp;</td>
                                        <td>
                                            <img class="arrowDown" src="Image/5.png" width="18" height="31" alt=" c" /></td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td></td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td></td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <img class="iconImg" src="Image/icon19.png" width="43" height="33" alt="x" /></td>
                                        <td class="mainTitelA "><a href="<%=this.ResolveUrl("~/F_15_Acc/GeneralAccounts.aspx?tcode=99&tname=Payment Voucher&Mod=Accounts")%>">Sales Order



                                        </a></td>
                                        <td>
                                            <img class="arrowRight" src="Image/7.png" width="35" height="19" alt="x" /></td>
                                        <td>
                                            <img class="iconImg" src="Image/icon10.png" width="43" height="33" alt="x" /></td>

                                        <td class="mainTitelA"><a href="<%=this.ResolveUrl("~/F_15_Acc/GeneralAccounts.aspx?tcode=99&tname=Journal Voucher&Mod=Accounts")%>">Approval

                                        </a></td>
                                        <td>
                                            <img class="arrowRight" src="Image/7.png" width="35" height="19" alt="x" /></td>
                                        <td>
                                            <img class="iconImg" src="Image/icon11.png" width="43" height="33" alt="x" /></td>
                                        <td class="mainTitelA mainTitelB"><a href="<%=this.ResolveUrl("~/F_15_Acc/GeneralAccounts.aspx?tcode=99&tname=Deposit Voucher&Mod=Accounts")%>">Delivery Order

                                        </a></td>
                                    </tr>
                                    <tr>
                                        <td>&nbsp;</td>
                                        <td></td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td></td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>
                                            <img class="arrowDown" src="Image/5.png" width="18" height="31" alt=" c" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <img class="iconImg" src="Image/icon12.png" width="43" height="33" alt="x" /></td>
                                        <td class="mainTitelA"><a href="<%=this.ResolveUrl("~/F_15_Acc/GeneralAccounts.aspx?tcode=99&tname=Payment Voucher&Mod=Accounts")%>">Export  Document
                                        </a></td>
                                        <td>
                                            <img class="arrowleft" src="Image/6.png" width="35" height="19" alt="x" /></td>
                                        <td>
                                            <img class="iconImg" src="Image/3.png" width="43" height="33" alt="x" /></td>

                                        <td class="mainTitelA"><a href="<%=this.ResolveUrl("~/F_15_Acc/GeneralAccounts.aspx?tcode=99&tname=Journal Voucher&Mod=Accounts")%>">Goods Delivery



                                        </a></td>
                                        <td>
                                            <img class="arrowleft" src="Image/6.png" width="35" height="19" alt="x" /></td>
                                        <td>
                                            <img class="iconImg" src="Image/12.png" width="43" height="33" alt="x" /></td>
                                        <td class="mainTitelA"><a href="<%=this.ResolveUrl("~/F_15_Acc/GeneralAccounts.aspx?tcode=99&tname=Deposit Voucher&Mod=Accounts")%>">Sales Invoice
                                        </a></td>
                                    </tr>
                                </table>
                            </div>





                            <div class="row">

                                <div class="col-md-12 relatedItems ">
                                    <h3>Related Items</h3>


                                    <ul class="nav-pills">
                                        <li><a href="<%=this.ResolveUrl("~/F_15_Acc/AccPayment.aspx?tcode=99&tname=Payment Voucher&Type=Acc")%>"><span class=""></span>Customer List


                                        </a></li>

                                        <%--glyphicon glyphicon-unchecked--%>
                                        <li><a href="<%=this.ResolveUrl("~/F_15_Acc/AccInterComVoucher.aspx")%>"><span class=""></span>New Supplier

                                        </a></li>
                                        <li><a href="<%=this.ResolveUrl("~/F_15_Acc/AccSales.aspx")%>"><span class=""></span>Sales Target

                                        </a></li>
                                        <li><a href="<%=this.ResolveUrl("~/F_15_Acc/AccPurchase.aspx")%>"><span class=""></span>Incentive


                                        </a></li>
                                        <li><a href="<%=this.ResolveUrl("~/F_15_Acc/AccPurchaseFor.aspx")%>"><span class=""></span>Free Product

                                        </a></li>
                                        <li><a href="<%=this.ResolveUrl("~/F_15_Acc/AccIsuUpdate.aspx")%>"><span class=""></span>Return
                                        </a></li>

                                        <li><a href="<%=this.ResolveUrl("~/F_15_Acc/AccIsuUpdate.aspx")%>"><span class=""></span>Debit Note

                                        </a></li>

                                        <li><a href="<%=this.ResolveUrl("~/F_15_Acc/AccIsuUpdate.aspx")%>"><span class=""></span>Credit Note

                                        </a></li>


                                    </ul>

                                </div>
                            </div>

                            <div class="clearfix"></div>
                        </div>




                    </div>




                    <div class="clearfix"></div>
                </asp:Panel>


                <asp:Panel runat="server" ID="pnlPur" Visible="false">

                    <div class="row ">
                        <div class="col-md-3 mfgacc2 ">
                            <ul class="sideMenu ">
                                <li><a href="#" style="background: #ffffff; border-radius: 0%; box-shadow: none; border: none;"></a></li>
                                <li><a class="nonBg" href="<%=this.ResolveUrl("~/F_11_Pro/PurInformation.aspx")%>" target="_blank">
                                    <img src="Image/int1.png" /><span style="padding: 0px; margin-left: 12px;">Interface</span></a></li>
                                <li><a href="#" style="background: #ffffff; border-radius: 0%; box-shadow: none; border: none;"></a></li>
                                <li><a href="#" style="background: #ffffff; border-radius: 0%; box-shadow: none; border: none;"></a></li>
                                <li><a href="#" style="background: #ffffff; border-radius: 0%; box-shadow: none; border: none;"></a></li>
                                <li><a href="#" style="background: #ffffff; border-radius: 0%; box-shadow: none; border: none;"></a></li>
                                <li><a href="F_15_Acc/RptAccDTransaction.aspx?Type=Accounts&TrMod=DTran">Status Report
                                </a></li>

                                <li><a href="<%=this.ResolveUrl("~/GenPage.aspx?Type=09")%>" target="_blank">Statements of Accounts
                                </a></li>
                                <li><a href="<%=this.ResolveUrl("~/GenPage.aspx?Type=09")%>" target="_blank">More Reports</a></li>
                                <li><a href="#" style="background: #ffffff; border-radius: 0%; box-shadow: none; border: none;"></a></li>
                                <li><a href="#" style="background: #ffffff; border-radius: 0%; box-shadow: none; border: none;"></a></li>
                                <li><a href="#" style="background: #ffffff; border-radius: 0%; box-shadow: none; border: none;"></a></li>

                                <li><a href="F_15_Acc/AccOpening.aspx" target="_blank">Customer's Opening
                                </a></li>
                                <li><a href="DeafultMenu.aspx?Type=4110" target="_blank">Settings</a></li>

                            </ul>

                        </div>
                        <div class="col-md-9">


                            <div class="row menu10">

                                <table class="menuTb" width="200" border="0" cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td height="52"></td>
                                        <td></td>
                                        <td>&nbsp;</td>
                                        <td>
                                            <img class="iconImg mIconImg" src="Image/icon13.png" width="43" height="33" alt="x" /></td>
                                        <td class="mainTitelA menubg mMenu">Purchase</td>
                                        <td>&nbsp;</td>
                                        <td></td>

                                        <td></td>
                                    </tr>
                                    <tr>
                                        <td>&nbsp;</td>
                                        <td></td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>
                                            <img class="arrowDown" src="Image/5.png" width="18" height="31" alt=" c" /></td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td></td>
                                    </tr>
                                    <tr>
                                        <td height="37">
                                            <img class="iconImg" src="Image/icon8.png" width="58" height="50" alt="x" /></td>
                                        <td class="mainTitelA mainTitelB"><a href="#">Price Offer (Manual / Auto) </a></td>
                                        <td>
                                            <img class="arrowleft" src="Image/6.png" width="37" height="19" alt="x" /></td>
                                        <td>
                                            <img class="iconImg" src="Image/icon14.png" width="58" height="50" alt="x" /></td>
                                        <td class="mainTitelA "><a href="<%=this.ResolveUrl("~/F_11_Pro/RptRequsitionStatus.aspx?WType=ReqStatus&Type=Purchase")%>">Requisition         Center
                                        </a></td>
                                        <td>
                                            <img class="arrowRight" src="Image/7.png" width="35" height="19" alt="x" /></td>
                                        <td>
                                            <img class="iconImg" src="Image/icon15.png" width="58" height="50" alt="x" /></td>
                                        <td class="mainTitelA"><a href="#">Create Requisition 
                                        </a></td>
                                    </tr>
                                    <tr>
                                        <td>&nbsp;</td>
                                        <td>
                                            <img class="arrowDown" src="Image/5.png" width="18" height="31" alt=" c" /></td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td></td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td></td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <img class="iconImg" src="Image/icon17.png" width="43" height="33" alt="x" /></td>
                                        <td class="mainTitelA mainTitelB"><a href="<%=this.ResolveUrl("~/F_07_Inv/PurMktSurvey.aspx?Type=MktSurvey")%>" target="_blank">Comparative Statement

                                        </a></td>
                                        <td>
                                            <img class="arrowRight" src="Image/7.png" width="35" height="19" alt="x" /></td>
                                        <td>
                                            <img class="iconImg" src="Image/icon10.png" width="43" height="33" alt="x" /></td>

                                        <td class="mainTitelA "><a href="<%=this.ResolveUrl("~/F_07_Inv/PurReqEntry02.aspx?InputType=HeadUsed")%>" target="_blank">Rate Proposal

                                        </a></td>
                                        <td>
                                            <img class="arrowRight" src="Image/7.png" width="35" height="19" alt="x" /></td>
                                        <td>
                                            <img class="iconImg" src="Image/icon18.png" width="43" height="33" alt="x" /></td>
                                        <td class="mainTitelA mainTitelB"><a href="<%=this.ResolveUrl("~/F_07_Inv/PurReqEntry02.aspx?InputType=FxtAstApproval")%>" target="_blank">Requisition Approval
                                        </a></td>
                                    </tr>
                                    <tr>
                                        <td>&nbsp;</td>
                                        <td></td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td></td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>
                                            <img class="arrowDown" src="Image/5.png" width="18" height="31" alt=" c" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <img class="iconImg" src="Image/2.png" width="43" height="33" alt="x" /></td>
                                        <td class="mainTitelA"><a href="<%=this.ResolveUrl("~/F_07_Inv/PurBillEntry.aspx?Type=BillEntry")%>" target="_blank">Purchase Invoice

                                        </a></td>
                                        <td>
                                            <img class="arrowleft" src="Image/6.png" width="35" height="19" alt="x" /></td>
                                        <td>
                                            <img class="iconImg" src="Image/icon19.png" width="43" height="33" alt="x" /></td>

                                        <td class="mainTitelA"><a href="<%=this.ResolveUrl("~/F_11_Pro/PurWrkOrderEntry.aspx?InputType=OrderEntry")%>" target="_blank">Purchase           Order

                                        </a></td>
                                        <td>
                                            <img class="arrowleft" src="Image/6.png" width="35" height="19" alt="x" /></td>
                                        <td>
                                            <img class="iconImg" src="Image/icon14.png" width="43" height="33" alt="x" /></td>
                                        <td class="mainTitelA"><a href="<%=this.ResolveUrl("~/F_07_Inv/PurAprovEntry.aspx?InputType=PurProposal")%>" target="_blank">Purchase Program


                                        </a></td>
                                    </tr>
                                </table>
                            </div>





                            <div class="row">

                                <div class="col-md-12 relatedItems ">
                                    <h3>Related Items</h3>


                                    <ul class="nav-pills">
                                        <li><a href="<%=this.ResolveUrl("~/F_15_Acc/AccPayment.aspx?tcode=99&tname=Payment Voucher&Type=Acc")%>" target="_blank"><span class=""></span>Customer List
                                        </a></li>
                                        <%--glyphicon glyphicon-unchecked--%>
                                        <li><a href="<%=this.ResolveUrl("~/F_15_Acc/AccInterComVoucher.aspx")%>" target="_blank"><span class=""></span>Supplier's List
                                        </a></li>
                                        <li><a href="<%=this.ResolveUrl("~/F_15_Acc/AccSales.aspx")%>" target="_blank"><span class=""></span>New Supplier
                                        </a></li>
                                        <li><a href="<%=this.ResolveUrl("~/F_15_Acc/AccPurchase.aspx")%>" target="_blank"><span class=""></span>Return
                                        </a></li>
                                        <li><a href="<%=this.ResolveUrl("~/F_15_Acc/AccPurchaseFor.aspx")%>" target="_blank"><span class=""></span>Debit Note
                                        </a></li>
                                        <li><a href="<%=this.ResolveUrl("~/F_15_Acc/AccIsuUpdate.aspx")%>" target="_blank"><span class=""></span>Credit Note
                                        </a></li>


                                    </ul>

                                </div>
                            </div>

                            <div class="clearfix"></div>
                        </div>




                    </div>




                    <div class="clearfix"></div>
                </asp:Panel>


                <asp:Panel runat="server" ID="Panel7" Visible="false">
                    <div class=" text-center accImg ">
                        <img src="p7.png" alt="Setting" width="1140" border="0" usemap="#Map" />
                        <map name="Map" id="Map">
                            <area shape="rect" coords="353,268,502,317" href="F_34_Mgt/RptUserLogDetails.aspx" target="_self" />
                            <area shape="rect" coords="578,376,727,425" href="F_22_Sal/MktSalsPayment.aspx?Type=Sales" target="_self" />
                            <area shape="rect" coords="794,370,943,419" href="F_34_Mgt/AccUserCash.aspx" target="_self" />
                            <area shape="rect" coords="792,267,941,316" href="F_34_Mgt/RptUserLogDetails.aspx" target="_self" />
                            <area shape="rect" coords="573,268,722,317" href="F_21_Mkt/RptMktAppointment.aspx?Type=Todaysdis&amp;UType=Mgt" target="_self" />
                            <area shape="rect" coords="137,268,278,318" href="DeafultMenu.aspx?Type=4112" target="_self" />
                        </map>
                    </div>

                    <div class="clearfix"></div>
                </asp:Panel>

                <asp:Panel runat="server" ID="PnFGoods" Visible="false">
                    <div class="row ">
                        <div class="col-md-3 mfgacc2 ">
                            <ul class="sideMenu ">
                                <li><a href="#" style="background: #ffffff; border-radius: 0%; box-shadow: none; border: none;"></a></li>
                                <li><a class="nonBg" href="<%=this.ResolveUrl("#")%>">
                                    <img src="Image/int1.png" /><span style="padding: 0px; margin-left: 12px;">Interface</span></a></li>
                                <li><a href="#" style="background: #ffffff; border-radius: 0%; box-shadow: none; border: none;"></a></li>
                                <li><a href="#" style="background: #ffffff; border-radius: 0%; box-shadow: none; border: none;"></a></li>
                                <li><a href="#" style="background: #ffffff; border-radius: 0%; box-shadow: none; border: none;"></a></li>
                                <li><a href="#" style="background: #ffffff; border-radius: 0%; box-shadow: none; border: none;"></a></li>
                                <li><a href="F_15_Acc/RptAccDTransaction.aspx?Type=Accounts&TrMod=DTran" target="_blank">Daily Transaction

                                </a></li>

                                <li><a href="<%=this.ResolveUrl("~/GenPage.aspx?Type=09")%>" target="_blank">Store Ledger</a></li>
                                <li><a href="<%=this.ResolveUrl("~/GenPage.aspx?Type=09")%>" target="_blank">All Reports
                                </a></li>
                                <li><a href="#" style="background: #ffffff; border-radius: 0%; box-shadow: none; border: none;"></a></li>
                                <li><a href="#" style="background: #ffffff; border-radius: 0%; box-shadow: none; border: none;"></a></li>
                                <li><a href="<%=this.ResolveUrl("~/GenPage.aspx?Type=09")%>" target="_blank">Store Items</a></li>
                                <li><a href="F_15_Acc/AccOpening.aspx" target="_blank">Store Opening</a></li>
                                <li><a href="DeafultMenu.aspx?Type=4110" target="_blank">Settings</a></li>

                            </ul>

                        </div>
                        <div class="col-md-9">


                            <div class="row menu10">

                                <table class="menuTb" width="200" border="0" cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td height="52"></td>
                                        <td></td>
                                        <td>&nbsp;</td>
                                        <td>
                                            <img class="iconImg mIconImg" src="Image/icon27.png" width="43" height="33" alt="x" /></td>
                                        <td class="mainTitelA menubg mMenu ">Goods Inventory

                                        </td>
                                        <td>&nbsp;</td>
                                        <td></td>

                                        <td></td>
                                    </tr>
                                    <tr>
                                        <td>&nbsp;</td>
                                        <td></td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>
                                            <img class="arrowDown" src="Image/5.png" width="18" height="31" alt=" c" /></td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td></td>
                                    </tr>
                                    <tr>
                                        <td height="37"></td>
                                        <td></td>
                                        <td></td>
                                        <td>
                                            <img class="iconImg" src="Image/12.png" width="58" height="50" alt="x" /></td>
                                        <td class="mainTitelA mainTitelB"><a href="#">Production Requisition (Auto)

                                        </a></td>
                                        <td>
                                            <img class="arrowRight" src="Image/7.png" width="35" height="19" alt="x" /></td>
                                        <td>
                                            <img class="iconImg" src="Image/icon19.png" width="58" height="50" alt="x" /></td>
                                        <td class="mainTitelA"><a href="~/F_23_SaM/MonthlySalesEntry.aspx">Sales Target Input</a> </td>
                                    </tr>
                                    <tr>
                                        <td>&nbsp;</td>
                                        <td></td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>
                                            <img class="arrowDown" src="Image/5.png" width="18" height="31" alt=" c" />
                                        </td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td></td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <img class="iconImg" src="Image/icon21.png" width="43" height="33" alt="x" /></td>
                                        <td class="mainTitelA"><a href="<%=this.ResolveUrl("~/F_23_SaM/DeliveryOrder.aspx")%>" target="_blank">Delivery 



                                        </a></td>
                                        <td>
                                            <img class="arrowRight" src="Image/6.png" width="35" height="19" alt="x" /></td>
                                        <td>
                                            <img class="iconImg" src="Image/11.png" width="43" height="33" alt="x" /></td>

                                        <td class="mainTitelA "><a href="<%=this.ResolveUrl("~/F_19_FGInv/ProReceive.aspx")%>" target="_blank">Goods Receive



                                        </a></td>
                                        <td>
                                            <img class="arrowRight" src="Image/7.png" width="35" height="19" alt="x" /></td>
                                        <td>
                                            <img class="iconImg" src="Image/icon12.png" width="43" height="33" alt="x" /></td>
                                        <td class="mainTitelA "><a href="<%=this.ResolveUrl("~/F_23_SaM/Chalan.aspx")%>" target="_blank">Transfer
                                        </a></td>
                                    </tr>
                                    <tr>
                                        <td>&nbsp;</td>
                                        <td></td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>
                                            <img class="arrowDown" src="Image/5.png" width="18" height="31" alt=" c" />
                                        </td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td></td>
                                    </tr>
                                    <tr>
                                        <td></td>
                                        <td></td>
                                        <td></td>
                                        <td>
                                            <img class="iconImg" src="Image/icon14.png" width="43" height="33" alt="x" /></td>

                                        <td class="mainTitelA"><a href="<%=this.ResolveUrl("~/F_07_Inv/EntryLostSoldADes.aspx")%>" target="_blank">Destroy


                                        </a></td>
                                        <td></td>
                                        <td></td>
                                        <td></td>
                                    </tr>
                                </table>
                            </div>





                            <div class="row">

                                <div class="col-md-12 relatedItems ">
                                    <h3>Related Items</h3>


                                    <ul class="nav-pills">
                                        <li><a href="<%=this.ResolveUrl("~/F_15_Acc/AccPayment.aspx?tcode=99&tname=Payment Voucher&Type=Acc")%>" target="_blank"><span class=""></span>Sales Return


                                        </a></li>
                                        <%--glyphicon glyphicon-unchecked--%>
                                        <li><a href="<%=this.ResolveUrl("~/F_15_Acc/AccInterComVoucher.aspx")%>" target="_blank"><span class=""></span>Expire Goods

                                        </a></li>
                                        <li><a href="<%=this.ResolveUrl("~/F_15_Acc/AccSales.aspx")%>" target="_blank"><span class=""></span>Wastage Sold

                                        </a></li>
                                        <li><a href="<%=this.ResolveUrl("~/F_15_Acc/AccPurchase.aspx")%>" target="_blank"><span class=""></span>Goods History

                                        </a></li>


                                    </ul>

                                </div>
                            </div>

                            <div class="clearfix"></div>
                        </div>




                    </div>
                </asp:Panel>

                <asp:Panel runat="server" ID="PnlSales" Visible="false">

                    <div class="row ">
                        <div class="col-md-3 mfgacc2 ">
                            <ul class="sideMenu ">
                                <li><a href="#" style="background: #ffffff; border-radius: 0%; box-shadow: none; border: none;"></a></li>
                                <li><a class="nonBg" href="<%=this.ResolveUrl("~/F_23_SaM/SalesInformation.aspx")%>" target="_blank">
                                    <img src="Image/int1.png" /><span style="padding: 0px; margin-left: 12px;">Interface</span></a></li>
                                <li><a href="#" style="background: #ffffff; border-radius: 0%; box-shadow: none; border: none;"></a></li>
                                <li><a href="#" style="background: #ffffff; border-radius: 0%; box-shadow: none; border: none;"></a></li>
                                <li><a href="#" style="background: #ffffff; border-radius: 0%; box-shadow: none; border: none;"></a></li>
                                <li><a href="#" style="background: #ffffff; border-radius: 0%; box-shadow: none; border: none;"></a></li>
                                <li><a href="F_15_Acc/RptAccDTransaction.aspx?Type=Accounts&TrMod=DTran" target="_blank">Status Report
                                </a></li>
                                <li><a href="F_15_Acc/AccLedger.aspx?Type=Ledger&RType=GLedger" target="_blank">Month wise Report
                                </a></li>
                                <li><a href="<%=this.ResolveUrl("~/GenPage.aspx?Type=09")%>" target="_blank">Statements of Accounts
                                </a></li>
                                <li><a href="<%=this.ResolveUrl("~/GenPage.aspx?Type=09")%>" target="_blank">More Reports</a></li>
                                <li><a href="#" style="background: #ffffff; border-radius: 0%; box-shadow: none; border: none;"></a></li>
                                <li><a href="#" style="background: #ffffff; border-radius: 0%; box-shadow: none; border: none;"></a></li>
                                <li><a href="#" style="background: #ffffff; border-radius: 0%; box-shadow: none; border: none;"></a></li>

                                <li><a href="F_15_Acc/AccOpening.aspx" target="_blank">Customer's Opening
                                </a></li>
                                <li><a href="DeafultMenu.aspx?Type=4110" target="_blank">Settings</a></li>

                            </ul>

                        </div>
                        <div class="col-md-9">


                            <div class="row menu10">

                                <table class="menuTb" width="200" border="0" cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td height="52"></td>
                                        <td></td>
                                        <td>&nbsp;</td>
                                        <td>
                                            <img class="iconImg mIconImg" src="Image/icon7.png" width="43" height="33" alt="x" /></td>
                                        <td class="mainTitelA menubg mMenu">Sales</td>
                                        <td>&nbsp;</td>
                                        <td></td>

                                        <td></td>
                                    </tr>
                                    <tr>
                                        <td>&nbsp;</td>
                                        <td></td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>
                                            <img class="arrowDown" src="Image/5.png" width="18" height="31" alt=" c" /></td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td></td>
                                    </tr>
                                    <tr>
                                        <td height="37">
                                            <img class="iconImg" src="Image/icon16.png" width="58" height="50" alt="x" /></td>
                                        <td class="mainTitelA"><a href="#">Order by Email</a> </td>
                                        <td>
                                            <img class="arrowleft" src="Image/6.png" width="37" height="19" alt="x" /></td>
                                        <td>
                                            <img class="iconImg" src="Image/icon8.png" width="58" height="50" alt="x" /></td>
                                        <td class="mainTitelA "><a href="~/F_21_Mkt/EntrySalesOrder.aspx">Order Input</a>   </td>
                                        <td>
                                            <img class="arrowRight" src="Image/7.png" width="35" height="19" alt="x" /></td>
                                        <td>&nbsp;</td>
                                        <td class="mainTitelA mainTitelB"><a href="~/F_23_SaM/SalesComProposal.aspx">Commercial Proposal</a> </td>
                                    </tr>
                                    <tr>
                                        <td>&nbsp;</td>
                                        <td>
                                            <img class="arrowDown" src="Image/5.png" width="18" height="31" alt=" c" /></td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td></td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td></td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <img class="iconImg" src="Image/icon9.png" width="43" height="33" alt="x" /></td>
                                        <td class="mainTitelA"><a href="<%=this.ResolveUrl("~/F_23_SaM/SalesOrderApproval.aspx")%>" target="_blank">Approval-1
                                        </a></td>
                                        <td>
                                            <img class="arrowRight" src="Image/7.png" width="35" height="19" alt="x" /></td>
                                        <td>
                                            <img class="iconImg" src="Image/icon10.png" width="43" height="33" alt="x" /></td>

                                        <td class="mainTitelA"><a href="<%=this.ResolveUrl("~/F_23_SaM/SalesOrderApproval.aspx")%>" target="_blank">Approval-2
                                        </a></td>
                                        <td>
                                            <img class="arrowRight" src="Image/7.png" width="35" height="19" alt="x" /></td>
                                        <td>
                                            <img class="iconImg" src="Image/icon11.png" width="43" height="33" alt="x" /></td>
                                        <td class="mainTitelA"><a href="<%=this.ResolveUrl("~/F_23_SaM/DeliveryOrder.aspx")%>" target="_blank">Delivery Order
                                        </a></td>
                                    </tr>
                                    <tr>
                                        <td>&nbsp;</td>
                                        <td></td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td></td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>
                                            <img class="arrowDown" src="Image/5.png" width="18" height="31" alt=" c" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <img class="iconImg" src="Image/icon12.png" width="43" height="33" alt="x" /></td>
                                        <td class="mainTitelA"><a href="<%=this.ResolveUrl("~/F_15_Acc/GeneralAccounts.aspx?tcode=99&tname=Payment Voucher&Mod=Accounts")%>" target="_blank">Goods Delivery 

                                        </a></td>
                                        <td>
                                            <img class="arrowleft" src="Image/6.png" width="35" height="19" alt="x" /></td>
                                        <td>
                                            <img class="iconImg" src="Image/9.png" width="43" height="33" alt="x" /></td>

                                        <td class="mainTitelA"><a href="<%=this.ResolveUrl("~/F_15_Acc/GeneralAccounts.aspx?tcode=99&tname=Journal Voucher&Mod=Accounts")%>" target="_blank">Collection
                                        </a></td>
                                        <td>
                                            <img class="arrowleft" src="Image/6.png" width="35" height="19" alt="x" /></td>
                                        <td>
                                            <img class="iconImg" src="Image/12.png" width="43" height="33" alt="x" /></td>
                                        <td class="mainTitelA"><a href="<%=this.ResolveUrl("~/F_15_Acc/GeneralAccounts.aspx?tcode=99&tname=Deposit Voucher&Mod=Accounts")%>" target="_blank">Sales Invoice

                                        </a></td>
                                    </tr>
                                </table>
                            </div>





                            <div class="row">

                                <div class="col-md-12 relatedItems ">
                                    <h3>Related Items</h3>


                                    <ul class="nav-pills">
                                        <li><a href="<%=this.ResolveUrl("~/F_23_SaM/MktEntryUnit.aspx")%>" target="_blank"><span class=""></span>Customer List
                                        </a></li>
                                        <%--glyphicon glyphicon-unchecked--%>
                                        <li><a href="<%=this.ResolveUrl("~/F_23_SaM/MktEntryUnit.aspx")%>" target="_blank"><span class=""></span>New Customer</a></li>
                                        <li><a href="<%=this.ResolveUrl("~/F_23_SaM/MonthlySalesBudget.aspx?Type=Monthly")%>" target="_blank"><span class=""></span>Sales Target</a></li>
                                        <li><a href="<%=this.ResolveUrl("~/F_23_SaM/RptSalSummery.aspx?Type=SalComm")%>" target="_blank"><span class=""></span>Incentive</a></li>
                                        <li><a href="<%=this.ResolveUrl("~/F_23_SaM/FreeSalesMemo.aspx")%>" target="_blank"><span class=""></span>Free Product</a></li>
                                        <li><a href="<%=this.ResolveUrl("~/F_23_SaM/ReturnSales.aspx")%>" target="_blank"><span class=""></span>Return</a></li>

                                        <li><a href="<%=this.ResolveUrl("~/F_15_Acc/AccProductionJV.aspx")%>" target="_blank"><span class=""></span>Debit Note</a></li>
                                        <li><a href="<%=this.ResolveUrl("~/F_15_Acc/AccChalanTransfer.aspx")%>" target="_blank"><span class=""></span>Credit Note</a></li>

                                    </ul>

                                </div>
                            </div>

                            <div class="clearfix"></div>
                        </div>




                    </div>

                    <div class="clearfix"></div>


                </asp:Panel>

                <asp:Panel runat="server" ID="pnlMrkt" Visible="false">
                    <div class="tree commmonTree treePnlBudgt">
                        <ul>
                            <li>
                                <a href="#">Basic Information</a>
                            </li>

                            <li>
                                <a class="titelPanel" href="#">Marketing</a>
                                <ul>
                                    <li>
                                        <a href="ComingSoon.aspx">Discussion </a>

                                    </li>

                                </ul>
                            </li>

                            <li><a class="smllMenuBox1" href="RptDefault.aspx">Reports</a></li>
                        </ul>
                    </div>

                    <div class="clearfix"></div>
                </asp:Panel>

                <asp:Panel runat="server" ID="pnlProd" Visible="false">
                    <div class="row ">
                        <div class="col-md-3 mfgacc2 ">
                            <ul class="sideMenu ">
                                <li><a href="#" style="background: #ffffff; border-radius: 0%; box-shadow: none; border: none;"></a></li>
                                <li><a class="nonBg" href="<%=this.ResolveUrl("~/F_13_ProdMon/ProductionInfo.aspx")%>" target="_blank">
                                    <img src="Image/int1.png" /><span style="padding: 0px; margin-left: 12px;">Interface</span></a></li>
                                <li><a href="#" style="background: #ffffff; border-radius: 0%; box-shadow: none; border: none;"></a></li>
                                <li><a href="#" style="background: #ffffff; border-radius: 0%; box-shadow: none; border: none;"></a></li>
                                <li><a href="#" style="background: #ffffff; border-radius: 0%; box-shadow: none; border: none;"></a></li>
                                <li><a href="#" style="background: #ffffff; border-radius: 0%; box-shadow: none; border: none;"></a></li>
                                <li><a href="F_15_Acc/RptAccDTransaction.aspx?Type=Accounts&TrMod=DTran" target="_blank">Daily Transaction

                                </a></li>

                                <li><a href="<%=this.ResolveUrl("~/GenPage.aspx?Type=09")%>" target="_blank">Store Ledger

                                </a></li>
                                <li><a href="<%=this.ResolveUrl("~/GenPage.aspx?Type=09")%>" target="_blank">All Reports
                                </a></li>
                                <li><a href="#" style="background: #ffffff; border-radius: 0%; box-shadow: none; border: none;"></a></li>
                                <li><a href="#" style="background: #ffffff; border-radius: 0%; box-shadow: none; border: none;"></a></li>
                                <li><a href="#">Store Items
                                </a></li>

                                <li><a href="F_15_Acc/AccOpening.aspx" target="_blank">Store Opening
                                </a></li>
                                <li><a href="DeafultMenu.aspx?Type=4110" target="_blank">Settings</a></li>

                            </ul>

                        </div>
                        <div class="col-md-9">


                            <div class="row menu10">

                                <table class="menuTb" width="200" border="0" cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td height="52"></td>
                                        <td></td>
                                        <td>&nbsp;</td>
                                        <td>
                                            <img class="iconImg mIconImg" src="Image/icon21.png" width="43" height="33" alt="x" /></td>
                                        <td class="mainTitelA menubg mMenu">Production
                                        </td>
                                        <td>&nbsp;</td>
                                        <td></td>

                                        <td></td>
                                    </tr>
                                    <tr>
                                        <td>&nbsp;</td>
                                        <td></td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>
                                            <img class="arrowDown" src="Image/5.png" width="18" height="31" alt=" c" /></td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td></td>
                                    </tr>
                                    <tr>
                                        <td height="37"></td>
                                        <td></td>
                                        <td></td>
                                        <td>
                                            <img class="iconImg" src="Image/12.png" width="58" height="50" alt="x" /></td>
                                        <td class="mainTitelA mainTitelB"><a href="~/F_03_StdCost/ProdBudget.aspx?Type=Entry">Target Production Quantity Input

                                        </a></td>
                                        <td>
                                            <img class="arrowRight" src="Image/7.png" width="35" height="19" alt="x" /></td>
                                        <td>
                                            <img class="iconImg" src="Image/icon22.png" width="58" height="50" alt="x" /></td>
                                        <td class="mainTitelA mainTitelB"><a href="~/F_11_Pro/ProdReq.aspx">Production Requisition (Auto)
                                        </a></td>
                                    </tr>
                                    <tr>
                                        <td>&nbsp;</td>
                                        <td></td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td></td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>
                                            <img class="arrowDown" src="Image/5.png" width="18" height="31" alt=" c" /></td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <img class="iconImg" src="Image/icon23.png" width="43" height="33" alt="x" /></td>
                                        <td class="mainTitelA"><a href="<%=this.ResolveUrl("#")%>" target="_blank">Wastage Input

                                        </a></td>
                                        <td>
                                            <img class="arrowRight" src="Image/6.png" width="35" height="19" alt="x" /></td>
                                        <td>
                                            <img class="iconImg" src="Image/icon24.png" width="43" height="33" alt="x" /></td>

                                        <td class="mainTitelA mainTitelB"><a href="<%=this.ResolveUrl("~/F_13_ProdMon/ProductionPlan.aspx")%>" target="_blank">Actual Production Quantity Input


                                        </a></td>
                                        <td>
                                            <img class="arrowRight" src="Image/6.png" width="35" height="19" alt="x" /></td>
                                        <td>
                                            <img class="iconImg" src="Image/icon26.png" width="43" height="33" alt="x" /></td>
                                        <td class="mainTitelA "><a href="<%=this.ResolveUrl("#")%>" target="_blank">Issue Confirmation

                                        </a></td>
                                    </tr>
                                    <tr>
                                        <td>&nbsp;</td>
                                        <td></td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>
                                            <img class="arrowDown" src="Image/5.png" width="18" height="31" alt=" c" /></td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td></td>
                                    </tr>
                                    <tr>
                                        <td></td>
                                        <td></td>
                                        <td></td>
                                        <td>
                                            <img class="iconImg" src="Image/icon25.png" width="43" height="33" alt="x" /></td>

                                        <td class="mainTitelA"><a href="<%=this.ResolveUrl("#")%>" target="_blank">Variance (Auto)
        
                                        </a></td>
                                        <td></td>
                                        <td></td>
                                        <td></td>
                                    </tr>
                                </table>
                            </div>





                            <div class="row">

                                <div class="col-md-12 relatedItems ">
                                    <h3>Related Items</h3>


                                    <ul class="nav-pills">
                                        <li><a href="<%=this.ResolveUrl("~/F_15_Acc/AccPayment.aspx?tcode=99&tname=Payment Voucher&Type=Acc")%>" target="_blank"><span class=""></span>Sales Return
                                        </a></li>
                                        <%--glyphicon glyphicon-unchecked--%>
                                        <li><a href="<%=this.ResolveUrl("~/F_15_Acc/AccInterComVoucher.aspx")%>" target="_blank"><span class=""></span>Expire Goods
                                        </a></li>
                                        <li><a href="<%=this.ResolveUrl("~/F_15_Acc/AccSales.aspx")%>" target="_blank"><span class=""></span>Wastage Sold
                                        </a></li>
                                        <li><a href="<%=this.ResolveUrl("~/F_15_Acc/AccPurchase.aspx")%>" target="_blank"><span class=""></span>Goods History
                                        </a></li>


                                    </ul>

                                </div>
                            </div>

                            <div class="clearfix"></div>
                        </div>




                    </div>

                    <div class="clearfix"></div>
                </asp:Panel>

                <asp:Panel runat="server" ID="pnlHR" Visible="false">
                    <%--<div class=" text-center accImg ">
                        <img src="hr2.png" alt="HR Menu" width="1043" border="0" usemap="#Map" />
                        <map name="Map" id="Map">
                            <area shape="rect" coords="643,364,805,391" href="F_81_Hrm/F_89_Pay/RptSalarySummary.aspx?Type=SalSum" target="_self" />
                            <area shape="rect" coords="643,335,805,362" href="F_81_Hrm/F_89_Pay/RptSalarySummary.aspx?Type=SalSum" target="_self" />
                            <area shape="rect" coords="643,127,806,154" href="F_81_Hrm/F_91_ACR/EmpPerAppraisal.aspx" target="_self" />
                            <area shape="rect" coords="643,158,805,185" href="F_81_Hrm/F_92_Mgt/HREmpConfirmation.aspx" target="_self" />
                            <area shape="rect" coords="644,221,806,248" href="F_81_Hrm/F_92_Mgt/EmpPro.aspx" target="_self" />
                            <area shape="rect" coords="644,190,807,217" href="F_81_Hrm/F_93_AnnInc/AnnualIncrement.aspx" target="_self" />
                            <area shape="rect" coords="644,249,807,276" href="F_81_Hrm/F_87_Tra/HREmpTransfer.aspx" target="_self" />
                            <area shape="rect" coords="643,278,805,305" href="F_81_Hrm/F_89_Pay/RptSalarySummary.aspx?Type=SalSum" target="_self" />
                            <area shape="rect" coords="643,305,805,332" href="F_81_Hrm/F_92_Mgt/EmpHold.aspx" target="_self" />
                            <area shape="rect" coords="428,311,590,338" href="F_81_Hrm/F_90_PF/AccProFund.aspx" target="_self" />
                            <area shape="rect" coords="428,279,590,306" href="F_81_Hrm/F_89_Pay/RptSalarySummary.aspx?Type=SalSum" target="_self" />
                            <area shape="rect" coords="428,248,591,275" href="F_81_Hrm/F_89_Pay/RpHRtPayroll.aspx?Type=Payslip" target="_self" />
                            <area shape="rect" coords="428,189,591,216" href="F_81_Hrm/F_89_Pay/EmpBankSalary.aspx" target="_self" />
                            <area shape="rect" coords="428,220,590,247" href="F_81_Hrm/F_89_Pay/EmpBankSalary.aspx" target="_self" />
                            <area shape="rect" coords="427,157,589,184" href="F_81_Hrm/F_89_Pay/RptSalarySummary.aspx?Type=SalSum" target="_self" />
                            <area shape="rect" coords="209,300,371,328" href="F_81_Hrm/F_84_Lea/HREmpLeave.aspx?Type=LeaveApp" target="_top" />
                            <area shape="rect" coords="208,328,372,357" href="F_81_Hrm/F_84_Lea/HREmpLeave.aspx?Type=LeaveApp" target="_self" />
                            <area shape="rect" coords="20,183,156,210" href="F_81_Hrm/F_81_Rec/JobAdvertisement.aspx" target="_self" />
                            <area shape="rect" coords="20,125,156,155" href="F_81_Hrm/F_81_Rec/JobAdvertisement.aspx" target="_self" />
                            <area shape="rect" coords="21,155,157,182" href="F_81_Hrm/F_81_Rec/JobAdvertisement.aspx" target="_self" />
                            <area shape="rect" coords="209,272,371,300" href="F_81_Hrm/F_84_Lea/HREmpLeave.aspx?Type=LeaveApp" target="_self" />
                            <area shape="rect" coords="209,243,371,271" href="F_81_Hrm/F_84_Lea/HREmpLeave.aspx?Type=FLeaveApp" target="_top" />
                            <area shape="rect" coords="209,215,370,242" href="F_81_Hrm/F_83_Att/HRDailyAtten.aspx" target="_self" />
                            <area shape="rect" coords="21,272,156,300" href="F_81_Hrm/F_81_Rec/LetterOfAppoinment.aspx?Type=LCreate" target="_self" />
                            <area shape="rect" coords="21,242,155,270" href="F_81_Hrm/F_81_Rec/ShortListing.aspx?Type=IResult" target="_self" />
                            <area shape="rect" coords="21,211,155,241" href="F_81_Hrm/F_81_Rec/ShortListing.aspx?Type=SList" target="_self" />
                            <area shape="rect" coords="209,185,370,212" href="F_81_Hrm/F_82_App/HREmpEntry.aspx?Type=Aggrement" target="_self" />
                            <area shape="rect" coords="207,157,371,183" href="F_81_Hrm/F_92_Mgt/EmpStatus02.aspx?Type=joiningRpt" target="_self" />
                            <area shape="rect" coords="427,126,590,153" href="F_81_Hrm/F_89_Pay/RptSalarySummary.aspx?Type=SalSum" target="_self" />
                            <area shape="rect" coords="886,129,1019,156" href="<%=this.ResolveUrl("~/DeafultMenu.aspx?Type=4102")%>" target="_self" />
                            <area shape="rect" coords="886,173,1016,201" href="<%=this.ResolveUrl("~/DeafultMenu.aspx?Type=4103")%>" target="_self" />
                            <area shape="rect" coords="885,218,1019,248" href="<%=this.ResolveUrl("~/DeafultMenu.aspx?Type=4104")%>" target="_self" />
                            <area shape="rect" coords="631,506,771,540" href="#" />
                            <area shape="rect" coords="208,126,370,153" href="F_81_Hrm/F_82_App/EmpEntry01.aspx" target="_self" />
                            <area shape="rect" coords="372,415,457,457" href="F_81_Hrm/F_97_MIS/RptMgtInterface.aspx" target="_self" />
                            <area shape="rect" coords="372,459,456,488" href="F_81_Hrm/F_92_Mgt/EmpStatus02.aspx?Type=joiningRpt" target="_self" />
                            <area shape="rect" coords="459,413,539,456" href="F_81_Hrm/F_83_Att/RptEmpDailyAttendance.aspx?Type=DailyAtten" target="_self" />
                            <area shape="rect" coords="540,415,636,456" href="F_81_Hrm/F_82_App/RptEmpInformation.aspx?Type=Services" target="_self" />
                            <area shape="rect" coords="460,460,539,490" href="F_81_Hrm/F_92_Mgt/EmpStatus02.aspx?Type=PenEmpCon" target="_self" />
                            <area shape="rect" coords="542,459,636,489" href="F_81_Hrm/F_92_Mgt/EmpStatus02.aspx?Type=SepType" target="_self" />
                        </map>
                    </div>--%>
                    <style>
                        * {
                            margin: 0;
                            padding: 0;
                        }

                        .pnlhrnbg {
                            overflow: hidden;
                            background-image: url(Image/bg.PNG) !important;
                        }

                        #na_wrawpr1 {
                            width: 1102px;
                            height: auto;
                            margin: 0 auto;
                            position: relative;
                            background-image: url(image/bg.PNG);
                            /*background-color: #F7F7F7;
                            background-image: linear-gradient(90deg, rgba(255,255,255,.07) 50%, transparent 50%),
                            linear-gradient(90deg, rgba(255,255,255,.13) 50%, transparent 50%),
                            linear-gradient(90deg, transparent 50%, rgba(255,255,255,.17) 50%),
                            linear-gradient(90deg, transparent 50%, rgba(255,255,255,.19) 50%);
                            background-size: 13px, 29px, 37px, 53px;*/
                        }

                        .na_first {
                            width: 170px;
                            float: left;
                        }

                        #na_wrawpr ul {
                            margin: 0;
                            padding: 0;
                        }

                        #na_wrawpr1 ul li {
                            list-style: none;
                            margin: 1px 0;
                            text-align: center;
                            height: 30px;
                        }

                        .na_first ul li h5 {
                            background: #f0fee6 none repeat scroll 0 0 !important;
                            border: 1px solid #699f44;
                            box-shadow: 0 0 4px 2px #bec9b6 inset;
                            font-family: "ar_cenaregular";
                            font-size: 17px;
                            font-weight: normal;
                            line-height: 28px;
                            text-align: center;
                            vertical-align: middle;
                            margin: 0;
                            color: #000;
                            padding: 0;
                        }

                        .na_first ul li a.MMenu {
                            background: #f0fee6 none repeat scroll 0 0 !important;
                            border: 1px solid #699f44;
                            box-shadow: 0 0 4px 2px #bec9b6 inset;
                            font-family: "ar_cenaregular";
                            font-size: 17px;
                            font-weight: normal;
                            line-height: 28px;
                            text-align: center;
                            vertical-align: middle;
                            margin: 0;
                            color: #000;
                            padding: 0;
                        }

                        .na_first ul li a {
                            background: rgba(0, 0, 0, 0) linear-gradient(to bottom, #ffffff 0%, #fbfbfb 49%, #d2d2d2 98%, #cce0f2 100%) repeat scroll 0 0;
                            font-family: "ar_cenaregular";
                            font-size: 14px;
                            font-weight: normal;
                            line-height: 28px;
                            display: block;
                            color: #000;
                            text-align: center;
                            vertical-align: middle;
                            border: 1px solid #999;
                            margin: 0;
                            padding: 0;
                            text-decoration: none;
                        }

                        .na_first2 {
                            width: 63px;
                            float: left;
                        }

                            .na_first2 ul li span {
                                height: 30px;
                                font-size: 20px;
                                color: #903;
                            }

                            .na_first2 ul li i {
                                height: 30px;
                                font-size: 20px;
                                color: #903;
                            }

                        .na_first ul li span {
                            color: #008000;
                            font-size: 22px;
                            height: 30px;
                        }



                        .na_first ul li i {
                            height: 30px;
                            font-size: 20px;
                            color: #903;
                        }




                        .right_dr {
                            margin: 7px 0 0 22px;
                        }

                        .down_dr {
                            margin-top: 6px;
                            color: green !important;
                        }

                        .left_dr {
                            margin: 7px 22px 0 0;
                            color: green;
                        }

                        .right2dr {
                            margin-left: 30px;
                        }


                        .na_comlogo {
                            height: 28px;
                            left: 978px;
                            position: absolute;
                            top: 4px;
                            width: 171px;
                        }

                        [class^="flaticon-"]::before, [class*=" flaticon-"]::before, [class^="flaticon-"]::after, [class*=" flaticon-"]::after {
                            width: 20px !important;
                            font-size: 20px !important;
                            padding: 0 10px 0 3px !important;
                            margin: 0;
                        }


                        .na_first ul a {
                            text-align: left !important;
                        }

                        .na_first ul h5 {
                            text-align: left !important;
                        }


                        .na_linqC1a {
                            background: #903 none repeat scroll 0 0;
                            height: 130px;
                            left: 199px;
                            position: absolute;
                            top: 203px;
                            width: 3px;
                        }

                        .na_linqC1b {
                            background: #903 none repeat scroll 0 0;
                            height: 3px;
                            left: 171px;
                            position: absolute;
                            top: 330px;
                            width: 29px;
                        }

                        .na_linqC3a {
                            background: #903 none repeat scroll 0 0;
                            height: 190px;
                            left: 433px;
                            position: absolute;
                            top: 172px;
                            width: 3px;
                        }

                        .na_linqC3b {
                            background: #903 none repeat scroll 0 0;
                            height: 3px;
                            left: 404px;
                            position: absolute;
                            top: 264px;
                            width: 29px;
                        }

                        .na_linqC3c {
                            background: #903 none repeat scroll 0 0;
                            height: 3px;
                            left: 404px;
                            position: absolute;
                            top: 359px;
                            width: 29px;
                        }

                        .na_linqC6a {
                            background: #903 none repeat scroll 0 0;
                            height: 3px;
                            left: 669px;
                            position: absolute;
                            top: 235px;
                            width: 29px;
                        }

                        .na_linqC6b {
                            background: #903 none repeat scroll 0 0;
                            height: 66px;
                            left: 666px;
                            position: absolute;
                            top: 172px;
                            width: 3px;
                        }

                        .na_linqC8a {
                            background: #903 none repeat scroll 0 0;
                            height: 127px;
                            left: 898px;
                            position: absolute;
                            top: 110px;
                            width: 3px;
                        }

                        .na_linqC8b {
                            background: #903 none repeat scroll 0 0;
                            height: 3px;
                            left: 900px;
                            position: absolute;
                            top: 110px;
                            width: 29px;
                        }

                        .na_linqC9a {
                            background: #008000 none repeat scroll 0 0;
                            height: 3px;
                            left: 83px;
                            position: absolute;
                            top: 68px;
                            width: 934px;
                        }

                        .na_linqC9b {
                            background: #008000 none repeat scroll 0 0;
                            height: 33px;
                            left: 549px;
                            position: absolute;
                            top: 35px;
                            width: 3px;
                        }



                        /*.na_linqC9da {
                            background: #008000 none repeat scroll 0 0;
                            height: 3px;
                            left: 316px;
                            position: absolute;
                            top: 469px;
                            width: 470px;
                        }

                        .na_linqC9d1a {
                            background: #008000 none repeat scroll 0 0;
                            height: 33px;
                            left: 549px;
                            position: absolute;
                            top: 437px;
                            width: 3px;
                        }*/




                        .KeyInformation {
                            height: 4px;
                            left: 5px;
                            position: absolute;
                            text-align: center;
                            top: 490px;
                            width: 40px;
                        }

                        .Attendance {
                            height: 4px;
                            left: 70px;
                            position: absolute;
                            text-align: center;
                            top: 490px;
                            width: 40px;
                        }


                        .ServiceHistory {
                            height: 4px;
                            left: 245px;
                            position: absolute;
                            text-align: center;
                            top: 490px;
                            width: 40px;
                        }

                        .Joining {
                            height: 4px;
                            left: 318px;
                            position: absolute;
                            text-align: center;
                            top: 490px;
                            width: 40px;
                        }

                        .Provision {
                            height: 4px;
                            left: 520px;
                            position: absolute;
                            text-align: center;
                            top: 490px;
                            width: 40px;
                        }

                        .Seperation {
                            height: 4px;
                            left: 720px;
                            position: absolute;
                            text-align: center;
                            top: 490px;
                            width: 40px;
                        }

                        .Salary {
                            height: 4px;
                            left: 790px;
                            position: absolute;
                            text-align: center;
                            top: 490px;
                            width: 40px;
                        }

                        .ACR {
                            height: 4px;
                            left: 968px;
                            position: absolute;
                            text-align: center;
                            top: 490px;
                            width: 40px;
                        }

                        .MyInterface {
                            height: 4px;
                            left: 1030px;
                            position: absolute;
                            text-align: center;
                            top: 490px;
                            width: 40px;
                        }

                        .deshb {
                            width: 70px;
                        }

                            .deshb a img {
                                display: table;
                                margin: 0 auto;
                            }

                            .deshb a.span {
                                display: block;
                                text-align: center;
                            }

                            .deshb span.inttext {
                                color: #000;
                                display: block;
                                text-align: center;
                            }


                        /*.inttext {
                            color: #000;
                            display: block;
                            left: 0;
                            text-align: justify;
                            top: 1px !important;
                            width: 78px;
                        }

                        .fsize {
                            font-size: 14px !important;
                            margin-top: 8px;
                        }*/
                    </style>

                    <div id="na_wrawpr1">


                        <div class="KeyInformation deshb">
                            <a href="<%=this.ResolveUrl("~/F_81_Hrm/F_97_MIS/RptMgtInterface.aspx")%>">
                                <img src="Image/logooo.jpg" height="25" width="25" alt="comlogo" />
                                <span class="inttext">Summary</span>
                            </a>
                        </div>

                        <div class="Attendance deshb">
                            <a href="<%=this.ResolveUrl("~/F_81_Hrm/F_83_Att/RptEmpDailyAttendance.aspx?Type=DailyAtten")%>">
                                <img src="Image/logooo.jpg" height="25" width="25" alt="comlogo" />
                                <span class="inttext">Attendance</span>
                            </a>
                        </div>

                        <div class="ServiceHistory deshb">
                            <a href="<%=this.ResolveUrl("~/F_81_Hrm/F_82_App/RptEmpInformation.aspx?Type=Services")%>">
                                <img src="Image/logooo.jpg" height="25" width="25" alt="comlogo" />
                                <span class="inttext">History</span>
                            </a>
                        </div>

                        <div class="Joining deshb">
                            <a href="<%=this.ResolveUrl("~/F_81_Hrm/F_92_Mgt/EmpStatus02.aspx?Type=joiningRpt")%>">
                                <img src="Image/logooo.jpg" height="25" width="25" alt="comlogo" />
                                <span class="inttext">Joining</span>
                            </a>
                        </div>

                        <div class="Provision deshb">
                            <a href="<%=this.ResolveUrl("~/F_81_Hrm/F_92_Mgt/EmpStatus02.aspx?Type=EmpCon")%>">
                                <img src="Image/logooo.jpg" height="25" width="25" alt="comlogo" />
                                <span class="inttext">Provision</span>
                            </a>
                        </div>

                        <div class="Seperation deshb">
                            <a href="<%=this.ResolveUrl("~/F_81_Hrm/F_92_Mgt/EmpStatus02.aspx?Type=SepType")%>">
                                <img src="Image/logooo.jpg" height="25" width="25" alt="comlogo" />
                                <span class="inttext">Seperation</span>
                            </a>
                        </div>

                        <div class="Salary deshb">
                            <a href="<%=this.ResolveUrl("~/F_81_Hrm/F_89_Pay/RpHRtPayroll.aspx?Type=Salary&Entry=Payroll")%>">
                                <img src="Image/logooo.jpg" height="25" width="25" alt="comlogo" />
                                <span class="inttext">Salary</span>
                            </a>
                        </div>

                        <div class="ACR deshb">
                            <a href="<%=this.ResolveUrl("~/F_81_Hrm/F_91_ACR/EmpPerAppraisal.aspx")%>">
                                <img src="Image/logooo.jpg" height="25" width="25" alt="comlogo" />
                                <span class="inttext">ACR</span>
                            </a>
                        </div>

                        <div class="MyInterface deshb">
                            <a href="<%=this.ResolveUrl("~/F_81_Hrm/F_82_App/RptMyInterface.aspx?empid=")%>">
                                <img src="Image/logooo.jpg" height="25" width="25" alt="comlogo" />
                                <span class="inttext">My Interface</span>
                            </a>
                        </div>




                        <div class="na_linqC1a"></div>
                        <div class="na_linqC1b"></div>

                        <div class="na_linqC3a"></div>
                        <div class="na_linqC3b"></div>
                        <div class="na_linqC3c"></div>

                        <div class="na_linqC6a"></div>
                        <div class="na_linqC6b"></div>

                        <div class="na_linqC8a"></div>
                        <div class="na_linqC8b"></div>

                        <div class="na_linqC9a"></div>
                        <div class="na_linqC9b"></div>


                        <div class="na_linqC9da"></div>
                        <div class="na_linqC9d1a"></div>






                        <div class="na_first">
                            <ul>
                                <li></li>
                                <li></li>
                                <li><span class="fa fa-long-arrow-down down_dr fa-fw"></span></li>
                                <li>
                                    <h5><span class="glyph-icon flaticon-budget nfal"></span>Recruitment</h5>
                                </li>
                                <li><span class="fa down_dr fa-long-arrow-down"></span></li>
                                <li><a href="<%=this.ResolveUrl("~/F_81_Hrm/F_81_Rec/JobAdvertisement.aspx")%>"><span class="glyph-icon flaticon-tray30 nfal"></span>Requisition</a></li>
                                <li><a href="<%=this.ResolveUrl("~/F_81_Hrm/F_81_Rec/JobAdvertisement.aspx")%>"><span class="glyph-icon flaticon-graph4 nfal"></span>Approval</a></li>
                                <li><a href="<%=this.ResolveUrl("~/F_81_Hrm/F_81_Rec/JobAdvertisement.aspx")%>"><span class="glyph-icon flaticon-soccer68 nfal"></span>Advertisement</a></li>
                                <li><a href="<%=this.ResolveUrl("~/F_81_Hrm/F_81_Rec/ShortListing.aspx?Type=SList")%>"><span class="glyph-icon flaticon-business73 nfal"></span>Short List</a></li>
                                <li><a href="<%=this.ResolveUrl("~/F_81_Hrm/F_81_Rec/ShortListing.aspx?Type=IResult")%>"><span class="glyph-icon flaticon-discount5 nfal"></span>Interview</a></li>
                                <li><a href="<%=this.ResolveUrl("~/F_81_Hrm/F_81_Rec/LetterOfAppoinment.aspx?Type=LCreate")%>"><span class="glyph-icon flaticon-basket41 nfal"></span>Appointment Letter</a></li>
                            </ul>
                        </div>

                        <div class="na_first2">
                            <ul>

                                <li><a href="<%=this.ResolveUrl("~/")%>"></a></li>
                                <li><a href="<%=this.ResolveUrl("~/")%>"></a></li>
                                <li><a href="<%=this.ResolveUrl("~/")%>"></a></li>
                                <li><a href="<%=this.ResolveUrl("~/")%>"></a></li>
                                <li><a href="<%=this.ResolveUrl("~/")%>"></a></li>
                                <li><a href="<%=this.ResolveUrl("~/")%>"></a></li>
                                <li><span class="fa fa-long-arrow-right  right_dr  fa-fw"></span></li>

                                <li><span class="fa fa-long-arrow-right right_dr  fa-fw"></span></li>
                                <li><a href="<%=this.ResolveUrl("~/")%>"></a></li>
                                <li><a href="<%=this.ResolveUrl("~/")%>"></a></li>
                                <li><a href="<%=this.ResolveUrl("~/")%>"></a></li>
                                <li><a href="<%=this.ResolveUrl("~/")%>"></a></li>



                                <li><a href="<%=this.ResolveUrl("~/")%>"></a></li>
                                <li><a href="<%=this.ResolveUrl("~/")%>"></a></li>
                            </ul>
                        </div>

                        <div class="na_first">
                            <ul>
                                <li></li>
                                <li></li>
                                <li><span class="fa fa-long-arrow-down down_dr fa-fw"></span></li>
                                <li>
                                    <h5><span class="glyph-icon flaticon-international36 nfal"></span>Appointment</h5>
                                </li>
                                <li><span class="fa down_dr fa-long-arrow-down"></span></li>
                                <li class="MMenu"><a href="<%=this.ResolveUrl("~/")%>"><span class="glyph-icon flaticon-payment5 nfal"></span>Data Center </a></li>
                                <li><a href="<%=this.ResolveUrl("~/")%>"><span class="glyph-icon flaticon-research1 nfal"></span>Joining Report </a></li>
                                <li><a href="<%=this.ResolveUrl("~/")%>"><span class="glyph-icon flaticon-information58 nfal"></span>Service Agreement</a></li>
                                <li><a href="<%=this.ResolveUrl("~/F_81_Hrm/F_83_Att/HRDailyAttenManually.aspx")%>"><span class="glyph-icon flaticon-credit56 nfal"></span>Attendance </a></li>
                                <li><a href="<%=this.ResolveUrl("~/F_84_Lea/HREmpLeave.aspx?Type=LeaveRule")%>"><span class="glyph-icon flaticon-shopping3 nfal"></span>Leave rule input </a></li>
                                <li><a href="<%=this.ResolveUrl("~/F_81_Hrm/F_84_Lea/HREmpLeave.aspx?Type=FLeaveApp")%>"><span class="glyph-icon flaticon-coins36 nfal"></span>Leave Application </a></li>
                                <li><a href="<%=this.ResolveUrl("~/F_81_Hrm/F_84_Lea/HREmpLeave.aspx?Type=LeaveApp")%>"><span class="glyph-icon flaticon-home183 nfal"></span>Leave Approval</a></li>
                                <li><a href="<%=this.ResolveUrl("~/F_81_Hrm/F_84_Lea/HREmpLeave.aspx?Type=LeaveApp")%>"><span class="glyph-icon flaticon-customerservice19 nfal"></span>Confirmation </a></li>
                                <%--<li></li>
                                <li></li>
                                <li><span class="fa down_dr fa-long-arrow-down"></span></li>--%>
                            </ul>
                        </div>

                        <div class="na_first2">
                            <ul>


                                <li><a href="<%=this.ResolveUrl("~/")%>"></a></li>
                                <li><a href="<%=this.ResolveUrl("~/")%>"></a></li>
                                <li><a href="<%=this.ResolveUrl("~/")%>"></a></li>
                                <li><a href="<%=this.ResolveUrl("~/")%>"></a></li>
                                <li><a href="<%=this.ResolveUrl("~/")%>"></a></li>
                                <li><span class="fa right_dr fa-long-arrow-right"></span></li>
                                <li><a href="<%=this.ResolveUrl("~/")%>"></a></li>
                                <li><a href="<%=this.ResolveUrl("~/")%>"></a></li>
                                <li><a href="<%=this.ResolveUrl("~/")%>"></a></li>
                                <li><a href="<%=this.ResolveUrl("~/")%>"></a></li>
                                <li><a href="<%=this.ResolveUrl("~/")%>"></a></li>
                                <li><a href="<%=this.ResolveUrl("~/")%>"></a></li>
                                <li><a href="<%=this.ResolveUrl("~/")%>"></a></li>
                                <li><a href="<%=this.ResolveUrl("~/")%>"></a></li>
                                <li><a href="<%=this.ResolveUrl("~/")%>"></a></li>
                                <li><a href="<%=this.ResolveUrl("~/")%>"></a></li>
                                <li><a href="<%=this.ResolveUrl("~/")%>"></a></li>

                            </ul>
                        </div>

                        <div class="na_first">
                            <ul>
                                <li>
                                    <h5><span class="glyph-icon flaticon-thumb32 nfal"></span>Human Resource </h5>
                                </li>
                                <li></li>
                                <li><span class="fa down_dr fa-long-arrow-down"></span></li>
                                <li>
                                    <h5><span class="glyph-icon flaticon-draft nfal"></span>Payroll </h5>
                                </li>

                                <li><span class="fa down_dr fa-long-arrow-down"></span></li>
                                <li><a href="<%=this.ResolveUrl("~/")%>"><span class="glyph-icon flaticon-data79 nfal"></span>Salary Sheet</a></li>
                                <li><a href="<%=this.ResolveUrl("~/")%>"><span class="glyph-icon flaticon-money483 nfal"></span>Cash Salary  </a></li>
                                <li><a href="<%=this.ResolveUrl("~/")%>"><span class="glyph-icon flaticon-data62 nfal"></span>Bank Salary </li>
                                <li><a href="<%=this.ResolveUrl("~/")%>"><span class="glyph-icon flaticon-calculating11 nfal"></span>Forwarding Letter </a></li>
                                <li><a href="<%=this.ResolveUrl("~/F_81_Hrm/F_89_Pay/RpHRtPayroll.aspx?Type=Payslip")%>"><span class="glyph-icon flaticon-data45 nfal"></span>Pay Slip </a></li>
                                <li><a href="<%=this.ResolveUrl("~/")%>"><span class="glyph-icon flaticon-report1 nfal"></span>P.F Rules </a></li>
                                <li><a href="<%=this.ResolveUrl("~/F_81_Hrm/F_90_PF/AccProFund.aspx")%>"><span class="glyph-icon flaticon-truck29 nfal"></span>P.F Accounts (Auto) </a></li>
                                <li></li>


                            </ul>
                        </div>

                        <div class="na_first2">
                            <ul>
                                <li><a href="<%=this.ResolveUrl("~/")%>"></a></li>
                                <li><a href="<%=this.ResolveUrl("~/")%>"></a></li>
                                <li><a href="<%=this.ResolveUrl("~/")%>"></a></li>
                                <li><a href="<%=this.ResolveUrl("~/")%>"></a></li>
                                <li></li>

                                <li><span class="fa left_dr   fa-long-arrow-left"></span></li>

                                <li><a href="<%=this.ResolveUrl("~/")%>"></a></li>
                                <li><a href="<%=this.ResolveUrl("~/")%>"></a></li>
                                <li><a href="<%=this.ResolveUrl("~/")%>"></a></li>
                                <li><a href="<%=this.ResolveUrl("~/")%>"></a></li>
                                <li><a href="<%=this.ResolveUrl("~/")%>"></a></li>
                                <li><a href="<%=this.ResolveUrl("~/")%>"></a></li>


                                <li><a href="<%=this.ResolveUrl("~/")%>"></a></li>
                                <li><a href="<%=this.ResolveUrl("~/")%>"></a></li>
                                <li><a href="<%=this.ResolveUrl("~/")%>"></a></li>
                            </ul>
                        </div>

                        <div class="na_first">
                            <ul>
                                <li></li>
                                <li></li>
                                <li><span class="fa fa-long-arrow-down down_dr fa-fw"></span></li>
                                <li>
                                    <h5><span class="glyph-icon flaticon-house118 nfal"></span>ACR </h5>
                                </li>
                                <li><span class="fa down_dr fa-long-arrow-down"></span></li>
                                <li><a href="<%=this.ResolveUrl("~/")%>"><span class="glyph-icon flaticon-domain2 nfal"></span>Appraisal</a></li>
                                <li><a href="<%=this.ResolveUrl("~/")%>"><span class="glyph-icon flaticon-contract11 nfal"></span>Service Confirmation </a></li>
                                <li><a href="<%=this.ResolveUrl("~/")%>"><span class="glyph-icon flaticon-website17 nfal"></span>Increment</a></li>
                                <li><a href="<%=this.ResolveUrl("~/")%>"><span class="glyph-icon flaticon-register nfal"></span>Promotion</a></li>
                                <li><a href="<%=this.ResolveUrl("~/")%>"><span class="glyph-icon flaticon-list23 nfal"></span>Transfer</a></li>
                                <li><a href="<%=this.ResolveUrl("~/")%>"><span class="glyph-icon flaticon-shoppingcart39 nfal"></span>Training</a></li>
                                <li><a href="<%=this.ResolveUrl("~/")%>"><span class="glyph-icon flaticon-telephone172 nfal"></span>Salary held up</a></li>
                                <li><a href="<%=this.ResolveUrl("~/")%>"><span class="glyph-icon flaticon-waves5 nfal"></span>Termination </a></li>
                                <li><a href="<%=this.ResolveUrl("~/")%>"><span class="glyph-icon flaticon-house223 nfal"></span>Resignation  </a></li>
                                <%--  <li></li>
                                <li><span class="fa down_dr fa-long-arrow-down"></span></li>--%>
                            </ul>
                        </div>

                        <div class="na_first2">
                            <ul>

                                <li><a href="<%=this.ResolveUrl("~/")%>"></a></li>
                                <li><a href="<%=this.ResolveUrl("~/")%>"></a></li>
                                <li><a href="<%=this.ResolveUrl("~/")%>"></a></li>
                                <li><a href="<%=this.ResolveUrl("~/")%>"></a></li>
                                <li><a href="<%=this.ResolveUrl("~/")%>"></a></li>
                                <li><span class="fa  right_dr fa-long-arrow-right"></span></li>
                                <li><span class="fa  right_dr fa-long-arrow-right"></span></li>
                                <li><span class="fa  right_dr fa-long-arrow-right"></span></li>
                                <li><a href="<%=this.ResolveUrl("~/")%>"></a></li>
                                <li><a href="<%=this.ResolveUrl("~/")%>"></a></li>
                                <li><a href="<%=this.ResolveUrl("~/")%>"></a></li>
                                <li><a href="<%=this.ResolveUrl("~/")%>"></a></li>



                            </ul>
                        </div>

                        <div class="na_first">
                            <ul>
                                <li></li>
                                <li></li>
                                <li><span class="fa fa-long-arrow-down down_dr fa-fw"></span></li>
                                <li>
                                    <h5><span class="glyph-icon flaticon-settings49 nfal"></span>Report </h5>
                                </li>
                                <li><span class="fa down_dr fa-long-arrow-down"></span></li>
                                <li><a href="<%=this.ResolveUrl("~/")%>"><span class="glyph-icon flaticon-marketing8 nfal"></span>General</a></li>
                                <li><a href="<%=this.ResolveUrl("~/")%>"><span class="glyph-icon flaticon-full22 nfal"></span>Financial Report </a></li>
                                <li><a href="<%=this.ResolveUrl("~/")%>"><span class="glyph-icon flaticon-discount5 nfal"></span>MIS </a></li>
                                <li></li>

                            </ul>
                        </div>

                    </div>
                    <div class="clearfix"></div>
                </asp:Panel>

                <asp:Panel runat="server" ID="pnlGeneral" Visible="false">
                    <div class="row lbl2SubMenu">
                        <div class="col-md-4">
                        </div>
                        <div class="col-md-4">

                            <ul id="Midul2" class="nav colMid">
                                <li>
                                    <h5>General Reports</h5>
                                </li>
                                <li><a href="F_81_Hrm/F_89_Pay/RpHRtPayroll.aspx?Type=Salary&amp;Entry=Mgt">01. Actual Salary Sheet</a></li>
                                <li><a href="F_81_Hrm/F_83_Att//RptHREmpStatus.aspx?Type=Approval">02. Employee Status</a></li>
                                <li><a href="F_81_Hrm/F_82_App/RptEmpInformation.aspx?Type=Services">03. Employee Services Period Information</a></li>
                                <li><a href="F_81_Hrm/F_92_Mgt/EmpStatus02.aspx?Type=joiningRpt">04. Joining Report Summary</a></li>
                                <li><a href="F_81_Hrm/F_92_Mgt/EmpStatus02.aspx?Type=JoinigdWise">05. New Joiners List</a></li>
                                <li><a href="F_81_Hrm/F_92_Mgt/EmpStatus02.aspx?Type=EmpList">06. Employee List</a></li>
                                <li><a href="F_81_Hrm/F_92_Mgt/EmpStatus02.aspx?Type=TransList">07. Employee Transfer List</a></li>
                                <li><a href="F_81_Hrm/F_92_Mgt/EmpStatus02.aspx?Type=PenEmpCon">08. Pending Employee Confirmation</a></li>
                                <li><a href="F_81_Hrm/F_92_Mgt/EmpStatus02.aspx?Type=EmpCon">09. Employee Confirmation</a></li>
                                <li><a href="F_81_Hrm/F_92_Mgt/EmpStatus02.aspx?Type=Manpower">10. Employee Manpower Report</a></li>
                                <li><a href="F_81_Hrm/F_92_Mgt/EmpStatus02.aspx?Type=SepType">11. Employee Seperation Report</a></li>
                                <li><a href="F_81_Hrm/F_92_Mgt/EmpStatus02.aspx?Type=EmpHold">12. Employee Hold List</a></li>
                                <li><a href="F_81_Hrm/F_92_Mgt/EmpStatus02.aspx?Type=EmpGradeADesig">13. Grade &amp; Designation Wise  Salary Detail</a></li>
                                <li><a href="F_81_Hrm/F_92_Mgt/RptEmpStatus03.aspx?Type=GradeWiseEmp">14. Grade Wise Employee Details</a></li>
                                <li><a href="F_81_Hrm/F_92_Mgt/RptEmpIncrAPro.aspx?Type=Increment">15. Increment (All Employee)</a></li>
                                <li><a href="F_81_Hrm/F_92_Mgt/RptUserLogDetails.aspx">16. Entry, Edit Record</a></li>
                                <li><a href="F_81_Hrm/F_92_Mgt/UserLoginfrm.aspx">17. User Permission</a></li>
                            </ul>


                        </div>
                        <div class="col-md-4">
                        </div>
                    </div>
                </asp:Panel>
                <asp:Panel runat="server" ID="PnlFinReports" Visible="false">
                    <div class="row lbl2SubMenu">
                        <div class="col-md-4">
                        </div>
                        <div class="col-md-4">

                            <ul class="nav colMid">
                                <li>
                                    <h5>Financial Reports</h5>
                                </li>
                                <li><a href="F_81_Hrm/F_89_Pay/RpHRtPayroll.aspx?Type=Salary&amp;Entry=Payroll">01. Actual Salary Sheet</a></li>
                                <li><a href="F_81_Hrm/F_89_Pay/RpHRtPayroll.aspx?Type=Bonus">02. Festival Bonus</a></li>
                                <li><a href="F_81_Hrm/F_83_Att/RptHREmpStatus.aspx?Type=Payroll">03. Employee Status</a></li>
                                <li><a href="F_81_Hrm/F_89_Pay/RpHRtPayroll.aspx?Type=Payslip">04. Pay Slip</a></li>
                                <li><a href="F_81_Hrm/F_89_Pay/RptSalSummary02.aspx?Type=RPTENVELOP">05. Envelop Print</a></li>
                                <li><a href="F_81_Hrm/F_89_Pay/RpHRtPayroll.aspx?Type=Signature">06. Signature Sheet</a></li>
                                <li><a href="F_81_Hrm/F_89_Pay/RptSalSummary02.aspx?Type=CashSalary">07. Salary Statement (Cash)</a></li>
                                <li><a href="F_81_Hrm/F_89_Pay/RptSalSummary02.aspx?Type=CashBonus">08. Bonus Sheet (Cash)</a></li>
                                <li><a href="F_81_Hrm/F_89_Pay/RptSalarySummary.aspx?Type=SalSum">09. Salary Summary</a></li>
                                <li><a href="F_81_Hrm/F_89_Pay/RptSalSummary02.aspx?Type=SalSummary">10. Salary Summary 02</a></li>
                                <li><a href="F_81_Hrm/F_89_Pay/RptSalSummary02.aspx?Type=BonusSummary">11. Bonus Summary</a></li>
                                <li><a href="F_81_Hrm/F_89_Pay/EmpBankSalary.aspx">12. Salary Transfer Statement</a></li>
                                <li><a href="F_81_Hrm/F_89_Pay/RptSalSummaryDetails.aspx">13. Details Salary Summary</a></li>
                                <li><a href="F_81_Hrm/F_89_Pay/RptSalSummary02.aspx?Type=SalLACA">14. Monthly Loan,Adv,Cell,Arrear Data Sheet</a></li>
                                <li><a href="F_81_Hrm/F_89_Pay/RptBankStatement.aspx?Type=Bnkstmntcwise">15. Bank Statement - Company Wise</a></li>
                                <li><a href="F_81_Hrm/F_89_Pay/RptBankStatement.aspx?Type=Bnkstmtbnkwise">16. Bank Statement - Bank Wise</a></li>
                                <li><a href="F_81_Hrm/F_89_Pay/EmpOverTimeSalary.aspx?Type=OvertimeSalary">17. Overtime Allowance</a></li>
                                <li><a href="F_81_Hrm/F_89_Pay/EmpOverTimeSalary.aspx?Type=OvertimeSalary02">17. Overtime Allowance 02</a></li>
                                <li><a href="F_81_Hrm/F_89_Pay/RptSalSummary02.aspx?Type=BonPaySlip">19.Pay Slip (Bonus)</a></li>
                            </ul>


                        </div>
                        <div class="col-md-4">
                        </div>
                    </div>
                </asp:Panel>
                <asp:Panel runat="server" ID="PnlMISReports" Visible="false">
                    <div class="row lbl2SubMenu">
                        <div class="col-md-4">
                        </div>
                        <div class="col-md-4">

                            <ul id="Midul4" class="nav colMid">
                                <li>
                                    <h5>A. MIS Reports</h5>
                                </li>
                                <li><a href="F_81_Hrm/F_82_App/RptEmpInformation.aspx?Type=EmpDyInfo">01. Need Base Report</a></li>
                                <li><a href="F_81_Hrm/F_82_App/RptEmpInformation.aspx?Type=EmpDyInfo02">02. Need Base Report 01</a></li>
                                <li><a href="F_81_Hrm/F_82_App/EmpBirthDeath.aspx?Type=EmpBirthdayList">03. Employee Birthday</a></li>
                                <li><a href="F_81_Hrm/F_97_MIS/RptMgtInterface.aspx">03. Management Interface (HR)</a></li>

                            </ul>


                        </div>
                        <div class="col-md-4">
                        </div>
                    </div>
                </asp:Panel>

                <asp:Panel runat="server" ID="PnlMFg" Visible="false">
                    <div class=" text-center accImg ">
                        <img src="mfg3.png" width="1050" alt="MFG Menu" border="0" usemap="#Map" />
                        <map name="Map" id="Map">
                            <area shape="rect" coords="104,108,278,142" href="F_07_Inv/PurReqEntry02.aspx?InputType=FxtAstEntry" target="_self" />
                            <area shape="rect" coords="224,408,381,435" href="F_07_Inv/PurBillEntry.aspx?Type=BillEntry" target="_self" />
                            <area shape="rect" coords="38,409,195,436" href="F_09_LCM/LCInformation.aspx?tname=costing&amp;tid=cst" target="_self" />
                            <area shape="rect" coords="190,489,364,516" href="F_07_Inv/PurMRREntry.aspx?Type=Entry" target="_self" />
                            <area shape="rect" coords="159,367,321,394" href="F_11_Pro/PurWrkOrderEntry.aspx?InputType=OrderEntry" target="_self" />
                            <area shape="rect" coords="9,276,172,302" href="F_09_LCM/LCInformation.aspx?tname=order&amp;tid=lc" target="_self" />
                            <area shape="rect" coords="224,337,379,361" href="F_07_Inv/PurAprovEntry.aspx?InputType=PurProposal" />
                            <area shape="rect" coords="224,307,377,335" href="F_07_Inv/PurReqEntry02.aspx?InputType=FxtAstApproval" target="_self" />
                            <area shape="rect" coords="436,113,593,137" href="F_03_StdCost/StdCostSheet.aspx?InputType=CostAnna" title="For 100 units production what resources we need" />
                            <area shape="rect" coords="661,174,820,198" href="F_23_SaM/SalesOrderApproval.aspx" target="_self" />
                            <area shape="rect" coords="662,233,819,262" href="F_23_SaM/SalesInvoice.aspx" />
                            <area shape="rect" coords="662,267,819,295" href="F_23_SaM/CustOthMoneyReceipt.aspx" target="_self" />
                            <area shape="rect" coords="663,202,819,228" href="F_23_SaM/DeliveryOrder.aspx" target="_self" />
                            <area shape="rect" coords="661,335,819,362" href="F_15_Acc/AccChalanTransfer.aspx" target="_self" />
                            <area shape="rect" coords="663,558,820,583" href="F_24_SaMF/EntryProformaInvoice.aspx" target="_self" />
                            <area shape="rect" coords="662,623,819,649" href="F_24_SaMF/EntryExportDocs.aspx" target="_self" />
                            <area shape="rect" coords="664,491,824,518" href="F_24_SaMF/ForSalesMemo.aspx" target="_self" />
                            <area shape="rect" coords="671,680,827,708" href="F_24_SaMF/EntryExportDocs.aspx" target="_self" />
                            <area shape="rect" coords="440,584,592,615" href="F_15_Acc/AccDashBoard.aspx" target="_self" alt="AcDashBoard" />
                            <area shape="rect" coords="439,551,519,581" href="F_23_SaM/SalesInformation.aspx" target="_self" alt="sales" />
                            <area shape="rect" coords="437,359,594,386" href="F_19_FGInv/ProReceive.aspx" target="_self" />
                            <area shape="rect" coords="436,203,593,231" href="F_03_StdCost/MatAvailability.aspx" />
                            <area shape="rect" coords="436,452,595,481" href="F_27_Mis/RptBgdVsActPro.aspx?Type=Income" target="_self" />
                            <area shape="rect" coords="436,421,594,447" href="F_15_Acc/AccProductionJV.aspx" target="_self" />
                            <area shape="rect" coords="428,929,587,962" href="#" />
                            <area shape="rect" coords="437,325,594,356" href="F_13_ProdMon/ProductionPlan.aspx" target="_self" />
                            <area shape="rect" coords="436,264,592,291" href="F_11_Pro/ProdReq.aspx" target="_self" />
                            <area shape="rect" coords="100,558,275,591" href="F_07_Inv/PBMatIssueSingle.aspx" target="_self" />
                            <area shape="rect" coords="247,675,420,704" href="F_09_PImp/PurLabIssue.aspx?Type=Current" target="_self" />
                            <area shape="rect" coords="447,677,599,706" href="RptDefault.aspx" target="_self" />
                            <area shape="rect" coords="421,838,579,870" href="F_24_CC/CustMaintenanceWork.aspx" target="_self" />
                            <area shape="rect" coords="422,888,579,915" href="F_32_Mis/RptPrjCostPerSFT.aspx?Type=RemainingCost" target="_self" />
                            <area shape="rect" coords="440,510,592,543" href="F_33_Mgt/DashBoardAll.aspx" target="_self" />
                            <area shape="rect" coords="11,243,172,270" href="F_07_Inv/PurMktSurvey.aspx?Type=MktSurvey" target="_self" />
                            <area shape="rect" coords="8,490,170,515" href="F_09_LCM/LCInformation.aspx?tname=receive&amp;tid=lc" target="_self" />
                            <area shape="rect" coords="102,609,274,639" href="F_09_PImp/PurLabIssue.aspx?Type=Current" target="_self" />
                            <area shape="rect" coords="10,304,172,334" href="F_15_Acc/GeneralAccounts.aspx?tcode=99&amp;tname=Payment Voucher&amp;Mod=Accounts" target="_self" />

                            <area shape="rect" coords="877,324,1036,350" href="F_23_SaM/SalesWiseVat.aspx" target="_self" />
                            <area shape="rect" coords="878,114,1035,139" href="F_01_BPlan/YearlyPlanningBudget.aspx?Type=Yearly" target="_self" />
                            <area shape="rect" coords="878,146,1033,171" href="F_15_Acc/AccMonthlyBgd.aspx" target="_self" />
                            <area shape="rect" coords="878,352,1035,379" href="F_15_Acc/AccSalesJournal.aspx" />
                            <area shape="rect" coords="876,618,1037,644" href="DeafultMenu.aspx?Type=4106" target="_self" />
                            <area shape="rect" coords="879,587,1036,613" href="RptDefault.aspx" target="_self" />
                            <area shape="rect" coords="873,829,1035,860" href="F_26_Alert/GenPage.aspx?Type=All" target="_self" />
                            <area shape="rect" coords="222,277,380,300" href="F_07_Inv/PurReqEntry02.aspx?InputType=HeadUsed" target="_self" />
                            <area shape="rect" coords="890,683,1044,709" href="F_15_Acc/AccOpening.aspx" target="_self" />
                            <area shape="rect" coords="663,588,818,615" href="F_24_SaMF/EntryMasterLC.aspx?Type=LCINF" target="_self" />
                            <area shape="rect" coords="662,423,824,452" href="F_19_FGInv/RptFGInvStore.aspx?Type=FgInv" target="_self" alt="Product Transfer" />
                            <area shape="rect" coords="662,367,821,395" href="F_23_SaM/Chalan.aspx" target="_self" />
                            <area shape="rect" coords="878,538,1036,564" href="#" />
                            <area shape="rect" coords="880,176,1034,202" href="F_15_Acc/GeneralAccounts.aspx?tcode=99&amp;tname=Payment Voucher&amp;Mod=Accounts" target="_self" />
                            <area shape="rect" coords="879,206,1035,233" href="F_15_Acc/GeneralAccounts.aspx?tcode=99&amp;tname=Deposit Voucher&amp;Mod=Accounts" target="_self" />
                            <area shape="rect" coords="879,237,1037,262" href="F_15_Acc/GeneralAccounts.aspx?tcode=99&amp;tname=Journal Voucher&amp;Mod=Accounts" target="_self" />
                            <area shape="rect" coords="522,552,593,581" href="F_11_Pro/PurInformation.aspx" target="_self" alt="Purchase" />
                            <area shape="rect" coords="436,295,594,322" href="F_07_Inv/PBMatIssueSingle.aspx" />
                            <area shape="rect" coords="437,173,594,199" href="F_03_StdCost/ProdBudget.aspx?Type=Entry" target="_self" />
                            <area shape="rect" coords="435,237,593,262" href="F_11_Pro/ProdReq.aspx" target="_self" />
                            <area shape="rect" coords="435,389,593,415" href="F_15_Acc/AccIsuUpdate.aspx" />
                            <area shape="rect" coords="436,143,593,169" href="F_23_SaM/MonthlySalesEntry.aspx" target="_self" title="All centers cumulative target comes here" />
                            <area shape="rect" coords="878,265,1036,289" href="F_15_Acc/AccInterComVoucher.aspx" target="_self" />
                            <area shape="rect" coords="222,243,378,271" href="F_07_Inv/PurMktSurvey.aspx?Type=MktSurvey" target="_self" />
                            <area shape="rect" coords="878,492,1036,520" href="F_15_Acc/SuplierPayment.aspx?tcode=99&amp;tname=Payment Voucher&amp;Mod=Accounts" target="_self" />
                            <area shape="rect" coords="878,464,1035,489" href="#" />
                            <area shape="rect" coords="53,676,205,707" href='<%=this.ResolveUrl("~/DeafultMenu.aspx?Type=4106")%>' target="_self" />
                            <area shape="rect" coords="879,392,1037,417" href="F_15_Acc/AccSalesJournal.aspx" target="_self" />
                            <area shape="rect" coords="662,141,821,167" href="F_21_Mkt/EntrySalesOrder.aspx" target="_self" />
                            <area shape="rect" coords="662,112,819,138" href="F_23_SaM/SalesComProposal.aspx" target="_self" />
                        </map>
                    </div>

                    <div class="clearfix"></div>
                </asp:Panel>

                <asp:Panel runat="server" ID="pnlSetting4106" Visible="false">
                    <div class="row lbl2SubMenu headTagh3 ">
                        <div class="col-md-4">
                        </div>
                        <div class="col-md-4">

                            <h3>A. 	Settings</h3>
                            <ul id="Midul5" class="nav colLeft">
                                <li>
                                    <h5>A1. Code Book</h5>
                                </li>
                                <li><a href="F_15_Acc/AccCodeBook.aspx?InputType=Accounts">01. Accounts Code Book</a></li>
                                <li><a href="F_15_Acc/AccSubCodeBook.aspx?InputType=ResCodePrint">02. Details Code</a></li>
                                <li><a href="F_15_Acc/AccOpening.aspx">01. Accounts Opening</a></li>
                                <li>
                                    <h5>A2. Production</h5>
                                </li>
                                <li><a href="F_07_Inv/MiniStockInput.aspx">01. Stock Label Input - Material</a></li>
                                <li><a href="F_07_Inv/MiniProStockInput.aspx">01. Stock Label Input - Product</a></li>
                                <li>
                                    <h5>A3. Sales</h5>
                                </li>
                                <li><a href="F_23_SaM/SalesPlaningCodeBook.aspx">01. Sales Team Code Book</a></li>
                                <li><a href="F_23_SaM/MonthlySalesBudget.aspx?Type=Monthly">02. Monthly Sales &amp; Collection Target</a></li>
                                <li><a href="F_23_SaM/ProPrice.aspx">03. Product Price</a></li>
                                <li><a href="F_81_Hrm/F_82_App/HRCodeBook.aspx">04. Information Field</a></li>
                                <li><a href="F_81_Hrm/F_82_App/HRDesigCode.aspx">05. Designation Code Book</a></li>
                                <li><a href="F_23_SaM/AccCrDr.aspx?Type=Cr">06. Create Customer</a></li>
                                <li><a href="F_81_Hrm/F_82_App/EmpEntry02.aspx?Type=Entry&empid=">07. Sales Team Setup</a></li>

                                <li>
                                    <h5>A4. Local Procurement</h5>
                                </li>
                                <li><a href="F_23_SaM/SalesCodeBook.aspx?Type=Procurement">01. Information Field</a></li>
                                <li><a href="F_11_Pro/PurSupplierinfo.aspx">02. Supplier Information</a></li>
                                <li><a href="F_07_Inv/PurMktSurvey.aspx?Type=SurveyLink">03. Survey Link</a></li>
                                <li><a href="F_07_Inv/PurMktSurvey.aspx?Type=MktSurvey">04. Comparative Statement</a></li>
                                <li>
                                    <h5></h5>
                                </li>

                                <li>
                                    <h5>B6. Cash Sales</h5>
                                </li>
                                <li><a href="F_23_SaM/SalesMemo.aspx">01. Sales Memo-Direct</a></li>
                                <li>
                                    <h5>B7. Foreign Sales</h5>
                                </li>
                                <li><a href="F_24_SaMF/EntryProformaInvoice.aspx">01. Proforma Invoice</a></li>
                                <li><a href="F_24_SaMF/EntryMasterLC.aspx?Type=LCINF">02. L/C Information</a></li>
                                <li><a href="F_24_SaMF/ForSalesMemo.aspx">03. Foreign Sales</a></li>
                                <li>
                                    <h5></h5>
                                </li>

                            </ul>


                        </div>
                        <div class="col-md-4">
                        </div>
                    </div>
                </asp:Panel>


                <asp:Panel runat="server" ID="mfgAcc" Visible="false">
                    <div class="row ">
                        <div class="col-md-3 mfgacc2 ">
                            <ul class="sideMenu ">
                                <li><a href="#" style="background: #ffffff; border-radius: 0%; box-shadow: none; border: none;"></a></li>
                                <li><a class="nonBg" href="<%=this.ResolveUrl("~/F_15_Acc/AccDashBoard.aspx")%>" target="_blank">
                                    <img src="Image/int1.png" /><span style="padding: 0px; margin-left: 12px;">Interface</span></a></li>
                                <li><a href="#" style="background: #ffffff; border-radius: 0%; box-shadow: none; border: none;"></a></li>
                                <li><a href="#" style="background: #ffffff; border-radius: 0%; box-shadow: none; border: none;"></a></li>
                                <li><a href="#" style="background: #ffffff; border-radius: 0%; box-shadow: none; border: none;"></a></li>
                                <li><a href="#" style="background: #ffffff; border-radius: 0%; box-shadow: none; border: none;"></a></li>
                                <li><a href="F_15_Acc/RptAccDTransaction.aspx?Type=Accounts&TrMod=DTran" target="_blank">Daily Transaction</a></li>
                                <li><a href="F_15_Acc/AccLedger.aspx?Type=Ledger&RType=GLedger" target="_blank">General Ledger</a></li>
                                <li><a href="<%=this.ResolveUrl("~/GenPage.aspx?Type=09")%>" target="_blank">All Reports</a></li>
                                <li><a href="#" style="background: #ffffff; border-radius: 0%; box-shadow: none; border: none;"></a></li>
                                <li><a href="#" style="background: #ffffff; border-radius: 0%; box-shadow: none; border: none;"></a></li>
                                <li><a href="#" style="background: #ffffff; border-radius: 0%; box-shadow: none; border: none;"></a></li>
                                <li><a href="F_15_Acc/AccCodeBook.aspx?InputType=Accounts" target="_blank">Chart Of Accounts</a></li>
                                <li><a href="F_15_Acc/AccOpening.aspx" target="_blank">Accounts Opening</a></li>
                                <li><a href="DeafultMenu.aspx?Type=4110" target="_blank">Settings</a></li>

                            </ul>

                        </div>
                        <div class="col-md-9">


                            <div class="row menu10">

                                <table class="menuTb" width="200" border="0" cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td height="52">
                                            <img class="iconImg" src="Image/2.png" width="45" height="38" alt="d" /></td>
                                        <td class="mainTitelA"><a href="<%=this.ResolveUrl("~/F_15_Acc/SuplierPayment.aspx?tcode=99&tname=Payment Voucher&Mod=Accounts")%>" target="_blank">Pay Bill</a></td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>
                                            <img class="iconImg" src="Image/12.png" width="43" height="33" alt="x" /></td>

                                        <td class="mainTitelA"><a href="<%=this.ResolveUrl("~/F_23_SaM/CustOthMoneyReceipt.aspx")%>" target="_blank">Sales</a></td>
                                    </tr>
                                    <tr>
                                        <td>&nbsp;</td>
                                        <td>
                                            <img class="arrowUp" src="Image/4.png" width="17" height="34" /></td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>
                                            <img class="arrowUp" src="Image/4.png" alt="" width="17" height="34" /></td>
                                    </tr>
                                    <tr>
                                        <td height="37">&nbsp;</td>
                                        <td class="mainTitelA menubg">Payment</td>
                                        <td>
                                            <img class="arrowleft" src="Image/6.png" width="37" height="19" alt="x" /></td>
                                        <td>
                                            <img class="iconImg" src="Image/10.png" width="58" height="50" alt="x" /></td>
                                        <td class="mainTitelA menubg">Accounts</td>
                                        <td>
                                            <img class="arrowRight" src="Image/7.png" width="35" height="19" alt="x" /></td>
                                        <td>&nbsp;</td>
                                        <td class="mainTitelA menubg">Collection</td>
                                    </tr>
                                    <tr>
                                        <td>&nbsp;</td>
                                        <td>
                                            <img class="arrowDown" src="Image/5.png" width="18" height="31" alt=" c" /></td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>
                                            <img class="arrowDown" src="Image/5.png" width="18" height="31" alt=" c" /></td>
                                        <td>&nbsp;</td>
                                        <td>&nbsp;</td>
                                        <td>
                                            <img class="arrowDown" src="Image/5.png" width="18" height="31" alt=" c" /></td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <img class="iconImg" src="Image/3.png" width="43" height="33" alt="x" /></td>
                                        <td class="mainTitelA"><a href="<%=this.ResolveUrl("~/F_15_Acc/GeneralAccounts.aspx?tcode=99&tname=Payment Voucher&Mod=Accounts")%>" target="_blank">General</a></td>
                                        <td>&nbsp;</td>
                                        <td>
                                            <img class="iconImg" src="Image/11.png" width="43" height="33" alt="x" /></td>

                                        <td class="mainTitelA"><a href="<%=this.ResolveUrl("~/F_15_Acc/GeneralAccounts.aspx?tcode=99&tname=Journal Voucher&Mod=Accounts")%>" target="_blank">Journal</a></td>
                                        <td>&nbsp;</td>
                                        <td>
                                            <img class="iconImg" src="Image/3.png" width="43" height="33" alt="x" /></td>
                                        <td class="mainTitelA"><a href="<%=this.ResolveUrl("~/F_15_Acc/GeneralAccounts.aspx?tcode=99&tname=Deposit Voucher&Mod=Accounts")%>" target="_blank">General</a></td>
                                    </tr>
                                </table>
                            </div>





                            <div class="row">

                                <div class="col-md-12 relatedItems ">
                                    <h3>Related Items</h3>


                                    <ul class="nav-pills">
                                        <li><a href="<%=this.ResolveUrl("~/F_15_Acc/AccPayment.aspx?tcode=99&tname=Payment Voucher&Type=Acc")%>" target="_blank"><span class=""></span>PDC Payment</a></li>
                                        <%--glyphicon glyphicon-unchecked--%>
                                        <li><a href="<%=this.ResolveUrl("~/F_15_Acc/AccInterComVoucher.aspx")%>" target="_blank"><span class=""></span>Inter Company</a></li>
                                        <li><a href="<%=this.ResolveUrl("~/F_15_Acc/AccSales.aspx")%>" target="_blank"><span class=""></span>Collection Update</a></li>
                                        <li><a href="<%=this.ResolveUrl("~/F_15_Acc/AccPurchase.aspx")%>" target="_blank"><span class=""></span>Purchase Update</a></li>
                                        <li><a href="<%=this.ResolveUrl("~/F_15_Acc/AccPurchaseFor.aspx")%>" target="_blank"><span class=""></span>Import Update</a></li>
                                        <li><a href="<%=this.ResolveUrl("~/F_15_Acc/AccIsuUpdate.aspx")%>" target="_blank"><span class=""></span>Issue Update</a></li>

                                        <li><a href="<%=this.ResolveUrl("~/F_15_Acc/AccProductionJV.aspx")%>" target="_blank"><span class=""></span>Production Update</a></li>
                                        <li><a href="<%=this.ResolveUrl("~/F_15_Acc/AccChalanTransfer.aspx")%>" target="_blank"><span class=""></span>Transfer Update</a></li>
                                        <li><a href="<%=this.ResolveUrl("~/F_15_Acc/AccSalesJournal.aspx")%>" target="_blank"><span class=""></span>Sales Update</a></li>
                                        <li><a href="<%=this.ResolveUrl("~/F_15_Acc/AccBankRecon.aspx?Type=Acc")%>" target="_blank"><span class=""></span>Reconciliation</a></li>

                                    </ul>

                                </div>
                            </div>

                            <div class="clearfix"></div>
                        </div>




                    </div>

                    <div class="clearfix"></div>
                </asp:Panel>

                <asp:Panel runat="server" ID="Panel8" Visible="false">
                    <div class="row flowMenu">

                        <div class="col-md-4">
                            <h3>Modules</h3>
                            <ul>



                                <li><span>
                                    <img class="menuImg" src="Image/LP.png" /></span><a href="<%=this.ResolveUrl("~/GenPage.aspx?Type=All")%>">Business Planning</a></li>

                                <li><span>
                                    <img class="menuImg" src="Image/B1.png" /></span><a href="<%=this.ResolveUrl("~/GenPage.aspx?Type=All")%>">Cost & Budget
                                    </a></li>

                                <li><span>
                                    <img class="menuImg" src="Image/CONS.png" /></span><a href="<%=this.ResolveUrl("~/GenPage.aspx?Type=All")%>">Raw Materials inventory</a></li>
                                <li><span>
                                    <img class="menuImg" src="Image/CONS.png" /></span><a href="<%=this.ResolveUrl("~/GenPage.aspx?Type=All")%>">Foreign Procurement</a></li>
                                <li><span>
                                    <img class="menuImg" src="Image/CC.png" /></span><a href="<%=this.ResolveUrl("~/GenPage.aspx?Type=All")%>">Local Procurement</a></li>
                                <li><span>
                                    <img class="menuImg" src="Image/S1.png" /></span><a href="<%=this.ResolveUrl("~/GenPage.aspx?Type=All")%>">Production Monitoring</a></li>
                                <li><span>
                                    <img class="menuImg" src="Image/CR.png" /></span><a href="<%=this.ResolveUrl("~/GenPage.aspx?Type=All")%>">General Accounts</a></li>

                                <li><span>
                                    <img class="menuImg" src="Image/FIN.png" /></span><a href="<%=this.ResolveUrl("~/GenPage.aspx?Type=All")%>">Finished Goods Inventory</a></li>
                                <li><span>
                                    <img class="menuImg" src="Image/ACC.png" /></span><a href="<%=this.ResolveUrl("~/GenPage.aspx?Type=All")%>">Service Management</a></li>

                                <li><span>
                                    <img class="menuImg" src="Image/ABP.png" /></span><a href="<%=this.ResolveUrl("~/GenPage.aspx?Type=All")%>">Sales Module</a></li>
                                <li><span>
                                    <img class="menuImg" src="Image/SO.png" /></span><a href="<%=this.ResolveUrl("~/GenPage.aspx?Type=All")%>">Foreign Sales</a></li>
                                <li><span>
                                    <img class="menuImg" src="Image/20.jpg" /></span><a href="<%=this.ResolveUrl("~/GenPage.aspx?Type=All")%>">Fixed Assets</a></li>

                                <li><span>
                                    <img class="menuImg" src="Image/3.jpg" /></span><a href="<%=this.ResolveUrl("~/GenPage.aspx?Type=All")%>">MIS</a></li>
                                <li><span>
                                    <img class="menuImg" src="Image/17.jpg" /></span><a href="<%=this.ResolveUrl("~/GenPage.aspx?Type=All")%>">Documentation</a></li>




                            </ul>

                        </div>
                        <div class="col-md-4">
                            <h3>DashBoard</h3>
                            <ul class="dashCir">
                                <li><a href="<%=this.ResolveUrl("~/F_23_SaM/SalesInformation.aspx")%>"><span>
                                    <img class="" src="Image/S2.png" /></span>Sales</a></li>
                                <li><a href="<%=this.ResolveUrl("~/F_11_Pro/PurInformation.aspx")%>"><span>
                                    <img class="" src="Image/P.png" /></span>Purchase</a></li>
                                <li><a href="<%=this.ResolveUrl("~/F_13_ProdMon/ProductionInfo.aspx")%>"><span>
                                    <img class="" src="Image/PRO.png" /></span>Production</a></li>
                                <li><a href="<%=this.ResolveUrl("~/F_15_Acc/AccDashBoard.aspx")%>"><span>
                                    <img class="" src="Image/ACC2.png" /></span>Accounts</a></li>
                                <li><a href="<%=this.ResolveUrl("~/F_33_Mgt/DashBoardAll.aspx")%>"><span>
                                    <img class="" src="Image/15.png" /></span>Overall</a></li>
                                <li><a href="GenPage.aspx?Type=All"><span>
                                    <img class="" src="Image/ALL.png" /></span>All Reports</a></li>

                                <li><a href="<%=this.ResolveUrl("~/F_07_Inv/RptCentralStore.aspx?InputType=General")%>"><span>
                                    <img class="" src="Image/money.png" /></span>Materials</a></li>

                                <li><a href="<%=this.ResolveUrl("~/F_23_SaM/StoreWiseStock.aspx")%>"><span>
                                    <img class="" src="Image/SE.png" /></span>Products</a></li>



                                <li><a href="<%=this.ResolveUrl("~/F_25_Fxt/RptFixAsset02.aspx")%>"><span>
                                    <img class="" src="Image/icon20.png" /></span>Fixed Assets</a></li>

                            </ul>
                            <div class="clearfix"></div>
                        </div>
                        <div class="col-md-4">
                            <h3>Action</h3>

                            <ul>
                                <li><span>
                                    <img class="menuImg" src="Image/LP.png" /></span><a href="<%=this.ResolveUrl("~/F_81_Hrm/F_92_Mgt/AllEmpList.aspx")%>">Members</a></li>
                                <%-- <li><span>
                                    <img class="menuImg" src="Image/PF.png" /></span><a href="<%=this.ResolveUrl("~/DeafultMenu.aspx?Type=pfpanal")%>">Project Feasibility</a></li>--%>
                                <li><span>
                                    <img class="menuImg" src="Image/B1.png" /></span><a href="<%=this.ResolveUrl("~/F_81_Hrm/F_99_MgtAct/RptgroupAttendance.aspx")%>">Attendance</a></li>

                                <li><span>
                                    <img class="menuImg" src="Image/B1.png" /></span><a href="<%=this.ResolveUrl("~/F_81_Hrm/F_83_Att/RptWeekPresence.aspx")%>">Attendance(W)</a></li>
                                <%-- <li><span>
                                    <img class="menuImg" src="Image/PP.png" /></span><a href="<%=this.ResolveUrl("~/DeafultMenu.aspx?Type=pppanal")%>">Project Planning</a></li>--%>
                                <li><span>
                                    <img class="menuImg" src="Image/CONS.png" /></span><a href="<%=this.ResolveUrl("~/F_81_Hrm/F_82_App/RptEmpInformation.aspx?Type=EmpAllInfo")%>">Profile</a></li>

                                <%-- <li><span>
                                    <img class="menuImg" src="Image/MS.png" /></span><a href="<%=this.ResolveUrl("~/DeafultMenu.aspx?Type=mspanal")%>">Materials Store</a></li>
                                <li><span>
                                    <img class="menuImg" src="Image/MAR.png" /></span><a href="<%=this.ResolveUrl("~/DeafultMenu.aspx?Type=Marketpanal")%>">Marketing</a></li>--%>
                                <li><span>
                                    <img class="menuImg" src="Image/CC.png" /></span><a href="<%=this.ResolveUrl("~/F_81_Hrm/F_97_MIS/RptNewJoingInfo.aspx")%>">Joining</a></li>
                                <li><span>
                                    <img class="menuImg" src="Image/S1.png" /></span><a href="<%=this.ResolveUrl("~/F_81_Hrm/F_92_Mgt/EmpStatus02.aspx?Type=EmpCon")%>">Provision</a></li>
                                <li><span>
                                    <img class="menuImg" src="Image/CR.png" /></span><a href="<%=this.ResolveUrl("~/F_81_Hrm/F_92_Mgt/EmpStatus02.aspx?Type=SepType")%>">Separation</a></li>

                                <li><span>
                                    <img class="menuImg" src="Image/FIN.png" /></span><a href="<%=this.ResolveUrl("~/F_81_Hrm/F_89_Pay/RpHRtPayroll.aspx?Type=Salary&Entry=Payroll")%>">Salary</a></li>
                                <li><span>
                                    <img class="menuImg" src="Image/ACC.png" /></span><a href="<%=this.ResolveUrl("~/F_81_Hrm/F_89_Pay/RpHRtPayroll.aspx?Type=Salary&Entry=Payroll")%>">ACR</a></li>

                                <li><span>
                                    <img class="menuImg" src="Image/ABP.png" /></span><a href="<%=this.ResolveUrl("~/F_81_Hrm/F_82_App/RptMyInterface.aspx?empid=")%>">My Interface</a></li>
                                <li><span>
                                    <img class="menuImg" src="Image/326.jpg" /></span><a href="<%=this.ResolveUrl("~/GenPage.aspx?Type=All")%>">KPI</a></li>


                            </ul>



                        </div>
                    </div>

                    <div class="clearfix"></div>
                </asp:Panel>

                <asp:Panel runat="server" ID="PanTrading" Visible="false">






                    <div class="row flowMenu">
                        <div class="col-md-1"></div>
                        <div class="col-md-3">
                            <h3>Modules</h3>
                            <ul>
                                <li><span>
                                    <img class="menuImg" src="Image/B1.png" /></span><a href="<%=this.ResolveUrl("~/DeafultMenu.aspx?Type=04")%>">Budget</a></li>
                                <li><span>
                                    <img class="menuImg" src="Image/FP.png" /></span><a href="<%=this.ResolveUrl("~/StepofOperation.aspx")%>">Import</a></li>
                                <li><span>
                                    <img class="menuImg" src="Image/LP.png" /></span><a href="<%=this.ResolveUrl("~/DeafultMenu.aspx?Type=14")%>">Local Purchase</a></li>

                                <li><span>
                                    <img class="menuImg" src="Image/6.jpg" /></span><a href="<%=this.ResolveUrl("~/DeafultMenu.aspx?Type=5001")%>">Goods Inventory</a></li>
                                <li><span>
                                    <img class="menuImg" src="Image/MAR.png" /></span><a href="<%=this.ResolveUrl("~/DeafultMenu.aspx?Type=21")%>">Marketing</a></li>
                                <li><span>
                                    <img class="menuImg" src="Image/S1.png" /></span><a href="<%=this.ResolveUrl("~/DeafultMenu.aspx?Type=22")%>">Sales</a></li>
                                <li><span>
                                    <img class="menuImg" src="Image/EX.png" /></span><a href="<%=this.ResolveUrl("~/StepofOperation.aspx")%>">Export</a></li>
                                <li><span>
                                    <img class="menuImg" src="Image/ACC.png" /></span><a href="<%=this.ResolveUrl("~/DeafultMenu.aspx?Type=5010")%>">Accounts</a></li>
                                <li><span>
                                    <img class="menuImg" src="Image/SO.png" /></span><a href="<%=this.ResolveUrl("~/StepofOperation.aspx")%>">Steps Of Operation</a></li>

                            </ul>

                        </div>
                        <div class="col-md-4">
                            <h3 style="padding-left: 118px;">DashBoard</h3>
                            <ul class="dashCir">

                                <li><a href="<%=this.ResolveUrl("~/F_23_SaM/SalesInformation.aspx")%>"><span>
                                    <img class="" src="Image/S2.png" /></span>Sales</a></li>
                                <li><a href="<%=this.ResolveUrl("~/F_11_Pro/PurInformation.aspx")%>"><span>
                                    <img class="" src="Image/P.png" /></span>Import</a></li>
                                <li><a href="<%=this.ResolveUrl("~/F_13_ProdMon/ProductionInfo.aspx")%>"><span>
                                    <img class="" src="Image/PRO.png" /></span>Export</a></li>
                                <li><a href="<%=this.ResolveUrl("~/F_15_Acc/AccDashBoard.aspx")%>"><span>
                                    <img class="" src="Image/ACC2.png" /></span>Accounts</a></li>
                                <li><a href="<%=this.ResolveUrl("~/F_33_Mgt/DashBoardAll.aspx")%>"><span>
                                    <img class="" src="Image/15.png" /></span>Overall</a></li>
                                <li><a href="GenPage.aspx?Type=All"><span>
                                    <img class="" src="Image/ALL.png" /></span>All Reports</a></li>

                            </ul>
                            <div class="clearfix"></div>
                        </div>
                        <div class="col-md-3">
                            <h3>Action</h3>
                            <ul>


                                <li><span>
                                    <img class="menuImg" src="Image/FLOW.png" /></span><a href="<%=this.ResolveUrl("~/DeafultMenu.aspx?Type=4105")%>">Flow Chart</a></li>
                                <%-- <li><span>
                                    <img class="menuImg" src="Image/ALL.png" /></span><a href="GenPage.aspx?Type=All">All Reports</a></li>--%>
                                <li><span>
                                    <img class="menuImg" src="Image/SE.png" /></span><a href="<%=this.ResolveUrl("~/DeafultMenu.aspx?Type=4105")%>">Settings</a></li>
                                <li style="height: 280px; border: none;"></li>



                                <li><span>

                                    <img class="menuImg" src="Image/16.jpg" /></span>
                                    <asp:LinkButton ID="LinkButton4" runat="server" OnClick="lnkbtnGeneral_Click">General</asp:LinkButton>


                                </li>

                                <li><span>

                                    <img class="menuImg" src="Image/16.jpg" /></span>
                                    <asp:LinkButton ID="LinkButton5" runat="server" OnClick="lnkbtnHr_Click">HR Management</asp:LinkButton>


                                </li>


                                <li><span>

                                    <img class="menuImg" src="Image/16.jpg" /></span>
                                    <asp:LinkButton ID="LinkButton6" runat="server" OnClick="lnkbtnKPI_Click">KPI</asp:LinkButton>


                                </li>

                                <li><span>
                                    <img class="menuImg" src="Image/GRP.png" /></span><a href="<%=this.ResolveUrl("~/StepofOperation.aspx")%>">Group Information</a></li>
                            </ul>
                        </div>
                        <%--<div class="col-md-3">
                            <h3>Action</h3>
                            <ul>
                                <li><span>
                                    <img class="menuImg" src="Image/FLOW.png" /></span><a href="<%=this.ResolveUrl("~/DeafultMenu.aspx?Type=4105")%>">Flow Chart</a></li>

                                <li><span>
                                    <img class="menuImg" src="Image/SE.png" /></span><a href="<%=this.ResolveUrl("~/DeafultMenu.aspx?Type=4105")%>">Settings</a></li>
                                <li style="height: 200px; border: none;"></li>

                                <li><span>

                                    <img class="menuImg" src="Image/16.jpg" /></span><a href="<%=this.ResolveUrl("~/DeafultMenu.aspx?Type=4101")%>">HR Management</a></li>

                                <li><span>
                                    <img class="menuImg" src="Image/GRP.png" /></span><a href="<%=this.ResolveUrl("~/StepofOperation.aspx")%>">Group Information</a></li>
                            </ul>
                        </div>--%>
                    </div>

                    <div class="clearfix"></div>
                </asp:Panel>

                <asp:Panel runat="server" ID="PnlGrp" Visible="false">
                    <div class="row flowMenu">
                        <div class="col-md-1"></div>
                        <div class="col-md-3 pading5px">
                            <h3>Horizontal Reports</h3>
                            <ul>

                                <li><span>
                                    <img class="menuImg" src="Image/5.jpg" /></span><a href="<%=this.ResolveUrl("~/F_35_GrAcc/RptAccRecPayment.aspx?Type=TrialBalance")%>">Trial Balance</a></li>

                                <li><span>
                                    <img class="menuImg" src="Image/MAR.png" /></span><a href="<%=this.ResolveUrl("~/F_35_GrAcc/RptAccRecPayment.aspx?Type=BalanceSheet")%>">Statement of Financial Position</a></li>
                                <li><span>
                                    <img class="menuImg" src="Image/6.jpg" /></span><a href="<%=this.ResolveUrl("~/F_35_GrAcc/RptAccRecPayment.aspx?Type=IncomeStatement")%>">Statement of Comprehensive Income</a></li>
                                <li><span>
                                    <img class="menuImg" src="Image/SO.png" /></span><a href="<%=this.ResolveUrl("~/F_35_GrAcc/RptAccRecPayment.aspx?Type=CashFlow")%>">Statement of Cash Flow</a></li>

                                <li><span>
                                    <img class="menuImg" src="Image/B1.png" /></span><a href="<%=this.ResolveUrl("~/F_35_GrAcc/RptAccRecPayment.aspx?Type=RecAndPayment")%>">Receipts & Payment</a></li>
                                <li><span>
                                    <img class="menuImg" src="Image/FP.png" /></span><a href="<%=this.ResolveUrl("~/F_35_GrAcc/RptGrpAccDailyTransaction.aspx?Type=GrpDTransaction")%>">Daily Transaction</a></li>
                                <li><span>
                                    <img class="menuImg" src="Image/LP.png" /></span><a href="<%=this.ResolveUrl("~/F_35_GrAcc/RptAccRecPayment.aspx?Type=BankBalance")%>">Bank Balance</a></li>
                                <li><span>
                                    <img class="menuImg" src="Image/RI.png" /></span><a href="<%=this.ResolveUrl("~/F_35_GrAcc/RptAccRecPayment.aspx?Type=Schedule")%>">Schedule</a></li>

                                <li><span>
                                    <img class="menuImg" src="Image/S1.png" /></span><a href="<%=this.ResolveUrl("~/F_35_GrAcc/RptAccRecPayment.aspx?Type=BalanceDet")%>">Bank Balance Details</a></li>
                                <%-- <li><span>
                                   <img class="menuImg" src="Image/EX.png" /></span><a href="<%=this.ResolveUrl("~/F_35_GrAcc/RptAccRecPayment.aspx?Type=IssueVsCollect")%>">Issue Vs. Collection</a></li>
                                <li><span>
                                    <img class="menuImg" src="Image/ACC.png" /></span><a href="<%=this.ResolveUrl("~/F_35_GrAcc/RptYearlySalCollPur.aspx")%>">Daily Target & Acheivement</a></li>
--%>

                            </ul>

                        </div>
                        <div class="col-md-4">
                            <h3>List of Companies</h3>
                            <div style="margin-left: 30px; margin-top: 5px;">




                                <%--  <asp:RadioButtonList ID="rbtnList1" runat="server" AutoPostBack="True" RepeatDirection="Vertical" RepeatColumns="1"
                                    OnSelectedIndexChanged="rbtnList1_SelectedIndexChanged" CssClass="nrbt myrbt">
                                </asp:RadioButtonList>--%>

                                <asp:RadioButtonList ID="rbtnList1" runat="server" AutoPostBack="True" RepeatDirection="Vertical" RepeatColumns="1"
                                    OnSelectedIndexChanged="rbtnList1_SelectedIndexChanged" CssClass="myrbtlist">
                                </asp:RadioButtonList>

                            </div>
                            <div class="clearfix"></div>
                        </div>


                        <div class="col-md-3 pading5px">
                            <h3>Vertical Reports</h3>

                            <ul class="dashCir dashCir1">

                                <li style="width: 100%; height: 70px;">
                                    <br />
                                </li>



                                <li><a href="<%=this.ResolveUrl("~/F_36_GrMgtInter/RptGrpMisDailyActiviteis.aspx")%>"><span>
                                    <img class="" src="Image/FLOW.png" /></span>All in One</a></li>

                                <%--                                   <li><a href="<%=this.ResolveUrl("~/F_36_GrMgtInter/RptGrpMisDailyActiviteis.aspx")%>"><span>
                                    <img class="" src="Image/SE.png" /></span>Management Interface</a></li>--%>


                                <li><span>

                                    <img class="menuImg" src="Image/L1.png" /></span>
                                    <asp:LinkButton ID="LinkButton1" runat="server" OnClick="lnkbtnCompany_Click">Back</asp:LinkButton>


                                </li>

                            </ul>

                        </div>
                    </div>





                    <div class="clearfix"></div>
                </asp:Panel>

                <asp:Panel runat="server" ID="PnlGrpDetails" Visible="false">
                    <div class="row flowMenu">
                      
                        <div class="col-md-4">
                            <%--<h3>General Reports</h3>--%>
                            <%--<ul>
                                <li><span>
                                    <img class="menuImg" src="Image/B1.png" /></span><a href="<%=this.ResolveUrl("~/F_35_GrAcc/RptAccRecPayment.aspx?Type=RecAndPayment")%>">Receipts & Payment</a></li>
                                <li><span>
                                    <img class="menuImg" src="Image/FP.png" /></span><a href="<%=this.ResolveUrl("~/F_35_GrAcc/RptGrpAccDailyTransaction.aspx?Type=GrpDTransaction")%>">Daily Transaction</a></li>
                                <li><span>
                                    <img class="menuImg" src="Image/LP.png" /></span><a href="<%=this.ResolveUrl("~/F_35_GrAcc/RptAccRecPayment.aspx?Type=BankBalance")%>">Bank Balance</a></li>
                                <li><span>
                                    <img class="menuImg" src="Image/RI.png" /></span><a href="<%=this.ResolveUrl("~/F_35_GrAcc/RptAccRecPayment.aspx?Type=Schedule")%>">Schedule</a></li>
                                <li><span>
                                    <img class="menuImg" src="Image/5.jpg" /></span><a href="<%=this.ResolveUrl("~/F_35_GrAcc/RptAccRecPayment.aspx?Type=TrialBalance")%>">Trial Balance</a></li>
                                <li><span>
                                    <img class="menuImg" src="Image/6.jpg" /></span><a href="<%=this.ResolveUrl("~/F_35_GrAcc/RptAccRecPayment.aspx?Type=IncomeStatement")%>">Statement of Comprehensive Income</a></li>
                                <li><span>
                                    <img class="menuImg" src="Image/MAR.png" /></span><a href="<%=this.ResolveUrl("~/F_35_GrAcc/RptAccRecPayment.aspx?Type=BalanceSheet")%>">Statement of Financial Position</a></li>
                                <li><span>
                                    <img class="menuImg" src="Image/S1.png" /></span><a href="<%=this.ResolveUrl("~/F_35_GrAcc/RptAccRecPayment.aspx?Type=BalanceDet")%>">Bank Balance Details</a></li>
                                <li><span>
                                    <img class="menuImg" src="Image/EX.png" /></span><a href="<%=this.ResolveUrl("~/F_35_GrAcc/RptAccRecPayment.aspx?Type=IssueVsCollect")%>">Issue Vs. Collection</a></li>
                                <li><span>
                                    <img class="menuImg" src="Image/ACC.png" /></span><a href="<%=this.ResolveUrl("~/F_35_GrAcc/RptYearlySalCollPur.aspx")%>">Daily Target & Acheivement</a></li>
                                <li><span>
                                    <img class="menuImg" src="Image/SO.png" /></span><a href="<%=this.ResolveUrl("~/F_35_GrAcc/RptAccRecPayment.aspx?Type=CashFlow")%>">Statement of Cash Flow</a></li>

                            </ul>--%>
                        </div>

                        <%--<div class="mainmneuWrp" id="main" role="main">



                            <script>
                                var url1 = '';
                                var url2 = '';



                                var comcod = '<%= this.GetCompCodeS()%>';
                            var a = "~/F_36_GrMgtInter/GrpSalesInformation.aspx?comcod=" + comcod
                            //if (comcod == '4') {

                            url1 = "<%=this.ResolveUrl("~/F_36_GrMgtInter/GrpSalesInformation.aspx?comcod=")%>";
                            
                                //}
                           <%--else if (comcod == '7') {

                                url1 = "<%=this.ResolveUrl("~/DeafultMenu.aspx?Type=8000")%>";
                                url2 = "<%=this.ResolveUrl("~/DeafultMenu.aspx?Type=4105")%>";
                           }
                           else if (comcod == '9') {

                               url1 = "<%=this.ResolveUrl("~/DeafultMenu.aspx?Type=9000")%>";
                               url2 = "";
                           }
                            else {
                                url1 = "<%=this.ResolveUrl("~/DeafultMenu.aspx?Type=5000")%>";
                                url2 = "<%=this.ResolveUrl("~/DeafultMenu.aspx?Type=4105")%>";

                            }--%>

                        <%--    $('#main').append("<ul class=dashCir><li><a href='" + url1 + "'><span>" +
                                        "<img src='Image/S2.png' /></span>Sales</a></li>"
                                        //+"<li><a href='" + url1 + "'>Management Interface</a></li>"
                                         + "<li><a href='" + url1 + "'>Interface</a></li>"
                                        + "<li><a href='" + url2 + "'>Flow</a></li>"
                                        + "<li><a href='<%=this.ResolveUrl("~/DeafultMenu.aspx?Type=4110")%>'>Settings</a></li></ul>");
                            //+ "<li><a href='<%=this.ResolveUrl("~/Image/rec.png")%>'>Settings</a></li></ul>");--%>



                        <%--   </script>


                            <div class=" clearfix"></div>
                        </div>--%>

                        <div class="col-md-4">
                            <h3>DashBoard</h3>
                            <ul class="dashCir">
                                <li>
                                    <asp:LinkButton ID="btnSales" runat="server" OnClick="btnSales_Click"><span>
                                    <img class="" src="Image/S2.png" /></span>Sales</asp:LinkButton></li>
                                <li>
                                    <asp:LinkButton ID="btnPur" runat="server" OnClick="btnPur_Click"><span>
                                    <img class="" src="Image/P.png" /></span>Purchase</asp:LinkButton></li>
                                <li>
                                    <asp:LinkButton ID="btnPro" runat="server" OnClick="btnPro_Click"><span>
                                    <img class="" src="Image/PRO.png" /></span>Production</asp:LinkButton></li>
                                <li>
                                    <asp:LinkButton ID="btnAcc" runat="server" OnClick="btnAcc_Click"><span>
                                    <img class="" src="Image/ACC2.png" /></span>Accounts</asp:LinkButton></li>
                                <li></li>
                                <li>
                                    <asp:LinkButton ID="btnOver" runat="server" OnClick="btnOver_Click"><span>
                                    <img class="" src="Image/15.png" /></span>Overall</asp:LinkButton></li>


                            </ul>
                            <div class="clearfix"></div>
                        </div>
                        <div class="col-md-4">
                            <%-- <h3>Special Reports</h3>
                            <ul>
                                <li><span>
                                    <img class="menuImg" src="Image/FLOW.png" /></span><a href="<%=this.ResolveUrl("~/F_36_GrMgtInter/RptGrpMisDailyActiviteis.aspx")%>">Operation Monitoring</a></li>
                             
                                <li><span>
                                    <img class="menuImg" src="Image/SE.png" /></span><a href="<%=this.ResolveUrl("~/F_36_GrMgtInter/RptGrpMisDailyActiviteis.aspx")%>">Management Interface</a></li>
                                <li style="height: 280px; border:none;"></li>

                                
                            </ul>--%>
                        </div>
                    </div>

                    <div class="clearfix"></div>
                </asp:Panel>

                <asp:Panel runat="server" ID="pnlAcc" Visible="false">
                    <img src="MFG_ACC.png" alt="Accounts Menu" width="1000" style="margin: 0 0 0 60px" border="0" usemap="#Map" />
                    <map name="Map" id="Map">
                        <area shape="rect" coords="447,395,522,427" href="DeafultMenu.aspx?Type=4106" target="_self" alt="Setting" />
                        <area shape="rect" coords="572,396,646,426" href="RptDefault.aspx" target="_self" alt="Reports" />
                        <area shape="rect" coords="651,396,725,426" href="#" target="_self" alt="Others" />
                        <area shape="rect" coords="797,396,871,426" href="F_15_Acc/AccMonthlyBgd.aspx" target="_self" alt="Budget " />
                        <area shape="rect" coords="876,394,950,424" href="F_01_BPlan/YearlyPlanningBudget.aspx?Type=Yearly" target="_self" alt="ABP" />
                        <area shape="rect" coords="795,348,950,380" href="F_15_Acc/AccSalesJournal.aspx" target="_self" alt="Sales Update" />
                        <area shape="rect" coords="794,306,949,338" href="F_15_Acc/AccChalanTransfer.aspx" target="_self" alt="Transfer up date" />
                        <area shape="rect" coords="795,265,951,299" href="F_15_Acc/AccProductionJV.aspx" target="_self" alt="Production Update" />
                        <area shape="rect" coords="794,226,950,260" href="F_15_Acc/AccIsuUpdate.aspx" target="_self" alt="Issue up-date" />
                        <area shape="rect" coords="572,307,718,338" href="F_15_Acc/AccPurchaseFor.aspx" target="_self" alt="Import Journal" />
                        <area shape="rect" coords="572,267,718,298" href="F_15_Acc/AccPurchase.aspx" target="_self" alt="Purchase Journal" />
                        <area shape="rect" coords="571,228,717,259" href="F_15_Acc/GeneralAccounts.aspx?tcode=99&tname=Journal Voucher&Mod=Accounts" target="_self" alt="General Journal" />
                        <area shape="rect" coords="366,306,518,337" href="F_15_Acc/AccSales.aspx" target="_self" alt="Collection Up-date" />
                        <area shape="rect" coords="367,266,519,297" href="F_23_SaM/CustOthMoneyReceipt.aspx" target="_self" alt="Sales Collection" />
                        <area shape="rect" coords="369,228,521,259" href="F_15_Acc/GeneralAccounts.aspx?tcode=99&tname=Deposit Voucher&Mod=Accounts" target="_self" alt="Genereal deposit" />
                        <area shape="rect" coords="218,396,383,427" href="F_15_Acc/AccBankRecon.aspx?Type=Acc" target="_self" alt="Bank Recondition" />
                        <area shape="rect" coords="128,349,290,380" href="F_15_Acc/AccInterComVoucher.aspx" target="_self" alt="Inter company" />
                        <area shape="rect" coords="129,306,291,337" href="F_15_Acc/AccPayment.aspx?tcode=99&tname=Payment Voucher&Type=Acc" target="_self" alt="Post Dated Payment" />
                        <area shape="rect" coords="128,226,290,257" href="F_15_Acc/GeneralAccounts.aspx?tcode=99&tname=Payment Voucher&Mod=Accounts" target="_self" alt="General Payment" />
                        <area shape="rect" coords="128,266,290,297" href="F_15_Acc/SuplierPayment.aspx?tcode=99&tname=Payment Voucher&Mod=Accounts" target="_self" alt="Account Payble" />
                        <area shape="rect" coords="128,101,297,140" href="F_15_Acc/AccCodeBook.aspx?InputType=Accounts" target="_self" />
                        <area shape="rect" coords="718,103,878,139" href="F_15_Acc/AccOpening.aspx" target="_self" />
                    </map>
                    <div class="clearfix"></div>
                </asp:Panel>
                <asp:Panel runat="server" ID="PanelHR" Visible="false">
                    <div class="row flowMenu">
                        <div class="col-md-1"></div>
                        <div class="col-md-3">
                            <h3 style="padding: 0 0px; text-align: center"><span class="pull-left"></span>Modules</h3>
                            <ul>
                                <li><span>
                                    <img class="menuImg" src="Image/rec.png" /></span><a href="<%=this.ResolveUrl("~/StepofOperation.aspx")%>">Recruitment</a></li>
                                <li><span>
                                    <img class="menuImg" src="Image/app.png" /></span><a href="<%=this.ResolveUrl("~/StepofOperation.aspx")%>">Appointment</a></li>
                                <li><span>
                                    <img class="menuImg" src="Image/att.png" /></span><a href="<%=this.ResolveUrl("~/StepofOperation.aspx")%>">Attendance</a></li>
                                <li><span>
                                    <img class="menuImg" src="Image/lea.png" /></span><a href="<%=this.ResolveUrl("~/StepofOperation.aspx")%>">Leave</a></li>
                                <li><span>
                                    <img class="menuImg" src="Image/loan.png" /></span><a href="<%=this.ResolveUrl("~/StepofOperation.aspx")%>">Loan</a></li>
                                <li><span>
                                    <img class="menuImg" src="Image/allo.png" /></span><a href="<%=this.ResolveUrl("~/StepofOperation.aspx")%>">Allowance</a></li>
                                <li><span>
                                    <img class="menuImg" src="Image/trns.png" /></span><a href="<%=this.ResolveUrl("~/StepofOperation.aspx")%>">Transfer</a></li>
                                <li><span>
                                    <img class="menuImg" src="Image/reg.png" /></span><a href="<%=this.ResolveUrl("~/")%>">Separation</a></li>

                                <li><span>
                                    <img class="menuImg" src="Image/pay.png" /></span><a href="<%=this.ResolveUrl("~/StepofOperation.aspx")%>">Payroll</a></li>
                                <li><span>
                                    <img class="menuImg" src="Image/pf.png" /></span><a href="<%=this.ResolveUrl("~/StepofOperation.aspx")%>">P.F Account</a></li>
                                <li><span>
                                    <img class="menuImg" src="Image/acr.png" /></span><a href="<%=this.ResolveUrl("~/StepofOperation.aspx")%>">ACR</a></li>
                                <li><span>
                                    <img class="menuImg" src="Image/inc.png" /></span><a href="<%=this.ResolveUrl("~/StepofOperation.aspx")%>">Increment</a></li>
                                <li><span>
                                    <img class="menuImg" src="Image/mgmt.png" /></span><a href="<%=this.ResolveUrl("~/StepofOperation.aspx")%>">Management</a></li>




                            </ul>

                        </div>
                        <div class="col-md-4">
                            <h3>DashBoard</h3>
                            <ul class="dashCir">
                                <li><a href="<%=this.ResolveUrl("~/F_81_Hrm/F_97_MIS/RptMgtInterface.aspx")%>"><span>
                                    <img class="menuImg" src="Image/326.jpg" /></span>Summary</a></li>
                                <li><a href="<%=this.ResolveUrl("~/F_81_Hrm/F_83_Att/RptEmpDailyAttendance.aspx?Type=DailyAtten")%>"><span>
                                    <img class="menuImg" src="Image/326.jpg" /></span>Attendance</a></li>
                                <li><a href="<%=this.ResolveUrl("~/F_81_Hrm/F_82_App/RptEmpInformation.aspx?Type=Services")%>"><span>
                                    <img class="menuImg" src="Image/326.jpg" /></span>History</a></li>
                                <li><a href="<%=this.ResolveUrl("~/F_81_Hrm/F_92_Mgt/EmpStatus02.aspx?Type=joiningRpt")%>"><span>
                                    <img class="menuImg" src="Image/326.jpg" /></span>Joining</a></li>
                                <li><a href="<%=this.ResolveUrl("~/F_81_Hrm/F_92_Mgt/EmpStatus02.aspx?Type=EmpCon")%>"><span>
                                    <img class="menuImg" src="Image/326.jpg" /></span>Provision</a></li>
                                <li><a href="<%=this.ResolveUrl("~/F_81_Hrm/F_92_Mgt/EmpStatus02.aspx?Type=SepType")%>"><span>
                                    <img class="menuImg" src="Image/326.jpg" /></span>Separation</a></li>
                                <li><a href="<%=this.ResolveUrl("~/F_81_Hrm/F_89_Pay/RpHRtPayroll.aspx?Type=Salary&Entry=Payroll")%>"><span>
                                    <img class="menuImg" src="Image/326.jpg" /></span>Salary</a></li>
                                <li><a href="<%=this.ResolveUrl("~/F_81_Hrm/F_89_Pay/RpHRtPayroll.aspx?Type=Salary&Entry=Payroll")%>"><span>
                                    <img class="menuImg" src="Image/326.jpg" /></span>ACR</a></li>
                                <li><a href="<%=this.ResolveUrl("~/F_81_Hrm/F_82_App/RptMyInterface.aspx?empid=")%>"><span>
                                    <img class="menuImg" src="Image/326.jpg" /></span>My Interface</a></li>
                            </ul>
                        </div>
                        <div class="col-md-3">
                            <h3>Action</h3>
                            <ul>
                                <li><span>
                                    <img class="menuImg" src="Image/FLOW.png" /></span><a href="<%=this.ResolveUrl("~/DeafultMenu.aspx?Type=4101")%>">Flow Chart</a></li>
                                <li><span>
                                    <img class="menuImg" src="Image/ALL.png" /></span><a href="GenPage.aspx?Type=All">All Reports</a></li>
                                <li><span>
                                    <img class="menuImg" src="Image/SE.png" /></span><a href="<%=this.ResolveUrl("~/DeafultMenu.aspx?Type=4110")%>">Settings</a></li>

                            </ul>
                        </div>
                    </div>

                    <div class="clearfix"></div>
                </asp:Panel>


                <asp:Panel ID="pnlAdminPermission" runat="server" Visible="False">
                    <div class="lbl2SubMenu headTagh3">
                        <div class="contentPart">
                            <div class="row">
                                <div class="col-md-4"></div>
                                <div class="col-md-4 adminpermission">

                                    <ul class="nav colRight " id="Rightul">
                                        <li>
                                            <h3>Admin Permission</h3>
                                        </li>
                                        <li><a href="F_33_Mgt/UserLoginfrm.aspx">01. User Permission</a></li>
                                        <li><a href="F_11_Pro/ProjectLink.aspx">02. Project Permission</a></li>
                                        <li><a href="F_33_Mgt/AccUserCash.aspx">03. Cash &amp; Bank Permission</a></li>
                                        <li><a href="F_33_Mgt/Tranlimitdate.aspx">04. Transaction Limit Day</a></li>
                                        <li><a href="F_33_Mgt/UserImage.aspx">05. User Image Upload</a></li>
                                        <li><a href="F_33_Mgt/RptUserLogDetails.aspx">06. Entry, Edit &amp; Cancellation Record</a></li>
                                        <li><a href="F_33_Mgt/RptUserLogStatus.aspx">07. User Log Information</a></li>
                                        <li><a href="F_33_Mgt/DataBackup.aspx">08. Auto Data Backup</a></li>
                                        <li><a href="F_15_Acc/AccCodeBook.aspx?InputType=Accounts">09. Accounts Code Book</a></li>
                                        <li><a href="F_15_Acc/AccSubCodeBook.aspx?InputType=ResCodePrint">10. Details Code Book</a></li>
                                        <li><a href="F_23_SaM/SalesCodeBook.aspx?Type=Sales">11. Sales Information</a></li>
                                        <li><a href="F_23_SaM/SalesCodeBook.aspx?Type=Procurement">12. Purchase Information</a></li>
                                        <li><a href="F_23_SaM/SalesCodeBook.aspx?Type=Gen">13. General Information</a></li>
                                        <li><a href="F_15_Acc/AccOpening.aspx">14. Accounts Opening</a></li>
                                    </ul>


                                </div>
                                <div class="col-md-4"></div>
                            </div>
                        </div>

                    </div>
                </asp:Panel>


                <asp:Panel runat="server" Visible="false" ID="pnlkMIS">
                    <style>
                        .companysummary {
                            height: 40px;
                            left: 295px;
                            position: absolute;
                            text-align: center !important;
                            top: 90px;
                            width: 40px;
                        }

                        .na_Entry {
                            height: 40px;
                            left: 195px;
                            position: absolute;
                            text-align: center !important;
                            top: 90px;
                            width: 40px;
                        }

                        .na_MyInterface {
                            height: 40px;
                            left: 395px;
                            position: absolute;
                            text-align: center;
                            top: 90px;
                            width: 40px;
                        }

                        .Other {
                            height: 40px;
                            left: 495px;
                            position: absolute;
                            text-align: center;
                            top: 90px;
                            width: 40px;
                        }

                        /*.Other {
                            height: 40px;
                            left: 595px;
                            position: absolute;
                            text-align: center;
                            top: 90px;
                            width: 40px;
                        }*/

                        .na_Sales {
                            height: 40px;
                            left: 195px;
                            position: absolute;
                            text-align: center;
                            top: 200px;
                            width: 40px;
                        }

                        .na_General {
                            height: 40px;
                            left: 295px;
                            position: absolute;
                            text-align: center;
                            top: 200px;
                            width: 40px;
                        }

                        .CR {
                            height: 40px;
                            left: 395px;
                            position: absolute;
                            text-align: center;
                            top: 200px;
                            width: 40px;
                        }

                        .Legal {
                            height: 40px;
                            left: 495px;
                            position: absolute;
                            text-align: center;
                            top: 200px;
                            width: 40px;
                        }

                        .na_JobSetup {
                            height: 40px;
                            left: 595px;
                            position: absolute;
                            text-align: center;
                            top: 200px;
                            width: 40px;
                        }

                        .deshboard {
                            width: 70px;
                        }

                            .deshboard a img {
                                display: table;
                                margin: 0 auto;
                            }

                            .deshboard a.span {
                                display: block;
                                text-align: center;
                            }

                            .deshboard span.inttext {
                                color: #000;
                                display: block;
                                text-align: center;
                            }

                        .dashCir {
                            border: 1px solid #85e11e;
                            border-radius: 50%;
                            display: block;
                            height: 45px;
                            margin: 0 auto;
                            padding: 2px;
                            width: 45px;
                        }
                    </style>

                    <div class="row ">

                        <div class="col-md-3 mfgacc ">
                            <ul class="sideMenu ">

                                <li>
                                    <img src="Image/int11.png" />
                                    <span style="padding: 0px; margin-left: 12px;">Interface</span></li>
                                <br>
                                <br>

                                <li><a href="F_15_Acc/RptAccDTransaction.aspx?Type=Accounts&amp;TrMod=DTran">Status Report</a></li>

                                <li><a href='<%=this.ResolveUrl("~/GenPage.aspx?Type=09")%>'>Monthly Report </a></li>
                                <li><a href="DeafultMenu.aspx?Type=4110">More Reports</a></li>

                                <li><a href="#">Login By:</a></li>
                                <li><a href="#">Computer:</a></li>
                            </ul>

                        </div>

                        <div class="col-md-9">


                            <div class="row menu10">
                                <div class="companysummary  deshboard">
                                    <a href="<%=this.ResolveUrl("~/F_39_MyPage/RptDeptEvaSheet.aspx?Type=DeptTarVAch")%>" target="_self">
                                        <img width="40" class="dashCir" height="40" alt="comlogo" src="Image/326.jpg">
                                        <span class="inttext">Summary</span>
                                    </a>
                                    &nbsp;&nbsp;&nbsp;&nbsp;
                                </div>


                                <div class="na_MyInterface  deshboard">
                                    <a href="<%=this.ResolveUrl("~/F_81_Hrm/F_82_App/RptMyInterface.aspx?empid=")%>" target="_self">
                                        <img width="40" class="dashCir" height="40" alt="comlogo" src="Image/326.jpg">
                                        <span class="inttext">My Interface</span>
                                    </a>
                                    &nbsp;&nbsp;&nbsp;&nbsp;
                                </div>

                                <div>
                                    <div class="na_Sales deshboard">
                                        <a href="<%=this.ResolveUrl("~/F_47_Kpi/RptEmpEvaSheet.aspx?Type=Mgt")%>" target="_self">
                                            <img width="40" class="dashCir" height="40" alt="comlogo" src="Image/326.jpg">
                                            <span class="inttext">Sales</span>
                                        </a>
                                        &nbsp;&nbsp;&nbsp;&nbsp;
                                    </div>


                                    <div class="Other deshboard">
                                        <a href="<%=this.ResolveUrl("~/")%>" target="_self">
                                            <img width="40" class="dashCir" height="40" alt="comlogo" src="Image/326.jpg">
                                            <span class="inttext">Others</span>
                                        </a>
                                    </div>


                                    <div class="na_General deshboard">
                                        <a href="<%=this.ResolveUrl("~/F_39_MyPage/RptEmpEvaSheetGen.aspx?Type=Mgt")%>" target="_self">
                                            <img width="40" class="dashCir" height="40" alt="comlogo" src="Image/326.jpg">
                                            <span class="inttext">General</span>
                                        </a>
                                        &nbsp;&nbsp;&nbsp;&nbsp;
                                    </div>

                                    <%--  <div class="na_Individual deshboard">
                                        <a href="<%=this.ResolveUrl("~/F_39_MyPage/EmpKpiEntry04All.aspx?Type=Mgt")%>" target="_self">
                                            <img width="40" class="dashCir" height="40" alt="comlogo" src="Image/326.jpg">
                                            <span class="inttext">Entry</span>
                                        </a>

                                        &nbsp;&nbsp;&nbsp;&nbsp;
                                    </div>--%>


                                    <div class="na_MyInterface  deshboard">
                                        <a href="<%=this.ResolveUrl("~/F_81_Hrm/F_82_App/RptMyInterface.aspx?empid=")%>" target="_self">
                                            <img width="40" class="dashCir" height="40" alt="comlogo" src="Image/326.jpg">
                                            <span class="inttext">My Interface</span>
                                        </a>
                                        &nbsp;&nbsp;&nbsp;&nbsp;
                                    </div>





                                    <%--  <div class="na_JobList deshboard">
                                        <a href="<%=this.ResolveUrl("~/F_34_Mgt/ActivitiesCode.aspx?Type=DeptList")%>" target="_self">
                                            <img width="40" class="dashCir" height="40" alt="comlogo" src="Image/326.jpg">
                                            <span class="inttext">Job List</span>
                                        </a>
                                    </div>--%>

                                    <%-- <div class="na_JobSetup deshboard">
                                        <a href="<%=this.ResolveUrl("~/F_34_Mgt/DeptWiseEmpList.aspx")%>" target="_self">
                                            <img width="40" class="dashCir" height="40" alt="comlogo" src="Image/326.jpg">
                                            <span class="inttext">Job Set-up</span>
                                        </a>
                                    </div>--%>



                                    <div class="CR deshboard">
                                        <a href="<%=this.ResolveUrl("~/")%>" target="_self">
                                            <img width="40" class="dashCir" height="40" alt="comlogo" src="Image/326.jpg">
                                            <span class="inttext">CR</span>
                                        </a>
                                        &nbsp;&nbsp;&nbsp;&nbsp;
                                    </div>

                                </div>
                            </div>




                            <div class="Legal deshboard">
                                <a href="<%=this.ResolveUrl("~/")%>" target="_self">
                                    <img width="40" class="dashCir" height="40" alt="comlogo" src="Image/326.jpg">
                                    <span class="inttext">Legal</span>
                                </a>
                                &nbsp;&nbsp;&nbsp;&nbsp;
                            </div>



                            <%--<div class="na_WeightSetup deshboard">
                                        <a href="<%=this.ResolveUrl("~/F_21_Kpi/EmpStdKpi.aspx")%>" target="_self">
                                            <img width="40" class="dashCir" height="40" alt="comlogo" src="Image/326.jpg">
                                            <span class="inttext">Weight Set-up</span>
                                        </a>
                                    </div>--%>





                            <div class="na_Entry deshboard">
                                <a href="<%=this.ResolveUrl("~/F_39_MyPage/EmpKpiEntry04All.aspx?Type=Mgt")%>" target="_self">
                                    <img width="40" class="dashCir" height="40" alt="comlogo" src="Image/326.jpg">
                                    <span class="inttext">Entry</span>
                                </a>
                                &nbsp;&nbsp;&nbsp;&nbsp;
                            </div>


                            <%--    <div class="Other deshboard">
                                <a href="<%=this.ResolveUrl("~/")%>" target="_self">
                                    <img width="40" class="dashCir" height="40" alt="comlogo" src="Image/326.jpg">
                                    <span class="inttext">Others</span>
                                </a>
                            </div>--%>
                        </div>





                        <div class="row">

                            <div class="col-md-12 relatedItems" style="width: 80%; float: right;">
                                <h3>Related Items</h3>
                                <ul class="nav-pills">
                                    <%--  <li><a href="<%=this.ResolveUrl("~/F_34_Mgt/ActivitiesCode.aspx?Type=DeptList")%>"><span class=""></span>Work List </a></li>
                                    <li><a href="<%=this.ResolveUrl("~/F_34_Mgt/EntryDeptLink.aspx")%>"><span class=""></span>Dep. Link </a></li>--%>
                                    <li><a href="<%=this.ResolveUrl("~/F_64_Mgt/DeptWiseEmpList.aspx")%>"><span class=""></span>Setup(General)</a></li>
                                    <li><a href="<%=this.ResolveUrl("~/F_47_Kpi/EmpStdKpi.aspx")%>"><span class=""></span>Setup(Sales) </a></li>
                                    <%--  <li><a href="<%=this.ResolveUrl("~/F_39_MyPage/EmpKpiEntry04All.aspx?Type=Mgt")%>"><span class=""></span>Entry </a></li>--%>
                                </ul>

                            </div>
                        </div>

                        <div class="clearfix"></div>
                    </div>


                    <div class="clearfix"></div>
                </asp:Panel>

                <div class="row">
                    <asp:Panel ID="pnlflowchart" runat="server" CssClass="pnlflowchart" Visible="false">

                        <style>
                            * {
                                margin: 0;
                                padding: 0;
                            }

                            .pnlflowchart {
                                overflow: hidden;
                                background-image: url(Image/bg.PNG) !important;
                            }

                            #na_wrawpr {
                                width: 1102px;
                                height: auto;
                                margin: 0 auto;
                                position: relative;
                                background-image: url(image/bg.PNG);
                                /*background-color: #F7F7F7;
                            background-image: linear-gradient(90deg, rgba(255,255,255,.07) 50%, transparent 50%),
                            linear-gradient(90deg, rgba(255,255,255,.13) 50%, transparent 50%),
                            linear-gradient(90deg, transparent 50%, rgba(255,255,255,.17) 50%),
                            linear-gradient(90deg, transparent 50%, rgba(255,255,255,.19) 50%);
                            background-size: 13px, 29px, 37px, 53px;*/
                            }

                            .na_first {
                                width: 170px;
                                float: left;
                            }

                            #na_wrawpr ul {
                                margin: 0;
                                padding: 0;
                            }

                                #na_wrawpr ul li {
                                    list-style: none;
                                    margin: 1px 0;
                                    text-align: center;
                                    height: 30px;
                                }

                            .na_first ul li h5 {
                                background: #f0fee6 none repeat scroll 0 0 !important;
                                border: 1px solid #699f44;
                                box-shadow: 0 0 4px 2px #bec9b6 inset;
                                font-family: "ar_cenaregular";
                                font-size: 17px;
                                font-weight: normal;
                                line-height: 28px;
                                text-align: center;
                                vertical-align: middle;
                                margin: 0;
                                color: #000;
                                padding: 0;
                            }

                            .na_first ul li a.MMenu {
                                background: #f0fee6 none repeat scroll 0 0 !important;
                                border: 1px solid #699f44;
                                box-shadow: 0 0 4px 2px #bec9b6 inset;
                                font-family: "ar_cenaregular";
                                font-size: 17px;
                                font-weight: normal;
                                line-height: 28px;
                                text-align: center;
                                vertical-align: middle;
                                margin: 0;
                                color: #000;
                                padding: 0;
                            }

                            .na_first ul li h4 a {
                                background: #f0fee6 none repeat scroll 0 0 !important;
                                border: 1px solid #699f44;
                                box-shadow: 0 0 4px 2px #bec9b6 inset;
                                font-family: Cambria;
                                font-size: 14px;
                                font-weight: bold;
                                line-height: 28px;
                                text-align: center;
                                vertical-align: middle;
                                margin: 0;
                                color: #000;
                                padding: 0;
                            }

                            .na_first ul li a {
                                background: rgba(0, 0, 0, 0) linear-gradient(to bottom, #ffffff 0%, #fbfbfb 49%, #d2d2d2 98%, #cce0f2 100%) repeat scroll 0 0;
                                font-family: "ar_cenaregular";
                                font-size: 14px;
                                font-weight: normal;
                                line-height: 28px;
                                display: block;
                                color: #000;
                                text-align: center;
                                vertical-align: middle;
                                border: 1px solid #999;
                                margin: 0;
                                padding: 0;
                                text-decoration: none;
                            }

                            .na_first2 {
                                width: 63px;
                                float: left;
                            }

                                .na_first2 ul li span {
                                    height: 30px;
                                    font-size: 20px;
                                    color: #903;
                                }

                                .na_first2 ul li i {
                                    height: 30px;
                                    font-size: 20px;
                                    color: #903;
                                }

                            .na_first ul li span {
                                color: #008000;
                                font-size: 15px;
                                height: 30px;
                            }



                            .na_first ul li i {
                                height: 30px;
                                font-size: 20px;
                                color: #903;
                            }




                            .right_dr {
                                margin: 7px 0 0 22px;
                            }

                            .down_dr {
                                margin-top: 12px;
                                color: green !important;
                            }

                            .left_dr {
                                margin: 7px 22px 0 0;
                                color: green;
                            }

                            .left_dr1 {
                                margin: 7px 10px 0 0;
                                color: green !important;
                            }

                            .right2dr {
                                margin-left: 30px;
                            }


                            .na_comlogo {
                                height: 28px;
                                left: 978px;
                                position: absolute;
                                top: 4px;
                                width: 171px;
                            }

                            [class^="flaticon-"]::before, [class*=" flaticon-"]::before, [class^="flaticon-"]::after, [class*=" flaticon-"]::after {
                                width: 20px !important;
                                font-size: 20px !important;
                                padding: 0 10px 0 3px !important;
                                margin: 0;
                            }


                            .na_first ul a {
                                text-align: left !important;
                            }

                            .na_first ul h5 {
                                text-align: left !important;
                            }



                            .na_linqC2h {
                                background: green none repeat scroll 0 0;
                                height: 2px;
                                left: 317px;
                                position: absolute;
                                top: 135px;
                                width: 702px;
                            }

                            .na_Importlogo {
                                height: 40px;
                                left: 720px;
                                position: absolute;
                                text-align: center;
                                top: 0px;
                                width: 40px;
                            }

                            .na_Settinglogo {
                                height: 43px;
                                left: 3px;
                                position: absolute;
                                top: 0px;
                                width: 45px;
                            }

                            .na_Saleslogo {
                                height: 43px;
                                left: 262px;
                                position: absolute;
                                top: 0px;
                                width: 45px;
                            }


                            .na_purlogo {
                                height: 43px;
                                left: 327px;
                                position: absolute;
                                top: 0px;
                                width: 45px;
                            }

                            .na_Production {
                                height: 40px;
                                left: 790px;
                                position: absolute;
                                text-align: center;
                                top: 0px;
                                width: 40px;
                            }

                            .na_Aclogo {
                                height: 40px;
                                left: 863px;
                                position: absolute;
                                top: 0px;
                                width: 40px;
                            }

                            .na_overalllogo {
                                height: 4px;
                                left: 935px;
                                position: absolute;
                                text-align: center;
                                top: 0px;
                                width: 40px;
                            }

                            .na_overalllogo2 {
                                height: 4px;
                                left: 1048px;
                                position: absolute;
                                text-align: center;
                                top: 0px;
                                width: 54px;
                            }

                            .inttext {
                                color: #000;
                                display: block;
                                left: 22px;
                                top: 1px !important;
                                margin-left: -8px;
                                text-align: center;
                            }


                            #rbtnList1 input[type="checkbox"], #rbtnList1 input[type="radio"] {
                                z-index: -1;
                            }

                            .h4, .h5, .h6, h4, h5, h6 {
                                margin-bottom: 10px;
                                margin-top: 2px !important;
                            }
                        </style>

                        <div id="na_wrawpr">

                            <div class="na_Settinglogo">
                                <a href="<%=this.ResolveUrl("~/DeafultMenu.aspx?Type=4106")%>">
                                    <img src="Image/ovinterface.png" height="30" width="30" alt="comlogo" />
                                    <span class="inttext">Settings</span>

                                </a>
                            </div>


                            <div class="na_Importlogo">
                                <a href="<%=this.ResolveUrl("~/DeafultMenu.aspx?Type=4106")%>">
                                    <img src="Image/int1.png" height="40" width="40" alt="comlogo" />
                                    <span class="inttext">Import</span>

                                </a>
                            </div>


                            <div class="na_Saleslogo">
                                <a href="<%=this.ResolveUrl("~/F_23_SaM/SalesInformation.aspx")%>">
                                    <img src="Image/int1.png" height="40" width="40" alt="comlogo" />
                                    <span class="inttext">Sales</span>

                                </a>
                            </div>

                            <div class="na_purlogo">
                                <a href="<%=this.ResolveUrl("~/F_11_Pro/PurInformation.aspx")%>">
                                    <img src="Image/int1.png" height="40" width="40" alt="comlogo" />
                                    <span class="inttext">Purchase</span>

                                </a>
                            </div>

                            <div class="na_Production">
                                <a href="<%=this.ResolveUrl("~/F_13_ProdMon/ProductionInfo.aspx")%>">
                                    <img src="Image/int1.png" height="40" width="40" alt="comlogo" />
                                    <span class="inttext">Production</span>
                                </a>
                            </div>

                            <div class="na_Aclogo">

                                <a href="<%=this.ResolveUrl("~/F_15_Acc/AccDashBoard.aspx")%>">

                                    <img src="Image/int1.png" height="40" width="40" alt="comlogo" />
                                    <span class="inttext">Accounts</span>
                                </a>
                            </div>

                            <div class="na_overalllogo">
                                <a href="<%=this.ResolveUrl("~/F_33_Mgt/DashBoardAll.aspx")%>">

                                    <img src="Image/int1.png" height="40" width="40" alt="comlogo" />
                                    <span class="inttext">Overall</span>
                                </a>
                            </div>

                            <div class="na_overalllogo2">
                                <a href="<%=this.ResolveUrl("~/GenPage.aspx?Type=All")%>">

                                    <img src="Image/ovinterface.png" height="30" width="30" alt="comlogo" />
                                    <span class="inttext">All Reports </span>
                                </a>
                            </div>


                            <div class="na_linqC6a"></div>
                            <div class="na_linqC6b"></div>

                            <div class="na_linqC6c"></div>
                            <div class="na_linqC6d"></div>

                            <div class="na_linqC6e"></div>

                            <div class="na_linqC6f"></div>
                            <div class="na_linqC6g"></div>

                            <div class="na_linqC8a"></div>
                            <div class="na_linqC8b"></div>

                            <div class="na_linqC8c"></div>
                            <div class="na_linqC8d"></div>
                            <%-- <div class="na_linqC8e"></div>--%>

                            <div class="na_linqC3a"></div>
                            <div class="na_linqC3b"></div>

                            <div class="na_linqC3c"></div>
                            <div class="na_linqC3d"></div>
                            <div class="na_linqC3e"></div>
                            <div class="na_linqC3f"></div>
                            <div class="na_linqC3g"></div>

                            <div class="na_linqC1a"></div>
                            <div class="na_linqC1b"></div>

                            <div class="na_linqC2e"></div>
                            <div class="na_linqC2f"></div>
                            <div class="na_linqC2g"></div>

                            <div class="na_linqC2h"></div>
                            <div class="na_linqC2i"></div>

                            <%--<div class="na_linqC1a"></div>

                 
                    <div class="na_linqC8k"></div>

                    <div class="na_comlogo">
                        <img src="housing3.PNG"  height="28" width="171" alt="comlogo"/>
                    </div>--%>


                            <div class="na_first">
                                <ul>
                                    <li></li>
                                    <li></li>

                                    <li></li>

                                    <li></li>
                                    <li></li>
                                    <li></li>
                                    <li></li>
                                    <li>
                                        <h5><span class="glyph-icon flaticon-budget nfal"></span>Import Management</h5>
                                    </li>
                                    <li><span class="fa down_dr fa-long-arrow-down"></span></li>
                                    <%-- <li><a href="<%=this.ResolveUrl("~/F_07_Inv/PurMktSurvey.aspx?Type=MktSurvey")%>"><span class="glyph-icon flaticon-tray30 nfal"></span>CS Preparation</a></li>--%>

                                    <li><a href="<%=this.ResolveUrl("~/F_09_LCM/LCInformation.aspx?tname=order&tid=lc")%>"><span class="glyph-icon flaticon-graph4 nfal"></span>L/C Open & Monitor</a></li>
                                    <li><a href="<%=this.ResolveUrl("~/F_15_Acc/GeneralAccounts.aspx?tcode=99&tname=Payment Voucher&Mod=Accounts")%>"><span class="glyph-icon flaticon-soccer68 nfal"></span>Payments</a></li>

                                    <li><a href="<%=this.ResolveUrl("~/F_09_LCM/LCInformation.aspx?tname=receive&tid=l")%>"><span class="glyph-icon flaticon-discount5 nfal"></span>Materials Receive (L/C)</a></li>
                                     <li><a href="<%=this.ResolveUrl("~/F_15_Acc/AccPurchaseFor.aspx")%>"><span class="glyph-icon flaticon-coins36 nfal"></span>Update- L/C Cost</a></li>
                                      <li><a href="<%=this.ResolveUrl("~/F_23_SaM/RptSalSummery.aspx?Type=LcReceive")%>"><span class="glyph-icon flaticon-calculating11 nfal"></span>L/C Received </a></li>
                                    <li><a href="<%=this.ResolveUrl("~/F_09_LCM/RptLCStatus.aspx?Type=LCCosting")%>"><span class="glyph-icon flaticon-data45 nfal"></span>L/C Costing</a></li>
                                    <%--<li><a href="<%=this.ResolveUrl("~/F_09_LCM/LCInformation.aspx?tname=costing&tid=cst")%>"><span class="glyph-icon flaticon-business73 nfal"></span>L/C Costing (Auto)</a></li>--%>
                                   
                                    <%-- <li><a href="<%=this.ResolveUrl("~/F_07_Inv/PBMatIssueSingle.aspx")%>"><span class="glyph-icon flaticon-basket41 nfal"></span>Cost Update</a></li>--%>
                                    <li></li>
                                    <%-- <li><span class="fa down_dr fa-long-arrow-down"></span></li>--%>
                                    <li></li>
                                    <li></li>
                                    <%-- <li>
                                        <h5><span class="glyph-icon flaticon-chart44 nfal"></span>Digital CRM</h5>
                                    </li>--%>
                                    <li><a href="<%=this.ResolveUrl("~/F_03_StdCost/MatAvailability.aspx")%>"><span class="glyph-icon flaticon-calculating11 nfal"></span>Materials Availability</a></li>
                                    <li><a href="<%=this.ResolveUrl("~/F_11_Pro/ProdReq.aspx")%>"><span class="glyph-icon flaticon-data45 nfal"></span>Production Planning</a></li>
                                  
                                </ul>
                            </div>

                            <div class="na_first2">
                                <ul>
                                    <li></li>
                                    <li></li>
                                    <li></li>
                                    <li><a href="<%=this.ResolveUrl("~/")%>"></a></li>
                                    <li><a href="<%=this.ResolveUrl("~/")%>"></a></li>
                                    <li><a href="<%=this.ResolveUrl("~/")%>"></a></li>
                                    <li><a href="<%=this.ResolveUrl("~/")%>"></a></li>
                                    <li><span class="fa fa-long-arrow-left  left_dr1 fa-fw"></span></li>
                                    <li><a href="<%=this.ResolveUrl("~/")%>"></a></li>

                                    <li><a href="<%=this.ResolveUrl("~/")%>"></a></li>
                                    <li><a href="<%=this.ResolveUrl("~/")%>"></a></li>
                                    <li><a href="<%=this.ResolveUrl("~/")%>"></a></li>
                                    <li><a href="<%=this.ResolveUrl("~/")%>"></a></li>
                                    <li><a href="<%=this.ResolveUrl("~/")%>"></a></li>
                                    <li></li>
                                    <li></li>

                                    <li><a href="<%=this.ResolveUrl("~/")%>"></a></li>
                                    <li><a href="<%=this.ResolveUrl("~/")%>"></a></li>
                                    <li><a href="<%=this.ResolveUrl("~/")%>"></a></li>
                                    <%--<li><span class="fa fa-long-arrow-left  left_dr1 fa-fw"></span></li>--%>
                                    <li><a href="<%=this.ResolveUrl("~/")%>"></a></li>
                                    <li><a href="<%=this.ResolveUrl("~/")%>"></a></li>
                                </ul>
                            </div>

                            <div class="na_first">
                                <ul>
                                    <li></li>
                                    <li></li>


                                    <li><a href="<%=this.ResolveUrl("~/F_03_StdCost/StdCostSheet.aspx?InputType=CostAnna")%>"><span class="glyph-icon flaticon-data79 nfal"></span>Standard Analysis</a></li>
                                    <li></li>

                                    <li><span class="fa down_dr fa-long-arrow-down"></span></li>

                                    <li>
                                        <h4><a href="<%=this.ResolveUrl("~/F_07_Inv/StockPosition.aspx")%>"><span class="glyph-icon flaticon-international36 nfal"></span>Materials Inventory</a></h4>
                                    </li>

                                    <%--<li>
                                        <h5 class="MMenu"></h5>
                                    </li>--%>
                                    <li><span class="fa down_dr fa-long-arrow-down"></span></li>
                                    <li>
                                        <h5><span class="glyph-icon flaticon-logistics3 nfal"></span>Procurement</h5>
                                    </li>
                                    <li><span class="fa down_dr fa-long-arrow-down"></span></li>

                                    <li><a href="<%=this.ResolveUrl("~/F_07_Inv/PurReqEntry02.aspx?InputType=FxtAstEntry&actcode=&genno=")%>"><span class="glyph-icon flaticon-payment5 nfal"></span>Requisition Status</a></li>
                                    
                                    <li><a href="<%=this.ResolveUrl("~/F_07_Inv/PurMktSurvey02.aspx?Type=Create&genno=")%>"><span class="glyph-icon flaticon-research1 nfal"></span>CS Preparation</a></li>
                                    <li><a href="<%=this.ResolveUrl("~//F_07_Inv/PurMktSurvey02.aspx?Type=Check&genno=")%>"><span class="glyph-icon flaticon-information58 nfal"></span>CS Check</a></li>

                                    <li><a href="<%=this.ResolveUrl("~/F_07_Inv/PurMktSurvey02.aspx?Type=Approved&genno=")%>"><span class="glyph-icon flaticon-credit56 nfal"></span>CS Approval</a></li>
                                    <li><a href="<%=this.ResolveUrl("~/F_07_Inv/PurAprovEntry.aspx?InputType=PurProposal&genno=")%>"><span class="glyph-icon flaticon-shopping3 nfal"></span>Order Process</a></li>
                                    <li><a href="<%=this.ResolveUrl("~/F_11_Pro/PurWrkOrderEntry.aspx?InputType=OrderEntry&genno=&actcode=")%>"><span class="glyph-icon flaticon-coins36 nfal"></span>Purchase Order</a></li>
                                    <li><a href="<%=this.ResolveUrl("~/F_07_Inv/PurMRREntry.aspx?Type=Entry&genno=")%>"><span class="glyph-icon flaticon-home183 nfal"></span>Received</a></li>

                                    <li><a href="<%=this.ResolveUrl("~/F_07_Inv/PurQCEntry.aspx?Type=Entry&genno=")%>"><span class="glyph-icon flaticon-customerservice19 nfal"></span>QC Check</a></li>
                                    <li><a href="<%=this.ResolveUrl("~/F_07_Inv/PurBillEntry.aspx?Type=BillEntry&genno=&sircode=")%>"><span class="glyph-icon flaticon-bank5 nfal"></span>Bill Confirmation</a></li>

                                    <li><a href="<%=this.ResolveUrl("~/F_15_Acc/AccPurchase.aspx")%>"><span class="glyph-icon flaticon-telephone172 nfal"></span>Update-Purchase </a></li>
                                    <li><a href="<%=this.ResolveUrl("~/F_15_Acc/SuplierPayment.aspx?tcode=99&tname=Payment Voucher&Mod=Accounts")%>"><span class="glyph-icon flaticon-payment5 nfal"></span>Supplier's payment</a></li>


                                </ul>
                            </div>

                            <div class="na_first2">
                                <ul>
                                    <li></li>
                                    <li></li>
                                    <li></li>
                                    <li><a href="<%=this.ResolveUrl("~/")%>"></a></li>
                                    <li><a href="<%=this.ResolveUrl("~/")%>"></a></li>
                                    <li><span class="fa left_dr fa-long-arrow-left"></span></li>
                                    <li><a href="<%=this.ResolveUrl("~/")%>"></a></li>
                                    <li><a href="<%=this.ResolveUrl("~/")%>"></a></li>
                                    <li><a href="<%=this.ResolveUrl("~/")%>"></a></li>
                                    <li><a href="<%=this.ResolveUrl("~/")%>"></a></li>
                                    <li><a href="<%=this.ResolveUrl("~/")%>"></a></li>
                                    <li><a href="<%=this.ResolveUrl("~/")%>"></a></li>
                                    <li><a href="<%=this.ResolveUrl("~/")%>"></a></li>
                                    <%-- <li><span class="fa right_dr fa-long-arrow-right"></span></li>--%>
                                    <li><a href="<%=this.ResolveUrl("~/")%>"></a></li>
                                    <li><a href="<%=this.ResolveUrl("~/")%>"></a></li>
                                    <li><a href="<%=this.ResolveUrl("~/")%>"></a></li>
                                    <li><a href="<%=this.ResolveUrl("~/")%>"></a></li>
                                    <li><a href="<%=this.ResolveUrl("~/")%>"></a></li>
                                    <li><a href="<%=this.ResolveUrl("~/")%>"></a></li>
                                    <li><a href="<%=this.ResolveUrl("~/")%>"></a></li>
                                    <li><a href="<%=this.ResolveUrl("~/")%>"></a></li>
                                    <li><a href="<%=this.ResolveUrl("~/")%>"></a></li>
                                    <li><a href="<%=this.ResolveUrl("~/")%>"></a></li>


                                </ul>
                            </div>

                            <div class="na_first">
                                <ul>
                                    <li>
                                        <h5><span class="glyph-icon flaticon-draft nfal"></span>Manufacturing ERP</h5>
                                    </li>
                                    <li><span class="fa down_dr fa-long-arrow-down"></span></li>
                                    <li>
                                        <h4><a href="<%=this.ResolveUrl("~/F_01_BPlan/YearlyPlanningBudget.aspx?Type=Yearly")%>"><span class="glyph-icon flaticon-data79 nfal"></span>Business Plan</a></h4>
                                        <%--F_01_BPlan/YearlyPlanningBudget.aspx?Type=Yearly--%>
                                    </li>
                                    <li><span class="fa down_dr fa-long-arrow-down"></span></li>
                                    <li><span class="fa down_dr fa-long-arrow-down"></span></li>
                                    <li>
                                        <h4><a href="<%=this.ResolveUrl("~/F_19_FGInv/StockPositionProd.aspx")%>"><span class="glyph-icon flaticon-lightbulbs13 nfal"></span>Goods Inventory</a></h4>
                                    </li>
                                    <li><span class="fa down_dr fa-long-arrow-down"></span></li>
                                    <li>
                                        <h5><span class="glyph-icon flaticon-lightbulbs13 nfal"></span>Production</h5>
                                    </li>
                                    <li><span class="fa down_dr fa-long-arrow-down"></span></li>
                                    <%-- <li><a href="<%=this.ResolveUrl("~/F_23_SaM/MonthlySalesEntry.aspx")%>"><span class="glyph-icon flaticon-money483 nfal"></span>Monthly Sales Target</a></li>--%>
                                    <li><a href="<%=this.ResolveUrl("~/F_03_StdCost/ProdBudget.aspx?Type=Entry")%>"><span class="glyph-icon flaticon-data62 nfal"></span>Production Budget</li>


                                    <li><a href="<%=this.ResolveUrl("~/F_11_Pro/ProdReq.aspx")%>"><span class="glyph-icon flaticon-report1 nfal"></span>Production Requisition</a></li>
                                    <li><a href="<%=this.ResolveUrl("~/F_07_Inv/PBMatIssueSingle.aspx")%>"><span class="glyph-icon flaticon-truck29 nfal"></span>Resources Issue</a></li>

                                    <li><a href="<%=this.ResolveUrl("~/F_13_ProdMon/ProProcessTrans.aspx")%>"><span class="glyph-icon flaticon-waves5 nfal"></span>QC</a></li>
                                    <li><a href="<%=this.ResolveUrl("~/F_13_ProdMon/ProductionPlan.aspx")%>"><span class="glyph-icon flaticon-bank5 nfal"></span>Production Entry</a></li>
                                    <li><a href="<%=this.ResolveUrl("~/F_19_FGInv/ProReceive.aspx")%>"><span class="glyph-icon flaticon-waves5 nfal"></span>Update-FGS After QC</a></li>
                                    <li><a href="<%=this.ResolveUrl("~/F_15_Acc/AccIsuUpdate.aspx")%>"><span class="glyph-icon flaticon-copy32 nfal"></span>Update- Issue</a></li>
                                    <li><a href="<%=this.ResolveUrl("~/F_15_Acc/AccProductionJV.aspx")%>"><span class="glyph-icon flaticon-data98 nfal"></span>Update- WIP</a></li>
                                    <%--  <li><span class="fa down_dr fa-long-arrow-down"></span></li>--%>
                                    <li></li>
                                    <li>
                                        <h5><span class="glyph-icon flaticon-lightbulbs13 nfal"></span>Distribution</h5>
                                    </li>
                                    <li><a href="<%=this.ResolveUrl("~/")%>"><span class="glyph-icon flaticon-contract11 nfal"></span>Requisition</a></li>
                                    <li><a href="<%=this.ResolveUrl("~/F_23_SaM/Chalan.aspx")%>"><span class="glyph-icon flaticon-waves5 nfal"></span>Product Transfer</a></li>
                                    <li><a href="<%=this.ResolveUrl("~/F_15_Acc/AccChalanTransfer.aspx")%>"><span class="glyph-icon flaticon-telephone172 nfal"></span>Update-Transfer</a></li>


                                    <li></li>
                                    <%--  <li><a href="<%=this.ResolveUrl("~/F_33_Mgt/DashBoardAll.aspx")%>"><span class="glyph-icon flaticon-report1 nfal"></span>Dashboard</a></li>
                                    <li><a href="<%=this.ResolveUrl("~/F_23_SaM/SalesInformation.aspx")%>"><span class="glyph-icon flaticon-house118 nfal"></span>Sales</a></li>
                                    <li><a href="<%=this.ResolveUrl("~/F_11_Pro/PurInformation.aspx")%>"><span class="glyph-icon flaticon-dollar178 nfal"></span>Purchase</a></li>
                                    <li><a href="<%=this.ResolveUrl("~/F_15_Acc/AccDashBoard.aspx")%>"><span class="glyph-icon flaticon-settings49 nfal"></span>Accounts</a></li>
                                    <li></li>--%>
                                </ul>
                            </div>

                            <div class="na_first2">
                                <ul>
                                    <li></li>
                                    <li></li>
                                    <li></li>
                                    <li></li>
                                    <li><a href="<%=this.ResolveUrl("~/")%>"></a></li>
                                    <li><span class="fa left_dr fa-long-arrow-left"></span></li>
                                    <li><a href="<%=this.ResolveUrl("~/")%>"></a></li>
                                    <li><a href="<%=this.ResolveUrl("~/")%>"></a></li>
                                    <li><a href="<%=this.ResolveUrl("~/")%>"></a></li>
                                    <li><a href="<%=this.ResolveUrl("~/")%>"></a></li>
                                    <li><a href="<%=this.ResolveUrl("~/")%>"></a></li>
                                    <li></li>
                                    <%--  <li><span class="fa left_dr fa-long-arrow-left"></span></li>--%>
                                    <li><a href="<%=this.ResolveUrl("~/")%>"></a></li>
                                    <%--  <li><span class="fa right_dr  right2dr fa-long-arrow-right"></span></li>--%>
                                    <li><a href="<%=this.ResolveUrl("~/")%>"></a></li>
                                    <li><a href="<%=this.ResolveUrl("~/")%>"></a></li>
                                    <li><a href="<%=this.ResolveUrl("~/")%>"></a></li>
                                    <li><a href="<%=this.ResolveUrl("~/")%>"></a></li>
                                    <li><a href="<%=this.ResolveUrl("~/")%>"></a></li>
                                    <li><a href="<%=this.ResolveUrl("~/")%>"></a></li>
                                    <%--<li><span class="fa right_dr fa-long-arrow-right"></span></li>--%>
                                    <li><span class="fa fa-long-arrow-up  left_dr1 fa-fw"></span></li>
                                    <li><a href="<%=this.ResolveUrl("~/")%>"></a></li>
                                    <li><a href="<%=this.ResolveUrl("~/")%>"></a></li>
                                    <li><a href="<%=this.ResolveUrl("~/")%>"></a></li>
                                </ul>
                            </div>



                            <div class="na_first">
                                <ul>
                                    <li></li>
                                    <li></li>
                                    <li></li>
                                    <li></li>
                                    <li><span class="fa down_dr fa-long-arrow-down"></span></li>
                                    <li>
                                        <h5><span class="glyph-icon flaticon-house118 nfal"></span>Sales Management</h5>
                                    </li>
                                    <li><span class="fa down_dr fa-long-arrow-down"></span></li>
                                    <li><a href="<%=this.ResolveUrl("~/F_23_SaM/SalesComProposal.aspx")%>"><span class="glyph-icon flaticon-domain2 nfal"></span>Commercial Proposal</a></li>
                                    <li><a href="<%=this.ResolveUrl("~/F_23_SaM/AllSaleOrderList.aspx?Type=All")%>"><span class="glyph-icon flaticon-contract11 nfal"></span>Sales Order</a></li>
                                    <li><a href="<%=this.ResolveUrl("~/F_23_SaM/SalesOrderApproval.aspx?Type=All")%>"><span class="glyph-icon flaticon-website17 nfal"></span>Sales Order Approval</a></li>
                                    <li><a href="<%=this.ResolveUrl("~/F_23_SaM/DeliveryOrder02.aspx?Type=Entry")%>"><span class="glyph-icon flaticon-register nfal"></span>DO - Sales Store </a></li>
                                    <li><a href="<%=this.ResolveUrl("~/F_23_SaM/DelOrderApproval.aspx?Type=Payment")%>"><span class="glyph-icon flaticon-register nfal"></span>Payment Confirmation</a></li>
                                    <li><a href="<%=this.ResolveUrl("~/F_23_SaM/DelOrderApproval.aspx?Type=Delivery")%>"><span class="glyph-icon flaticon-register nfal"></span>Delivery Confirmation</a></li>

                                    <li><a href="<%=this.ResolveUrl("~/F_23_SaM/InvListUpdate.aspx")%>"><span class="glyph-icon flaticon-list23 nfal"></span>Invoice / Bill</a></li>
                                    <li><a href="<%=this.ResolveUrl("~/F_23_SaM/AllCollection.aspx?Type=All")%>"><span class="glyph-icon flaticon-shoppingcart39 nfal"></span>Collection</a></li>
                                    <li><a href="<%=this.ResolveUrl("~/F_15_Acc/AccSalesJournal.aspx")%>"><span class="glyph-icon flaticon-stocks2 nfal"></span>Update - Sales </a></li>
                                    <li><a href="<%=this.ResolveUrl("~/F_15_Acc/AccSales.aspx")%>"><span class="glyph-icon flaticon-data62 nfal"></span>Update-Collection </a></li>
                                    <li></li>




                                </ul>
                            </div>

                            <div class="na_first2">
                                <ul>
                                    <li></li>
                                    <li></li>
                                    <li><a href="<%=this.ResolveUrl("~/")%>"></a></li>
                                    <li><a href="<%=this.ResolveUrl("~/")%>"></a></li>
                                    <li></li>
                                    <li></li>
                                    <li></li>
                                    <li><a href="<%=this.ResolveUrl("~/")%>"></a></li>
                                    <li><a href="<%=this.ResolveUrl("~/")%>"></a></li>
                                    <li><a href="<%=this.ResolveUrl("~/")%>"></a></li>
                                    <li><a href="<%=this.ResolveUrl("~/")%>"></a></li>
                                    <li><a href="<%=this.ResolveUrl("~/")%>"></a></li>
                                    <li><a href="<%=this.ResolveUrl("~/")%>"></a></li>
                                    <li><a href="<%=this.ResolveUrl("~/")%>"></a></li>
                                    <li><a href="<%=this.ResolveUrl("~/")%>"></a></li>
                                    <li><a href="<%=this.ResolveUrl("~/")%>"></a></li>
                                    <li><a href="<%=this.ResolveUrl("~/")%>"></a></li>
                                    <li><a href="<%=this.ResolveUrl("~/")%>"></a></li>
                                    <li><a href="<%=this.ResolveUrl("~/")%>"></a></li>
                                    <li><a href="<%=this.ResolveUrl("~/")%>"></a></li>
                                    <li><a href="<%=this.ResolveUrl("~/")%>"></a></li>
                                    <%--<li><span class="fa left_dr fa-long-arrow-left"></span></li>
                                    <li><span class="fa  right_dr fa-long-arrow-right"></span></li>
                                    <li><span class="fa  right_dr fa-long-arrow-right"></span></li>                                
                                    <li><span class="fa  right_dr fa-long-arrow-right"></span></li>                                   
                                    <li><span class="fa right_dr fa-long-arrow-right"></span></li>--%>
                                    <li><a href="<%=this.ResolveUrl("~/")%>"></a></li>


                                </ul>
                            </div>

                            <div class="na_first">
                                <ul>
                                    <li></li>
                                    <li></li>
                                    <li></li>
                                    <li></li>
                                    <li><span class="fa down_dr fa-long-arrow-down"></span></li>
                                    <li>
                                        <h5><span class="glyph-icon flaticon-settings49 nfal"></span>Finance & Accounts</h5>
                                    </li>
                                    <li><span class="fa down_dr fa-long-arrow-down"></span></li>
                                    <%--  <li><a href="<%=this.ResolveUrl("~/F_01_BPlan/YearlyPlanningBudget.aspx?Type=Yearly")%>"><span class="glyph-icon flaticon-marketing8 nfal"></span>Annual Business Plan</a></li>--%>
                                    <%--<li><a href="<%=this.ResolveUrl("~/F_15_Acc/AccMonthlyBgd.aspx")%>"><span class="glyph-icon flaticon-full22 nfal"></span>Working Budget</a></li>--%>
                                    <li><a href="<%=this.ResolveUrl("~/F_15_Acc/GeneralAccounts.aspx?tcode=99&tname=Payment Voucher&Mod=Accounts")%>"><span class="glyph-icon flaticon-discount5 nfal"></span>Payment Voucher</a></li>
                                    <li><a href="<%=this.ResolveUrl("~/F_15_Acc/GeneralAccounts.aspx?tcode=99&tname=Deposit Voucher&Mod=Accounts")%>"><span class="glyph-icon flaticon-news29 nfal"></span>Deposit Voucher</a></li>
                                    <li><a href="<%=this.ResolveUrl("~/F_15_Acc/GeneralAccounts.aspx?tcode=99&tname=Journal Voucher&Mod=Accounts")%>"><span class="glyph-icon flaticon-creditcard7 nfal"></span>Journal Voucher</a></li>
                                    <li><a href="<%=this.ResolveUrl("~/F_15_Acc/AccInterComVoucher.aspx")%>"><span class="glyph-icon flaticon-money405 nfal"></span>Inter Company Payment</a></li>
                                    <li><span class="fa down_dr fa-long-arrow-down"></span></li>
                                    <li><a href="<%=this.ResolveUrl("~/")%>"><span class="glyph-icon flaticon-magnifyingglass27 nfal"></span>Plan Vs Acheivement</a></li>
                                    <li></li>
                                    <li><a href="<%=this.ResolveUrl("~/F_27_Mis/RptBgdVsActPro.aspx?Type=Income")%>"><span class="glyph-icon flaticon-thumb32 nfal"></span>Performance </a></li>
                                    <%-- <li><a href="<%=this.ResolveUrl("~/F_23_SaM/SalesWiseVat.aspx")%>"><span class="glyph-icon flaticon-magnifyingglass27 nfal"></span>VAT Accounts (Auto)</a></li>
                                    --%>
                                    <%-- <li><a href="<%=this.ResolveUrl("~/RptDefault.aspx")%>"><span class="glyph-icon flaticon-checkmark11 nfal"></span>All Reports</a></li>
                                    <li><a href="<%=this.ResolveUrl("~/DeafultMenu.aspx?Type=4106")%>"><span class="glyph-icon flaticon-settings48 nfal"></span>Settings</a></li>--%>
                                </ul>
                            </div>

                        </div>

                    </asp:Panel>
                </div>

                <div class="row">
                    <asp:Panel ID="pnlflowchart01" runat="server" CssClass="pnlflowchart" Visible="false">

                        <style>
                            * {
                                margin: 0;
                                padding: 0;
                            }

                            .pnlflowchart {
                                overflow: hidden;
                                background-image: url(Image/bg.PNG) !important;
                            }

                            #na_wrawpr {
                                width: 1102px;
                                height: auto;
                                margin: 0 auto;
                                position: relative;
                                background-image: url(image/bg.PNG);
                                /*background-color: #F7F7F7;
                            background-image: linear-gradient(90deg, rgba(255,255,255,.07) 50%, transparent 50%),
                            linear-gradient(90deg, rgba(255,255,255,.13) 50%, transparent 50%),
                            linear-gradient(90deg, transparent 50%, rgba(255,255,255,.17) 50%),
                            linear-gradient(90deg, transparent 50%, rgba(255,255,255,.19) 50%);
                            background-size: 13px, 29px, 37px, 53px;*/
                            }

                            .na_first {
                                width: 170px;
                                float: left;
                            }

                            #na_wrawpr ul {
                                margin: 0;
                                padding: 0;
                            }

                                #na_wrawpr ul li {
                                    list-style: none;
                                    margin: 1px 0;
                                    text-align: center;
                                    height: 30px;
                                }

                            .na_first ul li h5 {
                                background: #f0fee6 none repeat scroll 0 0 !important;
                                border: 1px solid #699f44;
                                box-shadow: 0 0 4px 2px #bec9b6 inset;
                                font-family: "ar_cenaregular";
                                font-size: 17px;
                                font-weight: normal;
                                line-height: 28px;
                                text-align: center;
                                vertical-align: middle;
                                margin: 0;
                                color: #000;
                                padding: 0;
                            }

                            .na_first ul li a.MMenu {
                                background: #f0fee6 none repeat scroll 0 0 !important;
                                border: 1px solid #699f44;
                                box-shadow: 0 0 4px 2px #bec9b6 inset;
                                font-family: "ar_cenaregular";
                                font-size: 17px;
                                font-weight: normal;
                                line-height: 28px;
                                text-align: center;
                                vertical-align: middle;
                                margin: 0;
                                color: #000;
                                padding: 0;
                            }

                            .na_first ul li h4 a {
                                background: #f0fee6 none repeat scroll 0 0 !important;
                                border: 1px solid #699f44;
                                box-shadow: 0 0 4px 2px #bec9b6 inset;
                                font-family: Cambria;
                                font-size: 14px;
                                font-weight: bold;
                                line-height: 28px;
                                text-align: center;
                                vertical-align: middle;
                                margin: 0;
                                color: #000;
                                padding: 0;
                            }

                            .na_first ul li a {
                                background: rgba(0, 0, 0, 0) linear-gradient(to bottom, #ffffff 0%, #fbfbfb 49%, #d2d2d2 98%, #cce0f2 100%) repeat scroll 0 0;
                                font-family: "ar_cenaregular";
                                font-size: 14px;
                                font-weight: normal;
                                line-height: 28px;
                                display: block;
                                color: #000;
                                text-align: center;
                                vertical-align: middle;
                                border: 1px solid #999;
                                margin: 0;
                                padding: 0;
                                text-decoration: none;
                            }

                            .na_first2 {
                                width: 63px;
                                float: left;
                            }

                                .na_first2 ul li span {
                                    height: 30px;
                                    font-size: 20px;
                                    color: #903;
                                }

                                .na_first2 ul li i {
                                    height: 30px;
                                    font-size: 20px;
                                    color: #903;
                                }

                            .na_first ul li span {
                                color: #008000;
                                font-size: 15px;
                                height: 30px;
                            }



                            .na_first ul li i {
                                height: 30px;
                                font-size: 20px;
                                color: #903;
                            }




                            .right_dr {
                                margin: 7px 0 0 22px;
                            }

                            .down_dr {
                                margin-top: 12px;
                                color: green !important;
                            }

                            .left_dr {
                                margin: 7px 22px 0 0;
                                color: green;
                            }

                            .left_dr1 {
                                margin: 7px 10px 0 0;
                                color: green !important;
                            }

                            .right2dr {
                                margin-left: 30px;
                            }


                            .na_comlogo {
                                height: 28px;
                                left: 978px;
                                position: absolute;
                                top: 4px;
                                width: 171px;
                            }

                            [class^="flaticon-"]::before, [class*=" flaticon-"]::before, [class^="flaticon-"]::after, [class*=" flaticon-"]::after {
                                width: 20px !important;
                                font-size: 20px !important;
                                padding: 0 10px 0 3px !important;
                                margin: 0;
                            }


                            .na_first ul a {
                                text-align: left !important;
                            }

                            .na_first ul h5 {
                                text-align: left !important;
                            }



                            .na_linqC2h {
                                background: green none repeat scroll 0 0;
                                height: 2px;
                                left: 317px;
                                position: absolute;
                                top: 135px;
                                width: 702px;
                            }

                            .na_Importlogo {
                                height: 40px;
                                left: 720px;
                                position: absolute;
                                text-align: center;
                                top: 0px;
                                width: 40px;
                            }

                            .na_Settinglogo {
                                height: 43px;
                                left: 3px;
                                position: absolute;
                                top: 0px;
                                width: 45px;
                            }

                            .na_Saleslogo {
                                height: 43px;
                                left: 262px;
                                position: absolute;
                                top: 0px;
                                width: 45px;
                            }


                            .na_purlogo {
                                height: 43px;
                                left: 327px;
                                position: absolute;
                                top: 0px;
                                width: 45px;
                            }

                            .na_Production {
                                height: 40px;
                                left: 790px;
                                position: absolute;
                                text-align: center;
                                top: 0px;
                                width: 40px;
                            }

                            .na_Aclogo {
                                height: 40px;
                                left: 863px;
                                position: absolute;
                                top: 0px;
                                width: 40px;
                            }

                            .na_overalllogo {
                                height: 4px;
                                left: 935px;
                                position: absolute;
                                text-align: center;
                                top: 0px;
                                width: 40px;
                            }

                            .na_overalllogo2 {
                                height: 4px;
                                left: 1048px;
                                position: absolute;
                                text-align: center;
                                top: 0px;
                                width: 54px;
                            }

                            .inttext {
                                color: #000;
                                display: block;
                                left: 22px;
                                top: 1px !important;
                                margin-left: -8px;
                                text-align: center;
                            }


                            #rbtnList1 input[type="checkbox"], #rbtnList1 input[type="radio"] {
                                z-index: -1;
                            }

                            .h4, .h5, .h6, h4, h5, h6 {
                                margin-bottom: 10px;
                                margin-top: 2px !important;
                            }
                        </style>

                        <div id="na_wrawpr">

                            <div class="na_Settinglogo">
                                <a href="<%=this.ResolveUrl("~/DeafultMenu.aspx?Type=4106")%>">
                                    <img src="Image/ovinterface.png" height="30" width="30" alt="comlogo" />
                                    <span class="inttext">Settings</span>
                                </a>
                            </div>


                            <div class="na_Importlogo">
                                <a href="<%=this.ResolveUrl("~/DeafultMenu.aspx?Type=4106")%>">
                                    <img src="Image/int1.png" height="40" width="40" alt="comlogo" />
                                    <span class="inttext">Import</span>
                                </a>
                            </div>


                            <div class="na_Saleslogo">
                                <a href="<%=this.ResolveUrl("~/F_23_SaM/SalesInformation.aspx")%>">
                                    <img src="Image/int1.png" height="40" width="40" alt="comlogo" />
                                    <span class="inttext">Sales</span>
                                </a>
                            </div>

                            <div class="na_purlogo">
                                <a href="<%=this.ResolveUrl("~/F_11_Pro/PurInformation.aspx")%>">
                                    <img src="Image/int1.png" height="40" width="40" alt="comlogo" />
                                    <span class="inttext">Purchase</span>

                                </a>
                            </div>

                            <div class="na_Production">
                                <a href="<%=this.ResolveUrl("~/F_13_ProdMon/ProductionInfo.aspx")%>">
                                    <img src="Image/int1.png" height="40" width="40" alt="comlogo" />
                                    <span class="inttext">Production</span>
                                </a>
                            </div>

                            <div class="na_Aclogo">

                                <a href="<%=this.ResolveUrl("~/F_15_Acc/AccDashBoard.aspx")%>">

                                    <img src="Image/int1.png" height="40" width="40" alt="comlogo" />
                                    <span class="inttext">Accounts</span>
                                </a>
                            </div>

                            <div class="na_overalllogo">
                                <a href="<%=this.ResolveUrl("~/F_33_Mgt/DashBoardAll.aspx")%>">

                                    <img src="Image/int1.png" height="40" width="40" alt="comlogo" />
                                    <span class="inttext">Overall</span>
                                </a>
                            </div>

                            <div class="na_overalllogo2">
                                <a href="<%=this.ResolveUrl("~/GenPage.aspx?Type=All")%>">

                                    <img src="Image/ovinterface.png" height="30" width="30" alt="comlogo" />
                                    <span class="inttext">All Reports </span>
                                </a>
                            </div>


                            <div class="na_linqC6a"></div>
                            <div class="na_linqC6b"></div>

                            <div class="na_linqC6c"></div>
                            <div class="na_linqC6d"></div>

                            <div class="na_linqC6e"></div>

                            <div class="na_linqC6f"></div>
                            <div class="na_linqC6g"></div>

                            <div class="na_linqC8a"></div>
                            <div class="na_linqC8b"></div>

                            <div class="na_linqC8c"></div>
                            <div class="na_linqC8d"></div>
                            <%-- <div class="na_linqC8e"></div>--%>

                            <div class="na_linqC3a"></div>
                            <div class="na_linqC3b"></div>

                            <div class="na_linqC3c"></div>
                            <div class="na_linqC3d"></div>
                            <div class="na_linqC3e"></div>
                            <div class="na_linqC3f"></div>
                            <div class="na_linqC3g"></div>

                            <div class="na_linqC1a"></div>
                            <div class="na_linqC1b"></div>

                            <div class="na_linqC2e"></div>
                            <div class="na_linqC2f"></div>
                            <div class="na_linqC2g"></div>

                            <div class="na_linqC2h"></div>
                            <div class="na_linqC2i"></div>

                            <%--<div class="na_linqC1a"></div>

                 
                    <div class="na_linqC8k"></div>

                    <div class="na_comlogo">
                        <img src="housing3.PNG"  height="28" width="171" alt="comlogo"/>
                    </div>--%>


                            <div class="na_first">
                                <ul>
                                    <li></li>
                                    <li></li>

                                    <li></li>

                                    <li></li>
                                    <li></li>
                                    <li></li>
                                    <li></li>
                                    <li>
                                        <h5><span class="glyph-icon flaticon-budget nfal"></span>Import Management</h5>
                                    </li>
                                    <li><span class="fa down_dr fa-long-arrow-down"></span></li>
                                    <%-- <li><a href="<%=this.ResolveUrl("~/F_07_Inv/PurMktSurvey.aspx?Type=MktSurvey")%>"><span class="glyph-icon flaticon-tray30 nfal"></span>CS Preparation</a></li>--%>

                                    <li><a href="<%=this.ResolveUrl("~/F_09_LCM/LCInformation.aspx?tname=order&tid=lc")%>"><span class="glyph-icon flaticon-graph4 nfal"></span>L/C Open & Monitor</a></li>
                                    <li><a href="<%=this.ResolveUrl("~/F_15_Acc/GeneralAccounts.aspx?tcode=99&tname=Payment Voucher&Mod=Accounts")%>"><span class="glyph-icon flaticon-soccer68 nfal"></span>Payments</a></li>

                                    <li><a href="<%=this.ResolveUrl("~/F_09_LCM/LCInformation.aspx?tname=receive&tid=lc")%>"><span class="glyph-icon flaticon-discount5 nfal"></span>Materials Receive (L/C)</a></li>
                                    <li><a href="<%=this.ResolveUrl("~/F_09_LCM/LCCostingDetails.aspx?Type=Entry")%>"><span class="glyph-icon flaticon-data45 nfal"></span>L/C Costing</a></li>
                                     
                                    <li><a href="<%=this.ResolveUrl("~/F_15_Acc/AccPurchaseFor.aspx")%>"><span class="glyph-icon flaticon-coins36 nfal"></span>Update- L/C Cost</a></li>
                                  
                                   
                                    <li></li>
                                    <%-- <li><span class="fa down_dr fa-long-arrow-down"></span></li>--%>
                                    <li></li>
                                    <li></li>
                                    <%-- <li>
                                        <h5><span class="glyph-icon flaticon-chart44 nfal"></span>Digital CRM</h5>
                                    </li>--%>
                                    <li><a href="<%=this.ResolveUrl("~/F_03_StdCost/MatAvailability.aspx?Type=FG")%>"><span class="glyph-icon flaticon-calculating11 nfal"></span>Materials Availability</a></li>
                                    <li><a href="<%=this.ResolveUrl("~/F_11_Pro/ProdReq.aspx")%>"><span class="glyph-icon flaticon-data45 nfal"></span>Production Planning</a></li>
                                  
                                </ul>
                            </div>

                            <div class="na_first2">
                                <ul>
                                    <li></li>
                                    <li></li>
                                    <li></li>
                                    <li><a href="<%=this.ResolveUrl("~/")%>"></a></li>
                                    <li><a href="<%=this.ResolveUrl("~/")%>"></a></li>
                                    <li><a href="<%=this.ResolveUrl("~/")%>"></a></li>
                                    <li><a href="<%=this.ResolveUrl("~/")%>"></a></li>
                                    <li><span class="fa fa-long-arrow-left  left_dr1 fa-fw"></span></li>
                                    <li><a href="<%=this.ResolveUrl("~/")%>"></a></li>

                                    <li><a href="<%=this.ResolveUrl("~/")%>"></a></li>
                                    <li><a href="<%=this.ResolveUrl("~/")%>"></a></li>
                                    <li><a href="<%=this.ResolveUrl("~/")%>"></a></li>
                                    <li><a href="<%=this.ResolveUrl("~/")%>"></a></li>
                                    <li><a href="<%=this.ResolveUrl("~/")%>"></a></li>
                                    <li></li>
                                    <li></li>

                                    <li><a href="<%=this.ResolveUrl("~/")%>"></a></li>
                                    <li><a href="<%=this.ResolveUrl("~/")%>"></a></li>
                                    <li><a href="<%=this.ResolveUrl("~/")%>"></a></li>
                                    <%--<li><span class="fa fa-long-arrow-left  left_dr1 fa-fw"></span></li>--%>
                                    <li><a href="<%=this.ResolveUrl("~/")%>"></a></li>
                                    <li><a href="<%=this.ResolveUrl("~/")%>"></a></li>
                                </ul>
                            </div>

                            <div class="na_first">
                                <ul>
                                    <li></li>
                                    <li></li>


                                    <li><a href="<%=this.ResolveUrl("~/F_03_StdCost/StdCostSheet.aspx?InputType=CostAnna")%>"><span class="glyph-icon flaticon-data79 nfal"></span>Standard Analysis</a></li>
                                    <li></li>

                                    <li><span class="fa down_dr fa-long-arrow-down"></span></li>

                                    <li>
                                        <h4><a href="<%=this.ResolveUrl("~/F_07_Inv/StockPosition.aspx")%>"><span class="glyph-icon flaticon-international36 nfal"></span>Materials Inventory</a></h4>
                                    </li>

                                    <%--<li>
                                        <h5 class="MMenu"></h5>
                                    </li>--%>
                                    <li><span class="fa down_dr fa-long-arrow-down"></span></li>
                                    <li>
                                        <h5><span class="glyph-icon flaticon-logistics3 nfal"></span>Procurement</h5>
                                    </li>
                                    <li><span class="fa down_dr fa-long-arrow-down"></span></li>

                                    <li><a href="<%=this.ResolveUrl("~/F_07_Inv/PurReqEntry02.aspx?InputType=FxtAstEntry&actcode=&genno=")%>"><span class="glyph-icon flaticon-payment5 nfal"></span>Requisition Create</a></li>
                                    
                                    <li><a href="<%=this.ResolveUrl("~/F_07_Inv/PurMktSurvey02.aspx?Type=Create&genno=")%>"><span class="glyph-icon flaticon-research1 nfal"></span>CS Preparation</a></li>
                                    <li><a href="<%=this.ResolveUrl("~//F_07_Inv/PurMktSurvey02.aspx?Type=Check&genno=")%>"><span class="glyph-icon flaticon-information58 nfal"></span>CS Check</a></li>

                                    <li><a href="<%=this.ResolveUrl("~/F_07_Inv/PurMktSurvey02.aspx?Type=Approved&genno=")%>"><span class="glyph-icon flaticon-credit56 nfal"></span>CS Approval</a></li>
                                    <li><a href="<%=this.ResolveUrl("~/F_07_Inv/PurAprovEntry.aspx?InputType=PurProposal&genno=")%>"><span class="glyph-icon flaticon-shopping3 nfal"></span>Order Process</a></li>
                                    <li><a href="<%=this.ResolveUrl("~/F_11_Pro/PurWrkOrderEntry.aspx?InputType=OrderEntry&genno=&actcode=")%>"><span class="glyph-icon flaticon-coins36 nfal"></span>Purchase Order</a></li>
                                    <li><a href="<%=this.ResolveUrl("~/F_07_Inv/PurMRREntry.aspx?Type=Entry&genno=")%>"><span class="glyph-icon flaticon-home183 nfal"></span>Received</a></li>

                                    <li><a href="<%=this.ResolveUrl("~/F_07_Inv/PurQCEntry.aspx?Type=Entry&genno=")%>"><span class="glyph-icon flaticon-customerservice19 nfal"></span>QC Check</a></li>
                                    <li><a href="<%=this.ResolveUrl("~/F_07_Inv/PurBillEntry.aspx?Type=BillEntry&genno=&sircode=")%>"><span class="glyph-icon flaticon-bank5 nfal"></span>Bill Confirmation</a></li>

                                    <li><a href="<%=this.ResolveUrl("~/F_15_Acc/AccPurchase.aspx")%>"><span class="glyph-icon flaticon-telephone172 nfal"></span>Update-Purchase </a></li>
                                    <li><a href="<%=this.ResolveUrl("~/F_15_Acc/SuplierPayment.aspx?tcode=99&tname=Payment Voucher&Mod=Accounts")%>"><span class="glyph-icon flaticon-payment5 nfal"></span>Supplier's payment</a></li>


                                </ul>
                            </div>

                            <div class="na_first2">
                                <ul>
                                    <li></li>
                                    <li></li>
                                    <li></li>
                                    <li><a href="<%=this.ResolveUrl("~/")%>"></a></li>
                                    <li><a href="<%=this.ResolveUrl("~/")%>"></a></li>
                                    <li><span class="fa left_dr fa-long-arrow-left"></span></li>
                                    <li><a href="<%=this.ResolveUrl("~/")%>"></a></li>
                                    <li><a href="<%=this.ResolveUrl("~/")%>"></a></li>
                                    <li><a href="<%=this.ResolveUrl("~/")%>"></a></li>
                                    <li><a href="<%=this.ResolveUrl("~/")%>"></a></li>
                                    <li><a href="<%=this.ResolveUrl("~/")%>"></a></li>
                                    <li><a href="<%=this.ResolveUrl("~/")%>"></a></li>
                                    <li><a href="<%=this.ResolveUrl("~/")%>"></a></li>
                                    <%-- <li><span class="fa right_dr fa-long-arrow-right"></span></li>--%>
                                    <li><a href="<%=this.ResolveUrl("~/")%>"></a></li>
                                    <li><a href="<%=this.ResolveUrl("~/")%>"></a></li>
                                    <li><a href="<%=this.ResolveUrl("~/")%>"></a></li>
                                    <li><a href="<%=this.ResolveUrl("~/")%>"></a></li>
                                    <li><a href="<%=this.ResolveUrl("~/")%>"></a></li>
                                    <li><a href="<%=this.ResolveUrl("~/")%>"></a></li>
                                    <li><a href="<%=this.ResolveUrl("~/")%>"></a></li>
                                    <li><a href="<%=this.ResolveUrl("~/")%>"></a></li>
                                    <li><a href="<%=this.ResolveUrl("~/")%>"></a></li>
                                    <li><a href="<%=this.ResolveUrl("~/")%>"></a></li>


                                </ul>
                            </div>

                            <div class="na_first">
                                <ul>
                                    <li>
                                        <h5><span class="glyph-icon flaticon-draft nfal"></span>Manufacturing ERP</h5>
                                    </li>
                                   
                                    <li></li>
                                   
                                    <li></li>
                                    <li><span class="fa down_dr fa-long-arrow-down"></span></li>
                                     <li></li>
                                    <li>
                                        <h5><span class="glyph-icon flaticon-house118 nfal"></span>Sales Management</h5>
                                    </li>
                                    <li><span class="fa down_dr fa-long-arrow-down"></span></li>
                                    <li><a href="<%=this.ResolveUrl("~/F_23_SaM/SalesComProposal.aspx")%>"><span class="glyph-icon flaticon-domain2 nfal"></span>Commercial Proposal</a></li>
                                    <li><a href="<%=this.ResolveUrl("~/F_23_SaM/AllSaleOrderList.aspx?Type=All")%>"><span class="glyph-icon flaticon-contract11 nfal"></span>Sales Order</a></li>
                                    <li><a href="<%=this.ResolveUrl("~/F_23_SaM/SalesOrderApproval.aspx?Type=All")%>"><span class="glyph-icon flaticon-website17 nfal"></span>Sales Order Approval</a></li>
                                    <li><a href="<%=this.ResolveUrl("~/F_23_SaM/DeliveryOrder02.aspx?Type=Entry")%>"><span class="glyph-icon flaticon-register nfal"></span>DO - Sales Store </a></li>
                                    <li><a href="<%=this.ResolveUrl("~/F_23_SaM/DelOrderApproval.aspx?Type=Payment")%>"><span class="glyph-icon flaticon-register nfal"></span>Payment Confirmation</a></li>
                                    <li><a href="<%=this.ResolveUrl("~/F_23_SaM/DelOrderApproval.aspx?Type=Delivery")%>"><span class="glyph-icon flaticon-register nfal"></span>Delivery Confirmation</a></li>

                                    <li><a href="<%=this.ResolveUrl("~/F_23_SaM/InvListUpdate.aspx")%>"><span class="glyph-icon flaticon-list23 nfal"></span>Invoice / Bill</a></li>
                                    <li><a href="<%=this.ResolveUrl("~/F_23_SaM/AllCollection.aspx?Type=All")%>"><span class="glyph-icon flaticon-shoppingcart39 nfal"></span>Collection</a></li>
                                    <li><a href="<%=this.ResolveUrl("~/F_15_Acc/AccSalesJournal.aspx")%>"><span class="glyph-icon flaticon-stocks2 nfal"></span>Update - Sales </a></li>
                                    <li><a href="<%=this.ResolveUrl("~/F_15_Acc/AccSales.aspx")%>"><span class="glyph-icon flaticon-data62 nfal"></span>Update-Collection </a></li>
                                    <li></li>




                                </ul>
                            </div>

                            <div class="na_first2">
                                <ul>
                                    <li></li>
                                    <li></li>
                                    <li></li>
                                    <li></li>
                                    <li><a href="<%=this.ResolveUrl("~/")%>"></a></li>
                                    <li><span class="fa left_dr fa-long-arrow-left"></span></li>
                                    <li><a href="<%=this.ResolveUrl("~/")%>"></a></li>
                                    <li><a href="<%=this.ResolveUrl("~/")%>"></a></li>
                                    <li><a href="<%=this.ResolveUrl("~/")%>"></a></li>
                                    <li><a href="<%=this.ResolveUrl("~/")%>"></a></li>
                                    <li><a href="<%=this.ResolveUrl("~/")%>"></a></li>
                                    <li></li>
                                    <%--  <li><span class="fa left_dr fa-long-arrow-left"></span></li>--%>
                                    <li><a href="<%=this.ResolveUrl("~/")%>"></a></li>
                                    <%--  <li><span class="fa right_dr  right2dr fa-long-arrow-right"></span></li>--%>
                                    <li><a href="<%=this.ResolveUrl("~/")%>"></a></li>
                                    <li><a href="<%=this.ResolveUrl("~/")%>"></a></li>
                                    <li><a href="<%=this.ResolveUrl("~/")%>"></a></li>
                                    <li><a href="<%=this.ResolveUrl("~/")%>"></a></li>
                                    <li><a href="<%=this.ResolveUrl("~/")%>"></a></li>
                                    <li><a href="<%=this.ResolveUrl("~/")%>"></a></li>
                                    <%--<li><span class="fa right_dr fa-long-arrow-right"></span></li>--%>
                                    <li><span class="fa fa-long-arrow-up  left_dr1 fa-fw"></span></li>
                                    <li><a href="<%=this.ResolveUrl("~/")%>"></a></li>
                                    <li><a href="<%=this.ResolveUrl("~/")%>"></a></li>
                                    <li><a href="<%=this.ResolveUrl("~/")%>"></a></li>
                                </ul>
                            </div>
                            <div class="na_first">
                                <ul>
                                   <li></li>
                                    <li></li>
                                    <li>
                                        <h4><a href="<%=this.ResolveUrl("~/F_01_BPlan/YearlyPlanningBudget.aspx?Type=Yearly")%>"><span class="glyph-icon flaticon-data79 nfal"></span>Business Plan</a></h4>
                                        <%--F_01_BPlan/YearlyPlanningBudget.aspx?Type=Yearly--%>
                                    </li>
                                    <li><span class="fa down_dr fa-long-arrow-down"></span></li>
                                    <li><span class="fa down_dr fa-long-arrow-down"></span></li>
                                    <li>
                                        <h4><a href="<%=this.ResolveUrl("~/F_19_FGInv/StockPositionProd.aspx")%>"><span class="glyph-icon flaticon-lightbulbs13 nfal"></span>Goods Inventory</a></h4>
                                    </li>
                                    <li><span class="fa down_dr fa-long-arrow-down"></span></li>
                                    <li>
                                        <h5><span class="glyph-icon flaticon-lightbulbs13 nfal"></span>Production</h5>
                                    </li>
                                    <li><span class="fa down_dr fa-long-arrow-down"></span></li>
                                    <%-- <li><a href="<%=this.ResolveUrl("~/F_23_SaM/MonthlySalesEntry.aspx")%>"><span class="glyph-icon flaticon-money483 nfal"></span>Monthly Sales Target</a></li>--%>
                                    <li><a href="<%=this.ResolveUrl("~/F_03_StdCost/ProdBudget.aspx?Type=Entry")%>"><span class="glyph-icon flaticon-data62 nfal"></span>Production Budget</li>


                                    <li><a href="<%=this.ResolveUrl("~/F_11_Pro/ProdReq.aspx")%>"><span class="glyph-icon flaticon-report1 nfal"></span>Production Requisition</a></li>
                                    <li><a href="<%=this.ResolveUrl("~/F_07_Inv/PBMatIssueSingle.aspx")%>"><span class="glyph-icon flaticon-truck29 nfal"></span>Resources Issue</a></li>

                                    <li><a href="<%=this.ResolveUrl("~/F_13_ProdMon/ProProcessTrans.aspx")%>"><span class="glyph-icon flaticon-waves5 nfal"></span>QC</a></li>
                                    <li><a href="<%=this.ResolveUrl("~/F_13_ProdMon/ProductionPlan.aspx")%>"><span class="glyph-icon flaticon-bank5 nfal"></span>Production Entry</a></li>
                                    <li><a href="<%=this.ResolveUrl("~/F_19_FGInv/ProReceive.aspx")%>"><span class="glyph-icon flaticon-waves5 nfal"></span>Update-FGS After QC</a></li>
                                    <li><a href="<%=this.ResolveUrl("~/F_15_Acc/AccIsuUpdate.aspx")%>"><span class="glyph-icon flaticon-copy32 nfal"></span>Update- Issue</a></li>
                                    <li><a href="<%=this.ResolveUrl("~/F_15_Acc/AccProductionJV.aspx")%>"><span class="glyph-icon flaticon-data98 nfal"></span>Update- WIP</a></li>
                                    <%--  <li><span class="fa down_dr fa-long-arrow-down"></span></li>--%>
                                    <li></li>
                                    <li>
                                        <h5><span class="glyph-icon flaticon-lightbulbs13 nfal"></span>Distribution</h5>
                                    </li>
                                    <li><a href="<%=this.ResolveUrl("~/")%>"><span class="glyph-icon flaticon-contract11 nfal"></span>Requisition</a></li>
                                    <li><a href="<%=this.ResolveUrl("~/F_23_SaM/Chalan.aspx")%>"><span class="glyph-icon flaticon-waves5 nfal"></span>Product Transfer</a></li>
                                    <li><a href="<%=this.ResolveUrl("~/F_15_Acc/AccChalanTransfer.aspx")%>"><span class="glyph-icon flaticon-telephone172 nfal"></span>Update-Transfer</a></li>


                                    <li></li>
                                    <%--  <li><a href="<%=this.ResolveUrl("~/F_33_Mgt/DashBoardAll.aspx")%>"><span class="glyph-icon flaticon-report1 nfal"></span>Dashboard</a></li>
                                    <li><a href="<%=this.ResolveUrl("~/F_23_SaM/SalesInformation.aspx")%>"><span class="glyph-icon flaticon-house118 nfal"></span>Sales</a></li>
                                    <li><a href="<%=this.ResolveUrl("~/F_11_Pro/PurInformation.aspx")%>"><span class="glyph-icon flaticon-dollar178 nfal"></span>Purchase</a></li>
                                    <li><a href="<%=this.ResolveUrl("~/F_15_Acc/AccDashBoard.aspx")%>"><span class="glyph-icon flaticon-settings49 nfal"></span>Accounts</a></li>
                                    <li></li>--%>
                                </ul>
                            </div>

                            

                            <div class="na_first2">
                                <ul>
                                    <li></li>
                                    <li></li>
                                    <li><a href="<%=this.ResolveUrl("~/")%>"></a></li>
                                    <li><a href="<%=this.ResolveUrl("~/")%>"></a></li>
                                    <li></li>
                                    <li></li>
                                    <li></li>
                                    <li><a href="<%=this.ResolveUrl("~/")%>"></a></li>
                                    <li><a href="<%=this.ResolveUrl("~/")%>"></a></li>
                                    <li><a href="<%=this.ResolveUrl("~/")%>"></a></li>
                                    <li><a href="<%=this.ResolveUrl("~/")%>"></a></li>
                                    <li><a href="<%=this.ResolveUrl("~/")%>"></a></li>
                                    <li><a href="<%=this.ResolveUrl("~/")%>"></a></li>
                                    <li><a href="<%=this.ResolveUrl("~/")%>"></a></li>
                                    <li><a href="<%=this.ResolveUrl("~/")%>"></a></li>
                                    <li><a href="<%=this.ResolveUrl("~/")%>"></a></li>
                                    <li><a href="<%=this.ResolveUrl("~/")%>"></a></li>
                                    <li><a href="<%=this.ResolveUrl("~/")%>"></a></li>
                                    <li><a href="<%=this.ResolveUrl("~/")%>"></a></li>
                                    <li><a href="<%=this.ResolveUrl("~/")%>"></a></li>
                                    <li><a href="<%=this.ResolveUrl("~/")%>"></a></li>
                                    <%--<li><span class="fa left_dr fa-long-arrow-left"></span></li>
                                    <li><span class="fa  right_dr fa-long-arrow-right"></span></li>
                                    <li><span class="fa  right_dr fa-long-arrow-right"></span></li>                                
                                    <li><span class="fa  right_dr fa-long-arrow-right"></span></li>                                   
                                    <li><span class="fa right_dr fa-long-arrow-right"></span></li>--%>
                                    <li><a href="<%=this.ResolveUrl("~/")%>"></a></li>


                                </ul>
                            </div>

                            <div class="na_first">
                                <ul>
                                    <li></li>
                                    <li></li>
                                    <li></li>
                                    <li></li>
                                    <li><span class="fa down_dr fa-long-arrow-down"></span></li>
                                    <li>
                                        <h5><span class="glyph-icon flaticon-settings49 nfal"></span>Finance & Accounts</h5>
                                    </li>
                                    <li><span class="fa down_dr fa-long-arrow-down"></span></li>
                                    <%--  <li><a href="<%=this.ResolveUrl("~/F_01_BPlan/YearlyPlanningBudget.aspx?Type=Yearly")%>"><span class="glyph-icon flaticon-marketing8 nfal"></span>Annual Business Plan</a></li>--%>
                                    <%--<li><a href="<%=this.ResolveUrl("~/F_15_Acc/AccMonthlyBgd.aspx")%>"><span class="glyph-icon flaticon-full22 nfal"></span>Working Budget</a></li>--%>
                                    <li><a href="<%=this.ResolveUrl("~/F_15_Acc/GeneralAccounts.aspx?tcode=99&tname=Payment Voucher&Mod=Accounts")%>"><span class="glyph-icon flaticon-discount5 nfal"></span>Payment Voucher</a></li>
                                    <li><a href="<%=this.ResolveUrl("~/F_15_Acc/GeneralAccounts.aspx?tcode=99&tname=Deposit Voucher&Mod=Accounts")%>"><span class="glyph-icon flaticon-news29 nfal"></span>Deposit Voucher</a></li>
                                    <li><a href="<%=this.ResolveUrl("~/F_15_Acc/GeneralAccounts.aspx?tcode=99&tname=Journal Voucher&Mod=Accounts")%>"><span class="glyph-icon flaticon-creditcard7 nfal"></span>Journal Voucher</a></li>
                                    <li><a href="<%=this.ResolveUrl("~/F_15_Acc/AccInterComVoucher.aspx")%>"><span class="glyph-icon flaticon-money405 nfal"></span>Inter Company Payment</a></li>
                                    <li><span class="fa down_dr fa-long-arrow-down"></span></li>
                                    <li><a href="<%=this.ResolveUrl("~/")%>"><span class="glyph-icon flaticon-magnifyingglass27 nfal"></span>Plan Vs Acheivement</a></li>
                                    <li></li>
                                    <li><a href="<%=this.ResolveUrl("~/F_27_Mis/RptBgdVsActPro.aspx?Type=Income")%>"><span class="glyph-icon flaticon-thumb32 nfal"></span>Performance </a></li>
                                    <%-- <li><a href="<%=this.ResolveUrl("~/F_23_SaM/SalesWiseVat.aspx")%>"><span class="glyph-icon flaticon-magnifyingglass27 nfal"></span>VAT Accounts (Auto)</a></li>
                                    --%>
                                    <%-- <li><a href="<%=this.ResolveUrl("~/RptDefault.aspx")%>"><span class="glyph-icon flaticon-checkmark11 nfal"></span>All Reports</a></li>
                                    <li><a href="<%=this.ResolveUrl("~/DeafultMenu.aspx?Type=4106")%>"><span class="glyph-icon flaticon-settings48 nfal"></span>Settings</a></li>--%>
                                </ul>
                            </div>

                        </div>

                    </asp:Panel>
                </div>

            </div>






        </div>
    </div>
</asp:Content>

