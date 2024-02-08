/*
  Author:Shamim Shahrier Emon
  Date:07/08/2015

  steps:
    1.link this file in aspx
    2. Declare following variable in aspx
        var sdate = $('#txtDate').val();
        var endDate = $('#txttodate').val();
    3.Call thease function whenever dates are required.
    
     
*/

function GetStartDate() {
    sdate = $('#txtDate').val();
}
function GetEndDate() {
    endDate = $('#txttodate').val();
}