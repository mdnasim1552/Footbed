<%@ Page Title="" Language="C#" MasterPageFile="~/SPE.Master" AutoEventWireup="true" CodeBehind="HRGenOffdaySetup.aspx.cs" Inherits="SPEWEB.F_81_Hrm.F_92_Mgt.HRGenOffdaySetup" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <script type="text/javascript" language="javascript">
       
        $(document).ready(function () {

            var offdata = "";
            GETData();

            //$("input, select").bind("keydown", function (event) {
            //    var k1 = new KeyPress();
            //    k1.textBoxHandler(event);
            //});
            $('.chzn-select').chosen({ search_contains: true });
           // Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoaded);          
          
        });
        function SaveData() {

            let emptype = $("#ddlWstation option:selected").val();
            let monthid = $("#ddlMonth option:selected").val();
           // let notes = $('textarea#Notes').val(); 
            var notes = $("textarea#Notes").val(); 
            //console.log(monthid);
            console.log(notes);
            var table = document.getElementById("gvTable");
           
            for (var i = 1; i < table.rows.length; i++) {
                var reason = table.rows[i].cells[2].children[0].value;
                var weekend = (table.rows[i].cells[3].children[0].checked)?"True":"False";
                var holiday = (table.rows[i].cells[4].children[0].checked)?"True":"False";
                
                    offdata[i - 1]['reason'] = reason;
                    offdata[i - 1]['wekend'] = weekend;
                    offdata[i - 1]['holiday'] = holiday;
               
            }         
            offdata = offdata.filter(x => x.wekend == 'True' || x.holiday == 'True');
         
            $.ajax({
                type: "POST",
                url: "HRGenOffdaySetup.aspx/SaveEmpOffDays",
                contentType: "application/json; charset=utf-8",
                data: "{'offdata':" + JSON.stringify(offdata) + ",'emptype':" + emptype + ",'monthid':" + monthid + ",'remarks':'" + notes + "'}",      
                dataType: "json",
                success: function (response) {
                    //  console.log(JSON.parse(response.d));
                    alert(response.d);

                },
                failure: function (response) {
                    //  alert(response);
                    alert(response.d);
                },
                error: function (xhr, status, error) {
                    console.log(error)
                }
            });
        }
        function GETData() {
           
            let emptype = $("#ddlWstation option:selected").val();
            let monthid = $("#ddlMonth option:selected").val();
           
            $.ajax({
                type: "POST",
                url: "HRGenOffdaySetup.aspx/GetMonCalenderStatic",
                data: "{'emptype':" + emptype + ",'monthid':" + monthid + "}",              
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    console.log(JSON.parse(response.d));
                    offdata = JSON.parse(response.d);
                    //alert("True");
                    $("#gvTable").empty();

                    if (offdata.length > 0) {

                        $("#gvTable").append("<tr><th>Date</th> <th>Day</th><th>Reason</th><th>Weekend</th><th>Holiday</th></tr>");
                       
                       
                        for (var i = 0; i < offdata.length; i++) {
                            var wstatus = "";
                            var hstatus = "";
                            if (offdata[i].wekend == "True") {
                                 wstatus = "checked";
                            }
                            if (offdata[i].holiday == "True") {
                                hstatus = "checked";
                            }
                            //var wstatus = offdata[i].wekend;
                            $("#gvTable").append("<tr><td>" +
                        offdata[i].sdate + "</td> <td>" +
                        offdata[i].sdate1 + "</td> <td><input type='Text' class='form-control form-control-sm'  value='" +
                        offdata[i].reason + "'></td>"+
                        "<td><input type='checkbox' " + wstatus + "></td>" +                   
                        "<td><input type='checkbox' " + hstatus + "></td></tr>");
                        }
                      
                        $("textarea#Notes").val(offdata[0].notes);
                       
                    }
                },
                failure: function (response) {
                    //  alert(response);
                    alert("Sorry!");
                }
            });
        }
        function pageLoaded() {           
        }
    </script>

    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>

              <div class="card card-fluid">
                <div class="card-body">
                    <div class="row">                           
                            <div class="col-md-2 col-sm-2 col-lg-2">
                                <div class="form-group">
                                   
                                        <asp:Label ID="Label18" runat="server" CssClass="label">Employee Type</asp:Label>
                                        <asp:DropDownList ID="ddlWstation" ClientIDMode="Static" runat="server"  CssClass="chzn-select form-control form-control-sm" AutoPostBack="true" TabIndex="2"></asp:DropDownList>
                                    </div>
                                 </div>
                                    <div class="col-md-1 col-sm-1 col-lg-1">
                                           <div class="form-group">
                                        <asp:Label ID="Label1" runat="server" CssClass="label">Select Year</asp:Label>
                                        <asp:DropDownList ID="ddlyear" runat="server" CssClass="chzn-select form-control form-control-sm" TabIndex="2">
                                            <asp:ListItem>2019</asp:ListItem>
                                            <asp:ListItem>2020</asp:ListItem>
                                            <asp:ListItem>2021</asp:ListItem>
                                            <asp:ListItem>2022</asp:ListItem>
                                            <asp:ListItem>2023</asp:ListItem>
                                            <asp:ListItem>2024</asp:ListItem>
                                            <asp:ListItem>2025</asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                          </div>

                                    <div class="col-md-2 col-sm-2 col-lg-2">
                                        <div class="form-group">
                                            <asp:Label ID="Label2" runat="server" CssClass="label">Month</asp:Label>
                                             <asp:DropDownList ID="ddlMonth" ClientIDMode="Static" OnSelectedIndexChanged="ddlMonth_SelectedIndexChanged" AutoPostBack="true" runat="server" Width="205" CssClass="chzn-select form-control form-control-sm" TabIndex="2">
                                            </asp:DropDownList>
                                        </div>
                                    </div>

                                
                          </div>
                         
                        </div>
                      </div>
            <div class="card card-fluid">
                <div class="card-body">
                    <div class="row">  

                     <div class="col-md-6">
                    <table id="gvTable" class="table-striped table-hover table-bordered grvContentarea" >
                      
                    </table>
                         <button class="btn btn-success btn-xs " type="button" onclick="return SaveData(); ">Update</button>
                        </div>     
                     <div class="col-md-6">
                         <label class="label label-info">Notes</label>
                         <textarea rows="10" class="form-control" id="Notes"></textarea>
                         </div> 
                    </div>
                </div>
           </div>
            </ContentTemplate>
        </asp:UpdatePanel>
</asp:Content>

