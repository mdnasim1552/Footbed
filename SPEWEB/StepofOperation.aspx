<%@ Page Title="" Language="C#" MasterPageFile="~/SPEMaster.Master" AutoEventWireup="true" CodeBehind="StepofOperation.aspx.cs" Inherits="SPEWEB.StepofOperation" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript">
        $(document).ready(function () {

            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(GetMenu);

        });         
        function GetMenu() {

            try {

                var Moduleid = $('#<%=this.txtmodid.ClientID%>').val();
                var leftul = $('#Leftul');
                leftul.html('');
                var Midul = $('#Midul');
                Midul.html('');
                var Rightul = $('#Rightul');
                Rightul.html('');

                //var Moduleid = "27";
                var lInputName = "02%";
                var MInputName = "03%";
                var RInputName = "04%";

                var Moduleobj = new RealERPScript();
                var leftlist = Moduleobj.GetModule(Moduleid, lInputName);
                var Midtlist = Moduleobj.GetModule(Moduleid, MInputName);
                var Righttlist = Moduleobj.GetModule(Moduleid, RInputName);

                /*left , Middle and Right UL */
                $.each(leftlist, function (index, leftlist) {
                    if (leftlist.itemslct == false) {
                        leftul.append('<li><h5>' + leftlist.itemdesc + '</h5></li>');

                    }
                    else if (leftlist.itemslct == true && leftlist.itemdesc == "") {
                            ;

                    }

                    else {
                        leftul.append('<li><a href=' + encodeURI(leftlist.itemurl) + '>' + leftlist.itemdesc + '</a></li>');
                    }


                });
                $.each(Midtlist, function (index, Midtlist) {
                    if (Midtlist.itemslct == false) {
                        Midul.append('<li><h5>' + Midtlist.itemdesc + '</h5></li>');

                    }
                    else if (Midtlist.itemslct == true && Midtlist.itemdesc == "") {
                            ;
                    }

                    else {

                        Midul.append('<li><a href=' + encodeURI(Midtlist.itemurl) + '>' + Midtlist.itemdesc + '</a></li>');
                    }


                });
                $.each(Righttlist, function (index, Righttlist) {
                    if (Righttlist.itemslct == false) {
                        Rightul.append('<li><h5>' + Righttlist.itemdesc + '</h5></li>');

                    }
                    else if (Righttlist.itemslct == true && Righttlist.itemdesc == "") {
                            ;
                    }

                    else {
                        Rightul.append('<li><a href=' + encodeURI(Righttlist.itemurl) + '>' + Righttlist.itemdesc + '</a></li>');
                    }


                });

                /* end left , Middle and Right UL */
                leftul.show();
                Midul.show();
                Rightul.show();
            }

            catch (e) {

                alert(e);
            }

        }




    </script>


   
       
            <div class="container lbl2SubMenu headTagh3 moduleItemWrp cstepopertion">
                <div class="row">
                    <div class="col-md-4">
                        <asp:DropDownList ID="ddlModuleName" class="form-control ClCompAndMod" runat="server" TabIndex="2" AutoPostBack="True" OnSelectedIndexChanged="ddlModuleName_SelectedIndexChanged"></asp:DropDownList>

                    </div>
                    <div class="col-md-4">
                        <h2>
                            <asp:Label ID="modulenam" runat="server">Quick Tour</asp:Label></h2>
                    </div>
                    <div class="col-md-4">
                        <asp:DropDownList ID="ddlCompanyName" class="form-control ClCompAndMod" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlCompanyName_SelectedIndexChanged" TabIndex="1"></asp:DropDownList>
                    </div>



                </div>
                <div class="row">
                    <div class="col-md-4">
                        <h3>A. 	One Time Inputs</h3>
                        <ul id="Leftul" class="nav colLeft">
                        </ul>
                    </div>
                    <div class="col-md-4">
                        <h3>B. 	Operational Menu</h3>
                        <ul id="Midul" class="nav colMid">
                        </ul>

                    </div>
                    <div class="col-md-4">
                        <h3>C. 	General Report</h3>
                        <ul id="Rightul" class="nav colRight">
                        </ul>
                    </div>
                </div>
                <asp:TextBox ID="txtmodid" runat="server" Style="display: none;"></asp:TextBox>
            </div>
</asp:Content>



